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

using Audimat.UI;
using Transonic.Widget;

namespace Audimat
{
    public partial class AudimatWindow : Form
    {
        public ControlPanel controlPanel;
        public VSTRack rack;

        //child windows
        public PatchWindow patchWin;
        public MixerWnd mixerWnd;
        public KeyboardWnd keyboardWnd;

        public Settings settings;

        public bool mainShutdown;           //hide child windows instead of closing them until main prog shuts down
        public bool rackhidden;
        public int curRackHeight;
        public int minHeight;

        public AudimatWindow()
        {
            settings = new Settings();          //read prog settings in from config file

            InitializeComponent();

            //control panel goes just below menubar
            controlPanel = new ControlPanel(this);
            this.Controls.Add(controlPanel);
            controlPanel.Location = new Point(this.ClientRectangle.Left, AudimatMenu.Bottom);
            this.Controls.Add(controlPanel);

            //rack control fills up entire client area between control panel & status bar
            rack = new VSTRack(this);
            rack.Location = new Point(this.ClientRectangle.Left, controlPanel.Bottom);
            this.Controls.Add(rack);
            controlPanel.Width = rack.Width;

            //set initial sizes
            minHeight = this.AudimatMenu.Height + controlPanel.Height + this.AudimatStatus.Height;
            int rackHeight = settings.rackHeight;
            this.ClientSize = new System.Drawing.Size(rack.Size.Width, rackHeight + minHeight);
            this.MinimumSize = new System.Drawing.Size(this.Size.Width, this.Size.Height - rackHeight);
            this.MaximumSize = new System.Drawing.Size(this.Size.Width, Int32.MaxValue);
            this.Location = new Point(settings.rackPosX, settings.rackPosY);
            curRackHeight = rack.Height;

            controlPanel.newRig();              //initial empty rig

            //child windows
            keyboardWnd = new KeyboardWnd(controlPanel);
            keyboardWnd.Icon = this.Icon;
            keyboardWnd.FormClosing += new FormClosingEventHandler(keyboardWindow_FormClosing);
            keyboardWnd.Location = new Point(settings.keyWndPosX, settings.keyWndPosY);
            keyboardWnd.Hide();

            mixerWnd = new MixerWnd(this);
            patchWin = new PatchWindow(this);

            mainShutdown = false;
            rackhidden = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (rack != null)
            {
                rack.Size = new Size(this.ClientSize.Width, AudimatStatus.Top - controlPanel.Bottom);
                if (!rackhidden)
                {
                    settings.rackHeight = this.ClientSize.Height - (this.AudimatMenu.Height + controlPanel.Height + this.AudimatStatus.Height);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mainShutdown = true;

            settings.keyWndPosX = keyboardWnd.Location.X;
            settings.keyWndPosY = keyboardWnd.Location.Y;
            keyboardWnd.Close();

            controlPanel.shutdown();

            settings.rackPosX = this.Location.X;
            settings.rackPosY = this.Location.Y;
            settings.save();
        }

        //- file menu -----------------------------------------------------------------

        private void loadRigFileMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.loadRig();
        }

        private void newRigFileMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.newRig();
        }

        public void enableSaveRigMenuItem(bool enable)
        {
            saveRigFileMenuItem.Enabled = enable;
            controlPanel.btnSaveRig.Enabled = enable;
        }

        private void saveRigFileMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.saveRig(false);
        }

        private void saveRigAsFileMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.saveRig(true);
        }

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //- patch menu --------------------------------------------------------

        private void newPatchMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.newPatch();
        }

        private void savePatchMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.savePatch();
        }

        private void savePatchAsMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.saveNewPatch();
        }

        //- host menu ---------------------------------------------------------

        private void StartHost_Click(object sender, EventArgs e)
        {
            controlPanel.startHost();
        }

        private void StopHost_Click(object sender, EventArgs e)
        {
            controlPanel.stopHost();
        }

        public void enableKeyboardBarMenuItem(bool enable)
        {
            keyboardBarRackMenuItem.Enabled = enable;
            controlPanel.btnKeys.Enabled = enable;
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
            controlPanel.panicButton();
        }

        public void hideRack()
        {
            if (!rackhidden)
            {
                rackhidden = true;
                curRackHeight = rack.Height;
                this.ClientSize = new System.Drawing.Size(rack.Size.Width, minHeight);
                this.MaximumSize = this.MinimumSize;
            }
            else
            {
                rackhidden = false;
                this.MaximumSize = new System.Drawing.Size(this.Size.Width, Int32.MaxValue);
                this.ClientSize = new System.Drawing.Size(rack.Size.Width, curRackHeight + minHeight);
            }            
        }

        private void hideShowRackMenuItem_Click(object sender, EventArgs e)
        {
            hideRack();
        }

        public void showMixerWindow()
        {
            String msg = "the mixer window is coming soon\n" + "have patience!";
            MessageBox.Show(msg, "Coming soon");            
        }

        private void settingsHostMenuItem_Click(object sender, EventArgs e)
        {
            controlPanel.updateHostSettings();
        }

        //- plugin menu -------------------------------------------------------

        private void loadPlugin_Click(object sender, EventArgs e)
        {
            controlPanel.loadPlugin();
        }

        //- help menu -----------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Audimat\nversion " + Settings.VERSION + "\n\xA9 Transonic Software 2007-2019\n" + "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }

        //- status bar -----------------------------------------------------------------

        public void setStatusText(String text)
        {
            lblAudimatStatus.Text = text;
        }
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
