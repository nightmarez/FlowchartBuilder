namespace Makarov.FlowchartBuilder.Forms
{
    partial class BasicForm
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuSecondary = new System.Windows.Forms.ToolStrip();
            this.mnuStatus = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(514, 24);
            this.mnuMain.TabIndex = 0;
            // 
            // mnuSecondary
            // 
            this.mnuSecondary.Location = new System.Drawing.Point(0, 24);
            this.mnuSecondary.Name = "mnuSecondary";
            this.mnuSecondary.Size = new System.Drawing.Size(514, 25);
            this.mnuSecondary.TabIndex = 1;
            // 
            // mnuStatus
            // 
            this.mnuStatus.Location = new System.Drawing.Point(0, 375);
            this.mnuStatus.Name = "mnuStatus";
            this.mnuStatus.Size = new System.Drawing.Size(514, 22);
            this.mnuStatus.TabIndex = 2;
            // 
            // BasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 397);
            this.Controls.Add(this.mnuStatus);
            this.Controls.Add(this.mnuSecondary);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "BasicForm";
            this.Text = "BasicForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStrip mnuSecondary;
        private System.Windows.Forms.StatusStrip mnuStatus;
    }
}