﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace FinalProjectSquareChaserAdvanced
{ 
    public partial class Form1 : Form
    {
        List<Rectangle> squares = new List<Rectangle>();
        List<int> squareSpeeds = new List<int>();
        List<string> squareColours = new List<string>();

        int time = 500;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upDown = false;
        bool downDown = false;
        bool leftDown = false;
        bool rightDown = false;

        Rectangle player1 = new Rectangle(10, 210, 10, 10);
        Rectangle player2 = new Rectangle(10, 130, 10, 10);

        int player1Score = 0;
        int player2Score = 0;
        int player1Speed = 6;
        int player2Speed = 6;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        Random randGen = new Random();

        int groundHeight = 50;
        string gameState = "waiting";
        
        public void GameInitialize()
        {
            titleLabel.Text = "";
            subtitleLabel.Text = "";
            gameTimer.Enabled = true;
            gameState = "running";
            time = 60000;

            squares.Clear();
            squareColours.Clear();
            squareSpeeds.Clear();

            player1.X = this.Width / 2 - player1.Width / 2;
            player1.Y = this.Height - groundHeight - player1.Height;
            player2.X = this.Width / 2 - player2.Width / 2;
            player2.Y = this.Height - groundHeight - player2.Height;

            int x = randGen.Next(0, this.Width);
            int y = randGen.Next(0, this.Width);

            Rectangle newRec = new Rectangle(x, y, 10, 10);
            squares.Add(newRec);
            squareColours.Add("red");

            x = randGen.Next(0, this.Width);
            y = randGen.Next(0, this.Width);

            newRec = new Rectangle(x, y, 10, 10);
            squares.Add(newRec);
            squareColours.Add("green");
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
                case Keys.Space:
                    if (gameState == "waiting" || gameState == "over")
                    {
                        GameInitialize();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "over")
                    {
                        Application.Exit();
                    }
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upDown = false;
                    break;
                case Keys.Down:
                    downDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player 1
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= player1Speed;
            }
            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }
            if (aDown == true && player1.X > 0)
            {
                player1.X -= player1Speed;
            }
            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += player1Speed;
            }

            //move player2
            if (upDown == true && player2.Y > 0)
            {
                player2.Y -= player2Speed;
            }
            if (downDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }
            if (leftDown == true && player2.X > 0)
            {
                player2.X -= player2Speed;
            }
            if (rightDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += player2Speed;
            }

            // create code that checks if player 1 collides with ball1 and if it does
            // move ball1 to a different location.

            for (int i = 0; i < squares.Count(); i++)
            {
                if (player1.IntersectsWith(squares[i])) //white ball
                {
                    if (squareColours[i] == "green")
                    {
                        player1Score += 1;
                        p1scoreLabel.Text = $"P1: {player1Score}";

                        // get a new x and y value and reset the position of the green square at i
                    }
                    else
                    {
                        player1Score -= 1;
                    }
                }
            }
           
            for (int i = 0; i < squares.Count(); i++)
            {
                if (player2.IntersectsWith(squares[i])) //white ball
                {
                    if (squareColours[i] == "green")
                    {
                        player2Score += 1;
                        p1scoreLabel.Text = $"P1: {player2Score}";

                        // get a new x and y value and reset the position of the green square at i
                        

                    }
                    else
                    {
                        player2Score -= 1;
                    }
                }
            }
            
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }

            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "waiting")
            {
                titleLabel.Text = "Welcome to Square Chaser Advanced.";
                subtitleLabel.Text = "Press Space Bar to Start!";
                timeLabel.Text = $"";
                p1scoreLabel.Text = $"P1: 0";
                p2scoreLabel.Text = $"P2: 0 ";
            }
            else if (gameState == "running")
            {
                timeLabel.Text = $"Time: {time}";
                p1scoreLabel.Text = $"P1: {player1Score}";
                p2scoreLabel.Text = $"P2: {player2Score}";

                e.Graphics.FillRectangle(blueBrush, player1);
                e.Graphics.FillRectangle(yellowBrush, player2);

                for (int i = 0; i < squares.Count(); i++)
                {
                    if (squareColours[i] == "red")
                    {
                        e.Graphics.FillEllipse(redBrush, squares[i]);
                    }
                    else if (squareColours[i] == "green")
                    {
                        e.Graphics.FillEllipse(greenBrush, squares[i]);
                    }
                }
            }
            else if (gameState == "over")
            {
                timeLabel.Text = "";
                p1scoreLabel.Text = "";

                titleLabel.Text = "GAME OVER";
                subtitleLabel.Text += "\nPress Space Bar to Play Again or Escape to Exit";

                if (player1Score == 9)
                {
                    gameTimer.Enabled = false;
                    winLabel.Visible = true;
                    winLabel.Text = "Player 1 Wins!!";
                }
                else if (player2Score == 9)
                {
                    gameTimer.Enabled = false;
                    winLabel.Visible = true;
                    winLabel.Text = "Player 2 Wins!!";
                }
            }
            
            // e.Graphics.FillRectangle(blueBrush, player1);
            // e.Graphics.FillRectangle(yellowBrush, player2);
            ////draw squares 

        }
    }
}