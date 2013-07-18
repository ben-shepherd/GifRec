namespace GifRec
{
    partial class UserInputDialog
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
            this.picOK = new System.Windows.Forms.PictureBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkExit = new System.Windows.Forms.LinkLabel();
            this.pbcbUploadWeb = new GifRec.Checkbox();
            this.pbcbOpenAfter = new GifRec.Checkbox();
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcbUploadWeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcbOpenAfter)).BeginInit();
            this.SuspendLayout();
            // 
            // picOK
            // 
            this.picOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picOK.BackColor = System.Drawing.Color.Transparent;
            this.picOK.Image = global::GifRec.Properties.Resources.button4;
            this.picOK.Location = new System.Drawing.Point(435, 305);
            this.picOK.Margin = new System.Windows.Forms.Padding(7);
            this.picOK.Name = "picOK";
            this.picOK.Size = new System.Drawing.Size(149, 51);
            this.picOK.TabIndex = 5;
            this.picOK.TabStop = false;
            this.picOK.Click += new System.EventHandler(this.picOK_Click);
            // 
            // txtTime
            // 
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(32, 120);
            this.txtTime.Margin = new System.Windows.Forms.Padding(7);
            this.txtTime.MaxLength = 2;
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(308, 35);
            this.txtTime.TabIndex = 6;
            this.txtTime.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 34);
            this.label1.TabIndex = 7;
            this.label1.Text = "Recording Duration: (Seconds)";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(96, 187);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(436, 34);
            this.label2.TabIndex = 9;
            this.label2.Text = "Open after created";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(96, 261);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(436, 34);
            this.label3.TabIndex = 11;
            this.label3.Text = "Upload to Web (In development)";
            // 
            // linkExit
            // 
            this.linkExit.AutoSize = true;
            this.linkExit.BackColor = System.Drawing.Color.Transparent;
            this.linkExit.ForeColor = System.Drawing.Color.Transparent;
            this.linkExit.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkExit.LinkColor = System.Drawing.Color.White;
            this.linkExit.Location = new System.Drawing.Point(558, 9);
            this.linkExit.Name = "linkExit";
            this.linkExit.Size = new System.Drawing.Size(30, 29);
            this.linkExit.TabIndex = 12;
            this.linkExit.TabStop = true;
            this.linkExit.Text = "X";
            this.linkExit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkExit_LinkClicked_1);
            // 
            // pbcbUploadWeb
            // 
            this.pbcbUploadWeb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbcbUploadWeb.Checked = false;
            this.pbcbUploadWeb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbcbUploadWeb.Image = global::GifRec.Properties.Resources.checkboxNormal;
            this.pbcbUploadWeb.Location = new System.Drawing.Point(32, 252);
            this.pbcbUploadWeb.Margin = new System.Windows.Forms.Padding(7);
            this.pbcbUploadWeb.Name = "pbcbUploadWeb";
            this.pbcbUploadWeb.Size = new System.Drawing.Size(50, 50);
            this.pbcbUploadWeb.TabIndex = 10;
            this.pbcbUploadWeb.TabStop = false;
            // 
            // pbcbOpenAfter
            // 
            this.pbcbOpenAfter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbcbOpenAfter.Checked = false;
            this.pbcbOpenAfter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbcbOpenAfter.Image = global::GifRec.Properties.Resources.checkboxNormal;
            this.pbcbOpenAfter.Location = new System.Drawing.Point(32, 178);
            this.pbcbOpenAfter.Margin = new System.Windows.Forms.Padding(7);
            this.pbcbOpenAfter.Name = "pbcbOpenAfter";
            this.pbcbOpenAfter.Size = new System.Drawing.Size(50, 50);
            this.pbcbOpenAfter.TabIndex = 8;
            this.pbcbOpenAfter.TabStop = false;
            // 
            // UserInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::GifRec.Properties.Resources.backgroundOptions;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(600, 372);
            this.Controls.Add(this.linkExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbcbUploadWeb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbcbOpenAfter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.picOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "UserInputDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserInputDialog";
            this.Load += new System.EventHandler(this.UserInputDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcbUploadWeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcbOpenAfter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picOK;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private Checkbox pbcbOpenAfter;
        private Checkbox pbcbUploadWeb;
        private System.Windows.Forms.LinkLabel linkExit;
    }
}