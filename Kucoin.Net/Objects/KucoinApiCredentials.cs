using CryptoExchange.Net.Authentication;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Credentials for the Kucoin API
    /// </summary>
    public class KucoinApiCredentials : ApiCredentials
    {
        /// <summary>
        /// The pass phrase
        /// </summary>
        public string PassPhrase { get; set; }

        /// <summary>
        /// Creates new api credentials. Keep this information safe.
        /// </summary>
        /// <param name="key">The API key</param>
        /// <param name="secret">The API secret</param>
        /// <param name="passPhrase">The API passPhrase</param>
        public KucoinApiCredentials(string key, string secret, string passPhrase): base(key, secret)
        {
            PassPhrase = passPhrase;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy()
        {
            return new KucoinApiCredentials(Key, Secret, PassPhrase);
        }
    }
}
