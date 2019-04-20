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
            this.pluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopHostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devicesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AudimatToolbar = new System.Windows.Forms.ToolStrip();
            this.loadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.patchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.keysToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.panicToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.AudimatStatus = new System.Windows.Forms.StatusStrip();
            this.loadPluginDialog = new System.Windows.Forms.OpenFileDialog();
            this.AudimatMenu.SuspendLayout();
            this.AudimatToolbar.SuspendLayout();
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
            this.exitFileMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.exitFileMenuItem_Click);
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
            this.StartHostMenuItem.Click += new System.EventHandler(this.StartHost_Click);
            // 
            // StopHostMenuItem
            // 
            this.StopHostMenuItem.Name = "StopHostMenuItem";
            this.StopHostMenuItem.Size = new System.Drawing.Size(137, 22);
            this.StopHostMenuItem.Text = "Sto&p Engine";
            this.StopHostMenuItem.Click += new System.EventHandler(this.StopHost_Click);
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
            this.AudimatToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.AudimatToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripButton,
            this.patchToolStripButton,
            this.toolStripSeparator1,
            this.startToolStripButton,
            this.stopToolStripButton,
            this.toolStripSeparator2,
            this.keysToolStripButton,
            this.panicToolStripButton});
            this.AudimatToolbar.Location = new System.Drawing.Point(0, 24);
            this.AudimatToolbar.Name = "AudimatToolbar";
            this.AudimatToolbar.Size = new System.Drawing.Size(384, 25);
            this.AudimatToolbar.TabIndex = 1;
            this.AudimatToolbar.Text = "toolStrip1";
            // 
            // loadToolStripButton
            // 
            this.loadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("loadToolStripButton.Image")));
            this.loadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadToolStripButton.Name = "loadToolStripButton";
            this.loadToolStripButton.Size = new System.Drawing.Size(37, 22);
            this.loadToolStripButton.Text = "&Load";
            this.loadToolStripButton.Click += new System.EventHandler(this.loadPlugin_Click);
            // 
            // patchToolStripButton
            // 
            this.patchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.patchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("patchToolStripButton.Image")));
            this.patchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.patchToolStripButton.Name = "patchToolStripButton";
            this.patchToolStripButton.Size = new System.Drawing.Size(41, 22);
            this.patchToolStripButton.Text = "&Patch";
            this.patchToolStripButton.Click += new System.EventHandler(this.patchButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // startToolStripButton
            // 
            this.startToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("startToolStripButton.Image")));
            this.startToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startToolStripButton.Name = "startToolStripButton";
            this.startToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.startToolStripButton.Text = "&Start";
            this.startToolStripButton.Click += new System.EventHandler(this.StartHost_Click);
            // 
            // stopToolStripButton
            // 
            this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripButton.Image")));
            this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolStripButton.Name = "stopToolStripButton";
            this.stopToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.stopToolStripButton.Text = "St&op";
            this.stopToolStripButton.Click += new System.EventHandler(this.StopHost_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // keysToolStripButton
            // 
            this.keysToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.keysToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("keysToolStripButton.Image")));
            this.keysToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.keysToolStripButton.Name = "keysToolStripButton";
            this.keysToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.keysToolStripButton.Text = "&Keys";
            this.keysToolStripButton.Click += new System.EventHandler(this.keysButton_Click);
            // 
            // panicToolStripButton
            // 
            this.panicToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.panicToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("panicToolStripButton.Image")));
            this.panicToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.panicToolStripButton.Name = "panicToolStripButton";
            this.panicToolStripButton.Size = new System.Drawing.Size(40, 22);
            this.panicToolStripButton.Text = "Pani&c";
            this.panicToolStripButton.Click += new System.EventHandler(this.panicButton_Click);
            // 
            // AudimatStatus
            // 
            this.AudimatStatus.BackColor = System.Drawing.SystemColors.Control;
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
            this.AudimatToolbar.ResumeLayout(false);
            this.AudimatToolbar.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem hostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopHostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devicesMenuItem;
        private System.Windows.Forms.ToolStripButton patchToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton startToolStripButton;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton keysToolStripButton;
        private System.Windows.Forms.ToolStripButton panicToolStripButton;
        private System.Windows.Forms.ToolStripButton loadToolStripButton;
        private System.Windows.Forms.OpenFileDialog loadPluginDialog;
    }
}

