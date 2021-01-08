using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Connections;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournament : Form, IPrizeRequester, ITeamRequester
    {

        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournament()
        {
            InitializeComponent();
            WireupList();
        }

        private void WireupList()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {

            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;
            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);
                WireupList();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //Call the createPrizeForm
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();

        }

        public void PrizeComplete(PrizeModel model)
        {
            //Get back from the form a PrizeForm
            //Take the PrizeModel and put it into our list of selected  prizes

            selectedPrizes.Add(model);
            WireupList();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireupList();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void removeSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;
            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);
                WireupList();
            }

        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);
                WireupList();
            }
        }

        private void createTorunamentButton_Click(object sender, EventArgs e)
        {
            //Validate Data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);
            if (!feeAcceptable)
            {
                MessageBox.Show("You need to entry a valid Entry fee",
                    "Invalid Fee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            //Create Tournament mmodel
            TournamentModel tm = new TournamentModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;
            tm.Prizes = selectedPrizes;
            tm.EnteredTeam = selectedTeams;


            //Todo - Wire up our Matchups
            TournamentLogic.CreateRounds(tm);

            //Create tournament entry
            //Create all the prizes antries
            //Create all the teams entries
            GlobalConfig.Connection.CreateTournament(tm);
            TournamentViewerForm frm = new TournamentViewerForm(tm);
            frm.Show();
            this.Close();

        }
    }
}
