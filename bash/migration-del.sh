#!/bin/bash
echo "Remover Migration"

dotnet ef migrations remove --context DataContext --project src/infra/Rv.Database/Rv.Database.csproj --startup-project src/presentation/Rv.Api/Rv.Api.csproj --verbose
