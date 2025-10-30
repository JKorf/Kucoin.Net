using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Options;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Kucoin.Net
{
    internal class KucoinAuthenticationProvider : AuthenticationProvider
    {
        private readonly static ConcurrentDictionary<string, string> _phraseCache = new();
        private readonly static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(KucoinExchange.SerializerContext));

        public KucoinAuthenticationProvider(ApiCredentials credentials): base(credentials)
        {
            if (credentials.CredentialType != ApiCredentialsType.Hmac)
                throw new Exception("Only Hmac authentication is supported");

            if (string.IsNullOrEmpty(credentials.Pass))
                throw new ArgumentNullException(nameof(ApiCredentials.Pass), "Passphrase is required for Kucoin authentication");
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var brokerName = LibraryHelpers.GetClientReference(() => ((KucoinRestApiOptions)apiClient.ApiOptions).BrokerName, "Kucoin", apiClient is KucoinRestClientFuturesApi ? "FuturesName" : "SpotName");
            var brokerKey = LibraryHelpers.GetClientReference(() => ((KucoinRestApiOptions)apiClient.ApiOptions).BrokerKey, "Kucoin", apiClient is KucoinRestClientFuturesApi ? "FuturesKey" : "SpotKey");
            
            var timestamp = GetMillisecondTimestamp(apiClient).ToString();
            request.Headers.Add("KC-API-KEY", _credentials.Key);
            request.Headers.Add("KC-API-TIMESTAMP", timestamp);
            var phraseKey = _credentials.Key + "|" + _credentials.Pass;
            if (!_phraseCache.TryGetValue(phraseKey, out var phraseSign))
            {
                phraseSign = SignHMACSHA256(_credentials.Pass!, SignOutputType.Base64);
                _phraseCache.TryAdd(phraseKey, phraseSign);
            }

            request.Headers.Add("KC-API-PASSPHRASE", phraseSign);
            request.Headers.Add("KC-API-KEY-VERSION", "3");

            var bodyData = request.ParameterPosition == HttpMethodParameterPosition.InBody ? GetSerializedBody(_serializer, request.BodyParameters) : string.Empty;
            var queryString = request.GetQueryString(false);
            if (!string.IsNullOrEmpty(queryString))
                queryString = $"?{queryString}";

            var signData = $"{timestamp}{request.Method}{request.Path}{queryString}{bodyData}";
            request.Headers.Add("KC-API-SIGN", SignHMACSHA256(signData, SignOutputType.Base64));

            // Partner info
            request.Headers.Add("KC-API-PARTNER", brokerName!);
            var partnerSignData = $"{timestamp}{brokerName}{_credentials.Key}";

            using HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(brokerKey!));
            byte[] buff = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(partnerSignData));
            request.Headers.Add("KC-API-PARTNER-SIGN", BytesToBase64String(buff));

            request.SetBodyContent(bodyData);
            request.SetQueryString(queryString);
        }
    }
}
