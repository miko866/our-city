#!/usr/bin/env bash

echo "Building deploy package"

if [[ "$OSTYPE" =~ ^darwin ]]; then
    dotnet publish -c Release -r linux-arm64 --property:PublishDir=../build OurCity.backend.sln
fi

if [[ "$OSTYPE" =~ ^linux ]]; then
    dotnet publish -c Release -r linux-x64 --property:PublishDir=../build OurCity.backend.sln
fi

echo "Building docker image"

#echo "Running docker compose"
#docker compose up

