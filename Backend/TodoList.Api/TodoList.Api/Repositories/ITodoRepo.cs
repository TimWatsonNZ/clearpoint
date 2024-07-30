using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Data;

namespace TodoList.Api.Repositories
{
    public interface ITodoRepo
    {
        Task<TodoItem> Create(TodoItem item);
        Task<TodoItem> Update(TodoItem item);
        Task<TodoItem> Get(Guid id);
        Task<List<TodoItem>> Get();
        Task<bool> TodoItemDescriptionExists(string description);
        bool TodoItemIdExists(Guid id);
    }
}
