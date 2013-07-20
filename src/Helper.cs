using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GifRec
{
    public static class Helper
    {
        public static string RanString(int length)
        {
            Random random = new Random();
            string chars = "";
            string str = "";

            chars = "qwertyuiopasdfghjklzxcvbnm";
            chars += chars.ToUpper();

            for (int i = 0; i < length; i++)
                str += chars[random.Next(chars.Length - 1)];

            return str;
        }


        public static string GetRandomFile(bool useTmp = false, string ext = "tmp")
        {
            string path;

            if (useTmp)
            {
                path = Path.GetTempFileName();
            }
            else
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifRec\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path += RanString(5) + "." + ext;
            }

            return path;
        }
    }
}
