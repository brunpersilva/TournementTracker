﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.Enums;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initialize the database connections 
            TrackerLibrary.Connections.GlobalConfig.InitializedConnections(DatabaseType.Sql);

            Application.Run(new TournamentDashBoardForm());
            //Application.Run(new TournamentDashBoardForm());

        }
    }
}
