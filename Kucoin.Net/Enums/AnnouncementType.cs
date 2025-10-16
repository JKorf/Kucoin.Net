using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AnnouncementType>))]
    public enum AnnouncementType
    {
        /// <summary>
        /// Latest announcements
        /// </summary>
        [Map("latest-announcements")]
        LatestAnnouncements,
        /// <summary>
        /// Activities
        /// </summary>
        [Map("activities")]
        Activities,
        /// <summary>
        /// Product updates
        /// </summary>
        [Map("product-updates")]
        ProductUpdates,
        /// <summary>
        /// VIP
        /// </summary>
        [Map("vip")]
        Vip,
        /// <summary>
        /// Maintenance updates
        /// </summary>
        [Map("maintenance-updates")]
        MaintenanceUpdates,
        /// <summary>
        /// Delistings
        /// </summary>
        [Map("delistings")]
        Delistings,
        /// <summary>
        /// Others
        /// </summary>
        [Map("others")]
        Others,
        /// <summary>
        /// Api campaigns
        /// </summary>
        [Map("api-campaigns")]
        ApiCampaigns,
        /// <summary>
        /// New listings
        /// </summary>
        [Map("new-listings")]
        NewListings,
        /// <summary>
        /// Futures announcements
        /// </summary>
        [Map("futures-announcements")]
        FuturesAnnouncements
    }
}
