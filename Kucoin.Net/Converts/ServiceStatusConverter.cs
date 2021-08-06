using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Converts
{
    internal class ServiceStatusConverter : BaseConverter<ServiceStatus>
    {
        public ServiceStatusConverter() : this(true) { }

        public ServiceStatusConverter(bool quotes) : base(quotes)
        {
        }
        protected override List<KeyValuePair<ServiceStatus, string>> Mapping => new List<KeyValuePair<ServiceStatus, string>>
        {
            new KeyValuePair<ServiceStatus, string>(ServiceStatus.Open, "open"),
            new KeyValuePair<ServiceStatus, string>(ServiceStatus.Close, "close"),
            new KeyValuePair<ServiceStatus, string>(ServiceStatus.CancelOnly, "cancelOnly")
        };
    }
}
