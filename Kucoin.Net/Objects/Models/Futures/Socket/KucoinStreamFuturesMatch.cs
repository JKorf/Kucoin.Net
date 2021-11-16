namespace Kucoin.Net.Objects.Futures.Socket
{
    /// <summary>
    /// Match info
    /// </summary>
    public class KucoinStreamFuturesMatch: KucoinStreamMatchBase
    {
        /// <summary>
        /// Marer user id
        /// </summary>
        public string MakerUserId { get; set; } = string.Empty;
        /// <summary>
        /// Taker user id
        /// </summary>
        public string TakerUserId { get; set; } = string.Empty;
    }
}
