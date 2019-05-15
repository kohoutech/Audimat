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

namespace Audimat.UI
{
    public class ControlPanel : UserControl
    {
        public AudimatWindow auditwin;
        public Button btnPanic;
        public Button btnHide;
        public Button btnStop;
        private ToolTip controlPanelToolTip;
        private System.ComponentModel.IContainer components;
        private Button btnSaveRig;
        private Button btnNextPatch;
        private Button btnPrevPatch;
        public ComboBox cbxPatchList;
        private Button btnLoadRig;
        private Button btnNewRig;
        private Button btnSaveRigAs;
        private Button btnLoadPlugin;
        private Button btnKeys2;
        private Button btnMixer;
        private Button btnSavePatchAs;
        private Button btnSavePatch;
        public Button btnStart;

        public ControlPanel(AudimatWindow _auditwin)
        {
            auditwin = _auditwin;

            InitializeComponent();            
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
            this.cbxPatchList = new System.Windows.Forms.ComboBox();
            this.btnLoadRig = new System.Windows.Forms.Button();
            this.btnNewRig = new System.Windows.Forms.Button();
            this.btnSaveRigAs = new System.Windows.Forms.Button();
            this.btnLoadPlugin = new System.Windows.Forms.Button();
            this.btnKeys2 = new System.Windows.Forms.Button();
            this.btnMixer = new System.Windows.Forms.Button();
            this.btnSavePatchAs = new System.Windows.Forms.Button();
            this.btnSavePatch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPanic
            // 
            this.btnPanic.Location = new System.Drawing.Point(333, 37);
            this.btnPanic.Name = "btnPanic";
            this.btnPanic.Size = new System.Drawing.Size(42, 24);
            this.btnPanic.TabIndex = 9;
            this.btnPanic.Text = "Panic";
            this.controlPanelToolTip.SetToolTip(this.btnPanic, "panic button!");
            this.btnPanic.UseVisualStyleBackColor = true;
            this.btnPanic.Click += new System.EventHandler(this.btnPanic_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(294, 37);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(42, 24);
            this.btnHide.TabIndex = 10;
            this.btnHide.Text = "Hide";
            this.controlPanelToolTip.SetToolTip(this.btnHide, "show keyboard window");
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(333, 13);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(42, 24);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.controlPanelToolTip.SetToolTip(this.btnStop, "stop engine");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(294, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(42, 24);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "Start";
            this.controlPanelToolTip.SetToolTip(this.btnStart, "start engine");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSaveRig
            // 
            this.btnSaveRig.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnSaveRig.Location = new System.Drawing.Point(71, 37);
            this.btnSaveRig.Name = "btnSaveRig";
            this.btnSaveRig.Size = new System.Drawing.Size(24, 24);
            this.btnSaveRig.TabIndex = 13;
            this.btnSaveRig.Text = "S";
            this.btnSaveRig.UseVisualStyleBackColor = false;
            this.btnSaveRig.Click += new System.EventHandler(this.btnSaveRig_Click);
            // 
            // btnNextPatch
            // 
            this.btnNextPatch.Location = new System.Drawing.Point(215, 13);
            this.btnNextPatch.Name = "btnNextPatch";
            this.btnNextPatch.Size = new System.Drawing.Size(18, 24);
            this.btnNextPatch.TabIndex = 16;
            this.btnNextPatch.Text = "+";
            this.btnNextPatch.UseVisualStyleBackColor = true;
            // 
            // btnPrevPatch
            // 
            this.btnPrevPatch.Location = new System.Drawing.Point(25, 13);
            this.btnPrevPatch.Name = "btnPrevPatch";
            this.btnPrevPatch.Size = new System.Drawing.Size(18, 25);
            this.btnPrevPatch.TabIndex = 14;
            this.btnPrevPatch.Text = "-";
            this.btnPrevPatch.UseVisualStyleBackColor = true;
            // 
            // cbxPatchList
            // 
            this.cbxPatchList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPatchList.FormattingEnabled = true;
            this.cbxPatchList.Location = new System.Drawing.Point(43, 14);
            this.cbxPatchList.Name = "cbxPatchList";
            this.cbxPatchList.Size = new System.Drawing.Size(172, 23);
            this.cbxPatchList.TabIndex = 15;
            // 
            // btnLoadRig
            // 
            this.btnLoadRig.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnLoadRig.Location = new System.Drawing.Point(48, 37);
            this.btnLoadRig.Name = "btnLoadRig";
            this.btnLoadRig.Size = new System.Drawing.Size(24, 24);
            this.btnLoadRig.TabIndex = 17;
            this.btnLoadRig.Text = "L";
            this.btnLoadRig.UseVisualStyleBackColor = false;
            this.btnLoadRig.Click += new System.EventHandler(this.btnLoadRig_Click);
            // 
            // btnNewRig
            // 
            this.btnNewRig.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnNewRig.Location = new System.Drawing.Point(25, 37);
            this.btnNewRig.Name = "btnNewRig";
            this.btnNewRig.Size = new System.Drawing.Size(24, 24);
            this.btnNewRig.TabIndex = 18;
            this.btnNewRig.Text = "N";
            this.btnNewRig.UseVisualStyleBackColor = false;
            this.btnNewRig.Click += new System.EventHandler(this.btnNewRig_Click);
            // 
            // btnSaveRigAs
            // 
            this.btnSaveRigAs.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnSaveRigAs.Location = new System.Drawing.Point(94, 37);
            this.btnSaveRigAs.Name = "btnSaveRigAs";
            this.btnSaveRigAs.Size = new System.Drawing.Size(24, 24);
            this.btnSaveRigAs.TabIndex = 19;
            this.btnSaveRigAs.Text = "A";
            this.btnSaveRigAs.UseVisualStyleBackColor = false;
            this.btnSaveRigAs.Click += new System.EventHandler(this.btnSaveRigAs_Click);
            // 
            // btnLoadPlugin
            // 
            this.btnLoadPlugin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLoadPlugin.Location = new System.Drawing.Point(163, 37);
            this.btnLoadPlugin.Name = "btnLoadPlugin";
            this.btnLoadPlugin.Size = new System.Drawing.Size(24, 24);
            this.btnLoadPlugin.TabIndex = 20;
            this.btnLoadPlugin.Text = "L";
            this.btnLoadPlugin.UseVisualStyleBackColor = false;
            this.btnLoadPlugin.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnKeys2
            // 
            this.btnKeys2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnKeys2.Location = new System.Drawing.Point(186, 37);
            this.btnKeys2.Name = "btnKeys2";
            this.btnKeys2.Size = new System.Drawing.Size(24, 24);
            this.btnKeys2.TabIndex = 21;
            this.btnKeys2.Text = "K";
            this.btnKeys2.UseVisualStyleBackColor = false;
            this.btnKeys2.Click += new System.EventHandler(this.btnKeys_Click);
            // 
            // btnMixer
            // 
            this.btnMixer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnMixer.Location = new System.Drawing.Point(209, 37);
            this.btnMixer.Name = "btnMixer";
            this.btnMixer.Size = new System.Drawing.Size(24, 24);
            this.btnMixer.TabIndex = 22;
            this.btnMixer.Text = "M";
            this.btnMixer.UseVisualStyleBackColor = false;
            this.btnMixer.Click += new System.EventHandler(this.btnMixer_Click);
            // 
            // btnSavePatchAs
            // 
            this.btnSavePatchAs.BackColor = System.Drawing.Color.Yellow;
            this.btnSavePatchAs.Location = new System.Drawing.Point(140, 37);
            this.btnSavePatchAs.Name = "btnSavePatchAs";
            this.btnSavePatchAs.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatchAs.TabIndex = 23;
            this.btnSavePatchAs.Text = "A";
            this.btnSavePatchAs.UseVisualStyleBackColor = false;
            this.btnSavePatchAs.Click += new System.EventHandler(this.btnSavePatchAs_Click);
            // 
            // btnSavePatch
            // 
            this.btnSavePatch.BackColor = System.Drawing.Color.Yellow;
            this.btnSavePatch.Location = new System.Drawing.Point(117, 37);
            this.btnSavePatch.Name = "btnSavePatch";
            this.btnSavePatch.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatch.TabIndex = 24;
            this.btnSavePatch.Text = "S";
            this.btnSavePatch.UseVisualStyleBackColor = false;
            this.btnSavePatch.Click += new System.EventHandler(this.btnSavePatch_Click);
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.btnSavePatch);
            this.Controls.Add(this.btnSavePatchAs);
            this.Controls.Add(this.btnMixer);
            this.Controls.Add(this.btnKeys2);
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

        //- event handlers ----------------------------------------------------

        private void btnLoad_Click(object sender, EventArgs e)
        {
            auditwin.loadPlugin();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            auditwin.startHost();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            auditwin.stopHost();
        }

        private void btnKeys_Click(object sender, EventArgs e)
        {
            auditwin.showKeyboardWindow();
        }

        private void btnPanic_Click(object sender, EventArgs e)
        {
            //not implemented yet
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
            Color LEDColor = auditwin.rack.isRunning ? Color.FromArgb(0xff, 0, 0) : Color.FromArgb(0x40, 0, 0);
            Brush LEDBrush = new SolidBrush(LEDColor);
            g.FillEllipse(LEDBrush, 34, 12, 16, 16);
            g.DrawEllipse(Pens.White, 34, 12, 16, 16);
            LEDBrush.Dispose();            
        }

        private void btnNewRig_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadRig_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveRig_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveRigAs_Click(object sender, EventArgs e)
        {

        }

        private void btnSavePatch_Click(object sender, EventArgs e)
        {

        }

        private void btnSavePatchAs_Click(object sender, EventArgs e)
        {

        }

        private void btnMixer_Click(object sender, EventArgs e)
        {

        }

        private void btnHide_Click(object sender, EventArgs e)
        {

        }
    }
}
