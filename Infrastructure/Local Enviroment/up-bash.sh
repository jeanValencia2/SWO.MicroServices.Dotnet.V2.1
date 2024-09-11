#!/bin/bash

docker-compose up -d

sleep 15 # sleep 15 seconds to give time to docker to finish the setup
echo setup vault configuration
./vault/config.sh
echo setup consul configuration
./consul/config.sh
echo completed