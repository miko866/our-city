#!/usr/bin/env bash
echo "Create migration SQL Script into file: ./sql/sql_migrations/dblayout"
echo "Use like this to get a diff from one Migration to another: ./ef_generate_sqlscript.sh addLocationAndProductFields addIsOwnFlagToProductAndBusinessServiceType"
export ASPNETCORE_ENVIRONMENT=LocalDevelopment
dotnet ef migrations script "$1" "$2" --output ./sql/migrations/db_layout/$(date +"%Y-%m-%d")_sql_from_migrations.sql --verbose --project Data --startup-project Server --context ApplicationDBContext 
