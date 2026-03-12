using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// ["<c>MAIN</c>"] Main account
        /// </summary>
        [Map("MAIN")]
        Main,
        /// <summary>
        /// ["<c>TRADE</c>"] Trade account
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// ["<c>MARGIN</c>"] Margin account
        /// </summary>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// ["<c>POOl</c>"] Pool account
        /// </summary>
        [Map("POOl")]
        Pool,
        /// <summary>
        /// ["<c>CONTRACT</c>"] Contract
        /// </summary>
        [Map("CONTRACT")]
        Contract,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated margin account
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
        /// <summary>
        /// ["<c>TRADE_HF</c>"] High Frequency (PRO Account) spot account
        /// </summary>
        [Map("TRADE_HF")]
        SpotHf,
        /// <summary>
        /// ["<c>MARGIN_V2</c>"] High Frequency (PRO Account) margin account
        /// </summary>
        [Map("MARGIN_V2")]
        MarginHf,
        /// <summary>
        /// ["<c>ISOLATED_V2</c>"] High Frequency (PRO Account) isolated account
        /// </summary>
        [Map("ISOLATED_V2")]
        IsolatedMarginHf,
        /// <summary>
        /// ["<c>OPTION</c>"] Option Account
        /// </summary>
        [Map("OPTION")]
        Option,

        /// <summary>
        /// ["<c>UNIFIED</c>"] Unified account
        /// </summary>
        [Map("UNIFIED")]
        Unified
    }
}
