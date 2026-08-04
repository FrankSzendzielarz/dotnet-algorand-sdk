# Algorand .NET SDK (`Algorand4`)

[![NuGet](https://img.shields.io/nuget/v/Algorand4.svg?label=Algorand4)](https://www.nuget.org/packages/Algorand4/)
[![NuGet Unity](https://img.shields.io/nuget/v/Algorand4_Unity.svg?label=Algorand4_Unity)](https://www.nuget.org/packages/Algorand4_Unity/)
[![License: GPL-3.0](https://img.shields.io/badge/License-GPLv3-blue.svg)](LICENSE)

A comprehensive .NET SDK for the [Algorand](https://www.algorand.co/) blockchain (and AVM-compatible networks such as Voi and Aramid). It provides everything needed to build, sign, and submit transactions, query the chain, and interact with smart contracts from any .NET application (`netstandard2.1`), including a dedicated Unity build.

**Full API documentation:** <https://scholtz.github.io/dotnet-algorand-sdk/>

## Table of contents

- [Features](#features)
- [Installation](#installation)
- [Quick start](#quick-start)
- [Core concepts](#core-concepts)
- [Examples](#examples)
- [ARC56 smart-contract client generator](#arc56-smart-contract-client-generator)
- [Building and testing from source](#building-and-testing-from-source)
- [Versioning](#versioning)
- [License](#license)

## Features

- **Algod client** — generated from the official Algorand OpenAPI spec; query node state, submit transactions, simulate, and compile TEAL.
- **Indexer client** — rich queries over historical blocks, transactions, accounts, assets, and applications.
- **KMD client** — use an Algorand node's Key Management Daemon as a wallet/key store.
- **Transaction model** — strongly typed classes for payment, asset (ASA), application call, key registration, multisig, logic-signature, and rekeying transactions, with canonical msgpack encoding and Ed25519 signing.
- **Atomic transfers** — build and sign grouped transactions.
- **ABI / ARC4 / ARC56 support** — encode/decode ABI values and generate fully typed C# clients from ARC56 contract specs.
- **Gossip network client** — connect directly to the Algorand relay network and fetch blocks without an API service.
- **Unity build** — single ILRepacked assembly (`Algorand4_Unity`) that avoids Newtonsoft/CodeDom conflicts and supports WebGL via an injectable HTTP shim.

## Installation

Install the [`Algorand4`](https://www.nuget.org/packages/Algorand4/) package from NuGet:

```powershell
dotnet add package Algorand4
```

or via the Package Manager console:

```powershell
Install-Package Algorand4
```

For Unity projects use [`Algorand4_Unity`](https://www.nuget.org/packages/Algorand4_Unity/), a single wrapped assembly usable directly in Unity. Its `HttpClientConfigurator` accepts an optional shim parameter so WebGL builds can delegate to a different HTTP client.

## Quick start

### 1. Get a node

The fastest way to get a local Algorand node with funded test accounts is [AlgoKit](https://dev.algorand.co/algokit/algokit-intro/):

```bash
algokit localnet start
```

This starts algod on `http://localhost:4001` and KMD on `http://localhost:4002` (token: 64 × `a`), which matches the SDK's built-in `AlgodConfiguration.DockerNet` preset.

### 2. Connect, query, and send a payment

A complete, runnable program ([full source](sdk-examples/BasicExample.cs)):

```cs
using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;

try
{
    // Restore an account from a 25-word mnemonic.
    // On LocalNet: `algokit goal account list` then `algokit goal account export -a <address>`
    var src = new Account("arrive transfer silent pole congress loyal snap dirt dwarf relief easily plastic federal found siren point know polar quit very vanish ensure humor abstract broken");
    var DEST_ADDR = "5KFWCRTIJUMDBXELQGMRBGD2OQ2L3ZQ2MB54KT2XOQ3UWPKUU4Y7TQ4X7U";

    // Connect to a node. Presets: MainNet, TestNet, BetaNet, DockerNet (AlgoKit LocalNet), VoiMain, AramidMain
    using var httpClient = HttpClientConfigurator.ConfigureHttpClient(AlgodConfiguration.DockerNet);
    var algod = new AlgodClient(httpClient);

    // Query the node
    var supply = await algod.GetSupplyAsync();
    Console.WriteLine($"Total Algorand Supply: {supply.TotalMoney}");

    var accountInfo = await algod.AccountInformationAsync(src.Address.ToString(), null, null);
    Console.WriteLine($"Account Balance: {accountInfo.Amount} microAlgos");

    // Build, sign, submit, and confirm a payment transaction
    var transParams = await algod.TransactionParamsAsync();
    var amount = Utils.AlgosToMicroalgos(1);
    var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(
        src.Address, new Address(DEST_ADDR), amount, "pay message", transParams);
    var signedTx = tx.Sign(src);

    var id = await Utils.SubmitTransaction(algod, signedTx);
    Console.WriteLine($"Successfully sent tx with id: {id.Txid}");

    var resp = await Utils.WaitTransactionToComplete(algod, id.Txid);
    Console.WriteLine($"Confirmed Round is: {resp.ConfirmedRound}");
}
catch (ApiException<ErrorResponse> e)
{
    // Errors returned by the node carry details in e.Result.Message
    Console.WriteLine($"Error: {e.Result.Message}");
}
```

To connect to a custom node, pass the host and API token explicitly:

```cs
var httpClient = HttpClientConfigurator.ConfigureHttpClient(
    "http://localhost:4001/",
    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
var algod = new AlgodClient(httpClient);
```

## Core concepts

| Concept | Type(s) | Notes |
|---|---|---|
| Node connection | `HttpClientConfigurator`, `AlgodConfiguration` | Creates a configured `HttpClient`; pass it to a client proxy. |
| Algod API | `Algorand.Algod.AlgodClient`, `CommonApi` | Use `AlgodClient` for the primary Algod API. Derives from `DefaultApi` for backward compatibility. The `Common` API set covers additional network interaction. (`Private` APIs are node-admin-only and not exposed.) |
| Accounts & keys | `Algorand.Algod.Model.Account`, `Address` | `new Account(mnemonic)` restores a key pair; `new Account()` generates one. `Account` also signs transactions. |
| Transactions | `Algorand.Algod.Model.Transactions.*` | `PaymentTransaction`, `AssetCreateTransaction`, `ApplicationCallTransaction`, `KeyRegistrationTransaction`, … each with static factory helpers. |
| Submitting & waiting | `Algorand.Utils.Utils` | `SubmitTransaction`, `WaitTransactionToComplete`, `AlgosToMicroalgos`, atomic group helpers. |
| Error handling | `ApiException<ErrorResponse>` | Catch this to read the node's error details from `e.Result.Message`. |
| Indexer | `Algorand.Indexer` clients | Separate model from Algod by design — it keeps historical/deprecated fields so old data stays describable. |
| KMD | `Algorand.KMD.Api` | Wallet management via a node's KMD service; see the [KMD example](sdk-examples/KMDExample.cs). |

> **Technical note:** when specifying the host in `HttpClientConfigurator`, a trailing slash is automatically appended so relative URIs combine correctly. If you inject the `HttpClient` via DI yourself, make sure the base URL ends with a trailing slash (per [RFC 3986](https://datatracker.ietf.org/doc/html/rfc3986) and the `HttpClient` documentation).

## Examples

Complete, runnable examples live in the [`sdk-examples/`](sdk-examples/) project. Each is a standalone `Main` that can be selected via the dispatcher in [Program.cs](sdk-examples/Program.cs) (first CLI argument or the `EXAMPLE` environment variable):

```bash
dotnet run --project sdk-examples -- AssetExample
```

| Example | Demonstrates |
|---|---|
| [BasicExample.cs](sdk-examples/BasicExample.cs) | Connecting to a node, querying supply/balances, sending a payment |
| [AccountExample.cs](sdk-examples/AccountExample.cs) | Restoring an account from a mnemonic and reading account information |
| [AssetExample.cs](sdk-examples/AssetExample.cs) | Creating, configuring, opting in to, transferring, freezing, and revoking an Algorand Standard Asset (ASA) |
| [AtomicTransferExample.cs](sdk-examples/AtomicTransferExample.cs) | Grouping transactions so they atomically succeed or fail together |
| [MultisigExample.cs](sdk-examples/MultisigExample.cs) | Creating a multisig address and collecting signatures |
| [RekeyExample.cs](sdk-examples/RekeyExample.cs) | Rekeying an account to a different signing key |
| [OfflineSigningExample.cs](sdk-examples/OfflineSigningExample.cs) | Cold-wallet workflow: generating accounts, serializing unsigned transactions to msgpack, signing offline, submitting the signed bytes |
| [SimulateTransactionExample.cs](sdk-examples/SimulateTransactionExample.cs) | Dry-running a transaction group with the simulate endpoint before submitting it |
| [KeyRegistrationExample.cs](sdk-examples/KeyRegistrationExample.cs) | Registering an account online/offline for consensus participation (key registration transactions) |
| [NodeAndBlockExample.cs](sdk-examples/NodeAndBlockExample.cs) | Reading node status, waiting for rounds, and fetching blocks, block hashes, and transaction ids |
| [KMDExample.cs](sdk-examples/KMDExample.cs) | Using KMD to list wallets and retrieve LocalNet test accounts |
| [IndexerExample.cs](sdk-examples/IndexerExample.cs) | Querying historical data through the Indexer client |
| [CompileTeal.cs](sdk-examples/CompileTeal.cs) | Compiling TEAL source with the node |
| [LogicSignatureExample.cs](sdk-examples/LogicSignatureExample.cs) | Delegated logic signatures (smart signatures) |
| [LogicSignatureContractAccountExample.cs](sdk-examples/LogicSignatureContractAccountExample.cs) | Contract accounts controlled by a TEAL program |
| [StatefulContractExample.cs](sdk-examples/StatefulContractExample.cs) | Deploying and calling a stateful smart contract (application) |
| [SimpleBoxExample.cs](sdk-examples/SimpleBoxExample.cs) | Application box storage |
| [DryrunDebuggingExample.cs](sdk-examples/DryrunDebuggingExample.cs) | Debugging smart contracts with dryrun |
| [ConnectToGossipNetwork.cs](sdk-examples/ConnectToGossipNetwork.cs) | Connecting to the Algorand gossip (relay) network directly |

The test suites double as extensive usage references:

- [test/Arc56Tests.cs](test/Arc56Tests.cs) — generating and using typed ARC56 contract clients (deploy, method calls, structs, boxes)
- [test/Arc4Tests.cs](test/Arc4Tests.cs) — ARC4 ABI encoding and application calls
- [test/TransactionTests.cs](test/TransactionTests.cs) — building and signing all transaction types
- [SerialisationTests/](SerialisationTests/) — canonical msgpack encoding round-trips

## ARC56 smart-contract client generator

The SDK includes a generator that turns an [ARC56](https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0056.md) contract spec (JSON) into a fully typed C# client proxy — method calls, structs, state, and box accessors included.

### In code

```cs
using Algorand.AVM.ClientGenerator.ABI.ARC56;

var generator = new ClientGeneratorARC56();
generator.LoadFromByteArray(Encoding.UTF8.GetBytes(arc56Json));
var sourceCode = await generator.ToProxy("MyContractProxy");
File.WriteAllText("MyContractProxy.cs", sourceCode);
```

See [test/Arc56Tests.cs](test/Arc56Tests.cs) for end-to-end examples that generate a client, compile it, deploy the contract to LocalNet, and call its methods.

### Via Docker

The generator ships as the [`scholtz2/dotnet-avm-generated-client`](https://hub.docker.com/r/scholtz2/dotnet-avm-generated-client) image.

Generate a client from a URL:

```bash
docker run --rm -v "$(pwd):/app/out" scholtz2/dotnet-avm-generated-client:latest \
  dotnet client-generator.dll --namespace "MyNamespace" \
  --url https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecConfigProvider.arc56.json
```

Generate a client from the local filesystem:

```bash
docker run --rm -v "$(pwd)/contracts/artifacts:/app/out" -v "$(pwd)/contracts/artifacts:/app/artifacts" \
  scholtz2/dotnet-avm-generated-client:latest \
  dotnet client-generator.dll --namespace "MyNamespace" --file artifacts/BiatecConfigProvider.arc56.json
```

Options:

```
  -f, --file         Path to a local ARC56 JSON file.
  -u, --url          URL of an ARC56 JSON file.
  -n, --namespace    Namespace for the generated client.
  -o, --output       Output folder.
  --help             Display the help screen.
  --version          Display version information.
```

## Building and testing from source

```powershell
dotnet restore
dotnet build dotnet-algorand-sdk.sln --configuration Release --no-restore   # Unity build: --configuration Unity
dotnet test test/test.csproj                                                # main unit tests (needs AlgoKit LocalNet for ARC4/ARC56 fixtures)
dotnet test SerialisationTests/serialisation-tests.csproj                   # msgpack/canonical-encoding tests
```

Notes:

- `test/Arc4Tests.cs` and `test/Arc56Tests.cs` deploy contracts against a running AlgoKit LocalNet (`algokit localnet start`).
- Most of `Algod/Model/` and `DefaultApi.cs` are **generated** from Algorand's official OpenAPI spec — edit the hand-written partial-class files (`Foo.cs`), never the `*.generated.cs` files. See [CLAUDE.md](CLAUDE.md) for the full architecture overview.

## Versioning

NuGet package versions (4.x and onward) track the version of the algod build the SDK targets.

**Release note:** in v4 the KMD client was completely reworked (breaking change), and full integration with the Algorand generator introduced minor capitalisation changes in some fields. The overall shape of the SDK is now stable.

## License

Distributed under the [GNU General Public License v3.0](LICENSE).

The package icon (`icon.png`) is derived from the official Algorand logomark, geometry sourced from [Algorand_mark.svg](https://commons.wikimedia.org/wiki/File:Algorand_mark.svg) on Wikimedia Commons, licensed under [CC BY-SA 4.0](https://creativecommons.org/licenses/by-sa/4.0/).
