using Microsoft.EntityFrameworkCore;
using MyEshop.Models;

namespace MyEshop.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MyEshopContext _context;

        public UserRepository(MyEshopContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            //_context.Users.Add(user);
            _context.Add(user);
            _context.SaveChanges();
        }

        public User GetUserForLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool IsExistsUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
