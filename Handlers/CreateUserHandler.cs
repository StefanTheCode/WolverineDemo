using WolverineDemo.Models.Commands;
using WolverineDemo.Models;

namespace WolverineDemo.Handlers;

public class CreateUserHandler
{
    public User Handle(CreateUser command)
    {
        var user = new User(Guid.NewGuid(), command.Name, command.Email);
        InMemoryUsers.Users.Add(user);

        return user;
    }
}
