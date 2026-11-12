---
description: Bump the NuGet package version
argument-hint: <new version, e.g. 5.0.5>
---

Update the `<Version>` element in `dotnet-algorand-sdk/dotnet-algorand-sdk.csproj` (the non-Unity `PropertyGroup`) to `$ARGUMENTS`. Leave the Unity `PropertyGroup`'s `<Version>` and the `Algorand5_Unity.nuspec`/ILRepack `PostBuildEvent` version alone unless explicitly asked to bump those too — they version independently. Then run `/build` to confirm it still packs (`GeneratePackageOnBuild=True`).
