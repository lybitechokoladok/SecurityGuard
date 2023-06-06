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
    public class DBContext
    {
        private readonly IConfiguration _configuration;

        public DBContext(IConfiguration config)
        {
            _configuration = config;
        }

        public string GetConnectionString() 
        {
            return _configuration.GetConnectionString("default");
        }
    }
}
