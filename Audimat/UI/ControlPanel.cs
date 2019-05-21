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
using System.Drawing.Drawing2D;
using System.IO;

using Audimat.Graph;
using Transonic.VST;
using Transonic.MIDI.System;

namespace Audimat.UI
{
    public class ControlPanel : UserControl
    {
        public AudimatWindow auditwin;

        //controls
        private System.ComponentModel.IContainer components;
        public ComboBox cbxPatchList;
        private Button btnNextPatch;
        private Button btnPrevPatch;
        private Button btnLoadRig;
        private Button btnNewRig;
        public Button btnSaveRig;
        private Button btnSaveRigAs;
        private Button btnNewPatch;
        private Button btnSavePatchAs;
        private Button btnSavePatch;
        private Button btnLoadPlugin;
        private Button btnKeys;
        private Button btnMixer;
        public Button btnPanic;
        public Button btnHide;
        public Button btnStart;
        public Button btnStop;
        private ToolTip controlPanelToolTip;

        //child windows
        public PatchNameWnd patchNameWnd;

        //dialogs
        private OpenFileDialog loadRigDialog;
        private SaveFileDialog saveRigDialog;
        private OpenFileDialog loadPluginDialog;

        //backend & i/o
        public Vashti vashti;
        public VSTHost host;
        public MidiSystem midiDevices;

        //model
        public VSTRig currentRig;

        String curRigFilename;
        String curRigPath;
        String curPluginPath;

        //status
        public bool isRunning;

        int vstnum;         //for debugging

        public ControlPanel(AudimatWindow _auditwin)
        {
            auditwin = _auditwin;

            //init backend
            vashti = new Vashti();
            host = new VSTHost(vashti);
            midiDevices = new MidiSystem();

            InitializeComponent();

            //child windows
            patchNameWnd = null;

            //model
            currentRig = new VSTRig(this);              //empty rig w/ no modules or patches
            auditwin.Text = "Audimat - new rig";
            curRigFilename = null;
            curRigPath = Application.StartupPath;
            curPluginPath = Application.StartupPath;

            vstnum = 0;

            isRunning = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnPanic = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.controlPanelToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveRig = new System.Windows.Forms.Button();
            this.btnNextPatch = new System.Windows.Forms.Button();
            this.btnPrevPatch = new System.Windows.Forms.Button();
            this.btnLoadRig = new System.Windows.Forms.Button();
            this.btnNewRig = new System.Windows.Forms.Button();
            this.btnSaveRigAs = new System.Windows.Forms.Button();
            this.btnLoadPlugin = new System.Windows.Forms.Button();
            this.btnKeys = new System.Windows.Forms.Button();
            this.btnMixer = new System.Windows.Forms.Button();
            this.btnSavePatchAs = new System.Windows.Forms.Button();
            this.btnSavePatch = new System.Windows.Forms.Button();
            this.btnNewPatch = new System.Windows.Forms.Button();
            this.cbxPatchList = new System.Windows.Forms.ComboBox();
            this.loadRigDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveRigDialog = new System.Windows.Forms.SaveFileDialog();
            this.loadPluginDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnPanic
            // 
            this.btnPanic.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPanic.Location = new System.Drawing.Point(279, 37);
            this.btnPanic.Name = "btnPanic";
            this.btnPanic.Size = new System.Drawing.Size(24, 24);
            this.btnPanic.TabIndex = 15;
            this.btnPanic.Text = "P";
            this.controlPanelToolTip.SetToolTip(this.btnPanic, "panic button!");
            this.btnPanic.UseVisualStyleBackColor = false;
            this.btnPanic.Click += new System.EventHandler(this.btnPanic_Click);
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnHide.Location = new System.Drawing.Point(255, 37);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(24, 24);
            this.btnHide.TabIndex = 14;
            this.btnHide.Text = "H";
            this.controlPanelToolTip.SetToolTip(this.btnHide, "show / hide rack");
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(320, 37);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(38, 24);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.controlPanelToolTip.SetToolTip(this.btnStop, "stop engine");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(320, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(38, 24);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.controlPanelToolTip.SetToolTip(this.btnStart, "start engine");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSaveRig
            // 
            this.btnSaveRig.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSaveRig.Location = new System.Drawing.Point(73, 37);
            this.btnSaveRig.Name = "btnSaveRig";
            this.btnSaveRig.Size = new System.Drawing.Size(24, 24);
            this.btnSaveRig.TabIndex = 8;
            this.btnSaveRig.Text = "S";
            this.controlPanelToolTip.SetToolTip(this.btnSaveRig, "save rig");
            this.btnSaveRig.UseVisualStyleBackColor = false;
            this.btnSaveRig.Click += new System.EventHandler(this.btnSaveRig_Click);
            // 
            // btnNextPatch
            // 
            this.btnNextPatch.Enabled = false;
            this.btnNextPatch.Location = new System.Drawing.Point(218, 13);
            this.btnNextPatch.Name = "btnNextPatch";
            this.btnNextPatch.Size = new System.Drawing.Size(18, 24);
            this.btnNextPatch.TabIndex = 5;
            this.btnNextPatch.Text = "+";
            this.controlPanelToolTip.SetToolTip(this.btnNextPatch, "next patch");
            this.btnNextPatch.UseVisualStyleBackColor = true;
            this.btnNextPatch.Click += new System.EventHandler(this.btnNextPatch_Click);
            // 
            // btnPrevPatch
            // 
            this.btnPrevPatch.Enabled = false;
            this.btnPrevPatch.Location = new System.Drawing.Point(25, 12);
            this.btnPrevPatch.Name = "btnPrevPatch";
            this.btnPrevPatch.Size = new System.Drawing.Size(18, 25);
            this.btnPrevPatch.TabIndex = 3;
            this.btnPrevPatch.Text = "-";
            this.controlPanelToolTip.SetToolTip(this.btnPrevPatch, "previous patch");
            this.btnPrevPatch.UseVisualStyleBackColor = true;
            this.btnPrevPatch.Click += new System.EventHandler(this.btnPrevPatch_Click);
            // 
            // btnLoadRig
            // 
            this.btnLoadRig.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLoadRig.Location = new System.Drawing.Point(25, 37);
            this.btnLoadRig.Name = "btnLoadRig";
            this.btnLoadRig.Size = new System.Drawing.Size(24, 24);
            this.btnLoadRig.TabIndex = 6;
            this.btnLoadRig.Text = "L";
            this.controlPanelToolTip.SetToolTip(this.btnLoadRig, "load rig");
            this.btnLoadRig.UseVisualStyleBackColor = false;
            this.btnLoadRig.Click += new System.EventHandler(this.btnLoadRig_Click);
            // 
            // btnNewRig
            // 
            this.btnNewRig.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNewRig.Location = new System.Drawing.Point(49, 37);
            this.btnNewRig.Name = "btnNewRig";
            this.btnNewRig.Size = new System.Drawing.Size(24, 24);
            this.btnNewRig.TabIndex = 7;
            this.btnNewRig.Text = "N";
            this.controlPanelToolTip.SetToolTip(this.btnNewRig, "new rig");
            this.btnNewRig.UseVisualStyleBackColor = false;
            this.btnNewRig.Click += new System.EventHandler(this.btnNewRig_Click);
            // 
            // btnSaveRigAs
            // 
            this.btnSaveRigAs.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSaveRigAs.Location = new System.Drawing.Point(97, 37);
            this.btnSaveRigAs.Name = "btnSaveRigAs";
            this.btnSaveRigAs.Size = new System.Drawing.Size(24, 24);
            this.btnSaveRigAs.TabIndex = 9;
            this.btnSaveRigAs.Text = "A";
            this.controlPanelToolTip.SetToolTip(this.btnSaveRigAs, "save rig as");
            this.btnSaveRigAs.UseVisualStyleBackColor = false;
            this.btnSaveRigAs.Click += new System.EventHandler(this.btnSaveRigAs_Click);
            // 
            // btnLoadPlugin
            // 
            this.btnLoadPlugin.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLoadPlugin.Location = new System.Drawing.Point(212, 37);
            this.btnLoadPlugin.Name = "btnLoadPlugin";
            this.btnLoadPlugin.Size = new System.Drawing.Size(24, 24);
            this.btnLoadPlugin.TabIndex = 13;
            this.btnLoadPlugin.Text = "L";
            this.controlPanelToolTip.SetToolTip(this.btnLoadPlugin, "load plugin");
            this.btnLoadPlugin.UseVisualStyleBackColor = false;
            this.btnLoadPlugin.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnKeys
            // 
            this.btnKeys.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnKeys.Location = new System.Drawing.Point(255, 13);
            this.btnKeys.Name = "btnKeys";
            this.btnKeys.Size = new System.Drawing.Size(24, 24);
            this.btnKeys.TabIndex = 16;
            this.btnKeys.Text = "K";
            this.controlPanelToolTip.SetToolTip(this.btnKeys, "show keyboard window");
            this.btnKeys.UseVisualStyleBackColor = false;
            this.btnKeys.Click += new System.EventHandler(this.btnKeys_Click);
            // 
            // btnMixer
            // 
            this.btnMixer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnMixer.Location = new System.Drawing.Point(279, 13);
            this.btnMixer.Name = "btnMixer";
            this.btnMixer.Size = new System.Drawing.Size(24, 24);
            this.btnMixer.TabIndex = 17;
            this.btnMixer.Text = "M";
            this.controlPanelToolTip.SetToolTip(this.btnMixer, "show mixer window");
            this.btnMixer.UseVisualStyleBackColor = false;
            this.btnMixer.Click += new System.EventHandler(this.btnMixer_Click);
            // 
            // btnSavePatchAs
            // 
            this.btnSavePatchAs.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSavePatchAs.Location = new System.Drawing.Point(179, 37);
            this.btnSavePatchAs.Name = "btnSavePatchAs";
            this.btnSavePatchAs.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatchAs.TabIndex = 12;
            this.btnSavePatchAs.Text = "A";
            this.controlPanelToolTip.SetToolTip(this.btnSavePatchAs, "save patch as");
            this.btnSavePatchAs.UseVisualStyleBackColor = false;
            this.btnSavePatchAs.Click += new System.EventHandler(this.btnSavePatchAs_Click);
            // 
            // btnSavePatch
            // 
            this.btnSavePatch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSavePatch.Location = new System.Drawing.Point(155, 37);
            this.btnSavePatch.Name = "btnSavePatch";
            this.btnSavePatch.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatch.TabIndex = 11;
            this.btnSavePatch.Text = "S";
            this.controlPanelToolTip.SetToolTip(this.btnSavePatch, "save patch");
            this.btnSavePatch.UseVisualStyleBackColor = false;
            this.btnSavePatch.Click += new System.EventHandler(this.btnSavePatch_Click);
            // 
            // btnNewPatch
            // 
            this.btnNewPatch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNewPatch.Location = new System.Drawing.Point(131, 37);
            this.btnNewPatch.Name = "btnNewPatch";
            this.btnNewPatch.Size = new System.Drawing.Size(24, 24);
            this.btnNewPatch.TabIndex = 10;
            this.btnNewPatch.Text = "N";
            this.controlPanelToolTip.SetToolTip(this.btnNewPatch, "new patch");
            this.btnNewPatch.UseVisualStyleBackColor = false;
            this.btnNewPatch.Click += new System.EventHandler(this.btnNewPatch_Click);
            // 
            // cbxPatchList
            // 
            this.cbxPatchList.BackColor = System.Drawing.Color.GreenYellow;
            this.cbxPatchList.Enabled = false;
            this.cbxPatchList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPatchList.ForeColor = System.Drawing.Color.Black;
            this.cbxPatchList.FormattingEnabled = true;
            this.cbxPatchList.Location = new System.Drawing.Point(43, 14);
            this.cbxPatchList.Name = "cbxPatchList";
            this.cbxPatchList.Size = new System.Drawing.Size(175, 23);
            this.cbxPatchList.TabIndex = 4;
            this.cbxPatchList.SelectedIndexChanged += new System.EventHandler(this.cbxPatchList_SelectedIndexChanged);
            // 
            // loadRigDialog
            // 
            this.loadRigDialog.DefaultExt = "rig";
            this.loadRigDialog.Filter = "Audimat rigs (*.rig)|*.rig|All files (*.*)|*.*";
            this.loadRigDialog.Title = "load an Audimat rig";
            // 
            // saveRigDialog
            // 
            this.saveRigDialog.DefaultExt = "rig";
            this.saveRigDialog.Filter = "Audimat rigs (*.rig)|*.rig|All files (*.*)|*.*";
            this.saveRigDialog.Title = "save an Audimat rig";
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.btnNewPatch);
            this.Controls.Add(this.btnSavePatch);
            this.Controls.Add(this.btnSavePatchAs);
            this.Controls.Add(this.btnMixer);
            this.Controls.Add(this.btnKeys);
            this.Controls.Add(this.btnLoadPlugin);
            this.Controls.Add(this.btnSaveRigAs);
            this.Controls.Add(this.btnNewRig);
            this.Controls.Add(this.btnLoadRig);
            this.Controls.Add(this.btnNextPatch);
            this.Controls.Add(this.btnPrevPatch);
            this.Controls.Add(this.cbxPatchList);
            this.Controls.Add(this.btnSaveRig);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnPanic);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(400, 75);
            this.ResumeLayout(false);

        }

        public void shutdown()
        {
            host.stopEngine();
            currentRig.shutDown();      //unload all plugins in rack
            midiDevices.shutdown();     //close midi devices
            vashti.shutDown();          //shut down back end
        }

        //- callbacks -----------------------------------------------------------------

        //callback when rack's patch list has changed (added/deleted/renamed)
        public void rigPatchesChanged(String curPatchName)
        {
            updatePatchList(curPatchName);
            //saveRigFileMenuItem.Enabled = true;
            btnSaveRig.Enabled = true;
        }

        //callback when plugins have been added or removed from the rack
        public void rigModulesChanged()
        {
            auditwin.keyboardWnd.updatePluginList();
            //auditwin.saveRigFileMenuItem.Enabled = true;
            btnSaveRig.Enabled = true;
        }

        public void updatePatchList(String patchName)
        {
            cbxPatchList.Items.Clear();
            cbxPatchList.Text = "";
            foreach (Patch patch in currentRig.patches)
            {
                cbxPatchList.Items.Add(patch.name);
            }
            bool enabled = (cbxPatchList.Items.Count > 0);
            cbxPatchList.Enabled = enabled;
            btnPrevPatch.Enabled = enabled;
            btnNextPatch.Enabled = enabled;
            cbxPatchList.SelectedIndex = (enabled && patchName != null) ? cbxPatchList.FindString(patchName) : -1;
        }

        //- host management ----------------------------------------------------------

        public void startHost()
        {
            host.startEngine();
            isRunning = true;
            Invalidate();
            //auditwin.lblAudimatStatus.Text = "Engine is running";
        }

        public void stopHost()
        {
            host.stopEngine();
            isRunning = false;
            Invalidate();
            //auditwin.lblAudimatStatus.Text = "Engine is stopped";
        }

        //- rigging -------------------------------------------------------------------

        public void loadRig()
        {
            String rigPath = "";

#if (DEBUG)
            rigPath = "test1.rig";
#else
            loadRigDialog.InitialDirectory = curRigPath;
            DialogResult dlgresult = loadRigDialog.ShowDialog(this);
            if (dlgresult == DialogResult.Cancel) return;
            rigPath = loadRigDialog.FileName;            
#endif
            curRigPath = Path.GetDirectoryName(rigPath);
            VSTRig newRig = VSTRig.loadFromFile(rigPath, this);

            if (newRig != null)
            {
                currentRig.shutDown();
                currentRig = newRig;

                curRigFilename = Path.GetFileName(rigPath);
                auditwin.Text = "Audimat - " + Path.GetFileNameWithoutExtension(curRigFilename);
                //saveRigFileMenuItem.Enabled = false;
                btnSaveRig.Enabled = false;
            }
            else
            {
                MessageBox.Show("failed to load the rig file: " + rigPath, "Rig Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void newRig()
        {
            currentRig.shutDown();
            currentRig = new VSTRig(this);

            curRigFilename = null;
            auditwin.Text = "Audimat - new rig";
            //saveRigFileMenuItem.Enabled = false;
            btnSaveRig.Enabled = false;
        }

        public void saveRig(bool rename)
        {
            String rigPath = curRigFilename;
            if (rename || curRigFilename == null)           //rename automatically is we don't already have a name
            {
                saveRigDialog.InitialDirectory = curRigPath;
                DialogResult result = saveRigDialog.ShowDialog(this);
                if (result == DialogResult.Cancel) return;

                rigPath = saveRigDialog.FileName;
                curRigPath = Path.GetDirectoryName(rigPath);
                curRigFilename = Path.GetFileName(rigPath);
            }
            currentRig.saveToFile(rigPath);

            auditwin.Text = "Audimat - " + Path.GetFileNameWithoutExtension(curRigFilename);
            //saveRigFileMenuItem.Enabled = false;
            btnSaveRig.Enabled = false;
        }

        //- patching -------------------------------------------------------------------

        public void newPatch()
        {
            currentRig.clearPatch();
        }

        public void savePatch()
        {
            currentRig.updatePatch();
        }

        public void saveNewPatch()
        {
            patchNameWnd = new PatchNameWnd(this);
            patchNameWnd.Icon = auditwin.Icon;
            if (currentRig.currentPatch != null)
            {
                patchNameWnd.txtPatchname.Text = currentRig.currentPatch.name;
            }

            DialogResult result = patchNameWnd.ShowDialog(this);
            if (result == DialogResult.Cancel) return;

            currentRig.addNewPatch(patchNameWnd.txtPatchname.Text);
        }

        //- plugin management -------------------------------------------------

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
            loadPluginDialog.InitialDirectory = curPluginPath;
            loadPluginDialog.Filter =  "VST plugins (*.dll)|*.dll|All files (*.*)|*.*";
            loadPluginDialog.ShowDialog();            
            pluginPath = loadPluginDialog.FileName;
            if (pluginPath.Length == 0) return;
            curPluginPath = Path.GetDirectoryName(pluginPath);
#endif
            bool result = currentRig.addPanel(pluginPath);

            if (!result)
            {
                MessageBox.Show("failed to load the plugin file: " + pluginPath, "Plugin Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //- patch selector ----------------------------------------------------

        private void cbxPatchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentRig.setCurrentPatch(cbxPatchList.SelectedIndex);
        }

        private void btnPrevPatch_Click(object sender, EventArgs e)
        {
            int curpatch = cbxPatchList.SelectedIndex;
            curpatch--;
            if (curpatch >= 0)
            {
                cbxPatchList.SelectedIndex = curpatch;
            }
        }

        private void btnNextPatch_Click(object sender, EventArgs e)
        {
            int curpatch = cbxPatchList.SelectedIndex;
            curpatch++;
            if (curpatch < cbxPatchList.Items.Count)
            {
                cbxPatchList.SelectedIndex = curpatch;
            }
        }

        //- rig buttons -------------------------------------------------------

        private void btnLoadRig_Click(object sender, EventArgs e)
        {
            loadRig();
        }

        private void btnNewRig_Click(object sender, EventArgs e)
        {
            newRig();
        }

        private void btnSaveRig_Click(object sender, EventArgs e)
        {
            saveRig(false);
        }

        private void btnSaveRigAs_Click(object sender, EventArgs e)
        {
            saveRig(true);
        }

        //- patch buttons -------------------------------------------------------

        private void btnNewPatch_Click(object sender, EventArgs e)
        {
            newPatch();
        }

        private void btnSavePatch_Click(object sender, EventArgs e)
        {
            savePatch();
        }

        private void btnSavePatchAs_Click(object sender, EventArgs e)
        {
            saveNewPatch();
        }

        //- plugin buttons ----------------------------------------------------

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadPlugin();
        }

        //- window buttons ----------------------------------------------------

        private void btnKeys_Click(object sender, EventArgs e)
        {
            auditwin.showKeyboardWindow();
        }

        private void btnMixer_Click(object sender, EventArgs e)
        {
            auditwin.showMixerWindow();
        }

        //- performance buttons -----------------------------------------------

        private void btnHide_Click(object sender, EventArgs e)
        {
            auditwin.hideRack();
        }

        private void btnPanic_Click(object sender, EventArgs e)
        {
            auditwin.panicButton();
        }

        //- host buttons ------------------------------------------------------

        private void btnStart_Click(object sender, EventArgs e)
        {
            startHost();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopHost();
        }

        public void updateHostSettings()
        {
            HostSettingsWnd hostsettings = new HostSettingsWnd();
            hostsettings.Icon = auditwin.Icon;
            hostsettings.setSampleRate(vashti.host.sampleRate);
            hostsettings.setBlockSize(vashti.host.blockSize);

            hostsettings.ShowDialog(this);

            if (hostsettings.DialogResult == DialogResult.OK)
            {
                vashti.host.setSampleRate(hostsettings.sampleRate);
                vashti.host.setBlockSize(hostsettings.blockSize);
            }
        }

        //- painting ------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //beveled edge
            g.DrawLine(Pens.SteelBlue, 1, this.Height - 1, this.Width - 1, this.Height - 1);        //bottom
            g.DrawLine(Pens.SteelBlue, this.Width - 1, 1, this.Width - 1, this.Height - 1);         //right

            //running LED
            Color LEDColor = isRunning ? Color.FromArgb(0xff, 0, 0) : Color.FromArgb(0x40, 0, 0);
            Brush LEDBrush = new SolidBrush(LEDColor);
            g.DrawEllipse(Pens.Black, 380, 28, 20, 20);
            g.FillEllipse(Brushes.White, 381, 29, 19, 19);
            g.FillEllipse(LEDBrush, 382, 30, 16, 16);
            LEDBrush.Dispose();
        }
    }
}
