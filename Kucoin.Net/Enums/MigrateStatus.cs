using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Migration status
    /// </summary>
    public enum MigrateStatus
    {
        /// <summary>
        /// Not migrated
        /// </summary>
        [Map("0")]
        NotMigrated,
        /// <summary>
        /// Migrating
        /// </summary>
        [Map("1")]
        Migrating,
        /// <summary>
        /// Migrated
        /// </summary>
        [Map("2")]
        Migrated
    }
}
