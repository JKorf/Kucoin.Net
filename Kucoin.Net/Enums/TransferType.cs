using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Internal
        /// </summary>
        [Map("INTERNAL")]
        Internal,
        /// <summary>
        /// Parent to sub
        /// </summary>
        [Map("PARENT_TO_SUB")]
        ParentToSub,
        /// <summary>
        /// Sub to parent
        /// </summary>
        [Map("SUB_TO_PARENT")]
        SubToParent
    }
}
