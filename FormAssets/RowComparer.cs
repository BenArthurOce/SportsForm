using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstForm.FormAssets
{
    class RowComparer : System.Collections.IComparer
    {
        private static int sortOrderModifier = -1;

        public RowComparer(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

            // Try to sort based on the League Points Column.
            int CompareResult = System.String.Compare(
                DataGridViewRow1.Cells[5].Value.ToString(),
                DataGridViewRow2.Cells[5].Value.ToString());

            // If the League Points are equal, sort based on the Percentage.
            if (CompareResult == 0)
            {
                CompareResult = System.String.Compare(
                    DataGridViewRow1.Cells[4].Value.ToString(),
                    DataGridViewRow2.Cells[4].Value.ToString());
            }
            return CompareResult * sortOrderModifier;

        }

    }

}
