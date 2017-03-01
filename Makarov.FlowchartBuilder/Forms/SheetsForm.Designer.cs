namespace Makarov.FlowchartBuilder.Forms
{
    partial class SheetsForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbShowOnStartup = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstView
            // 
            this.lstView.BackColor = System.Drawing.SystemColors.Control;
            this.lstView.Location = new System.Drawing.Point(12, 12);
            this.lstView.MultiSelect = false;
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(610, 401);
            this.lstView.TabIndex = 0;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.SelectedIndexChanged += new System.EventHandler(this.lstView_SelectedIndexChanged);
            this.lstView.DoubleClick += new System.EventHandler(this.lstView_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(547, 419);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(466, 419);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbShowOnStartup
            // 
            this.cbShowOnStartup.AutoSize = true;
            this.cbShowOnStartup.Checked = true;
            this.cbShowOnStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowOnStartup.Location = new System.Drawing.Point(12, 425);
            this.cbShowOnStartup.Name = "cbShowOnStartup";
            this.cbShowOnStartup.Size = new System.Drawing.Size(107, 17);
            this.cbShowOnStartup.TabIndex = 3;
            this.cbShowOnStartup.Text = "Show On Startup";
            this.cbShowOnStartup.UseVisualStyleBackColor = true;
            this.cbShowOnStartup.CheckStateChanged += new System.EventHandler(this.cbShowOnStartup_CheckStateChanged);
            // 
            // SheetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 454);
            this.Controls.Add(this.cbShowOnStartup);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lstView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SheetsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FBNew";
            this.Shown += new System.EventHandler(this.SheetsForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FBNew_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbShowOnStartup;
    }
}