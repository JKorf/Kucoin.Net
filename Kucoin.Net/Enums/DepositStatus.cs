using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Status of a deposit
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// ["<c>PROCESSING</c>"] In progress
        /// </summary>
        [Map("PROCESSING")]
        Processing,
        /// <summary>
        /// ["<c>SUCCESS</c>"] Successful
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAILURE</c>"] Failed
        /// </summary>
        [Map("FAILURE")]
        Failure,
        /// <summary>
        /// ["<c>PRE_SUCCESS</c>"] Funds have been credited to the account ahead of final Block confirmation.
        /// </summary>
        [Map("PRE_SUCCESS")]
        PreSuccess,
        /// <summary>
        /// ["<c>WAIT_TRM_MGT</c>"] The deposit is undergoing standard compliance verification. Please contact support for assistance.
        /// </summary>
        [Map("WAIT_TRM_MGT")]
        WaitingComplianceVerification,
        /// <summary>
        /// ["<c>TRM_MGT_REJECTED</c>"] Compliance verification rejected
        /// </summary>
        [Map("TRM_MGT_REJECTED")]
        ComplianceVerificationFailed,
        /// <summary>
        /// ["<c>ROLLBACKING</c>"] Rolling back
        /// </summary>
        [Map("ROLLBACKING")]
        RollingBack,
        /// <summary>
        /// ["<c>ROLLBACK</c>"] Rolled back
        /// </summary>
        [Map("ROLLBACK")]
        RolledBack,
        /// <summary>
        /// ["<c>WAIT_RISK_MGT</c>"] Waiting for risk management approval
        /// </summary>
        [Map("WAIT_RISK_MGT")]
        WaitingRiskManagement,
        /// <summary>
        /// ["<c>RISK_MGT_REJECTED</c>"] Risk management rejected
        /// </summary>
        [Map("RISK_MGT_REJECTED")]
        RiskManagementRejected,
    }
}
