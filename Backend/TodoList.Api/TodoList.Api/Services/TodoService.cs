using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Data;
using TodoList.Api.Exceptions;
using TodoList.Api.Repositories;

namespace TodoList.Api.Services
{
    public class TodoService : ITodoService
    {
        private ITodoRepo _repo;

        public TodoService(ITodoRepo repo)
        {
            _repo = repo;
        }

        public async Task<TodoItem> Create(TodoItem item)
        {
            if (await _repo.TodoItemDescriptionExists(item.Description))
            {
                throw new EntityAlreadyExistsException($"An item with description: {item.Description} already exists");
            }
            return await _repo.Create(item);
        }

        public async Task<TodoItem> GetItem(Guid id)
        {
            return await _repo.Get(id);
        }

        public async Task<List<TodoItem>> GetItems()
        {
            return await _repo.Get();
        }

        public async Task<TodoItem> Update(TodoItem item)
        {
            if (!_repo.TodoItemIdExists(item.Id))
            {
                throw new EntityNotFoundException($"Entity with id {item.Id} not found.");
            }
            return await _repo.Update(item);
        }
    }
}
