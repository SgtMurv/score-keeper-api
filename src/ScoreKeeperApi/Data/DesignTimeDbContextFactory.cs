using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ScoreKeeperApi.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ScoreKeeperDbContext>
{
    public ScoreKeeperDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ScoreKeeperDbContext>();
        
        // Use the same connection string as in local.settings.json
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=ScoreKeeperDb;Trusted_Connection=true;MultipleActiveResultSets=true;";
        
        optionsBuilder.UseSqlServer(connectionString);

        return new ScoreKeeperDbContext(optionsBuilder.Options);
    }
}