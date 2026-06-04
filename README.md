# Projeto CRUD Pessoa

API REST desenvolvida em ASP.NET Core 8 para gerenciamento de pessoas, utilizando SQL Server, Entity Framework Core, FluentValidation e arquitetura em camadas.

## Objetivo

Implementar uma API para cadastro, atualização, remoção lógica e consulta de pessoas seguindo boas práticas de desenvolvimento e organização do código.

---

## Tecnologias Utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* FluentValidation
* Swagger/OpenAPI

---

## Arquitetura

O projeto foi organizado em camadas para separação de responsabilidades:

```text
ProjetoCrudPessoa
│
├── Controllers
├── DTOs
├── Data
├── Domain
├── Migrations
├── Repositories
├── Services
├── Validators
│
├── Program.cs
├── appsettings.json
└── ProjetoCrudPessoa.sln
```

### Responsabilidades

* **Controllers:** Recebem as requisições HTTP.
* **Services:** Contêm as regras de negócio.
* **Repositories:** Responsáveis pelo acesso aos dados.
* **Validators:** Validações utilizando FluentValidation.
* **DTOs:** Objetos de transferência de dados.
* **Domain:** Entidades da aplicação.
* **Data:** Configuração do Entity Framework Core e contexto do banco de dados.

---

## Padrões Utilizados

* Repository Pattern
* Service Layer
* Dependency Injection
* DTO Pattern
* FluentValidation

---

## Banco de Dados

O projeto utiliza SQL Server.

Exemplo de Connection String:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ProjetoCrudPessoaDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Caso utilize outra instância do SQL Server, ajuste a Connection String conforme seu ambiente.

---

## Executando o Projeto

### Restaurar dependências

```bash
dotnet restore
```

### Aplicar as migrations

```bash
dotnet ef database update
```

### Executar a aplicação

```bash
dotnet run
```

Ou execute diretamente pelo Visual Studio utilizando o perfil HTTPS.

---

## Documentação da API

Após executar a aplicação, o Swagger será aberto automaticamente no navegador.

Caso necessário, acesse:

```text
https://localhost:{porta}/swagger
```

A porta utilizada pode variar conforme a configuração local e está definida no arquivo `Properties/launchSettings.json`.

A documentação é gerada automaticamente pelo Swagger/OpenAPI.

---

# Endpoints

## Cadastrar Pessoa

### POST

```http
POST /api/pessoas
```

### Exemplo de requisição

```json
{
  "nome": "João Silva",
  "cpf": "12345678901",
  "idade": 30,
  "dataNascimento": "1994-05-10"
}
```

---

## Atualizar Pessoa

### PATCH

```http
PATCH /api/pessoas/{id}
```

### Exemplo de requisição

```json
{
  "nome": "João Santos",
  "idade": 31
}
```

---

## Remover Pessoa (Remoção Lógica)

### DELETE

```http
DELETE /api/pessoas/{id}
```

A remoção é lógica através da alteração do campo:

```text
Status = 0
```

A remoção é lógica, alterando o campo Status para 0.

Os registros permanecem armazenados no banco de dados, porém não são retornados nas consultas da API.

---

## Consultar Pessoas

### GET

```http
GET /api/pessoas?page=1&pageSize=10
```

### Filtros disponíveis

#### Filtrar por nome

```http
GET /api/pessoas?nome=joao
```

#### Filtrar por CPF

```http
GET /api/pessoas?cpf=12345678901
```

#### Filtrar por nome e CPF

```http
GET /api/pessoas?nome=joao&cpf=12345678901
```

#### Filtrar utilizando paginação

```http
GET /api/pessoas?page=1&pageSize=5&nome=joao
```

Os filtros são opcionais e podem ser combinados com a paginação.

---

## Regras de Validação

### Nome

* Obrigatório
* Mínimo de 3 caracteres

### CPF

* Obrigatório
* Deve conter exatamente 11 dígitos
* Não permite cadastro de CPF duplicado

### Idade

* Deve ser maior que zero

### Data de Nascimento

* Não pode ser uma data futura

---

## Funcionalidades Implementadas

* Cadastro de pessoas
* Atualização parcial utilizando PATCH
* Remoção lógica
* Consulta paginada
* Filtro opcional por nome
* Filtro opcional por CPF
* Validação de CPF duplicado
* Validação com FluentValidation
* Persistência utilizando Entity Framework Core
* Migrations para criação do banco de dados
* Injeção de dependência
* Documentação automática com Swagger

---

## Autor

Matheus Siqueira Sordi
