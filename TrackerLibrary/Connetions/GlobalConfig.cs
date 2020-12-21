using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Connections
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connection { get; private set; } = new List<IDataConnection>();

        public static void InitializedConnections(bool database, bool textFile)
        {
            if (database)
            {
                // TODO - Set up the sql properly
                SqlConnector sql = new SqlConnector();
                Connection.Add(sql);
            }

            if (textFile)
            {
                // TODO - CREATE TEXT CONNECTION
                TextConnection text = new TextConnection();
                Connection.Add(text);
            }
        }
    }
}
