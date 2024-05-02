namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user info
    /// </summary>
    public class KucoinSubUser
    {
        /// <summary>
        /// The sub user id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The uid
        /// </summary>
        public string Uid { get; set; } = string.Empty;
        /// <summary>
        /// The sub user name
        /// </summary>
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Account type
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Remarks for this sub user
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
        /// <summary>
        /// Access level
        /// </summary>
        public string Access { get; set; } = string.Empty;
    }
}
