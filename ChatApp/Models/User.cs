using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public class User : IdentityUser
    {
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
    }
}