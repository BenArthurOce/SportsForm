using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstForm.RoundRobinAssets
{
    class Match
    {

        public Team TeamA { set; get; }
        public Team TeamB { set; get; }

        public Match(Team _TeamA, Team _TeamB)
        {
            this.TeamA = _TeamA;
            this.TeamB = _TeamB;
        }
    }
}
