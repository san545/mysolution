using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMERWEBAPI.DataAccess.Data
{
    public class sqlDBConnectionFactory
    {
        private readonly string _conn;
        public sqlDBConnectionFactory(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
        public SqlConnection CreateConnection()
        {

            return new SqlConnection(_conn);

        }
    }
}
