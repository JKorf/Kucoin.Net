namespace Kucoin.Net.Objects.Spot.Socket
{
    /// <summary>
    /// Stream tick
    /// </summary>
    public class KucoinStreamTick: KucoinTick
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
    }
}
