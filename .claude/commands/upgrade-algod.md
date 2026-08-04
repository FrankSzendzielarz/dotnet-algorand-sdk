---
description: Upgrade the SDK to track a new algod build version end-to-end — spec refresh, model regen, version bump everywhere, tests
argument-hint: <new version, e.g. 4.7.4>
---

Full upgrade checklist for moving this SDK from one algod build version to `$ARGUMENTS`. Do not skip steps — this exists because version bumps have previously drifted (see `audits/` for past incidents of independent, out-of-sync version strings). Work through them in order and report what changed at the end.

## 1. Refresh the OpenAPI spec and regenerate the model

Follow `/regen-model`'s mechanics (also documented in CLAUDE.md under "Model code generation"):

1. Download the latest `algod.oas2.json` for the target algod version from `https://raw.githubusercontent.com/algorand/go-algorand/rel/stable/daemon/algod/api/algod.oas2.json` (or the specific commit if the target version pins one — check `.github/workflows/generator_build.yml` for the currently-pinned commit hash first).
2. Diff it against `api-generator/dotnet_templates/algod.oas2.json` *before* overwriting — this diff is the actual protocol change surface for this upgrade. Read it carefully: new endpoints, new/removed/renamed fields, changed types, changed required-ness.
3. Overwrite `api-generator/dotnet_templates/algod.oas2.json` with the new spec.
4. Regenerate (java 17 required):
   ```powershell
   java -jar api-generator/generator-1.0.0-jar-with-dependencies.jar template -s api-generator/dotnet_templates/Transactions.json -t api-generator/dotnet_templates/ -m dotnet-algorand-sdk/Algod/Model/Transactions -p api-generator/dotnet_templates/transactions_model_config.properties
   java -jar api-generator/generator-1.0.0-jar-with-dependencies.jar template -s api-generator/dotnet_templates/algod.oas2.json -t api-generator/dotnet_templates -m dotnet-algorand-sdk/Algod/Model/ -p api-generator/dotnet_templates/algod_model_config.properties
   java -jar api-generator/generator-1.0.0-jar-with-dependencies.jar template -s api-generator/dotnet_templates/algod.oas2.json -t api-generator/dotnet_templates -c dotnet-algorand-sdk/Algod/ -p api-generator/dotnet_templates/algod_default_config.properties
   ```
5. `git diff --stat` to see which `.generated.cs`/`.Generated.cs` files actually changed, and inspect each for added/removed/renamed properties.
6. For every new/renamed/removed model field or endpoint found in steps 2/5, check whether a hand-written sibling partial (`Foo.cs`, never the `.generated.cs`/`.Generated.cs` file) needs a matching update — constructors, MessagePack `Key` ordering, helper methods, signing logic. This is the "implement the protocol changes" step; do not treat a clean regen as done if the diff shows new fields nobody wired up.
7. If `.github/workflows/generator_build.yml` pins a specific go-algorand commit, update that pinned commit hash to match the new spec source.

If the spec has not changed at all for this bump (patch releases sometimes don't touch algod.oas2.json), say so explicitly and skip to step 2 — don't fabricate a diff.

## 2. Bump the version string everywhere

The package version is a **single source of truth**: `<AlgorandSdkVersion>` in `dotnet-algorand-sdk/dotnet-algorand-sdk.csproj` (see the comment above it) drives both the regular `Algorand4` package and `Algorand4_Unity` — do not add a second independent version anywhere in the csproj.

1. Update `<AlgorandSdkVersion>` in `dotnet-algorand-sdk/dotnet-algorand-sdk.csproj` to `$ARGUMENTS.$([System.DateTime]::UtcNow.ToString(yyyyMMddHH))`.
2. Grep the whole repo for the *old* version string (e.g. `git grep -n '4\.7\.3'` for a 4.7.3→4.7.4 bump) to catch every remaining occurrence — do not rely on this checklist's fixed list alone, since new ones get added over time. As of this writing that list includes:
   - `compose-client-generator-docker.sh` — the `ver=` default.
   - `.github/workflows/client-generator-docker.yml` — the `workflow_dispatch.inputs.version` description example and `default`, and the fallback `ver="..."` in the "Determine version" step.
   - This file's own `argument-hint` example above.
   Update every hit that represents the *current* SDK/image version (skip hits inside `audits/*.md`, changelogs, or other historical/point-in-time records — those describe the past on purpose).
3. Do not touch the Unity `<Version>`/`<NuspecProperties>` separately — they already derive from `$(AlgorandSdkVersion)`.

## 3. Build and test

1. Run `/build` (restore + Release build) and fix any breakage from the model regen.
2. Also build the Unity configuration: `dotnet build dotnet-algorand-sdk.sln --configuration Unity --no-restore` — a model change can compile fine in Release but the ILRepacked Unity assembly is a separate artifact worth checking.
3. Run `/test` and fix any failures caused by the spec/model changes. LocalNet- and MainNet-dependent fixtures follow the usual exclusions from CLAUDE.md — don't chase those as regressions unless they're clearly related to this upgrade.

## 4. Report

Summarize: the algod version this upgrade targets, whether the OpenAPI spec changed and what protocol-level changes it contained, which model files/endpoints were added/changed/removed and how the hand-written partials were updated to match, every file where the version string was bumped, and final build/test status.
