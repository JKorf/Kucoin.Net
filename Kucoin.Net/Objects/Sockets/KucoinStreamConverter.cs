//using CryptoExchange.Net.Converters;
//using CryptoExchange.Net.Interfaces;
//using CryptoExchange.Net.Objects.Sockets;
//using Kucoin.Net.Objects.Models.Spot.Socket;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Kucoin.Net.Objects.Sockets
//{
//    internal class KucoinStreamConverter : SocketConverter
//    {
//        private static readonly Dictionary<string, Type> _subjectMapping = new Dictionary<string, Type>()
//        {
//            { "trade.ticker", typeof(KucoinSocketUpdate<KucoinStreamTick>) },
//            { "trade.snapshot", typeof(KucoinSocketUpdate<KucoinStreamSnapshotWrapper>) }
//        };

//        private static readonly Dictionary<string, Type> _topicMapping = new Dictionary<string, Type>()
//        {
//            { "/market/ticker:all", typeof(KucoinSocketUpdate<KucoinStreamTick>) },
//        };


//        public override MessageInterpreterPipeline InterpreterPipeline { get; } = new MessageInterpreterPipeline
//        {
//            //PostInspectCallbacks = new List<object>
//            //{
//            //    new PostInspectCallback
//            //    {
//            //        TypeFields = new List<TypeField>
//            //        {
//            //            new TypeField("id"),
//            //            new TypeField("type"),
//            //        },
//            //        Callback = GetDeserializationTypeQueryResponse
//            //    },
//            //    new PostInspectCallback
//            //    {
//            //        TypeFields = new List<TypeField>
//            //        {
//            //            new TypeField("topic"),
//            //            new TypeField("subject"),
//            //        },
//            //        Callback = GetDeserializationTypeUpdate
//            //    }
//            //},
//            GetIdentity = GetIdentity,
//        };
//        private static string GetIdentity(IMessageAccessor accessor)
//        {
//            var id = accessor.GetStringValue("id");
//            var type = accessor.GetStringValue("type");
//            if (type == "welcome")
//                return type;

//            if (id != null)
//                return id;

//            return accessor.GetStringValue("topic").ToLowerInvariant();
//        }

//        private static PostInspectResult GetDeserializationTypeQueryResponse(IMessageAccessor accessor, Dictionary<string, Type> processors)
//        {
//            if (accessor.GetStringValue("type") == "welcome")
//                return new PostInspectResult { Type = typeof(KucoinSocketResponse), Identifier = accessor.GetStringValue("type") };

//            if (!processors.TryGetValue(accessor.GetStringValue("id"), out var type))
//            {
//                // Probably shouldn't be exception
//                throw new Exception("Unknown update type");
//            }

//            return new PostInspectResult { Type = type, Identifier = accessor.GetStringValue("id") };
//        }

//        private static PostInspectResult GetDeserializationTypeUpdate(IMessageAccessor accessor, Dictionary<string, Type> processors)
//        {
//            var streamId = accessor.GetStringValue("subject")!;
//            if (_subjectMapping.TryGetValue(streamId, out var streamIdMapping))
//                return new PostInspectResult { Type = streamIdMapping, Identifier = accessor.GetStringValue("topic").ToLowerInvariant() };

//            var topicId = accessor.GetStringValue("topic")!;
//            if (_topicMapping.TryGetValue(topicId, out var typeIdMapping))
//                return new PostInspectResult { Type = typeIdMapping, Identifier = accessor.GetStringValue("topic").ToLowerInvariant() };

//            return new PostInspectResult();
//        }
//    }
//}
