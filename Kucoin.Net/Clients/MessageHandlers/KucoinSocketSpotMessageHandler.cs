using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets;
using System;
using System.Linq;
using System.Text.Json;

namespace Kucoin.Net.Clients.MessageHandlers
{
    internal class KucoinSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(KucoinExchange.SerializerContext);

        public KucoinSocketSpotMessageHandler()
        {
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamTick>>(x =>
            {
                var symbol = x.Topic.Split(':')[1];
                if (symbol.Equals("all"))
                    return x.Subject;

                return symbol;
            });
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamSnapshotWrapper>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamBestOffers>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamOrderBook>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamMatch>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamCandle>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamOrderBookChanged>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamIndicatorPrice>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinCallAuctionInfo>>(x => x.Data.Symbol);

            AddTopicMapping<KucoinSocketUpdate<KucoinMarginOrderUpdate>>(x => x.Data.Asset);
            AddTopicMapping<KucoinSocketUpdate<KucoinMarginOrderDoneUpdate>>(x => x.Data.Asset);
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
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

             new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("topic") { Constraint = x => x!.Equals("/spotMarket/tradeOrdersV2", StringComparison.Ordinal) },
                    new PropertyFieldReference("type") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic") + x.FieldValue("type")
            },


             new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("topic") 
                    { 
                        Constraint = x => x!.StartsWith("/margin/position", StringComparison.Ordinal) 
                                       || x!.StartsWith("/margin/loan", StringComparison.Ordinal)
                    },
                    new PropertyFieldReference("subject"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!.Split(':')[0] + x.FieldValue("subject")
            },

             new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!.Split(':')[0]
            },
        ];
    }
}
