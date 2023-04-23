<p align="center">
    <img alt="logo" src="https://user-images.githubusercontent.com/5068797/161198565-ac18c5ac-c0d9-4669-9568-b2009e944d77.png#gh-light-mode-only" />
    <img alt="logo" src="https://user-images.githubusercontent.com/5068797/161364257-0c1d81f6-62ac-4192-93f8-836b4ce0fd06.png#gh-dark-mode-only" />
</p>

# DevStore - A microservices e-commerce reference application built with ASP.NET 6

This is application was created for studying purposes and was based in diverse courses from Dev IO website.
A real-world reference application powered by [desenvolvedor.io](https://desenvolvedor.io/) <img alt="Brasil" src="https://user-images.githubusercontent.com/5068797/161345649-c7184fdc-2bc3-42a9-8fb6-6ffee9c8f9c2.png" width="20" height="14" /> implementing the most common and used technologies to share with the technical community the best way to develop full and complex applications with .NET

---

<p align="center">
    <img alt="DevStore" src="https://user-images.githubusercontent.com/5068797/164293734-a72fbeeb-0965-4413-a624-29e1c56c25df.png" />
</p>

## Want to learn everything to build an app like this?  :mortar_board:
Check this online courses at [desenvolvedor.io](https://desenvolvedor.io) (only in portuguese)

- [ASP.NET Core Expert](https://desenvolvedor.io/formacao/asp-net-core-expert)
- [Software Architect](https://desenvolvedor.io/formacao/arquiteto-de-software)

## Technologies / Components implemented

- .NET 6
    - ASP.NET MVC Core
    - ASP.NET WebApi
    - ASP.NET Minimal API
    - ASP.NET Identity Core
    - Refresh Token
    - JWT with rotactive public / private key    
    - GRPC
    - Background Services
    - Entity Framework Core 6

- Components / Services
    - RabbitMQ
    - Kafka
    - EasyNetQ
    - Refit 
    - Polly
    - Bogus
    - Dapper
    - FluentValidator
    - MediatR
    - Swagger UI with JWT support
    - NetDevPack
    - NetDevPack.Identity
    - NetDevPack.Security.JWT

- Hosting
    - IIS
    - NGINX
    - Docker (with compose)

## Architecture:

### Complete architecture implementing the most important and used concerns as:

- Hexagonal Architecture
- Clean Code
- Clean Architecture
- DDD - Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Domain Validations
- CQRS (Imediate Consistency)
- Retry Pattern
- Circuit Breaker
- Unit of Work
- Repository
- Specification Pattern
- API Gateway / BFF

---

## Architecture Overview

### The entire application is based in an unique solution with 7 API's and one web application (MVC)
<p align="center">
    <img alt="read before" src="https://user-images.githubusercontent.com/5068797/161202409-edcf2f38-0714-4de5-927d-1a02be4501ec.png" />
</p>

---

This is a reference application, each microservice has its own database and represents a bounded context (DDD concept).
There is a BFF / API Gateway to manage the Basket / Order / Payment requests and data structure from responses.

<p align="center">
    <img alt="read before" src="https://user-images.githubusercontent.com/5068797/161207732-e4f67ce4-624d-4067-a756-67ee1bb553de.png" />
</p>

---

## Getting Started
You can run the DevStore project on any operating system. **Make sure you have installed docker in your environment.** ([Get Docker Installation](https://docs.docker.com/get-docker/))

Clone DevStore repository and navigate to the **/Docker** folder and then:

### If you just want run the DevStore application in your Docker enviroment:

```
docker-compose -f nerdstore_production.yml up
```

### If you want to build the local images and run the DevStore application in your Docker enviroment:

This compose will provide one database container each API service.

```
docker-compose -f nerdstore_production.yml up --build
```
---

### If you want run locally with VS/VS Code:

You will need:

- Docker
- SQL instance (or container)
- Kafka

So you can edit the Docker compose to just run the database and queue dependencies and save your time.

### If you want Visual Studio with F5 and debug experience:

- You will need at least Visual Studio 2022 and .NET 6.
- The latest SDK and tools can be downloaded from https://dot.net/core
- Setup the solution to start multiple projects and hit F5
 
![image](https://user-images.githubusercontent.com/5068797/161358024-bd5754b6-61e3-47f2-bd17-bd3c32ec4bdd.png)

---

## About
DevStore was proudly developed by [desenvolvedor.io](https://desenvolvedor.io/)❤<img alt="Brasil" src="https://user-images.githubusercontent.com/5068797/161345649-c7184fdc-2bc3-42a9-8fb6-6ffee9c8f9c2.png" width="20" height="14" /> team under the [MIT license](LICENSE).
