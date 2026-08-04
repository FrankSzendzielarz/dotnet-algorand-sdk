---
description: Run the main unit and serialisation test suites, or a filtered subset
argument-hint: [optional dotnet test --filter expression]
---

Run:

```powershell
dotnet test test/test.csproj $ARGUMENTS
dotnet test SerialisationTests/serialisation-tests.csproj $ARGUMENTS
```

test/Arc4Tests.cs and test/Arc56Tests.cs within test.csproj need an AlgoKit LocalNet running first (`algokit localnet start`, algod on :4001, kmd on :4002) or they'll fail with connection errors — check LocalNet is up before treating those as real failures.

test/Gossip/BlockFetcherTests.cs dials a hardcoded MainNet relay list (GossipHttpConfiguration.MainNetArchival) that rotates over time; DNS failures there (`Name or service not known` / `NameResolutionError`) are relay-list staleness, not a code regression — don't chase them as bugs.

(gossip-client-tests and specflow/algorand_tests are excluded by default — they require live MainNet network access. Ask before running those.)

Report failures grouped by fixture, with the assertion message. Do not modify code unless asked.
