using System;
using System.Drawing;
using System.Windows.Forms;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Sheets.Iso;
using Makarov.Framework.Components;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class MainForm : BasicForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void FBMainWindow_Load(object sender, EventArgs e)
        {
            // Заголовок окна.
            Text = Core.Instance.CurrentTranslator["FlowchartBuilder_Caption"];

            // Если первый запуск, задаём дефолтные размеры.
            if (Core.Instance.IsFirstRun)
            {
                Left = 50;
                Top = 50;
                Width = 800;
                Height = 600;
                WindowState = FormWindowState.Maximized;
            }

            LoadWindowParamsFromRegistry();

            // Загружаем главное меню.
            MenuLoader.LoadMainMenu(this);

            // Загружаем вторичное меню.
            MenuLoader.LoadSecondaryMenu(this);

            // Создаём окно палитры глифов.
            Core.Instance.GetWindowAsChild<GlyphsForm>().Show();

            // Создаём окно свойств.
            Core.Instance.GetWindowAsChild<PropertiesForm>().Show();

            DocumentsTabs.SelectedTabChanged += (s, args) =>
                                                    {
                                                        var tab = DocumentsTabs.SelectedTab;

                                                        // TODO: Exception?

                                                        Core.Instance.SelectDocument(tab.Id);
                                                    };
        }

        /// <summary>
        /// Происходит ли закрытие приложения.
        /// </summary>
        public bool IsClosing
        {
            get; set;
        }

        private void FBMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsClosing)
                return;

            SaveWindowParamsToRegistry();
            IsClosing = true;

            // Закрываем все окна.
            foreach (var wnd in Core.Instance.Windows.Windows)
                if (wnd != this)
                    wnd.Close();

            Close();
        }

        private void FBMainWindow_Paint(object sender, PaintEventArgs e)
        {
            Core.Instance.Redraw();
        }

        private void FBMainWindow_Resize(object sender, EventArgs e)
        {
            // Обновляем расположение прилипающих окон.
            foreach (var wnd in Core.Instance.Windows.Windows)
                if (wnd is DockingForm)
                {
                    var dockingWnd = (DockingForm)wnd;
                    //dockingWnd.UpdateDockingWindowOnResize();
                    dockingWnd.UpdateDockedWindowPosition();
                }

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        /// <summary>
        /// Область окна в которой нужно рисовать. Относительно графикса.
        /// </summary>
        public override Rectangle InnerRectangle
        {
            get
            {
                int rulersSize = Settings.Environment.Rulers ? Settings.Environment.RulersSize : 0;
                int docsBarHeight = documentsTabsBar.Height;
                Rectangle rect = base.InnerRectangle;
                return new Rectangle(
                    rect.Left + rulersSize,
                    rect.Top + rulersSize + docsBarHeight,
                    rect.Width - rulersSize,
                    rect.Height - rulersSize - docsBarHeight);
            }
        }

        /// <summary>
        /// Область окна в которой нужно рисовать. Относительно графикса.
        /// С учётом скроллбаров.
        /// </summary>
        public Rectangle InnerRectangleWithScrollbars
        {
            get
            {
                int hScrollHeight = hScrollBar.Visible ? hScrollBar.Height : 0;
                int vScrollWidth = vScrollBar.Visible ? vScrollBar.Width : 0;
                var rect = InnerRectangle;
                return new Rectangle(
                    rect.Left,
                    rect.Top,
                    rect.Width - vScrollWidth,
                    rect.Height - hScrollHeight);
            }
        }

        /// <summary>
        /// Рабочая область окна. Относительно окна.
        /// С учётом скроллбаров.
        /// </summary>
        public Rectangle WorkRectangleWithScrollbars
        {
            get
            {
                int hScrollHeight = hScrollBar.Visible ? hScrollBar.Height : 0;
                int vScrollWidth = vScrollBar.Visible ? vScrollBar.Width : 0;
                var rect = WorkRectangle;
                return new Rectangle(
                    rect.Left,
                    rect.Top,
                    rect.Width - vScrollWidth,
                    rect.Height - hScrollHeight);
            }
        }

        private void FBMainWindow_Shown(object sender, EventArgs e)
        {
            // Показать ли при старте окно выбора листа.
            if (!Framework.Core.Settings.Environment.IsMono)
            {
                // TODO: некорректно работает диалог под Mono
                string showOnStartup = Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.NewSheetDialog.ShowOnStartup];
                if (string.IsNullOrEmpty(showOnStartup) || bool.Parse(showOnStartup))
                    if (string.IsNullOrEmpty(Settings.Environment.LoadedDocumentName))
                        Core.Instance.GetWindowAsChild<SheetsForm>().ShowDialog(this);
            }
            else
            {
                // TODO: remove this
                Core.Instance.AddDocument(new Document(new VerticalA4()));
                Core.Instance.Redraw();
            }

            // Если это первый запуск программы, устанавливаем стандартное расположение окон.)
            if (Core.Instance.IsFirstRun)
                Core.Instance.SetDefaultWindowsPos();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        private void FBMainWindow_Enter(object sender, EventArgs e)
        {
            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        /// <summary>
        /// Преобразует координаты из системы окна в систему листа.
        /// </summary>
        private Point CoordinatesInSheet(Point pt)
        {
            if (Core.Instance.CurrentDocument.DocumentSheet is IScaledSize)
            {
                var sizedSheet = (IScaledSize)Core.Instance.CurrentDocument.DocumentSheet;

                // Смещение листа, заданное скроллбарами.
                var hbar = HorizontalScrollBar;
                var vbar = VerticalScrollBar;
                int xScrollbarOffset = hbar.Visible ? hbar.Value - (hbar.Maximum >> 1) : 0;
                int yScrollbarOffset = vbar.Visible ? vbar.Value - (vbar.Maximum >> 1) : 0;

                int x = pt.X - InnerRectangle.Left -
                        ((InnerRectangle.Width - sizedSheet.ScaledWidth.MMToPx())/2) +
                        xScrollbarOffset;
                int y = pt.Y - InnerRectangle.Top -
                        ((InnerRectangle.Height - sizedSheet.ScaledHeight.MMToPx())/2) +
                        yScrollbarOffset;

                return new Point(x, y);
            }
            else
            {
                int x = pt.X - InnerRectangle.Left;
                int y = pt.Y - InnerRectangle.Top;
                return new Point(x, y);
            }
        }

        private void FBMainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (Core.Instance.IsDocumentsExists)
                Core.Instance.CurrentDocument.DocumentSheet.MouseDown(CoordinatesInSheet(new Point(e.X, e.Y)));
        }

        private void FBMainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (Core.Instance.IsDocumentsExists)
                Core.Instance.CurrentDocument.DocumentSheet.MouseUp();
        }

        private void FBMainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (Core.Instance.IsDocumentsExists)
                Core.Instance.CurrentDocument.DocumentSheet.MouseMove(CoordinatesInSheet(new Point(e.X, e.Y)));
        }

        private void FBMainWindow_Move(object sender, EventArgs e)
        {
            // Обновляем расположение прилипающих окон.
            foreach (var wnd in Core.Instance.Windows.Windows)
                if (wnd is DockingForm)
                {
                    var dockingWnd = (DockingForm)wnd;
                    //dockingWnd.UpdateDockingWindowOnMove();
                    dockingWnd.UpdateDockedWindowPosition();
                }
        }

        public HScrollBar HorizontalScrollBar
        {
            get { return hScrollBar; }
        }

        public VScrollBar VerticalScrollBar
        {
            get { return vScrollBar; }
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Core.Instance.Redraw();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Core.Instance.Redraw();
        }

        public FormWindowState OldWindowState
        {
            get; private set;
        }

        public TabsPanel DocumentsTabs
        {
            get { return documentsTabsBar; }
        }

        protected override void DefWndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;

            if (m.Msg == WM_SYSCOMMAND && m.WParam == (IntPtr)SC_MINIMIZE)
            {
                OldWindowState = WindowState;
            }

            base.DefWndProc(ref m);
        }
    }
}
