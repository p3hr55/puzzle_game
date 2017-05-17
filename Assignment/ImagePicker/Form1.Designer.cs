namespace ImagePicker
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.moves_label = new System.Windows.Forms.Label();
            this.moves_int_label = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.border_p = new System.Windows.Forms.Panel();
            this.container_p = new System.Windows.Forms.Panel();
            this.restart_butt = new System.Windows.Forms.Button();
            this.file_pick_but = new System.Windows.Forms.Button();
            this.quit_but = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.stopwatch1 = new ImagePicker.Stopwatch();
            this.border_p.SuspendLayout();
            this.container_p.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // moves_label
            // 
            this.moves_label.AutoSize = true;
            this.moves_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moves_label.Location = new System.Drawing.Point(27, 133);
            this.moves_label.Name = "moves_label";
            this.moves_label.Size = new System.Drawing.Size(146, 20);
            this.moves_label.TabIndex = 5;
            this.moves_label.Text = "Number of moves:";
            // 
            // moves_int_label
            // 
            this.moves_int_label.AutoSize = true;
            this.moves_int_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moves_int_label.Location = new System.Drawing.Point(82, 162);
            this.moves_int_label.Name = "moves_int_label";
            this.moves_int_label.Size = new System.Drawing.Size(24, 25);
            this.moves_int_label.TabIndex = 6;
            this.moves_int_label.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // border_p
            // 
            this.border_p.BackColor = System.Drawing.SystemColors.ControlText;
            this.border_p.Controls.Add(this.container_p);
            this.border_p.Location = new System.Drawing.Point(664, 3);
            this.border_p.Name = "border_p";
            this.border_p.Size = new System.Drawing.Size(205, 605);
            this.border_p.TabIndex = 9;
            // 
            // container_p
            // 
            this.container_p.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.container_p.Controls.Add(this.button2);
            this.container_p.Controls.Add(this.button1);
            this.container_p.Controls.Add(this.label4);
            this.container_p.Controls.Add(this.label1);
            this.container_p.Controls.Add(this.label3);
            this.container_p.Controls.Add(this.label2);
            this.container_p.Controls.Add(this.restart_butt);
            this.container_p.Controls.Add(this.stopwatch1);
            this.container_p.Controls.Add(this.file_pick_but);
            this.container_p.Controls.Add(this.quit_but);
            this.container_p.Controls.Add(this.moves_label);
            this.container_p.Controls.Add(this.moves_int_label);
            this.container_p.Location = new System.Drawing.Point(3, 3);
            this.container_p.Name = "container_p";
            this.container_p.Size = new System.Drawing.Size(199, 599);
            this.container_p.TabIndex = 0;
            // 
            // restart_butt
            // 
            this.restart_butt.FlatAppearance.BorderSize = 0;
            this.restart_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.restart_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.restart_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restart_butt.Image = global::ImagePicker.Properties.Resources.RETRY;
            this.restart_butt.Location = new System.Drawing.Point(31, 6);
            this.restart_butt.Name = "restart_butt";
            this.restart_butt.Size = new System.Drawing.Size(132, 54);
            this.restart_butt.TabIndex = 14;
            this.restart_butt.UseVisualStyleBackColor = true;
            this.restart_butt.Click += new System.EventHandler(this.button5_Click);
            // 
            // file_pick_but
            // 
            this.file_pick_but.BackColor = System.Drawing.Color.Transparent;
            this.file_pick_but.CausesValidation = false;
            this.file_pick_but.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.file_pick_but.FlatAppearance.BorderSize = 0;
            this.file_pick_but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.file_pick_but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.file_pick_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.file_pick_but.Image = ((System.Drawing.Image)(resources.GetObject("file_pick_but.Image")));
            this.file_pick_but.Location = new System.Drawing.Point(5, 66);
            this.file_pick_but.Name = "file_pick_but";
            this.file_pick_but.Size = new System.Drawing.Size(177, 46);
            this.file_pick_but.TabIndex = 0;
            this.file_pick_but.TabStop = false;
            this.file_pick_but.UseVisualStyleBackColor = false;
            this.file_pick_but.Click += new System.EventHandler(this.button1_Click);
            // 
            // quit_but
            // 
            this.quit_but.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.quit_but.FlatAppearance.BorderSize = 0;
            this.quit_but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.quit_but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.quit_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quit_but.Image = global::ImagePicker.Properties.Resources.quittt;
            this.quit_but.Location = new System.Drawing.Point(32, 540);
            this.quit_but.Name = "quit_but";
            this.quit_but.Size = new System.Drawing.Size(141, 53);
            this.quit_but.TabIndex = 4;
            this.quit_but.UseVisualStyleBackColor = true;
            this.quit_but.Click += new System.EventHandler(this.button4_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel1.Controls.Add(this.border_p, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(874, 611);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Best Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(135, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(123, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(94, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Pause";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(105, 438);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Resume";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // stopwatch1
            // 
            this.stopwatch1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.stopwatch1.Location = new System.Drawing.Point(2, 251);
            this.stopwatch1.Name = "stopwatch1";
            this.stopwatch1.Size = new System.Drawing.Size(193, 181);
            this.stopwatch1.TabIndex = 10;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(874, 611);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1237, 1047);
            this.MinimumSize = new System.Drawing.Size(840, 650);
            this.Name = "Game";
            this.Text = "Puzzle Game";
            this.ResizeEnd += new System.EventHandler(this.Game_ResizeEnd);
            this.border_p.ResumeLayout(false);
            this.container_p.ResumeLayout(false);
            this.container_p.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button file_pick_but;
        private System.Windows.Forms.Button quit_but;
        private System.Windows.Forms.Label moves_label;
        private System.Windows.Forms.Label moves_int_label;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel border_p;
        private System.Windows.Forms.Panel container_p;
        private System.Windows.Forms.Button restart_butt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Stopwatch stopwatch1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

