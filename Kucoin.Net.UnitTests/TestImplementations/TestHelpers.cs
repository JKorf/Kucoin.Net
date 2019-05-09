using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using Kucoin.Net;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Moq;
using Newtonsoft.Json;

namespace Kucoin.Net.UnitTests.TestImplementations
{
    public class TestHelpers
    {
        [ExcludeFromCodeCoverage]
        public static bool AreEqual(object self, object to, params string[] ignore)
        {
            if (self == null && to == null)
                return true;

            if ((self != null && to == null) || (self == null && to != null))
                return false;

            var type = self.GetType();
            if (type.IsArray)
            {
                var list = (Array) self;
                var listOther = (Array)to;
                for (int i = 0; i < list.Length; i++)
                {
                    if(!AreEqual(list.GetValue(i), listOther.GetValue(i)))
                        return false;
                }

                return true;
            }

            if (type.IsValueType || type == typeof(string))
                return Equals(self, to);

            var ignoreList = new List<string>(ignore);
            foreach (var pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (ignoreList.Contains(pi.Name))
                    continue;

                var selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                var toValue = type.GetProperty(pi.Name).GetValue(to, null);

                if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                {
                    if (!Equals(selfValue, toValue))
                        return false;
                    continue;
                }

                if (pi.PropertyType.IsClass)
                {
                    if (!AreEqual(selfValue, toValue, ignore))
                        return false;
                    continue;
                }

                if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    return false;
            }

            return true;
        }

        public static KucoinSocketClient CreateSocketClient(IWebsocket socket, KucoinSocketClientOptions options = null)
        {
            KucoinSocketClient client;
            client = options != null ? new KucoinSocketClient(options) : new KucoinSocketClient(new KucoinSocketClientOptions() { LogVerbosity = LogVerbosity.Debug, ApiCredentials = new KucoinApiCredentials("Test", "Test", "Test") });
            client.SocketFactory = Mock.Of<IWebsocketFactory>();
            Mock.Get(client.SocketFactory).Setup(f => f.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket);
            return client;
        }

        public static KucoinSocketClient CreateAuthenticatedSocketClient(IWebsocket socket, KucoinSocketClientOptions options = null)
        {
            KucoinSocketClient client;
            client = options != null ? new KucoinSocketClient(options) : new KucoinSocketClient(new KucoinSocketClientOptions(){ LogVerbosity = LogVerbosity.Debug, ApiCredentials = new KucoinApiCredentials("Test", "Test", "Test")});
            client.SocketFactory = Mock.Of<IWebsocketFactory>();
            Mock.Get(client.SocketFactory).Setup(f => f.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket);
            return client;
        }

        public static IKucoinClient CreateClient(KucoinClientOptions options = null)
        {
            IKucoinClient client;
            client = options != null ? new KucoinClient(options) : new KucoinClient(new KucoinClientOptions(){LogVerbosity = LogVerbosity.Debug});
            client.RequestFactory = Mock.Of<IRequestFactory>();
            return client;
        }

        public static IKucoinClient CreateAuthResponseClient(string response)
        {
            var client = (KucoinClient)CreateClient(new KucoinClientOptions(){ ApiCredentials = new KucoinApiCredentials("Test", "test", "Test")});
            SetResponse(client, response);
            return client;
        }


        public static IKucoinClient CreateResponseClient(string response, KucoinClientOptions options = null)
        {
            var client = (KucoinClient)CreateClient(options);
            SetResponse(client, response);
            return client;
        }

        public static IKucoinClient CreateResponseClient<T>(T response, KucoinClientOptions options = null)
        {
            var client = (KucoinClient)CreateClient(options);
            SetResponse(client, JsonConvert.SerializeObject(response));
            return client;
        }

        public static void SetResponse(RestClient client, string responseData)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.GetResponseStream()).Returns(responseStream);

            var request = new Mock<IRequest>();
            request.Setup(c => c.Headers).Returns(new WebHeaderCollection());
            request.Setup(c => c.Uri).Returns(new Uri("http://www.test.com"));
            request.Setup(c => c.GetResponse()).Returns(Task.FromResult(response.Object));
            request.Setup(c => c.GetRequestStream()).Returns(Task.FromResult((Stream)new MemoryStream()));

            var factory = Mock.Get(client.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(request.Object);
        }

        public static void SetErrorWithResponse(IKucoinClient client, string responseData, HttpStatusCode code)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var r = new Mock<HttpWebResponse>();
            r.Setup(x => x.GetResponseStream()).Returns(responseStream);
            var we = new WebException("", null, WebExceptionStatus.Success, r.Object);

            var request = new Mock<IRequest>();
            request.Setup(c => c.Headers).Returns(new WebHeaderCollection());
            request.Setup(c => c.GetResponse()).Throws(we);

            var factory = Mock.Get(client.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(request.Object);
        }


        public static T CreateObjectWithTestParameters<T>() where T: class
        {
            var type = typeof(T);
            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var result = Array.CreateInstance(elementType, 2);
                result.SetValue(GetTestValue(elementType, 0), 0);
                result.SetValue(GetTestValue(elementType, 1), 1);
                return (T)Convert.ChangeType(result, type);
            }
            else
            {
                var obj = Activator.CreateInstance<T>();
                return FillWithTestParameters(obj);
            }
        }

        public static T FillWithTestParameters<T>(T obj) where T : class
        {
            var properties = obj.GetType().GetProperties();
            int i = 1;
            foreach (var property in properties)
            {
                var value = GetTestValue(property.PropertyType, i);
                Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                property.SetValue(obj, safeValue, null);
                i++;
            }

            return obj;
        }

        public static object[] CreateParametersForMethod(MethodInfo method, Dictionary<string, object> defaultValues)
        {
            var param = method.GetParameters();
            var result = new object[param.Length];
            for(int i = 0; i < param.Length; i++)
            {
                if (defaultValues.ContainsKey(param[i].Name))
                    result[i] = defaultValues[param[i].Name];
                else
                    result[i] = GetTestValue(param[i].ParameterType, i);
            }

            return result;
        }

        public static object GetTestValue(Type type, int i)
        {
            if (type == typeof(bool))
                return true;

            if (type == typeof(bool?))
                return (bool?) true;

            if (type == typeof(decimal))
                return i / 10m;

            if (type == typeof(decimal?))
                return (decimal?) (i / 10m);

            if (type == typeof(int) || type == typeof(long))
                return i;

            if (type == typeof(int?))
                return (int?) i;

            if (type == typeof(long?))
                return (long?) i;

            if (type == typeof(DateTime))
                return new DateTime(2019, 1, i);

            if(type == typeof(DateTime?))
                return (DateTime?) new DateTime(2019, 1, i);

            if (type == typeof(string))
                return "string" + i;

            if (type.IsEnum)
            {
                return Activator.CreateInstance(type);
            }

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var result = Array.CreateInstance(elementType, 2);
                result.SetValue(GetTestValue(elementType, 0), 0);
                result.SetValue(GetTestValue(elementType, 1), 1);
                return result;
            }

            if (type.IsClass)
                return FillWithTestParameters(Activator.CreateInstance(type));

            return null;
        }
    }
}
