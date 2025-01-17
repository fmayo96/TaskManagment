using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TaskManagment.Models
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool  IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
