using CryptoExchange.Net.Attributes;


namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferType>))]
    public enum TransferType
    {
        /// <summary>
        /// ["<c>INTERNAL</c>"] Internal
        /// </summary>
        [Map("INTERNAL")]
        Internal,
        /// <summary>
        /// ["<c>PARENT_TO_SUB</c>"] Parent to sub
        /// </summary>
        [Map("PARENT_TO_SUB")]
        ParentToSub,
        /// <summary>
        /// ["<c>SUB_TO_PARENT</c>"] Sub to parent
        /// </summary>
        [Map("SUB_TO_PARENT")]
        SubToParent
    }
}
