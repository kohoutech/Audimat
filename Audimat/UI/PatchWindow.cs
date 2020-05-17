/* ----------------------------------------------------------------------------
Audimat : an audio plugin host
Copyright (C) 2005-2020  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Kohoutech.Patch;

namespace Audimat.UI
{
    public class PatchWindow : Form
    {
        public AudimatWindow audimatWindow;
        public PatchCanvas canvas; 


        public PatchWindow(AudimatWindow _audimatWindow)
        {
            audimatWindow = _audimatWindow;

            canvas = new PatchCanvas();
            canvas.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            canvas.Location = new Point(0, 0);
            canvas.BackColor = Color.LawnGreen;
            this.Controls.Add(canvas);

            this.ShowInTaskbar = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (canvas != null)
            {
                canvas.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
