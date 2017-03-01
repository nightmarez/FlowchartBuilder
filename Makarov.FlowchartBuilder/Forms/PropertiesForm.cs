using System.Windows.Forms;
using Makarov.FlowchartBuilder.Commands;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class PropertiesForm : DockingForm
    {
        public PropertiesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Имя окна (для хранения настроек в реестре).
        /// </summary>
        protected override string RegistrySettingsName
        {
            get { return "PropertiesForm"; }
        }

        private void FBProperties_Load(object sender, System.EventArgs e)
        {
            // Заголовок окна.
            Text = Core.Instance.CurrentTranslator["Wnd_Properties_Title"];
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            if (gridView != null)
            {
                gridView.Left = ClientRectangle.Left;
                gridView.Top = ClientRectangle.Top;
                gridView.Width = ClientRectangle.Width;
                gridView.Height = ClientRectangle.Height;
            }
        }

        private void FBProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Core.Instance.MainWindow.IsClosing)
                return;

            // Не закрываем окно, а скрываем его.
            e.Cancel = true;
            Hide();

            // Включаем команду отображения окна.
            Command.GetInstance("PropertiesWindowCommand").Enabled = true;
        }

        public PropertyGrid Grid
        {
            get { return gridView; }
        }

        private void gridView_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Core.Instance.Redraw();
        }
    }
}
