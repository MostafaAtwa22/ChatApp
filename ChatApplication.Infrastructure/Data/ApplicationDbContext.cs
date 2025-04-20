using ChatApplication.Core.Models;
using ChatApplication.Core.Models.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }    
        public virtual DbSet<Chat> Chats { get; set; }    
        public virtual DbSet<Message> Messages { get; set; }    
    }
}
