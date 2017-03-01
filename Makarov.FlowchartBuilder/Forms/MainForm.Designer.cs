namespace Makarov.FlowchartBuilder.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.documentsTabsBar = new Makarov.Framework.Components.TabsPanel();
            this.SuspendLayout();
            // 
            // hScrollBar
            // 
            this.hScrollBar.Location = new System.Drawing.Point(0, 307);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(467, 17);
            this.hScrollBar.TabIndex = 3;
            this.hScrollBar.Visible = false;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Location = new System.Drawing.Point(479, 79);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 209);
            this.vScrollBar.TabIndex = 4;
            this.vScrollBar.Visible = false;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // documentsTabsBar
            // 
            this.documentsTabsBar.BackColor = System.Drawing.SystemColors.Control;
            this.documentsTabsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.documentsTabsBar.Location = new System.Drawing.Point(0, 49);
            this.documentsTabsBar.MaximumSize = new System.Drawing.Size(10000, 24);
            this.documentsTabsBar.Name = "documentsTabsBar";
            this.documentsTabsBar.Size = new System.Drawing.Size(501, 24);
            this.documentsTabsBar.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 346);
            this.Controls.Add(this.documentsTabsBar);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "FBMainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FBMainWindow_FormClosing);
            this.Load += new System.EventHandler(this.FBMainWindow_Load);
            this.Shown += new System.EventHandler(this.FBMainWindow_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FBMainWindow_Paint);
            this.Enter += new System.EventHandler(this.FBMainWindow_Enter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FBMainWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FBMainWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FBMainWindow_MouseUp);
            this.Move += new System.EventHandler(this.FBMainWindow_Move);
            this.Resize += new System.EventHandler(this.FBMainWindow_Resize);
            this.Controls.SetChildIndex(this.vScrollBar, 0);
            this.Controls.SetChildIndex(this.hScrollBar, 0);
            this.Controls.SetChildIndex(this.documentsTabsBar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private Framework.Components.TabsPanel documentsTabsBar;



    }
}