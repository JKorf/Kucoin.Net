using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
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
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamCandle>>(x => x.Topic.Split(':')[1]);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamOrderBookChanged>>(x => x.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinStreamIndicatorPrice>>(x => x.Data.Symbol);
            AddTopicMapping<KucoinSocketUpdate<KucoinCallAuctionInfo>>(x => x.Data.Symbol);

            AddTopicMapping<KucoinSocketUpdate<KucoinMarginOrderUpdate>>(x => x.Data.Asset);
            AddTopicMapping<KucoinSocketUpdate<KucoinMarginOrderDoneUpdate>>(x => x.Data.Asset);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [
             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("type").WithEqualConstraint("welcome"),
                ],
                StaticIdentifier = "welcome"
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("id"),
                    new PropertyFieldReference("topic").WithCustomConstraint(x => {
                        if (x == null)
                            return false;

                        // These have an id field but should identify by topic regardless
                        return string.Equals(x, "/account/balance", System.StringComparison.Ordinal)
                             || x.StartsWith("/margin/fundingBook", StringComparison.Ordinal);
                    }),
                ],
                TypeIdentifierCallback = x => x.FieldValue("topic")!
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic").WithEqualConstraint("/spotMarket/tradeOrdersV2"),
                    new PropertyFieldReference("type") { Depth = 2 },
                ],
                TypeIdentifierCallback = x => x.FieldValue("topic") + x.FieldValue("type")
            },


             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic").WithStartsWithConstraints("/margin/position", "/margin/loan"),
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
