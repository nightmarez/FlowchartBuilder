using System;
using System.Windows.Forms;

namespace Makarov.Framework.Core
{
    public partial class CriticalErrorDialog : Form
    {
        public CriticalErrorDialog(string exception, string message, string stackTrace,
            string errorReportMail, string errorReportTitle)
        {
            InitializeComponent();

            ErrorReportMail = errorReportMail;
            ErrorReportTitle = errorReportTitle;

            lblException.Text = string.Format("Exception: {0}", exception);
            tbExceptionDetails.Text = string.Format("Message:{0}{1}{0}{0}Stack trace:{0}{2}",
                                                    Environment.NewLine,
                                                    message,
                                                    stackTrace);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
            Close();
        }

        private void btnSendReport_Click(object sender, EventArgs e)
        {
            if (!SendReport.Send(ErrorReportMail, ErrorReportTitle, tbExceptionDetails.Text))
            {
                // TODO: не удалось отправить отчёт об ошибке
            }
        }

        /// <summary>
        /// Адрес электронной почты, на которую нужно слать отчёты об ошибках.
        /// </summary>
        public string ErrorReportMail
        {
            get; private set;
        }

        /// <summary>
        /// Заголовок письма отчёта об ошибке.
        /// </summary>
        public string ErrorReportTitle
        {
            get; private set;
        }
    }
}
