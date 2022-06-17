using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using System.Collections.Generic;

namespace Kucoin.Net.Converters
{
    internal class LoanStatusConverter : BaseConverter<LoanStatus>
    {
        public LoanStatusConverter() : this(true) { }
        public LoanStatusConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<LoanStatus, string>> Mapping => new List<KeyValuePair<LoanStatus, string>>
        {
            new KeyValuePair<LoanStatus, string>(LoanStatus.Processing, "Processing"),
            new KeyValuePair<LoanStatus, string>(LoanStatus.Done, "Finish"),
            new KeyValuePair<LoanStatus, string>(LoanStatus.Done, "Cancel"),
            new KeyValuePair<LoanStatus, string>(LoanStatus.Done, "Done"),
        };
    }
}
