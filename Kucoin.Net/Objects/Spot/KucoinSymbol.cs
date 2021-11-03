using CryptoExchange.Net.ExchangeInterfaces;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public class KucoinSymbol: ICommonSymbol
    {
        /// <summary>
        /// The symbol identifier
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The name of the symbol
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The market the symbol is on
        /// </summary>
        public string Market { get; set; } = string.Empty;
        /// <summary>
        /// The base currency
        /// </summary>
        public string BaseCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The quote currency
        /// </summary>
        public string QuoteCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The min order size in the base currency
        /// </summary>
        public decimal BaseMinSize { get; set; }
        /// <summary>
        /// The min order size in the quote currency
        /// </summary>
        public decimal QuoteMinSize { get; set; }
        /// <summary>
        /// The max order size in the base currency
        /// </summary>
        public decimal BaseMaxSize { get; set; }
        /// <summary>
        /// The max order size in the quote currency
        /// </summary>
        public decimal QuoteMaxSize { get; set; }
        /// <summary>
        /// The quantity of an order when using the quantity field must be a multiple of this
        /// </summary>
        public decimal BaseIncrement { get; set; }
        /// <summary>
        /// The funds of an order when using the funds field must be a multiple of this
        /// </summary>
        public decimal QuoteIncrement { get; set; }
        /// <summary>
        /// The price of an order must be a multiple of this
        /// </summary>
        public decimal PriceIncrement { get; set; }
        /// <summary>
        /// The currency the fee will be on
        /// </summary>
        public string FeeCurrency { get; set; } = string.Empty;
        /// <summary>
        /// Whether trading is enabled
        /// </summary>
        public bool EnableTrading { get; set; }
        /// <summary>
        /// Is margin trading enabled
        /// </summary>
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// Threshold for price protection
        /// </summary>
        public decimal PriceLimitRate { get; set; }

        string ICommonSymbol.CommonName => Symbol;
        decimal ICommonSymbol.CommonMinimumTradeSize => BaseMinSize;
    }
}
