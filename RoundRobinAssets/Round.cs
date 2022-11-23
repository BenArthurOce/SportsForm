using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstForm.RoundRobinAssets
{
    class Round
    {
        public int RoundNumber { get; set; }
        public List<Match> ListOfMatches { get; set; }
        // Needs to hold all the rounds
        // Needs to Schedule the matches?
        // Needs to store winners and losers

        // Needs to advance round
        // Needs to have a list of team names

        public Round(int _RoundNumber)
        {
            this.RoundNumber = _RoundNumber;
            this.ListOfMatches = new List<Match>();
        }
    }
}