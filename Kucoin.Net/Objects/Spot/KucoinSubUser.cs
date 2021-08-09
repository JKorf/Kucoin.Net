namespace Kucoin.Net.Objects.Spot
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
        /// The sub user name
        /// </summary>
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Remarks for this sub user
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
    }
}
