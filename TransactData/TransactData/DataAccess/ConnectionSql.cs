using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TransactData.DataAccess
{
    
    public abstract class ConnectionSql
    {
        private readonly string connectionString;
        public ConnectionSql()
        {
            connectionString = "Server=Dantes;database=Store;Integrated security=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
