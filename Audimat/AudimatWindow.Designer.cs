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
            this.AudimatToolbar = new System.Windows.Forms.ToolStrip();
            this.AudimatStatus = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // AudimatMenu
            // 
            this.AudimatMenu.Location = new System.Drawing.Point(0, 0);
            this.AudimatMenu.Name = "AudimatMenu";
            this.AudimatMenu.Size = new System.Drawing.Size(284, 24);
            this.AudimatMenu.TabIndex = 0;
            this.AudimatMenu.Text = "menuStrip1";
            // 
            // AudimatToolbar
            // 
            this.AudimatToolbar.Location = new System.Drawing.Point(0, 24);
            this.AudimatToolbar.Name = "AudimatToolbar";
            this.AudimatToolbar.Size = new System.Drawing.Size(284, 25);
            this.AudimatToolbar.TabIndex = 1;
            this.AudimatToolbar.Text = "toolStrip1";
            // 
            // AudimatStatus
            // 
            this.AudimatStatus.Location = new System.Drawing.Point(0, 239);
            this.AudimatStatus.Name = "AudimatStatus";
            this.AudimatStatus.Size = new System.Drawing.Size(284, 22);
            this.AudimatStatus.TabIndex = 2;
            this.AudimatStatus.Text = "statusStrip1";
            // 
            // AudimatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.AudimatStatus);
            this.Controls.Add(this.AudimatToolbar);
            this.Controls.Add(this.AudimatMenu);
            this.MainMenuStrip = this.AudimatMenu;
            this.Name = "AudimatWindow";
            this.Text = "Audimat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AudimatMenu;
        private System.Windows.Forms.ToolStrip AudimatToolbar;
        private System.Windows.Forms.StatusStrip AudimatStatus;
    }
}

