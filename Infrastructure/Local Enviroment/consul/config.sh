#!/bin/bash
# Infrastructure
docker exec -it consul consul services register -name=RabbitMQ -address=127.0.0.1
docker exec -it consul consul services register -name=SecretManager -address=http://127.0.0.1 -port=8200
docker exec -it consul consul services register -name=MySql -address=127.0.0.1 -port=3306
docker exec -it consul consul services register -name=MongoDb -address=localhost -port=27017
docker exec -it consul consul services register -name=SQLServer -address=distrisystemserver.database.windows.net -port=1433
docker exec -it consul consul services register -name=Graylog --address=127.0.0.1 -port=12201
docker exec -it consul consul services register -name=OpenTelemetryCollector --address=127.0.0.1 -port=4317

# Services
docker exec -it consul consul services register -name=SecurityApi -address=https://127.0.0.1 -port=44391
docker exec -it consul consul services register -name=CustomerApi -address=https://127.0.0.1 -port=44326
docker exec -it consul consul services register -name=ProductsApi -address=https://127.0.0.1 -port=44336
docker exec -it consul consul services register -name=CartsApi -address=https://127.0.0.1 -port=44357