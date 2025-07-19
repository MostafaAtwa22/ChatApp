namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = default!;
    }
}