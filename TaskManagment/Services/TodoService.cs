using TaskManagment.Models;

namespace TaskManagment.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _todoContext;
        public TodoService(TodoDbContext todoContext)
        {
            _todoContext = todoContext;
        }

        public List<Todo> GetTasks(Guid userId) => _todoContext.Todos.Where(t => t.UserId == userId).ToList();
        public Todo? GetTaskById(int id, Guid userId) => _todoContext.Todos.Where(t => t.UserId == userId)
            .FirstOrDefault(t => t.Id == id);
        public async Task<Todo> CreateTask(Todo task, Guid userId)
        {
            task.UserId = userId;
            task.CreatedAt = DateTime.UtcNow;
            task.IsCompleted = false;
            await _todoContext.Todos.AddAsync(task);
            await _todoContext.SaveChangesAsync();
            return task;
        }
        public async Task<Todo?> UpdateTask(int id, Todo updateTask, Guid userId)
        {
            var task = _todoContext.Todos.Where(t => t.UserId == userId).FirstOrDefault( t => t.Id == id);
            if (task == null) return null;
            task.Title = updateTask.Title;
            task.Description = updateTask.Description;
            await _todoContext.SaveChangesAsync();
            return task;
        }
        public async Task<Todo?> DeleteTask(int id, Guid userId)
        {
            var task = _todoContext.Todos.Where(t => t.UserId == userId).FirstOrDefault(t => t.Id == id);
            if (task == null) return null;
            _todoContext.Todos.Remove(task);
            await _todoContext.SaveChangesAsync();
            return task;
        }
    }
}
