using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Models;

namespace UserAuthentication.Utils;

public class AppDbContext : IdentityDbContext
{
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base (options)
    {
        
    }
}
