using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagment.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Todo> Todos { get; } = new List<Todo>(); 
    }
}
