#!/bin/bash
echo "Remover Migration"

dotnet ef migrations remove --context DataContext --project src/infra/Mastership.Database/Mastership.Database.csproj --startup-project src/presentation/Mastership.Api/Mastership.Api.csproj --verbose
