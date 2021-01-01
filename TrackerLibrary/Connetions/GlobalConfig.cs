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

        public const string PrizesFile = "PrizeModels.csv";
        public const string PeopleFile = "PersonModels.csv";
        public const string TeamFile = "TeamModel.csv";
        public const string TournamentFile = "TournamentModels.csv";
        public const string MatchupFile = "MatchupFile.csv";
        public const string MatchupEntryFile = "MatchupEntryFile.csv";


        public static IDataConnection Connection { get; private set; }
        private static Dictionary<DatabaseType, Lazy<IDataConnection>> _strategiesConnection = new Dictionary<DatabaseType, Lazy<IDataConnection>>
        {
            [DatabaseType.Sql] = new Lazy<IDataConnection>(() => new SqlConnector()),
            [DatabaseType.TextFile] = new Lazy<IDataConnection>(() => new TextConnector())
        };

        public static void InitializedConnections(DatabaseType db)
        {
            Connection = _strategiesConnection[db].Value;
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        //        public static void InitializedConnections(DatabaseType db)
        //        {

        //            if (db == DatabaseType.Sql)
        //            {
        //                // TODO - Set up the sql properly
        //                SqlConnector sql = new SqlConnector();
        //                Connection = sql;
        //            }

        //            else if (db == DatabaseType.TextFile)
        //            {
        //                // TODO - CREATE TEXT CONNECTION
        //                TextConnector text = new TextConnector();
        //                Connection = text;
        //;
        //            }

    }
}

