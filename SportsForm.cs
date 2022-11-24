using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyFirstForm.FormAssets;
using MyFirstForm.RoundRobinAssets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace MyFirstForm
{
    public partial class SportsForm : Form
    {

        // List that contains information for dictionaries, Lists, Teams
        private AllTeams AllTeamClass = new AllTeams(new List<TeamClass>(), new Dictionary<string, TeamClass>());

        //  private AllTeams AllTeamClass2 = new AllTeams( new Dictionary<string, TeamClass>());

        Tournament tourny = new Tournament();

        // Radiobutton List
        private IList<RadioButton> ListRadioButtons = new List<RadioButton>();

        // Spinbox List
        private IList<NumericUpDown> ListSpinBoxes = new List<NumericUpDown>();

        // Ladder
        private ListViewColumnSorter lvwColumnSorter;

        public SportsForm()
        {
            InitializeComponent();
            btnSubmitScores.Visible = false;

            // All the Data is generated in the AllTeamsClass
            AllTeamClass.createAllTeamData();

            // Create lists of Radiobuttons and NumericUpDowns
            CreateRadioButtonList();
            CreateSpinBoxList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void CreateRadioButtonList()
        {
            ListRadioButtons.Add(btnradioGame1Team1);
            ListRadioButtons.Add(btnradioGame1Team2);
            ListRadioButtons.Add(btnradioGame2Team1);
            ListRadioButtons.Add(btnradioGame2Team2);
            ListRadioButtons.Add(btnradioGame3Team1);
            ListRadioButtons.Add(btnradioGame3Team2);
            ListRadioButtons.Add(btnradioGame4Team1);
            ListRadioButtons.Add(btnradioGame4Team2);
        }

        private void CreateSpinBoxList()
        {
            ListSpinBoxes.Add(spinbxGame1Team1);
            ListSpinBoxes.Add(spinbxGame1Team2);
            ListSpinBoxes.Add(spinbxGame2Team1);
            ListSpinBoxes.Add(spinbxGame2Team2);
            ListSpinBoxes.Add(spinbxGame3Team1);
            ListSpinBoxes.Add(spinbxGame3Team2);
            ListSpinBoxes.Add(spinbxGame4Team1);
            ListSpinBoxes.Add(spinbxGame4Team2);
        }

 
        private void FillRadioButtons()
        {
            var shuffled = AllTeamClass.ShuffuleTeamList();
            int i = 0;
            foreach (var rbtn in ListRadioButtons)
            {
                rbtn.Text = shuffled[i];
                i += 1;
            }
        }

        public void ResetRadioButtons()
        {
            foreach (var rbtn in ListRadioButtons)
            {
                rbtn.Checked = false;
            }
        }

        private void ResetSpinBoxes()
        {
            foreach (var spinBox in ListSpinBoxes)
            {
                spinBox.Value = 0;
            }
        }

        // 
        // Clicking New Round Button
        // 
        private void btnNewRound_Click(object sender, EventArgs e)
        {
            btnSubmitScores.Visible = true;
            ResetRadioButtons();
            ResetSpinBoxes();
            // FillRadioButtons();

            tourny.IncreaseRoundByOne();



            int i = 0;
            i = tourny.CurrentRound;
            // i = tourny.CurrentRound;

            FillFormRadioButtons(i);


        }

        private void FillFormRadioButtons(int i)
        {
            int m = 0;
            foreach (GroupBox grpBox in Controls.OfType<GroupBox>())
            {

                List<RadioButton> ListRadioButtonsMatch = new List<RadioButton>();
                foreach (RadioButton rBtn in grpBox.Controls.OfType<RadioButton>())
                {
                    ListRadioButtonsMatch.Add(rBtn);
                }

                ListRadioButtonsMatch[0].Text = tourny.ListOfRounds[i].ListOfMatches[m].TeamA.teamName.ToString();
                ListRadioButtonsMatch[1].Text = tourny.ListOfRounds[i].ListOfMatches[m].TeamB.teamName.ToString();

                m += 1;
            }
        }

        // 
        // Clicking Submit Scores Button
        // 
        private void btnSubmitScores_Click(object sender, EventArgs e)
        {
            // Check if tickboxes are correct, check if matches are correct
            if (IsFormValid() == false) { return; }

            // Create a List that will store all the match classes
            IList<MatchClass> ListMatchClassList = new List<MatchClass>();

            // Collect all data seperatly withhin each GroupBox and store it in a MatchClass. Store that MatchClass in a List that holds all the matches
            foreach (GroupBox grpBox in Controls.OfType<GroupBox>())
            {
                // Collect information about the teams and scores
                var radioButtons = new List<RadioButton>();
                var spinboxes = new List<NumericUpDown>();

                foreach (Control control in grpBox.Controls)
                {
                    if (control.Name.ToString().StartsWith("btnradio"))
                    {
                        radioButtons.Add((RadioButton)control);
                    }
                    if (control.Name.ToString().StartsWith("spinbx"))
                    {
                        spinboxes.Add((NumericUpDown)control);
                    }
                }
                // Create a Match Class based on the teams and scores data and add to the list
                MatchClass GeneratedMatch = new MatchClass(radioButtons, spinboxes);
                ListMatchClassList.Add(GeneratedMatch);
            }

            // Validate the Matches that are held within the MatchClass List
            foreach (MatchClass GeneratedMatch in ListMatchClassList)
            {
                if (GeneratedMatch.IsRoundValid() == false)
                {
                    MessageBox.Show(GeneratedMatch.ReasonForInvalid);
                    return;
                };
            }

            // Validation Has Gone Through. Remove Score Submit Button and clear Ladder
            btnSubmitScores.Visible = false;
            ClearLadder();

            // Determine the winners and losers
            foreach (MatchClass GeneratedMatch in ListMatchClassList)
            {
                GeneratedMatch.DeclareWinnersLosers();

                // Update the data dictonary for the winning team
                AllTeamClass.IncreaseLeaguePoints(GeneratedMatch.WinTeamName, 4);
                AllTeamClass.IncreasePointsFor(GeneratedMatch.WinTeamName, GeneratedMatch.WinTeamScore);
                AllTeamClass.IncreasePointsAgainst(GeneratedMatch.WinTeamName, GeneratedMatch.LoseTeamScore);
                AllTeamClass.CalculatePercentage(GeneratedMatch.WinTeamName);

                // Update the data dictionary for the losing team
                AllTeamClass.IncreaseLeaguePoints(GeneratedMatch.LoseTeamName, 2);
                AllTeamClass.IncreasePointsFor(GeneratedMatch.LoseTeamName, GeneratedMatch.LoseTeamScore);
                AllTeamClass.IncreasePointsAgainst(GeneratedMatch.LoseTeamName, GeneratedMatch.WinTeamScore);
                AllTeamClass.CalculatePercentage(GeneratedMatch.LoseTeamName);

                // Add both teams to the ladder
                AddToLadder(
                    0
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.WinTeamName].teamName
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.WinTeamName].pointsFor
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.WinTeamName].pointsAgainst
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.WinTeamName].pointsPercentage
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.WinTeamName].leagueScore
                );

                AddToLadder(
                    0
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.LoseTeamName].teamName
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.LoseTeamName].pointsFor
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.LoseTeamName].pointsAgainst
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.LoseTeamName].pointsPercentage
                    , AllTeamClass.AllTeamsDictionary[GeneratedMatch.LoseTeamName].leagueScore
                );
            }
        
        // Organise the ladder by league points, then percentage
        SortLadder();
    }


    // 
    // Clicking Random Scores and Random Winners Button
    // 
    private void btnRandomScores_Click(object sender, EventArgs e)
        {
            ResetSpinBoxes();
            ResetRadioButtons();

            Random rnd = new Random();
            int num = 0;
            foreach (GroupBox grpBox in Controls.OfType<GroupBox>())
            {
                num = rnd.Next(3);
                foreach (RadioButton rBtn in grpBox.Controls.OfType<RadioButton>())
                {
                    if (num == 1) ;
                    { rBtn.Checked = true; }
                }

                foreach (Control spnBox in grpBox.Controls.OfType<NumericUpDown>())
                {
                    num = rnd.Next(60);
                    spnBox.Text = num.ToString();
                }
            }
        }

        // Count if all required radio buttons are checked
        private bool IsFormValid()
        {
            int numberChecked = 0;
            foreach (var rbtn in ListRadioButtons)
            {
                if (rbtn.Checked == true) { numberChecked += 1; }
            }
            return numberChecked == 4;
        }








        // Sorts the ladder - Should be its own class?
        private void SortLadder()
        {
            gridViewLadder.Sort(new RowComparer(SortOrder.Descending));
        }

        //https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-customize-sorting-in-the-windows-forms-datagridview-control?view=netframeworkdesktop-4.8


        // Adds to the ladder - Should be its own class?
        private void AddToLadder(int Position, string TeamName, int PointsFor, int PointsAgainst, decimal Percentage, int LeaguePoints)
        {
            int n = gridViewLadder.Rows.Add();
            gridViewLadder.Rows[n].Cells[0].Value = Position;           //position
            gridViewLadder.Rows[n].Cells[1].Value = TeamName;           //name
            gridViewLadder.Rows[n].Cells[2].Value = PointsFor;          //PointsFor
            gridViewLadder.Rows[n].Cells[3].Value = PointsAgainst;      //PointsAgainst
            gridViewLadder.Rows[n].Cells[4].Value = Percentage;         //Percentage
            gridViewLadder.Rows[n].Cells[5].Value = LeaguePoints;       //LeaguePoints
        }

        // Clears the Ladder
        private void ClearLadder()
        {
            gridViewLadder.Rows.Clear();
        }




        private void column_click(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
        }
    }
}