using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GifRec
{
    public class FormDraggable
    {
        private Form form;
        private bool down;
        private Point touch;

        public FormDraggable(Form _form)
        {
            form = _form;
            down = false;

            form.MouseDown += new MouseEventHandler(form_MouseDown);
            form.MouseUp += new MouseEventHandler(form_MouseUp);
            form.MouseMove += new MouseEventHandler(form_MouseMove);
        }

        private void form_MouseDown(object sender, MouseEventArgs e)
        {
            down = true;
            touch = e.Location;
        }

        private void form_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }

        private void form_MouseMove(object sender, MouseEventArgs e)
        {
            if (down)
            {
                form.Location = new Point(Cursor.Position.X - touch.X, Cursor.Position.Y - touch.Y);
            }
        }
    }
}
