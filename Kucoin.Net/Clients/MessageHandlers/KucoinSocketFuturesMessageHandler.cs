using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets;
using System;
using System.Linq;
using System.Text.Json;

namespace Kucoin.Net.Clients.MessageHandlers
{
    internal class KucoinSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(KucoinExchange.SerializerContext);

        public KucoinSocketFuturesMessageHandler()
        {
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamFuturesMatch>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamFuturesKlineUpdate>>(x => x.Topic.Split(':')[1]);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamFuturesTick>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinFuturesOrderBookChange>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamOrderBookChanged>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamTransactionStatisticsUpdate>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamFuturesOrderUpdate>>(x => x.Data.Symbol);

            AddTopicMapping<KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamFuturesFundingRate>>(x => x.Symbol);

            AddTopicMapping<KucoinSocketUpdate<KucoinPositionUpdate>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinPositionMarkPriceUpdate>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate>>(x => x.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("type").WithEqualContstraint("welcome"),
                ],
                StaticIdentifier = "welcome"
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("id"),
                    new PropertyFieldReference("type").WithNotEqualContstraint("message"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic").WithStartsWithContstraint("/contract/position"),
                    new PropertyFieldReference("subject").WithStartsWithContstraint("position.change"),
                    new PropertyFieldReference("changeReason").WithStartsWithContstraint("markPriceChange")
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("topic")}position.changemarkPriceChange"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic")
                        .WithStartsWithContstraints(
                            "/contract/instrument",
                            "/margin/position",
                            "/contract/position",
                            "/contractAccount/wallet"),
                    new PropertyFieldReference("subject"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("topic")!.Split(':')[0] + x.FieldValue("subject")
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("topic")!.Split(':')[0]
            },
        ];
    }
}
