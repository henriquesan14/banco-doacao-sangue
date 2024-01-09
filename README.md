## API de Gestão De Doações de Sangue

### Features

- [x] Documentação da API com swagger
- [x] Relatório do estoque de sangue disponível
- [x] Cadastrar e visualizar doadores
- [x] Cadastrar e visualizar doações

### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:
- [.NET](https://dotnet.microsoft.com/en-us/)
- [SQLServer](https://www.microsoft.com/pt-br/sql-server/sql-server-2019)
- [Microsoft Azure](https://azure.microsoft.com/pt-br/)

### 🛠 Padrões Utilizados

As seguintes padrões foram usados na construção do projeto:
- Arquitetura limpa (Clean Architecture)
- CQRS (Command Query Responsibility Segregation)
- SOLID
- Unit Of Work

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.NET](https://dotnet.microsoft.com/en-us/).
[SQLServer](https://www.microsoft.com/pt-br/sql-server/sql-server-2019) ou subir container utilizando o [Docker](https://www.docker.com/).
Também é preciso configurar as váriaveis de conexão com banco de dados e host do RabbitMQ no arquivo `banco-doacao-sangue/src/BancoDoacaoSangue.API/appsettings.Development.json`.
Além disto é bom ter um editor para trabalhar com o código como [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/).


### 🎲 Rodando o Back End (servidor)

#### Rodando Restaurant.API

```bash
# Clone este repositório
$ git clone <https://github.com/henriquesan14/banco-doacao-sangue.git>

# Acesse a pasta do projeto no terminal/cmd
$ cd banco-doacao-sangue

# Vá para a pasta da BancoDoacaoSangue.API
$ cd src/BancoDoacaoSangue.API

# Execute a aplicação com o comando do dotnet
$ dotnet run

# A API iniciará na porta:5000 com HTTP e na porta:5001 com HTTPS - acesse <http://localhost:5001>
```

### Autor
---

<a href="https://www.linkedin.com/in/henrique-san/">
 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/33522361?v=4" width="100px;" alt=""/>
 <br />
 <sub><b>Henrique Santos</b></sub></a> <a href="https://www.linkedin.com/in/henrique-san/">🚀</a>


Feito com ❤️ por Henrique Santos 👋🏽 Entre em contato!

[![Linkedin Badge](https://img.shields.io/badge/-Henrique-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/henrique-san/)](https://www.linkedin.com/in/henrique-san/) 
