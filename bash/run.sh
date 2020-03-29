#!/bin/bash
echo "Run"

dotnet build ../Mastership.sln

dotnet run --project '../2-Service/Mastership.Services.Api/Mastership.Services.Api.csproj'
