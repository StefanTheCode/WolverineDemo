using WolverineDemo.Models.Commands;

namespace WolverineDemo.Handlers;

public class DeleteUserHandler
{
    public bool Handle(DeleteUser command)
    {
        var user = InMemoryUsers.Users.FirstOrDefault(u => u.Id == command.Id);
        if (user is null) return false;

        InMemoryUsers.Users.Remove(user);
        return true;
    }
}