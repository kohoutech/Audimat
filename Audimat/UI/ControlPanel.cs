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

namespace Audimat.UI
{
    public class ControlPanel : UserControl
    {
        public AudimatWindow auditwin;

        public Button btnLoad;
        public Button btnPanic;
        public Button btnKeys;
        public Button btnStop;
        public Button btnStart;

        public ControlPanel(AudimatWindow _auditwin)
        {
            auditwin = _auditwin;

            InitializeComponent();            
        }

        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPanic = new System.Windows.Forms.Button();
            this.btnKeys = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(13, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(39, 24);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPanic
            // 
            this.btnPanic.Location = new System.Drawing.Point(173, 8);
            this.btnPanic.Name = "btnPanic";
            this.btnPanic.Size = new System.Drawing.Size(42, 24);
            this.btnPanic.TabIndex = 9;
            this.btnPanic.Text = "Panic";
            this.btnPanic.UseVisualStyleBackColor = true;
            this.btnPanic.Click += new System.EventHandler(this.btnPanic_Click);
            // 
            // btnKeys
            // 
            this.btnKeys.Location = new System.Drawing.Point(132, 8);
            this.btnKeys.Name = "btnKeys";
            this.btnKeys.Size = new System.Drawing.Size(38, 24);
            this.btnKeys.TabIndex = 10;
            this.btnKeys.Text = "Keys";
            this.btnKeys.UseVisualStyleBackColor = true;
            this.btnKeys.Click += new System.EventHandler(this.btnKeys_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(93, 8);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(37, 24);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(54, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(37, 24);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnKeys);
            this.Controls.Add(this.btnPanic);
            this.Controls.Add(this.btnLoad);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(400, 40);
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
    }
}
