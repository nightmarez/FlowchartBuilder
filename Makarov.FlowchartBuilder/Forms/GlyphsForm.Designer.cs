using System.Windows.Forms;

namespace Makarov.FlowchartBuilder.Forms
{
    partial class GlyphsForm
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
            this.lstView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstView
            // 
            this.lstView.BackColor = System.Drawing.SystemColors.Control;
            this.lstView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstView.Location = new System.Drawing.Point(0, 0);
            this.lstView.MultiSelect = false;
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(234, 417);
            this.lstView.TabIndex = 0;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstView_ItemSelectionChanged);
            this.lstView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstView_MouseDoubleClick);
            // 
            // GlyphsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 417);
            this.Controls.Add(this.lstView);
            this.Name = "GlyphsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FBGlyphs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FBGlyphs_FormClosing);
            this.Load += new System.EventHandler(this.FBGlyphs_Load);
            this.Shown += new System.EventHandler(this.FBGlyphs_Shown);
            this.Resize += new System.EventHandler(this.FBGlyphs_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lstView;
    }
}