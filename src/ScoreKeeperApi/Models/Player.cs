using System.ComponentModel.DataAnnotations;

namespace ScoreKeeperApi.Models;

public class Player
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();
}