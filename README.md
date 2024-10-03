# Web Scraper Teste API

Uma API desenvolvida para extrair informações sobre alimentos de um site específico e armazená-las em um banco de dados.

## Visão Geral

Esta API permite que os usuários extraiam dados de alimentos, incluindo informações detalhadas sobre cada alimento e seus componentes. A extração é feita a partir do site [TBCA](https://www.tbca.net.br), onde os dados são organizados em tabelas.

## Tecnologias Utilizadas

- **.NET 6**: A plataforma de desenvolvimento para construir a API.
- **Entity Framework Core**: Para interações com o banco de dados.
- **HtmlAgilityPack**: Uma biblioteca para facilitar a extração de dados HTML.
- **SQLite**: Para armazenamento de dados.
- **ASP.NET Core**: Para a construção da API.

## Funcionalidades

- **Extração de Alimentos**:
  - Endpoint: `GET /api/alimentos/scrape`
  - Descrição: Raspa os alimentos de uma página da web e os armazena no banco de dados. Você pode especificar a página a ser raspada, com um parâmetro opcional.

## Instruções de Instalação

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/othongonzaga/web-scraper-api-teste-vaga.git
   cd web-scraper-api

2. **Instale as dependências**:
 - Certifique-se de ter o .NET SDK instalado.
	- Execute o seguinte comando no terminal:
   ```bash
   dotnet restore

3. **Configure o banco de dados**:
   - Abra o arquivo AppDbContext.cs e configure a string de conexão de acordo com o seu ambiente

4. **Migrations**:
 - Execute o seguinte comando para criar a tabela no banco de dados:
   ```bash
   dotnet ef database update

5. **Execute a aplicação**:
   ```bash
   dotnet run

6. **Teste a API**:
   - Utilize uma ferramenta como Postman, Swagger ou Insomnia para interagir com a API.