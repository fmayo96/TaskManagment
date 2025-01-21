using TaskManagment.Models;

namespace TaskManagment.Services
{
    public interface ITodoService
    {
        public List<Todo> GetTasks(Guid userId);
        public Todo? GetTaskById(int id, Guid userId);
        public Task<Todo> CreateTask(Todo task, Guid userId);
        public Task<Todo?> CompleteTask(int id, Guid userId);
        public Task<Todo?> UpdateTask(int id, Todo updateTask, Guid userId);
        public Task<Todo?> DeleteTask(int id, Guid userId);
    }
}
