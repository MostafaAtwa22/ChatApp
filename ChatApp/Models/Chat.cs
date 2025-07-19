using ChatApp.Enums;

namespace ChatApp.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
        public ChatType chatType { get; set; } 
    }
}