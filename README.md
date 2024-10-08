# FIAP.TechChalenge.InvestNetHub

# Invest Net Hub

Invest Net Hub é um sistema de gestão de investimentos desenvolvido em .NET, seguindo os princípios de Clean Architecture, DDD (Domain-Driven Design) e boas práticas de desenvolvimento. O sistema permite que usuários gerenciem portfólios de investimentos, ativos e transações financeiras, fornecendo uma base robusta para expandir com funcionalidades futuras.

## Sumário

- [Funcionalidades Principais](#funcionalidades-principais)
- [Arquitetura](#arquitetura)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Como Rodar Testes](#como-rodar-testes)
- [Como Adicionar e Rodar Migrations](#como-adicionar-e-rodar-migrations)

## Funcionalidades Principais

- **Cadastro de Usuário**: Registro e autenticação de usuários.
- **Gerenciamento de Portfólios**: Criação, atualização, visualização e exclusão de portfólios de investimentos.
- **Gerenciamento de Ativos**: Adição e remoção de ativos aos portfólios.
- **Gerenciamento de Transações**: Registro de transações de compra e venda de ativos.

## Arquitetura

O projeto segue uma arquitetura em camadas que divide as responsabilidades de forma clara:

```mermaid
graph TD
    subgraph Clients
        A1[Web App]
        A2[Mobile App]
        A3[API Client]
        A4[Insomnia]
    end

    subgraph API Layer
        B1[Invest Net Hub API]
    end

    subgraph Application Layer
        C1[Use Cases]
        C2[MediatR]
        C3[Validation]
    end

    subgraph Domain Layer
        D1[Entities]
        D2[Domain Services]
        D3[Repositories Interfaces]
    end

    subgraph Infrastructure Layer
        E1[EF Core Repositories]
        E2[Logging]
        E3[External Services]
    end

    subgraph Data Layer
        F1[MySQL Database]
        F2[External APIs]
    end

    A1 -->|REST Calls| B1
    A2 -->|REST Calls| B1
    A3 -->|REST Calls| B1
    A4 -->|REST Calls| B1

    B1 -->|Handles Requests| C1
    C1 -->|Business Logic| D1
    C1 -->|Data Access| D3
    D3 -->|Implementation| E1

    E1 -->|Database Access| F1
    E3 -->|External Data| F2

    C1 -->|Commands and Queries| C2
    C1 -->|Validation| C3
    E2 -->|Logs| F1

```

## Descrição das Camadas
- Clients: Aplicações que consomem a API (Web, Mobile, Insomnia).
- API Layer: Gerencia as requisições e respostas.
- Application Layer: Lida com a lógica de aplicação, regras de uso e validações.
- Domain Layer: Contém as regras de negócios e contratos.
- Infrastructure Layer: Implementações de repositórios, logs e serviços externos.
- Data Layer: Banco de dados MySQL e APIs externas.

## Tecnologias Utilizadas
- ASP.NET Core 6.0: Framework principal para a construção da API.
- Entity Framework Core: ORM para acesso ao banco de dados.
- MediatR: Para implementação do padrão CQRS e comunicação entre as camadas.
- MySQL: Banco de dados relacional utilizado.
- xUnit e FluentAssertions: Frameworks de teste.

## Como Rodar Testes

Execute os testes:

```
dotnet test
```

## Como Adicionar e Rodar Migrations
### Para adicionar uma nova migration:

```
dotnet ef migrations add NomeDaMigration --project src/InvestNetHub.Infra.Data
```

### Para aplicar as migrations ao banco de dados:

```
dotnet ef database update --project src/InvestNetHub.Infra.Data
```
