# Ambev Developer Evaluation

Bem-vindo ao repositório da aplicação **Ambev Developer Evaluation**!
Este projeto é uma aplicação web API desenvolvida em .NET que gerencia vendas, produtos, filiais e usuários. 
Ele utiliza Docker para facilitar a execução e integração de todos os componentes necessários, 
como banco de dados PostgreSQL, RabbitMQ e a própria API.

---

## Visão Geral

Este projeto foi desenvolvido para avaliar habilidades de desenvolvimento, utilizando tecnologias modernas e boas práticas de engenharia de software. A aplicação é composta por uma API RESTful que gerencia operações de vendas, produtos, filiais e usuários, com suporte a mensageria via RabbitMQ para eventos como criação, modificação e cancelamento de vendas.

### Tecnologias Utilizadas

- **.NET**: Framework principal para desenvolvimento da API.
- **Docker**: Contêinerização da aplicação e seus serviços.
- **PostgreSQL**: Banco de dados relacional para armazenamento de dados.
- **RabbitMQ**: Sistema de mensageria para eventos assíncronos.
- **Rebus**: Biblioteca para integração com RabbitMQ.

---

## 🔧 Requisitos

Antes de começar, certifique-se de ter os seguintes softwares instalados em sua máquina:

- **Docker**: [Instale o Docker](https://docs.docker.com/get-docker/)
- **Docker Compose**: [Instale o Docker Compose](https://docs.docker.com/compose/install/)
- **.NET SDK** (opcional, apenas se quiser rodar migrações ou executar a aplicação fora do Docker): [Instale o .NET SDK](https://dotnet.microsoft.com/download)

---

## 🛠️ Configuração do Projeto

### 1. Clonar o Repositório
```
git clone https://github.com/RafaelSolVargas/ambev-developer-evaluation.git
cd ambev-developer-evaluation
```

### 2. Iniciar os Contêineres com Docker Compose

O arquivo **docker-compose.yml** está na raiz do projeto. Para iniciar os serviços, execute:

```  
docker-compose up -d 
```

Isso levantará os seguintes serviços:

- **ambev.developerevaluation.webapi**: Aplicação Web API.
- **ambev.developerevaluation.database**: Banco de dados PostgreSQL.
- **ambev.developerevaluation.rabbitmq**: Servidor RabbitMQ para mensageria.

### 3. Analise as aplicações levantadas

Verifique se API foi levantada corretamente pelos endpoints:
- **https://localhost:7182/**
- **http://localhost:7181/**

Também verifique se o servidor da mensageira está funcionando:
- **http://localhost:15672/**
    - Username: **user**
    - Password: **password**

Teste sua conexão ao banco de dados:
- **Host**: **localhost**
- **Database**: **developer_evaluation**
- **Port**: **5432**
- **User**: **developer**
- **Password**: **ev@luAt10n**


## 💰 Aplicar Migrações no Banco de Dados

Para criar as tabelas e inserir dados iniciais:
```
cd src\Ambev.DeveloperEvaluation.ORM
dotnet ef database update --startup-project ..\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj
```

Caso queira resetar o banco de dados ou reiniciar o processo de migração:
```
cd src/Ambev.DeveloperEvaluation.ORM
dotnet ef database drop --force --startup-project ../Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
dotnet ef database update --startup-project ../Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
```
E depois executar o database update novamente

## 📝 Analisando o projeto

### 1. Executar testes


``` 
dotnet test
```
### 2. Descrição de endpoints

| Método | Endpoint                                 | Descrição                                        |
|--------|------------------------------------------|--------------------------------------------------|
| POST   | /api/Auth                                | Faça autenticação                                |
| POST   | /api/Branch                              | Crie uma filial                                 |
| POST   | /api/Products                            | Crie um produto                                 |
| PUT    | /api/Products/{id}                       | Edite um produto                                |
| GET    | /api/Products/{id}                       | Busque dados de um produto                      |
| DELETE | /api/Products/{id}                       | Delete um produto                               |
| POST   | /api/Sales                               | Crie uma venda (Envie ID de usuário, filial e lista de produtos) |
| GET    | /api/Sales                               | Busque dados de todas as vendas, com paginação, filtros e ordenação |
| GET    | /api/Sales/{id}                          | Busque uma venda pelo ID                        |
| DELETE | /api/Sales/{id}                          | Delete uma venda                                |
| PUT    | /api/Sales/{id}                          | Edite uma venda                                 |
| GET    | /api/Sales/branch/{branchId}             | Busque todas as vendas de uma filial            |
| GET    | /api/Sales/customer/{customerId}         | Busque todas as vendas de um cliente            |
| PATCH  | /api/Sales/ChangeStatus/{id}/{status}     | Edite o status de uma venda, cancele ou desfaça o cancelamento |


---

## 🔢 Testando o projeto

### 1. Utilizando os endpoints


Para testar a API, você pode utilizar a UI do Swagger que foi disponibilizada mesmo em Realease pelas URLs:
- https://localhost:7182/swagger/index.html
- http://localhost:7181/swagger/index.html

Inicie sua analise fazendo uma requisição de Login com os usuários padrões:
- POST **http://localhost:7181/api/Auth**
``` json
{
    "email": carlos.oliveira@example.com
    "password": "Senha1234!"
}
```


Verifique as Sales já cadastradas:
> GET **http://localhost:7181/api/Sales**


Crie sales para testes, essa requisição já está com IDs fixos da aplicação, mais abaixo estarão disponíveis outros IDs também fixos.
``` json
{
  "number": "NUMBERX1",
  "dateSold": "2025-03-18T04:26:46.010Z",
  "customerId": "ffb140dc-53be-48d5-8c6e-5d3f93271bff",
  "branchId": "f2495571-1390-411a-9bfd-669185afea64",
  "products": [
    {
      "productId": "2555343d-facf-4c33-b8a6-5fad76be36a3",
      "quantity": 5,
      "unitPrice": 10
    },
    {
      "productId": "0f038ef4-2b67-4b7b-9454-801ab6581f54",
      "quantity": 15,
      "unitPrice": 18.5
    }
  ]
}
```




### 1. Dados Iniciais

Para facilitar os testes, alguns dados são inseridos automaticamente durante a mgiração. Aqui estão os IDs principais:

#### 🍒 *Produtos*:
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

#### 👨️🎓 *Usuários*:
- ffb140dc-53be-48d5-8c6e-5d3f93271bff
- bb6f91f2-1720-4191-9022-24254953fa18
- 0af35800-836a-418a-8a44-f9d21b832fa2

#### 🏢  *Filiais*:
- e49c076d-3046-4399-8d41-94cc9bb65dc0
- c34d2387-e900-4704-9fe4-09bfaa50f0e1
- f2495571-1390-411a-9bfd-669185afea64

#### 🛒 *Vendas (Sales)*:
- 6fc34cef-afed-4edb-ab5f-44a1e2eea0a9
- 62bad870-cf24-4dee-8470-988fb0ee3361
- e8765c98-ce78-4090-ae9a-903101d023c1
- a37f6397-3a09-435f-829d-a19c19728a9b
- acfeb46e-4383-48dc-af5a-9d1d6444504

#### 🛒🍒 *Produtos de Venda (SaleProduct)*:
- 019e40fb-fd1a-4edb-a061-b036060c1a64
- 4b263c2b-d9ad-4149-9e41-0ca45977f7f2
- 60e1e8fd-8346-4210-a01e-8a1ee795435b
- 2edf25e5-a58d-4e3b-b93c-3d1957433b31
- 4dfbec1a-adbb-4148-ab8d-4b23d692f549
- bc70cb2d-e0cd-4b3d-a610-1c101f846926
- 26fe8552-e4ca-4036-8470-9abdd06f48b6
- 4a93decd-8ce8-4a58-bc82-d159769f9965
- 1063acfd-e15c-4f6f-8825-ae7f9036e188
