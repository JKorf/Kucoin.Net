using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinBalanceSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly SocketApiClient _client;
        private readonly Action<DataEvent<KucoinStreamFuturesWalletUpdate>>? _onWalletUpdate;
        private readonly Action<DataEvent<KucoinStreamOrderMarginUpdate>>? _onOrderMarginUpdate;
        private readonly Action<DataEvent<KucoinStreamFuturesBalanceUpdate>>? _onBalanceUpdate;
        private readonly Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>>? _onWithdrawableUpdate;
        private readonly string _topic = "/contractAccount/wallet";

        public KucoinBalanceSubscription(
            ILogger logger,
            SocketApiClient client,
            Action<DataEvent<KucoinStreamOrderMarginUpdate>>? onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>>? onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>>? onWithdrawableUpdate,
            Action<DataEvent<KucoinStreamFuturesWalletUpdate>>? onWalletUpdate
            ) : base(logger, true)
        {
            _client = client;
            _onOrderMarginUpdate = onOrderMarginUpdate;
            _onBalanceUpdate = onBalanceUpdate;
            _onWithdrawableUpdate = onWithdrawableUpdate;
            _onWalletUpdate = onWalletUpdate;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamFuturesWalletUpdate>>(_topic + "walletBalance.change", DoHandleWalletChange),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamOrderMarginUpdate>>(_topic + "orderMargin.change", DoHandleMarginChange),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamFuturesBalanceUpdate>>(_topic + "availableBalance.change", DoHandleAvailableChange),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamFuturesWithdrawableUpdate>>(_topic + "withdrawHold.change", DoHandleWithdrawableChange),
                ]);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "subscribe", _topic, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleWalletChange(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamFuturesWalletUpdate>> message)
        {
            _onWalletUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMarginChange(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamOrderMarginUpdate>> message)
        {
            _onOrderMarginUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleAvailableChange(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamFuturesBalanceUpdate>> message)
        {
            _onBalanceUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleWithdrawableChange(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamFuturesWithdrawableUpdate>> message)
        {
            _onWithdrawableUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }
    }
}
