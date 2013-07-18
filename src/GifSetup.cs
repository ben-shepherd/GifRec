using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GifRec
{
    public partial class GifSetup : Form
    {
        FormDraggable draggable;
        Rectangle region = Rectangle.Empty;
        bool isWorking = false;

        /// <summary>
        /// Check if requirements are met to start gif, enable button if so
        /// <param name="draggable">Used to make the form draggable</param>
        /// </summary>
        public GifSetup()
        {
            InitializeComponent();

            draggable = new FormDraggable(this);

            DoDurationRegionCheck();
        }

        /// <summary>
        /// Sets icon
        /// Enable hand cursors on buttons
        /// Disable start button
        /// Set label recording time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GifSetup_Load(object sender, EventArgs e)
        {
            //Set icon
            this.Icon = Properties.Resources.favicon;

            //Set hover cursor state for image buttons
            picBtnRegion.Cursor = picBtnStart.Cursor = picBtnTime.Cursor = Cursors.Hand;

            //Disable start button
            ToggleStart(false);

            //Set record time
            labelTime.Text = "Record time: " + Options.Get("duration").ToString();
        }

        /// <summary>
        /// Select region butotn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBtnRegion_Click(object sender, EventArgs e)
        {
            RegionSelector rs = new RegionSelector();

            rs.FormClosing += (s, _e) =>
                {
                    if (rs.SelectedRegion)
                    {
                        region = rs.Region;

                        DoDurationRegionCheck();
                    }
                };

            rs.Show();
        }

        /// <summary>
        /// Set duration button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBtnTime_Click(object sender, EventArgs e)
        {
            UserInputDialog form = new UserInputDialog();

            form.FormClosing += (s, _e) =>
                {
                    labelTime.Text = "Record time: " + Options.Get("duration").ToString();

                    DoDurationRegionCheck();
                };

            form.ShowDialog(this);
        }

        /// <summary>
        /// Start recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBtnStart_Click(object sender, EventArgs e)
        {
            if (!isWorking)
            {
                isWorking = false;
                GifRecorder gf = new GifRecorder(region, SetText, ToggleStart, StopWorking, SetClipboard);

                ToggleStart(false);
                gf.Start();
            }
        }

        /// <summary>
        /// Set clipboard text
        /// </summary>
        /// <param name="text"></param>
        private void SetClipboard(string text)
        {
            Invoke((Action)(() =>
                {
                    Clipboard.SetText(text);
                }));
        }

        /// <summary>
        /// isWorking is used to check whether it is processing an image
        /// </summary>
        private void StopWorking()
        {
            isWorking = false;
        }

        /// <summary>
        /// Checks if the user has selected a region
        /// Enable start button if true
        /// </summary>
        private void DoDurationRegionCheck()
        {
            if (region != Rectangle.Empty)
                ToggleStart(true);
        }

        /// <summary>
        /// Sets status
        /// </summary>
        /// <param name="text"></param>
        delegate void SetStatusDelegate(string text);
        private void SetText(string text)
        {
            if(InvokeRequired)
                Invoke(new SetStatusDelegate(SetText), text);
            else
            {
                labelStatus.Text = "Status: " + text;
            }
        }

        /// <summary>
        /// Enables/Disables "Start" button
        /// </summary>
        /// <param name="active"></param>
        delegate void ToggleStartDelegate(bool active);
        bool startBtnActive = false;
        private void ToggleStart(bool active)
        {
            if (InvokeRequired)
                Invoke(new ToggleStartDelegate(ToggleStart), active);
            else
            {
                if (active)
                {
                    picBtnStart.Image = Properties.Resources.button3;
                    picBtnStart.Cursor = Cursors.Hand;
                }
                else
                {
                    picBtnStart.Image = Properties.Resources.button3_2;
                    picBtnStart.Cursor = Cursors.Arrow;
                }
            }

            startBtnActive = active;
        }

        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkExit_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Minimizes application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkMinimize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Taskbar.GifSetShowing = false;
            Taskbar.RunningInBackground();
            this.Close();
        }
    }
}
