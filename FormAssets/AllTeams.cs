using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstForm.FormAssets
{
    class AllTeams
    {
        public IDictionary<string, TeamClass> AllTeamsDictionary { set; get; } 

        public AllTeams(IList<TeamClass> _AllTeamsList, IDictionary<string, TeamClass> _AllTeamsDictionary)
        {
            AllTeamsDictionary = _AllTeamsDictionary;
        }

     //   public AllTeams2(IDictionary<string, TeamClass> _AllTeamsDictionary)
     //   {
     //       AllTeamsDictionary = _AllTeamsDictionary;
     //   }


        // The Team Class is created here. This block of code contains the 8 teams
        public void createAllTeamData()
        {
            List<string> tempTestList = new List<string>()
                {"Jets","Sharks","Nerds","Moms","Surfers","Blues","Petes","Pirates"};

            int numTeams = tempTestList.Count;
            for (int i = 0; i < numTeams; i++)
            {
                string teamName = tempTestList[i];
                TeamClass newTeam = new TeamClass(i, teamName, 0, 0, 0, 0, 0);

                this.AllTeamsDictionary.Add(newTeam.teamName.ToString(), newTeam);
            }
        }

        public IList<string> GetListOfTeamNames()
        {
            IList<string> listOfNames = new List<string>();

            foreach (KeyValuePair<string, TeamClass> entry in this.AllTeamsDictionary)
            {
                listOfNames.Add(entry.Key.ToString());
            }
            return listOfNames;
        }


        public IList<string> ShuffuleTeamList()
        {
            Random rand = new Random();
            IList<string> shuffled1 = GetListOfTeamNames();
            IList<string> shuffled2 = shuffled1.OrderBy(_ => rand.Next()).ToList();
            IList<string> shuffled3 = shuffled2.OrderBy(i => Guid.NewGuid()).ToList();
            return (IList<string>)shuffled3;
        }


        public void IncreasePointsFor(string teamName, int amount)
        {
            this.AllTeamsDictionary[teamName.ToString()].pointsFor += amount;
        }

        public void IncreasePointsAgainst(string teamName, int amount)
        {
            this.AllTeamsDictionary[teamName.ToString()].pointsAgainst += amount;
        }

        public void IncreaseLeaguePoints(string teamName, int amount)
        {
            this.AllTeamsDictionary[teamName.ToString()].leagueScore += amount;
        }

        public void CalculatePercentage(string teamName)
        {
            float pointsFor = this.AllTeamsDictionary[teamName.ToString()].pointsFor;
            float pointsAgainst = this.AllTeamsDictionary[teamName.ToString()].pointsAgainst;

            decimal result = (decimal)(pointsFor / pointsAgainst);

            this.AllTeamsDictionary[teamName.ToString()].pointsPercentage = Math.Round(result,2);
        }
    }

}
