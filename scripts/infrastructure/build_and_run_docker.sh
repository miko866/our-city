#!/usr/bin/env bash

cat << "EOF"

  ______    __    __  .______              ______  __  .___________.____    ____ 
 /  __  \  |  |  |  | |   _  \            /      ||  | |           |\   \  /   / 
|  |  |  | |  |  |  | |  |_)  |    ______|  ,----'|  | `---|  |----` \   \/   /  
|  |  |  | |  |  |  | |      /    |______|  |     |  |     |  |       \_    _/   
|  `--'  | |  `--'  | |  |\  \----.      |  `----.|  |     |  |         |  |     
 \______/   \______/  | _| `._____|       \______||__|     |__|         |__|     

EOF

echo "Start docker build"
docker build . -t ourcity
echo "Docker build is done"

echo "Run docker compose"
docker compose up -d
echo "Docker compose is running"