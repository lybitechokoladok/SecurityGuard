using Dapper;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;

        public UserRepository(DBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User entity)
        {
            using (var connection = DBContext.CreateConnection())
            {
               await connection.QueryAsync("Insert into [User] " +
                                                 "values(@Id, @FirstName, @LastName, @Patronomic, @JobTitle,@Birthday, @HashedPassword )",
                   new { Id= entity.Id,
                         FirstName = entity.FirstName,
                         LastName = entity.LastName,
                         Patronomic = entity.Patronomic,
                         JobTitle = entity.JobTitle,
                         Birthday = entity.Birthday,
                         HashedPassword = entity.HashedPassword,});
            }
        }

        public async Task<IEnumerable<User>> GetAllListAsync()
        {
            using (var connection = DBContext.CreateConnection())
            {
                return await connection.QueryAsync<User>("Select * From [User] where");
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var connection = DBContext.CreateConnection()) 
            {
                return await connection.QueryFirstOrDefaultAsync<User>("Select * From [User] where Id = @id",
                    new {id = id});
            }
        }

        public async Task<User> GetUserByFirstNameAsync(string firstName)
        {
            using (var connection = DBContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>("Select * From [User] where FirstName = @firstName",
                    new { firstName = firstName });
            }
        }
       
    }
}
