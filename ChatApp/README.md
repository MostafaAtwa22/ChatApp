# ChatApp

A real-time group and private chat application built with ASP.NET Core, SignalR, and Entity Framework Core.

## Features
- Real-time messaging using SignalR
- Group and private chat support
- User authentication and registration
- Message history and persistence
- Role-based chat membership (Admin/Member)

## Technology Stack
- ASP.NET Core MVC (.NET 8)
- SignalR for real-time communication
- Entity Framework Core (SQL Server)
- Identity for authentication
- JavaScript (with Axios and SignalR client)

## Project Structure
- **Controllers/**: MVC controllers for chat, home, and account management
- **Models/**: Entity models for User, Chat, Message, ChatUser
- **Hubs/ChatHub.cs**: SignalR hub for real-time messaging
- **Views/Home/**: Razor views for chat UI
- **wwwroot/js/**: JavaScript files for SignalR and chat logic

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Setup
1. **Clone the repository:**
   ```bash
   git clone <your-repo-url>
   cd ChatApp
   ```
2. **Configure the database:**
   - Update the connection string in `appsettings.json` if needed:
     ```json
     "ConnectionStrings": {
       "Default": "Data Source=.;Initial Catalog=ChatApp;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
     }
     ```
3. **Apply migrations:**
   ```bash
   dotnet ef database update
   ```
4. **Run the application:**
   ```bash
   dotnet run
   ```
5. **Access in browser:**
   - Navigate to `https://localhost:7131` (or the port shown in your terminal)

## Usage
- **Register/Login:** Create a new user or log in with existing credentials.
- **Join/Create Group:** Join available groups or create a new group chat.
- **Private Chat:** Start a private chat with another user.
- **Send Messages:** Type and send messages in real time. Messages appear instantly for all group members.

## Data Models
- **User:** Inherits from IdentityUser, represents an application user.
- **Chat:** Represents a chat group or private chat.
- **Message:** Stores individual chat messages with sender, content, and timestamp.
- **ChatUser:** Associates users with chats and roles.

## Real-Time Communication
- SignalR is used for real-time message delivery.
- The client connects to `/chatHub` and joins the appropriate group.
- Messages are sent via the `/Chats/SendMessage` endpoint and broadcast to the group.

## Customization
- Update UI in `Views/Home/` and styles in `wwwroot/` as needed.
- Extend models/controllers for more features (e.g., file sharing, notifications).

## License
MIT (or your chosen license) 