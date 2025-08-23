using System.ComponentModel.DataAnnotations;

namespace ScoreKeeperApi.Models;

public class User
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}