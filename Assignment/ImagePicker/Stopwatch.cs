using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagePicker
{
    public partial class Stopwatch : UserControl
    {
        private BufferedGraphicsContext CurrentContext;
        private float radius;
        private float radius2;
        private System.Drawing.Point center;
        private System.Drawing.Point center2;
        private int start_seconds;
        private int start_minutes;
        private int minutes = 0;
        private bool inc = false;
        private int seconds;

        public Stopwatch()
        {
            InitializeComponent();
            if (Width < Height)
            {
                radius = (Width * 0.9f) / 2;
                radius2 = (Width * 0.2f) / 2;
            }

            else
            {
                radius = (Height * 0.9f) / 2;
                radius2 = (Height * 0.2f) / 2;
            }

            center = new System.Drawing.Point(Width / 2, Height / 2);
            center2 = new System.Drawing.Point(Width / 2, Height / 2);
        }

        private void Stopwatch_Load(object sender, EventArgs e)
        {
            CurrentContext = BufferedGraphicsManager.Current;

            System.Drawing.BufferedGraphics bg = CurrentContext.Allocate(CreateGraphics(), ClientRectangle);

            bg.Graphics.Clear(SystemColors.ActiveCaption);
            DrawNumbers(bg.Graphics);
            DrawTicks(bg.Graphics);


            bg.Render();
        }

        public void DrawHands(System.Drawing.Graphics g)
        {
            seconds = DateTime.Now.Second - start_seconds + delay;

            if (seconds == 0 && inc == true)
            {
                minutes++;
                inc = false;
            }

            if (seconds == 1)
                inc = true;

            System.Drawing.Pen penhours = new System.Drawing.Pen(Color.Black, radius * .05f);
            System.Drawing.Pen penminutes = new System.Drawing.Pen(Color.Black, radius * .03f);
            System.Drawing.Pen penseconds = new System.Drawing.Pen(Color.Black, radius * .02f);

            float x = center.X;// - (float)Math.Sin((seconds * 6) * Math.PI / 180) * radius * .15f;
            float y = center.Y;//- (float)-Math.Cos((seconds * 6) * Math.PI / 180) * radius * .15f;

            float x2 = (float)Math.Sin((seconds * 6) * Math.PI / 180) * radius * .85f + center.X;
            float y2 = (float)-Math.Cos((seconds * 6) * Math.PI / 180) * radius * .85f + center.Y;
            g.DrawLine(penseconds, x, y, x2, y2);

            System.Drawing.Pen centerpen = new System.Drawing.Pen(Color.Black, radius * .05f);
            Rectangle r = new Rectangle((int)(center.X - radius * 0.02f), (int)(center.Y - radius * 0.02), (int)(radius * 0.04), (int)(radius * 0.04));
            g.DrawArc(centerpen, r, 0, 360);

            g.DrawString("Minutes: " + minutes, new Font("Tahoma", radius * 0.08f, FontStyle.Bold),
                new SolidBrush(System.Drawing.Color.Black), center.X - radius / 3, center.Y - radius / 2);

        }

        public void DrawTicks(System.Drawing.Graphics g)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(Color.Black, radius * .02f);

            for (int i = 0; i < 60; i++)
            {
                float x = (float)Math.Cos(i * 6 * Math.PI / 180) * radius * .95f + center.X;
                float y = (float)Math.Sin(i * 6 * Math.PI / 180) * radius * .95f + center.Y;
                float x2 = (float)Math.Cos(i * 6 * Math.PI / 180) * radius * 1.0f + center.X;
                float y2 = (float)Math.Sin(i * 6 * Math.PI / 180) * radius * 1.0f + center.Y;
                g.DrawLine(pen, x, y, x2, y2);
            }

            System.Drawing.Pen centerpen = new System.Drawing.Pen(Color.Black, radius * .01f);
            g.DrawArc(centerpen, new Rectangle((int)(center.X - radius), (int)(center.Y - radius), (int)(radius * 2), (int)(radius * 2)), 0, 360);

        }

        public void DrawNumbers(System.Drawing.Graphics g)
        {
            System.Drawing.StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font numfont = new Font("Tahoma", radius * 0.08f, FontStyle.Bold);
            Brush numbrush = new SolidBrush(System.Drawing.Color.Black);

            for (int i = 1; i <= 60; i++)
            {
                if (i % 5 == 0)
                {
                    g.DrawString(i.ToString(), numfont, numbrush,
                    (float)Math.Sin((i / 5) * 30 * Math.PI / 180) * radius * .8f + center.X,
                    (float)-Math.Cos((i / 5) * 30 * Math.PI / 180) * radius * .8f + center.Y, sf);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CurrentContext = BufferedGraphicsManager.Current;

            System.Drawing.BufferedGraphics bg = CurrentContext.Allocate(CreateGraphics(), ClientRectangle);

            bg.Graphics.Clear(SystemColors.InactiveCaption);
            DrawNumbers(bg.Graphics);
            DrawTicks(bg.Graphics);
            DrawHands(bg.Graphics);


            bg.Render();

        }

        private int pause_time;
        private int delay = 0;

        public void start()
        {
            start_seconds = DateTime.Now.Second;
            delay = 0;
            minutes = 0;
            inc = false;
            timer1.Enabled = true;
        }

        public void pause()
        {
            timer1.Enabled = false;
            pause_time = DateTime.Now.Second;
        }

        public void resume()
        {
            timer1.Enabled = true;
            delay += (pause_time - DateTime.Now.Second);
        }

        public int Seconds
        {
            get
            {
                return seconds;
            }
        }

        public int Minutes
        {
            get
            {
                return minutes;
            }
        }

        private void Stopwatch_Resize(object sender, EventArgs e)
        {
            if (Width < Height)
            {
                radius = (Width * 0.9f) / 2;
            }
            else
            {
                radius = (Height * 0.9f) / 2;
            }
            center = new System.Drawing.Point(Width / 2, Height / 2);
        }

    }
}
