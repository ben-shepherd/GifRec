using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using NGif; //Create gif
using ImageMagick;


namespace GifRec
{
    class GifRecorder
    {
        Rectangle region;
        AnimatedGifEncoder gifEncoder;
        int fps = 6;
        string path;
        bool resizeGif;
        bool HQ; 

        Action<string> SetText; //Function used to notify user of updates
        Action<GifSetup.StartButtonType> ChangeButton; //Used to re-enable start button
        Action StopWorking; //Tell the program it's no longer doing any work
        Action<string> SetClipboard;

        //Max width & height
        const int MAX_WIDTH = 350;
        const int MAX_HEIGHT = 350;

        public static bool AllowRecording;
        public static bool AllowProcessing;

        public GifRecorder(Rectangle _region, Action<string> _SetText, Action<GifSetup.StartButtonType> _ToggleStart, Action _StopWorking, Action<string> _SetClipboard)
        {
            region = _region;
            SetText = _SetText;
            ChangeButton = _ToggleStart;
            StopWorking = _StopWorking;
            SetClipboard = _SetClipboard;
        }

        public void Start()
        {
            Options.Load();
            resizeGif = bool.Parse(Options.Get("autoresize").ToString());

            AllowRecording = AllowProcessing = true;
            FTP.AllowUploading = true;
            
            //Set screen capture quality
            HQ = bool.Parse(Options.Get("hq").ToString());
            ScreenCap.quality = HQ ? ScreenCap.ImageQuality.High : ScreenCap.ImageQuality.Low;

            Console.WriteLine("Resize Gif: {0}", resizeGif ? "Yes" : "No");
            Console.WriteLine("High quality: {0}", HQ ? "Yes" : "No");

            //Set path file
            path = Helper.GetRandomFile(ext: "gif");

            //Setup Gif Encoder
            gifEncoder = new AnimatedGifEncoder();
            gifEncoder.Start(path);
            gifEncoder.SetDelay(1000 / fps);
            gifEncoder.SetRepeat(0);

            //Tell user it is recording
            GifSetup.SetTextMethod("Recording");

            //Start recording thread
            System.Threading.Thread recorder = new System.Threading.Thread(RecordGif);
            recorder.Start();
        }

        private void RecordGif()
        {
            double time = 0;
            double max = 10;
            string tmp;
            List<string> frameList = new List<string>();

            while (AllowRecording && time / 1000 != max)
            {
                //Capture frame
                Bitmap frame = ScreenCap.CaptureArea(region);

                //Resize if frame is too large
                if (frame.Width > MAX_WIDTH || frame.Height > MAX_HEIGHT && resizeGif)
                {
                    frame = ScreenCap.Resize(frame, MAX_WIDTH, MAX_HEIGHT);
                }

                ////Get temp file
                //tmp = Helper.GetRandomFile(true);

                ////Save file
                //frame.Save(tmp);

                tmp = ScreenCap.SaveTmp(frame, reduceColors: HQ);
                
                //Add frame to collection
                frameList.Add(tmp);

                //Dispose of frame
                frame.Dispose();

                time += 1000 / fps;
                System.Threading.Thread.Sleep(1000 / fps);
            }

            for (int i = 0; i < frameList.Count; i++)
            {
                if (!AllowProcessing)
                {
                    break;
                }

                var perc = (i + 1) * 100 / frameList.Count;

                GifSetup.SetTextMethod("Creating " + perc.ToString() + "%");

                gifEncoder.AddFrame(Image.FromFile(frameList[i]));
            }

            if (AllowProcessing)
            {
                gifEncoder.Finish();
                RmFrames(frameList);
                HandleProcessedGif();
            }
            else
                GifSetup.SetTextMethod("Idle");
        }

        /// <summary>
        /// Checks user settings, uploads if user has allowed it to upload
        /// </summary>
        private void HandleProcessedGif()
        {
            if (bool.Parse(Options.Get("upload").ToString()))
            {
                Taskbar.Balloon("Uploading GIF");

                SetText("Uploading");

                if (FTP.Upload(path))
                {
                    SetClipboard(FTP.lastUploadUrl);
                    Taskbar.Balloon("Link copied to clipboard", "GifRec Uploaded");

                    if (bool.Parse(Options.Get("openafter").ToString()))
                    {
                        System.Diagnostics.Process.Start(FTP.lastUploadUrl);
                    }
                }

                SetText("Idle");
            }
            else
            {
                //Notify user
                Taskbar.Balloon(path, "GIF Saved", OnClick: (Action)(() => { System.Diagnostics.Process.Start(path); }));

                if (bool.Parse(Options.Get("openafter").ToString()))
                    System.Diagnostics.Process.Start(path);
            }

            //Turn start button back on
            ChangeButton(GifSetup.StartButtonType.Start);
            GifSetup.SetTextMethod("Idle");
        }

        /// <summary>
        /// Deletes stored framed files from the computer
        /// </summary>
        private void RmFrames(List<string> frames)
        {
            for (int i = 0; i < frames.Count; i++)
            {
                try
                {
                    File.Delete(frames[i]);
                }
                catch
                {
                    //Do nothing
                }
            }

            frames.Clear();
        }

        #region Old code
        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    //Stop adding frames if the second count equals 10, or the user has pressed the "Stop" button
        //    if (!AllowRecording || ms / 1000 == 10)
        //    {
        //        //Save gif
        //        gifEncoder.Finish();

        //        //Disable timer
        //        timer.Enabled = false;

        //        ////Notify user
        //        //SetText("Creating GIF");

        //        //Start generating gif
        //        //System.Threading.Thread genGifThread = new System.Threading.Thread(GenerateGif);
        //        //genGifThread.Start();

        //        HandleProcessedGif();

        //        //Stop
        //        return;
        //    }

        //    //Capture frame
        //    using (Bitmap bmp = ScreenCap.Resize(ScreenCap.CaptureArea(region), MAX_WIDTH, MAX_HEIGHT))
        //    {
        //        //Save
        //        //string tmp = ScreenCap.SaveTmp(bmp, reduceColors: true);

        //        gifEncoder.AddFrame(bmp);
        //    }

        //    //Add frame
        //    //frames.Add(tmp);

        //    //frames.Add(ScreenCap.SaveTmp(ScreenCap.CaptureArea(region), reduceColors: true));

        //    //Update time
        //    SetText("Recording");
        //    ms += 1000 / fps;
        //}

        //private void GenerateGif()
        //{
        //    AllowProcessing = true;

        //    //NGif
        //    AnimatedGifEncoder e = new AnimatedGifEncoder();
        //    e.Start(path);
        //    e.SetDelay(1000 / fps);
        //    e.SetRepeat(0);

        //    //Loop through and add frames, update percentage
        //    for (int i = 0; i < frames.Count; i++)
        //    {
        //        if (AllowProcessing)
        //        {
        //            e.AddFrame(Image.FromFile(frames[i]));

        //            var percent = (i + 1) * 100 / frames.Count;

        //            SetText("Working... " + percent.ToString() + "%");
        //        }
        //        else
        //            break;
        //    }
        //    e.Finish();

        //    System.Threading.Thread delTmpFramesThread = new System.Threading.Thread(RmFrames);
        //    delTmpFramesThread.Start();

        //    if (!AllowProcessing)
        //    {
        //        SetText("Idle");
        //        return;
        //    }


        //    //Update status
        //    SetText("Done");

        //    //Notify user
        //    Taskbar.Balloon(path, "GIF Saved", OnClick: (Action)(() => { System.Diagnostics.Process.Start(path);  }));

        //    if(bool.Parse(Options.Get("upload").ToString()))
        //    {
        //        SetText("Uploading");

        //        if (FTP.Upload(path))
        //        {
        //            SetClipboard(FTP.lastUploadUrl);
        //            Taskbar.Balloon("Link copied to clipboard", "GifRec Uploaded");

        //            if (bool.Parse(Options.Get("openafter").ToString()))
        //            {
        //                System.Diagnostics.Process.Start(FTP.lastUploadUrl);
        //            }
        //        }

        //        SetText("Idle");
        //    }
        //    else
        //    {
        //        if (bool.Parse(Options.Get("openafter").ToString()))
        //            System.Diagnostics.Process.Start(path);
        //    }

        //    //Set isWorking to false
        //    StopWorking();

        //    //Turn start button back on
        //    ChangeButton(GifSetup.StartButtonType.Start);
        //}
        #endregion
    }
}
