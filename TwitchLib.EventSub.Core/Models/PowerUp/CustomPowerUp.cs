namespace TwitchLib.EventSub.Core.Models.PowerUp;

public class CustomPowerUp
{
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The title of the custom Power-up.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    public int Bits { get; set; }

    public string? Prompt { get; set; }
}
