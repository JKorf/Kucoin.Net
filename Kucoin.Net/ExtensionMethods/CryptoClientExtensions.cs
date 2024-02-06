using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Kucoin REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IKucoinRestClient Kucoin(this ICryptoRestClient baseClient) => baseClient.TryGet<IKucoinRestClient>(() => new KucoinRestClient());

        /// <summary>
        /// Get the Kucoin Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IKucoinSocketClient Kucoin(this ICryptoSocketClient baseClient) => baseClient.TryGet<IKucoinSocketClient>(() => new KucoinSocketClient());
    }
}
