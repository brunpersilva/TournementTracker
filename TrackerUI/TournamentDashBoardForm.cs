using System;
using System.Collections.Generic;
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

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournament frm = new CreateTournament();
            frm.Show();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel tm = (TournamentModel)loadExistingTournamentDropBox.SelectedItem;
            TournamentViewerForm frm = new TournamentViewerForm(tm);
            frm.Show();
        }
    }
}
