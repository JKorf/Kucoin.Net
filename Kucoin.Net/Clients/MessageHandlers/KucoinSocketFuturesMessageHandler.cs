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
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamFuturesKlineUpdate>>(x => x.Data.Symbol);
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

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

             new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("type") { Constraint = x => x!.Equals("welcome") },
                ],
                StaticIdentifier = "welcome"
            },

             new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("id"),
                    new PropertyFieldReference("type") { Constraint = x => !x!.Equals("message") },
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

             new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("topic")
                    {
                        Constraint = x => x!.StartsWith("/contract/position", StringComparison.Ordinal)
                    },
                    new PropertyFieldReference("subject")
                    {
                        Constraint = x => x!.StartsWith("position.change", StringComparison.Ordinal)
                    },
                    new PropertyFieldReference("changeReason")
                    {
                        Depth = 2,
                        Constraint = x => x!.StartsWith("markPriceChange", StringComparison.Ordinal)
                    },
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("topic")}position.changemarkPriceChange"
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("topic")
                    {
                        Constraint = x => x!.StartsWith("/contract/instrument", StringComparison.Ordinal)
                                       || x!.StartsWith("/margin/position", StringComparison.Ordinal)
                                       || x!.StartsWith("/contract/position", StringComparison.Ordinal)
                                       || x!.StartsWith("/contractAccount/wallet", StringComparison.Ordinal)
                    },
                    new PropertyFieldReference("subject"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!.Split(':')[0] + x.FieldValue("subject")
            },

             new MessageEvaluator {
                Priority = 6,
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!.Split(':')[0]
            },
        ];
    }
}
