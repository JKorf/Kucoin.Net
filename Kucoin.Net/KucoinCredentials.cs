using CryptoExchange.Net.Authentication;

namespace Kucoin.Net
{
    /// <summary>
    /// Kucoin API credentials
    /// </summary>
    public class KucoinCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public KucoinCredentials(string key, string secret, string pass) : base(key, secret, pass)
        {
        }
    }
}
