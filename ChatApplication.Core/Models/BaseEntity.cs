using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Core.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
