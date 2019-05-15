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
        public Button btnKeys;
        public Button btnStop;
        private ToolTip controlPanelToolTip;
        private System.ComponentModel.IContainer components;
        private Button btnPlugEditor;
        private Button btnNextProg;
        private Button btnPrevProg;
        public ComboBox cbxProgList;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
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
            this.btnKeys = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.controlPanelToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPlugEditor = new System.Windows.Forms.Button();
            this.btnNextProg = new System.Windows.Forms.Button();
            this.btnPrevProg = new System.Windows.Forms.Button();
            this.cbxProgList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
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
            // btnKeys
            // 
            this.btnKeys.Location = new System.Drawing.Point(294, 37);
            this.btnKeys.Name = "btnKeys";
            this.btnKeys.Size = new System.Drawing.Size(42, 24);
            this.btnKeys.TabIndex = 10;
            this.btnKeys.Text = "Keys";
            this.controlPanelToolTip.SetToolTip(this.btnKeys, "show keyboard window");
            this.btnKeys.UseVisualStyleBackColor = true;
            this.btnKeys.Click += new System.EventHandler(this.btnKeys_Click);
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
            // btnPlugEditor
            // 
            this.btnPlugEditor.Location = new System.Drawing.Point(71, 37);
            this.btnPlugEditor.Name = "btnPlugEditor";
            this.btnPlugEditor.Size = new System.Drawing.Size(24, 24);
            this.btnPlugEditor.TabIndex = 13;
            this.btnPlugEditor.Text = "S";
            this.btnPlugEditor.UseVisualStyleBackColor = true;
            // 
            // btnNextProg
            // 
            this.btnNextProg.Location = new System.Drawing.Point(215, 13);
            this.btnNextProg.Name = "btnNextProg";
            this.btnNextProg.Size = new System.Drawing.Size(18, 24);
            this.btnNextProg.TabIndex = 16;
            this.btnNextProg.Text = "+";
            this.btnNextProg.UseVisualStyleBackColor = true;
            // 
            // btnPrevProg
            // 
            this.btnPrevProg.Location = new System.Drawing.Point(25, 13);
            this.btnPrevProg.Name = "btnPrevProg";
            this.btnPrevProg.Size = new System.Drawing.Size(18, 25);
            this.btnPrevProg.TabIndex = 14;
            this.btnPrevProg.Text = "-";
            this.btnPrevProg.UseVisualStyleBackColor = true;
            // 
            // cbxProgList
            // 
            this.cbxProgList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProgList.FormattingEnabled = true;
            this.cbxProgList.Location = new System.Drawing.Point(43, 14);
            this.cbxProgList.Name = "cbxProgList";
            this.cbxProgList.Size = new System.Drawing.Size(172, 23);
            this.cbxProgList.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 17;
            this.button1.Text = "L";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 24);
            this.button2.TabIndex = 18;
            this.button2.Text = "N";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(94, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 24);
            this.button3.TabIndex = 19;
            this.button3.Text = "A";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(163, 37);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 24);
            this.button4.TabIndex = 20;
            this.button4.Text = "L";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(186, 37);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(24, 24);
            this.button5.TabIndex = 21;
            this.button5.Text = "K";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(209, 37);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(24, 24);
            this.button6.TabIndex = 22;
            this.button6.Text = "M";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(140, 37);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(24, 24);
            this.button7.TabIndex = 23;
            this.button7.Text = "A";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(117, 37);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(24, 24);
            this.button8.TabIndex = 24;
            this.button8.Text = "S";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNextProg);
            this.Controls.Add(this.btnPrevProg);
            this.Controls.Add(this.cbxProgList);
            this.Controls.Add(this.btnPlugEditor);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnKeys);
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
        }
    }
}
