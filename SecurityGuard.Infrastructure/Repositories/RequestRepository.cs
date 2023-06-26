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
                            left join [Client] c on r.ClientId = c.Id
                            left join [RequestType] rt on r.RequestTypeId = rt.Id
                            left join [RequestDetails] rd on r.RequestDetailId = rd.Id
                            inner join [User] u on rd.UserId = u.Id
                            inner join [RequestState] rs on rd.RequestStateId = rs.Id";

               var requests = await connection.QueryAsync<Request, Client,RequestType,RequestDetails,User,RequestState,  Request>
                    (sql, (request, client, requestType, requestDetail ,user, requestState) => 
               {
                   request.Clients = client;
                   request.Type = requestType;
                   request.RequestDetails = requestDetail;
                   requestDetail.User = user;
                   requestDetail.RequestState = requestState;
                   return request;
               });

                return requests;
            }
        }

        public async Task<IEnumerable<Request>> GetAllListByStateAsync(int stateId)
        {
            using (IDbConnection connection = new SqlConnection(_connection.GetConnectionString()))
            {
                var sql = @"Select * from [Request] r
                            left join [Client] c on r.ClientId = c.Id
                            left join [RequestType] rt on r.RequestTypeId = rt.Id
                            left join [RequestDetails] rd on r.RequestDetailId = rd.Id
                            inner join [User] u on rd.UserId = u.Id
                            inner join [RequestState] rs on rd.RequestStateId = rs.Id
                            where rs.Id = @stateId";

                var requests = await connection.QueryAsync<Request, Client, RequestType, RequestDetails, User, RequestState, Request>
                     (sql, (request, client, requestType, requestDetail, user, requestState) =>
                     {
                         request.Clients = client;
                         request.Type = requestType;
                         request.RequestDetails = requestDetail;
                         requestDetail.User = user;
                         requestDetail.RequestState = requestState;
                         return request;
                     }, new { stateId} );

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

        public async Task<bool>UpdateStateAsync(int requestDetailsId, int newState)
        {
            using(IDbConnection connection = new SqlConnection(_connection.GetConnectionString())) 
            {
                var sql = @"Update RequestDetails set RequestStateId=@newState where Id=@requestDetailsId";

                var result = await connection.ExecuteAsync(sql, new { newState, requestDetailsId });

                if(result == 0)
                    return false;
                else
                    return true;
            }
        }
    }
}
