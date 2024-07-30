using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Data;

namespace TodoList.Api.Repositories
{
    public class TodoRepo : ITodoRepo
    {
        private readonly TodoContext _context;

        public TodoRepo(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TodoItem> Get(Guid id)
        {
            var result = await _context.TodoItems.FindAsync(id);
            return result;
        }

        public async Task<List<TodoItem>> Get()
        {
            var results = await _context.TodoItems
                    .Where(x => !x.IsCompleted)
                    .ToListAsync();

            return results;
        }

        public async Task<TodoItem> Update(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public Task<bool> TodoItemDescriptionExists(string description)
        {
            return _context.TodoItems
                   .AnyAsync(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
        }

        public bool TodoItemIdExists(Guid id)
        {
            return _context.TodoItems.Any(x => x.Id == id);
        }
    }
}
