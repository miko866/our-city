#!/usr/bin/env bash
export ASPNETCORE_ENVIRONMENT=LocalDevelopment
dotnet ef migrations remove --project Data --startup-project Server --context ApplicationDBContext --verbose