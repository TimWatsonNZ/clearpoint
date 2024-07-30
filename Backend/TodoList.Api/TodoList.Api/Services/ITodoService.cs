using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Data;

namespace TodoList.Api.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetItems();
        Task<TodoItem> GetItem(Guid id);
        Task<TodoItem> Update(TodoItem item);
        Task<TodoItem> Create(TodoItem item);
    }
}
