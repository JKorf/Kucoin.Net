using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Converters
{
    internal class TransactionStatusConverter : BaseConverter<TransactionStatus>
    {
        public TransactionStatusConverter() : this(true) { }
        public TransactionStatusConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<TransactionStatus, string>> Mapping => new List<KeyValuePair<TransactionStatus, string>>
        {
            new KeyValuePair<TransactionStatus, string>(TransactionStatus.Processing, "PROCESSING"),
            new KeyValuePair<TransactionStatus, string>(TransactionStatus.Success, "SUCCESS"),
            new KeyValuePair<TransactionStatus, string>(TransactionStatus.WalletProcessing, "WALLET_PROCESSING"),
            new KeyValuePair<TransactionStatus, string>(TransactionStatus.Failure, "FAILURE"),
        };
    }
}
