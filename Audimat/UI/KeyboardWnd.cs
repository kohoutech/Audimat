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
using Transonic.VST;
using Transonic.MIDI;

namespace Audimat.UI
{
    public class KeyboardWnd : Form, IKeyboardWindow
    {
        String[] keySizeStrings = { "25 keys", "37 keys", "49 keys", "61 keys", "76 keys", "88 keys"};
        KeyboardBar.Range[] keySizeVals = { KeyboardBar.Range.TWENTYFIVE, KeyboardBar.Range.THIRTYSEVEN, KeyboardBar.Range.FORTYNINE,
                                              KeyboardBar.Range.SIXTYONE, KeyboardBar.Range.SEVENTYSIX, KeyboardBar.Range.EIGHTYEIGHT};

        public AudimatWindow auditwin;
        public List<VSTPlugin> plugins;
        public VSTPlugin currentPlugin;

        private ComboBox cbxPlugin;
        public ComboBox cbxKeySize;
        public KeyboardBar keyboardBar;

        public KeyboardBar.Range keySize;

        public KeyboardWnd(AudimatWindow _auditwin)
        {
            auditwin = _auditwin;
            currentPlugin = null;

            InitializeComponent();

            keySize = KeyboardBar.Range.SIXTYONE;
            keyboardBar = new KeyboardBar(this, keySize, KeyboardBar.KeySize.FULL, KeyboardBar.KeyMode.PLAYING);
            keyboardBar.Location = new Point(0, cbxPlugin.Bottom);
            keyboardBar.BackColor = Color.FromArgb(63, 255, 0);
            this.Controls.Add(keyboardBar);            

            cbxKeySize.Items.AddRange(keySizeStrings);
            cbxKeySize.SelectedIndex = 3;

            setPluginList();
        }

        private void InitializeComponent()
        {
            this.cbxPlugin = new System.Windows.Forms.ComboBox();
            this.cbxKeySize = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxPlugin
            // 
            this.cbxPlugin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPlugin.FormattingEnabled = true;
            this.cbxPlugin.Location = new System.Drawing.Point(12, 12);
            this.cbxPlugin.Name = "cbxPlugin";
            this.cbxPlugin.Size = new System.Drawing.Size(150, 21);
            this.cbxPlugin.TabIndex = 6;
            this.cbxPlugin.SelectedIndexChanged += new System.EventHandler(this.cbxPlugin_SelectedIndexChanged);
            // 
            // cbxKeySize
            // 
            this.cbxKeySize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKeySize.FormattingEnabled = true;
            this.cbxKeySize.Location = new System.Drawing.Point(341, 12);
            this.cbxKeySize.Name = "cbxKeySize";
            this.cbxKeySize.Size = new System.Drawing.Size(80, 21);
            this.cbxKeySize.TabIndex = 7;
            this.cbxKeySize.SelectedIndexChanged += new System.EventHandler(this.cbxKeySize_SelectedIndexChanged);
            // 
            // KeyboardWnd
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(434, 46);
            this.Controls.Add(this.cbxKeySize);
            this.Controls.Add(this.cbxPlugin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "KeyboardWnd";
            this.ShowInTaskbar = false;
            this.Text = "Audimat Keyboard";
            this.ResumeLayout(false);
        }

        public void setSize(int keySize)
        {
            cbxKeySize.SelectedIndex = keySize;
        }

        public void setPluginList()
        {
            plugins = auditwin.rack.getPluginList();
            if (plugins.Count > 0)
            {
                if (cbxPlugin.DataSource == null) cbxPlugin.Items.Clear();      //remove "no plugins loaded"
                cbxPlugin.DisplayMember = "name";
                cbxPlugin.DataSource = plugins;
                cbxPlugin.Enabled = true;
            }
            else
            {
                cbxPlugin.DisplayMember = null;
                cbxPlugin.DataSource = null;
                cbxPlugin.Items.Add("no plugins loaded");
                cbxPlugin.SelectedIndex = 0;
                cbxPlugin.Enabled = false;
            }
        }

        public void setSelectedPlugin(VSTPlugin keyWindowPlugin)
        {
            //previous cur plugin maybe have been been unloaded, so check for it in plugin list
            for (int i = 0; i < plugins.Count; i++)
            {
                if (plugins[i] == keyWindowPlugin)
                {
                    cbxPlugin.SelectedIndex = i;
                }
            }
        }

        //- event handlers ----------------------------------------------------

        public void onKeyPress(int keyNumber)
        {
            if (currentPlugin != null)
            {
                currentPlugin.sendMidiMessage(new NoteOnMessage(0, keyNumber, 0x60));
            }
        }

        public void onKeyRelease(int keyNumber)
        {
            if (currentPlugin != null)
            {
                currentPlugin.sendMidiMessage(new NoteOffMessage(0, keyNumber, 0x60));
            }
        }

        private void cbxPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (plugins.Count > 0)
            {
                currentPlugin = (VSTPlugin)cbxPlugin.SelectedItem;
            }
            else
            {
                currentPlugin = null;
            }
        }

        private void cbxKeySize_SelectedIndexChanged(object sender, EventArgs e)
        {
            keySize = keySizeVals[cbxKeySize.SelectedIndex];
            keyboardBar.setKeyboardSize(keySize, KeyboardBar.KeySize.FULL);
            ClientSize = new Size(keyboardBar.Width, keyboardBar.Height + keyboardBar.Top);
            cbxKeySize.Location = new Point(this.ClientSize.Width - cbxKeySize.Width - 12, cbxKeySize.Top);                    
        }

        public void updatePluginList()
        {
            setPluginList();
            if (currentPlugin != null)
            {
                setSelectedPlugin(currentPlugin);
            }
        }
    }
}
