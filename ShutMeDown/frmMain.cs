using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutMeDown
{
    public partial class frmMain : Form
    {
        // Counters 
        public int totalSeconds = 0;
        public int timePassed = 0; 
        public frmMain()
        {
            InitializeComponent();
            button2.Enabled = false; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // start button 

            // dummy check 
            if(numTime.Value <= 0)
            {
                MessageBox.Show("Invalid time, please enter a time greater then 0"); 
            }
            else
            {
                // valid time to count down
                // convert to minutes to seconds to ms 
                timePassed = 0; 
                totalSeconds = (int)numTime.Value * (1000 * 60);

                // start timer 
                tmr.Start();

                // swap buttons 
                button1.Enabled = false;
                button2.Enabled = true; 
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            // incrase by 1 second -> 1000 ms 
            timePassed += 1000;

            // calculate time left for label 
            int minLeft = ((totalSeconds - timePassed) / 1000) / 60;
            int secLeft = ((totalSeconds - timePassed)  / 1000) % 60;

            // update lable text 
            lblRemain.Text = string.Format("{0:00}:{1:00} (MM:SS)", minLeft, secLeft); 

            // If time is up...   
            if(minLeft == 0 && secLeft == 0)
            {
                // send forced shutdown command 
                Process.Start("shutdown", "/s /f /t 0");

                // stop timer to stop command from being sent multiple times 
                tmr.Stop(); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // cancel button 

            // cancel timer 
            tmr.Stop();

            // swap buttons 
            button1.Enabled = true;
            button2.Enabled = false; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numTime_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick(); 
            }
        }
    }
}
