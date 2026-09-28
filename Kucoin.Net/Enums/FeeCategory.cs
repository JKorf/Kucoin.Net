using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Fee category
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FeeCategory>))]
    public enum FeeCategory
    {
        /// <summary>
        /// ["<c>classA</c>"] Class A
        /// </summary>
        [Map("classA")]
        ClassA,
        /// <summary>
        /// ["<c>classB</c>"] Class B
        /// </summary>
        [Map("classB")]
        ClassB,
        /// <summary>
        /// ["<c>classC</c>"] Class C
        /// </summary>
        [Map("classC")]
        ClassC
    }
}
