using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TaskManagment.Models;
using TaskManagment.Services;

namespace TaskManagment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TaskController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet("/all")]
        public List<Todo> GetTasks() => _todoService.GetTasks();
        [HttpGet("/id/{id}")]
        public ActionResult<Todo> GetTaskById(int id)
        {
            var task = _todoService.GetTaskById(id);
            return (task == null) ? NotFound() : Ok(task);
        }
        [HttpPost("/create")]
        public async Task<IActionResult> CreateTask(Todo task)
        {
            var newTask = await _todoService.CreateTask(task);
            return (newTask == null) ? BadRequest() : Created();       
        }
        [HttpPut("/update/{id}")]
        public async Task<ActionResult<Todo>> UpdateTask(int id, Todo updateTask)
        {
            var task = await _todoService.UpdateTask(id, updateTask);
            return (task==null)? NotFound() : Ok(task);
        }
        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult<Todo>> DeleteTask(int id)
        {
            var task = await _todoService.DeleteTask(id);
            return (task == null) ? NotFound() : Ok(task);
        }
    }
}
