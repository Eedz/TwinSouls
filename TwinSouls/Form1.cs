using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwinSouls
{
    public partial class Form1 : Form
    {
        Player player1;

        Player player2;
        List<Enemy> enemies;
        List<Projectile> bullets;

        int backLeft = 8; // background speed

        public Form1()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            // set up player 1
            player1 = new Player();
            player1.Left = 100;
            player1.Top = 100;
            player1.Width = 50;
            player1.Height = 60;
            player1.soul = "light";
            player1.EquippedWeapon = new Weapon(50);

            playerLeft.DataBindings.Add(new Binding("Text", player1, "Left"));
            playerTop.DataBindings.Add(new Binding("Text", player1, "Top"));

            p1 = new PictureBox();
            p1.Image = Properties.Resources.player;
            p1.DataBindings.Add(new Binding("Left", player1, "Left"));
            p1.DataBindings.Add(new Binding("Top", player1, "Top"));
            p1.Width = player1.Width;
            p1.Height = player1.Height;
            p1.Tag = "player";
            this.Controls.Add(p1);

            // player 2
            player2 = new Player();
            player2.Left = 100;
            player2.Top = 400;
            player2.Width = 50;
            player2.Height = 60;
            player2.soul = "dark";
            player2.EquippedWeapon = new Weapon(50);

            p2 = new PictureBox();
            p2.Image = Properties.Resources.player;
            p2.DataBindings.Add(new Binding("Left", player2, "Left"));
            p2.DataBindings.Add(new Binding("Top", player2, "Top"));
            p2.Width = player2.Width;
            p2.Height = player2.Height;
            p2.Tag = "player";
            this.Controls.Add(p2);

            // enemies
            enemies = new List<Enemy>();
        }

        // shoot or attack (melee)
        private void Shoot(Player p)
        {
            PictureBox shot = new PictureBox();
            shot.Bounds = p.GetRectangle();
            shot.Image = Properties.Resources.key;
            shot.Tag = "shot";
            Controls.Add(shot);
            shot.Left += p.EquippedWeapon.Range;

            for (int i = 0; i < enemies.Count; i++)
            {
                if (shot.Bounds.IntersectsWith(enemies[i].GetRectangle()))
                {
                    for (int c = 0; c < this.Controls.Count; c++)
                    {
                        if (this.Controls[c] is PictureBox)
                        if (this.Controls[c].Tag.Equals("enemy" + enemies[i].ID))
                            this.Controls.RemoveAt(c);
                    }
                    enemies.RemoveAt(i);
                    
                }
            }
            
        }

        private void gameTick (Player p)
        {
            // links the jumpspeed with the player 
            p.Top += p.jumpSpeed;

            // if jumping is true and force is less than 0
            // then change jumping to false
            if (p.jumping && p.force < 0)
            {
                p.jumping = false;
            }

            // if jumping is true
            // then change jump speed to -12
            // reduce force by 1
            if (p.jumping)
            {
                p.jumpSpeed = -15;
                p.force -= 1;
            }
            else
            {
                // else change the jump speed to 12
                p.jumpSpeed = 15;
            }

            // if goleft is true and player's left is greater than 100 pixels
            // only then move the player left
            if (p.goleft && p.Left > 100)
            {
                p.Left -= p.playSpeed;
            }

            // if goright is true
            // player left plus player width plus 100 is less than the form width
            // then we move the player towards the right by adding the player's left
            if (p.goright && p.Left + (p.Width + 100) < this.ClientSize.Width)
            {
                p.Left += p.playSpeed;
            }

            // check each control on the form
            foreach (Control x in this.Controls)
            {
                // is x a platform
                if (x is PictureBox && x.Tag == "platform")
                {
                    // we are checking if the player is colliding with a platform
                    if (p.GetRectangle().IntersectsWith(x.Bounds))
                    {
                        p.force = 8;
                        p.Top = x.Top - p.Height; // place player on the platform
                        p.jumpSpeed = 0; // stop the jump
                    }

                }

                // if the picture box is a coin
                if (x is PictureBox && x.Tag == "coin")
                {
                    // now if the player collides with the coin box
                    if (p.GetRectangle().IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x); // remove the coin
                        p.score++;
                    }
                }

                // check enemies
                if (x is PictureBox)
                {
                    if (x.Tag.ToString().StartsWith("enemy") && p.GetRectangle().IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop(); // stop the timer
                        MessageBox.Show("You Died.");
                    }
                }

                if (x is PictureBox && x.Tag == "shot")
                {
                    this.Controls.Remove(x);
                }
            }

            

            

            if (p.Top + p.Height > this.ClientSize.Height + 60)
            {
                gameTimer.Stop(); // stop the timer
                MessageBox.Show("You Died.");
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gameTick(player1);
            gameTick(player2);

            foreach (LightEnemy le in enemies)
            {
                enemyTick(le);
            }
        }

        private void enemyTick(Enemy enemy)
        {

            if (player1.Left < enemy.Left)
            {
                enemy.Left -= enemy.Speed;
            }
            else
            {
                enemy.Left += enemy.Speed;
            }

            if (player1.Top < enemy.Top)
            {
                enemy.Top -= enemy.Speed;
            }
            else
            {
                enemy.Top += enemy.Speed;
            }

            // links the jumpspeed with the player 
            enemy.Top += enemy.jumpSpeed;

            // if jumping is true and force is less than 0
            // then change jumping to false
            if (enemy.jumping && enemy.force < 0)
            {
                enemy.jumping = false;
            }

            // if jumping is true
            // then change jump speed to -12
            // reduce force by 1
            if (enemy.jumping)
            {
                enemy.jumpSpeed = -15;
                enemy.force -= 1;
            }
            else
            {
                // else change the jump speed to 12
                enemy.jumpSpeed = 15;
            }

            // check each control on the form
            foreach (Control x in this.Controls)
            {
                // is x a platform
                if (x is PictureBox && x.Tag == "platform")
                {
                    // then we are checking if the enemy is colliding with a platform
                    if (enemy.GetRectangle().IntersectsWith(x.Bounds))
                    {
                        enemy.force = 8;
                        enemy.Top = x.Top - enemy.Height; // place player on the platform
                        enemy.jumpSpeed = 0; // stop the jump
                    }
                }
            }

        }

        // add a new enemy every 5 seconds, up to 5 enemies
        private void enemyTimer_Tick(object sender, EventArgs e)
        {
            if (enemies.Count > 4)
                return;

            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            LightEnemy le = new LightEnemy();
            le.ID = enemies.Count();
            le.Left = 1200;
            le.Top = 100;
            le.Width = 60;
            le.Height = 45;
            enemies.Add(le);

            enemyLeft.DataBindings.Clear();
            enemyLeft.DataBindings.Add(new Binding("Text", le, "Left"));
            enemyTop.DataBindings.Clear();
            enemyTop.DataBindings.Add(new Binding("Text", le, "Top"));

            PictureBox e1 = new PictureBox();

            e1.DataBindings.Add(new Binding("Left", le, "Left"));
            e1.DataBindings.Add(new Binding("Top", le, "Top"));
            e1.Height = le.Height;
            e1.Width = le.Width;
            e1.Tag = "enemy" + le.ID;

            e1.Image = Properties.Resources.door_closed;
            e1.Visible = true;
            this.Controls.Add(e1);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // if the player pressed the left key AND the player is inside the panel
            // then set the left boolean true
            if (e.KeyCode == Keys.Left)
            {
                player1.goleft = true;
            
            }

            if (e.KeyCode == Keys.A)
            {
                player2.goleft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                player1.goright = true;
              
            }

            if (e.KeyCode == Keys.D)
            {
                player2.goright = true;
            }

            if (e.KeyCode == Keys.Space && !player1.jumping)
            {
                player1.jumping = true;
            }

            if (e.KeyCode == Keys.LControlKey && !player2.jumping)
            {
                player2.jumping = true;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                Shoot(player1);
            }

            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player1.goleft = false;
            }

            if (e.KeyCode == Keys.A)
            {
                player2.goleft = false;
            }

            if (e.KeyCode == Keys.Right)
                player1.goright = false;

            if (e.KeyCode == Keys.D)
                player2.goright = false;

            if (player1.jumping)
                player1.jumping = false;

            if (player2.jumping)
                player2.jumping = false;
        }

        private void UnusedTutorialCode()
        {
            // if the player collides with a door and has a key
            //if (player1.Bounds.IntersectsWith(door.Bounds) && hasKey)
            //{
            //    // change the door image to open
            //    door.Image = Properties.Resources.door_open;
            //    // stop the timer
            //    gameTimer.Stop();
            //    MessageBox.Show("You completed the level!");
            //}

            //if (player1.Bounds.IntersectsWith(key.Bounds))
            //{
            //    // then remove the key
            //    this.Controls.Remove(key);
            //    // change the has key to true
            //    hasKey = true;
            //}

            // if go right is true and the background picture left is > 1352
            // then we move the background picture towards the left
            //if (goright && background.Left > -1353)
            //{
            //    background.Left -= backLeft;
            //    foreach (Control x in this.Controls)
            //    {
            //        if (x is PictureBox && x.Tag == "platform" || x is PictureBox && x.Tag == "coin" || x is PictureBox && x.Tag = "door" || x is PictureBox & x.Tag = "key")
            //        {
            //            x.Left -= backLeft;
            //        }
            //    }
            //}

            // if goleft is true and the background picture's left is less than 2
            // then we move the background picture towards the right
            //if (goleft && background.Left < 2)
            //{
            //    background.Left += backLeft;
            // move the platforms and coins towards the right
            //    foreach (Control x in this.Controls)
            //    {
            //        if (x is PictureBox && x.Tag == "platform" || x is PictureBox && x.Tag== "coin" || x is PictureBox && x.Tag == "door" || x is PictureBox && x.Tag == "key")
            //        {
            //            x.Left += backLeft;
            //        }
            //    }
            //}
        }
    }
}
