#!/usr/bin/env bash

cat << "EOF"

  ______    __    __  .______              ______  __  .___________.____    ____ 
 /  __  \  |  |  |  | |   _  \            /      ||  | |           |\   \  /   / 
|  |  |  | |  |  |  | |  |_)  |    ______|  ,----'|  | `---|  |----` \   \/   /  
|  |  |  | |  |  |  | |      /    |______|  |     |  |     |  |       \_    _/   
|  `--'  | |  `--'  | |  |\  \----.      |  `----.|  |     |  |         |  |     
 \______/   \______/  | _| `._____|       \______||__|     |__|         |__|     

EOF

echo "Start build net app"
dotnet publish -c Release -r linux-x64 --property:PublishDir=../build OurCity.backend.sln
echo "Build net app is done"

echo "Start copy files to server"
scp -r /Users/miko866/Desktop/Unicorn/bakalarska_praca/project/OurCity.backend/build dev_deployer@176.56.237.18:/home/dev_deployer/deployment

scp -r /Users/miko866/Desktop/Unicorn/bakalarska_praca/project/OurCity.backend/sql -i .ssh/our_city_ssh dev_deployer@176.56.237.18:/home/dev_deployer/deployment

scp -r /Users/miko866/Desktop/Unicorn/bakalarska_praca/project/OurCity.backend/storage -i .ssh/our_city_ssh dev_deployer@176.56.237.18:/home/dev_deployer/deployment
echo "Copy files to server is done"

