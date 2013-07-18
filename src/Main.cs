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
    public partial class Main : Form
    {
        public Main()
        {
            //Console.WriteLine("This window is used for debugging purposes only");
            InitializeComponent();

            //Hide taskbar when form closes
            FormClosing += (s, e) =>
            {
                Taskbar.Remove();
            };

            //Check and write NGif library 
            string ngif = "NGif.dll";
            if (!File.Exists(ngif))
            {
                try
                {
                    File.WriteAllBytes(ngif, Properties.Resources.NGif);
                }
                catch
                {
                    MessageBox.Show("Error writing '" + ngif + "' to harddisk. This file is required to generate GIF files. Without it, this program will not work.", "Error");
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Set option defaults
            Options.Set("duration", 5, true);
            Options.Set("openafter", true, true);
            Options.Set("upload", true, true);
            Options.Set("fps", 10, true);

            //Load user options
            Options.Load();

            //Setup taskbar
            Taskbar.Update();

            //Open up main form
            Taskbar.OpenGifMain();
        }
    }
}
