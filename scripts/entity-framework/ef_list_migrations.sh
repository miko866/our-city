#!/usr/bin/env bash
export ASPNETCORE_ENVIRONMENT=LocalDevelopment
dotnet ef migrations list --project Data --startup-project Server --context ApplicationDBContext --verbose
