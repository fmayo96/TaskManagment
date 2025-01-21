using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        [Authorize]
        [HttpGet("all")]
        public List<Todo> GetTasks()
        {
            var userId = GetUserId();
            return _todoService.GetTasks(userId);   
        }
        [Authorize]
        [HttpGet("id/{id}")]
        public ActionResult<Todo> GetTaskById(int id)
        {
            var userId = GetUserId();
            var task = _todoService.GetTaskById(id, userId);
            return (task == null) ? NotFound() : Ok(task);
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask(Todo task)
        {
            var userId = GetUserId();
            var newTask = await _todoService.CreateTask(task, userId);
            return (newTask == null) ? BadRequest() : Created();       
        }
        [Authorize]
        [HttpPut("complete/{id}")]
        public async Task<ActionResult<Todo>> CompleteTask(int id)
        {
            var userId = GetUserId();
            var task = await _todoService.CompleteTask(id, userId);
            return (task is null)? NotFound() : Ok(task);
        }
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Todo>> UpdateTask(int id, Todo updateTask)
        {
            var userId = GetUserId();
            var task = await _todoService.UpdateTask(id, updateTask, userId);
            return (task==null)? NotFound() : Ok(task);
        }
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Todo>> DeleteTask(int id)
        {
            var userId = GetUserId();
            var task = await _todoService.DeleteTask(id, userId);
            return (task == null) ? NotFound() : Ok(task);
        }

        private Guid GetUserId()
        {
            var nameIdentifier = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            if (nameIdentifier is null) throw new ArgumentNullException();
            string userId = nameIdentifier.ToString().Split(' ')[1];
            return new Guid(userId);
        }
    }
}
