using System;
using System.Text;
using System.Windows.Forms;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FBAbout_Load(object sender, EventArgs e)
        {
            Text = Core.Instance.CurrentTranslator["Wnd_About_Title"];

            lblVersion.Text = string.Format("{0}{1}",
                                            Core.Instance.CurrentTranslator["Wnd_About_VersionLabel"],
                                            Settings.Environment.Version);
            lblCopyright.Text = Core.Instance.CurrentTranslator["Wnd_About_CopyrightLabel"];

            btnOk.Text = Core.Instance.CurrentTranslator["Close"];

            txtLicense.Text = Core.Instance.CurrentTranslator["License_Text"];

            var plugins = new StringBuilder();
            foreach (IPlugin plugin in PluginsManager.Instance.Plugins)
                plugins.AppendLine(plugin.Name);
            foreach (string name in PluginsManager.Instance.GlyphsLibraries)
                plugins.AppendLine(name + " [glyph library]");
        }
    }
}
