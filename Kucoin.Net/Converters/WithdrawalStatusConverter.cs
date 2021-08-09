using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class WithdrawalStatusConverter : BaseConverter<KucoinWithdrawalStatus>
    {
        public WithdrawalStatusConverter() : this(true) { }
        public WithdrawalStatusConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinWithdrawalStatus, string>> Mapping => new List<KeyValuePair<KucoinWithdrawalStatus, string>>
        {
            new KeyValuePair<KucoinWithdrawalStatus, string>(KucoinWithdrawalStatus.Processing, "PROCESSING"),
            new KeyValuePair<KucoinWithdrawalStatus, string>(KucoinWithdrawalStatus.Success, "SUCCESS"),
            new KeyValuePair<KucoinWithdrawalStatus, string>(KucoinWithdrawalStatus.WalletProcessing, "WALLET_PROCESSING"),
            new KeyValuePair<KucoinWithdrawalStatus, string>(KucoinWithdrawalStatus.Failure, "FAILURE"),
        };
    }
}
