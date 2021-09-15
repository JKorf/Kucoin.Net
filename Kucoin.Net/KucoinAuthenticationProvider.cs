using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Kucoin.Net
{
    internal class KucoinAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;

        public KucoinAuthenticationProvider(KucoinApiCredentials credentials): base(credentials)
        {
            if (credentials.Secret == null)
                throw new ArgumentException("ApiKey/Secret needed");

            encryptor = new HMACSHA256(Encoding.UTF8.GetBytes(credentials.Secret.GetString()));
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, HttpMethodParameterPosition parameterPosition, ArrayParametersSerialization arraySerialization)
        {
            if (!signed)
                return new Dictionary<string, string>();

            if (Credentials.Key == null)
                throw new ArgumentException("ApiKey/Secret needed");

            var result = new Dictionary<string, string>
            {
                ["KC-API-KEY"] = Credentials.Key.GetString(),
                ["KC-API-TIMESTAMP"] = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                ["KC-API-PASSPHRASE"] = ((KucoinApiCredentials) Credentials).PassPhrase.GetString()
            };

            var jsonContent = string.Empty;
            if (parameterPosition == HttpMethodParameterPosition.InBody)
                jsonContent = JsonConvert.SerializeObject(parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));

            uri = uri.Substring(uri.IndexOf(".com", StringComparison.InvariantCulture) + 4);
            var signData = result["KC-API-TIMESTAMP"] + method + uri + jsonContent;
            result["KC-API-SIGN"] = Convert.ToBase64String(encryptor.ComputeHash(Encoding.UTF8.GetBytes(signData)));
            return result;
        }
    }
}
