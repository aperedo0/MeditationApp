using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace MeditationApp.Models;


public class User : IdentityUser
{
    [Key]
    public string? FullName { get; set; }
}
