using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Databases
{
    public class AppDB
    {
        public MySqlConnection database { get; }

        public AppDB(string connectionString)
        {
            database = new MySqlConnection(connectionString);
        }
        public void Dispose() => database.Dispose();
    }
}
