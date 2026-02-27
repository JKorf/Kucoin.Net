using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Models.Unified;
using Kucoin.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Kucoin.Net.Clients.MessageHandlers
{
    internal class KucoinSocketUnifiedMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(KucoinExchange.SerializerContext);

        public KucoinSocketUnifiedMessageHandler()
        {
            AddTopicMapping<KucoinUnifiedSocketUpdate<KucoinUaTickerUpdate>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinUnifiedSocketUpdate<KucoinUaKlineUpdate>>(x => x.Data.Symbol + x.Data.Interval);
            AddTopicMapping<KucoinUnifiedSocketUpdate<KucoinUaOrderBookUpdate>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinUnifiedSocketUpdate<KucoinUaTradeUpdate>>(x => x.Data.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [
             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("message").WithEqualConstraint("welcome"),
                ],
                StaticIdentifier = "welcome"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("T"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("T")!
            },
        ];
    }
}
