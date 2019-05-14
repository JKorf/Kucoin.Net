using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Kucoin.Net
{
    public class KucoinAuthenticationProvider : AuthenticationProvider
    {
        HMACSHA256 encryptor;

        public KucoinAuthenticationProvider(KucoinApiCredentials credentials): base(credentials)
        {
            encryptor = new HMACSHA256(Encoding.UTF8.GetBytes(credentials.Secret.GetString()));
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            if (!signed)
                return new Dictionary<string, string>();

            var result = new Dictionary<string, string>();
            result["KC-API-KEY"] = Credentials.Key.GetString();
            result["KC-API-TIMESTAMP"] = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture);
            result["KC-API-PASSPHRASE"] = ((KucoinApiCredentials)Credentials).PassPhrase.GetString();

            string jsonContent = "";
            if (method != Constants.GetMethod && method != Constants.DeleteMethod)
                jsonContent = JsonConvert.SerializeObject(parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));

            uri = uri.Substring(uri.IndexOf(".com") + 4);
            var signData = result["KC-API-TIMESTAMP"] + method + uri + jsonContent;
            result["KC-API-SIGN"] = Convert.ToBase64String(encryptor.ComputeHash(Encoding.UTF8.GetBytes(signData)));
            return result;
        }
    }
}
