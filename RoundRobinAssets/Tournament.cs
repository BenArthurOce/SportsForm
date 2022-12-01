using MyFirstForm.FormAssets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static MyFirstForm.DataSportsResults;

namespace MyFirstForm.RoundRobinAssets
{
    class Tournament
    {
        public TeamsInLeagueDataTable dtTeamNames;
        public LeagueLadderDataTable dtLeagueLadder;
        public MatchHistoryDataTable dtMatchHistory;

         

        public int CurrentRound { get; set; }
        public int NumberOfRounds { get; set; }
        public List<Round> ListOfRounds { get; set; }
        public List<Team> ListOfTeams { get; set; }

        public Dictionary<(int, int), string> MatchHistory { get; set; }

        //   public int Schedule { get; set; }
        // Needs to hold all the rounds
        // Needs to Schedule the matches?
        // Needs to store winners and losers

        // Needs to advance round
        // Needs to have a list of team names

        // When Active
        //  Needs to Start looping through the rounds
        //      So we're on round 1
        //          Needs to loop through the matches inside round 1
        //      Move to round 2
        //          Loop through all the matches inside round 2

        public Tournament()
        {
            this.CurrentRound = 0;
            this.NumberOfRounds = 7;
            this.ListOfRounds = new List<Round>();
            this.ListOfTeams = new List<Team>();
            this.ListOfTeams = createAllTeamData();
            this.MatchHistory = new Dictionary<(int, int), string>();


            this.dtTeamNames = new DataSportsResults.TeamsInLeagueDataTable();
            this.dtLeagueLadder = new DataSportsResults.LeagueLadderDataTable();
            this.dtMatchHistory = new DataSportsResults.MatchHistoryDataTable();

            TournamentScheduler();
            AddTeamsToDataTable();
            TournamentScheduler2();
        }




        public void IncreaseRoundByOne()
        {
            this.CurrentRound += 1;
        }

        // https://www.youtube.com/watch?v=vmjwqfIm11k
 
        public void AddTeamsToDataTable()
        {
            // dtTeamNames
           // DataSportsResults.TeamsInLeagueDataTable dtc = new DataSportsResults.TeamsInLeagueDataTable();
            
            List<string> TempListOfTeams = new List<string>(){ "Jets", "Sharks", "Nerds", "Moms", "Surfers", "Blues", "Petes", "Pirates" };

            foreach (string TempTeam in TempListOfTeams)
            {
                DataSportsResults.TeamsInLeagueRow dr = dtTeamNames.NewTeamsInLeagueRow();
                dr["TeamName"] = TempTeam;
                dtTeamNames.Rows.Add(dr);
            }

            // Create a List



            /*
            foreach (DataRow r in dtc.Rows)

                Console.WriteLine("Result = {0}", r["TeamName"]);

            Console.Read();
            */
        }





        public List<Team> createAllTeamData()
        {
            List<string> tempTestList = new List<string>()
                {"Jets","Sharks","Nerds","Moms","Surfers","Blues","Petes","Pirates"};

            int numTeams = tempTestList.Count;
            for (int i = 0; i < numTeams; i++)
            {
                string teamName = tempTestList[i];
                this.ListOfTeams.Add(new Team(i, teamName, 0, 0, 0, 0));
            }

            return this.ListOfTeams;
        }

        public IList<Team> ShuffuleTeamList()
        {
            Random rand = new Random();
            IList<Team> shuffled1 = this.ListOfTeams;
            IList<Team> shuffled2 = shuffled1.OrderBy(_ => rand.Next()).ToList();
            IList<Team> shuffled3 = shuffled2.OrderBy(i => Guid.NewGuid()).ToList();
            return (IList<Team>)shuffled3;
        }


        private void TournamentScheduler2()
        {
            // Create List of Teams
            IList<String> ListAllTeams = new List<String>();
            foreach (DataRow r in dtTeamNames.Rows)
            {
                ListAllTeams.Add(r["TeamId"].ToString());
            }


            // match number
            int j = 1;

            for (int i = 1; i < this.NumberOfRounds+1; i++)
            {

                // Shuffle Team List
                Random rand = new Random();
                IList<String> ShuffledTeamList = ListAllTeams.OrderBy(_ => rand.Next()).ToList();

                AddMatchToMatchHistoryDataSet(
                    j, i, 1, ShuffledTeamList[0], 0, ShuffledTeamList[1], 0, false, false, false);
                    j += 1;

                AddMatchToMatchHistoryDataSet(
                    j, i, 2, ShuffledTeamList[2], 0, ShuffledTeamList[3], 0, false, false, false);
                    j += 1;

                AddMatchToMatchHistoryDataSet(
                    j, i, 3, ShuffledTeamList[4], 0, ShuffledTeamList[5], 0, false, false, false);
                    j += 1;

                AddMatchToMatchHistoryDataSet(
                    j, i, 4, ShuffledTeamList[6], 0, ShuffledTeamList[7], 0, false, false, false);
                    j += 1;

            }
        }


        public void AddMatchToMatchHistoryDataSet(int GameID, int RoundNum, int MatchNum, string TeamAName, int TeamAScore, string TeamBName, int TeamBScore, bool IsTeamAWin, bool IsTeamBWin, bool isDraw)
        {
            dtMatchHistory.Rows.Add
                (GameID, RoundNum, MatchNum, TeamAName, TeamAScore, TeamBName, TeamBScore, IsTeamAWin, IsTeamBWin, isDraw);
        }


        public void TournamentScheduler()
        {
            for (int i = -1; i < this.NumberOfRounds; i++)
            {

                this.ListOfTeams = (List<Team>)ShuffuleTeamList();

                // get a round and add it
                Round NewRound = new Round(i);
                ListOfRounds.Add(NewRound);

                // In the round we add a match
                Match NewMatch1 = new Match(this.ListOfTeams[0], this.ListOfTeams[1]);
                NewRound.ListOfMatches.Add(NewMatch1);

                Match NewMatch2 = new Match(this.ListOfTeams[2], this.ListOfTeams[3]);
                NewRound.ListOfMatches.Add(NewMatch2);

                Match NewMatch3 = new Match(this.ListOfTeams[4], this.ListOfTeams[5]);
                NewRound.ListOfMatches.Add(NewMatch3);

                Match NewMatch4 = new Match(this.ListOfTeams[6], this.ListOfTeams[7]);
                NewRound.ListOfMatches.Add(NewMatch4);

            }

        }

    }
}
