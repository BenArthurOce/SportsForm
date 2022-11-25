using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstForm.RoundRobinAssets

{    class Team
    {
        public int RefNumber { set; get; }
        public string TeamName { set; get; }
        public int PointsFor { set; get; }
        public int PointsAgainst { set; get; }
        public decimal PointsPercentage { set; get; }
        public int LeagueScore { set; get; }

        // Create a class constructor with multiple parameters
        public Team(int _refNumber, string _teamName, int _pointsFor, int _pointsAgainst, decimal _pointsPercentage, int _leagueScore)
        {
            this.RefNumber = _refNumber;
            this.TeamName = _teamName;
            this.PointsFor = _pointsFor;
            this.PointsAgainst = _pointsAgainst;
            this.PointsPercentage = _pointsPercentage;
            this.LeagueScore = _leagueScore;
        }
    }
}

