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
        /// ["<c>0</c>"] Internal
        /// </summary>
        [Map("0")]
        Internal,
        /// <summary>
        /// ["<c>1</c>"] Parent to sub
        /// </summary>
        [Map("1")]
        ParentToSub,
        /// <summary>
        /// ["<c>2</c>"] Sub to parent
        /// </summary>
        [Map("2")]
        SubToParent,
        /// <summary>
        /// ["<c>3</c>"] Sub to sub
        /// </summary>
        [Map("3")]
        SubToSub
    }
}
