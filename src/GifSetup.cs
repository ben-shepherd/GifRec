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
        StartButtonType buttonType; //Start/stop/cancel/disabled button

        //Methods used across classes
        public static Action<string> SetTextMethod;
        public static Action<StartButtonType> ChangeButtonMethod;

        /// <summary>
        /// Check if requirements are met to start gif, enable button if so
        /// <param name="draggable">Used to make the form draggable</param>
        /// </summary>
        public GifSetup()
        {
            InitializeComponent();
            draggable = new FormDraggable(this);

            SetTextMethod = SetText;
            ChangeButtonMethod = ChangeButton;
        }

        public enum StartButtonType
        {
            Start,
            StartDisabled,
            Stop,
            Cancel
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
            picBtnRegion.Cursor = picBtnStart.Cursor = picBtnOptions.Cursor = Cursors.Hand;

            //Disable start button
            ChangeButton(StartButtonType.StartDisabled);

            //Auto resize option
            cbAutoResize.Checked = bool.Parse(Options.Get("autoresize").ToString());

            //High quality option
            cbHq.Checked = bool.Parse(Options.Get("hq").ToString());
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
                        ChangeButton(StartButtonType.Start);
                    }
                };

            rs.Show();
        }

        /// <summary>
        /// Start recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBtnStart_Click(object sender, EventArgs e)
        {
            if (buttonType == StartButtonType.Start)
            {
                GifRecorder gf = new GifRecorder(region, SetText, ChangeButton, StopWorking, SetClipboard);

                ChangeButton(StartButtonType.Stop);
                gf.Start();

            }
            else if (buttonType == StartButtonType.Stop)
            {
                GifRecorder.AllowRecording = false;

                ChangeButton(StartButtonType.Cancel);
                SetText();
            }
            else if (buttonType == StartButtonType.Cancel)
            {
                FTP.AllowUploading = false;
                GifRecorder.AllowProcessing = false;

                ChangeButton(StartButtonType.Start);
                SetText();
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
            //isWorking = false;
        }

        /// <summary>
        /// Checks if the user has selected a region
        /// Enable start button if true
        /// </summary>
        private bool isStartButtonAllowed()
        {
            if (region == Rectangle.Empty)
                return false;

            return true;
        }

        /// <summary>
        /// Sets status
        /// </summary>
        /// <param name="text"></param>
        delegate void SetStatusDelegate(string text);
        private void SetText(string text = "Idle")
        {
            if(InvokeRequired)
                Invoke(new SetStatusDelegate(SetText), text);
            else
            {
                labelStatus.Text = "Status: " + text;
            }
        }

        /// <summary>
        /// Changes the start button type
        /// </summary>
        /// <param name="type"></param>
        delegate void ChangeButtonDelegate(StartButtonType type);
        private void ChangeButton(StartButtonType type)
        {
            if (InvokeRequired)
                Invoke(new ChangeButtonDelegate(ChangeButton), type);
            else
            {
                switch (type.ToString().ToLower())
                {
                    case "start":

                        if (isStartButtonAllowed())
                        {
                            picBtnStart.Image = Properties.Resources.buttonStart;
                            buttonType = StartButtonType.Start;
                        }

                        break;
                    case "stop":

                        picBtnStart.Image = Properties.Resources.buttonStop;
                        buttonType = StartButtonType.Stop;

                        break;
                    case "cancel":

                        picBtnStart.Image = Properties.Resources.buttonCancel;
                        buttonType = StartButtonType.Cancel;

                        break;
                    case "startdisabled":

                        picBtnStart.Image = Properties.Resources.buttonStartDisabled;
                        buttonType = StartButtonType.StartDisabled;

                        break;
                }

                if (buttonType == StartButtonType.StartDisabled)
                    picBtnStart.Cursor = Cursors.Arrow;
                else
                    picBtnStart.Cursor = Cursors.Hand;
            }
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

        /// <summary>
        /// Open the user options form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBtnOptions_Click(object sender, EventArgs e)
        {
            UserInputDialog form = new UserInputDialog();

            form.FormClosing += (s, _e) =>
            {
                ChangeButton(StartButtonType.Start);
            };

            form.ShowDialog(this);
        }

        private void cbAutoResize_Click(object sender, EventArgs e)
        {
            Options.Set("autoresize", cbAutoResize.Checked);
            Options.Save();
        }

        private void cbHq_Click(object sender, EventArgs e)
        {
            Options.Set("hq", cbHq.Checked);
            Options.Save();
        }
    }
}
