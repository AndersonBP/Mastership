#!/bin/bash
echo "Update database"

dotnet ef migrations script --context DataContext --project src/infra/Mastership.Database/Mastership.Database.csproj --startup-project src/presentation/Mastership.Api/Mastership.Api.csproj --verbose -o script.sql
