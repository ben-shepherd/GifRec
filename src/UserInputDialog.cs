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

        }

        //Exit button
        private void linkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        //OK Button
        private void picOK_Click(object sender, EventArgs e)
        {
            //Set options
            Options.Set("openafter", pbcbOpenAfter.Checked);
            Options.Set("upload", pbcbUploadWeb.Checked);

            //Save
            Options.Save();

            this.Close();
        }

        //Form load
        private void UserInputDialog_Load(object sender, EventArgs e)
        {
            //Load options
            Options.Load();

            bool openAfter, uploadWeb;

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

    /// <summary>
    /// PictureBox turned into a custom checkbox
    /// </summary>
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
