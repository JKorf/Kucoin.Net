using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinQuery : Query<KucoinSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public KucoinQuery(string type, string topic, bool auth) : base(new KucoinRequest(ExchangeHelpers.NextId().ToString(), type, topic, auth), auth)
        {
            ListenerIdentifiers = new HashSet<string> { ((KucoinRequest)Request).Id };
        }

        public override CallResult<KucoinSocketResponse> HandleMessage(SocketConnection connection, DataEvent<KucoinSocketResponse> message)
        {
            var kucoinResponse = message.Data;
            if (string.Equals(kucoinResponse.Type, "error", StringComparison.Ordinal))
                return new CallResult<KucoinSocketResponse>(new ServerError(kucoinResponse.Code ?? 0, kucoinResponse.Data!));

            return base.HandleMessage(connection, message);
        }
    }
}
