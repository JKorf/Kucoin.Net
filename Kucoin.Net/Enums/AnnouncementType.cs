using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AnnouncementType>))]
    public enum AnnouncementType
    {
        /// <summary>
        /// ["<c>latest-announcements</c>"] Latest announcements
        /// </summary>
        [Map("latest-announcements")]
        LatestAnnouncements,
        /// <summary>
        /// ["<c>activities</c>"] Activities
        /// </summary>
        [Map("activities")]
        Activities,
        /// <summary>
        /// ["<c>product-updates</c>"] Product updates
        /// </summary>
        [Map("product-updates")]
        ProductUpdates,
        /// <summary>
        /// ["<c>vip</c>"] VIP
        /// </summary>
        [Map("vip")]
        Vip,
        /// <summary>
        /// ["<c>maintenance-updates</c>"] Maintenance updates
        /// </summary>
        [Map("maintenance-updates")]
        MaintenanceUpdates,
        /// <summary>
        /// ["<c>delistings</c>"] Delistings
        /// </summary>
        [Map("delistings")]
        Delistings,
        /// <summary>
        /// ["<c>others</c>"] Others
        /// </summary>
        [Map("others")]
        Others,
        /// <summary>
        /// ["<c>api-campaigns</c>"] Api campaigns
        /// </summary>
        [Map("api-campaigns")]
        ApiCampaigns,
        /// <summary>
        /// ["<c>new-listings</c>"] New listings
        /// </summary>
        [Map("new-listings")]
        NewListings,
        /// <summary>
        /// ["<c>futures-announcements</c>"] Futures announcements
        /// </summary>
        [Map("futures-announcements")]
        FuturesAnnouncements
    }
}
