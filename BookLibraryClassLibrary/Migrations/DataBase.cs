using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BookLibraryClassLibrary.Migrations
{
    public static class DataBase
    {
        public static void EnsureDataBase(IConfiguration config, string DbName = "NewGuidBookLibraryDB")
        {
            var parameters = new DynamicParameters();
            parameters.Add("name", DbName);

            using var conn = new SqlConnection(config.GetConnectionString("Master"));


            if (!conn.Query("SELECT 1 FROM sys.databases WHERE name = @name", parameters).Any())
            {
                conn.Execute($"CREATE DATABASE {DbName}");
            }
        }
    }
}
