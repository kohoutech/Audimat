namespace Audimat
{
    partial class AudimatWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudimatWindow));
            this.AudimatMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startRackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.keyboardBarRackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panicRackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsRackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AudimatStatus = new System.Windows.Forms.StatusStrip();
            this.lblAudimatStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.loadPluginDialog = new System.Windows.Forms.OpenFileDialog();
            this.loadRigFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRigFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRigFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRigAsFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newPatchFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePatchPatchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePatchAsPatchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideShowRackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AudimatMenu.SuspendLayout();
            this.AudimatStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // AudimatMenu
            // 
            this.AudimatMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.patchToolStripMenuItem,
            this.pluginMenuItem,
            this.hostMenuItem,
            this.helpMenuItem});
            this.AudimatMenu.Location = new System.Drawing.Point(0, 0);
            this.AudimatMenu.Name = "AudimatMenu";
            this.AudimatMenu.Size = new System.Drawing.Size(384, 24);
            this.AudimatMenu.TabIndex = 0;
            this.AudimatMenu.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRigFileMenuItem,
            this.newRigFileMenuItem,
            this.saveRigFileMenuItem,
            this.saveRigAsFileMenuItem,
            this.toolStripSeparator1,
            this.exitFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // exitFileMenuItem
            // 
            this.exitFileMenuItem.Name = "exitFileMenuItem";
            this.exitFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.exitFileMenuItem_Click);
            // 
            // hostMenuItem
            // 
            this.hostMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startRackMenuItem,
            this.stopRackMenuItem,
            this.toolStripSeparator3,
            this.keyboardBarRackMenuItem,
            this.panicRackMenuItem,
            this.toolStripSeparator4,
            this.hideShowRackMenuItem,
            this.settingsRackMenuItem});
            this.hostMenuItem.Name = "hostMenuItem";
            this.hostMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hostMenuItem.Text = "&Rack";
            // 
            // startRackMenuItem
            // 
            this.startRackMenuItem.Name = "startRackMenuItem";
            this.startRackMenuItem.Size = new System.Drawing.Size(182, 22);
            this.startRackMenuItem.Text = "&Start Engine";
            this.startRackMenuItem.Click += new System.EventHandler(this.StartHost_Click);
            // 
            // stopRackMenuItem
            // 
            this.stopRackMenuItem.Name = "stopRackMenuItem";
            this.stopRackMenuItem.Size = new System.Drawing.Size(182, 22);
            this.stopRackMenuItem.Text = "St&op Engine";
            this.stopRackMenuItem.Click += new System.EventHandler(this.StopHost_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // keyboardBarRackMenuItem
            // 
            this.keyboardBarRackMenuItem.Name = "keyboardBarRackMenuItem";
            this.keyboardBarRackMenuItem.Size = new System.Drawing.Size(182, 22);
            this.keyboardBarRackMenuItem.Text = "&Keyboard Bar";
            this.keyboardBarRackMenuItem.Click += new System.EventHandler(this.keysButton_Click);
            // 
            // panicRackMenuItem
            // 
            this.panicRackMenuItem.Name = "panicRackMenuItem";
            this.panicRackMenuItem.Size = new System.Drawing.Size(182, 22);
            this.panicRackMenuItem.Text = "&Panic (All Notes Off)";
            this.panicRackMenuItem.Click += new System.EventHandler(this.panicButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(179, 6);
            // 
            // settingsRackMenuItem
            // 
            this.settingsRackMenuItem.Name = "settingsRackMenuItem";
            this.settingsRackMenuItem.Size = new System.Drawing.Size(182, 22);
            this.settingsRackMenuItem.Text = "&Rack Settings";
            this.settingsRackMenuItem.Click += new System.EventHandler(this.settingsHostMenuItem_Click);
            // 
            // pluginMenuItem
            // 
            this.pluginMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPluginMenuItem});
            this.pluginMenuItem.Name = "pluginMenuItem";
            this.pluginMenuItem.ShowShortcutKeys = false;
            this.pluginMenuItem.Size = new System.Drawing.Size(53, 20);
            this.pluginMenuItem.Text = "&Plugin";
            // 
            // loadPluginMenuItem
            // 
            this.loadPluginMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadPluginMenuItem.Name = "loadPluginMenuItem";
            this.loadPluginMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadPluginMenuItem.Text = "&Load Plugin";
            this.loadPluginMenuItem.Click += new System.EventHandler(this.loadPlugin_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutHelpMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // aboutHelpMenuItem
            // 
            this.aboutHelpMenuItem.Name = "aboutHelpMenuItem";
            this.aboutHelpMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutHelpMenuItem.Text = "&About...";
            this.aboutHelpMenuItem.Click += new System.EventHandler(this.aboutHelpMenuItem_Click);
            // 
            // AudimatStatus
            // 
            this.AudimatStatus.BackColor = System.Drawing.SystemColors.Control;
            this.AudimatStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAudimatStatus});
            this.AudimatStatus.Location = new System.Drawing.Point(0, 339);
            this.AudimatStatus.Name = "AudimatStatus";
            this.AudimatStatus.Size = new System.Drawing.Size(384, 22);
            this.AudimatStatus.TabIndex = 2;
            this.AudimatStatus.Text = "statusStrip1";
            // 
            // lblAudimatStatus
            // 
            this.lblAudimatStatus.Name = "lblAudimatStatus";
            this.lblAudimatStatus.Size = new System.Drawing.Size(100, 17);
            this.lblAudimatStatus.Text = "Engine is stopped";
            // 
            // loadRigFileMenuItem
            // 
            this.loadRigFileMenuItem.Name = "loadRigFileMenuItem";
            this.loadRigFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadRigFileMenuItem.Text = "&Load Rig";
            this.loadRigFileMenuItem.Click += new System.EventHandler(this.loadRigFileMenuItem_Click);
            // 
            // newRigFileMenuItem
            // 
            this.newRigFileMenuItem.Name = "newRigFileMenuItem";
            this.newRigFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newRigFileMenuItem.Text = "&New Rig";
            this.newRigFileMenuItem.Click += new System.EventHandler(this.newRigFileMenuItem_Click);
            // 
            // saveRigFileMenuItem
            // 
            this.saveRigFileMenuItem.Name = "saveRigFileMenuItem";
            this.saveRigFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveRigFileMenuItem.Text = "&Save Rig";
            this.saveRigFileMenuItem.Click += new System.EventHandler(this.saveRigFileMenuItem_Click);
            // 
            // saveRigAsFileMenuItem
            // 
            this.saveRigAsFileMenuItem.Name = "saveRigAsFileMenuItem";
            this.saveRigAsFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveRigAsFileMenuItem.Text = "Save Rig &As";
            this.saveRigAsFileMenuItem.Click += new System.EventHandler(this.saveRigAsFileMenuItem_Click);
            // 
            // patchToolStripMenuItem
            // 
            this.patchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPatchFileMenuItem,
            this.savePatchPatchMenuItem,
            this.savePatchAsPatchMenuItem});
            this.patchToolStripMenuItem.Name = "patchToolStripMenuItem";
            this.patchToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.patchToolStripMenuItem.Text = "P&atch";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // newPatchFileMenuItem
            // 
            this.newPatchFileMenuItem.Name = "newPatchFileMenuItem";
            this.newPatchFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newPatchFileMenuItem.Text = "&New Patch";
            this.newPatchFileMenuItem.Click += new System.EventHandler(this.newPatchFileMenuItem_Click);
            // 
            // savePatchPatchMenuItem
            // 
            this.savePatchPatchMenuItem.Name = "savePatchPatchMenuItem";
            this.savePatchPatchMenuItem.Size = new System.Drawing.Size(152, 22);
            this.savePatchPatchMenuItem.Text = "&Save Patch";
            this.savePatchPatchMenuItem.Click += new System.EventHandler(this.savePatchPatchMenuItem_Click);
            // 
            // savePatchAsPatchMenuItem
            // 
            this.savePatchAsPatchMenuItem.Name = "savePatchAsPatchMenuItem";
            this.savePatchAsPatchMenuItem.Size = new System.Drawing.Size(152, 22);
            this.savePatchAsPatchMenuItem.Text = "Save Patch &As";
            this.savePatchAsPatchMenuItem.Click += new System.EventHandler(this.savePatchAsPatchMenuItem_Click);
            // 
            // hideShowRackMenuItem
            // 
            this.hideShowRackMenuItem.Name = "hideShowRackMenuItem";
            this.hideShowRackMenuItem.Size = new System.Drawing.Size(182, 22);
            this.hideShowRackMenuItem.Text = "&Hide/Show Rack";
            this.hideShowRackMenuItem.Click += new System.EventHandler(this.hideShowRackMenuItem_Click);
            // 
            // AudimatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.AudimatMenu);
            this.Controls.Add(this.AudimatStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.AudimatMenu;
            this.Name = "AudimatWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Audimat";
            this.AudimatMenu.ResumeLayout(false);
            this.AudimatMenu.PerformLayout();
            this.AudimatStatus.ResumeLayout(false);
            this.AudimatStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AudimatMenu;
        private System.Windows.Forms.StatusStrip AudimatStatus;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startRackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopRackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.OpenFileDialog loadPluginDialog;
        private System.Windows.Forms.ToolStripStatusLabel lblAudimatStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem keyboardBarRackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panicRackMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem settingsRackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRigFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRigFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRigFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRigAsFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem patchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPatchFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePatchPatchMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePatchAsPatchMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideShowRackMenuItem;
    }
}

