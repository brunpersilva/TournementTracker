using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.Connections.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            return $@"{ConfigurationManager.AppSettings["filePath"]}\{fileName} ";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');
                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }
                output.Add(t);

            }
            return output;
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);

                output.Add(p);
            }

            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAdress = cols[3];
                p.CellPhoneNumber = cols[4];

                output.Add(p);
            }

            return output;
        }
        public static List<TournamentModel> ConvertToTournamentModel(
            this List<string> lines,
            string teamFileName,
            string peopleFileName,
            string prizesFileName)
        {
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = teamFileName.FullFilePath().LoadFile().ConvertToTeamModels(peopleFileName);
            List<PrizeModel> prizes = prizesFileName.FullFilePath().LoadFile().ConvertToPrizeModels();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TournamentModel tm = new TournamentModel();
                tm.Id = int.Parse(cols[0]);
                tm.TournamentName = cols[1];
                tm.EntryFee = decimal.Parse(cols[2]);
                string[] teamIds = cols[3].Split('|');

                foreach (string id in teamIds)
                {
                    tm.EnteredTeam.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }

                string[] prizesId = cols[4].Split('|');
                foreach (string id in prizesId)
                {
                    tm.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }

                //Todo - Capture rounds information

                output.Add(tm);

            }
            return output;
        }
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SavesToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAdress},{p.CellPhoneNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id },{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTournamentFiles(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel tm in models)
            {
                lines.Add($@"{tm.Id},
                    {tm.TournamentName},
                    {tm.EntryFee},
                    {convertTeamListToString(tm.EnteredTeam)},
                    {convertPrizeListToString(tm.Prizes)},
                    {convertRoundListToString(tm.Rounds)}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
        private static string convertRoundListToString(List<List<MatchupModel>> rounds)
        {
            string output = "";
            if (rounds.Count == 0)
            {
                return "";
            }
            foreach (List<MatchupModel> r in rounds)
            {
                output += $"{ConvertMatchupListToString(r)}|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        private static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            string output = "";
            if (matchups.Count == 0)
            {
                return "";
            }
            foreach (MatchupModel m in matchups)
            { 
                output += $"{m.Id}^";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        private static string convertPrizeListToString(List<PrizeModel> prizes)
        {
            string output = "";
            if (prizes.Count == 0)
            {
                return "";
            }
            foreach (PrizeModel p in prizes)
            {
                output += $"{p.Id}|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        private static string convertTeamListToString(List<TeamModel> teams)
        {
            string output = "";
            if (teams.Count == 0)
            {
                return "";
            }
            foreach (TeamModel t in teams)
            {
                output += $"{t.Id}|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            if (people.Count == 0)
            {
                return "";
            }
            foreach (PersonModel p in people)
            {
                output += $"{p.Id}|";
            }

            output = output.Substring(0, output.Length - 1);
            return output;
        }

    }
}