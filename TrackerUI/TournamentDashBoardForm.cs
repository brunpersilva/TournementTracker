using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.Connections;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentDashBoardForm : Form
    {

        List<TournamentModel> tournaments = GlobalConfig.Connection.GetTournament_All();

        private void WireUpList()
        {
            //loadExistingTournamentDropBox.DataSource = null;
            loadExistingTournamentDropBox.DataSource = tournaments;
            loadExistingTournamentDropBox.DisplayMember = "TournamentName";
        }
        public TournamentDashBoardForm()
        {
            InitializeComponent();
            WireUpList();
        }
    }
}
