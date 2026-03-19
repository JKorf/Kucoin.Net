using CryptoExchange.Net.Authentication;
using System;

namespace Kucoin.Net
{
    /// <summary>
    /// Kucoin API credentials
    /// </summary>
    public class KucoinCredentials : HMACPassCredential
    {
        /// <summary>
        /// Create new credentials
        /// </summary>
        public KucoinCredentials() { }

        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public KucoinCredentials(string key, string secret, string pass) : base(key, secret, pass)
        {
        }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC credentials</param>
        public KucoinCredentials(HMACPassCredential credential) : base(credential.Key, credential.Secret, credential.Pass)
        {
        }

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public KucoinCredentials WithHMAC(string key, string secret, string pass)
        {
            if (!string.IsNullOrEmpty(Key)) throw new InvalidOperationException("Credentials already set");

            Key = key;
            Secret = secret;
            Pass = pass;
            return this;
        }
    }
}
