#!/usr/bin/env bash
export ASPNETCORE_ENVIRONMENT=LocalDevelopment
dotnet ef dbcontext info --project Data --startup-project Server --context ApplicationDBContext --verbose
