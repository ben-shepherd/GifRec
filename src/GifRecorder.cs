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
        //File paths are stored here
        List<string> frames = new List<string>();

        //Timer used to capture frames
        Timer timer;

        //Crop area
        Rectangle region;

        int duration = 5; //Default duration
        int fps = 4; //Frames per second
        int ms = 0; //Current millisecond

        string path; //GIF path

        Action<string> SetText; //Function used to notify user of updates
        Action<bool> ToggleStart; //Used to re-enable start button
        Action StopWorking; //Tell the program it's no longer doing any work
        Action<string> SetClipboard;

        //Max width & height
        const int MAX_WIDTH = 500;
        const int MAX_HEIGHT = 500;

        public GifRecorder(Rectangle _region, Action<string> _SetText, Action<bool> _ToggleStart, Action _StopWorking, Action<string> _SetClipboard)
        {
            region = _region;
            duration = int.Parse(Options.Get("duration").ToString());
            fps = int.Parse(Options.Get("fps").ToString());
            SetText = _SetText;
            ToggleStart = _ToggleStart;
            StopWorking = _StopWorking;
            SetClipboard = _SetClipboard;

            //Generate random file path
            SetPath();
        }

        public void SetPath(bool useTmp = false)
        {
            if (useTmp)
            {
                path = Path.GetTempFileName();
            }
            else
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifRec\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path += RandomString.Create(5) + ".gif";
            }
        }

        public void Start()
        {
            //Clear any frames
            frames.Clear();
            
            //Set screen capture quality
            ScreenCap.quality = ScreenCap.ImageQuality.Low;

            //Set tmp file
            SetPath();

            //Setup timer
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000 / fps;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //If milliseconds equals duration, then recording is complete
            if (ms / 1000 == duration)
            {
                //Disable timer
                timer.Enabled = false;

                //Notify user
                SetText("Creating GIF");

                //Start generating gif
                System.Threading.Thread genGifThread = new System.Threading.Thread(GenerateGif);
                genGifThread.Start();

                //Stop
                return;
            }

            //Capture frame
            Bitmap bmp = ScreenCap.CaptureArea(region);

            //Resize if need to
            bmp = ScreenCap.Resize(bmp, MAX_WIDTH, MAX_HEIGHT);

            //Save
            string tmp = ScreenCap.SaveTmp(bmp, reduceColors: true);

            //Dispose
            bmp.Dispose();

            //Add frame
            frames.Add(tmp);

            //frames.Add(ScreenCap.SaveTmp(ScreenCap.CaptureArea(region), reduceColors: true));

            //Update time
            SetText("Elapsed time " + (ms / 1000));
            ms += 1000 / fps;
        }

        private void GenerateGif()
        {
            //NGif
            AnimatedGifEncoder e = new AnimatedGifEncoder();
            e.Start(path);
            e.SetDelay(1000 / fps);
            e.SetRepeat(0);

            //Loop through and add frames, update percentage
            for (int i = 0; i < frames.Count; i++)
            {
                e.AddFrame(Image.FromFile(frames[i]));

                var percent = (i + 1) * 100 / frames.Count;

                SetText("Working... " + percent.ToString() + "%");
            }
            e.Finish();


            //Update status
            SetText("Done");

            //Notify user
            Taskbar.Balloon(path, "GIF Saved", OnClick: (Action)(() => { System.Diagnostics.Process.Start(path);  }));

            if(bool.Parse(Options.Get("upload").ToString()))
            {
                SetText("Uploading");

                if (FTP.Upload(path, SetText))
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
                if (bool.Parse(Options.Get("openafter").ToString()))
                    System.Diagnostics.Process.Start(path);
            }

            //Set isWorking to false
            StopWorking();

            //Turn start button back on
            ToggleStart(true);
        }
    }
}
