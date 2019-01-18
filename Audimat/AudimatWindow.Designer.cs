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
            this.AudimatMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devicesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AudimatToolbar = new System.Windows.Forms.ToolStrip();
            this.AudimatStatus = new System.Windows.Forms.StatusStrip();
            this.AudimatMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AudimatMenu
            // 
            this.AudimatMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.pluginMenuItem,
            this.hostMenuItem,
            this.devicesMenuItem,
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
            this.exitFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.exitFileMenuItem_Click);
            // 
            // pluginMenuItem
            // 
            this.pluginMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPluginMenuItem,
            this.unloadPluginMenuItem});
            this.pluginMenuItem.Name = "pluginMenuItem";
            this.pluginMenuItem.Size = new System.Drawing.Size(53, 20);
            this.pluginMenuItem.Text = "&Plugin";
            // 
            // loadPluginMenuItem
            // 
            this.loadPluginMenuItem.Name = "loadPluginMenuItem";
            this.loadPluginMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.loadPluginMenuItem.Size = new System.Drawing.Size(190, 22);
            this.loadPluginMenuItem.Text = "&Load Plugin";
            // 
            // unloadPluginMenuItem
            // 
            this.unloadPluginMenuItem.Name = "unloadPluginMenuItem";
            this.unloadPluginMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.unloadPluginMenuItem.Size = new System.Drawing.Size(190, 22);
            this.unloadPluginMenuItem.Text = "&Unload Plugin";
            // 
            // hostMenuItem
            // 
            this.hostMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartHostMenuItem,
            this.StopHostMenuItem});
            this.hostMenuItem.Name = "hostMenuItem";
            this.hostMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hostMenuItem.Text = "H&ost";
            // 
            // StartHostMenuItem
            // 
            this.StartHostMenuItem.Name = "StartHostMenuItem";
            this.StartHostMenuItem.Size = new System.Drawing.Size(137, 22);
            this.StartHostMenuItem.Text = "&Start Engine";
            // 
            // StopHostMenuItem
            // 
            this.StopHostMenuItem.Name = "StopHostMenuItem";
            this.StopHostMenuItem.Size = new System.Drawing.Size(137, 22);
            this.StopHostMenuItem.Text = "Sto&p Engine";
            // 
            // devicesMenuItem
            // 
            this.devicesMenuItem.Name = "devicesMenuItem";
            this.devicesMenuItem.Size = new System.Drawing.Size(59, 20);
            this.devicesMenuItem.Text = "&Devices";
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
            // AudimatToolbar
            // 
            this.AudimatToolbar.Location = new System.Drawing.Point(0, 24);
            this.AudimatToolbar.Name = "AudimatToolbar";
            this.AudimatToolbar.Size = new System.Drawing.Size(384, 25);
            this.AudimatToolbar.TabIndex = 1;
            this.AudimatToolbar.Text = "toolStrip1";
            // 
            // AudimatStatus
            // 
            this.AudimatStatus.Location = new System.Drawing.Point(0, 339);
            this.AudimatStatus.Name = "AudimatStatus";
            this.AudimatStatus.Size = new System.Drawing.Size(384, 22);
            this.AudimatStatus.TabIndex = 2;
            this.AudimatStatus.Text = "statusStrip1";
            // 
            // AudimatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.AudimatStatus);
            this.Controls.Add(this.AudimatToolbar);
            this.Controls.Add(this.AudimatMenu);
            this.MainMenuStrip = this.AudimatMenu;
            this.Name = "AudimatWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audimat";
            this.AudimatMenu.ResumeLayout(false);
            this.AudimatMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AudimatMenu;
        private System.Windows.Forms.ToolStrip AudimatToolbar;
        private System.Windows.Forms.StatusStrip AudimatStatus;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devicesMenuItem;
    }
}

