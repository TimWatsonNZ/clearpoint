using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TodoList.Api.Data;
using TodoList.Api.Exceptions;
using TodoList.Api.Repositories;
using TodoList.Api.Services;

namespace TodoList.Api.UnitTests;

public class TodoServiceTests
{
    private ServiceProvider _serviceProvider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("UnitTestDb"));
        services.AddScoped<ITodoRepo, TodoRepo>();
        services.AddScoped<ITodoService, TodoService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void Teardown()
    {
        var context = _serviceProvider.GetService<TodoContext>();
        context.Database.EnsureDeleted();
        _serviceProvider.Dispose();
    }

    [Test]
    public void TodoItemDescriptionMustBeUnique()
    {
        var service = _serviceProvider.GetService<ITodoService>();
        var todo1 = new TodoItem()
        {
            Description = "MustBeUnique"
        };

        Assert.DoesNotThrowAsync(async () =>
        {
            await service.Create(todo1);
        });

        Assert.ThrowsAsync<EntityAlreadyExistsException>(async () =>
        {
            await service.Create(todo1);
        });
    }

    [Test]
    public void UpdateFailsIfEntityNotFound()
    {
        var service = _serviceProvider.GetService<ITodoService>();

        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await service.Update(new TodoItem()
            {
                Id = Guid.Empty,
                Description = "Update please"
            });
        });
    }

    [Test]
    public async Task UpdateSucceedsIfEntityFound()
    {
        var service = _serviceProvider.GetService<ITodoService>();
        var context = _serviceProvider.GetService<TodoContext>();
        var myTodo = new TodoItem()
        {
            Description = "MyTodo",
            IsCompleted = false
        };

        var createdTodo = await service.Create(myTodo);

        //  Required because service has scoped lifetime.
        context.ChangeTracker.Clear();
        Assert.DoesNotThrowAsync(async () =>
        {
            await service.Update(new TodoItem()
            {
                Id = createdTodo.Id,
                IsCompleted = true
            });
        });
    }
}