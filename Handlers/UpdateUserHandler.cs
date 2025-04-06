using WolverineDemo.Models.Commands;
using WolverineDemo.Models;

namespace WolverineDemo.Handlers;

public class UpdateUserHandler
{
    public User? Handle(UpdateUser command)
    {
        var user = InMemoryUsers.Users.FirstOrDefault(u => u.Id == command.Id);
        if (user is null) return null;

        var updated = user with { Name = command.Name, Email = command.Email };

        InMemoryUsers.Users.Remove(user);
        InMemoryUsers.Users.Add(updated);

        return updated;
    }
}