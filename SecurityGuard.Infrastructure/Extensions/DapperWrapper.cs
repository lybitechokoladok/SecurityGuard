using Dapper;
using SecurityGuard.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure.Extensions
{
    public class DapperWrapper : IDapperWrapper
    {

        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql)
        {
            return await connection.QueryAsync<T>(sql);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param)
        {
            return await connection.QueryAsync<T>(sql, param);
        }
    }
}
