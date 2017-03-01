using System;
using System.Windows.Forms;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var translator = Core.Instance.CurrentTranslator;

            Text = translator["Wnd_OtherSettings"];

            tabPage1.Text = translator["Tab_Common"];
            cbLimitedHistory.Text = translator["Cb_LimitedHistory"];
            cbCacheImages.Text = translator["Cb_CacheImages"];
            cbCacheResources.Text = translator["Cb_CacheResources"];
            cbAntialiasing.Text = translator["Cb_Antialiasing"];

            btnOk.Text = translator["OK"];
            btnCancel.Text = translator["Cancel"];

            cbLimitedHistory.Checked = Settings.Environment.LimitedHistory;
            cbCacheImages.Checked = Settings.Environment.CacheImages;
            cbCacheResources.Checked = Settings.Environment.CacheResources;
            cbAntialiasing.Checked = Settings.Environment.Antialiasing;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.Environment.LimitedHistory = cbLimitedHistory.Checked;
            Settings.Environment.CacheImages = cbCacheImages.Checked;
            Settings.Environment.CacheResources = cbCacheResources.Checked;
            Settings.Environment.Antialiasing = cbAntialiasing.Checked;

            Core.Instance.Redraw();

            Close();
        }

        private void cbLimitedHistory_CheckStateChanged(object sender, EventArgs e)
        {
            tbHistoryCount.Enabled = cbLimitedHistory.Checked;
        }

        private void cbLimitedHistory_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void cbLimitedHistory_Leave(object sender, EventArgs e)
        {
            
        }
    }
}
