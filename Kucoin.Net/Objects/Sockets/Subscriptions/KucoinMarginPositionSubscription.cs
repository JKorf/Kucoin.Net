﻿using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinMarginPositionSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly Action<DataEvent<KucoinMarginDebtRatioUpdate>>? _onDebtRatioChange;
        private readonly Action<DataEvent<KucoinMarginPositionStatusUpdate>>? _onPositionStatusChange;
        private readonly string _topic = "/margin/position";
        

        public KucoinMarginPositionSubscription(
            ILogger logger,
            Action<DataEvent<KucoinMarginDebtRatioUpdate>>? onDebtRatioChange,
            Action<DataEvent<KucoinMarginPositionStatusUpdate>>? onPositionStatusChange
            ) : base(logger, true)
        {
            _onDebtRatioChange = onDebtRatioChange;
            _onPositionStatusChange = onPositionStatusChange;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<KucoinSocketUpdate<KucoinMarginDebtRatioUpdate>>(_topic + "debt.ratio", DoHandleMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinMarginPositionStatusUpdate>>(_topic + "position.status", DoHandleMessage)
                ]);
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery("subscribe", _topic, Authenticated);
        }

        public override Query? GetUnsubQuery()
        {
            return new KucoinQuery("unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinMarginDebtRatioUpdate>> message)
        {
            _onDebtRatioChange?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinMarginPositionStatusUpdate>> message)
        {
            _onPositionStatusChange?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
