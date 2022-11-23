using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstForm.RoundRobinAssets

{    class Team
    {
        public int refNumber { set; get; }
        public string teamName { set; get; }
        public int pointsFor { set; get; }
        public int pointsAgainst { set; get; }
        public decimal pointsPercentage { set; get; }
        public int leagueScore { set; get; }

        // Create a class constructor with multiple parameters
        public Team(int _refNumber, string _teamName, int _pointsFor, int _pointsAgainst, decimal _pointsPercentage, int _leagueScore)
        {
            this.refNumber = _refNumber;
            this.teamName = _teamName;
            this.pointsFor = _pointsFor;
            this.pointsAgainst = _pointsAgainst;
            this.pointsPercentage = _pointsPercentage;
            this.leagueScore = _leagueScore;
        }
    }
}

