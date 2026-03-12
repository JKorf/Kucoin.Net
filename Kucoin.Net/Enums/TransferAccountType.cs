using CryptoExchange.Net.Attributes;


namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Transfer account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferAccountType>))]
    public enum TransferAccountType
    {
        /// <summary>
        /// ["<c>MAIN</c>"] Main account
        /// </summary>
        [Map("MAIN")]
        Main,
        /// <summary>
        /// ["<c>TRADE</c>"] Trade
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// ["<c>CONTRACT</c>"] Contract
        /// </summary>
        [Map("CONTRACT")]
        Contract,
        /// <summary>
        /// ["<c>MARGIN</c>"] Margin
        /// </summary>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
        /// <summary>
        /// ["<c>TRADE_HF</c>"] HF trade
        /// </summary>
        [Map("TRADE_HF")]
        TradeHf,
        /// <summary>
        /// ["<c>MARGIN_V2</c>"] Margin
        /// </summary>
        [Map("MARGIN_V2")]
        MarginV2,
        /// <summary>
        /// ["<c>ISOLATED_V2</c>"] Isolated
        /// </summary>
        [Map("ISOLATED_V2")]
        IsolatedV2,
        /// <summary>
        /// ["<c>OPTION</c>"] Option
        /// </summary>
        [Map("OPTION")]
        Option
    }
}
