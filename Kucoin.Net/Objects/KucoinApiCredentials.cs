using CryptoExchange.Net.Authentication;
using System.Security;

namespace Kucoin.Net.Objects
{
    public class KucoinApiCredentials : ApiCredentials
    {
        /// <summary>
        /// The passphrase
        /// </summary>
        public SecureString Passphrase { get; }

        /// <summary>
        /// Creates new api credentials. Keep this information safe.
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="apiSecret">The API secret</param>
        /// <param name="apiPassphrase">The API passphrase</param>
        public KucoinApiCredentials(string apiKey, string apiSecret, string apiPassphrase): base(apiKey, apiSecret)
        {
            var securePassphrase = new SecureString();
            foreach (var c in apiPassphrase)
                securePassphrase.AppendChar(c);
            securePassphrase.MakeReadOnly();
            Passphrase = securePassphrase;
        }
    }
}
