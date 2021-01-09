using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Connections;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel _tournament;
        private List<int> rounds = new List<int>();
        private List<MatchupModel> selectedMatchups = new List<MatchupModel>();
        public TournamentViewerForm(TournamentModel tournament)
        {
            InitializeComponent();
            _tournament = tournament;
            LoadFormData();
            LoadRounds();
        }

        private void LoadFormData()
        {
            tournmantName.Text = _tournament.TournamentName;
        }
        private void WireupRoundsList()
        {
            roundDropDown.DataSource = null;
            roundDropDown.DataSource = rounds;
        }
        private void WireupMatchupsList()
        {
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }
        private void LoadRounds()
        {
            rounds = new List<int>();
            rounds.Add(1);
            int currRound = 1;

            foreach (List<MatchupModel> matchups in _tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currRound)
                {
                    currRound = matchups.First().MatchupRound;
                    rounds.Add(currRound);
                }

            }
            WireupRoundsList();
        }
        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }
        private void LoadMatchups()
        {
            int round = (int)roundDropDown.SelectedItem;
            foreach (List<MatchupModel> matchups in _tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups = new List<MatchupModel>();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckbox.Checked)
                        {
                            selectedMatchups.Add(m);
                        }
                    }
                }
            }
            WireupMatchupsList();
            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            bool isVisible = (selectedMatchups.Count > 0);
            teamOneName.Visible = isVisible;
            teamOneScoreLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;
            teamTwoName.Visible = isVisible;
            teamTwoScoreLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;
            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
        }
        private void LoadMatchup()
        {
            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                        teamTwoName.Text = "<bye>";
                        teamTwoScoreValue.Text = "0";
                    }
                    else
                    {
                        teamOneName.Text = "Not yet set";
                        teamOneScoreValue.Text = "";
                    }
                }
                if (i == 1)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamTwoName.Text = m.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoName.Text = "Not yet set";
                        teamTwoScoreValue.Text = "";
                    }
                }
            }
        }
        private string ValidateData()
        {
            string output = "";
            double teamOneScore = 0;
            double teamTwoScore = 0;
            bool scoreOneValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
            bool scoreTwoValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);

            if (!scoreOneValid)
            {
                output = "The score 1 value is not a valid number";
            }
            else if (!scoreTwoValid)
            {
                output = "The score 2 value is not a valid number";
            }
            else if (teamOneScore == 0 && teamTwoScore == 0)
            {
                output = "You did not enter a score for either team";
            }
            else if (teamOneScore == teamTwoScore)
            {
                output = "We do not allow tie in this application";
            }

            return output;
        }
        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup();
        }

        private void unplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            string errorMessage = ValidateData();

            if (errorMessage.Length > 0)
            {
                MessageBox.Show($"Error message: {errorMessage}");
                return;
            }
            {
                MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
                double teamOneScore = 0;
                double teamTwoScore = 0;

                for (int i = 0; i < m.Entries.Count; i++)
                {
                    if (i == 0)
                    {
                        if (m.Entries[0].TeamCompeting != null)
                        {
                            teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                            bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
                            if (scoreValid)
                            {
                                m.Entries[0].Score = teamOneScore;
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid score for Team 1");
                                return;
                            }

                        }
                    }
                    if (i == 1)
                    {
                        if (m.Entries[0].TeamCompeting != null)
                        {
                            bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);
                            if (scoreValid)
                            {
                                m.Entries[1].Score = teamTwoScore;
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid score for Team 1");
                                return;
                            }
                        }
                    }
                }

                try
                {
                    TournamentLogic.UpdateTournamentResult(_tournament);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"The Application had the following error: {ex.Message}");
                }
                LoadMatchups();
                GlobalConfig.Connection.UpdateMatchup(m);
            }
        }
    }
}