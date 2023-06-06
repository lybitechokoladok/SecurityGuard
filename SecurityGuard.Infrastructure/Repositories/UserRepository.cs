using Dapper;
using Microsoft.Data.SqlClient;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System.Collections.Generic;
using System.Data;
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
            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString()))
            {
               await connection.ExecuteAsync("Insert into [User] " +
                                                 "values(@Id, @FirstName, @LastName, @Patronomic, @JobTitle,@Birthday, @HashedPassword )",
                   new { entity });
            }
        }

        public async Task<IEnumerable<User>> GetAllListAsync()
        {
            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString()))
            {
                return await connection.QueryAsync<User>("Select * From [User]");
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString())) 
            {
                return await connection.QueryFirstOrDefaultAsync<User>("Select * From [User] where Id = @id",
                    new { id });
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString()))
            {
                return await connection.QueryFirstOrDefaultAsync<User>("Select * From [User] where Username = @username",
                    new { username });
            }
        }
       
    }
}
