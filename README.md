# QuickShared

QuickShared is a file-sharing application developed in .NET that stands out for using Telegram as its primary storage service. The application allows users to upload, search, and download files simply and efficiently.

## ✨ Features

- **File Upload**: Drag-and-drop interface or file selection for easy uploading.
- **Telegram Storage**: Uploaded files are stored in a private Telegram channel or chat, using the Telegram Bot API.
- **File Compression**: (Assuming this is part of the process) Files are compressed before upload to optimize storage.
- **File Search**: Search functionality to find files by name.
- **Direct Download**: Generation of direct download links for files.

## 🚀 Technologies and Architecture

The project was built using a modern approach with .NET, following the principles of Clean Architecture and Vertical Slice Architecture.

### Tech Stack

- **Backend**:
  - **Framework**: .NET 9 / ASP.NET Core
  - **Architecture Pattern**: Clean Architecture with Vertical Slices
  - **Communication**: MediatR for implementing the CQRS pattern
- **Frontend**:
  - **UI**: ASP.NET Core Razor Pages
  - **Styling**: Custom CSS (with possible use of frameworks like Bootstrap/Tailwind)
- **Database**:
  - **ORM**: Entity Framework Core
  - **DBMS**: PostgreSQL
- **File Storage**:
  - **Service**: Telegram Bot API
- **DevOps**:
  - **CI/CD**: GitHub Actions
  - **Deploy**: FTP

## 🏗️ Project Structure

The source code is organized into distinct projects, each with its own responsibility:

- `QuickShared.Domain`: Contains business entities, abstractions, and core domain logic.
- `QuickShared.Application`: Contains application logic, such as MediatR handlers for each feature (Save File, Search File, etc.).
- `QuickShared.Infrastructure`: Implements external services, such as database access (EF Core) and the Telegram storage service.
- `QuickShared.Web`: Web interface for user interaction, built with Razor Pages.
- `QuickShared.CrossCutting`: Project for configuring dependency injection and other cross-cutting concerns.

## ⚙️ How to Run the Project

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- PostgreSQL
- A Telegram Bot and the ID of a chat/channel for storage.

### Configuration

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-user/QuickShared.git
   ```
2. **Configure credentials:**
   - In the `src/QuickShared.Web/appsettings.json` file, configure the `ConnectionString` for your PostgreSQL database.
   - Configure your Telegram Bot information, such as `BotToken` and `ChatId`, in the `StorageTelegramOptions` section. It is highly recommended to use User Secrets for this in a development environment.
3. **Apply Migrations:**
   Run the command below from the `src/QuickShared.Infrastructure` folder to create the database structure:
   ```bash
   dotnet ef database update
   ```
4. **Run the web application:**
   Navigate to the `src/QuickShared.Web` folder and run the command:
   ```bash
   dotnet run
   ```
   The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## 🔄 CI/CD

The project has a Continuous Integration and Continuous Deployment pipeline configured with GitHub Actions in the `.github/workflows/dotnet-ci-cd.yml` file.

The pipeline is triggered on each `push` to the `main` branch and performs the following steps:
1.  Builds the solution.
2.  Runs automated tests.
3.  Publishes the web application artifacts.
4.  Deploys the artifacts via FTP to the production server.