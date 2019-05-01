/* ----------------------------------------------------------------------------
Audimat : an audio plugin host
Copyright (C) 2005-2019  George E Greaney

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

using Transonic.Widget;

namespace Audimat.UI
{
    public class KeyboardWnd : Form, IKeyboardWindow
    {
        public AudimatWindow auditwin;
        private ComboBox cbxPlugin;
        public KeyboardBar keyboardBar;

        public KeyboardWnd(AudimatWindow _auditwin)
        {
            InitializeComponent();

            keyboardBar = new KeyboardBar(this, KeyboardBar.Range.SIXTYONE, KeyboardBar.KeySize.FULL);
            keyboardBar.Location = new Point(0, cbxPlugin.Bottom);
            keyboardBar.BackColor = Color.FromArgb(63, 255, 0);
            this.Controls.Add(keyboardBar);

            this.ClientSize = new Size(keyboardBar.Width, keyboardBar.Height + keyboardBar.Top);
        }

        public void onKeyPress(int keyNumber)
        {
        }

        public void onKeyRelease(int keyNumber)
        {
        }

        private void InitializeComponent()
        {
            this.cbxPlugin = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxPlugin
            // 
            this.cbxPlugin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPlugin.FormattingEnabled = true;
            this.cbxPlugin.Location = new System.Drawing.Point(12, 12);
            this.cbxPlugin.Name = "cbxPlugin";
            this.cbxPlugin.Size = new System.Drawing.Size(212, 21);
            this.cbxPlugin.TabIndex = 6;
            // 
            // KeyboardWnd
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(433, 44);
            this.Controls.Add(this.cbxPlugin);
            this.Name = "KeyboardWnd";
            this.ShowInTaskbar = false;
            this.Text = "Audimat Keyboard";
            this.ResumeLayout(false);

        }
    }
}
