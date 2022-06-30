using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UserAuthentication.Models;

public class AppUser : IdentityUser
{
    [Column(TypeName = "nvarchar(150)")]
    public string? Name { get; set; } = string.Empty;
    [Column(TypeName = "nvarchar(150)")]
    public string? LastName { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(1000)")]
    public string? Description { get; set; } = string.Empty;
    [Column(TypeName = "nvarchar(200)")]
    public string? ImagePath { get; set; } = string.Empty;
}
