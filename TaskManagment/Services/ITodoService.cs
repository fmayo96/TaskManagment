using TaskManagment.Models;

namespace TaskManagment.Services
{
    public interface ITodoService
    {
        public List<Todo> GetTasks();
        public Todo? GetTaskById(int id);
        public Task<Todo> CreateTask(Todo task);
        public Task<Todo?> UpdateTask(int id, Todo updateTask);
        public Task<Todo?> DeleteTask(int id);
    }
}
