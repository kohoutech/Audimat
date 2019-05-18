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
using Transonic.Widget;

namespace Audimat
{
    public partial class AudimatWindow : Form
    {
        public String VERSION = "1.2.2";

        public ControlPanel controlPanel;
        public VSTRack rack;

        //child windows
        public PatchWindow patchWin;
        public KeyboardWnd keyboardWnd;

        //backend & i/o
        public Vashti vashti;

        public Settings settings;

        public bool mainShutdown;

        String curpluginPath;
        int vstnum;         //for debugging

        public AudimatWindow()
        {
            vashti = new Vashti();
            settings = new Settings("Audimat.cfg");

            InitializeComponent();


            //control panel goes just below menubar
            controlPanel = new ControlPanel(this);
            this.Controls.Add(controlPanel);
            controlPanel.Location = new Point(this.ClientRectangle.Left, AudimatMenu.Bottom);
            //controlPanel.Anchor = AnchorStyles.Top;
            this.Controls.Add(controlPanel);

            //rack control fills up entire client area between control panel & status bar
            rack = new VSTRack(this, vashti.host);
            rack.Location = new Point(this.ClientRectangle.Left, controlPanel.Bottom);
            this.Controls.Add(rack);
            controlPanel.Width = rack.Width;

            int minHeight = this.AudimatMenu.Height + controlPanel.Height + this.AudimatStatus.Height;
            int rackHeight = settings.getIntValue("global-settings.rack-window-height", VSTPanel.PANELHEIGHT);
            this.ClientSize = new System.Drawing.Size(rack.Size.Width, rackHeight + minHeight);
            this.MinimumSize = new System.Drawing.Size(this.Size.Width, this.Size.Height - rackHeight);
            this.MaximumSize = new System.Drawing.Size(this.Size.Width, Int32.MaxValue);
            int rackX = settings.getIntValue("global-settings.rack-window-pos.x", 100);
            int rackY = settings.getIntValue("global-settings.rack-window-pos.y", 100);
            this.Location = new Point(rackX, rackY);

            patchWin = new PatchWindow(this);

            mainShutdown = false;

            //child windows
            keyboardWnd = new KeyboardWnd(this);
            keyboardWnd.Icon = this.Icon;
            keyboardWnd.FormClosing += new FormClosingEventHandler(keyboardWindow_FormClosing);
            int keyX = settings.getIntValue("global-settings.keyboard-window-pos.x", 200);
            int keyY = settings.getIntValue("global-settings.keyboard-window-pos.y", 200);
            keyboardWnd.Location = new Point(keyX, keyY);
            keyboardWnd.Hide();

            curpluginPath = "";
            vstnum = 0;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (rack != null)
            {
                rack.Size = new Size(this.ClientSize.Width, AudimatStatus.Top - controlPanel.Bottom);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            mainShutdown = true;
            keyboardWnd.Close();

            rack.shutdown();            //unload all plugins in rack
            vashti.shutDown();          //shut down back end

            saveGlobalSettings();
        }

        public void saveGlobalSettings()
        {
            int rackheight = this.ClientSize.Height - (this.AudimatMenu.Height + controlPanel.Height + this.AudimatStatus.Height);
            settings.setStringValue("version", VERSION);
            settings.setIntValue("global-settings.rack-window-height", rackheight);
            settings.setIntValue("global-settings.rack-window-pos.x", this.Location.X);
            settings.setIntValue("global-settings.rack-window-pos.y", this.Location.Y);
            settings.setIntValue("global-settings.keyboard-window-pos.x", keyboardWnd.Location.X);
            settings.setIntValue("global-settings.keyboard-window-pos.y", keyboardWnd.Location.Y);

            settings.saveToFile();
        }

        //callback when plugins have been added or removed from the rack
        public void rackChanged()
        {
            keyboardWnd.updatePluginList();
        }

        //- file menu -----------------------------------------------------------------

        public void loadRig()
        {
        }

        public void newRig()
        {
        }

        public void saveRig(bool rename)
        {
        }
        
        private void loadRigFileMenuItem_Click(object sender, EventArgs e)
        {
            loadRig();
        }

        private void newRigFileMenuItem_Click(object sender, EventArgs e)
        {
            newRig();
        }

        private void saveRigFileMenuItem_Click(object sender, EventArgs e)
        {
            saveRig(false);
        }

        private void saveRigAsFileMenuItem_Click(object sender, EventArgs e)
        {
            saveRig(true);
        }

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //- patch menu --------------------------------------------------------

        public void newPatch()
        {
        }

        public void savePatch(bool rename)
        {
        }

        private void newPatchFileMenuItem_Click(object sender, EventArgs e)
        {
            newPatch();
        }

        private void savePatchPatchMenuItem_Click(object sender, EventArgs e)
        {
            savePatch(false);
        }

        private void savePatchAsPatchMenuItem_Click(object sender, EventArgs e)
        {
            savePatch(true);
        }

        //- plugin menu -------------------------------------------------------

        public void loadPlugin()
        {
            String pluginPath = "";

#if (DEBUG)
            //useful for testing, don't have to go through the FileOPen dialog over & over
            string[] vstlist = File.ReadAllLines("vst.lst");
            pluginPath = vstlist[vstnum++];
            if (vstnum >= vstlist.Length) vstnum = 0;       //wrap it around

#else
            loadPluginDialog.Title = "load a VST";
            loadPluginDialog.InitialDirectory = curpluginPath;
            loadPluginDialog.Filter =  "VST plugins (*.dll)|*.dll|All files (*.*)|*.*";
            loadPluginDialog.ShowDialog();            
            pluginPath = loadPluginDialog.FileName;
            if (pluginPath.Length == 0) return;
            curpluginPath = Path.GetDirectoryName(pluginPath);
#endif
            bool result = rack.addPanel(pluginPath);

            if (!result)
            {
                MessageBox.Show("failed to load the plugin file: " + pluginPath, "Plugin Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadPlugin_Click(object sender, EventArgs e)
        {
            loadPlugin();
        }

        //- rack menu ---------------------------------------------------------

        public void startHost()
        {
            rack.startEngine();
            controlPanel.Invalidate();
            lblAudimatStatus.Text = "Engine is running";
        }

        public void stopHost()
        {
            rack.stopEngine();
            controlPanel.Invalidate();
            lblAudimatStatus.Text = "Engine is stopped";
        }

        private void StartHost_Click(object sender, EventArgs e)
        {
            startHost();
        }

        private void StopHost_Click(object sender, EventArgs e)
        {
            stopHost();
        }

        public void enableKeyboardBarMenuItem(bool enable)
        {
            keyboardBarRackMenuItem.Enabled = enable;
            controlPanel.btnHide.Enabled = enable;
        }

        public void showKeyboardWindow()
        {
            keyboardWnd.updatePluginList();
            keyboardWnd.Show();
            enableKeyboardBarMenuItem(false);
        }

        private void keysButton_Click(object sender, EventArgs e)
        {
            showKeyboardWindow();
        }

        private void keyboardWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mainShutdown)
            {
                e.Cancel = true;
                keyboardWnd.currentPlugin = null;
                keyboardWnd.Hide();
                enableKeyboardBarMenuItem(true);
            }
        }

        private void panicButton_Click(object sender, EventArgs e)
        {
            //not implemented yet
        }

        private void hideShowRackMenuItem_Click(object sender, EventArgs e)
        {
            //not implemented yet
        }

        private void settingsHostMenuItem_Click(object sender, EventArgs e)
        {
            HostSettingsWnd hostsettings = new HostSettingsWnd();
            hostsettings.Icon = this.Icon;
            hostsettings.setSampleRate(vashti.host.sampleRate);
            hostsettings.setBlockSize(vashti.host.blockSize);

            hostsettings.ShowDialog(this);

            if (hostsettings.DialogResult == DialogResult.OK)
            {
                vashti.host.setSampleRate(hostsettings.sampleRate);
                vashti.host.setBlockSize(hostsettings.blockSize);
            }
        }

        //- help menu -----------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Audimat\nversion 1.3.0\n" + "\xA9 Transonic Software 2007-2019\n" + "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
