using GameClient;
using Newtonsoft.Json;
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
        GameState gameState = SingletonGameState.GetInstance().GetGameState();
        UserCommand userCommand = SingletonGameState.GetInstance().GetUserCommand();
        bool canSend = true;
        int clientId = 0;
 
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


            timer1.Interval = 15;
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gameState = SingletonGameState.GetInstance().GetGameState();
           if (gameState.GameActualState == 1)
            {
                label2.Text = "Várakozás a második játékosra...";
                label2.ForeColor = Color.White;
                textBoxName.Enabled = false;
                buttonConnect.Enabled = false;
                clientId = 1;
            }

            if (gameState.GameActualState == 2)
            {               
                panel1.Visible = false;
                this.KeyDown += new KeyEventHandler(Form1_KeyDown);
                this.KeyUp += new KeyEventHandler(Form1_KeyUp);
                if (clientId == 0)
                {
                    clientId = 2;
                }
            }

            player1Name.Text = gameState.Player1Name;
            player2Name.Text = gameState.Player2Name;
            player1Value.Text = gameState.Player1Value.ToString();
            player2Value.Text = gameState.Player2value.ToString();
            pictureHomePlayer1.Location = new Point(gameState.PictureHomePlayer1X, gameState.PictureHomePlayer1Y);
            pcictureHomeGoalKeeper.Location = new Point(gameState.PcictureHomeGoalKeeperX, gameState.PcictureHomeGoalKeeperY);
            pictureAwayPlayer1.Location = new Point(gameState.PictureAwayPlayer1X, gameState.PictureAwayPlayer1Y);
            pictureAwayGoalKeeper.Location = new Point(gameState.PictureAwayGoalKeeperX, gameState.PictureAwayGoalKeeperY);
            pictureBall.Location = new Point(gameState.PictureBallX, gameState.PictureBallY);

            canSend = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            userCommand.ClientId = clientId;
            if (e.KeyCode == Keys.Up )
            {
                userCommand.Up = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                userCommand.Down = true;
            }
            if (e.KeyCode == Keys.Left )
            {
                userCommand.Left = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                userCommand.Right = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                userCommand.Kick = true;
                userCommand.KickForce = 1;
            }
            if (e.KeyCode == Keys.S)
            {
                userCommand.Kick = true;
                userCommand.KickForce = 40;
            }
            if (e.KeyCode == Keys.D)
            {
                userCommand.Kick = true;
                userCommand.KickForce = 100;
            }
            if (canSend)
            {
                comm.SendMessage(JsonConvert.SerializeObject(userCommand));
                canSend = false;
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                userCommand.Up = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                userCommand.Down = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                userCommand.Left = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                userCommand.Right = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                userCommand.Kick = false;
                userCommand.KickForce = 0;
            }
            if (e.KeyCode == Keys.S)
            {
                userCommand.Kick = false;
                userCommand.KickForce = 0;
            }
            if (e.KeyCode == Keys.D)
            {
                userCommand.Kick = false;
                userCommand.KickForce = 0;
            }
            if (canSend)
            {
                comm.SendMessage(JsonConvert.SerializeObject(userCommand));
                canSend = false;
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
