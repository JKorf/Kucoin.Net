using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Product type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesProductType>))]
    public enum FuturesProductType
    {
        /// <summary>
        /// ["<c>USDT-FUTURES</c>"] USDT futures
        /// </summary>
        [Map("USDT-FUTURES")]
        UsdtFutures,
        /// <summary>
        /// ["<c>USDC-FUTURES</c>"] USDC futures
        /// </summary>
        [Map("USDC-FUTURES")]
        UsdcFutures,
        /// <summary>
        /// ["<c>COIN-FUTURES</c>"] Coin futures
        /// </summary>
        [Map("COIN-FUTURES")]
        CoinFutures,
    }
}
