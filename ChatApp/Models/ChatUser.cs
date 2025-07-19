using ChatApp.Enums;

namespace ChatApp.Models
{
    public class ChatUser
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = default!;
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = default!;
        public UserRole Role  { get; set; } = UserRole.Member;
    }

}