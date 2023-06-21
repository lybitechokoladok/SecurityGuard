using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure
{
    public class DbConnection
    {
        private readonly string _connectionString = "Data Source=DESKTOP-0D1UU6P;Initial Catalog=SecurityGuard;Integrated Security=True; Trust Server Certificate= True";


        public string GetConnectionString() 
        {
            if (!string.IsNullOrWhiteSpace(_connectionString))
                return _connectionString;
            else
                throw new Exception();
        }
    }
}
