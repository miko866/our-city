#!/usr/bin/env bash

echo "===========Executes all dotnet tests.============="
export ASPNETCORE_ENVIRONMENT=LocalIntegrationTest
#dotnet test ./TestServer/ -v=normal
dotnet test ./TestServer/ 
