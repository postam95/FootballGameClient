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

namespace GameClientNamespace
{
    public partial class Form1 : Form
    {
        Socket comm = new Socket();
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
 
        public Form1()
        {
            InitializeComponent();
            pictureField.Controls.Add(pictureHomePlayer1);
            pictureField.Controls.Add(pcictureHomeGoalKeeper);
            pictureField.Controls.Add(pictureAwayPlayer1);
            pictureField.Controls.Add(pictureAwayGoalKeeper);
            pictureField.Controls.Add(pictureBall);
            pictureField.Controls.Add(player1Name);
            pictureField.Controls.Add(player2Name);
            pictureField.Controls.Add(player1Value);
            pictureField.Controls.Add(player2Value);
            pictureField.Controls.Add(label3);


            timer1.Interval = 25;
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           if (GameState.gameActualState == 1)
            {
                label2.Text = "Várakozás a második játékosra...";
                label2.ForeColor = Color.White;
                textBoxName.Enabled = false;
                buttonConnect.Enabled = false;
                this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.L && GameState.message)
            {
                GameState.message = false;
                comm.SendMessage("FEL");
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (comm.ConnectToTcpServer())
            {
                comm.SendMessage("Name:" + textBoxName.Text);
            }
            else
            {
                label2.Text = "Hiba a csatlakozásban";
                label2.ForeColor = Color.Red;
            }
        }
    }
}
