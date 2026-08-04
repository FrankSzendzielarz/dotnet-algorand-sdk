# AI Audit Instructions — dotnet-algorand-sdk

## Purpose

This document instructs an AI agent (or human auditor) on how to conduct a recurring
security audit of this repository. The **single most important goal** of every audit is:

> **Give developers who depend on this SDK justified confidence that it cannot steal,
> leak, or misuse their users' private keys, mnemonics, or signed transactions — and
> clearly document the residual risks that remain even when the SDK behaves correctly.**

This is a wallet-adjacent library: it generates keys, signs transactions, talks to
untrusted network endpoints (Algod/Indexer/Kmd nodes, gossip relays), and drives a
code-generation pipeline (ARC56 client generator, including a Docker-based generator)
that produces code developers will compile and run. Each of these is a place where key
material could leak or where generated/third-party code could act maliciously.

Audits are cumulative: read the most recent prior audit and `RISKS.md` before starting,
note what changed since then (`git log` since the last audit's commit hash), and update
rather than blindly re-deriving everything from scratch.

## Audit cadence and output

- Each audit produces a new file `audits/AUDIT-YYYY-MM-DD.md` (UTC date of the audit).
- Each audit updates `audits/RISKS.md` (see below) — never overwrite the risk register,
  extend/revise it.
- Never edit past `audits/AUDIT-*.md` files after the fact — if a finding turns out to be
  wrong, correct it in the *next* audit's file with a note referencing the original.
- Record the git commit hash the audit was performed against at the top of the audit file.

## Scope — what every audit must examine

### 1. Key material lifecycle
- Where are private keys, seeds, and mnemonics generated? Confirm the RNG is
  cryptographically secure (e.g. `System.Security.Cryptography.RandomNumberGenerator`),
  never `System.Random`, `Guid.NewGuid()`-as-entropy, or time-seeded generators.
- Where do private keys live in memory? Prefer byte arrays that can be cleared
  (`Array.Clear`) over `string`/`SecureString`-free representations that linger in the
  GC heap and can't be wiped deterministically. Flag any `string` that holds raw key or
  mnemonic material.
- Does any `ToString()`, `Equals()`, exception message, or default serialization
  (JSON/msgpack `[Key]` attributes) risk printing/logging/transmitting key material?
  Check logging statements, unhandled exception paths, and any `Console.Write`/`Debug.*`
  near key-handling code.

### 2. Signing paths
- Confirm all transaction/logic-sig signing happens **fully offline** — no network call
  is made using the private key, and the only bytes that leave the process after signing
  are the signed transaction blob (public by design once submitted).
- Kmd is the one intentional exception (it is a remote key-management daemon by design).
  Explicitly verify: does the SDK ever *send* a raw private key/mnemonic *to* Kmd over the
  wire, or does it only ever ask Kmd to sign / import via Kmd's own documented API? Flag
  any deviation from "Kmd receives at most what its own API contract calls for."

### 3. Network surface
- Enumerate every place the SDK makes outbound HTTP/network calls (Algod, Indexer, Kmd,
  gossip/relay clients). For each client, confirm no code path attaches private keys,
  mnemonics, or seeds to a request URL, header, or body outside the Kmd exception above.
- Check default endpoints/hostnames hardcoded in the SDK (e.g. relay lists, sample
  base URLs) — are they to legitimate, well-known Algorand infrastructure? Flag anything
  pointing at an unfamiliar or unexplained host.
- Check TLS/certificate handling — no disabled certificate validation, no accepting
  arbitrary/self-signed certs by default.

### 4. Dependency supply chain
- List all `PackageReference`s in the main SDK project(s) and the client generator
  project(s). For each cryptography-relevant dependency (ed25519, sha512, msgpack,
  json), note whether it's a well-known, actively maintained package or an obscure/
  unmaintained one, and whether the pinned version has known CVEs.
- Confirm the Docker-based ARC56 client generator (`scholtz2/dotnet-avm-generated-client`)
  does not exfiltrate the user's contract spec/ABI JSON to a remote server as a side
  effect of generating code, and that generated client code contains no embedded
  credentials, telemetry, or key material — it should be pure typed method stubs calling
  the SDK's own Algod client.
- Note the update/patch cadence risk: does this repo have a process for reacting quickly
  to a CVE in a signing/crypto dependency?

### 5. Generated code / code-gen pipeline
- `Algod/Model/*.generated.cs` and the ARC56 client generator both produce code that
  developers compile directly into their applications. Confirm the Velocity templates
  (`api-generator/dotnet_templates/*.vm`) and generator jar do not embed any network
  calls, telemetry, or key handling beyond what the hand-written partner classes already
  do — a compromised template is a supply-chain risk affecting every consumer.

### 6. Unity build variant
- The ILRepacked Unity assembly bundles all dependencies into one binary. Confirm the
  ILRepack config doesn't silently swap in a different (weaker) crypto implementation
  and that the merge doesn't strip any security-relevant code paths.

## Severity and confidence rubric

For every finding, assign:
- **Severity**: Critical / High / Medium / Low / Informational
- **Confidence**: Confirmed (reproduced/read directly) / Plausible (inferred, not
  executed) — do not report a finding as Confirmed unless you actually read the exact
  code path and traced the data flow.
- **Status**: New / Recurring (seen in a prior audit — cite it) / Resolved since last
  audit (cite the commit that fixed it).

## `RISKS.md` — the standing risk register

Every audit must maintain `audits/RISKS.md`, a living register of **all risks a
developer or end-user of this SDK (or of code produced by the ARC56 client generator)
could face** — including risks the SDK maintainers have **no ability to mitigate**
(e.g. "a developer hardcodes a mnemonic in client-side JS after using a generated
client," "a user's OS-level malware reads process memory," "an Algod node operator the
developer chose to trust is malicious"). Do not omit a risk just because it's outside
this repo's control — the point of the register is to give downstream developers a
complete threat picture, not just a changelog of code fixes.

For each risk, `RISKS.md` must record:

| Field | Meaning |
|---|---|
| **ID** | Stable short ID, e.g. `RISK-001`, never renumbered/reused |
| **Description** | Precise statement of the risk and the affected component (SDK core, Kmd client, ARC56 client generator, generated app code, Unity build, etc.) |
| **Who is exposed** | End users (wallet holders), developers, node operators, etc. |
| **Mitigable by this repo?** | Yes / Partially / No — and if Yes/Partially, what mitigation exists or is recommended |
| **Current mitigation status** | Mitigated / Partially mitigated / Unmitigated / Not applicable (inherent, out of scope) |
| **5-year misuse likelihood** | A percentage estimate of the probability this risk is actually exploited/misused *somewhere* across the SDK's user base within the next 5 years (see estimation method below) — plus one sentence justifying the number |
| **Impact if realized** | Loss of funds / loss of a single key / loss of many keys / denial of service / no direct impact, etc. |
| **First recorded** | Audit date it was first added |
| **Last reviewed** | Audit date it was last reconfirmed/updated |

### Estimating the 5-year misuse-likelihood percentage

This is a judgment call, not a measured statistic — be explicit that it is an estimate,
and keep the method consistent across audits so the numbers are comparable over time:

- Base the estimate on: how many developers/apps realistically depend on this exact code
  path, how easy the risk is to trigger (does it require an unusual misconfiguration, or
  is it the default behavior?), whether it requires an attacker to have already achieved
  some other foothold (network position, local code execution, malicious node) versus
  being remotely triggerable with no prerequisites, and whether similar issues have
  caused real incidents in comparable ecosystems (other chain SDKs, wallet libraries).
- Use these anchor bands and pick within them rather than inventing ad hoc numbers:
  - **<1%** — requires multiple independent unlikely failures/attacker capabilities
    stacked together, or affects a code path that is opt-in and rarely used.
  - **1–10%** — plausible in a realistic misuse/misconfiguration scenario, but not the
    default path; requires a specific developer mistake or attacker positioning.
  - **10–35%** — a common developer mistake this SDK makes easy to fall into (e.g. an
    insecure-by-default sample/pattern in the README that gets copy-pasted), or a risk
    inherent to a widely-used dependency/integration.
  - **>35%** — near-certain to be hit by *someone* at scale over 5 years (e.g. "some
    developer will log an exception containing a mnemonic because the SDK doesn't warn
    against it") — reserve this band for risks with essentially no barrier to occurrence.
- Re-review every existing risk's percentage at each audit; adjust up/down with a
  one-line note on why (e.g. "no incidents observed since last audit, lowering to X%" or
  "similar issue caused a real incident in <other SDK>, raising to Y%").
- Never delete a risk entry, even if it becomes fully mitigated — mark it "Mitigated"
  and keep it for historical traceability; lower its percentage to reflect that
  exploitation now requires the fix to be bypassed or reverted.

## Report format for `audits/AUDIT-YYYY-MM-DD.md`

```markdown
# Security Audit — dotnet-algorand-sdk — YYYY-MM-DD

Audited commit: <git hash>
Auditor: AI agent (Claude) per audits/AI-AUDITS-INSTRUCTIONS.md
Previous audit: audits/AUDIT-<prev-date>.md (or "none — first audit")

## Summary
<2-5 sentence top-line verdict: does the SDK, as of this commit, put user private keys
at risk? What changed since the last audit?>

## Scope covered
<bullet list of which of the six areas above were reviewed, and what was explicitly
out of scope this time (e.g. "gossip-client-tests network path not re-verified this
cycle")>

## Findings
For each finding: Severity, Confidence, Status, file:line references, description of
the concrete failure scenario, and a recommendation.

## Risk register changes
<what was added/updated/re-scored in RISKS.md this cycle, and why>

## Recommendations for maintainers
<prioritized, actionable list>
```

## Ground rules for the auditing AI

- Read actual code — do not assume behavior from naming or from memory of "how SDKs
  usually work." Cite `file:line`.
- Prefer false negatives over false positives you can't back up: a finding must trace a
  real, readable code path. Mark inferred-but-unverified concerns as Confidence:
  Plausible, not Confirmed.
- Do not silently skip Kmd, the gossip client, or the ARC56 client generator just because
  they're less central than Algod — they are explicitly in scope every audit.
- Do not modify SDK source code as part of an audit. An audit only produces/updates
  files under `audits/`. If a fix is warranted, recommend it in the report; a separate,
  explicit task should implement the fix.
- Keep language calibrated — this document exists to build developer trust, which
  requires the audits to be credible, not maximally alarming or maximally reassuring.
