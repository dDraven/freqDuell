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
    public partial class Start : Form
    {
        public string Team1Name = string.Empty;
        public string Team2Name = string.Empty;

        WaveOut outAudio;

        string pathIntroSound = @"C:\Users\Dave\MasterarbeitWorkspace\TCPSockets\testClientVisualStudio\Family Duell\Family Duell\Musik\Familien Duell Intromusik.mp3";

        public Start()
        {
            InitializeComponent();

            Mp3FileReader fillSound = new Mp3FileReader(pathIntroSound);
            outAudio = new WaveOut();
            outAudio.Init(fillSound);
            outAudio.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (outAudio != null)
            {
                outAudio.Stop();
            }
            Team1Name = textBox1.Text;
            Team2Name = textBox2.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
