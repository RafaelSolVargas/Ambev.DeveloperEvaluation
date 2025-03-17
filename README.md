# Ambev Developer Evaluation

Bem-vindo ao repositório da aplicação **Ambev Developer Evaluation**! Este projeto é uma aplicação web API desenvolvida em .NET que gerencia vendas, produtos, filiais e usuários. Ele utiliza Docker para facilitar a execução e integração de todos os componentes necessários, como banco de dados PostgreSQL, MongoDB, Redis, RabbitMQ e a própria API.

Os eventos solicitados foram implementados utilizando a biblioteca Rebus junto com RabbitMQ:
* SaleCreated
* SaleModified
* SaleCancelled


Abaixo, você encontrará instruções detalhadas sobre como configurar e executar a aplicação.

## Pré-requisitos

Antes de começar, certifique-se de ter os seguintes softwares instalados em sua máquina:

- **Docker**: [Instale o Docker](https://docs.docker.com/get-docker/)
- **Docker Compose**: [Instale o Docker Compose](https://docs.docker.com/compose/install/)
- **.NET SDK** (opcional, apenas se quiser rodar migrações ou executar a aplicação fora do Docker): [Instale o .NET SDK](https://dotnet.microsoft.com/download)

## Configuração do Projeto

### 1. Clone o Repositório

Primeiro, clone o repositório para o seu ambiente local:

```bash
git clone https://github.com/RafaelSolVargas/ambev-developer-evaluation.git
cd ambev-developer-evaluation
```
<hr>


### 2. Levante os Contêineres com Docker Compose

```bash
docker-compose up -d
```

O arquivo docker-compose.yml se encontra no root do projeto

Isso levantará os seguintes serviços:

- ambev.developerevaluation.webapi: Aplicação Web API.
- ambev.developerevaluation.database: Banco de dados PostgreSQL.
- ambev.developerevaluation.nosql: Banco de dados MongoDB.
- ambev.developerevaluation.cache: Servidor Redis para cache.
- ambev.developerevaluation.rabbitmq: Servidor RabbitMQ para mensageria.

<hr>

### 3. Aplicar Migrações no Banco de Dados

Após levantar os contêineres, você precisa aplicar as migrações para configurar e popular o banco de dados PostgreSQL. Navegue até o diretório do ORM e execute o seguinte comando:

```bash
cd backend\src\Ambev.DeveloperEvaluation.ORM
dotnet ef database update --startup-project ..\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj
```

Isso criará as tabelas e adicionará os dados iniciais ao banco de dados.

Caso você queria resetar os dados fazendo a migration novamente, você pode fazer drop do banco de dados com o seguinte comando:
```bash
cd backend\src\Ambev.DeveloperEvaluation.ORM
dotnet ef database drop --force --startup-project ..\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj
```
E depois executar o database update novamente

## Testando o projeto

Você pode utilizar os comandos dotnet test para executar os testes que foram desenvolvidos.

## Dados Iniciais

Para acelerar o processo de verificações do projeto, alguns dados são inseridos automaticamente durante o processo de migration.

Abaixo, estão listados os IDs deles:

#### *Produtos*:
- 2555343d-facf-4c33-b8a6-5fad76be36a3
- 0f038ef4-2b67-4b7b-9454-801ab6581f54
- ccc9047b-23d4-4260-b29e-2b9c0229bf86
- 4e662887-962a-46d8-9aed-aa6c0efb7e76
- 184c3336-c66e-42e5-8738-11cbe96f527a
- 5d0b27be-eaeb-4483-8960-489e65f2452d
- d7dae433-b37b-41c4-8e86-330fe3259c03
- 955f7c88-fe46-483b-9411-19ee00271974
- 1ce1afda-b45d-41ea-a2f4-3c4554154aa6
- eb938eb0-572a-4ae2-953a-2bd55e1709bc

#### *Usuários*:
- ffb140dc-53be-48d5-8c6e-5d3f93271bff
- bb6f91f2-1720-4191-9022-24254953fa18
- 0af35800-836a-418a-8a44-f9d21b832fa2

#### *Filiais*:
- e49c076d-3046-4399-8d41-94cc9bb65dc0
- c34d2387-e900-4704-9fe4-09bfaa50f0e1
- f2495571-1390-411a-9bfd-669185afea64

#### *Vendas (Sales)*:
- 6fc34cef-afed-4edb-ab5f-44a1e2eea0a9
- 62bad870-cf24-4dee-8470-988fb0ee3361
- e8765c98-ce78-4090-ae9a-903101d023c1
- a37f6397-3a09-435f-829d-a19c19728a9b
- acfeb46e-4383-48dc-af5a-9d1d6444504

#### *Produtos de Venda (SaleProduct)*:
- 019e40fb-fd1a-4edb-a061-b036060c1a64
- 4b263c2b-d9ad-4149-9e41-0ca45977f7f2
- 60e1e8fd-8346-4210-a01e-8a1ee795435b
- 2edf25e5-a58d-4e3b-b93c-3d1957433b31
- 4dfbec1a-adbb-4148-ab8d-4b23d692f549
- bc70cb2d-e0cd-4b3d-a610-1c101f846926
- 26fe8552-e4ca-4036-8470-9abdd06f48b6
- 4a93decd-8ce8-4a58-bc82-d159769f9965
- 1063acfd-e15c-4f6f-8825-ae7f9036e188
