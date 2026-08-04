---
description: Restore and build the solution in Release configuration
---

Run, in order, stopping to report the first failure:

```powershell
dotnet restore
dotnet build dotnet-algorand-sdk.sln --configuration Release --no-restore
```

Summarize any warnings/errors with file:line references. Do not attempt fixes unless asked.
