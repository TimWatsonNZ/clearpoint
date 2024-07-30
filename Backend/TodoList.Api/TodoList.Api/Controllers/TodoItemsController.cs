using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Data;
using TodoList.Api.Dtos;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(
            ITodoService todoService, 
            IMapper mapper,
            ILogger<TodoItemsController> logger)
        {
            _todoService = todoService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var results = await _todoService.GetItems();
            var response = _mapper.Map<List<TodoDto>>(results);
            return Ok(response);
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var result = await _todoService.GetItem(id);

            if (result == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<List<TodoDto>>(result);
            return Ok(response);
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoDto todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            await _todoService.Update(_mapper.Map<TodoItem>(todoItem));

            return NoContent();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoDto todoItem)
        {
            var input = _mapper.Map<TodoItem>(todoItem);
            var createdItem = await _todoService.Create(input);
            var response = _mapper.Map<TodoDto>(createdItem);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, response);
        }
    }
}
