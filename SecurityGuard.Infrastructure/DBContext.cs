using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure
{
    public class DBContext
    {
        private static string _conectionString;

        public static SqlConnection CreateConnection() 
        {
            return new SqlConnection(_conectionString);
        }
    }
}
