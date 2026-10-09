using System.Text;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.Models.HypeTrain;
using TwitchLib.EventSub.Webhooks;
using TwitchLib.EventSub.Webhooks.Core.Models;

// Executable regression: no provider connection, credentials, subscription activation, or mocked parser.
// Assertions intentionally compile against both the original int DTO and the corrected string DTO.
var failures = new List<string>();
Console.WriteLine($"Core assembly: {typeof(SharedTrainParticipants).Assembly.Location}");
Console.WriteLine($"Participant identity type: {typeof(SharedTrainParticipants).GetProperty(nameof(SharedTrainParticipants.BroadcasterUserId))!.PropertyType.Name}");

foreach (var kind in new[] { "begin", "progress", "end" })
{
    var subscriptionType = "channel.hype_train." + kind;
    var metadata = new WebhookEventSubMetadata
    {
        MessageId = "shared-hype-" + kind,
        MessageType = "notification",
        SubscriptionType = subscriptionType,
        SubscriptionVersion = "2"
    };
    var webhooks = new EventSubWebhooks();
    var errors = new List<string>();
    var callbacks = new List<string>();
    var awaitedCallbacks = 0;
    webhooks.Error += (_, error) => { errors.Add(error.Reason); return Task.CompletedTask; };

    async Task Observe(string actualKind, EventSubMetadata actualMetadata, bool isShared,
        SharedTrainParticipants[]? participants)
    {
        callbacks.Add(actualKind);
        Check(ReferenceEquals(actualMetadata, metadata), "delivery metadata is preserved");
        Check(isShared, "shared train flag is preserved");
        Check(participants?.Length == 1, "exactly one shared participant is delivered");
        if (participants is { Length: 1 })
        {
            // Boxing also makes wrong numeric types an assertion failure instead of a compile failure.
            Check(Equals((object)participants[0].BroadcasterUserId, "4567891234"), "participant ID remains a string above Int32 range");
            Check(Equals((object)participants[0].BroadcasterUserLogin, "alice"), "alphabetic login is preserved");
            Check(Equals((object)participants[0].BroadcasterUserName, "Alice"), "display name is preserved");
        }
        await Task.Yield();
        awaitedCallbacks++;
    }

    webhooks.ChannelHypeTrainBeginV2 += (_, args) => Observe("begin", args.Metadata,
        args.Payload.Event.IsSharedTrain, args.Payload.Event.SharedTrainParticipants);
    webhooks.ChannelHypeTrainProgressV2 += (_, args) => Observe("progress", args.Metadata,
        args.Payload.Event.IsSharedTrain, args.Payload.Event.SharedTrainParticipants);
    webhooks.ChannelHypeTrainEndV2 += (_, args) => Observe("end", args.Metadata,
        args.Payload.Event.IsSharedTrain, args.Payload.Event.SharedTrainParticipants);

    var body = $$"""
        {
          "subscription": {
            "id":"sub-shared-hype", "status":"enabled", "type":"{{subscriptionType}}", "version":"2",
            "cost":0, "condition":{"broadcaster_user_id":"1001"}
          },
          "event": {
            "id":"hype-1", "broadcaster_user_id":"1001", "broadcaster_user_login":"channel",
            "broadcaster_user_name":"Channel", "level":1, "total":10, "progress":5, "goal":20,
            "top_contributions":[], "is_shared_train":true, "type":"regular",
            "shared_train_participants":[{
              "broadcaster_user_id":"4567891234", "broadcaster_user_login":"alice", "broadcaster_user_name":"Alice"
            }],
            "started_at":"2026-10-01T12:00:00Z", "expires_at":"2026-10-01T12:05:00Z",
            "ended_at":"2026-10-01T12:05:00Z", "cooldown_ends_at":"2026-10-01T13:05:00Z"
          }
        }
        """;
    await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(body));
    await webhooks.ProcessNotificationAsync(metadata, stream);

    // TwitchLib catches parser exceptions and returns normally. Callback and error assertions are essential.
    Check(callbacks.SequenceEqual(new[] { kind }), "correct event callback runs exactly once");
    Check(awaitedCallbacks == 1, "the asynchronous callback completes before parser return");
    Check(errors.Count == 0, "no parser error occurs; observed=" + string.Join(',', errors));
    Console.WriteLine($"{subscriptionType}: callbacks={callbacks.Count}, awaited={awaitedCallbacks}, errors={errors.Count}");

    void Check(bool condition, string assertion)
    {
        if (!condition) failures.Add(subscriptionType + ": " + assertion);
    }
}

foreach (var failure in failures) Console.Error.WriteLine("FAIL: " + failure);
Console.WriteLine($"Shared Hype Train wire proof: {(failures.Count == 0 ? "PASS" : "FAIL")}");
return failures.Count == 0 ? 0 : 1;
