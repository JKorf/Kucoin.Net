using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Internal;
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

        public override Task<CallResult<KucoinSocketResponse>> HandleMessageAsync(SocketConnection connection, DataEvent<KucoinSocketResponse> message)
        {
            var kucoinResponse = message.Data;
            if (kucoinResponse.Type == "error")
                return Task.FromResult(new CallResult<KucoinSocketResponse>(new ServerError(kucoinResponse.Code ?? 0, kucoinResponse.Data!)));

            return base.HandleMessageAsync(connection, message);
        }
    }
}
