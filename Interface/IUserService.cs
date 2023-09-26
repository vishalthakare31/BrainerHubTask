using System.Collections.Generic;
using BrainerHubTask.Model;

namespace BrainerHubTask.Services
{
    public interface IUserService
    {
        User GetUserById(int userId);

        IEnumerable<User> GetAllUsers();
        User CreateUser(User newUser);
    }
}
