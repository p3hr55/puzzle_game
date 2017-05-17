using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ImagePicker
{
    public partial class Game : Form
    {
        private int board_size = -1; //Represents board dimensions
        private int empty_sp1, empty_sp2; //Keeps track of where the empty spot is on the board
        private bool game_session = false; //Represents if the game is currently being played
        private const int board_width = 576;
        private bool check = false; //So the randomize method doesn't solve the puzzle
        private bool orien = true;
        private int moves = 0;
        private int counter = 10;
        private float cx = 820, cy = 630;
        private PictureBox[,] puzzle; //The puzzle we are changing
        private PictureBox[,] puzzle_completed; //A reference to the originial puzzle
        private PictureBox reference; //Small image to be placed for hint
        private Bitmap start_image;
        private int height, width;
        private Point start_index;
        private TableLayoutPanel table, tableLayoutPanel2;
        private int best_seconds = 61, best_hours = 24;

        public Game()
        {
            InitializeComponent();
        }

        //Resizes the bitmap to specified size
        private Bitmap res(Image i, Size s)
        {
            return new Bitmap(i, s);
        }

        //Allows the bitmap to be placed into pictureboxes
        private static Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        //For opening up a filebrowser and creating the bitmap image/reference image
        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel2 = new TableLayoutPanel();
            float local_h, local_w, ratio;

            if (!game_session) //Will not allow pictures to be uploaded during game progress
            {
                FilePick f = new FilePick();
                DialogResult d = f.ShowDialog();
                if (d == DialogResult.OK)
                {
                    string p = f.path;
                    board_size = f.getsize;
                    start_image = new Bitmap(p);
                    start_image = new Bitmap(p);
                    reference = new PictureBox();

                    if (start_image.Height >= start_image.Width)
                    {
                        local_h = start_image.Height;
                        local_w = start_image.Width;
                        ratio = local_w / local_h;

                        reference.Height = 115;
                        reference.Width = (int)(115 * ratio);

                        //funky method of setting up sizes
                        start_image = res(start_image, new Size((int)(this.Width * ratio), this.Height));
                        reference.Image = res(start_image, new Size((int)(115 * ratio), 115));

                        orien = false; //used to tell what the orientation of the image is

                        start_index = new Point((576 - start_image.Width) / 2, 0);
                        height = this.Height - 45;
                        width = (int)(((float)tableLayoutPanel1.Width) * ratio);

                        //tablelayoutpanel2 will need to be setup differently if it's a portrait or landscape image
                        tableLayoutPanel2.RowCount = 0;
                        tableLayoutPanel2.ColumnCount = 2;
                        tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
                        tableLayoutPanel2.Dock = DockStyle.Fill;

                        //setting the panel columns width
                        tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, (int)((width / (float)this.Width) * 100)));
                        tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 - (height / (float)this.Height) * 100));
                    }

                    else
                    {
                        local_h = start_image.Height;
                        local_w = start_image.Width;
                        ratio = local_h / local_w;

                        reference.Height = (int)(140 * ratio);
                        reference.Width = 140;

                        start_image = res(start_image, new Size(this.Width - 235, (int)(this.Height * ratio)));
                        reference.Image = res(start_image, new Size(140, (int)(140 * ratio)));

                        start_index = new Point(0, (585 - start_image.Height) / 2);
                        height = (int)((this.Height - 45) * ratio);
                        width = tableLayoutPanel1.Width - 220;

                        //tablelayoutpanel2 will need to be setup differently if it's a portrait or landscape image
                        tableLayoutPanel2.RowCount = 2;
                        tableLayoutPanel2.ColumnCount = 0;
                        tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
                        tableLayoutPanel2.Dock = DockStyle.Fill;

                        //setting the panel row widths
                        tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (int)((height / (float)this.Height) * 100)));
                        tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 - (height / (float)this.Height) * 100));
                    }

                    tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0); //adds panel we just constructed to main panel
                    tableLayoutPanel2.BringToFront();

                    //creates subpanels that will contain the puzzle peices
                    table  = new TableLayoutPanel();
                    table.ColumnCount = board_size;
                    table.RowCount = board_size;
                    table.Dock = DockStyle.Fill;

                    //sets height and width of the subpanels
                    for (int i = 0; i < board_size; i++)
                    {
                        table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / board_size));
                        table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / board_size));
                    }

                    tableLayoutPanel2.Controls.Add(table, 0, 0);

                    puzzle = new PictureBox[board_size, board_size];
                    puzzle_completed = new PictureBox[board_size, board_size];

                    int incr = 0;

                    //uses puzzle_completed differently in this program, used to change the indexes
                    for (int i = 0; i < board_size; i++)
                    {
                        for (int x = 0; x < board_size; x++)
                        {
                            puzzle[i, x] = puzzle_completed[i, x] = new PictureBox();
                            puzzle[i, x].Image = puzzle_completed[i, x].Image = cropImage(start_image, new Rectangle(new Point(i * width / board_size, x * height / board_size), new Size(width / board_size, height / board_size)));
                            table.Controls.Add(puzzle[i, x], i, x);
                            puzzle[i, x].Dock = DockStyle.Fill;
                            puzzle[i, x].BorderStyle = BorderStyle.FixedSingle;
                            puzzle[i, x].Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                            puzzle[i, x].SizeMode = PictureBoxSizeMode.StretchImage;
                            puzzle[i, x].Click += toggle;
                        }
                    }

                    //our empty puzzle peice
                    PictureBox g = new PictureBox();
                    g.Dock = DockStyle.Fill;
                    g.BorderStyle = BorderStyle.FixedSingle;
                    g.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                    g.SizeMode = PictureBoxSizeMode.StretchImage;
                    table.Controls.RemoveAt(board_size * board_size - 1);
                    table.Controls.Add(g, board_size - 1, board_size - 1);
                    puzzle[board_size - 1, board_size - 1] = puzzle_completed[board_size - 1, board_size - 1] = g;
                    puzzle[board_size - 1, board_size - 1].Click += toggle;
                    empty_sp1 = board_size - 1;
                    empty_sp2 = board_size - 1;

                    randomize(); //Shuffles the board
                   
                    //Enabled some last things
                    check = true;
                    game_session = true;
                    timer1.Enabled = true;
                    stopwatch1.start();
                }
            }
        }

        private void randomize()
        {
            Random r = new Random();
            ProgressBar b = new ProgressBar();
            int percent;

            //Creating the progress bar
            b.Maximum = board_size * 2000;
            b.Step = 1;
            b.Width = 200;
            //b.Location = new System.Drawing.Point(game_ground.Location.X + 190, game_ground.Location.Y + 275);

            Controls.Add(b);
            b.BringToFront(); //Needs to be infront of everything on the screen

            for (int i = 0; i < board_size * 2000; i++) //Might be overkill but gives randomized boards
            {
                switch (r.Next(0, 6))
                {
                    case 1:
                        if (empty_sp1 + 2 <= board_size)
                            toggle(puzzle[empty_sp1 + 1, empty_sp2], System.EventArgs.Empty);
                        break;

                    case 2:
                        if (empty_sp1 - 1 >= 0)
                            toggle(puzzle[empty_sp1 - 1, empty_sp2], System.EventArgs.Empty);
                        break;

                    case 3:
                        if (empty_sp2 + 2 <= board_size)
                            toggle(puzzle[empty_sp1, empty_sp2 + 1], System.EventArgs.Empty);
                        break;

                    case 4:
                        if (empty_sp2 - 1 >= 0)
                            toggle(puzzle[empty_sp1, empty_sp2 - 1], System.EventArgs.Empty);
                        break;
                }

                //Progress bar advances
                b.PerformStep();
                percent = (int)(((double)b.Value / (double)b.Maximum) * 100);
                b.CreateGraphics().DrawString("Randomizing : " + percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(40, b.Height / 2 - 7));
            }

            //After we load the image we have no use for a progress bar
            Controls.Remove(b);
        }

        //Gets what size board is desired from CustomBox
        //A little bit of spaghetti code was used to retrieve from a static method
        private void button3_Click(object sender, EventArgs e)
        {
            if (!game_session)
            {
                board_size = CustomBox.Show();
                puzzle = new PictureBox[board_size, board_size];
                puzzle_completed = new PictureBox[board_size, board_size];
            }
        }

        //Quits instance
        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Allows our timer to run
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--; //Only increment every second

            //Some spaghetti to make the timer work
            if (counter == 0)
            {
            }
        }

        //Shows us our hint
        private void button6_MouseHover(object sender, EventArgs e)
        {
            if (start_image != null && game_session)
            {
                if (orien)
                    reference.Location = new System.Drawing.Point(this.Width - 190, file_pick_but.Location.Y + 70);

                else
                    reference.Location = new System.Drawing.Point(this.Width - 170, file_pick_but.Location.Y + 58);

                Controls.Add(reference);
                reference.BringToFront();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            stopwatch1.pause();
            for (int i = 0; i < board_size; i++)
            {
                for (int x = 0; x < board_size; x++)
                {
                    puzzle[i, x].Hide();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopwatch1.resume();
            for (int i = 0; i < board_size; i++)
            {
                for (int x = 0; x < board_size; x++)
                {
                    puzzle[i, x].Show();
                }
            }
        }

        //Removes the hint
        private void button6_MouseLeave(object sender, EventArgs e)
        {
            if (start_image != null && game_session)
                Controls.Remove(reference);
        }

        //Resets the board, is also used for game wins
        private void button5_Click(object sender, EventArgs e)
        {
            ///game_ground.BackColor = System.Drawing.SystemColors.ControlDarkDark; //Original background color

            for (int i = 0; i < board_size * board_size; i++)
                table.Controls.RemoveAt(0);

            stopwatch1.pause();
            tableLayoutPanel2.Controls.Remove(table);
            tableLayoutPanel1.Controls.Remove(tableLayoutPanel2);


            //Resetting a few more fields for reset
            check = false;
            game_session = false;
            timer1.Enabled = false;
            orien = true;
            start_image = null;
            board_size = -1;
            moves = 0;

            moves_int_label.Text = "0";
            //secs_label.Text = mins_label.Text = "00";
        }

        //Funky method used for moving the boxes
        private void toggle(object sender, EventArgs e)
        {
            PictureBox s = sender as PictureBox;
            PictureBox p;
            Image w;
            bool valid_move = false;

            //all of the following methods will change the state of the board  depending on where the empty peice is in relation to clicking square
            //will swap the images inside of the panels and will swap the pictureboxes inside of puzzle_completed array
            //each move will change the state of where the empty puzzle peice is and will also signify a valid move
            if (empty_sp2 + 1 <= board_size - 1 && puzzle[empty_sp1, empty_sp2 + 1] == s)
            {
                w = puzzle[empty_sp1, empty_sp2 + 1].Image;
                puzzle[empty_sp1, empty_sp2  +1].Image = puzzle[empty_sp1, empty_sp2].Image;
                puzzle[empty_sp1, empty_sp2].Image = w;

                p = puzzle_completed[empty_sp1, empty_sp2 + 1];
                puzzle_completed[empty_sp1, empty_sp2 + 1] = puzzle_completed[empty_sp1, empty_sp2];
                puzzle_completed[empty_sp1, empty_sp2] = p;

                valid_move = true;
                empty_sp2++;
            }

            else if (empty_sp2 - 1 >= 0 && puzzle[empty_sp1, empty_sp2 - 1] == s)
            {

                w = puzzle[empty_sp1, empty_sp2 - 1].Image;
                puzzle[empty_sp1, empty_sp2 - 1].Image = puzzle[empty_sp1, empty_sp2].Image;
                puzzle[empty_sp1, empty_sp2].Image = w;

                p = puzzle_completed[empty_sp1, empty_sp2 - 1];
                puzzle_completed[empty_sp1, empty_sp2 - 1] = puzzle_completed[empty_sp1, empty_sp2];
                puzzle_completed[empty_sp1, empty_sp2] = p;

                valid_move = true;
                empty_sp2--;
            }

            else if (empty_sp1 + 1 <= board_size - 1 && puzzle[empty_sp1 + 1, empty_sp2] == s)
            {
                w = puzzle[empty_sp1 + 1, empty_sp2].Image;
                puzzle[empty_sp1 + 1, empty_sp2].Image = puzzle[empty_sp1, empty_sp2].Image;
                puzzle[empty_sp1, empty_sp2].Image = w;

                p = puzzle_completed[empty_sp1 + 1, empty_sp2];
                puzzle_completed[empty_sp1 + 1, empty_sp2] = puzzle_completed[empty_sp1, empty_sp2];
                puzzle_completed[empty_sp1, empty_sp2] = p;

                valid_move = true;
                empty_sp1++;
            }

            else if (empty_sp1 - 1 >= 0 && puzzle[empty_sp1 - 1, empty_sp2] == s)
            {
                w = puzzle[empty_sp1 - 1, empty_sp2].Image;
                puzzle[empty_sp1 - 1, empty_sp2].Image = puzzle[empty_sp1, empty_sp2].Image;
                puzzle[empty_sp1, empty_sp2].Image = w;

                p = puzzle_completed[empty_sp1 - 1, empty_sp2];
                puzzle_completed[empty_sp1 - 1, empty_sp2] = puzzle_completed[empty_sp1, empty_sp2];
                puzzle_completed[empty_sp1, empty_sp2] = p;

                valid_move = true;
                empty_sp1--;
            }

            if (check && valid_move) //If this global is set to true then we will start looking to see if the game was solved with each move
            {
                //Little bit of code to handle incrementing of move coutner
                if (moves_int_label.Text == "9")
                    moves_int_label.Location = new System.Drawing.Point(moves_int_label.Location.X - 5, moves_int_label.Location.Y);

                else if (moves_int_label.Text == "99")
                    moves_int_label.Location = new System.Drawing.Point(moves_int_label.Location.X - 5, moves_int_label.Location.Y);

                moves_int_label.Text = "" + ++moves;

                
                //Funky way I devised to tell when the puzzle is completed
                for (int x = 0; x < board_size; x++)
                {
                    for (int y = 0; y < board_size; y++)
                    {
                        if (puzzle[x, y] != puzzle_completed[x, y])
                            return;
                    }
                }

                stopwatch1.pause();
                if (stopwatch1.Minutes < best_hours)
                {
                    best_hours = stopwatch1.Minutes;
                    best_seconds = stopwatch1.Seconds;
                    label4.Text = "" + best_hours;
                    label2.Text = "" + best_seconds;
                }
                else if (stopwatch1.Minutes == best_hours && (stopwatch1.Seconds < best_seconds))
                {
                    best_hours = stopwatch1.Minutes;
                    best_seconds = stopwatch1.Seconds;
                    label4.Text = "" + best_hours;
                    label2.Text = "" + best_seconds;
                }

                if (label2.Text.Length == 1)
                    label2.Text = "0" + label2.Text;

                if (label4.Text.Length == 1)
                    label4.Text = "0" + label4.Text;

                Win.Show();
                button5_Click(null, EventArgs.Empty);
            }
        }

        //used to put in labels, got glitchy and decided to not use for now
        private void KastKode(Object s, EventArgs e)
        {
            try
            {
                Label l = s as Label;
                toggle(l.Parent, EventArgs.Empty);
            }

            catch(Exception foo)
            {
                MessageBox.Show("Error: " + foo.Message, "Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //because our righthand panel is always going to be an absolute width (because the buttons look weird when resizing)
        //we can just resize the picture by taking away the absolute value from the width and applying that value to height, or vise versa
        private void Game_ResizeEnd(object sender, EventArgs e)
        {
            int changex = this.Width - (int)cx;
            int changey = this.Height - (int)cy;

            if (Math.Abs(changex) > Math.Abs(changey))
                this.Height = this.Width - 190;

            else 
                this.Width = this.Height + 190;

            //changes look of side border each time
            border_p.Height = this.Height - 49;
            container_p.Height = border_p.Height - 6;
            quit_but.Location = new Point(quit_but.Location.X, this.Height - 117);

            cx = this.Width;
            cy = this.Height;
        }
    }

}
