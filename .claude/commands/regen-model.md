---
description: Regenerate the Algod model/client from a (possibly updated) algod.oas2.json spec, and summarize what changed
---

This project's `Algod/Model/`, `Algod/Model/Transactions/`, and `DefaultApi.cs` are code-generated. Full mechanics are in CLAUDE.md under "Model code generation" — read that section first if unfamiliar.

Steps:
1. If asked to pick up upstream changes, download the latest `algod.oas2.json` from `https://raw.githubusercontent.com/algorand/go-algorand/rel/stable/daemon/algod/api/algod.oas2.json` and diff it against `api-generator/dotnet_templates/algod.oas2.json` before overwriting.
2. Regenerate (java 17 required):
   ```powershell
   java -jar api-generator/generator-1.0.0-jar-with-dependencies.jar template -s api-generator/dotnet_templates/Transactions.json -t api-generator/dotnet_templates/ -m dotnet-algorand-sdk/Algod/Model/Transactions -p api-generator/dotnet_templates/transactions_model_config.properties
   java -jar api-generator/generator-1.0.0-jar-with-dependencies.jar template -s api-generator/dotnet_templates/algod.oas2.json -t api-generator/dotnet_templates -m dotnet-algorand-sdk/Algod/Model/ -p api-generator/dotnet_templates/algod_model_config.properties
   java -jar api-generator/generator-1.0.0-jar-with-dependencies.jar template -s api-generator/dotnet_templates/algod.oas2.json -t api-generator/dotnet_templates -c dotnet-algorand-sdk/Algod/ -p api-generator/dotnet_templates/algod_default_config.properties
   ```
3. `git diff --stat` to see which `.generated.cs`/`.Generated.cs` files changed, and inspect each for added/removed/renamed properties.
4. For any new/renamed type, check whether a hand-written partial (`Foo.cs`) needs a matching update.
5. Build and run tests (`/build`, `/test`) and fix breakage.

Report a summary of added/removed/renamed model fields — this is the basis for release notes.
