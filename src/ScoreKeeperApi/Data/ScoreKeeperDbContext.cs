using Microsoft.EntityFrameworkCore;
using ScoreKeeperApi.Models;

namespace ScoreKeeperApi.Data;

public class ScoreKeeperDbContext(DbContextOptions<ScoreKeeperDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Game> Players { get; set; } = null!;
    public DbSet<Game> GamePlayer { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();
        });
        
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => new { e.Id });
            entity.HasOne(e => e.User)
                .WithMany(e => e.Games)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => new { e.Id });
            entity.HasOne(e => e.User)
                .WithMany(e => e.Players)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<GamePlayer>(entity =>
        {
            entity.HasKey(e => new { e.Id });
            
            entity.HasOne(e => e.Game)
                .WithMany(e => e.GamePlayers)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Player)
                .WithMany(e => e.GamePlayers)
                .HasForeignKey(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Ensure a player can only be in a game once
            entity.HasIndex(e => new { e.GameId, e.PlayerId }).IsUnique();
        });
    }
}