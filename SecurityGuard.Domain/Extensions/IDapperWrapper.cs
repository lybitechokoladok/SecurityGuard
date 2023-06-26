using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Extensions
{
    public interface IDapperWrapper
    {
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql);
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param);
    }
}
