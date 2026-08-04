# Risk Register — dotnet-algorand-sdk

Living register of risks faced by developers and end-users of this SDK and of code
produced by its ARC56 client generator, maintained per
`audits/AI-AUDITS-INSTRUCTIONS.md`. Includes risks this repository has **no ability to
mitigate** — the goal is a complete threat picture for downstream developers, not just
a changelog of fixes. Percentages are estimates of the likelihood the risk is actually
exploited/misused *somewhere* across the SDK's user base within the next 5 years (see
estimation method in the instructions file); they are judgment calls, kept methodologically
consistent across audits, not measured statistics.

Never delete a row. Mark it Mitigated and keep it for historical traceability.

---

## RISK-001 — Private key bytes are never zeroed/disposed from process memory

- **Description**: `KeyPair.ClearTextPrivateKey` and related BouncyCastle
  `Ed25519PrivateKeyParameters` objects held by `Account` are plain, long-lived
  `byte[]`/managed objects with no `Dispose`/explicit-clear support. The raw private
  key can remain in process memory (and be copied during GC compaction) for the
  lifetime of the `Account` object and potentially beyond.
- **Who is exposed**: End users whose keys are held in a long-running developer
  application (e.g. a signer service, a bot, a backend wallet).
- **Mitigable by this repo?**: Partially — best-effort key wiping can be added (see
  `AUDIT-2026-07-16.md` F1), but the CLR provides no strong guarantee that all copies
  are scrubbed (string interning, GC compaction history, swap/hibernation files are
  outside the SDK's control).
- **Current mitigation status**: Mitigated (best-effort) — `KeyPair`/`Account` now
  implement `IDisposable`; `Dispose()` zeroes `ClearTextPrivateKey` and drops
  BouncyCastle key object references. Still only best-effort per the CLR caveats above,
  and callers must remember to call `Dispose()` themselves.
- **5-year misuse likelihood**: **1%** (down from 3%) — the fix removes the "no way to
  even try" gap; residual risk is now only (a) a memory-read attacker catching a copy
  before `Dispose()` is called, or (b) a developer never calling `Dispose()` at all.
  Lowered, not eliminated, since disposal is opt-in and the CLR guarantee remains
  inherently partial.
- **Impact if realized**: Loss of the specific key(s) held by the compromised process.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16 (remediation update, same day)

## RISK-002 — BouncyCastle crypto dependency vendored as an unpinned local DLL

- **Description**: The Ed25519 signing implementation this SDK's entire key security
  model depends on ships as `Libs\BouncyCastle.Crypto.dll`, referenced via a raw
  `HintPath`, not the official versioned NuGet package. No automated tooling
  (Dependabot, `dotnet list package --vulnerable`) can track CVEs against it, and its
  provenance can't be verified from source control alone.
- **Who is exposed**: All developers depending on `Algorand4`, transitively all their
  end users.
- **Mitigable by this repo?**: Yes — migrate to the official pinned NuGet package (see
  `AUDIT-2026-07-16.md` F2).
- **Current mitigation status**: Mitigated — `dotnet-algorand-sdk.csproj` now references
  `BouncyCastle.Cryptography` v2.6.2 via `PackageReference`; the vendored
  `Libs\BouncyCastle.Crypto.dll` was deleted. Standard NuGet/Dependabot-style CVE
  tracking now applies. Verified via full Release build, `AccountTests` golden-transaction
  signing test, and the full `SerialisationTests` suite (23/23), all passing with the new
  package. The Unity build's ILRepack filename filter was updated to match, but could not
  be end-to-end verified in this environment due to an unrelated, pre-existing Unity
  build issue (confirmed present before this fix too) — flagged for a maintainer with a
  working Unity toolchain to confirm.
- **5-year misuse likelihood**: **1%** (down from 4%) — the primary risk (no CVE
  tracking / unverifiable provenance) is resolved; residual risk is only the small
  chance of the Unity ILRepack path silently breaking in a way this environment
  couldn't verify, which would be a build/functionality issue rather than a security
  one.
- **Impact if realized**: Depending on the nature of a hypothetical crypto flaw, up to
  full key compromise across all consumers on the vulnerable version.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16 (remediation update, same day)

## RISK-003 — Exception messages can carry exported key material into application logs

- **Description**: `ApiException.Message`/`.ToString()` embeds up to 512 characters of
  the raw HTTP response body; `ExportKeyAsync`/`ExportMasterKeyAsync` on the Kmd client
  return private/master key material in that body. An application that logs caught
  exceptions verbosely (a very common default pattern) could write key material to
  logs, log aggregators, or crash-reporting services.
- **Who is exposed**: Developers who call Kmd's export endpoints and use conventional
  exception logging; downstream, the end users whose keys were exported.
- **Mitigable by this repo?**: Yes — redact response bodies from exception messages for
  export endpoints, or add explicit doc warnings (see `AUDIT-2026-07-16.md` F3).
- **Current mitigation status**: Mitigated — `ApiException.Message`/`.ToString()` no
  longer embed the raw HTTP response body (only its length); the full body remains
  available via the `Response` property for callers who deliberately want it. Verified
  via new `ApiExceptionTests` covering both the redaction and the null-response case.
  Because `ApiException.cs` is technically NSwag-generated (though not currently
  regenerated in the normal `regen-model` flow), a code comment flags that this patch
  must be reapplied if the file is ever regenerated from scratch.
- **5-year misuse likelihood**: **2%** (down from 12%) — the accidental-logging vector
  via `Message`/`ToString()` is closed; residual risk is now only (a) a caller
  explicitly logging the `.Response` property itself (a deliberate action, not an
  accident) or (b) the fix being silently reverted by a future full regeneration of
  `ApiException.cs` if a maintainer isn't aware of the code comment.
- **Impact if realized**: Loss of the specific exported key(s) that ended up in a log
  sink, plus any secondary exposure from that log sink's own access controls (e.g. a
  shared logging dashboard, a third-party log aggregator).
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16 (remediation update, same day)

## RISK-004 — Kmd's remote-custody model means keys are sent over the wire by design

- **Description**: `Kmd.ImportKeyAsync` sends a raw private key to whatever endpoint
  the developer configured the Kmd client with. This is inherent to Kmd's documented
  purpose (a remote key-management daemon) and is not something this SDK can safely
  change without breaking Kmd's own API contract.
- **Who is exposed**: End users whose keys are imported into a Kmd instance the
  developer chose to point at — if that instance is compromised, misconfigured to
  listen on a public interface, or is not actually the trusted local daemon the
  developer thought it was, the key is directly exposed.
- **Mitigable by this repo?**: No (inherent to Kmd's design) — Partially, in that the
  SDK could add prominent documentation/warnings on the relevant methods.
  Documentation is the achievable mitigation; the underlying network transmission is
  not something this repo can remove.
- **Current mitigation status**: Not applicable (inherent, out of scope) — Partially
  mitigated via recommended documentation (not yet added as of this audit).
- **5-year misuse likelihood**: **8%** — Kmd is typically run locally (AlgoKit
  LocalNet) and rarely exposed to untrusted networks in production Algorand
  deployments, but misconfiguration (e.g. binding Kmd to `0.0.0.0` in a container
  without a firewall rule) is a realistic, previously-seen category of cloud
  misconfiguration across many ecosystems.
- **Impact if realized**: Full compromise of any key imported into the exposed Kmd
  instance.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16

## RISK-005 — Developers hardcode or log mnemonics/private keys in their own application code

- **Description**: Nothing about using this SDK prevents a developer from hardcoding a
  mnemonic in source control, embedding it in client-side JS/mobile code (e.g. via a
  generated ARC56 client used in a frontend), printing it via `Console.WriteLine`, or
  committing a `.env` file containing it. `Account.ToString()` is safely redacted, but
  `Account.ToMnemonic()` returns a plain `string` a developer can do anything with.
- **Who is exposed**: End users of applications built with this SDK, whenever the
  building developer makes this mistake.
- **Mitigable by this repo?**: No — this is a downstream developer practice issue. The
  SDK can only reduce the odds via documentation/samples that model secure patterns
  and never show insecure ones (e.g. sample code should never print a mnemonic to
  console or hardcode one in an example meant to be copy-pasted).
- **Current mitigation status**: Not applicable (inherent, out of scope)
- **5-year misuse likelihood**: **45%** — hardcoded secrets and accidental logging of
  sensitive values are among the most common security mistakes across all ecosystems
  (see e.g. widespread hardcoded-API-key/secret-scanning findings industry-wide); with
  a large enough developer population using this SDK over 5 years, it is near-certain
  that *some* developer somewhere makes this mistake. This is deliberately scored high
  per the instructions' >35% band ("near-certain to be hit by someone at scale").
- **Impact if realized**: Loss of the specific key(s)/mnemonic(s) exposed by the
  affected developer's mistake — bounded to that application's users, not systemic.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16

## RISK-006 — Malicious or compromised Algod/Indexer/Kmd node operator

- **Description**: This SDK, by design, sends signed transactions and (for Kmd)
  signing requests to a node endpoint the developer configures. If that node is
  malicious (e.g. a "free public Algod API" run by an untrustworthy third party) or
  compromised, it can lie about network state (stale/forged block data, censoring
  transactions) and, in the Kmd case specifically, has direct custody of any imported
  keys.
- **Who is exposed**: End users, if the developer chose an untrustworthy node
  provider.
- **Mitigable by this repo?**: No — node trust is an inherent architectural choice made
  by the developer integrating this SDK, not something a client library can enforce.
- **Current mitigation status**: Not applicable (inherent, out of scope)
- **5-year misuse likelihood**: **6%** — most production integrations use well-known
  node providers (Algonode, Nodely, AlgoNode, PureStake historically, or
  self-hosted/AlgoKit nodes); the realistic misuse case is a smaller developer picking
  an unvetted free/public endpoint, which happens but is not the dominant pattern.
- **Impact if realized**: Ranges from transaction censorship/stale-data served to full
  key compromise (if Kmd custody is involved).
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16

## RISK-007 — Host/OS-level compromise (malware, debugger, memory scraping)

- **Description**: Any malware or attacker with code execution on the same host as a
  process using this SDK can read process memory, intercept the CSPRNG output, or hook
  the signing calls directly, regardless of anything this SDK does correctly.
- **Who is exposed**: End users, whenever their device or the developer's server
  hosting keys is compromised at the OS level.
- **Mitigable by this repo?**: No — entirely outside a client library's control.
- **Current mitigation status**: Not applicable (inherent, out of scope)
- **5-year misuse likelihood**: **20%** — host-level malware/infostealers targeting
  crypto wallets are a large and growing, well-documented category across the
  industry; scored in the "common, not the default path, but not requiring an unusual
  misconfiguration" band because it depends on a host being compromised through some
  other channel each time, not zero-click against this SDK.
- **Impact if realized**: Full compromise of any key material accessible to the
  compromised process/host.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16

## RISK-008 — ARC56 client-generator Docker image / generated code supply chain

- **Description**: The `scholtz2/dotnet-avm-generated-client` Docker image (and the
  underlying `client-generator/Program.cs` CLI it wraps) fetches an ARC56 JSON contract
  spec from a URL the developer supplies and emits generated `.cs` client code that
  gets compiled into the developer's application. This audit found the tool only fetches
  the ABI JSON and writes generated source — no key/account data is touched — but a
  compromised Docker image (e.g. a tag takeover on Docker Hub) or a malicious/tampered
  ARC56 JSON spec injected into the generation process are supply-chain vectors this
  repo has only partial control over (control over the source/Dockerfile, not over
  Docker Hub's registry security or a user's choice to trust an unverified spec URL).
- **Who is exposed**: Developers who use the Docker-based generator and, transitively,
  their end users if generated code is compromised.
- **Mitigable by this repo?**: Partially — pin/sign the Docker image, publish
  checksums, and document that ARC56 spec URLs should only be pointed at trusted
  sources.
- **Current mitigation status**: Unmitigated
- **5-year misuse likelihood**: **2%** — requires a specific, uncommon attack (registry
  tag takeover, or a developer pointing the generator at an untrusted spec URL) and the
  generator's actual code surface (ABI-to-stub generation) offers little obvious
  attacker leverage even if triggered, since it doesn't handle key material.
- **Impact if realized**: Malicious generated code compiled into a developer's
  application — impact depends entirely on what that code is made to do, potentially
  significant if it added a hidden network call to a generated client method.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16

## RISK-009 — Unity ILRepack build omits the `MessagePack` package

- **Description**: `dotnet-algorand-sdk/ILRepack.Targets` merges several dependencies
  into the ILRepacked `Algorand.dll` for the Unity build configurations, but does not
  include `MessagePack` (the NuGet package the SDK's signing-critical serialization
  attributes — `MessagePackObject`/`Key`/`IgnoreMember` — depend on at runtime).
  `Algorand4_Unity.nuspec` also doesn't declare `MessagePack` as something the consumer
  must separately install. This is a build/packaging correctness issue, not a
  crypto-weakening one — see `AUDIT-2026-07-16b.md` finding U1.
- **Who is exposed**: Unity/game developers who install the packed `Algorand4_Unity`
  package and hit a missing-type error the first time they serialize or sign a
  transaction.
- **Mitigable by this repo?**: Yes — add `MessagePack` to the ILRepack merge list
  and/or the nuspec dependency group.
- **Current mitigation status**: Mitigated — `ILRepack.Targets` now also merges
  `MessagePack` and `MessagePack.Annotations` (confirmed as a genuine separate
  transitive assembly, not just an alias) alongside the existing dependencies. The
  nuspec's `<dependencies>` group was left unchanged (Microsoft.CSharp,
  System.ComponentModel.Annotations only) with a comment explaining `MessagePack` is
  intentionally merged rather than declared as a separate consumer-installed
  dependency. Verified: standard Release build and both non-network-dependent test
  suites still pass (`SerialisationTests` 23/23, `test` 96/96 excluding the
  LocalNet-dependent Arc4/Arc56 fixtures). The Unity configuration itself still fails
  to *compile* end-to-end due to the pre-existing, unrelated Unity-stub-DLL issue
  (missing `SerializeField`/`Tooltip`/`InspectorName`), confirmed still present in this
  environment — so the ILRepack step's actual runtime output could not be produced or
  verified here. A maintainer with a working Unity toolchain should confirm the packed
  assembly resolves `MessagePack` types at runtime.
- **5-year misuse likelihood**: **N/A — this is a functional/reliability risk, not a
  security-misuse risk.** Recorded here per the audit instructions' registry scope
  (all risks developers/users of this SDK face), but the percentage framework doesn't
  apply since there's no "misuse" — a Unity developer following the package as
  documented would simply hit a broken build, not a security compromise, until fixed.
- **Impact if realized**: Build/runtime failure (`TypeLoadException` or similar) for
  Unity consumers attempting to sign or serialize a transaction — no key-security
  impact.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16 (remediation update, same day)

## RISK-010 — Official SDK example prints a generated mnemonic to the console

- **Description**: `sdk-examples/OfflineSigningExample.cs:26` calls
  `Console.WriteLine("Backup mnemonic:    " + newAccount.ToMnemonic());` on a freshly
  generated account. This models the exact anti-pattern `RISK-005` warns downstream
  developers against — except here it's in code this repo's maintainers directly
  control, unlike `RISK-005` which is about third-party application code. See
  `AUDIT-2026-07-16b.md` finding E1.
- **Who is exposed**: Developers who copy this example's pattern into applications
  that log console output to a file, terminal-capture tool, or CI log, and end users
  whose mnemonics get exposed as a result.
- **Mitigable by this repo?**: Yes — remove or redact the printed mnemonic in the
  example; trivial, no functional impact on the rest of the example.
- **Current mitigation status**: Mitigated — the `Console.WriteLine` of
  `newAccount.ToMnemonic()` was removed and replaced with a comment directing the
  reader to persist the mnemonic via a secure channel of their own choosing; the
  mnemonic remains available in-memory via `newAccount.ToMnemonic()` for the rest of
  the example, unaffected. Verified: `sdk-examples/sdk-examples.csproj` builds with 0
  errors after the change.
- **5-year misuse likelihood**: **1%** (down from 5%) — the example no longer models
  the anti-pattern at all, so residual risk is limited to a developer copying an older
  cached/forked version of the file from before this fix.
- **Impact if realized**: Loss of the specific mnemonic(s) printed and subsequently
  captured by whatever consumed the console output (terminal scrollback, CI logs, a
  captured-output log file).
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16 (remediation update, same day)

## RISK-011 — `Utils.GetRandomAssetMetaHash()` uses `System.Random`, not a CSPRNG

- **Description**: `dotnet-algorand-sdk/Utils/Utils.cs:89-96` generates a 32-byte value
  for the `AssetParams.MetadataHash` field using `System.Random` (time-seeded,
  predictable) rather than a cryptographic RNG. This is **not** in the key-generation or
  signing path — `Account()`'s real key generation uses BouncyCastle's `SecureRandom`,
  confirmed unaffected — and `MetadataHash` is a public, on-chain value with no
  confidentiality requirement. The risk is purely that the method's name/lack of
  documentation could mislead a developer into assuming its output carries a
  cryptographic guarantee it doesn't have. See `AUDIT-2026-07-16c.md` finding G1.
- **Who is exposed**: Developers who call this helper expecting an unpredictable or
  tamper-evident value for asset metadata; no end-user key or fund is directly at risk.
- **Mitigable by this repo?**: Yes — switch to `RandomNumberGenerator`-backed
  generation, or add an XML-doc caveat that the output has no cryptographic guarantee.
  Trivial, low-priority fix.
- **Current mitigation status**: Mitigated — `GetRandomAssetMetaHash()` now fills the
  32-byte buffer via `System.Security.Cryptography.RandomNumberGenerator.Fill(bts)`
  instead of `System.Random`. Verified: `dotnet build --configuration Release` (0
  errors), `SerialisationTests` (23/23) and `test.csproj` non-LocalNet subset (96/96)
  all still pass.
- **5-year misuse likelihood**: **N/A — this is a functional/API-honesty risk, not a
  key-security risk.** Recorded here per the audit instructions' full-threat-picture
  registry scope, but the percentage framework doesn't apply since no plausible misuse
  path leads to key or fund loss — at worst a developer relies on a weaker-than-expected
  "hash" for a public metadata field.
- **Impact if realized**: No direct impact to key/fund security; at most a predictable
  public asset-metadata value where a developer expected unpredictability.
- **First recorded**: 2026-07-16
- **Last reviewed**: 2026-07-16

## RISK-012 — Canonical transaction encoding depends on an unmaintained, single-maintainer bridge package

- **Description**: `dotnet-algorand-sdk/Utils/Encoder.cs`'s canonical msgpack encode path
  (`EncodeToMsgPackOrdered`/`EncodeToMsgPackNoOrder`, used before every transaction
  signature) and its decode path both go through `Newtonsoft.Msgpack`'s
  `MessagePackWriter`/`MessagePackReader`, not the `MessagePack`-CSharp library. The
  `Newtonsoft.Msgpack` NuGet package (`0.1.11`, pinned in `dotnet-algorand-sdk.csproj`) has
  a single maintainer (`darkl`), 12 releases total, and was last published 2019-08-31 —
  `0.1.11` remains the newest version on nuget.org over 6 years later, and it has no
  strong-named build. See `AUDIT-2026-07-18.md` finding F2.
- **Who is exposed**: All developers depending on `Algorand4` for transaction signing,
  transitively all their end users, if a future vulnerability were ever found in this
  package's encode/decode logic with no maintainer available to patch it.
- **Mitigable by this repo?**: Partially — no immediate action possible (no newer version
  exists), but the repo could eventually replace `Newtonsoft.Msgpack`'s role with
  `MessagePack`-CSharp's own APIs (already a direct dependency) to remove the dependency,
  though this is a non-trivial refactor since the ordered-field-encoding behavior the
  canonical-encoding tests depend on comes from `Newtonsoft.Msgpack`'s
  `JsonSerializer`+`ContractResolver` approach specifically.
- **Current mitigation status**: Unmitigated (no known CVE today; risk is supply-chain
  continuity, not a live exploit)
- **5-year misuse likelihood**: **2%** — no known vulnerability exists in this package
  today; the risk is contingent on one being discovered in a package with no active
  maintainer, which is possible but not the default expected outcome for a narrowly-scoped
  serialization bridge with a small, stable feature surface.
- **Impact if realized**: Depending on the nature of a hypothetical parsing flaw, ranges
  from a denial-of-service (malformed input crash) to, in the worst case, a signing/decoding
  integrity issue affecting consumers who can't upgrade past the vulnerable version.
- **First recorded**: 2026-07-18
- **Last reviewed**: 2026-07-18

## RISK-013 — Unused Roslyn `using` in a `MessagePack` formatter (dependency itself confirmed legitimate, not bloat)

- **Description**: `dotnet-algorand-sdk/Algod/Model/Converters/MsgPack/NoDefaultsFormatter.cs:5`
  had a stray `using Microsoft.CodeAnalysis.Host;` with no corresponding type usage
  anywhere in that file. This row was originally filed (same day) as a broader claim that
  the main project's unconditional `Microsoft.CodeAnalysis.CSharp`/`.CSharp.Workspaces`/
  `.Workspaces.Common` `PackageReference`s (`4.14.0`) were unnecessary dependency bloat.
  **That broader claim was wrong and is corrected here**: attempting the fix and rebuilding
  showed `ClientGenerator/Compiler/TealSharpSyntaxWalker.cs` and
  `ClientGenerator/Compiler/CompilerStates/*.cs` (which do use Roslyn syntax/symbol types
  extensively) have no `<Compile Remove>` exclusion in `dotnet-algorand-sdk.csproj`, so
  they compile into the main `Algorand4` package via default SDK-style globbing, not only
  into the separate `ClientGenerator/AlgoStudio.csproj` project as originally assumed.
  Removing the three package references produced 819 `CS0246` build errors, confirming they
  are genuinely required. See `AUDIT-2026-07-18.md` finding F1 for the full correction.
- **Who is exposed**: N/A for the dependency itself (legitimate, not exposure). The only
  real issue was the single unused `using` — no exposure once removed.
- **Mitigable by this repo?**: Yes — remove the stray `using` (the `PackageReference`s
  themselves should stay).
- **Current mitigation status**: Mitigated — the stray `using` was removed same day;
  verified via a full `Release` solution build (0 warnings, 0 errors, `Algorand4` package
  builds and packs). The three Roslyn `PackageReference`s were restored/left in place as
  confirmed-necessary.
- **5-year misuse likelihood**: **N/A — this was a dependency-hygiene finding, not a
  misuse-likelihood risk, and the underlying dependencies are legitimate, not bloat.**
- **Impact if realized**: None — no exposure; the corrected understanding is that these
  dependencies are load-bearing for the main package's own code-generation/compilation
  features, not avoidable bloat.
- **First recorded**: 2026-07-18
- **Last reviewed**: 2026-07-18 (corrected and mitigated same day)

## RISK-014 — `Microsoft.Build.Utilities.Core` CVE-2025-55247 in the Unity build-time helper task

- **Description**: `BuildTasks/IlRepackMergeTask/IlRepackMergeTask.csproj` (a build-time-only
  helper, never shipped in either NuGet package — see `AUDIT-2026-07-16d.md`) referenced
  `Microsoft.Build.Utilities.Core` `17.14.8`, within the vulnerable range
  (`>=17.14.0, <=17.14.8`) of `CVE-2025-55247`/`GHSA-w3q9-fxm7-j8fq`. Commit `b912e53`
  bumped it to `17.14.28`, confirmed to be exactly the first patched version for the
  17.14.x branch. See `AUDIT-2026-07-18.md` finding F3.
- **Who is exposed**: Nobody at runtime (build-tooling-only, not distributed in any
  package); at most a developer building the Unity configuration from source with a
  vulnerable cached package.
- **Mitigable by this repo?**: Yes — already done.
- **Current mitigation status**: Mitigated
- **5-year misuse likelihood**: **<1%** — build-tooling-only dependency, never distributed,
  now on a patched version.
- **Impact if realized**: None expected now that this is patched; historically, at most a
  build-environment-local issue, not a distributed-package issue.
- **First recorded**: 2026-07-18
- **Last reviewed**: 2026-07-18
