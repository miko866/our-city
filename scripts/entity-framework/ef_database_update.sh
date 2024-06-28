#!/usr/bin/env bash
echo "Executes migrations on the db."
echo "Usage: /ef_database_update.sh <MigrationName>  (use '0' as param to empty DB)"
export ASPNETCORE_ENVIRONMENT=LocalDevelopment
dotnet ef database update "$1" --project Data --startup-project Server --context ApplicationDBContext --verbose
