#!/usr/bin/env bash

echo "###################################################"
echo "Setup Permissions for entity framework core scripts"
echo "###################################################"
echo " "

chmod u+x ./scripts/entity-framework/ef_add_migration.sh 
chmod u+x ./scripts/entity-framework/ef_database_update.sh 
chmod u+x ./scripts/entity-framework/ef_dbcontext_info.sh 
chmod u+x ./scripts/entity-framework/ef_generate_sqlscript.sh  
chmod u+x ./scripts/entity-framework/ef_list_migrations.sh 
chmod u+x ./scripts/entity-framework/ef_remove_migration.sh  

echo "##########################"
echo "DONE"
echo "##########################"