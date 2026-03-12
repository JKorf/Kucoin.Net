using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Margin position update event type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginEventType>))]
    public enum MarginEventType
    {
        /// <summary>
        /// ["<c>FROZEN_FL</c>"] When the debt ratio exceeds the liquidation threshold and the position is frozen, the system will push this event.
        /// </summary>
        [Map("FROZEN_FL")]
        Frozen,
        /// <summary>
        /// ["<c>UNFROZEN_FL</c>"] When the liquidation is finished and the position returns to “EFFECTIVE” status, the system will push this event.
        /// </summary>
        [Map("UNFROZEN_FL")]
        Unfrozen,
        /// <summary>
        /// ["<c>FROZEN_RENEW</c>"] When the auto-borrow renewing is complete and the position returns to “EFFECTIVE” status, the system will push this event.
        /// </summary>
        [Map("FROZEN_RENEW")]
        FrozenRenew,
        /// <summary>
        /// ["<c>UNFROZEN_RENEW</c>"] When the account reaches a negative balance, the system will push this event.
        /// </summary>
        [Map("UNFROZEN_RENEW")]
        UnfrozenRenew,
        /// <summary>
        /// ["<c>LIABILITY</c>"] When the account reaches a negative balance, the system will push this event.
        /// </summary>
        [Map("LIABILITY")]
        Liability,
        /// <summary>
        /// ["<c>UNLIABILITY</c>"] When all the liabilities is repaid and the position returns to “EFFECTIVE” status, the system will push this event.
        /// </summary>
        [Map("UNLIABILITY")]
        Unliability
    }
}
