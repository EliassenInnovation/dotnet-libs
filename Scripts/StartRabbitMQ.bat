

@ECHO OFF

docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:latest
docker start rabbitmq 