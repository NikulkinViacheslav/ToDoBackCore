using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListReact.Domain;

namespace TodoListReact.Controllers
{
    [EnableCors("AllowAll")]    // Применяем политику к контроллеру
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {        
        private readonly ApplicationDbContext context;

        public TodoItemController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/TodoItem
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemsAsync()
        {
            return await context.TodoItems.ToListAsync();
        }

        // GET: api/ToDoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItemsAsync(int id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();
            return todoItem;
        }

        // PUT: api/ToDoItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemAsync(int id, TodoItem model)
        {
            model.Id = id;
            
            context.Entry(model).State = EntityState.Modified;
            
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // POST: api/ToDoItem
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem model)
        {
            context.TodoItems.Add(model);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetTodoItemsAsync", new { id = model.Id }, model);
        }

        // DELETE: api/ToDoItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            
            if (todoItem == null)
                return NotFound();
            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync();
            
            return NoContent();
        }

        private bool TodoItemExists(int id)
        {
            return context.TodoItems.Any(x => x.Id == id);
        }
    }
}
