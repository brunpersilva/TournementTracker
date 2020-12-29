using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        //Order list randomly of teams
        //check if big enough - if not, add in byes 
        //create our first round of matchup
        //create every round after that - 8, 4, 2, 1 matchup rounds

        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamModel(model.EnteredTeam);
            int rounds = FindNumberofRounds(randomizedTeams.Count);
            int byes = NumberofByes(rounds, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));

            CreateOtherRounds(model, rounds);
        }

        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach(MatchupModel match in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;

                currentRound = new List<MatchupModel>();
                round += 1;  
            } 
        }

        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel currentmodel = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                currentmodel.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (byes > 0 || currentmodel.Entries.Count > 1)
                {
                    currentmodel.MatchupRound = 1;
                    output.Add(currentmodel);
                    currentmodel = new MatchupModel();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }
            }
            return output;
        }


        private static int NumberofByes(int rounds, int numberofteams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            output = totalTeams - numberofteams;
            return output;
        }

        private static int FindNumberofRounds(int teamcount)
        {
            int output = 1;
            int val = 2;

            while (val < teamcount)
            {
                output += 1;
                val *= 2;
            }

            return output;

        }

        private static List<TeamModel> RandomizeTeamModel(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
