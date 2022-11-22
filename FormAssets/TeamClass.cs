using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstForm.FormAssets
{
    class TeamClass
    {
        public int teamNumber { set; get; }
        public string teamName { set; get; }
        public int pointsFor { set; get; }
        public int pointsAgainst { set; get; }
        public decimal pointsPercentage { set; get; }
        public int leagueScore { set; get; }
        public int leaguePosition { set; get; }

        // Create a class constructor with multiple parameters
        public TeamClass(int _teamNumber, string _teamName, int _pointsFor, int _pointsAgainst, decimal _pointsPercentage, int _leagueScore, int _leaguePosition)
        {
            this.teamNumber = _teamNumber;
            this.teamName = _teamName;
            this.pointsFor = _pointsFor;
            this.pointsAgainst = _pointsAgainst;
            this.pointsPercentage = _pointsPercentage;
            this.leagueScore = _leagueScore;
            this.leaguePosition = _leaguePosition;
        }
    }
}
