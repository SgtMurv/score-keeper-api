# Database Guide

This document covers Entity Framework Core operations for the Score Keeper API project.

## Prerequisites

### Required Tools

1. **Entity Framework Core Tools** (Global Tool)
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **SQL Server LocalDB** (for local development)
    - Installed with Visual Studio
    - Or download separately from Microsoft

3. **Verify Installation**
   ```bash
   dotnet ef --version
   ```

## Project Structure

All database-related code is organized in the `Data/` folder:

```
src/ScoreKeeperApi/
├── Data/
│   ├── Migrations/                    # Generated migration files
│   ├── ScoreKeeperDbContext.cs        # Entity Framework context
│   └── DesignTimeDbContextFactory.cs  # EF design-time factory
├── Models/                            # Domain entities
│   ├── User.cs
│   ├── Player.cs
│   ├── Game.cs
│   └── GamePlayer.cs
└── local.settings.json                # Connection string configuration
```

## Connection Strings

### Local Development
Connection string is configured in `local.settings.json`:

```json
{
  "Values": {
    "ConnectionStrings:DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ScoreKeeperDb;Trusted_Connection=true;MultipleActiveResultSets=true;"
  }
}
```

### Production (Azure)
Connection string will be configured in Azure App Settings:
- `ConnectionStrings:DefaultConnection`

## Common EF Core Commands

**Important**: Always run these commands from the project directory (`src/ScoreKeeperApi/`), not from the Data folder.

### Creating Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName -o Data/Migrations

# Examples:
dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef migrations add AddPlayerStats -o Data/Migrations
dotnet ef migrations add UpdateGameModel -o Data/Migrations
```

### Database Operations

```bash
# Update database to latest migration
dotnet ef database update

# Update to specific migration
dotnet ef database update MigrationName

# Revert to previous migration
dotnet ef database update PreviousMigrationName

# Drop database (careful!)
dotnet ef database drop
```

### Information Commands

```bash
# List all migrations
dotnet ef migrations list

# Show migration history
dotnet ef migrations has-pending-model-changes

# Generate SQL script for migrations
dotnet ef migrations script

# Generate SQL for specific migration range
dotnet ef migrations script FromMigration ToMigration
```

### Removing Migrations

```bash
# Remove last migration (if not applied to database)
dotnet ef migrations remove

# Remove specific migration files manually if needed
# (delete files from Data/Migrations folder)
```

## Development Workflow

### Initial Setup (First Time)

1. **Clone the repository**
   ```bash
   git clone <repo-url>
   cd score-keeper-api/src/ScoreKeeperApi
   ```

2. **Install EF Tools** (if not already installed)
   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. **Create and apply initial migration**
   ```bash
   dotnet ef migrations add InitialCreate -o Data/Migrations
   dotnet ef database update
   ```

### Making Model Changes

1. **Modify entity models** in `Models/` folder

2. **Create migration** for your changes
   ```bash
   dotnet ef migrations add DescriptiveNameForChanges -o Data/Migrations
   ```

3. **Review generated migration** in `Data/Migrations/`

4. **Apply migration** to database
   ```bash
   dotnet ef database update
   ```

5. **Test your changes** locally

6. **Commit migration files** to source control

### Working with Teams

**Always commit migration files** to version control:
- Migration files in `Data/Migrations/`
- Updated model snapshot file

**When pulling changes** that include new migrations:
```bash
git pull
dotnet ef database update
```

## Troubleshooting

### Common Issues

#### "ConnectionString property has not been initialized"
- Ensure `local.settings.json` has the correct connection string
- Check that `DesignTimeDbContextFactory.cs` exists in the `Data/` folder

#### "Build failed" during migration
```bash
# Clean and rebuild
dotnet clean
dotnet build
dotnet ef migrations add YourMigration -o Data/Migrations
```

#### LocalDB not found
```bash
# Check LocalDB installation
sqllocaldb info

# Start LocalDB instance
sqllocaldb start mssqllocaldb
```

#### Migration conflicts
```bash
# If multiple developers create migrations simultaneously:
# 1. Pull latest changes
git pull

# 2. Remove your local migration
dotnet ef migrations remove

# 3. Create new migration after pulling
dotnet ef migrations add YourNewMigrationName -o Data/Migrations
```

### SQL Server LocalDB Commands

```bash
# List LocalDB instances
sqllocaldb info

# Start instance
sqllocaldb start mssqllocaldb

# Stop instance
sqllocaldb stop mssqllocaldb

# Create new instance
sqllocaldb create "ScoreKeeper" -s
```

## Design-Time Factory

The `DesignTimeDbContextFactory` allows EF tools to create a DbContext during design time (when running migrations).

**Why it's needed:**
- EF migration commands run outside the application runtime
- No access to dependency injection or configuration
- Factory provides EF with connection string and DbContext configuration

**Location:** `Data/DesignTimeDbContextFactory.cs`

## Database Schema

### Current Tables

- **Users**: Application users who create games
- **Players**: Game participants (linked to users)
- **Games**: Individual game sessions
- **GamePlayers**: Junction table linking games to players with scores

### Relationships

```
User 1:N Games
User 1:N Players
Game 1:N GamePlayers
Player 1:N GamePlayers
```

## Production Deployment

### Azure SQL Database

For production deployment:

1. **Create Azure SQL Database**
2. **Update connection string** in Azure App Settings
3. **Run migrations** as part of deployment pipeline:
   ```bash
   dotnet ef database update --connection "ProductionConnectionString"
   ```

### Migration Strategy

- **Automated**: Run migrations during deployment
- **Manual**: Generate SQL scripts and apply manually
  ```bash
  dotnet ef migrations script > migration.sql
  ```

## Best Practices

### Migration Naming
- Use descriptive names: `AddUserEmailIndex`, `UpdateGameStatusEnum`
- Avoid generic names: `Update1`, `Changes`, `Fix`

### Model Changes
- Review generated migrations before applying
- Test migrations against copy of production data
- Consider data migration needs for existing records

### Team Collaboration
- Always pull latest changes before creating migrations
- Don't modify existing migration files
- Resolve migration conflicts through communication

### Performance
- Add database indexes for frequently queried columns
- Consider data seeding for reference data
- Monitor migration performance on large datasets

---

*For more Entity Framework Core documentation, visit: https://docs.microsoft.com/en-us/ef/core/*