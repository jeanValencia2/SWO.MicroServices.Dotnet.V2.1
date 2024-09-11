#!/bin/bash

## this is the code for the first part of the tutorial. 
## example code for get KeyValue secrets. 
docker exec -it vault vault kv put secret/rabbitmq username=DistriSystemAdmin password=7yW59G6EokFU

## this is the code for the rabbitmq integration with vault
# docker exec -it vault vault secrets enable rabbitmq

# docker exec -it vault vault write rabbitmq/config/connection \
#     connection_uri="http://rabbitmq:15672" \
#     username="DistriSystemAdmin" \
#     password="7yW59G6EokFU" 

# docker exec -it vault vault write rabbitmq/roles/distribt-role \
#     vhosts='{"/":{"write": ".*", "read": ".*"}}'

    
## User&Pass for mongoDb
docker exec -it vault vault kv put secret/mongodb username=DistriSystemAdmin password=7yW59G6EokFU

##User&Pass for MySql
docker exec -it vault vault kv put secret/mysql username=DistriSystemAdmin password=7yW59G6EokFU

##User&Pass for SQLServer
docker exec -it vault vault kv put secret/sqlserver username=DistriSystemAdmin password=7yW59G6EokFU

##Token secrets
docker exec -it vault vault kv put secret/tokensecrets Key=3FC9F951259391B84C7CA73F449C43FC Lifetime=3600 RefreshTokenExpiryTime=4000 Audience=MicroservicesClient Issuer=MicroservicesIdentity
