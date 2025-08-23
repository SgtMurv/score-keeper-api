using System.ComponentModel.DataAnnotations;

namespace ScoreKeeperApi.Models;

public enum GameStatus
{
    InProgress = 0,
    Completed = 1,
    Cancelled = 2
}

public class Game
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    public Guid UserId { get; set; }
    public GameStatus Status { get; set; } = GameStatus.InProgress;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    
    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();
}