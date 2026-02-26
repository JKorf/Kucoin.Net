using CryptoExchange.Net.Attributes;


namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedTransferType>))]
    public enum UnifiedTransferType
    {
        /// <summary>
        /// Internal
        /// </summary>
        [Map("0")]
        Internal,
        /// <summary>
        /// Parent to sub
        /// </summary>
        [Map("1")]
        ParentToSub,
        /// <summary>
        /// Sub to parent
        /// </summary>
        [Map("2")]
        SubToParent,
        /// <summary>
        /// Sub to sub
        /// </summary>
        [Map("3")]
        SubToSub
    }
}
