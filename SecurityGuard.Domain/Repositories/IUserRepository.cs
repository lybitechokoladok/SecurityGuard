using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByFirstNameAsync(string firstName);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllListAsync();
        Task AddAsync(User entity);
    }
}
