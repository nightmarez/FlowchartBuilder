namespace Makarov.Framework.Core
{
    partial class CriticalErrorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CriticalErrorDialog));
            this.lblError = new System.Windows.Forms.Label();
            this.pbErrorIcon = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSendReport = new System.Windows.Forms.Button();
            this.lblException = new System.Windows.Forms.Label();
            this.tbExceptionDetails = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbErrorIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(50, 12);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(233, 13);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "This application has encountered a critical error.";
            // 
            // pbErrorIcon
            // 
            this.pbErrorIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbErrorIcon.Image")));
            this.pbErrorIcon.Location = new System.Drawing.Point(12, 12);
            this.pbErrorIcon.Name = "pbErrorIcon";
            this.pbErrorIcon.Size = new System.Drawing.Size(32, 32);
            this.pbErrorIcon.TabIndex = 2;
            this.pbErrorIcon.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(307, 239);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSendReport
            // 
            this.btnSendReport.Enabled = false;
            this.btnSendReport.Location = new System.Drawing.Point(197, 239);
            this.btnSendReport.Name = "btnSendReport";
            this.btnSendReport.Size = new System.Drawing.Size(104, 23);
            this.btnSendReport.TabIndex = 0;
            this.btnSendReport.Text = "Send report";
            this.btnSendReport.UseVisualStyleBackColor = true;
            this.btnSendReport.Click += new System.EventHandler(this.btnSendReport_Click);
            // 
            // lblException
            // 
            this.lblException.AutoSize = true;
            this.lblException.Location = new System.Drawing.Point(50, 31);
            this.lblException.Name = "lblException";
            this.lblException.Size = new System.Drawing.Size(69, 13);
            this.lblException.TabIndex = 5;
            this.lblException.Text = "Exception: ...";
            // 
            // tbExceptionDetails
            // 
            this.tbExceptionDetails.Location = new System.Drawing.Point(12, 50);
            this.tbExceptionDetails.Multiline = true;
            this.tbExceptionDetails.Name = "tbExceptionDetails";
            this.tbExceptionDetails.ReadOnly = true;
            this.tbExceptionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbExceptionDetails.Size = new System.Drawing.Size(370, 183);
            this.tbExceptionDetails.TabIndex = 6;
            this.tbExceptionDetails.TabStop = false;
            // 
            // CriticalErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 274);
            this.Controls.Add(this.tbExceptionDetails);
            this.Controls.Add(this.lblException);
            this.Controls.Add(this.btnSendReport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbErrorIcon);
            this.Controls.Add(this.lblError);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CriticalErrorDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Critical Error";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbErrorIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.PictureBox pbErrorIcon;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSendReport;
        private System.Windows.Forms.Label lblException;
        private System.Windows.Forms.TextBox tbExceptionDetails;

    }
}