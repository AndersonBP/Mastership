#!/bin/bash
echo "Update database"

dotnet ef migrations script --context DataContext --project '../5-Infra/5.1-Data/Mastership.Infra.Data/Mastership.Infra.Data.csproj' --startup-project '../2-Service/Mastership.Services.Api/Mastership.Services.Api.csproj' --verbose -o script.sql
