
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

using Plannifier.Components;

namespace Plannifier
{
    public class Reports
    {
        Workspace TheWorkspace;

        /// <summary>
        /// 
        /// </summary>
        public void ReportWorkLoad()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += pd_PrintPage;

            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            printPrvDlg.Document = pd;
            printPrvDlg.ShowDialog();

            /*
            PrintDialog pdi = new PrintDialog();
            pdi.Document = pd;

            if (pdi.ShowDialog() == DialogResult.OK)
            {
                // pd.Print();
            }
             * */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        public Reports(Workspace AWorkspace)
        {
            TheWorkspace = AWorkspace;
        }
    }
}
