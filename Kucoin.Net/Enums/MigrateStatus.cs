using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Migration status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MigrateStatus>))]
    public enum MigrateStatus
    {
        /// <summary>
        /// ["<c>0</c>"] Not migrated
        /// </summary>
        [Map("0")]
        NotMigrated,
        /// <summary>
        /// ["<c>1</c>"] Migrating
        /// </summary>
        [Map("1")]
        Migrating,
        /// <summary>
        /// ["<c>2</c>"] Migrated
        /// </summary>
        [Map("2")]
        Migrated
    }
}
