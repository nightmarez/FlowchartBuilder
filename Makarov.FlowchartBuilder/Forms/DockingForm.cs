using System;
using System.Windows.Forms;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class DockingForm : Form
    {
        public DockingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Имя данного класса.
        /// </summary>
        public string ClassName
        {
            get
            {
                string[] clsType = GetType().Name.Split('.');
                return clsType[clsType.Length - 1];
            }
        }

        /// <summary>
        /// Часть имени ключа реестра, хранящего настройки данного окна.
        /// </summary>
        protected string RegistrySettingsKey
        {
            get
            {
                return Settings.RegistryKeys.SecondaryWindowParams + "." + RegistrySettingsName;
            }
        }

        /// <summary>
        /// Имя окна (для хранения настроек в реестре).
        /// </summary>
        protected virtual string RegistrySettingsName
        {
            get { throw new NotImplementedException(); }
        }

        private void DockingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Environment.InDesignTime) return;

            if (!(Visible || Enabled))
                return;

            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Left"] = Left.ToString();
            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Top"] = Top.ToString();
            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Width"] = Width.ToString();
            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Height"] = Height.ToString();

            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockLeft"] = DockLeft.ToString();
            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockRight"] = DockRight.ToString();
            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockTop"] = DockTop.ToString();
            Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockBottom"] = DockBottom.ToString();
        }

        /// <summary>
        /// Прилеплено ли окно к левому краю.
        /// </summary>
        public bool DockLeft
        {
            get;
            set;
        }

        /// <summary>
        /// Прилеплено ли окно к правому краю.
        /// </summary>
        public bool DockRight
        {
            get;
            set;
        }

        /// <summary>
        /// Прилеплено ли окно к верхнему краю.
        /// </summary>
        public bool DockTop
        {
            get;
            set;
        }

        /// <summary>
        /// Прилеплено ли окно к нижнему краю.
        /// </summary>
        public bool DockBottom
        {
            get;
            set;
        }

        private void DockingForm_Load(object sender, EventArgs e)
        {
            if (Settings.Environment.InDesignTime) return;

            // Размеры окна.
            string x = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Left"];
            string y = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Top"];
            string width = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Width"];
            string height = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Height"];

            // Устанавливаем координату X окна.
            if (!string.IsNullOrEmpty(x))
            {
                int ix = int.Parse(x);
                if (ix < 0)
                {
                    ix = 0;
                    Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Left"] = ix.ToString();
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
                    Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Top"] = iy.ToString();
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
                    Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Width"] = iwidth.ToString();
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
                    Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".Height"] = iheight.ToString();
                }
                Height = iheight;
            }

            // Прилипание окна.
            string dockLeft = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockLeft"];
            string dockRight = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockRight"];
            string dockTop = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockTop"];
            string dockBottom = Core.Instance.CachedSettingsStorage[RegistrySettingsKey + ".DockBottom"];

            // Прилипание к левому краю.
            if (!string.IsNullOrEmpty(dockLeft))
                DockLeft = bool.Parse(dockLeft);

            // Прилипание к правому краю.
            if (!string.IsNullOrEmpty(dockRight))
                DockRight = bool.Parse(dockRight);

            // Прилипание к верхнему краю.
            if (!string.IsNullOrEmpty(dockTop))
                DockTop = bool.Parse(dockTop);

            // Прилипание к нижнему краю.
            if (!string.IsNullOrEmpty(dockBottom))
                DockBottom = bool.Parse(dockBottom);
        }

        /// <summary>
        /// Прилипание окна при движении.
        /// </summary>
        public void UpdateDockingWindowOnMove()
        {
            // Прилипание к левому краю.
            if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Left - Left) <= Settings.Environment.Docking.DockDistance)
            {
                Left = Core.Instance.MainWindow.WorkRectangle.Left;
                DockLeft = true;
            }
            else if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Left - Left) >= Settings.Environment.Docking.UndockDistance)
                DockLeft = false;

            // Прилипание к верхнему краю.
            if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Top - Top) <= Settings.Environment.Docking.DockDistance)
            {
                Top = Core.Instance.MainWindow.WorkRectangle.Top;
                DockTop = true;
            }
            else if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Top - Top) >= Settings.Environment.Docking.UndockDistance)
                DockTop = false;
        }

        /// <summary>
        /// Прилипание окна при изменении размеров.
        /// </summary>
        public void UpdateDockingWindowOnResize()
        {
            // Прилипание к правому краю.
            if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Right - (Left + Width)) <= Settings.Environment.Docking.DockDistance)
            {
                Width += Core.Instance.MainWindow.WorkRectangle.Right - (Left + Width);
                DockRight = true;
            }
            else if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Right - (Left + Width)) >= Settings.Environment.Docking.UndockDistance)
                DockRight = false;

            // Прилипание к нижнему краю.
            if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Bottom - (Top + Height)) <= Settings.Environment.Docking.DockDistance)
            {
                Height += Core.Instance.MainWindow.WorkRectangle.Bottom - (Top + Height);
                DockBottom = true;
            }
            else if (Math.Abs(Core.Instance.MainWindow.WorkRectangle.Bottom - (Top + Height)) >= Settings.Environment.Docking.UndockDistance)
                DockBottom = false;
        }

        /// <summary>
        /// Обновление размеров и координат прилепленного окна.
        /// </summary>
        public void UpdateDockedWindowPosition()
        {
            if (DockLeft)
                Left = Core.Instance.MainWindow.WorkRectangleWithScrollbars.Left;

            if (DockTop)
                Top = Core.Instance.MainWindow.WorkRectangleWithScrollbars.Top;

            if (DockRight)
                Left = Core.Instance.MainWindow.WorkRectangleWithScrollbars.Right - Width;

            if (DockBottom)
                Top = Core.Instance.MainWindow.WorkRectangleWithScrollbars.Bottom - Height;
        }

        private void DockingForm_Move(object sender, EventArgs e)
        {
            if (Settings.Environment.InDesignTime) return;

            UpdateDockingWindowOnMove();
            UpdateDockingWindowOnResize();
        }

        private void DockingForm_Resize(object sender, EventArgs e)
        {
            if (Settings.Environment.InDesignTime) return;

            UpdateDockingWindowOnResize();
        }
    }
}
