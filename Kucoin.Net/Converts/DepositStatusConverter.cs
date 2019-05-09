using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    public class DepositStatusConverter : BaseConverter<KucoinDepositStatus>
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
