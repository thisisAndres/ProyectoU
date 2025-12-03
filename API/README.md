# Requisitos para a API
1 - Descargar Docker

2- Tener .NET 8.0 o superior instalado

3- Tener en la ruta API/ que se llame .env

4- Tener el archivo en API/ llamado docker-compose.yml

# Paso a paso de comandos para levantar la API
1- docker compose config -> valida que el docker-compose.yml y el .env esten bien escritos

2- docker compose up -d -> levanta los contenedores en segundo plano

3- psql -h 127.0.0.1 -p 5433 -U postgres -d university -W -> conecta a la base de datos (la contraseña es la que se puso en el .env)

4- dotnet ef database update -> aplica las migraciones a la base de datos

5- dotnet run -> levanta la API