using MyEshop.Models;

namespace MyEshop.Data.Repositories
{
    public interface IUserRepository
    {
        bool IsExistsUserByEmail(string email);
        void AddUser(User user);

        User GetUserForLogin(string email, string password);
    }
}
