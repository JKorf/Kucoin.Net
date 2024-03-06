using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Kucoin.Net
{
    internal class KucoinAuthenticationProvider : AuthenticationProvider<KucoinApiCredentials>
    {
        public KucoinAuthenticationProvider(KucoinApiCredentials credentials): base(credentials)
        {
            if (credentials.CredentialType != ApiCredentialsType.Hmac)
                throw new Exception("Only Hmac authentication is supported");
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>();

            if (!auth)
                return;

            var brokerName = ((KucoinRestApiOptions)apiClient.ApiOptions).BrokerName;
            var brokerKey = ((KucoinRestApiOptions)apiClient.ApiOptions).BrokerKey;

            if (string.IsNullOrEmpty(brokerName) && string.IsNullOrEmpty(brokerKey))
            {
                brokerName = apiClient is KucoinRestClientFuturesApi ? "Easytradingfutures" : "Easytrading";
                brokerKey = apiClient is KucoinRestClientFuturesApi ? "9e08c05f-454d-4580-82af-2f4c7027fd00" : "f8ae62cb-2b3d-420c-8c98-e1c17dd4e30a";
            }

            uri = uri.SetParameters(uriParameters, arraySerialization);
            headers.Add("KC-API-KEY", _credentials.Key!.GetString());
            headers.Add("KC-API-TIMESTAMP", GetMillisecondTimestamp(apiClient).ToString());
            headers.Add("KC-API-PASSPHRASE", SignHMACSHA256(_credentials.PassPhrase.GetString(), SignOutputType.Base64));
            headers.Add("KC-API-KEY-VERSION", "2");

            var jsonContent = parameterPosition == HttpMethodParameterPosition.InBody ? JsonConvert.SerializeObject(bodyParameters) : string.Empty;
            var signData = headers["KC-API-TIMESTAMP"] + method + Uri.UnescapeDataString(uri.PathAndQuery) + jsonContent;
            headers.Add("KC-API-SIGN", SignHMACSHA256(signData, SignOutputType.Base64));

            // Partner info
            headers.Add("KC-API-PARTNER", brokerName!);
            var partnerSignData = headers["KC-API-TIMESTAMP"] + brokerName + _credentials.Key!.GetString();
            using HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(brokerKey));
            byte[] buff = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(partnerSignData));
            headers.Add("KC-API-PARTNER-SIGN", BytesToBase64String(buff));
        }
    }
}
