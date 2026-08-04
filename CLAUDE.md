# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A .NET SDK (`Algorand4` NuGet package, target `netstandard2.1`) for talking to Algorand nodes: Algod, Indexer, Kmd, and the gossip network, plus transaction building/signing and an ARC56 smart-contract client generator. There is also a Unity build variant (single ILRepacked assembly). NuGet package version corresponds to the algod build it targets. Full docs: https://frankszendzielarz.github.io/dotnet-algorand-sdk/api/index.html

## Build / test commands

```powershell
dotnet restore
dotnet build dotnet-algorand-sdk.sln --configuration Release --no-restore   # Unity build: --configuration Unity
dotnet test test/test.csproj                                                # main unit tests (NUnit)
dotnet test test/test.csproj --filter "FullyQualifiedName~Arc56Tests"       # single fixture
dotnet test test/test.csproj --filter "Name=SpecificTestMethod"             # single test
dotnet test SerialisationTests/serialisation-tests.csproj                   # msgpack/canonical-encoding round-trip tests
dotnet test gossip-client-tests/gossip-client-tests.csproj                  # gossip network client tests (needs live network access)
```

`test/Arc4Tests.cs` and `test/Arc56Tests.cs` deploy contracts against `http://localhost:4001` (algod) and `http://localhost:4002` (kmd) — they need an [AlgoKit](https://developer.algorand.org/docs/get-started/algokit/) LocalNet running (`algokit localnet start`) before `dotnet test test/test.csproj` will pass. `test/Gossip/BlockFetcherTests.cs` and `gossip-client-tests` instead hit Algorand MainNet directly and need outbound network access, not LocalNet — and `BlockFetcherTests` specifically dials the hardcoded relay hostnames in `GossipHttpConfiguration.MainNetArchival`, which rotate over time (some no longer resolve via public DNS even with working internet), so treat its failures as relay-list staleness rather than a real regression unless you've confirmed the relay is actually up.

`Encoder.EncodeToJson` explicitly forces `\r\n` line endings (see `Utils/Encoder.cs`) rather than relying on `Environment.NewLine`, so indented-JSON output — and the hardcoded fixture strings compared against it in `ModelTests.cs` — is identical on Windows and Linux. Don't remove that override; without it, those fixture tests fail on Linux CI while passing locally on Windows.

`specflow/algorand_tests.csproj` contains SpecFlow integration tests that exercise a real Algod/Indexer/Kmd (e.g. AlgoKit `localnet`) — not run as part of a normal unit-test pass; see `specflow/test_harness.ps1`.

The solution has configurations `Debug;Release;Test_Debug;Integration_Test_Debug;Unity;Unity_debug` — `Unity`/`Unity_debug` build the ILRepacked single-assembly package for Unity via `dotnet-algorand-sdk/ILRepack.Config.props`/`ILRepack.Targets`.

## Architecture

### Model code generation (important — don't hand-edit generated files)

Most of `Algod/Model/`, `Algod/Model/Transactions/`, and the Algod client (`DefaultApi.cs`) are generated from Algorand's official OpenAPI spec, not written by hand. The recommended client class is `AlgodClient` (defined in `AlgodClient.cs`), which derives from the generated `DefaultApi` for backward compatibility.

- Spec source: `api-generator/dotnet_templates/algod.oas2.json`, pinned to a specific commit of `algorand/go-algorand`'s `daemon/algod/api/algod.oas2.json` (see `.github/workflows/generator_build.yml` for the exact URL/commit — updating the model means bumping that commit and re-downloading).
- Generator: `api-generator/generator-1.0.0-jar-with-dependencies.jar` (a Java 17 tool, Velocity templates in `api-generator/dotnet_templates/*.vm`), invoked three times:
  1. Transactions model — `-s .../Transactions.json -m dotnet-algorand-sdk/Algod/Model/Transactions -p transactions_model_config.properties`
  2. Algod model — `-s .../algod.oas2.json -m dotnet-algorand-sdk/Algod/Model/ -p algod_model_config.properties`
  3. Algod client — `-s .../algod.oas2.json -c dotnet-algorand-sdk/Algod/ -p algod_default_config.properties`
- Output convention: every generated model `Foo` produces `Foo.generated.cs` (or `Foo.Generated.cs` under `Transactions/`), a `partial class Foo` with only the OpenAPI-derived properties. Hand-written behavior (constructors, signing, helper methods, MessagePack customization) lives in a sibling **non**-generated `Foo.cs` partial class — always edit the hand-written file, never the `.generated.cs`/`.Generated.cs` one, since it gets overwritten by the next codegen run.
- `*_config.properties` files (`model_skip`, `property_skip`, `namespace`, ...) control which types/members the generator skips (typically because a hand-written equivalent already exists, e.g. `Address`, `Signature`, `TEALProgram`).
- The Indexer model (`Indexer/Model/`) is intentionally a separate, broader model than Algod's — historical/deprecated fields that Algod drops are kept there so Indexer can still describe old data.

### Serialization

Types are dual-annotated for both `Newtonsoft.Json` (`JsonProperty`) and `MessagePack` (`MessagePackObject`/`Key`) since Algorand transactions are canonically msgpack-encoded and signed; `Utils/` and `SerialisationTests/` cover canonical encoding, msgpack formatters, and signing round-trips. Be careful with field ordering/omission rules when touching serialization — they affect transaction IDs and signatures.

### Client generator (ARC56)

`dotnet-algorand-sdk/ClientGenerator` (project `AlgoStudio`) and `client-generator/` generate a typed C# client from an ARC56 smart-contract JSON spec, distributed as the `scholtz2/dotnet-avm-generated-client` Docker image (see `compose-client-generator-docker.sh`, `client-generator/Dockerfile`). Usage examples: `test/Arc56Tests.cs`, README "ARC56 DotNet c# Client generator" section.

### Signing key

`dotnet-algorand-sdk/Algorand.snk` strong-names the assembly (`SignAssembly=true`) — required for the build to produce a valid package.

### Publishing

`.github/workflows/publish-nuget.yml` builds, tests, and pushes the `Algorand4` NuGet package using nuget.org **Trusted Publishing** (GitHub OIDC — no long-lived `NUGET_API_KEY` secret). It fires on `v*` tags or manual dispatch. Requires a one-time Trusted Publishing policy on nuget.org (repository owner `scholtz`, repo `dotnet-algorand-sdk`, workflow file `publish-nuget.yml`, environment `nuget-publish`) and a `NUGET_USER` secret (your nuget.org profile name) in that environment.
