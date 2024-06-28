# ********************************* GLOBAL CODE ***************************************************
# add timezone packages, as our gitlab runners do not have them 
# https://unix.stackexchange.com/questions/206540/date-d-command-fails-on-docker-alpine-linux-container
apk add --update coreutils && rm -rf /var/cache/apk/*
apk add tzdata

#we set the timezone
export TZ=Europe/Zurich
# date +"%Z %z" #debug

echo "global config done"

# ********************************* FUNCTIONS ***************************************************
exit_error()
{
    echo "-------> Error: $1"
    exit 1
}

exit_success()
{

    echo "all done! Message: $1"
    echo "have a nice day"
    exit 0
}
