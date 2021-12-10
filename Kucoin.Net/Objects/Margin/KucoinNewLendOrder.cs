using CryptoExchange.Net.ExchangeInterfaces;

namespace Kucoin.Net.Objects.Margin
{
    /// <summary>
    /// New order id
    /// </summary>
    public class KucoinNewLendOrder: ICommonOrderId
    {
        /// <summary>
        /// The id of the new order
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        string ICommonOrderId.CommonId => OrderId;
    }
}
