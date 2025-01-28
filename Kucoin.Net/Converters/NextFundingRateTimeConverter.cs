using System;
using System.Text.Json;

namespace Kucoin.Net.Converters
{
    /// <summary>
    /// Does basically the same thing as the shared DateTimeConverter, with a slight difference.
    /// Instead of adding the time to Jan 01 1970, it adds it to the current time. This isn't ideal since
    /// it produces inaccuracies, but it's the format the kucoin api returns for the next funding rate time.
    /// </summary>
    internal class NextFundingRateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var value = reader.GetInt64();
            var now = DateTime.UtcNow;
            var offset = TimeSpan.FromMilliseconds(value);
            return now + offset;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            throw new NotSupportedException("This converter only supports reading");
        }
    }
}
