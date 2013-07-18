using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GifRec
{
    public static class FTP
    {
        public static string lastUploadUrl;
        private static string ftpurl = "ftp://gifrec.uni.me/";
        private static string weburl = "http://www.gifrec.uni.me/i/";
        private static NetworkCredential ftpcred = new NetworkCredential("appadmin@gifrec.uni.me", "OgE^)gZV!(J[");

        /// <summary>
        /// Uploads file to web server
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <param name="SetText">Status method</param>
        /// <returns></returns>
        public static bool Upload(string path, Action<string> SetText)
        {
            FileInfo fi = new FileInfo(path);
            lastUploadUrl = null;

            try
            {
                if(!File.Exists(path))
                    throw new Exception("'" + fi.Name + "' does not exist");

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpurl + fi.Name);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = ftpcred;
                request.ContentLength = fi.Length;

                using (var inputStream = File.OpenRead(path))
                using(var outputStream = request.GetRequestStream())
                {
                    //Buffer size
                    byte[] buffer = new byte[4096];
                    int totalBytesRead = 0;
                    int readBytesCount;

                    while((readBytesCount = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputStream.Write(buffer, 0, readBytesCount);
                        totalBytesRead += readBytesCount;

                        var progress = totalBytesRead * 100 / inputStream.Length;

                        SetText("Uploading " + (progress != 100 ? progress.ToString() + "%" : ""));

                    }
                }

                lastUploadUrl = weburl + fi.Name;
            }
            catch (Exception ex)
            {
                Taskbar.Balloon(ex.Message, "Error uploading", System.Windows.Forms.ToolTipIcon.Error);
                return false;
            }

            return true;
        }
    }

    //This doesn't work :-(

    //public static class Imgur
    //{
    //    static string url = "http://api.imgur.com/3/upload.xml";
    //    static string clientId = "";
    //    static string secret = "";

    //    public static void UploadFile(string path)
    //    {
    //        try
    //        {
    //            using (var w = new WebClient())
    //            {
    //                var values = new NameValueCollection {
    //                    {
    //                        "image", Convert.ToBase64String(File.ReadAllBytes(path))
    //                    }
    //                };

    //                w.Headers.Add("Authorization", "Client-ID " + clientId);
    //                byte[] response = w.UploadValues(Imgur.url, values);

    //                System.Windows.Forms.MessageBox.Show(Encoding.ASCII.GetString(response));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Taskbar.Balloon(ex.Message, "Error uploading", System.Windows.Forms.ToolTipIcon.Error);
    //        }
    //    }
    //}
}
