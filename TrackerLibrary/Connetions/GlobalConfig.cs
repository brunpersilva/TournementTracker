using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using TrackerLibrary.Enums;

namespace TrackerLibrary.Connections
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        public static void InitializedConnections(DatabaseType db)
        {

            if (db == DatabaseType.Sql)
            {
                // TODO - Set up the sql properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }

            else if (db == DatabaseType.TextFile)
            {
                // TODO - CREATE TEXT CONNECTION
                TextConnector text = new TextConnector();
                Connection = text;
;
            }

        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}