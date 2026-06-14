using System;
using TwitchLib.EventSub.Core.Models.PowerUp;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

/// <summary>
/// Custom PowerUp Redemption V1 subscription type model
/// <para>Description:</para>
/// <para>A viewer has redeemed a custom Power-up on the specified channel.</para>
/// </summary>
public class CustomPowerUpRedemptionV1
{
    /// <summary>
    /// The redemption identifier.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The requested broadcaster ID.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;

    /// <summary>
    /// The requested broadcaster login.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;

    /// <summary>
    /// The requested broadcaster display name.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;

    /// <summary>
    /// User ID of the user that redeemed the custom Power-up.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Login of the user that redeemed the custom Power-up.
    /// </summary>
    public string UserLogin { get; set; } = string.Empty;

    /// <summary>
    /// Display name of the user that redeemed the custom Power-up.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// The user input provided. Empty string if not provided.
    /// </summary>
    public string UserInput { get; set; } = string.Empty;

    /// <summary>
    /// The status of the custom Power-up redemption.
    /// Defaults to unfulfilled. Possible values are unknown, unfulfilled, fulfilled, and canceled.
    /// </summary>
    public string Status { get; set; } = "unfulfilled";
    
    /// <summary>
    /// Basic information about the custom Power-up that was redeemed, at the time it was redeemed.
    /// </summary>
    public CustomPowerUp CustomPowerUp { get; set; } = new();
    
    /// <summary>
    /// Date when the custom Power-up was redeemed.
    /// </summary>
    public DateTimeOffset RedeemedAt { get; set; }
}
