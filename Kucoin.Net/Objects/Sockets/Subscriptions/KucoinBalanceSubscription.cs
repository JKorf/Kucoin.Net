using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinBalanceSubscription : Subscription
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

            MessageRouter = MessageRouter.Create([
                MessageRoute<KucoinSocketUpdate<KucoinStreamFuturesWalletUpdate>>.CreateWithoutTopicFilter(_topic + "walletBalance.change", DoHandleWalletChange),
                MessageRoute<KucoinSocketUpdate<KucoinStreamOrderMarginUpdate>>.CreateWithoutTopicFilter(_topic + "orderMargin.change", DoHandleMarginChange),
                MessageRoute<KucoinSocketUpdate<KucoinStreamFuturesBalanceUpdate>>.CreateWithoutTopicFilter(_topic + "availableBalance.change", DoHandleAvailableChange),
                MessageRoute<KucoinSocketUpdate<KucoinStreamFuturesWithdrawableUpdate>>.CreateWithoutTopicFilter(_topic + "withdrawHold.change", DoHandleWithdrawableChange),
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

        public CallResult DoHandleWalletChange(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamFuturesWalletUpdate> message)
        {
            _onWalletUpdate?.Invoke(
                new DataEvent<KucoinStreamFuturesWalletUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithUpdateType(SocketUpdateType.Update)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMarginChange(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamOrderMarginUpdate> message)
        {
            _onOrderMarginUpdate?.Invoke(
                new DataEvent<KucoinStreamOrderMarginUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithUpdateType(SocketUpdateType.Update)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleAvailableChange(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamFuturesBalanceUpdate> message)
        {
            _onBalanceUpdate?.Invoke(
                new DataEvent<KucoinStreamFuturesBalanceUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithUpdateType(SocketUpdateType.Update)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleWithdrawableChange(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamFuturesWithdrawableUpdate> message)
        {
            _onWithdrawableUpdate?.Invoke(
                new DataEvent<KucoinStreamFuturesWithdrawableUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithUpdateType(SocketUpdateType.Update)
                );
            return CallResult.SuccessResult;
        }
    }
}
