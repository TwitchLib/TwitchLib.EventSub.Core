namespace TwitchLib.EventSub.Core.Models.HypeTrain;

public class SharedTrainParticipants
{
    /// <summary>
    /// The ID of the broadcaster participating in the shared Hype Train.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;

    /// <summary>
    /// The login of the broadcaster participating in the shared Hype Train.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;

    /// <summary>
    /// The display name of the broadcaster participating in the shared Hype Train.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;
}
