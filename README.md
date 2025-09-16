# QuickShared

QuickShared é uma aplicação de compartilhamento de arquivos desenvolvida em .NET que se destaca por utilizar o Telegram como seu principal serviço de armazenamento. A aplicação permite que os usuários façam upload, pesquisem e baixem arquivos de forma simples e eficiente.

## ✨ Funcionalidades

- **Upload de Arquivos**: Interface de arrastar e soltar (drag-and-drop) ou seleção de arquivo para um upload fácil.
- **Armazenamento no Telegram**: Os arquivos enviados são armazenados em um canal ou chat privado do Telegram, utilizando a API de Bots do Telegram.
- **Compressão de Arquivos**: (Assumindo que isso faz parte do processo) Os arquivos são comprimidos antes do upload para otimizar o armazenamento.
- **Busca de Arquivos**: Funcionalidade de busca para encontrar arquivos pelo nome.
- **Download Direto**: Geração de links para download direto dos arquivos.

## 🚀 Tecnologias e Arquitetura

O projeto foi construído utilizando uma abordagem moderna com .NET, seguindo princípios de Arquitetura Limpa e _Vertical Slice Architecture_.

### Tech Stack

- **Backend**:
  - **Framework**: .NET 9 / ASP.NET Core
  - **Padrão de Arquitetura**: Arquitetura Limpa com _Vertical Slices_
  - **Comunicação**: MediatR para implementação do padrão CQRS
- **Frontend**:
  - **UI**: ASP.NET Core Razor Pages
  - **Estilização**: CSS customizado (com possível uso de frameworks como Bootstrap/Tailwind)
- **Banco de Dados**:
  - **ORM**: Entity Framework Core
  - **SGBD**: PostgreSQL
- **Armazenamento de Arquivos**:
  - **Serviço**: Telegram Bot API
- **DevOps**:
  - **CI/CD**: GitHub Actions
  - **Deploy**: FTP

## 🏗️ Estrutura do Projeto

O código-fonte é organizado em projetos distintos, cada um com sua responsabilidade:

- `QuickShared.Domain`: Contém as entidades de negócio, abstrações e lógica de domínio principal.
- `QuickShared.Application`: Contém a lógica da aplicação, como os _handlers_ do MediatR para cada funcionalidade (Salvar Arquivo, Buscar Arquivo, etc.).
- `QuickShared.Infrastructure`: Implementa os serviços externos, como o acesso ao banco de dados (EF Core) e o serviço de armazenamento no Telegram.
- `QuickShared.Web`: Interface web para interação do usuário, construída com Razor Pages.
- `QuickShared.CrossCutting`: Projeto para configuração da injeção de dependência e outras configurações transversais.

## ⚙️ Como Executar o Projeto

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- PostgreSQL
- Um Bot do Telegram e o ID de um chat/canal para o armazenamento.

### Configuração

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seu-usuario/QuickShared.git
   ```
2. **Configure as credenciais:**
   - No arquivo `src/QuickShared.Web/appsettings.json`, configure a `ConnectionString` para o seu banco de dados PostgreSQL.
   - Configure as informações do seu Bot do Telegram, como o `BotToken` e o `ChatId`, na seção `StorageTelegramOptions`. É altamente recomendado usar _User Secrets_ para isso em ambiente de desenvolvimento.
3. **Aplique as Migrations:**
   Execute o comando abaixo a partir da pasta `src/QuickShared.Infrastructure` para criar a estrutura do banco de dados:
   ```bash
   dotnet ef database update
   ```
4. **Execute a aplicação web:**
   Navegue até a pasta `src/QuickShared.Web` e execute o comando:
   ```bash
   dotnet run
   ```
   A aplicação estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

## 🔄 CI/CD

O projeto possui um pipeline de Integração e Deploy Contínuo configurado com GitHub Actions no arquivo `.github/workflows/dotnet-ci-cd.yml`.

O pipeline é acionado a cada `push` na branch `main` e executa os seguintes passos:
1.  Builda a solução.
2.  Executa os testes automatizados.
3.  Publica os artefatos da aplicação web.
4.  Realiza o deploy dos artefatos via FTP para o servidor de produção.
