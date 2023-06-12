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
    public class RequestRepository : IRequestRepository
    {
        private readonly DbConnection _connection;
        public RequestRepository(DbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Request>> GetAllListAsync()
        {
            using (IDbConnection connection = new SqlConnection(_connection.GetConnectionString()))
            {
                var sql = @"Select * from [Request] r
                            left join [Client] c
                            on r.ClientId = c.Id";

               var requests = await connection.QueryAsync<Request, Client, Request>(sql, (request, client) => 
               {
                   request.Client = client;
                   return request;
               });

                return requests;
            }
        }

        public async Task<Request> GetRequestByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connection.GetConnectionString()))
            {
                return await connection.QueryFirstOrDefaultAsync<Request>("Select * from [Request] where Id=@id",
                    new { id });
            }
        }
    }
}
