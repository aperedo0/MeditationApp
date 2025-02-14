using System.ComponentModel.DataAnnotations;
namespace MeditationApp.Models;

public class MeditationSession
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AudioUrl { get; set; } // Store meditation audio links
    public int Duration { get; set; } // in minutes
    [Required]
    public string? UserId { get; set; }
}
