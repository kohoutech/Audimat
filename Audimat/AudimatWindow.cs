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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Audimat.UI;
using Transonic.VST;
using Transonic.MIDI.System;

namespace Audimat
{
    public partial class AudimatWindow : Form
    {
        public VSTRack rack;

        public PatchWindow patchWin;

        public Vashti vashti;
        public WaveDevices waveDevices;
        public MidiSystem midiDevices;

        public AudimatWindow()
        {
            InitializeComponent();

            //rack control fills up entire client area between menu/tool & status bars
            rack = new VSTRack(this);
            //rack.Size = new Size(this.ClientSize.Width, AudimatStatus.Top - AudimatToolbar.Bottom);
            rack.Location = new Point(this.ClientRectangle.Left, AudimatToolbar.Bottom);
            this.Controls.Add(rack);

            int minHeight = this.AudimatMenu.Height + this.AudimatToolbar.Height + this.AudimatStatus.Height;
            this.ClientSize = new System.Drawing.Size(rack.Size.Width, rack.Size.Height + minHeight);
            this.MinimumSize = new System.Drawing.Size(this.Size.Width, this.Size.Height - VSTPanel.PANELHEIGHT);
            this.MaximumSize = new System.Drawing.Size(this.Size.Width, Int32.MaxValue);

            patchWin = new PatchWindow(this);

            vashti = new Vashti();
            waveDevices = new WaveDevices();
            midiDevices = new MidiSystem();            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (rack != null)
            {
                rack.Size = new Size(this.ClientSize.Width, AudimatStatus.Top - AudimatToolbar.Bottom);
            }
        }
        

//- file menu -----------------------------------------------------------------

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//- plugin menu -------------------------------------------------------

        public void loadPlugin()
        {
            String pluginPath = "";
            string[] vstlist = File.ReadAllLines("vst.list");
            pluginPath = vstlist[0];
            //loadPluginDialog.InitialDirectory = Application.StartupPath;
            //loadPluginDialog.ShowDialog();
            //pluginPath = loadPluginDialog.FileName;
            //if (pluginPath.Length == 0) return;

            bool result = rack.loadPlugin(pluginPath);

            if (result)
            {
                //plugLoaded[plugNum] = true;
                //plugloadItems[plugNum].Text = "Unload Plugin " + pluginLetters[plugNum];
                //plugselectItems[plugNum].Enabled = true;
                //plugselectButtons[plugNum].Enabled = true;
                //setCurrentPlugin(plugNum);
            }
            else
            {
                MessageBox.Show("failed to load the plugin file: " + pluginPath, "Plugin Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadPlugin_Click(object sender, EventArgs e)
        {
            loadPlugin();
        }

        //- host menu -----------------------------------------------------------------

        private void StartHost_Click(object sender, EventArgs e)
        {
            vashti.startEngine();
            lblAudimatStatus.Text = "Engine is running";
        }

        private void StopHost_Click(object sender, EventArgs e)
        {
            vashti.stopEngine();
            lblAudimatStatus.Text = "Engine is stopped";
        }
        
        //- help menu -----------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Audimat\nversion 1.1.0\n" + "\xA9 Transonic Software 2007-2019\n" + "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }

        //- toolbar -----------------------------------------------------------------

        private void patchButton_Click(object sender, EventArgs e)
        {
            patchWin.Show();
        }

        private void keysButton_Click(object sender, EventArgs e)
        {

        }

        private void panicButton_Click(object sender, EventArgs e)
        {

        }

    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
