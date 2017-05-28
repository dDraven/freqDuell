using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Family_Duell
{
    public partial class Finale : Form
    {
        public Dictionary<string, string> allResults;
        string pathFinalErrorSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\Final Fail.mp3";
        string pathErrorSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\falsch.mp3";
        string pathFillLineSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\richtig.mp3";
        string pathFillNumberSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\richtig_zahl.mp3";
        
        
        public enum Teams
        {
            Left,
            Right
        }
        public Teams activeTeam;


        string leftvalue1 = "default1";
        string leftnumber1 = "1";

        string leftvalue2 = "default2";
        string leftnumber2 = "2";

        string leftvalue3 = "default3";
        string leftnumber3 = "0";

        string leftvalue4;
        string leftnumber4;

        string leftvalue5;
        string leftnumber5;


        string rightvalue1;
        string rightnumber1;

        string rightvalue2;
        string rightnumber2;

        string rightvalue3;
        string rightnumber3;

        string rightvalue4;
        string rightnumber4;

        string rightvalue5;
        string rightnumber5;

        public Finale(string leftTeam, string rightTeam)
        {
            InitializeComponent();
            S.client.eventRefreshResults += client_eventRefreshResults;
            tableLayoutPanel1.BackColor = S.backcolor;
        }

        public void client_eventRefreshResults(string text)
        {
            fillResultDictionary(text);


        }

        public void fillResultDictionary(string serverResultString)
        {
            if (serverResultString.ToLower().Contains("Frage1!".ToLower()))
            {
                string result = serverResultString.Split('!')[1];

                string[] valueAndAmount = result.Split(':');

                if (activeTeam == Teams.Left)
                {
                    leftvalue1 = valueAndAmount[0];
                    leftnumber1 = valueAndAmount[1];
                }
                else
                {
                    rightvalue1 = valueAndAmount[0];
                    rightnumber1 = valueAndAmount[1];
                }
                
            }
            else if (serverResultString.ToLower().Contains("Frage2!".ToLower()))
            {
                string result = serverResultString.Split('!')[1];

                string[] valueAndAmount = result.Split(':');

                if (activeTeam == Teams.Left)
                {
                    leftvalue2 = valueAndAmount[0];
                    leftnumber2 = valueAndAmount[1];
                }
                else
                {
                    rightvalue2 = valueAndAmount[0];
                    rightnumber2 = valueAndAmount[1];
                }
            }
            else if (serverResultString.ToLower().Contains("Frage3!".ToLower()))
            {
                string result = serverResultString.Split('!')[1];
                
                string[] valueAndAmount = result.Split(':');

                if (activeTeam == Teams.Left)
                {
                    leftvalue3 = valueAndAmount[0];
                    leftnumber3 = valueAndAmount[1];
                }
                else
                {
                    rightvalue3 = valueAndAmount[0];
                    rightnumber3 = valueAndAmount[1];
                }
            }
            else if (serverResultString.ToLower().Contains("Frage4!".ToLower()))
            {
                string result = serverResultString.Split('!')[1];

                string[] valueAndAmount = result.Split(':');

                if (activeTeam == Teams.Left)
                {
                    leftvalue4 = valueAndAmount[0];
                    leftnumber4 = valueAndAmount[1];
                }
                else
                {
                    rightvalue4 = valueAndAmount[0];
                    rightnumber4 = valueAndAmount[1];
                }
            }
            else if (serverResultString.ToLower().Contains("Frage5!".ToLower()))
            {
                string result = serverResultString.Split('!')[1];

                string[] valueAndAmount = result.Split(':');

                if (activeTeam == Teams.Left)
                {
                    leftvalue5 = valueAndAmount[0];
                    leftnumber5 = valueAndAmount[1];
                }
                else
                {
                    rightvalue5 = valueAndAmount[0];
                    rightnumber5 = valueAndAmount[1];
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Right))
            {
                activeTeam = Teams.Right;
            }
            else if (keyData == (Keys.Left))
            {
                activeTeam = Teams.Left;
            }

            else if (keyData == (Keys.D1))
            {
                if (activeTeam == Teams.Left)
                {


                    if (txt_leftvalue1.Text == leftvalue1)
                    {
                        SoundFillNumber(leftnumber1);
                        txt_leftnumber1.Text = leftnumber1;
                    }
                    else
                    {

                        SoundFillText();
                        txt_leftvalue1.Text = leftvalue1;
                    }
                }
                else
                {
                    if (txt_rightvalue1.Text == rightvalue1)
                    {
                        SoundFillNumber(rightnumber1);
                        txt_rightnumber1.Text = rightnumber1;
                    }
                    else
                    {

                        SoundFillText();
                        txt_rightvalue1.Text = rightvalue1;
                    }
                }
            }

            else if (keyData == (Keys.D2))
            {
                if (activeTeam == Teams.Left)
                {


                    if (txt_leftvalue2.Text == leftvalue2)
                    {
                        SoundFillNumber(leftnumber2);
                        txt_leftnumber2.Text = leftnumber2;
                    }
                    else
                    {

                        SoundFillText();
                        txt_leftvalue2.Text = leftvalue2;
                    }
                }
                else
                {
                    if (txt_rightvalue2.Text == rightvalue2)
                    {
                        SoundFillNumber(rightnumber2);
                        txt_rightnumber2.Text = rightnumber2;
                    }
                    else
                    {

                        SoundFillText();
                        txt_rightvalue2.Text = rightvalue2;
                    }
                }
            }
            else if (keyData == (Keys.D3))
            {
                if (activeTeam == Teams.Left)
                {


                    if (txt_leftvalue3.Text == leftvalue3)
                    {
                        SoundFillNumber(leftnumber3);
                        txt_leftnumber3.Text = leftnumber3;
                    }
                    else
                    {

                        SoundFillText();
                        txt_leftvalue3.Text = leftvalue3;
                    }
                }
                else
                {
                    if (txt_rightvalue3.Text == rightvalue3)
                    {
                        SoundFillNumber(rightnumber3);
                        txt_rightnumber3.Text = rightnumber3;
                    }
                    else
                    {

                        SoundFillText();
                        txt_rightvalue3.Text = rightvalue3;
                    }
                }
            }
            else if (keyData == (Keys.D4))
            {
                if (activeTeam == Teams.Left)
                {


                    if (txt_leftvalue4.Text == leftvalue4)
                    {
                        SoundFillNumber(leftnumber4);
                        txt_leftnumber4.Text = leftnumber4;
                    }
                    else
                    {

                        SoundFillText();
                        txt_leftvalue4.Text = leftvalue4;
                    }
                }
                else
                {
                    if (txt_rightvalue4.Text == rightvalue4)
                    {
                        SoundFillNumber(rightnumber4);
                        txt_rightnumber4.Text = rightnumber4;
                    }
                    else
                    {

                        SoundFillText();
                        txt_rightvalue4.Text = rightvalue4;
                    }
                }
            }
            else if (keyData == (Keys.D5))
            {
                if (activeTeam == Teams.Left)
                {


                    if (txt_leftvalue5.Text == leftvalue5)
                    {
                        SoundFillNumber(leftnumber5);
                        txt_leftnumber5.Text = leftnumber5;
                    }
                    else
                    {

                        SoundFillText();
                        txt_leftvalue5.Text = leftvalue5;
                    }
                }
                else
                {
                    if (txt_rightvalue5.Text == rightvalue5)
                    {
                        SoundFillNumber(rightnumber5);
                        txt_rightnumber5.Text = rightnumber5;
                    }
                    else
                    {

                        SoundFillText();
                        txt_rightvalue5.Text = rightvalue5;
                    }
                }
            }


            else if (keyData == (Keys.D0))
            {
                WaveStream errorSound = new Mp3FileReader(pathFinalErrorSound);
                var waveOut = new WaveOut();
                waveOut.Init(errorSound);
                waveOut.Play();
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SoundFillText()
        {
            WaveStream errorSound = new Mp3FileReader(pathFillLineSound);
            var waveOut = new WaveOut();
            waveOut.Init(errorSound);
            waveOut.Play();
        }

        public void SoundFillNumber(string number)
        {
            WaveStream numberSound;
            if (!String.IsNullOrEmpty(number) && number != "0")
            {
                numberSound = new Mp3FileReader(pathFillNumberSound);
            }
            else
            {
                numberSound = new Mp3FileReader(pathErrorSound);
            }

            var waveOut = new WaveOut();
            waveOut.Init(numberSound);
            waveOut.Play();
        }

        private void Points()
        {
            int lnumber1 = 0;
            int lnumber2 = 0;
            int lnumber3 = 0;
            int lnumber4 = 0;
            int lnumber5 = 0;
            int.TryParse(txt_leftnumber1.Text, out lnumber1);
            int.TryParse(txt_leftnumber2.Text, out lnumber2);
            int.TryParse(txt_leftnumber3.Text, out lnumber3);
            int.TryParse(txt_leftnumber4.Text, out lnumber4);
            int.TryParse(txt_leftnumber5.Text, out lnumber5);

            int leftSum = lnumber1 + lnumber2 + lnumber3 + lnumber4 + lnumber5;
            LeftPoints.Text = leftSum.ToString();

            int rnumber1 = 0;
            int rnumber2 = 0;
            int rnumber3 = 0;
            int rnumber4 = 0;
            int rnumber5 = 0;
            int.TryParse(txt_rightnumber1.Text, out rnumber1);
            int.TryParse(txt_rightnumber2.Text, out rnumber2);
            int.TryParse(txt_rightnumber3.Text, out rnumber3);
            int.TryParse(txt_rightnumber4.Text, out rnumber4);
            int.TryParse(txt_rightnumber5.Text, out rnumber5);

            int rightSum = rnumber1 + rnumber2 + rnumber3 + rnumber4 + rnumber5;
            RightPoints.Text = rightSum.ToString();



        }

        private void txt_leftnumber1_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_leftnumber2_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_leftnumber3_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_leftnumber4_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_leftnumber5_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_rightnumber1_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_rightnumber2_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_rightnumber3_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_rightnumber4_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void txt_rightnumber5_TextChanged(object sender, EventArgs e)
        {
            Points();
        }

        private void Finale_FormClosing(object sender, FormClosingEventArgs e)
        {
            S.client.eventRefreshResults -= client_eventRefreshResults;
        }
    }
}
