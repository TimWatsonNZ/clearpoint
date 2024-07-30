using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Dtos
{
    public class TodoDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
