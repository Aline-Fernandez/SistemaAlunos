# Sistema de Gestão de Alunos (Backend - Console)

Bem-vindo(a) ao projeto do **Sistema de Gestão de Alunos**!

Este repositório contém o backend inicial de um sistema de gestão de alunos, desenvolvido em C# como uma aplicação de console. Ele se conecta a um banco de dados Azure SQL Server e implementa as operações fundamentais de CRUD (Criar, Ler, Atualizar, Excluir).

Este projeto está configurado para ser a base para a futura camada de API, que será consumida por um frontend moderno.

## 📄 Sumário

* [Visão Geral do Projeto](#1-visão-geral-do-projeto)
* [Tecnologias Utilizadas](#2-tecnologias-utilizadas)
* [Configuração do Ambiente (Backend)](#3-configuração-do-ambiente-backend)

  * [Pré-requisitos](#pré-requisitos)
  * [Configuração do Banco de Dados Azure SQL](#configuração-do-banco-de-dados-azure-sql)
  * [Configuração do Projeto C#](#configuração-do-projeto-c)
* [Como Executar o Backend (Console)](#como-executar-o-backend-console)
* [Para o Desenvolvedor Frontend](#4-para-o-desenvolvedor-frontend)
* [Próximos Passos Sugeridos para o Backend (API)](#próximos-passos-sugeridos-para-o-backend-api)
* [Estrutura de Dados (Model)](#estrutura-de-dados-model)
* [Regras de Negócio Importantes](#regras-de-negócio-importantes)
* [Camada de Acesso a Dados (Repository)](#camada-de-acesso-a-dados-repository)
* [Colaboração](#5-colaboração)
* [Contato](#6-contato)

## 1. Visão Geral do Projeto

O objetivo principal deste projeto é gerenciar informações de alunos de forma eficiente. A versão atual é um protótipo de console que demonstra a interação com o banco de dados. A próxima fase envolve a criação de uma API RESTful a partir deste backend para permitir a integração com uma interface de usuário mais rica e interativa.

## 2. Tecnologias Utilizadas

**Backend (Atual):**

* Linguagem: C#
* Plataforma: .NET (Console Application)
* Banco de Dados: Azure SQL Database (SQL Server)
* Acesso a Dados: Microsoft.Data.SqlClient (ADO.NET)

**Frontend (Futuro):**

* (Preencher com as tecnologias planejadas: React, Angular, etc.)

## 3. Configuração do Ambiente (Backend)

### Pré-requisitos

* **Visual Studio**: Versão 2019 ou superior, com workloads ".NET desktop development" e "Azure development"
* **SQL Server Management Studio (SSMS)**
* **Conta Azure** com banco de dados Azure SQL criado

### Configuração do Banco de Dados Azure SQL

Conecte-se ao banco de dados `SistemaAlunoDB` com o SSMS e execute:

```sql
USE SistemaAlunoDB;

CREATE TABLE Alunos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    DataNascimento DATE,
    Curso VARCHAR(100)
);

ALTER TABLE dbo.Alunos ADD CPF VARCHAR(14);

-- Atualizar CPFs nulos para valores únicos, se necessário
-- UPDATE dbo.Alunos SET CPF = '000.000.000-01' WHERE Id = 1; etc...

ALTER TABLE dbo.Alunos ADD CONSTRAINT UQ_Alunos_CPF UNIQUE (CPF);
-- Para tornar obrigatório:
-- ALTER TABLE dbo.Alunos ALTER COLUMN CPF VARCHAR(14) NOT NULL;
```

### Configuração do Projeto C\#

1. Clone ou baixe o repositório
2. Abra o arquivo `.sln` no Visual Studio
3. Restaure os pacotes NuGet
4. Atualize a `connectionString` em `SistemaAlunos/Data/Conexao.cs` com seus dados reais:

```csharp
private static string connectionString = "Server=tcp:SEU_SERVIDOR.database.windows.net,1433;Initial Catalog=SistemaAlunoDB;Persist Security Info=False;User ID=SEU_USUARIO;Password=SUA_SENHA;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
```

## Como Executar o Backend (Console)

Pressione `F5` no Visual Studio ou clique em **Iniciar Depuração** para executar o sistema no console. O menu interativo permite operações CRUD diretamente com o banco de dados.

## 4. Para o Desenvolvedor Frontend

Este backend serve como base para uma futura API RESTful. A transformação ideal seria a criação de um projeto ASP.NET Core Web API, que reutilize os modelos e repositórios existentes.

## Próximos Passos Sugeridos para o Backend (API)

* Criar um projeto ASP.NET Core Web API
* Reutilizar classes `Models`, `Data`, `Repositories` em uma Class Library
* Expor métodos de CRUD como endpoints REST (GET, POST, PUT, DELETE)

## Estrutura de Dados (Model)

```csharp
public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Curso { get; set; }
    public string CPF { get; set; }
}
```

## Regras de Negócio Importantes

* **CPF único:** Validação garantida pelo banco (constraint UNIQUE). Evite duplicidade.
* **ID automático:** O campo `Id` é gerado automaticamente pelo banco.

## Camada de Acesso a Dados (Repository)

Classe `AlunoRepository` com os seguintes métodos:

* `ListarTodos()`
* `Adicionar(Aluno aluno)`
* `Atualizar(Aluno aluno)`
* `Excluir(int id)`
* `BuscarPorId(int id)`
* `ExisteCPF(string cpf)`

## 5. Colaboração

* Crie branches por funcionalidade: `git checkout -b feature/nova-feature`
* Commits pequenos e claros
* Pull Requests com revisão antes do merge

## 6. Contato

**Aline** – \[Seu LinkedIn ou e-mail aqui]
**Frontend Dev (se houver)** – \[Contato dele aqui]
