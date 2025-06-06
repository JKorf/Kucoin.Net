﻿using CryptoExchange.Net;
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

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            if (!auth)
                return;

            var brokerName = ((KucoinRestApiOptions)apiClient.ApiOptions).BrokerName;
            var brokerKey = ((KucoinRestApiOptions)apiClient.ApiOptions).BrokerKey;

            if (string.IsNullOrEmpty(brokerName) && string.IsNullOrEmpty(brokerKey))
            {
                brokerName = apiClient is KucoinRestClientFuturesApi ? "Easytradingfutures" : "Easytrading";
                brokerKey = apiClient is KucoinRestClientFuturesApi ? "9e08c05f-454d-4580-82af-2f4c7027fd00" : "f8ae62cb-2b3d-420c-8c98-e1c17dd4e30a";
            }

            if (uriParameters != null)
                uri = uri.SetParameters(uriParameters, arraySerialization);

            headers ??= new Dictionary<string, string>();
            headers.Add("KC-API-KEY", _credentials.Key);
            headers.Add("KC-API-TIMESTAMP", GetMillisecondTimestamp(apiClient).ToString());
            var phraseKey = _credentials.Key + "|" + _credentials.Pass;
            if (!_phraseCache.TryGetValue(phraseKey, out var phraseSign))
            {
                phraseSign = SignHMACSHA256(_credentials.Pass!, SignOutputType.Base64);
                _phraseCache.TryAdd(phraseKey, phraseSign);
            }

            headers.Add("KC-API-PASSPHRASE", phraseSign);
            headers.Add("KC-API-KEY-VERSION", "3");

            string jsonContent = string.Empty;
            if (parameterPosition == HttpMethodParameterPosition.InBody)
            {
                if (bodyParameters?.Any() == true)
                {
                    jsonContent = GetSerializedBody(_serializer, bodyParameters);
                }
                else
                {
                    jsonContent = "{}";
                }
            }

            var signData = headers["KC-API-TIMESTAMP"] + method + Uri.UnescapeDataString(uri.PathAndQuery) + jsonContent;
            headers.Add("KC-API-SIGN", SignHMACSHA256(signData, SignOutputType.Base64));

            // Partner info
            headers.Add("KC-API-PARTNER", brokerName!);
            var partnerSignData = headers["KC-API-TIMESTAMP"] + brokerName + _credentials.Key;
            using HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(brokerKey!));
            byte[] buff = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(partnerSignData));
            headers.Add("KC-API-PARTNER-SIGN", BytesToBase64String(buff));
        }
    }
}
