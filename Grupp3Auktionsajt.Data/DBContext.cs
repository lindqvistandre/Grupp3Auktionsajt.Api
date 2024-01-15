using Grupp3Auktionsajt.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data
{
    public class DBContext : IDBContext// klar
    {
        private readonly string? _connString;

        public DBContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("Grupp3Auktionsajt");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
