#!/bin/bash
echo "Adicionar Migration"

dotnet ef migrations add $1 --context DataContext --project '../5-Infra/5.1-Data/Mastership.Infra.Data/Mastership.Infra.Data.csproj' --startup-project '../2-Service/Mastership.Services.Api/Mastership.Services.Api.csproj' --verbose
