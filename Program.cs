using Wolverine;
using WolverineDemo.Models.Commands;
using WolverineDemo.Models.Queries;
using WolverineDemo.Models;

namespace WolverineDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            builder.Host.UseWolverine();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/users", async (CreateUser request, IMessageBus bus) =>
            {
                var user = await bus.InvokeAsync<User>(request);
                return Results.Created($"/users/{user.Id}", user);
            });

            app.MapGet("/users/{id:guid}", async (Guid id, IMessageBus bus) =>
            {
                var user = await bus.InvokeAsync<User?>(new GetUser(id));
                return user is null ? Results.NotFound() : Results.Ok(user);
            });

            app.MapPut("/users/{id:guid}", async (Guid id, UpdateUser command, IMessageBus bus) =>
            {
                if (id != command.Id) return Results.BadRequest();

                var updated = await bus.InvokeAsync<User?>(command);
                return updated is null ? Results.NotFound() : Results.Ok(updated);
            });

            app.MapDelete("/users/{id:guid}", async (Guid id, IMessageBus bus) =>
            {
                var deleted = await bus.InvokeAsync<bool>(new DeleteUser(id));
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            app.Run();
        }
    }
}
