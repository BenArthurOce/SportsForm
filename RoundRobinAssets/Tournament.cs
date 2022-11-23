using MyFirstForm.FormAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstForm.RoundRobinAssets
{
    class Tournament
    {
        public int NumberOfRounds { get; set; }
        public List<Round> ListOfRounds { get; set; }
        public List<Team> ListOfTeams { get; set; }
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
            this.NumberOfRounds = 7;
            this.ListOfRounds = new List<Round>();
            this.ListOfTeams = new List<Team>();
          //  this.Schedule = TournamentScheduler();
        }


        public void createAllTeamData()
        {
            List<string> tempTestList = new List<string>()
                {"Jets","Sharks","Nerds","Moms","Surfers","Blues","Petes","Pirates"};

            int numTeams = tempTestList.Count;
            for (int i = 0; i < numTeams; i++)
            {
                string teamName = tempTestList[i];
                Team newTeam = new Team(i, teamName, 0, 0, 0, 0);

                this.ListOfTeams.Add(newTeam);
            }
        }


        public List<Team> ShuffuleTeamList()
        {
            Random rand = new Random();
            List<Team> shuffled1 = this.ListOfTeams;
            List<Team> shuffled2 = shuffled1.OrderBy(_ => rand.Next()).ToList();
            return shuffled2;
        }

        public void TournamentScheduler()
        {
            for (int i = 0; i < this.NumberOfRounds; i++)
            {

                // we get the list
                // we shuffle the list
                List<Team> ShuffledList = ShuffuleTeamList();

                // get a round and add it
                Round NewRound = new Round(i);
                ListOfRounds.Add(NewRound);

                // In the round we add a match
                Match NewMatch1 = new Match(ShuffledList[0], ShuffledList[1]);
                NewRound.ListOfMatches.Add(NewMatch1);

                Match NewMatch2 = new Match(ShuffledList[2], ShuffledList[3]);
                NewRound.ListOfMatches.Add(NewMatch2);

                Match NewMatch3 = new Match(ShuffledList[4], ShuffledList[5]);
                NewRound.ListOfMatches.Add(NewMatch3);

                Match NewMatch4 = new Match(ShuffledList[6], ShuffledList[7]);
                NewRound.ListOfMatches.Add(NewMatch4);

            }





        }

    }
}
