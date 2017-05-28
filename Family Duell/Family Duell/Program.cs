using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Family_Duell
{
    static class Program
    {
        
        static Form1 gameForm;
        static Finale finale;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Start startWindow = new Start();
            DialogResult result = startWindow.ShowDialog();
            

            if (result == DialogResult.OK)
            {
                string leftTeamName = startWindow.Team1Name;
                string rightTeamName = startWindow.Team2Name;

                startWindow.Close();
                S.client = new TcpConnect();

                gameForm = new Form1(leftTeamName, rightTeamName);
                gameForm.ShowDialog();

                finale = new Finale(leftTeamName, rightTeamName);
                finale.ShowDialog();

            }
        }

        

    }
}
