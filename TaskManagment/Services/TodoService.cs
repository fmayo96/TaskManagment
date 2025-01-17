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

        public List<Todo> GetTasks() => _todoContext.Todos.ToList();
        public Todo? GetTaskById(int id) => _todoContext.Todos.FirstOrDefault(t => t.Id == id);
        public async Task<Todo> CreateTask(Todo task)
        {
            await _todoContext.Todos.AddAsync(task);
            await _todoContext.SaveChangesAsync();
            return task;
        }
        public async Task<Todo?> UpdateTask(int id, Todo updateTask)
        {
            var task = _todoContext.Todos.FirstOrDefault( t => t.Id == id);
            if (task == null) return null;
            task.Title = updateTask.Title;
            task.Description = updateTask.Description;
            task.DueDate = updateTask.DueDate;
            task.UpdatedAt = DateTime.Now.Date;
            await _todoContext.SaveChangesAsync();
            return task;
        }
        public async Task<Todo?> DeleteTask(int id)
        {
            var task = _todoContext.Todos.FirstOrDefault(t => t.Id == id);
            if (task == null) return null;
            _todoContext.Todos.Remove(task);
            await _todoContext.SaveChangesAsync();
            return task;
        }
    }
}
