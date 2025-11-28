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
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamTick>>(x => x.Topic.Equals("/market/ticker:all") ? ("/market/ticker:" + x.Subject) : x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamSnapshotWrapper>>(x => x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamBestOffers>>(x => x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamOrderBook>>(x => x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamMatch>>(x => x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamCandle>>(x => x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamOrderBookChanged>>(x => x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamIndicatorPrice>>(x => x.Topic);
            AddTopicMapping<KucoinSocketUpdate<KucoinCallAuctionInfo>>(x => x.Topic);

#warning WIP implement further, not tested
        }

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

             new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

             new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("type") { Constraint = x => x!.Equals("welcome") },
                ],
                StaticIdentifier = "welcome"
            },

             new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!.Split(':')[0]
            },
        ];
    }
}
