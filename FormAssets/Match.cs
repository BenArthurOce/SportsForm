using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstForm.FormAssets
{
    class MatchClass
    {
        public List<RadioButton> RadioButtons { get; set; }
        public List<NumericUpDown> SpinBoxes { get; set; }
        public string Team1Name;
        public bool Team1Checked;
        public int Team1Score;
        public string Team2Name;
        public bool Team2Checked;
        public int Team2Score;
        public string WinTeamName;
        public int WinTeamScore;
        public string LoseTeamName;
        public int LoseTeamScore;
        public bool IsMatchValid;
        public string ReasonForInvalid;

        // Create a class constructor with multiple parameters
        public MatchClass(List<RadioButton> _RadioButtons, List<NumericUpDown> _SpinBoxes)
        {
            this.Team1Name          = _RadioButtons[0].Text;
            this.Team1Checked       = _RadioButtons[0].Checked;
            this.Team1Score         = (int)_SpinBoxes[0].Value;
            this.Team2Name          = _RadioButtons[1].Text;
            this.Team2Checked       = _RadioButtons[1].Checked;
            this.Team2Score         = (int)_SpinBoxes[1].Value;
            this.WinTeamName        = "";
            this.WinTeamScore       = 0;
            this.LoseTeamName       = "";
            this.LoseTeamScore      = 0;
            this.IsMatchValid       = false;
            this.ReasonForInvalid = "";
        }

        public bool IsRoundValid()
        {
            // Both Teams in Match cannot have unchecked radioButtons
            if (this.Team1Checked == false && this.Team2Checked == false) 
            {
                this.IsMatchValid = false;
                this.ReasonForInvalid = "A team winner must be selected";
                return false; 
            }

            // Both teams cannot have the same score
            if (this.Team1Score == this.Team2Score)
            {
                this.IsMatchValid = false;
                this.ReasonForInvalid = "Both teams cannot have the same score";
                return false;
            }

            // Both teams have nil score
            if (this.Team1Score == 0 && this.Team2Score == 0)
            {
                this.IsMatchValid = false;
                this.ReasonForInvalid = "Both teams cannot have nil score";
                return false;
            }

            // The winning team1 must have a higher score
            if (this.Team1Checked == true && Team1Score < Team2Score)
            {
                this.IsMatchValid = false;
                this.ReasonForInvalid = "Team 1 is delcared winner, but has lesser score";
                return false;
            }

            // The winning team2 must have a higher score
            if (this.Team2Checked == true && Team2Score < Team1Score)
            {
                this.IsMatchValid = false;
                this.ReasonForInvalid = "Team 2 is delcared winner, but has lesser score";
                return false;
            }

            // If no errors, the match is valid
            this.IsMatchValid = true;
            return true;
        }

        public void DeclareWinnersLosers()
        {
            if (Team1Score > Team2Score)
            {
                this.WinTeamName = this.Team1Name;
                this.WinTeamScore = this.Team1Score;
                this.LoseTeamName = this.Team2Name;
                this.LoseTeamScore = this.Team2Score;
            }
            if (Team2Score > Team1Score)
            {
                this.WinTeamName = this.Team2Name;
                this.WinTeamScore = this.Team2Score;
                this.LoseTeamName = this.Team1Name;
                this.LoseTeamScore = this.Team1Score;
            }
        }
    }
}
