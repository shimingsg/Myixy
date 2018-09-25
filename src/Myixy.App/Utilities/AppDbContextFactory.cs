using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Myixy.App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myixy.App.Utilities
{
    public class AppDbContextFactory
    {
        public static Dictionary<DbType, string> ConnectionStrings { get; private set; }

        static AppDbContextFactory()
        {
            ConnectionStrings = new Dictionary<DbType, string>();
        }

        public static void SetDbContextOption(DbContextOptionsBuilder optionsBuilder, DbType connid)
        {
            var connStr = ConnectionStrings[connid];
            switch (connid)
            {
                case DbType.MySql:
                    optionsBuilder.UseMySql(connStr);
                    break;
                case DbType.MSSql:
                    optionsBuilder.UseSqlServer(connStr);
                    break;
                case DbType.Sqlite:
                    optionsBuilder.UseSqlite(connStr);
                    break;
                default:
                    optionsBuilder.UseSqlServer(connStr);
                    break;
            }
        }

        public static void AddConnectionStrings(IConfiguration configuration)
        {
            ConnectionStrings.Add(DbType.MSSql, configuration.GetConnectionString(DbType.MSSql.ToString()));
            ConnectionStrings.Add(DbType.MySql, configuration.GetConnectionString(DbType.MySql.ToString()));
            ConnectionStrings.Add(DbType.Sqlite, configuration.GetConnectionString(DbType.Sqlite.ToString()));
            ConnectionStrings.Add(DbType.Default, configuration.GetConnectionString(DbType.Default.ToString()));
        }
    }

    public enum DbType
    {
        Default,
        MSSql,
        MySql,
        Sqlite
    }
}
