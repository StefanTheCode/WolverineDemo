using WolverineDemo.Models.Queries;
using WolverineDemo.Models;

namespace WolverineDemo.Handlers;

public class GetUserHandler
{
    public User? Handle(GetUser query)
    {
        return InMemoryUsers.Users.FirstOrDefault(u => u.Id == query.Id);
    }
}