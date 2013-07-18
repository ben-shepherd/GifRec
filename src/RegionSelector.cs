using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GifRec
{
    public partial class RegionSelector : Form
    {
        bool selected = false;
        Rectangle area = Rectangle.Empty;

        bool mouseDown = false;
        Point sp, ep = Point.Empty;

        public bool SelectedRegion
        {
            get { return selected; }
        }

        public Rectangle Region
        {
            get { return area; }
        }

        Image background;

        public RegionSelector()
        {
            InitializeComponent();

            background = (Image)ScreenCap.Fullscreen();
            picBox.MouseDown += new MouseEventHandler(picBox_MouseDown);
            picBox.MouseUp += new MouseEventHandler(picBox_MouseUp);
            picBox.MouseMove += new MouseEventHandler(picBox_MouseMove);
            picBox.Paint += new PaintEventHandler(picBox_Paint);
        }

        private void RegionSelector_Load(object sender, EventArgs e)
        {
            picBox.Image = background;
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                this.Close();
            else
            {
                mouseDown = true;
                sp = ep = e.Location;
            }
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                ep = e.Location;
                picBox.Invalidate();
            }
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            area = GetRectangle(sp, ep);

            if (area.Width > 10 && area.Height > 10)
            {
                mouseDown = false;
                ep = e.Location;
                selected = true;
            }

            this.Close();
        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Blue, GetRectangle(sp, ep));
        }

        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                    Math.Min(p1.X, p2.X),
                    Math.Min(p1.Y, p2.Y),
                    Math.Abs(p1.X - p2.X),
                    Math.Abs(p1.Y - p2.Y)
                );
        }
    }
}
