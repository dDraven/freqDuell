using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using SharpDX.IO;
using NAudio;
using NAudio.Wave;
using System.Net.Sockets;


namespace Family_Duell
{
    public partial class Form1 : Form
    {

        System.Threading.Timer blinkTimer;
        System.Threading.Timer blinkEndTimer;
        bool timerIsRunning = false;

        delegate void RefreshResults(string text);
        delegate void delClear();
        public Teams activeTeam;

        public Dictionary<string, string> allResults;

        int TeamLeftErrors = 0;
        int TeamRightErrors = 0;

        BrandonPotter.XBox.XBoxController controller1;
        BrandonPotter.XBox.XBoxController controller2;

        System.Threading.Timer controller1Timer;
        System.Threading.Timer controller2Timer;



        string pathErrorSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\falsch.mp3";
        string pathFillLineSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\richtig.mp3";
        string pathFillNumberSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\richtig_zahl.mp3";

        public enum Teams
        {
            Left,
            Right
        }

        public Form1(string leftTeamName, string rightTeamName)
        {
            InitializeComponent();
            S.client.eventRefreshResults += client_eventRefreshResults;

            LeftTeamName.Text = leftTeamName;
            RightTeamName.Text = rightTeamName;
            SetColors();




            //default
            fillResultDictionary("hallo:10;test:5;blablabla:3");

            ConnectController();
        }

        public void client_eventRefreshResults(string text)
        {
            fillResultDictionary(text);


        }

        public void ConnectController()
        {

            BrandonPotter.XBox.XBoxControllerWatcher watcher = new BrandonPotter.XBox.XBoxControllerWatcher();
            watcher.ControllerConnected += watcher_ControllerConnected;
            watcher.ControllerDisconnected += watcher_ControllerDisconnected;
            
        }

        public void watcher_ControllerConnected(BrandonPotter.XBox.XBoxController controller)
        {
            if (controller1 == null)
            {
                controller1 = controller;
                controller1Timer = new System.Threading.Timer(Controller1Check, null, 0, 100);
                
            }
            else if (controller2 == null)
            {
                controller2 = controller;
                controller2Timer = new System.Threading.Timer(Controller2Check, null, 0, 100);
            }
        }

        public void Controller1Check(object test)
        {
            if (controller1.ButtonAPressed)
            {
                Buzzer(Teams.Left);
            }
        }

        public void Controller2Check(object test)
        {
            if (controller2.ButtonAPressed)
            {
                Buzzer(Teams.Right);
            }
        }

        public void watcher_ControllerDisconnected(BrandonPotter.XBox.XBoxController controller)
        {
            if (controller1 == controller)
            {
                controller1 = null;
            }
            else if (controller2 == controller)
            {
                controller2 = null;
            }
        }

        public void SetColors()
        {
            this.BackColor = S.backcolor;
            tbl_Ergebnisse.BackColor = S.backcolor;
            LeftTeamName.BackColor = S.backcolor;
            LeftSide.BackColor = S.backcolor;

            RightTeamName.BackColor = S.backcolor;
            RightSide.BackColor = S.backcolor;

            textBox1.BackColor = S.rowcolor;
            textBox2.BackColor = S.rowcolor;
            textBox3.BackColor = S.rowcolor;
            textBox4.BackColor = S.rowcolor;
            textBox5.BackColor = S.rowcolor;
            textBox6.BackColor = S.rowcolor;
            textBox7.BackColor = S.rowcolor;
            textBox8.BackColor = S.rowcolor;
            textBox9.BackColor = S.rowcolor;
            textBox10.BackColor = S.rowcolor;
            textBox11.BackColor = S.rowcolor;
            textBox12.BackColor = S.rowcolor;
        }

        public void ClearAll()
        {
            if (this.InvokeRequired)
            {
                delClear d = new delClear(ClearAll);
                this.Invoke(d);
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox11.Clear();
                textBox12.Clear();


                LeftBox1.Image = null;
                LeftBox2.Image = null;
                LeftBox3.Image = null;

                RightBox1.Image = null;
                RightBox2.Image = null;
                RightBox3.Image = null;
            }
        }

        public void fillResultDictionary(string serverResultString)
        {
            string aaa = serverResultString;

            if (aaa.Contains("!"))
            {
                aaa = serverResultString.Split('!')[1];
            }
            ClearAll();

            allResults = new Dictionary<string, string>();
            string[] rows = aaa.Split(';');
            foreach (string singleRow in rows)
            {
                string[] nameAndAmount = singleRow.Split(':');
                if (nameAndAmount.Count() != 2)
                {
                    continue;
                }
                allResults.Add(nameAndAmount[0], nameAndAmount[1]);

            }
        }



        public void Buzzer(Teams buzzedTeam)
        {
            // Buzzed Sound
            if (timerIsRunning == false)
            {
                timerIsRunning = true;

                activeTeam = buzzedTeam;
                Blink();
            }

            
        }


        public void Blink()
        {
            blinkTimer = new System.Threading.Timer(Blinking, null, 0, 400);
            blinkEndTimer = new System.Threading.Timer(BlinkEnd, null, 5000, Timeout.Infinite);
        }

        public void BlinkEnd(object test)
        {
            blinkTimer.Dispose();
            timerIsRunning = false;

            if (activeTeam == Teams.Left)
            {
                LeftSide.BackColor = S.backcolor;
            }
            else
            {
                RightSide.BackColor = S.backcolor;
            }

        }

        public void Blinking(object test)
        {
            TableLayoutPanel blinkObject;

            if (activeTeam == Teams.Left)
            {
                blinkObject = LeftSide;

                if (blinkObject.BackColor == S.backcolor)
                {
                    blinkObject.BackColor = S.leftcolor;
                }
                else
                {
                    blinkObject.BackColor = S.backcolor;
                }
            }
            else
            {
                blinkObject = RightSide;

                if (blinkObject.BackColor == S.backcolor)
                {
                    blinkObject.BackColor = S.rightcolor;
                }
                else
                {
                    blinkObject.BackColor = S.backcolor;
                }
            }


        }

        public void SoundFillText()
        {
            WaveStream fillSound = new Mp3FileReader(pathFillLineSound);
            var waveOut = new WaveOut();
            waveOut.Init(fillSound);
            waveOut.Play();
        }

        public void SoundFillNumber()
        {
            WaveStream fillNumberSound = new Mp3FileReader(pathFillNumberSound);
            var waveOut = new WaveOut();
            waveOut.Init(fillNumberSound);
            waveOut.Play();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.D1))
            {
                if (textBox1.Text == allResults.Keys.ElementAt(0))
                {
                    SoundFillNumber();
                    textBox2.Text = allResults.Values.ElementAt(0);
                }
                else
                {

                    SoundFillText();
                    textBox1.Text = allResults.Keys.ElementAt(0);
                }
            }

            else if (keyData == (Keys.D2))
            {
                if (textBox3.Text == allResults.Keys.ElementAt(1))
                {
                    SoundFillNumber();
                    textBox4.Text = allResults.Values.ElementAt(1);
                }
                else
                {

                    SoundFillText();
                    textBox3.Text = allResults.Keys.ElementAt(1);
                }
            }
            else if (keyData == (Keys.D3))
            {
                if (textBox5.Text == allResults.Keys.ElementAt(2))
                {
                    SoundFillNumber();
                    textBox6.Text = allResults.Values.ElementAt(2);
                }
                else
                {

                    SoundFillText();
                    textBox5.Text = allResults.Keys.ElementAt(2);
                }
            }
            else if (keyData == (Keys.D4))
            {
                if (textBox7.Text == allResults.Keys.ElementAt(3))
                {
                    SoundFillNumber();
                    textBox8.Text = allResults.Values.ElementAt(3);
                }
                else
                {

                    SoundFillText();
                    textBox7.Text = allResults.Keys.ElementAt(3);
                }
            }
            else if (keyData == (Keys.D5))
            {
                if (textBox9.Text == allResults.Keys.ElementAt(4))
                {
                    SoundFillNumber();
                    textBox10.Text = allResults.Values.ElementAt(4);
                }
                else
                {

                    SoundFillText();
                    textBox9.Text = allResults.Keys.ElementAt(4);
                }
            }
            else if (keyData == (Keys.D6))
            {
                if (textBox11.Text == allResults.Keys.ElementAt(5))
                {
                    SoundFillNumber();
                    textBox12.Text = allResults.Values.ElementAt(5);
                }
                else
                {

                    SoundFillText();
                    textBox11.Text = allResults.Keys.ElementAt(5);
                }
            }
            else if (keyData == (Keys.D0))
            {
                WaveStream errorSound = new Mp3FileReader(pathErrorSound);
                var waveOut = new WaveOut();
                waveOut.Init(errorSound);
                waveOut.Play();

                NextError();
            }



            else if (keyData == (Keys.Y))
            {
                Buzzer(Teams.Left);
            }
            else if (keyData == (Keys.Return))
            {
                Buzzer(Teams.Right);
            }

            else if (keyData == (Keys.Left))
            {
                activeTeam = Teams.Left;
                int number1 = 0;
                int number2 = 0;
                int number3 = 0;
                int number4 = 0;
                int number5 = 0;

                int.TryParse(textBox2.Text, out number1);
                int.TryParse(textBox4.Text, out number2);
                int.TryParse(textBox6.Text, out number3);
                int.TryParse(textBox8.Text, out number4);
                int.TryParse(textBox10.Text, out number5);

                int current = 0;
                int.TryParse(LPunkte.Text, out current);


                int LSum = number1 + number2 + number3 + number4 + number5;
                current += LSum;

                LPunkte.Text = current.ToString();
            }
            else if (keyData == (Keys.Right))
            {
                activeTeam = Teams.Right;

                int number1 = 0;
                int number2 = 0;
                int number3 = 0;
                int number4 = 0;
                int number5 = 0;

                int.TryParse(textBox2.Text, out number1);
                int.TryParse(textBox4.Text, out number2);
                int.TryParse(textBox6.Text, out number3);
                int.TryParse(textBox8.Text, out number4);
                int.TryParse(textBox10.Text, out number5);

                int current = 0;
                int.TryParse(RPunkte.Text, out current);

                int RSum = number1 + number2 + number3 + number4 + number5;
                current += RSum;

                RPunkte.Text = current.ToString();

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void NextError()
        {
            if (activeTeam == Teams.Left)
            {
                TeamLeftErrors += 1;

                if (TeamLeftErrors == 1)
                {
                    LeftBox1.Image = Family_Duell.Properties.Resources.RotesX;
                }
                else if (TeamLeftErrors == 2)
                {
                    LeftBox2.Image = Family_Duell.Properties.Resources.RotesX;
                }
                else if (TeamLeftErrors == 3)
                {
                    LeftBox3.Image = Family_Duell.Properties.Resources.RotesX;
                    activeTeam = Teams.Right;
                }
            }
            else
            {
                TeamRightErrors += 1;

                if (TeamRightErrors == 1)
                {
                    RightBox1.Image = Family_Duell.Properties.Resources.RotesX;
                }
                else if (TeamRightErrors == 2)
                {
                    RightBox2.Image = Family_Duell.Properties.Resources.RotesX;
                }
                 else if (TeamRightErrors == 3)
                {
                    RightBox3.Image = Family_Duell.Properties.Resources.RotesX;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            S.client.eventRefreshResults -= client_eventRefreshResults;
        }
    }
}
