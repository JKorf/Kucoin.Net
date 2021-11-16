namespace Kucoin.Net.Objects.Models.Spot.Socket
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
