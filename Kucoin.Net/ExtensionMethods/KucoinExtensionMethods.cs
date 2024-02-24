using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using Kucoin.Net.SymbolOrderBooks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace Kucoin.Net.ExtensionMethods
{
    /// <summary>
    /// Extension methods specific to using the Kucoin API
    /// </summary>
    public static class KucoinExtensionMethods
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
                throw new ArgumentException($"{symbolString} is not a valid Kucoin symbol. Should be [BaseAsset]-[QuoteAsset] e.g. ETH-BTC");
        }
    }
}
