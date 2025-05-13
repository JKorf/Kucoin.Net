using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Main account
        /// </summary>
        [Map("MAIN")]
        Main,
        /// <summary>
        /// Trade
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// Contract
        /// </summary>
        [Map("CONTRACT")]
        Contract,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// Isolated
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
        /// <summary>
        /// HF trade
        /// </summary>
        [Map("TRADE_HF")]
        TradeHf,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("MARGIN_V2")]
        MarginV2,
        /// <summary>
        /// Isolated
        /// </summary>
        [Map("ISOLATED_V2")]
        IsolatedV2,
        /// <summary>
        /// Option
        /// </summary>
        [Map("OPTION")]
        Option
    }
}
