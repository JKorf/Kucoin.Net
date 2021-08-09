using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class OperationTypeConverter : BaseConverter<KucoinOrderOperationType>
    {
        public OperationTypeConverter() : this(true) { }
        public OperationTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinOrderOperationType, string>> Mapping => new List<KeyValuePair<KucoinOrderOperationType, string>>
        {
            new KeyValuePair<KucoinOrderOperationType, string>(KucoinOrderOperationType.Deal, "DEAL"),
            new KeyValuePair<KucoinOrderOperationType, string>(KucoinOrderOperationType.Cancel, "CANCEL")
        };
    }
}
