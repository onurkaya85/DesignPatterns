using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyPattern.Web.Models
{
    public class Settings
    {
        public static string claimDatabaseType = "databaseType";
        public EDatabaseType DatabaseType;
        public EDatabaseType GetDefaultDtabaseType => EDatabaseType.SqlServer;

    }

    public enum EDatabaseType
    {
        SqlServer = 1,
        MongoDb = 2
    }
}
