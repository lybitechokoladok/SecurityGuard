using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure.Extensions
{
    public interface IDapperWrapper
    {
        IEnumerable<T> Query<T>(IDbConnection connection, string sql);
    }
}
