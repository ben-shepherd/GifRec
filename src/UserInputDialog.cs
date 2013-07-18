using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GifRec
{
    public partial class UserInputDialog : Form
    {
        FormDraggable draggable;

        public UserInputDialog()
        {
            InitializeComponent();

            //Allow form to be dragged
            draggable = new FormDraggable(this);

            //Hover cursor
            picOK.Cursor = Cursors.Hand;

            //Disable web upload box
            //pbcbUploadWeb.Enabled = false;
        }

        //Exit button
        private void linkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        //OK Button
        private void picOK_Click(object sender, EventArgs e)
        {
            //Validation
            if (txtTime.Text.Length == 0)
            {
                MessageBox.Show("Record time cannot be empty!");
                return;
            }
            if (int.Parse(txtTime.Text) > 30)
            {
                MessageBox.Show("Record time cannot be larger than 30 seconds");
                return;
            }

            //Set options
            Options.Set("duration", txtTime.Text);
            Options.Set("openafter", pbcbOpenAfter.Checked);
            Options.Set("upload", pbcbUploadWeb.Checked);

            //Save
            Options.Save();

            this.Close();
        }

        //TextChange for record duration
        string lastText = null;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int duration; //only used to test parse

            if (!int.TryParse(txtTime.Text, out duration))
            {
                if (lastText == null)
                    lastText = "5";

                txtTime.Text = lastText;
            }
        }

        //Form load
        private void UserInputDialog_Load(object sender, EventArgs e)
        {
            //Load options
            Options.Load();

            bool openAfter, uploadWeb;

            //Time duration
            txtTime.Text = Options.Get("duration").ToString();

            //Open after created
            bool.TryParse(Options.Get("openafter").ToString(), out openAfter);
            pbcbOpenAfter.Checked = openAfter;

            //Upload to web
            bool.TryParse(Options.Get("upload").ToString(), out uploadWeb);
            pbcbUploadWeb.Checked = uploadWeb;
        }

        private void linkExit_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }

    public class Checkbox : PictureBox
    {
        private bool isChecked = false;

        public bool Checked
        {
            get { return isChecked; }
            set { isChecked = value; RefreshCheckboxImage(); }
        }

        public bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = value; RefreshCheckboxImage(); }
        }

        public Checkbox()
        {
            base.Cursor = Cursors.Hand;
            base.MouseDown += (s, e) =>
                {
                    isChecked = isChecked ? false : true;
                    RefreshCheckboxImage();
                };
        }

        private void RefreshCheckboxImage()
        {
            if (this.Enabled)
            {
                if (this.Checked)
                    base.Image = Properties.Resources.checkboxChecked;
                else
                    base.Image = Properties.Resources.checkboxNormal;
            }
            else
                base.Image = Properties.Resources.checkboxDisabled;
        }
    }
}
