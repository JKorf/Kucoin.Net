using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin options
    /// </summary>
    public class KucoinOptions : LibraryOptions<KucoinRestOptions, KucoinSocketOptions, KucoinApiCredentials, KucoinEnvironment>
    {
    }
}
