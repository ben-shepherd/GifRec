using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace GifRec
{
    public static class CaptureMethods
    {
        /// <summary>
        /// This is to stop users doing tasks more than once, may change in the future
        /// </summary>
        public static bool working = false;

        /// <summary>
        /// Captures the full screen
        /// </summary>
        public static void Fullscreen()
        {
            if(!working)
                HandleImage(ScreenCap.Fullscreen());
        }

        /// <summary>
        /// Allows the user to capture parts of their screen
        /// </summary>
        public static void Area()
        {
            if (!working)
            {
                working = true;
                var bmp = ScreenCap.Fullscreen();

                RegionSelector rs = new RegionSelector();
                rs.FormClosing += (s, e) =>
                    {
                        if (rs.SelectedRegion)
                        {
                            Rectangle region = rs.Region;
                            bmp = ScreenCap.Crop(bmp, region);

                            HandleImage(bmp);

                        }
                    };
                rs.Show();
            }
        }

        public static void FromComputer()
        {
            if (!working)
            {
                working = true;

                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
                open.ShowDialog();

                if (open.FileName.Length > 0)
                {
                    try
                    {
                        var image = Image.FromFile(open.FileName);
                    }
                    catch
                    {
                        
                        Taskbar.Balloon("This file is not an image file and cannot be uploaded", "GifRec", ToolTipIcon.Error);
                        working = false;

                        return;
                    }

                    new Thread(delegate()
                        {
                            Taskbar.Balloon("Uploading...");
                            GifSetup.SetTextMethod("Preparing...");
                            GifSetup.ChangeButtonMethod(GifSetup.StartButtonType.StartDisabled);

                            if (FTP.Upload(open.FileName, true))
                            {
                                System.Diagnostics.Process.Start(FTP.lastUploadUrl);
                            }

                            GifSetup.SetTextMethod("Idle");
                            GifSetup.ChangeButtonMethod(GifSetup.StartButtonType.Start);

                            working = false;

                        }).Start();
                }
            }
        }

        /// <summary>
        /// Handles the image sent through
        /// </summary>
        /// <param name="bmp"></param>
        static void HandleImage(Bitmap bmp)
        {
            GifSetup.ChangeButtonMethod(GifSetup.StartButtonType.StartDisabled);

            string file = Helper.GetRandomFile(ext: "jpg");
            bmp.Save(file, ImageFormat.Jpeg);

            if (bool.Parse(Options.Get("upload").ToString()))
            {
                new Thread(new ThreadStart(((Action)(() =>
                    {
                        Taskbar.Balloon("GifRec is uploading your image", "GifRec");
                        GifSetup.SetTextMethod("Preparing...");

                        if (FTP.Upload(file))
                        {
                            System.Diagnostics.Process.Start(FTP.lastUploadUrl);
                        }

                        working = false;
                        GifSetup.ChangeButtonMethod(GifSetup.StartButtonType.Start);
                        GifSetup.SetTextMethod("Idle");

                    })))).Start();
            }
            else
            {
                if(bool.Parse(Options.Get("openafter").ToString()))
                {
                    System.Diagnostics.Process.Start(file);
                }

                working = false;
                GifSetup.ChangeButtonMethod(GifSetup.StartButtonType.Start);
                GifSetup.SetTextMethod("Idle");
            }

            bmp.Dispose();
        }
    }
}
