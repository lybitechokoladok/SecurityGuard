using Dapper;
using Microsoft.Data.SqlClient;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DbConnection _context;
        public ClientRepository(DbConnection context)
        {
            _context = context;
        }

        public async Task<bool> FindClientBlackListByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString())) 
            {
                var sql = @"Select * from [ClientBlackList] where ClientId = @id";
                var client = await connection.QueryFirstOrDefaultAsync<ClientBlackList>(sql, new { id });

                if (client != null)
                    return true;
                return false;
            }
        }

        public Task<Client> GetClientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
