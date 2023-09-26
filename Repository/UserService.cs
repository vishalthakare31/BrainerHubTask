using System;
using System.Collections.Generic;
using BrainerHubTask.Data;
using BrainerHubTask.Model;
using System.Linq;

namespace BrainerHubTask.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public User GetUserById(int userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User CreateUser(User newUser)
        {
            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser));

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }
    }
}
