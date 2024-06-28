# *****************************************************************************************************************
# OUR-CITY pipeline file
# 
# Generates the appsettings.json & nlog.config based on the branch (=environment) and then builds the code. 
#
# *****************************************************************************************************************
echo "Starting run build"

# include the file with the vars. use full path! currently nothing from this file is needed, but we include it anyway
source ./k8s/bash/00_global_variables.sh

BRANCH_NAME_TEMP=$CI_COMMIT_REF_SLUG 
if [ -z ${BRANCH_NAME_TEMP+x} ]; then
  exit_error "UNKNOWN BRANCH NAME"
fi

# what you need to know: the variables ($DOTENV_*) are set in the Gitlab Project settings -> CI. 
# If the variable ist protected it can only be accessed from a protected branch! the value will be '' if accessed from another branch
APPSETTINGS_PATH_TARGET="./Server/appsettings.json"
NLOG_CONFIG_PATH_TARGET="./Server/nlog.config"

# defaults to development
DEBUGMSG="branch=main, env=development"  
MY_APPSETTINGS=$APPSETTINGS_DEVELOPMENT
MY_NLOG_CONFIG=$NLOG_CONFIG_DEVELOPMENT

# For Tags / Current Testing Environment
if [[ $BRANCH_NAME_TEMP =~ [0-9] ]]; then
    DEBUGMSG="branch=$BRANCH_NAME_TEMP, env=testing" 
    MY_APPSETTINGS=$APPSETTINGS_TESTING
    MY_NLOG_CONFIG=$NLOG_CONFIG_TESTING
fi

echo "---------------- DEBUG INFO -------------------"
echo "DEBUGMSG (BASED ON BRANCH): $DEBUGMSG"
echo "-----------------------------------------------"

# what you need to know: the variables ($APPSETTINGS_*) are set in the Gitlab Project settings -> CI. 
# If the variable ist protected it can only be accessed from a protected branch! the value will be '' if accessed from another branch
if [ -z ${MY_APPSETTINGS+x} ]; then
  exit_error "ABORTING, NO ENV FOUND! Are you trying to access an protected variable from an unprotected branch?"
fi

# write File 
cat "$MY_APPSETTINGS" > $APPSETTINGS_PATH_TARGET || exit_error "ERROR WRITING APPSETTINGS"
cat "$MY_NLOG_CONFIG" > $NLOG_CONFIG_PATH_TARGET || exit_error "ERROR WRITING NLOG CONFIG"

# delete unnecessary files
find "./Server" -name appsettings.*.json -type f -delete
find "./Server" -name nlog.*.config -type f -delete

echo "Building deploy package"

CI=false dotnet publish -c Release -r linux-x64 --property:PublishDir=../build OurCity.backend.sln || exit_error "BUILDING APP WENT WRONG! PLEASE CHECK"

exit_success "Built app successfully!"

