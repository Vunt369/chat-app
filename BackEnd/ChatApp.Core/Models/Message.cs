using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Sender))]
        public string SenderId { get; set; } // User gửi
        public virtual ApplicationUser Sender { get; set; }
        [Required]
        [ForeignKey(nameof(Receiver))]
        public string ReceiverId { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        //public string GroupName { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}