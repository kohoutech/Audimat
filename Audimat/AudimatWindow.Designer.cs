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
            this.startHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.keyboardBarHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panicHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AudimatStatus = new System.Windows.Forms.StatusStrip();
            this.lblAudimatStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.loadPluginDialog = new System.Windows.Forms.OpenFileDialog();
            this.AudimatMenu.SuspendLayout();
            this.AudimatStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // AudimatMenu
            // 
            this.AudimatMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.hostMenuItem,
            this.pluginMenuItem,
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
            this.exitFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // exitFileMenuItem
            // 
            this.exitFileMenuItem.Name = "exitFileMenuItem";
            this.exitFileMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.exitFileMenuItem_Click);
            // 
            // hostMenuItem
            // 
            this.hostMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startHostMenuItem,
            this.stopHostMenuItem,
            this.toolStripSeparator3,
            this.keyboardBarHostMenuItem,
            this.panicHostMenuItem,
            this.toolStripSeparator4,
            this.settingsHostMenuItem});
            this.hostMenuItem.Name = "hostMenuItem";
            this.hostMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hostMenuItem.Text = "H&ost";
            // 
            // startHostMenuItem
            // 
            this.startHostMenuItem.Name = "startHostMenuItem";
            this.startHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.startHostMenuItem.Text = "&Start Engine";
            this.startHostMenuItem.Click += new System.EventHandler(this.StartHost_Click);
            // 
            // stopHostMenuItem
            // 
            this.stopHostMenuItem.Name = "stopHostMenuItem";
            this.stopHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.stopHostMenuItem.Text = "Sto&p Engine";
            this.stopHostMenuItem.Click += new System.EventHandler(this.StopHost_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // keyboardBarHostMenuItem
            // 
            this.keyboardBarHostMenuItem.Name = "keyboardBarHostMenuItem";
            this.keyboardBarHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.keyboardBarHostMenuItem.Text = "&Keyboard Bar";
            this.keyboardBarHostMenuItem.Click += new System.EventHandler(this.keysButton_Click);
            // 
            // panicHostMenuItem
            // 
            this.panicHostMenuItem.Name = "panicHostMenuItem";
            this.panicHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.panicHostMenuItem.Text = "Panic (All Notes Off)";
            this.panicHostMenuItem.Click += new System.EventHandler(this.panicButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(179, 6);
            // 
            // settingsHostMenuItem
            // 
            this.settingsHostMenuItem.Name = "settingsHostMenuItem";
            this.settingsHostMenuItem.Size = new System.Drawing.Size(182, 22);
            this.settingsHostMenuItem.Text = "&Host Settings";
            this.settingsHostMenuItem.Click += new System.EventHandler(this.settingsHostMenuItem_Click);
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
            this.loadPluginMenuItem.Size = new System.Drawing.Size(137, 22);
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
            this.aboutHelpMenuItem.Size = new System.Drawing.Size(116, 22);
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
        private System.Windows.Forms.ToolStripMenuItem startHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.OpenFileDialog loadPluginDialog;
        private System.Windows.Forms.ToolStripStatusLabel lblAudimatStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem keyboardBarHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panicHostMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem settingsHostMenuItem;
    }
}

