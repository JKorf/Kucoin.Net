using CryptoExchange.Net.ExchangeInterfaces;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// New order id
    /// </summary>
    public class KucoinNewOrder: ICommonOrderId
    {
        /// <summary>
        /// The id of the new order
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        string ICommonOrderId.CommonId => OrderId;
    }
}
