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

using Transonic.VST;
using Transonic.MIDI.System;

namespace Audimat.UI
{
    class PluginSettingsWnd : Form
    {
        VSTPanel panel;
        VSTPlugin plugin;

        WaveDevices waveDevices;
        MidiSystem midiDevices;

        private Label lblMidiOut;
        private Label lblAudioIn;
        private Label lblAudioOut;
        private ComboBox cbxAudioIn;
        private ComboBox cbxAudioOut;
        private ComboBox cbxMidiIn;
        private ComboBox cbxMidiOut;
        private Button btnOK;
        private Button btnCancel;
        private Label lblMidiIn;
    
        //cons
        public PluginSettingsWnd(VSTPanel _panel)
        {
            InitializeComponent();

            panel = _panel;
            plugin = panel.plugin;
            waveDevices = panel.host.waveDevices;
            midiDevices = panel.midiDevices;

            cbxAudioIn.DataSource = waveDevices.getInDevNameList();
            cbxAudioOut.DataSource = waveDevices.getOutDevNameList();
            
            cbxMidiIn.DataSource = midiDevices.getInDevNameList();
            cbxMidiIn.SelectedIndex = cbxMidiIn.FindString((panel.midiInDevice != null) ? panel.midiInDevice.devName : "no input");

            cbxMidiOut.DataSource = midiDevices.getOutDevNameList();
        }

        private void InitializeComponent()
        {
            this.lblMidiIn = new System.Windows.Forms.Label();
            this.lblMidiOut = new System.Windows.Forms.Label();
            this.lblAudioIn = new System.Windows.Forms.Label();
            this.lblAudioOut = new System.Windows.Forms.Label();
            this.cbxAudioIn = new System.Windows.Forms.ComboBox();
            this.cbxAudioOut = new System.Windows.Forms.ComboBox();
            this.cbxMidiIn = new System.Windows.Forms.ComboBox();
            this.cbxMidiOut = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMidiIn
            // 
            this.lblMidiIn.AutoSize = true;
            this.lblMidiIn.Location = new System.Drawing.Point(16, 125);
            this.lblMidiIn.Name = "lblMidiIn";
            this.lblMidiIn.Size = new System.Drawing.Size(108, 13);
            this.lblMidiIn.TabIndex = 1;
            this.lblMidiIn.Text = "select MIDI In device";
            // 
            // lblMidiOut
            // 
            this.lblMidiOut.AutoSize = true;
            this.lblMidiOut.Location = new System.Drawing.Point(16, 178);
            this.lblMidiOut.Name = "lblMidiOut";
            this.lblMidiOut.Size = new System.Drawing.Size(116, 13);
            this.lblMidiOut.TabIndex = 2;
            this.lblMidiOut.Text = "select MIDI Out device";
            // 
            // lblAudioIn
            // 
            this.lblAudioIn.AutoSize = true;
            this.lblAudioIn.Location = new System.Drawing.Point(16, 21);
            this.lblAudioIn.Name = "lblAudioIn";
            this.lblAudioIn.Size = new System.Drawing.Size(112, 13);
            this.lblAudioIn.TabIndex = 3;
            this.lblAudioIn.Text = "select Audio In device";
            // 
            // lblAudioOut
            // 
            this.lblAudioOut.AutoSize = true;
            this.lblAudioOut.Location = new System.Drawing.Point(16, 74);
            this.lblAudioOut.Name = "lblAudioOut";
            this.lblAudioOut.Size = new System.Drawing.Size(120, 13);
            this.lblAudioOut.TabIndex = 4;
            this.lblAudioOut.Text = "select Audio Out device";
            // 
            // cbxAudioIn
            // 
            this.cbxAudioIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAudioIn.FormattingEnabled = true;
            this.cbxAudioIn.Location = new System.Drawing.Point(16, 38);
            this.cbxAudioIn.Name = "cbxAudioIn";
            this.cbxAudioIn.Size = new System.Drawing.Size(297, 21);
            this.cbxAudioIn.TabIndex = 5;
            // 
            // cbxAudioOut
            // 
            this.cbxAudioOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAudioOut.FormattingEnabled = true;
            this.cbxAudioOut.Location = new System.Drawing.Point(16, 90);
            this.cbxAudioOut.Name = "cbxAudioOut";
            this.cbxAudioOut.Size = new System.Drawing.Size(297, 21);
            this.cbxAudioOut.TabIndex = 6;
            // 
            // cbxMidiIn
            // 
            this.cbxMidiIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMidiIn.FormattingEnabled = true;
            this.cbxMidiIn.Location = new System.Drawing.Point(16, 142);
            this.cbxMidiIn.Name = "cbxMidiIn";
            this.cbxMidiIn.Size = new System.Drawing.Size(297, 21);
            this.cbxMidiIn.TabIndex = 7;
            // 
            // cbxMidiOut
            // 
            this.cbxMidiOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMidiOut.FormattingEnabled = true;
            this.cbxMidiOut.Location = new System.Drawing.Point(16, 194);
            this.cbxMidiOut.Name = "cbxMidiOut";
            this.cbxMidiOut.Size = new System.Drawing.Size(297, 21);
            this.cbxMidiOut.TabIndex = 8;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(238, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(147, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PluginSettingsWnd
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(334, 271);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbxMidiOut);
            this.Controls.Add(this.cbxMidiIn);
            this.Controls.Add(this.cbxAudioOut);
            this.Controls.Add(this.cbxAudioIn);
            this.Controls.Add(this.lblAudioOut);
            this.Controls.Add(this.lblAudioIn);
            this.Controls.Add(this.lblMidiOut);
            this.Controls.Add(this.lblMidiIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PluginSettingsWnd";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            plugin.setAudioIn(cbxAudioIn.SelectedIndex - 1);
            plugin.setAudioOut(cbxAudioOut.SelectedIndex - 1);
            panel.setMidiIn(cbxMidiIn.Text);
            panel.setMidiOut(cbxMidiOut.SelectedIndex - 1);

            this.Close();
        }
    }
}
