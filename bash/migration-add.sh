#!/bin/bash
echo "Adicionar Migration"

dotnet ef migrations add $1 --context DataContext --project src/infra/Rv.Database/Rv.Database.csproj --startup-project src/presentation/Rv.Api/Rv.Api.csproj --verbose
