using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GifRec
{
    public static class Taskbar
    {
        static ContextMenu menu = null;
        static NotifyIcon icon = new NotifyIcon();

        static GifSetup gs = null;
        static bool gsShowing = false;

        public static bool GifSetShowing
        {
            get { return gsShowing; }
            set { gsShowing = value; Update(); }
        }

        /// <summary>
        /// Displays a balloon in the system icon tray with text and a title
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <param name="_icon"></param>
        /// <param name="duration"></param>
        /// <param name="OnClick"></param>
        public static void Balloon(string text, string title, ToolTipIcon _icon = ToolTipIcon.Info, int duration = 3000, Action OnClick = null)
        {
            if (icon != null)
            {
                icon.BalloonTipText = text;
                icon.BalloonTipTitle = title;
                icon.BalloonTipIcon = _icon;
                icon.ShowBalloonTip(duration);
                icon.BalloonTipClicked += (s, e) =>
                    {
                        if(OnClick != null)
                            OnClick();
                    };
            }
        }

        /// <summary>
        /// Updates the icon in the icon tray
        /// </summary>
        public static void Update()
        {
            menu = new ContextMenu();

            menu.MenuItems.Add(gsShowing ? "Hide window" : "Show window").Click += (s, e) =>
            {
                if (gsShowing)
                {
                    CloseGifMain();
                }
                else
                {
                    OpenGifMain();
                }
            };
            menu.MenuItems.Add("-");
            menu.MenuItems.Add("Exit").Click += (s, e) =>
            {
                Environment.Exit(0);
            };

            icon.ContextMenu = menu;
            icon.Icon = Properties.Resources.favicon;
            icon.Visible = true;
            icon.Click += (s, e) => { if(!gsShowing) OpenGifMain(); };

        }

        /// <summary>
        /// Open up main form
        /// </summary>
        public static void OpenGifMain()
        {
            if (!gsShowing)
            {
                gs = new GifSetup();
                gs.Show();
                gsShowing = true;
            }
        }

        /// <summary>
        /// Close form
        /// </summary>
        public static void CloseGifMain()
        {
            if (gs != null && gsShowing)
            {
                gsShowing = false;
                gs.Close();
            }
        }

        /// <summary>
        /// Hide taskbar icon
        /// </summary>
        public static void Remove()
        {
            icon.Visible = false;
        }

        /// <summary>
        /// Notifies user that app is running in the background
        /// </summary>
        public static void RunningInBackground()
        {
            Taskbar.Balloon("GifRec is running in the background", "GifRec", OnClick: OpenGifMain);
        }
    }
}
