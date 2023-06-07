using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Main account
        /// </summary>
        [Map("MAIN")]
        Main,
        /// <summary>
        /// Trade account
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// Margin account
        /// </summary>
        Margin,
        /// <summary>
        /// Pool account
        /// </summary>
        Pool,
        /// <summary>
        /// Isolated marging account
        /// </summary>
        Isolated,
        /// <summary>
        /// High Frequency (PRO Account) account
        /// </summary>
        HighFrequency
    }
}
