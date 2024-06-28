#!/bin/bash

exit_error()
{
    echo "-------> Error: $1"
    exit 1
}

exit_success()
{

    echo "All done! Message: $1"
    echo "Have a nice day"
    exit 0
}
echo "--------------- START BUILD SCRIPT -----------"

BRANCH_NAME_TEMP=$CI_COMMIT_REF_SLUG 
if [ -z ${BRANCH_NAME_TEMP+x} ]; then
  exit_error "UNKNOWN BRANCH NAME"
fi

APPSETTINGS_PATH_TARGET="./Server/appsettings.json"

# Defaults to development
DEBUGMSG="env=development"  
MY_APPSETTINGS=$APPSETTINGS_DEVELOPMENT

if [ "$BRANCH_NAME_TEMP" == "testing" ]; then
  DEBUGMSG="env=testing" 
  MY_APPSETTINGS=$APPSETTINGS_TESTING
fi

if [ "$BRANCH_NAME_TEMP" == "staging" ]; then
  DEBUGMSG="env=staging" 
  MY_APPSETTINGS=$APPSETTINGS_STAGING
fi

if [ "$BRANCH_NAME_TEMP" == "stable" ]; then
  DEBUGMSG="branch=stable"
  MY_APPSETTINGS=$APPSETTINGS_STABLE
fi

echo "---------------- DEBUG INFO -------------------"
echo "DEBUGMSG (BASED ON BRANCH): $DEBUGMSG"
echo "-----------------------------------------------"

if [ -z ${MY_APPSETTINGS+x} ]; then
  exit_error "ABORTING, NO ENV FOUND! Are you trying to access an protected variable from an unprotected branch?"
fi

cat "$MY_APPSETTINGS" > $APPSETTINGS_PATH_TARGET || exit_error "ERROR WRITING APPSETTINGS"

# Delete unnecessary files
find "./Server" -name appsettings.*.json -type f -delete

echo "Building deploy package"

CI=false dotnet publish -c Release -r linux-x64 --property:PublishDir=../build OurCity.backend.sln || exit_error "BUILDING APP WENT WRONG! PLEASE CHECK"

exit_success "Built app successfully!"