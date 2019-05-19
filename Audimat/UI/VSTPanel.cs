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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using Audimat;
using Transonic.VST;
using Transonic.MIDI;
using Transonic.MIDI.System;

namespace Audimat.UI
{
    public class VSTPanel : UserControl
    {
        public VSTRack rack;                //container
        public AudimatWindow audiwin;       //container's container - for using main window icon for panel child windows

        public VSTPlugin plugin;
        public int plugNum;
        public String plugPath;             //full path
        public String fileName;             //plugin filename w/o ext
        public String plugName;

        //widgets
        public const int PANELHEIGHT = 75;
        public const int PANELWIDTH = 400;
        public Label lblPlugName;
        public ComboBox cbxProgList;
        private Button btnNextProg;
        private Button btnPrevProg;
        private Button btnPlugSettings;
        private Button btnPlugInfo;
        private Button btnPlugParam;
        private Button btnPlugEditor;
        private Button btnPlugClose;
        private ToolTip paneltoolTip;
        private System.ComponentModel.IContainer components;

        //child windows
        PluginSettingsWnd pluginSettingsWnd;
        PluginInfoWnd pluginInfoWnd;
        PluginParamWnd paramEditorWnd;
        Form editorWindow;
        Size editorWindowSize;

        public int midiInDeviceNum;
        public PanelMidiIn midiInUnit;
        public int midiOutDeviceNum;


        //cons
        public VSTPanel(VSTRack _rack, int _plugNum)
        {
            InitializeComponent();

            rack = _rack;
            audiwin = rack.auditwin;

            plugNum = _plugNum;
            this.lblPlugName.Text = plugName;
            plugPath = null;
            fileName = null;
            plugin = null;

            this.Size = new Size(PANELWIDTH, PANELHEIGHT);

            pluginInfoWnd = null;
            paramEditorWnd = null;
            editorWindow = null;

            midiInDeviceNum = -1;
            midiInUnit = null;
            midiOutDeviceNum = -1;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblPlugName = new System.Windows.Forms.Label();
            this.cbxProgList = new System.Windows.Forms.ComboBox();
            this.btnPlugInfo = new System.Windows.Forms.Button();
            this.btnPlugParam = new System.Windows.Forms.Button();
            this.btnPlugEditor = new System.Windows.Forms.Button();
            this.btnPlugClose = new System.Windows.Forms.Button();
            this.paneltoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnNextProg = new System.Windows.Forms.Button();
            this.btnPrevProg = new System.Windows.Forms.Button();
            this.btnPlugSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPlugName
            // 
            this.lblPlugName.AutoSize = true;
            this.lblPlugName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlugName.Location = new System.Drawing.Point(25, 12);
            this.lblPlugName.Name = "lblPlugName";
            this.lblPlugName.Size = new System.Drawing.Size(78, 19);
            this.lblPlugName.TabIndex = 0;
            this.lblPlugName.Text = "not loaded";
            this.lblPlugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxProgList
            // 
            this.cbxProgList.BackColor = System.Drawing.Color.GreenYellow;
            this.cbxProgList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProgList.ForeColor = System.Drawing.Color.Black;
            this.cbxProgList.FormattingEnabled = true;
            this.cbxProgList.Location = new System.Drawing.Point(43, 37);
            this.cbxProgList.Name = "cbxProgList";
            this.cbxProgList.Size = new System.Drawing.Size(175, 23);
            this.cbxProgList.TabIndex = 2;
            this.cbxProgList.SelectedIndexChanged += new System.EventHandler(this.cbxProgList_SelectedIndexChanged);
            // 
            // btnPlugInfo
            // 
            this.btnPlugInfo.Location = new System.Drawing.Point(268, 36);
            this.btnPlugInfo.Name = "btnPlugInfo";
            this.btnPlugInfo.Size = new System.Drawing.Size(24, 24);
            this.btnPlugInfo.TabIndex = 5;
            this.btnPlugInfo.Text = "I";
            this.paneltoolTip.SetToolTip(this.btnPlugInfo, "show plugin info");
            this.btnPlugInfo.UseVisualStyleBackColor = true;
            this.btnPlugInfo.Click += new System.EventHandler(this.btnPlugInfo_Click);
            // 
            // btnPlugParam
            // 
            this.btnPlugParam.Location = new System.Drawing.Point(292, 36);
            this.btnPlugParam.Name = "btnPlugParam";
            this.btnPlugParam.Size = new System.Drawing.Size(24, 24);
            this.btnPlugParam.TabIndex = 6;
            this.btnPlugParam.Text = "P";
            this.paneltoolTip.SetToolTip(this.btnPlugParam, "edit plugin parameters");
            this.btnPlugParam.UseVisualStyleBackColor = true;
            this.btnPlugParam.Click += new System.EventHandler(this.btnPlugParam_Click);
            // 
            // btnPlugEditor
            // 
            this.btnPlugEditor.Location = new System.Drawing.Point(316, 36);
            this.btnPlugEditor.Name = "btnPlugEditor";
            this.btnPlugEditor.Size = new System.Drawing.Size(24, 24);
            this.btnPlugEditor.TabIndex = 7;
            this.btnPlugEditor.Text = "E";
            this.paneltoolTip.SetToolTip(this.btnPlugEditor, "show plugin editor");
            this.btnPlugEditor.UseVisualStyleBackColor = true;
            this.btnPlugEditor.Click += new System.EventHandler(this.btnPlugEditor_Click);
            // 
            // btnPlugClose
            // 
            this.btnPlugClose.BackColor = System.Drawing.Color.Red;
            this.btnPlugClose.Location = new System.Drawing.Point(350, 12);
            this.btnPlugClose.Name = "btnPlugClose";
            this.btnPlugClose.Size = new System.Drawing.Size(24, 24);
            this.btnPlugClose.TabIndex = 8;
            this.btnPlugClose.Text = "X";
            this.paneltoolTip.SetToolTip(this.btnPlugClose, "close plugin");
            this.btnPlugClose.UseVisualStyleBackColor = false;
            this.btnPlugClose.Click += new System.EventHandler(this.btnPlugClose_Click);
            // 
            // btnNextProg
            // 
            this.btnNextProg.Location = new System.Drawing.Point(218, 36);
            this.btnNextProg.Name = "btnNextProg";
            this.btnNextProg.Size = new System.Drawing.Size(18, 24);
            this.btnNextProg.TabIndex = 3;
            this.btnNextProg.Text = "+";
            this.paneltoolTip.SetToolTip(this.btnNextProg, "next program");
            this.btnNextProg.UseVisualStyleBackColor = true;
            this.btnNextProg.Click += new System.EventHandler(this.btnNextProg_Click);
            // 
            // btnPrevProg
            // 
            this.btnPrevProg.Location = new System.Drawing.Point(25, 36);
            this.btnPrevProg.Name = "btnPrevProg";
            this.btnPrevProg.Size = new System.Drawing.Size(18, 24);
            this.btnPrevProg.TabIndex = 1;
            this.btnPrevProg.Text = "-";
            this.paneltoolTip.SetToolTip(this.btnPrevProg, "previous program");
            this.btnPrevProg.UseVisualStyleBackColor = true;
            this.btnPrevProg.Click += new System.EventHandler(this.btnPrevProg_Click);
            // 
            // btnPlugSettings
            // 
            this.btnPlugSettings.Location = new System.Drawing.Point(244, 36);
            this.btnPlugSettings.Name = "btnPlugSettings";
            this.btnPlugSettings.Size = new System.Drawing.Size(24, 24);
            this.btnPlugSettings.TabIndex = 4;
            this.btnPlugSettings.Text = "S";
            this.paneltoolTip.SetToolTip(this.btnPlugSettings, "change plugin settings");
            this.btnPlugSettings.UseVisualStyleBackColor = true;
            this.btnPlugSettings.Click += new System.EventHandler(this.btnPlugSettings_Click);
            // 
            // VSTPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnPlugSettings);
            this.Controls.Add(this.btnPlugEditor);
            this.Controls.Add(this.btnPlugParam);
            this.Controls.Add(this.btnPlugInfo);
            this.Controls.Add(this.btnPlugClose);
            this.Controls.Add(this.btnNextProg);
            this.Controls.Add(this.btnPrevProg);
            this.Controls.Add(this.cbxProgList);
            this.Controls.Add(this.lblPlugName);
            this.Name = "VSTPanel";
            this.Size = new System.Drawing.Size(400, 75);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //- panel management ----------------------------------------------------------

        public bool loadPlugin(String _plugPath)
        {
            plugPath = _plugPath;
            fileName = Path.GetFileNameWithoutExtension(plugPath);
            plugin = rack.host.loadPlugin(plugPath);
            if (plugin != null)
            {
                plugName = (plugin.name.Length > 0) ? plugin.name : fileName;
                lblPlugName.Text = plugName;
                editorWindowSize = new Size(plugin.editorWidth, plugin.editorHeight);
                cbxProgList.DisplayMember = "name";
                cbxProgList.DataSource = plugin.programs;
                return true;
            }
            return false;
        }

        public void unloadPlugin()
        {
            //close child windows
            if (pluginSettingsWnd != null)
            {
                pluginSettingsWnd.Close();
                pluginSettingsWnd = null;
            }
            if (pluginInfoWnd != null)
            {
                pluginInfoWnd.Close();
                pluginInfoWnd = null;
            }
            if (paramEditorWnd != null)
            {
                paramEditorWnd.Close();
                paramEditorWnd = null;
            }
            if (editorWindow != null)
            {
                editorWindow.Close();
                editorWindow = null;
            }

            //disconnect midi i/o
            if (midiInDeviceNum != -1)
            {
                rack.disconnectMidiInput(midiInDeviceNum, midiInUnit);
            }

            rack.host.unloadPlugin(plugin);     //disconnect and unload back end
        }

        //- painting ------------------------------------------------------------------

        void drawRackScrew(Graphics g, int xpos, int ypos)
        {
            g.DrawEllipse(Pens.Black, xpos, ypos, VSTRack.SCREWHOLE, VSTRack.SCREWHOLE);
            g.FillEllipse(Brushes.Gray, xpos, ypos, VSTRack.SCREWHOLE, VSTRack.SCREWHOLE);
            Pen slotPen = new Pen(Color.Black, 2);
            g.DrawLine(slotPen, xpos + 5, ypos, xpos + 5, ypos + 10);
            g.DrawLine(slotPen, xpos, ypos + 5, xpos + 10, ypos + 5);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //beveled edge
            g.DrawLine(Pens.SteelBlue, 1, PANELHEIGHT - 3, PANELWIDTH - 1, PANELHEIGHT - 3);        //bottom
            g.DrawLine(Pens.SteelBlue, PANELWIDTH - 3, 1, PANELWIDTH - 3, PANELHEIGHT - 1);         //right

            //rack screws
            int rightOfs = PANELWIDTH - VSTRack.SCREWHOLE - VSTRack.SCREWOFS;
            int bottomofs = PANELHEIGHT - (VSTRack.SCREWHOLE * 2);
            drawRackScrew(g, VSTRack.SCREWOFS, VSTRack.SCREWHOLE);
            drawRackScrew(g, VSTRack.SCREWOFS, bottomofs);
            drawRackScrew(g, rightOfs, VSTRack.SCREWHOLE);
            drawRackScrew(g, rightOfs, bottomofs);
        }

        //- event handlers ------------------------------------------------------------

        private void cbxProgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            plugin.setProgram(cbxProgList.SelectedIndex);
        }

        private void btnPrevProg_Click(object sender, EventArgs e)
        {
            int curprog = cbxProgList.SelectedIndex;
            curprog--;
            if (curprog >= 0)
            {
                cbxProgList.SelectedIndex = curprog;
            }
        }

        private void btnNextProg_Click(object sender, EventArgs e)
        {
            int curprog = cbxProgList.SelectedIndex;
            curprog++;
            if (curprog < plugin.programs.Length)
            {
                cbxProgList.SelectedIndex = curprog;
            }
        }

        //- plugin settings window ------------------------------------------------

        private void btnPlugSettings_Click(object sender, EventArgs e)
        {
            btnPlugSettings.Enabled = false;
            pluginSettingsWnd = new PluginSettingsWnd(this);
            pluginSettingsWnd.Text = plugName + " settings";
            pluginSettingsWnd.Icon = audiwin.Icon;
            pluginSettingsWnd.FormClosing += new FormClosingEventHandler(settingsWindow_FormClosing);
            pluginSettingsWnd.Show();
        }

        private void settingsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnPlugSettings.Enabled = true;
        }

        public void setMidiIn(int deviceNum)
        {
            if (midiInDeviceNum != deviceNum)
            {
                if (midiInUnit != null)
                {
                    rack.disconnectMidiInput(midiInDeviceNum, midiInUnit);
                }
                midiInDeviceNum = deviceNum;
                if (deviceNum != -1)
                {
                    midiInUnit = new PanelMidiIn(this);
                    rack.connectMidiInput(deviceNum, midiInUnit);
                }
                else
                {
                    midiInUnit = null;
                }
            }
        }

        public void setMidiOut(int idx)
        {
            if (midiOutDeviceNum != idx)
            {
                midiOutDeviceNum = idx;
            }
        }

        //- plugin info window ------------------------------------------------

        private void btnPlugInfo_Click(object sender, EventArgs e)
        {
            btnPlugInfo.Enabled = false;
            pluginInfoWnd = new PluginInfoWnd(this);
            pluginInfoWnd.Text = plugName + " info";
            pluginInfoWnd.Icon = audiwin.Icon;
            pluginInfoWnd.FormClosing += new FormClosingEventHandler(infoWindow_FormClosing);
            pluginInfoWnd.Show();
        }

        private void infoWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnPlugInfo.Enabled = true;
        }

        //- plugin parameter window -------------------------------------------

        private void btnPlugParam_Click(object sender, EventArgs e)
        {
            btnPlugParam.Enabled = false;
            paramEditorWnd = new PluginParamWnd(this);
            paramEditorWnd.Text = plugName + " parameters";
            paramEditorWnd.Icon = audiwin.Icon;
            paramEditorWnd.FormClosing += new FormClosingEventHandler(paramWindow_FormClosing);
            paramEditorWnd.Show();
        }

        private void paramWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnPlugParam.Enabled = true;
        }

        //- plugin editor window ------------------------------------------------------

        private void btnPlugEditor_Click(object sender, EventArgs e)
        {
            btnPlugEditor.Enabled = false;
            editorWindow = new Form();
            editorWindow.ClientSize = editorWindowSize;
            editorWindow.Text = plugName + " editor";
            editorWindow.Icon = audiwin.Icon;
            editorWindow.ShowInTaskbar = false;
            editorWindow.MaximizeBox = false;
            editorWindow.FormBorderStyle = FormBorderStyle.FixedSingle;
            editorWindow.FormClosing += new FormClosingEventHandler(editorWindow_FormClosing);
            editorWindow.Show();
            plugin.openEditorWindow(editorWindow.Handle);
        }

        private void editorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnPlugEditor.Enabled = true;
            plugin.closeEditorWindow();
            editorWindow = null;
        }

        //-----------------------------------------------------------------------------

        private void btnPlugClose_Click(object sender, EventArgs e)
        {
            rack.removePanel(this);
        }
    }

    //- midi listener ---------------------------------------------------------

    //midi input listener unit
    public class PanelMidiIn : SystemUnit
    {
        public VSTPanel panel;

        public PanelMidiIn(VSTPanel _panel)
            : base(_panel.plugName)
        {
            panel = _panel;
        }

        public override void receiveMessage(byte[] msg)
        {
            //Console.WriteLine(" sending midi message {0} {1} {2}", msg[0], msg[1], msg[2]);
            panel.plugin.sendShortMidiMessage(msg[0], msg[1], msg[2]);
        }
    }
}
