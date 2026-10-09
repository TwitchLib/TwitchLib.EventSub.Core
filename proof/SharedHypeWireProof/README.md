# Shared Hype Train wire regression (#3386)

This executable fixture feeds synthetic begin, progress, and end v2 notifications
through the actual `TwitchLib.EventSub.Webhooks` 3.0.0 parser. Its Core dependency
is the sibling source project, so it tests the candidate DTO rather than a cached
Core package. It makes no provider connections and needs no credentials.

Every notification contains participant ID `"4567891234"` (above Int32 range),
login `"alice"`, and display name `"Alice"`. It requires the corresponding awaited
callback exactly once, unchanged metadata and string fields, and zero parser
errors. The original numeric DTO compiles with the fixture but fails at runtime:
TwitchLib emits `Application_Error` and returns without invoking the callback.

Run from the repository root, choosing a task-owned package cache:

```sh
export NUGET_PACKAGES=/tmp/shared-hype-wire-packages
dotnet restore proof/SharedHypeWireProof/SharedHypeWireProof.csproj \
  --source https://api.nuget.org/v3/index.json --disable-parallel \
  -p:TargetFrameworks=net10.0 -p:TargetFramework=net10.0
dotnet build proof/SharedHypeWireProof/SharedHypeWireProof.csproj \
  --no-restore -m:1 -p:TargetFrameworks=net10.0 \
  -p:UseSharedCompilation=false -p:BuildInParallel=false
dotnet proof/SharedHypeWireProof/bin/Debug/net10.0/SharedHypeWireProof.dll
```

On SDK installations missing .NET 10 framework pruning metadata, add
`-p:AllowMissingPrunePackageData=true` to restore and build. This is an environment
workaround, not part of the DTO correction.

For the negative control, temporarily restore only
`TwitchLib.EventSub.Core/Models/HypeTrain/SharedTrainParticipants.cs` from the
original source commit, rebuild, and require exit code 1. Restore the correction
exactly, rebuild, and require exit code 0. Preserve both logs.

The fixture covers the SDK wire boundary. It does not prove application owner
routing, canonical fact publication, durable receipt settlement, or live delivery.
Those remain ChannelCheevos #3386 acceptance checks after a real corrected package
becomes available. This source patch does not change a package version or publish
an artifact. The repository's MIT license applies.
