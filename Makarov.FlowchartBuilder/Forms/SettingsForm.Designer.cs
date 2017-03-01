namespace Makarov.FlowchartBuilder.Forms
{
    partial class SettingsForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbCacheImages = new System.Windows.Forms.CheckBox();
            this.cbAntialiasing = new System.Windows.Forms.CheckBox();
            this.cbCacheResources = new System.Windows.Forms.CheckBox();
            this.tbHistoryCount = new System.Windows.Forms.TrackBar();
            this.cbLimitedHistory = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHistoryCount)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(450, 279);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.cbCacheImages);
            this.tabPage1.Controls.Add(this.cbAntialiasing);
            this.tabPage1.Controls.Add(this.cbCacheResources);
            this.tabPage1.Controls.Add(this.tbHistoryCount);
            this.tabPage1.Controls.Add(this.cbLimitedHistory);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(442, 253);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tab_Common";
            // 
            // cbCacheImages
            // 
            this.cbCacheImages.AutoSize = true;
            this.cbCacheImages.Location = new System.Drawing.Point(6, 103);
            this.cbCacheImages.Name = "cbCacheImages";
            this.cbCacheImages.Size = new System.Drawing.Size(110, 17);
            this.cbCacheImages.TabIndex = 3;
            this.cbCacheImages.Text = "Cb_CacheImages";
            this.cbCacheImages.UseVisualStyleBackColor = true;
            // 
            // cbAntialiasing
            // 
            this.cbAntialiasing.AutoSize = true;
            this.cbAntialiasing.Location = new System.Drawing.Point(6, 126);
            this.cbAntialiasing.Name = "cbAntialiasing";
            this.cbAntialiasing.Size = new System.Drawing.Size(98, 17);
            this.cbAntialiasing.TabIndex = 4;
            this.cbAntialiasing.Text = "Cb_Antialiasing";
            this.cbAntialiasing.UseVisualStyleBackColor = true;
            // 
            // cbCacheResources
            // 
            this.cbCacheResources.AutoSize = true;
            this.cbCacheResources.Location = new System.Drawing.Point(6, 80);
            this.cbCacheResources.Name = "cbCacheResources";
            this.cbCacheResources.Size = new System.Drawing.Size(127, 17);
            this.cbCacheResources.TabIndex = 2;
            this.cbCacheResources.Text = "Cb_CacheResources";
            this.cbCacheResources.UseVisualStyleBackColor = true;
            // 
            // tbHistoryCount
            // 
            this.tbHistoryCount.Enabled = false;
            this.tbHistoryCount.Location = new System.Drawing.Point(6, 29);
            this.tbHistoryCount.Name = "tbHistoryCount";
            this.tbHistoryCount.Size = new System.Drawing.Size(359, 45);
            this.tbHistoryCount.TabIndex = 1;
            // 
            // cbLimitedHistory
            // 
            this.cbLimitedHistory.AutoSize = true;
            this.cbLimitedHistory.Location = new System.Drawing.Point(6, 6);
            this.cbLimitedHistory.Name = "cbLimitedHistory";
            this.cbLimitedHistory.Size = new System.Drawing.Size(110, 17);
            this.cbLimitedHistory.TabIndex = 0;
            this.cbLimitedHistory.Text = "Cb_LimitedHistory";
            this.cbLimitedHistory.UseVisualStyleBackColor = true;
            this.cbLimitedHistory.CheckStateChanged += new System.EventHandler(this.cbLimitedHistory_CheckStateChanged);
            this.cbLimitedHistory.Leave += new System.EventHandler(this.cbLimitedHistory_Leave);
            this.cbLimitedHistory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cbLimitedHistory_MouseUp);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(387, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Btn_Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(306, 297);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 21;
            this.btnOk.Text = "Btn_Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 332);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHistoryCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox cbAntialiasing;
        private System.Windows.Forms.CheckBox cbCacheResources;
        private System.Windows.Forms.TrackBar tbHistoryCount;
        private System.Windows.Forms.CheckBox cbLimitedHistory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbCacheImages;
    }
}