using ChatApplication.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Core.Models
{
    public class Message : BaseEntity, ISoftDeleteable
    {
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MinLength(3), MaxLength(3000)]
        public string Text { get; set; } = string.Empty;

        public DateTime TimeStamp { get; set; }

        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; } = default!;

        public bool IsDeleted { get; set; }

        public DateTime? DateOFDelete { get; set; }
    }
}
