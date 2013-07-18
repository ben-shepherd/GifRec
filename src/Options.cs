using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GifRec
{
    public static class Options
    {
        private static string path = null;
        private static Dictionary<string, object> optionsDict = new Dictionary<string, object>();
        private static Dictionary<string, object> defDict = new Dictionary<string, object>();

        static void init()
        {
            if (path == null)
            {
                string dir;

                dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GifRec\\";

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                path = dir + "settings.dat";

            }
        }

        public static void Set(string key, object value)
        {
            optionsDict[key] = value;
        }

        public static void Set(string key, object value, bool asDefault)
        {
            if (asDefault)
                optionsDict[key] = defDict[key] = value;
            else
                Set(key, value);
        }

        public static object Get(string key)
        {
            return optionsDict.ContainsKey(key) ? optionsDict[key] : null;
        }

        public static void Load()
        {
            init();
            optionsDict.Clear();

            foreach (var pair in defDict)
            {
                optionsDict[pair.Key] = pair.Value;
            }

            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains('='))
                            {
                                string[] split = line.Split('=');
                                optionsDict[split[0]] = split[1];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Taskbar.Balloon(ex.Message, "Error saving options", System.Windows.Forms.ToolTipIcon.Error);
            }
        }

        public static void Save()
        {
            init();

            try
            {
                string str = string.Empty;

                foreach (var pair in optionsDict)
                {
                    str += string.Format("{0}={1}\r\n", pair.Key, pair.Value);
                }

                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(str);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Taskbar.Balloon(ex.Message, "Error saving options", System.Windows.Forms.ToolTipIcon.Error);
            }
        }
    }
}
