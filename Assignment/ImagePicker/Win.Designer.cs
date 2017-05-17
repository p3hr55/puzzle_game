namespace ImagePicker
{
    partial class Win
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win));
            this.video = new System.Windows.Forms.WebBrowser();
            this.msg_1 = new System.Windows.Forms.Label();
            this.msg_2 = new System.Windows.Forms.Label();
            this.schwifty = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.schwifty)).BeginInit();
            this.SuspendLayout();
            // 
            // video
            // 
            resources.ApplyResources(this.video, "video");
            this.video.Name = "video";
            this.video.Url = new System.Uri("http://www.youtube.com/embed/hm9gF5Gi9x4?autoplay=1", System.UriKind.Absolute);
            // 
            // msg_1
            // 
            resources.ApplyResources(this.msg_1, "msg_1");
            this.msg_1.ForeColor = System.Drawing.SystemColors.Menu;
            this.msg_1.Name = "msg_1";
            // 
            // msg_2
            // 
            resources.ApplyResources(this.msg_2, "msg_2");
            this.msg_2.ForeColor = System.Drawing.SystemColors.Menu;
            this.msg_2.Name = "msg_2";
            // 
            // schwifty
            // 
            this.schwifty.Image = global::ImagePicker.Properties.Resources.schwiftyyy;
            resources.ApplyResources(this.schwifty, "schwifty");
            this.schwifty.Name = "schwifty";
            this.schwifty.TabStop = false;
            // 
            // Win
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.schwifty);
            this.Controls.Add(this.msg_2);
            this.Controls.Add(this.msg_1);
            this.Controls.Add(this.video);
            this.MaximizeBox = false;
            this.Name = "Win";
            ((System.ComponentModel.ISupportInitialize)(this.schwifty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser video;
        private System.Windows.Forms.Label msg_1;
        private System.Windows.Forms.Label msg_2;
        private System.Windows.Forms.PictureBox schwifty;
    }
}