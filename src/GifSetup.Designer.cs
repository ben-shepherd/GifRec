namespace GifRec
{
    partial class GifSetup
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
            this.picBtnRegion = new System.Windows.Forms.PictureBox();
            this.picBtnTime = new System.Windows.Forms.PictureBox();
            this.picBtnStart = new System.Windows.Forms.PictureBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.linkExit = new System.Windows.Forms.LinkLabel();
            this.linkMinimize = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnStart)).BeginInit();
            this.SuspendLayout();
            // 
            // picBtnRegion
            // 
            this.picBtnRegion.BackColor = System.Drawing.Color.Transparent;
            this.picBtnRegion.Image = global::GifRec.Properties.Resources.button1;
            this.picBtnRegion.Location = new System.Drawing.Point(52, 130);
            this.picBtnRegion.Name = "picBtnRegion";
            this.picBtnRegion.Size = new System.Drawing.Size(149, 50);
            this.picBtnRegion.TabIndex = 0;
            this.picBtnRegion.TabStop = false;
            this.picBtnRegion.Click += new System.EventHandler(this.picBtnRegion_Click);
            // 
            // picBtnTime
            // 
            this.picBtnTime.BackColor = System.Drawing.Color.Transparent;
            this.picBtnTime.Image = global::GifRec.Properties.Resources.buttonOptions;
            this.picBtnTime.Location = new System.Drawing.Point(52, 190);
            this.picBtnTime.Name = "picBtnTime";
            this.picBtnTime.Size = new System.Drawing.Size(149, 50);
            this.picBtnTime.TabIndex = 1;
            this.picBtnTime.TabStop = false;
            this.picBtnTime.Click += new System.EventHandler(this.picBtnTime_Click);
            // 
            // picBtnStart
            // 
            this.picBtnStart.BackColor = System.Drawing.Color.Transparent;
            this.picBtnStart.Image = global::GifRec.Properties.Resources.button3_2;
            this.picBtnStart.Location = new System.Drawing.Point(207, 130);
            this.picBtnStart.Name = "picBtnStart";
            this.picBtnStart.Size = new System.Drawing.Size(149, 50);
            this.picBtnStart.TabIndex = 2;
            this.picBtnStart.TabStop = false;
            this.picBtnStart.Click += new System.EventHandler(this.picBtnStart_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.ForeColor = System.Drawing.Color.White;
            this.labelStatus.Location = new System.Drawing.Point(207, 190);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(60, 13);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "Status: Idle";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Location = new System.Drawing.Point(207, 209);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(67, 13);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "Record time:";
            // 
            // linkExit
            // 
            this.linkExit.AutoSize = true;
            this.linkExit.BackColor = System.Drawing.Color.Transparent;
            this.linkExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkExit.ForeColor = System.Drawing.Color.Transparent;
            this.linkExit.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkExit.LinkColor = System.Drawing.Color.White;
            this.linkExit.Location = new System.Drawing.Point(355, 1);
            this.linkExit.Name = "linkExit";
            this.linkExit.Size = new System.Drawing.Size(30, 29);
            this.linkExit.TabIndex = 6;
            this.linkExit.TabStop = true;
            this.linkExit.Text = "X";
            this.linkExit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkExit_LinkClicked_1);
            // 
            // linkMinimize
            // 
            this.linkMinimize.AutoSize = true;
            this.linkMinimize.BackColor = System.Drawing.Color.Transparent;
            this.linkMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkMinimize.ForeColor = System.Drawing.Color.Transparent;
            this.linkMinimize.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkMinimize.LinkColor = System.Drawing.Color.White;
            this.linkMinimize.Location = new System.Drawing.Point(355, 30);
            this.linkMinimize.Name = "linkMinimize";
            this.linkMinimize.Size = new System.Drawing.Size(26, 29);
            this.linkMinimize.TabIndex = 7;
            this.linkMinimize.TabStop = true;
            this.linkMinimize.Text = "_";
            this.linkMinimize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMinimize_LinkClicked);
            // 
            // GifSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GifRec.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.ControlBox = false;
            this.Controls.Add(this.linkMinimize);
            this.Controls.Add(this.linkExit);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.picBtnStart);
            this.Controls.Add(this.picBtnTime);
            this.Controls.Add(this.picBtnRegion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GifSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.GifSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBtnRegion;
        private System.Windows.Forms.PictureBox picBtnTime;
        private System.Windows.Forms.PictureBox picBtnStart;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.LinkLabel linkExit;
        private System.Windows.Forms.LinkLabel linkMinimize;

    }
}