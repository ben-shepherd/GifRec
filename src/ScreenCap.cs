using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace GifRec
{
    public static class ScreenCap
    {
        public static ImageQuality quality = ImageQuality.High;
        public enum ImageQuality
        {
            Low,
            High
        }

        public static Bitmap Fullscreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                if (quality == ImageQuality.Low)
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                }
                else
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                }

                g.CopyFromScreen(Point.Empty, Point.Empty, Screen.PrimaryScreen.Bounds.Size);
            }

            return bmp;
        }

        public static Bitmap CaptureArea(Rectangle area)
        {
            Bitmap bmp = new Bitmap(area.Width, area.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                if (quality == ImageQuality.Low)
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                }
                else
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                }

                g.CopyFromScreen(new Point(area.X, area.Y), Point.Empty, area.Size);
            }

            return bmp;
        }

        public static Bitmap Crop(Bitmap src, Rectangle area)
        {
            Bitmap target = src.Clone(area, src.PixelFormat);
            return target;
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();

            for (j = 0; j < encoders.Length; j++)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }

            return null;
        }

        public static string SaveTmp(Bitmap src, ImageFormat format = null, bool reduceColors = false)
        {
            if (format == null)
                format = ImageFormat.Bmp;

            string file = System.IO.Path.GetTempFileName();

            if (reduceColors)
            {
                ImageCodecInfo codecInfo;
                Encoder encoder;
                EncoderParameter encPm;
                EncoderParameters encPms;

                codecInfo = GetEncoderInfo("image/gif");
                encoder = Encoder.ColorDepth;
                encPm = new EncoderParameter(encoder, 64L);
                encPms = new EncoderParameters(1);
                encPms.Param[0] = encPm;

                src.Save(file, codecInfo, encPms);

            }
            else
            {
                src.Save(file, format);
            }

            return file;
        }

        public static Bitmap Resize(Bitmap img, double maxWidth, double maxHeight)
        {
            double resizeWidth = img.Width;
            double resizeHeight = img.Height;
            double ratio = resizeWidth / resizeHeight;

            if (resizeWidth > maxWidth)
            {
                resizeWidth = maxWidth;
                resizeHeight = resizeWidth / ratio;
            }
            if (resizeHeight > maxHeight)
            {
                ratio = resizeWidth / resizeHeight;
                resizeHeight = maxHeight;
                resizeWidth = resizeHeight * ratio;
            }

            return new Bitmap(img, new Size((int)resizeWidth, (int)resizeHeight));
        }

        //public static Bitmap Resize(Bitmap bmp, double maxHeight)
        //{
        //    double srcWidth = bmp.Width;
        //    double srcHeight = bmp.Height;

        //    double resWidth = srcWidth;
        //    double resHeight = srcHeight;

        //    double aspect = srcWidth / srcHeight;
        //    Bitmap newImg = null;

        //    if (srcHeight > maxHeight)
        //    {
        //        resHeight = maxHeight;
        //        resWidth = resHeight * aspect;
                
        //        newImg = new Bitmap((int)resWidth, (int)resHeight);
        //        using (Graphics g = Graphics.FromImage(newImg))
        //        {
        //            g.DrawImage(bmp, 0, 0, (int)resWidth, (int)resHeight);
        //        }
        //    }

        //    return newImg != null ? newImg : bmp;
        //}
    }
}
