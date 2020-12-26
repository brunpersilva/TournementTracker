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
    public partial class CreateTournament : Form
    {

        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        public CreateTournament()
        {
            InitializeComponent();
            InitializeList();
        }

        private void InitializeList()
        {
            selectTeamDropBox.DataSource = availableTeams;
            selectTeamDropBox.DisplayMember = "TeamName";
        }
    }
}
