using System.Drawing;
using System.Windows.Forms;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class BasicForm : Form
    {
        public BasicForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Главное меню.
        /// </summary>
        public MenuStrip MainMenu
        {
            get { return mnuMain; }
        }

        /// <summary>
        /// Вторичное меню.
        /// </summary>
        public ToolStrip SecondaryMenu
        {
            get { return mnuSecondary; }
        }

        /// <summary>
        /// Статус.
        /// </summary>
        public StatusStrip StatusMenu
        {
            get { return mnuStatus; }
        }

        /// <summary>
        /// Область окна в которой нужно рисовать. Относительно графикса.
        /// </summary>
        public virtual Rectangle InnerRectangle
        {
            get
            {
                return new Rectangle(
                    ClientRectangle.Left,
                    ClientRectangle.Top + SecondaryMenu.Top + SecondaryMenu.Height,
                    ClientRectangle.Width - 2,
                    ClientRectangle.Height - StatusMenu.Height - SecondaryMenu.Height - MainMenu.Height - 2);
            }
        }

        /// <summary>
        /// Загружает размеры и положение окна из реестра.
        /// </summary>
        protected void LoadWindowParamsFromRegistry()
        {
            // Размеры окна.
            string x = Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.X];
            string y = Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Y];
            string width = Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Width];
            string height = Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Height];

            // Устанавливаем координату X окна.
            if (!string.IsNullOrEmpty(x))
            {
                int ix = int.Parse(x);
                if (ix < 0)
                {
                    ix = 0;
                    Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.X] = ix.ToString();
                }
                Left = ix;
            }

            // Устанавливаем координату Y окна.
            if (!string.IsNullOrEmpty(y))
            {
                int iy = int.Parse(y);
                if (iy < 0)
                {
                    iy = 0;
                    Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Y] = iy.ToString();
                }
                Top = iy;
            }

            // Устанавливаем координату ширину окна.
            if (!string.IsNullOrEmpty(width))
            {
                int iwidth = int.Parse(width);
                if (iwidth < 100)
                {
                    iwidth = 100;
                    Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Width] = iwidth.ToString();
                }
                Width = iwidth;
            }

            // Устанавливаем координату высоту окна.
            if (!string.IsNullOrEmpty(height))
            {
                int iheight = int.Parse(height);
                if (iheight < 100)
                {
                    iheight = 100;
                    Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Height] = iheight.ToString();
                }
                Height = iheight;
            }

            // Состояние окна.
            string windowState = Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.WindowState];
            if (!string.IsNullOrEmpty(windowState))
            {
                if (windowState == FormWindowState.Maximized.ToString())
                    WindowState = FormWindowState.Maximized;
                else if (windowState == FormWindowState.Minimized.ToString())
                    WindowState = FormWindowState.Minimized;
            }
        }

        /// <summary>
        /// Сохраняет размеры и положение окна из реестра.
        /// </summary>
        protected void SaveWindowParamsToRegistry()
        {
            // Сохраняем размеры окна.
            if (WindowState == FormWindowState.Normal)
            {
                Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.X] = Left.ToString();
                Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Y] = Top.ToString();
                Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Width] = Width.ToString();
                Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.Height] = Height.ToString();
            }

            // Сохраняем состояние окна.
            Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.MainWindow.WindowState] = WindowState.ToString();
        }

        /// <summary>
        /// Рабочая область окна. Относительно окна.
        /// </summary>
        public Rectangle WorkRectangle
        {
            get
            {
                int borderWidth = (Width - ClientSize.Width) / 2;
                int titlebarHeight = Height - ClientSize.Height - borderWidth;

                return new Rectangle(
                    Left + InnerRectangle.Left + borderWidth,
                    Top + InnerRectangle.Top + titlebarHeight,
                    InnerRectangle.Width,
                    InnerRectangle.Height
                    );
            }
        }
    }
}
