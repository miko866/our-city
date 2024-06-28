#!/usr/bin/env bash
export ASPNETCORE_ENVIRONMENT=LocalDevelopment
dotnet ef migrations add "$1" --project Data --startup-project Server --context ApplicationDBContext --verbose
