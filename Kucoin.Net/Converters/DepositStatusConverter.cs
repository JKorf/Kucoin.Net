using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class DepositStatusConverter : BaseConverter<KucoinDepositStatus>
    {
        public DepositStatusConverter() : this(true) { }
        public DepositStatusConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinDepositStatus, string>> Mapping => new List<KeyValuePair<KucoinDepositStatus, string>>
        {
            new KeyValuePair<KucoinDepositStatus, string>(KucoinDepositStatus.Processing, "PROCESSING"),
            new KeyValuePair<KucoinDepositStatus, string>(KucoinDepositStatus.Success, "SUCCESS"),
            new KeyValuePair<KucoinDepositStatus, string>(KucoinDepositStatus.Failure, "FAILURE"),
        };
    }
}
