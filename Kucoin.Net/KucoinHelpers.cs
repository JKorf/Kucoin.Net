using System;
using System.Text.RegularExpressions;

namespace Kucoin.Net
{
    public static class KucoinHelpers
    {
        /// <summary>
        /// Validate the string is a valid Kucoin symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static void ValidateKucoinSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");

            if (!Regex.IsMatch(symbolString, "^((([A-Z]|[0-9]){1,})[-](([A-Z]|[0-9]){1,}))$"))
                throw new ArgumentException($"{symbolString} is not a valid Kucoin symbol. Should be [QuoteCurrency]-[BaseCurrency] e.g. ETH-BTC");
        }
    }
}
