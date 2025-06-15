using System;
using TwitchLib.EventSub.Core.Models.HypeTrain;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

/// <summary>
/// HypeTrain Begin subscription type model
/// <para>Description:</para>
/// <para>A Hype Train begins on the specified channel.</para>
/// </summary>
[Obsolete("This subscription type is deprecated (2025‑06‑13). Use HypeTrainBeginV2 instead.")]
public sealed class HypeTrainBegin : HypeTrainBase
{
    /// <summary>
    /// The number of points contributed to the Hype Train at the current level.
    /// </summary>
    public int Progress { get; set; }
    /// <summary>
    /// The number of points required to reach the next level.
    /// </summary>
    public int Goal { get; set; }

    /// <summary>
    /// The most recent contribution.
    /// </summary>
    public HypeTrainContribution LastContribution { get; set; } = new();
    /// <summary>
    /// The time when the Hype Train expires. The expiration is extended when the Hype Train reaches a new level.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.MinValue;
}