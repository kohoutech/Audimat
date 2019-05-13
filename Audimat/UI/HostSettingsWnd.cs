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

namespace Audimat.UI
{
    class HostSettingsWnd : Form
    {
        public int sampleRate;
        public int blockSize;

        public List<int> blockSizes;
        public int[] sampleRates = new int[] { 44100, 48000, 88200, 96000, 176400, 192000};

        private Label lblSampleRate;
        private ComboBox cbxSampleRate;
        private Label lblBlockSize;
        private Button btnOK;
        private ComboBox cbxBlockSize;
        private Button btnCancel;

        public HostSettingsWnd()
        {
            InitializeComponent();

            foreach (int rate in sampleRates)
            {
                cbxSampleRate.Items.Add(rate);
            }

            setSampleRate(44100);
            setBlockSize(2205);
        }

        private void InitializeComponent()
        {
            this.cbxSampleRate = new System.Windows.Forms.ComboBox();
            this.lblSampleRate = new System.Windows.Forms.Label();
            this.lblBlockSize = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxBlockSize = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxSampleRate
            // 
            this.cbxSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSampleRate.FormattingEnabled = true;
            this.cbxSampleRate.Location = new System.Drawing.Point(12, 35);
            this.cbxSampleRate.Name = "cbxSampleRate";
            this.cbxSampleRate.Size = new System.Drawing.Size(360, 21);
            this.cbxSampleRate.TabIndex = 7;
            this.cbxSampleRate.SelectedIndexChanged += new System.EventHandler(this.cbxSampleRate_SelectedIndexChanged);
            // 
            // lblSampleRate
            // 
            this.lblSampleRate.AutoSize = true;
            this.lblSampleRate.Location = new System.Drawing.Point(12, 10);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new System.Drawing.Size(115, 13);
            this.lblSampleRate.TabIndex = 6;
            this.lblSampleRate.Text = "select host sample rate";
            // 
            // lblBlockSize
            // 
            this.lblBlockSize.AutoSize = true;
            this.lblBlockSize.Location = new System.Drawing.Point(12, 70);
            this.lblBlockSize.Name = "lblBlockSize";
            this.lblBlockSize.Size = new System.Drawing.Size(123, 13);
            this.lblBlockSize.TabIndex = 9;
            this.lblBlockSize.Text = "set block size in samples";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(295, 140);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(205, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxBlockSize
            // 
            this.cbxBlockSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBlockSize.FormattingEnabled = true;
            this.cbxBlockSize.Location = new System.Drawing.Point(12, 95);
            this.cbxBlockSize.Name = "cbxBlockSize";
            this.cbxBlockSize.Size = new System.Drawing.Size(360, 21);
            this.cbxBlockSize.TabIndex = 18;
            this.cbxBlockSize.SelectedIndexChanged += new System.EventHandler(this.cbxBlockSize_SelectedIndexChanged);
            // 
            // HostSettingsWnd
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(384, 171);
            this.Controls.Add(this.cbxBlockSize);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblBlockSize);
            this.Controls.Add(this.cbxSampleRate);
            this.Controls.Add(this.lblSampleRate);
            this.Name = "HostSettingsWnd";
            this.ShowInTaskbar = false;
            this.Text = "Host Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void setSampleRate(int rate)
        {
            int rateIdx = 0;                                //default rate
            for (int i = 0; i < sampleRates.Length; i++)
            {
                if (sampleRates[i] == rate)
                {
                    rateIdx = i;
                    break;
                }
            }
            cbxSampleRate.SelectedIndex = rateIdx;
        }

        //should this be precomputed?
        private void calculateBlockSizes()
        {
            blockSizes = new List<int>();
            cbxBlockSize.Items.Clear();

            //calculate block sizes from 2 ms to 250 ms
            int maxsize = sampleRate / 4;
            int minsize = sampleRate / 500;
            for (int i = maxsize; i >= minsize; i--)
            {
                int blocksPerSec = (sampleRate / i);
                if ((blocksPerSec * i) == sampleRate)
                {
                    blockSizes.Add(i);
                    cbxBlockSize.Items.Add(i + " samples (" + blocksPerSec + " blocks/sec)");
                }
            }
            //cbxBlockSize.SelectedIndex = 0;
        }

        public void setBlockSize(int size)
        {
            int sizeIdx = 0;                                //default rate
            for (int i = 0; i < blockSizes.Count; i++)
            {
                if (blockSizes[i] == size)
                {
                    sizeIdx = i;
                    break;
                }
            }
            cbxBlockSize.SelectedIndex = sizeIdx;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void cbxSampleRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            sampleRate = sampleRates[cbxSampleRate.SelectedIndex];
            calculateBlockSizes();
        }

        private void cbxBlockSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            blockSize = blockSizes[cbxBlockSize.SelectedIndex];
        }
    }
}
