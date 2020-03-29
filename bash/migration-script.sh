#!/bin/bash
echo "Update database"

dotnet ef migrations script --context DataContext --project src/infra/Rv.Database/Rv.Database.csproj --startup-project src/presentation/Rv.Api/Rv.Api.csproj --verbose -o script.sql
