#!/bin/bash
echo "Update database"

dotnet ef database update --context DataContext --project src/infra/Mastership.Database/Mastership.Database.csproj --startup-project src/presentation/Mastership.Api/Mastership.Api.csproj --verbose $1
