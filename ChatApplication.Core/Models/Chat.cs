using ChatApplication.Core.Interfaces;
using ChatApplication.Core.Models.Enums;
using ChatApplication.Core.Models.Identities;

namespace ChatApplication.Core.Models
{
    public class Chat : BaseEntity, ISoftDeleteable
    {
        public ICollection<Message> Message { get; set; } = new List<Message>();

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();

        public ChatType ChatType { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateOFDelete { get; set; }
    }
}
