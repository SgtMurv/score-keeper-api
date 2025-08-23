# Score Keeper API

A CRUD API built with Azure Functions for tracking game scores between multiple players.

## Overview

Score Keeper is a multi-player game scoring application that allows users to:
- Start new games and select participants
- Track individual player scores during gameplay
- Increment/decrement player scores in real-time
- Reset all scores to zero
- Complete games and view game history
- Manage player profiles

## Architecture

- **Backend**: Azure Functions (.NET 8, Isolated Worker)
- **Database**: SQL Server (LocalDB for development, Azure SQL for production)
- **ORM**: Entity Framework Core
- **Future**: Blazor frontend, Docker containerization, Azure deployment

## Project Structure

```
score-keeper-api/
├── src/
│   ├── score-keeper-api.sln
│   ├── ScoreKeeperApi/              # Main Azure Functions project
│   │   ├── Models/                  # Data models and DbContext
│   │   │   ├── User.cs
│   │   │   ├── Player.cs
│   │   │   ├── Game.cs
│   │   │   ├── GamePlayer.cs
│   │   │   └── ScoreKeeperDbContext.cs
│   │   ├── Program.cs
│   │   ├── Startup.cs               # Dependency injection configuration
│   │   ├── host.json
│   │   └── local.settings.json
│   └── ScoreKeeperApi.UnitTests/    # Unit test project
├── .gitignore
├── LICENSE
└── README.md
```

## Data Model

### Entities

- **User**: Application users who create and manage games
- **Player**: Participants in games (can be users or others)
- **Game**: Individual game sessions with status tracking
- **GamePlayer**: Junction table linking games to players with scores

### Relationships

- User has many Games (1:N)
- User has many Players (1:N) 
- Game has many GamePlayers (1:N)
- Player has many GamePlayers (1:N)
- Game ↔ Player (N:N through GamePlayer)

## Planned API Endpoints

### Game Management
- `POST /api/games` - Start new game
- `GET /api/games` - Get user's games (past and current)
- `GET /api/games/{id}` - Get specific game details
- `PUT /api/games/{id}/complete` - Complete a game
- `DELETE /api/games/{id}` - Delete a game

### Score Management
- `PUT /api/games/{gameId}/players/{playerId}/score` - Update player score
- `POST /api/games/{gameId}/reset-scores` - Reset all scores to 0

### Player Management
- `GET /api/players` - Get all players for user
- `POST /api/players` - Create new player
- `PUT /api/players/{id}` - Update player info

## Development Setup

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 / JetBrains Rider
- SQL Server LocalDB
- Azure Functions Core Tools

### Getting Started

1. **Clone the repository**
   ```bash
   git clone git@github.com:YourUserName/score-keeper-api.git
   cd score-keeper-api
   ```

2. **Open in your IDE**
   ```bash
   # For Rider
   rider src/score-keeper-api.sln
   
   # For Visual Studio
   start src/score-keeper-api.sln
   ```

3. **Install dependencies**
   - NuGet packages should restore automatically
   - If not, run: `dotnet restore` in the `src/` directory

4. **Set up the database**
   ```bash
   # Create migration (once implemented)
   dotnet ef migrations add InitialCreate
   
   # Update database
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   cd src/ScoreKeeperApi
   func start
   ```

### Local Development Database

The application uses SQL Server LocalDB for local development with the following connection string:
```
Server=(localdb)\mssqllocaldb;Database=ScoreKeeperDb;Trusted_Connection=true;MultipleActiveResultSets=true;
```

## Technology Stack

### Current
- **.NET 8** - Runtime framework
- **Azure Functions v4** - Serverless compute platform
- **Entity Framework Core** - Object-relational mapper
- **SQL Server** - Database engine
- **xUnit** - Testing framework

### Planned Additions
- **Docker** - Containerization for local development
- **Blazor** - Frontend web application
- **Azure SQL Database** - Production database
- **Azure App Service** - Production hosting
- **Azure Key Vault** - Secrets management

## Development Status

### ✅ Completed
- [x] Project structure and solution setup
- [x] Data models and Entity Framework configuration
- [x] Git repository with proper .gitignore
- [x] NuGet package dependencies

### 🚧 In Progress
- [ ] Database migrations and initial setup
- [ ] Dependency injection configuration
- [ ] First API endpoint implementation

### 📋 Planned
- [ ] Complete CRUD operations for all entities
- [ ] Input validation and error handling
- [ ] Unit and integration tests
- [ ] Authentication and authorization
- [ ] Docker containerization
- [ ] Azure deployment pipeline
- [ ] Blazor frontend application

## Contributing

This is a learning project focused on exploring:
- Azure Functions development
- Entity Framework Core
- Docker containerization
- Azure cloud deployment
- Blazor web development

## License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.
