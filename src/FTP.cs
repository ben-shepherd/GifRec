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
        //Last uploaded file's web url
        public static string lastUploadUrl;

        //Read only
        private static string FTP_URL = "ftp://gifrec.uni.me/";
        private static string WEB_URL = "http://www.gifrec.uni.me/i/";
        private static NetworkCredential FTP_CRED = new NetworkCredential("USERNAME", "PASSWORD,");

        //Used to cancel uploads
        public static bool AllowUploading;

        /// <summary>
        /// Uploads file to web server
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <param name="SetText">Status method</param>
        /// <returns></returns>
        public static bool Upload(string path, bool newFileName = false)
        {
            AllowUploading = true;
            FileInfo fi = new FileInfo(path);
            lastUploadUrl = null;

            string targetFile = !newFileName ? fi.Name : new FileInfo(Helper.GetRandomFile(ext: fi.Extension.Replace(".", ""))).Name;

            Console.WriteLine("Target file: {0}", targetFile);

            try
            {
                if(!File.Exists(path))
                    throw new Exception("'" + fi.Name + "' does not exist");

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_URL + targetFile);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = FTP_CRED;
                request.ContentLength = fi.Length;

                using (var inputStream = File.OpenRead(path))
                using(var outputStream = request.GetRequestStream())
                {
                    if (AllowUploading)
                    {
                        //Buffer size
                        byte[] buffer = new byte[1024 * 4];
                        int totalBytesRead = 0;
                        int readBytesCount;

                        while ((readBytesCount = inputStream.Read(buffer, 0, buffer.Length)) > 0 && AllowUploading)
                        {
                            outputStream.Write(buffer, 0, readBytesCount);
                            totalBytesRead += readBytesCount;

                            var progress = totalBytesRead * 100 / inputStream.Length;
                                
                            GifSetup.SetTextMethod("Uploading " + (progress != 100 ? progress.ToString() + "%" : ""));
                        }
                    }
                }
                if (AllowUploading)
                {
                    FtpWebResponse resp = (FtpWebResponse)request.GetResponse();
                    Console.WriteLine("FTP Response ({0}): {1}", resp.StatusCode, resp.StatusDescription);
                    resp.Close();

                    lastUploadUrl = WEB_URL + targetFile;
                }
                else
                {
                    Taskbar.Balloon("Your upload has been stopped.", "GifRec", System.Windows.Forms.ToolTipIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Taskbar.Balloon(ex.Message, "Error uploading", System.Windows.Forms.ToolTipIcon.Error);
                return false;
            }

            return true;
        }
    }
}
