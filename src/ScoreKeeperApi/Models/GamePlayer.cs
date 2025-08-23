namespace ScoreKeeperApi.Models;

public class GamePlayer
{
    public Guid Id { get; set; }
    
    public Guid GameId { get; set; }
    public Guid PlayerId { get; set; }
    public int Score { get; set; } = 0;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual Game Game { get; set; } = null!;
    public virtual Player Player { get; set; } = null!;
}