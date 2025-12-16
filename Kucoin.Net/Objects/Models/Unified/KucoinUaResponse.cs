using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Unified
{
    internal record KucoinUaResponse<T>
    {
        [JsonPropertyName("tradeType")]
        public ProductType ProductType { get; set; }
        [JsonPropertyName("list")]
        public T Data { get; set; } = default!;
    }
}
