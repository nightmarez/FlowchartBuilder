// <copyright file="Core.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-16</date>
// <summary>Кора (Синглетон).</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Makarov.FlowchartBuilder.Forms;
using Makarov.Framework.Core;
using Makarov.Framework.Graphics;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Кора (Синглетон).
    /// </summary>
    public sealed partial class Core : Singleton<Core>, IDisposable
    {
        #region Constructors
        private Core()
        {
            // Запоминаем, первый ли это запуск программы.
            if (CachedSettingsStorage.KeyExists(Settings.RegistryKeys.FirstStart))
                IsFirstRun = false;
            else
            {
                CachedSettingsStorage[Settings.RegistryKeys.FirstStart] = DateTime.Now.ToString();
                IsFirstRun = true;
            }

            // Сохраняем в реестр время последнего (текущего) запуска.
            CachedSettingsStorage[Settings.RegistryKeys.LastStart] = DateTime.Now.ToString();

            // Указываем, что нужно перерисовывать главное окно.
            NeedRedraw = true;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Запускает главное окно.
        /// </summary>
        /// <param name="args">Аргументы программы.</param>
        public void Run(IEnumerable<string> args)
        {
            // Если заданы имена файлов, загружаем их.
            foreach (string arg in args)
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    AddDocument(new Document(arg));

                    // TODO: лолштоэто?
                    Settings.Environment.LoadedDocumentName = arg;
                }
            }

            // Открываем главное окно.
            Application.Run(MainWindow);
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                throw new CoreAlreadyDisposedException();

            if (_windowsManager != null)
            {
                _windowsManager.Dispose();
                _windowsManager = null;
            }

            if (_graphicsManager != null)
            {
                _graphicsManager.Clear();
                _graphicsManager = null;
                // TODO: dispose?
            }

            if (_iconsManager != null)
            {
                _iconsManager.Clear();
                _iconsManager = null;
                // TODO: dispose?
            }

            if (_brushes != null)
            {
                _brushes.Dispose();
                _brushes = null;
            }

            if (_pens != null)
            {
                _pens.Dispose();
                _pens = null;
            }

            _disposed = true;
        }

        /// <summary>
        /// Возвращает окно указанного типа, являющееся дочерним
        /// по отношению к главному окну.
        /// </summary>
        /// <typeparam name="T">Тип окна.</typeparam>
        /// <returns>Окно указанного типа.</returns>
        public T GetWindowAsChild<T>()
            where T : Form
        {
            if (typeof(T) == typeof(MainForm))
                throw new CantUseMainWindowAsChildException();

            var window = Windows.GetWindow<T>();
            // ReSharper disable RedundantCheckBeforeAssignment
            if (window.Owner != MainWindow) window.Owner = MainWindow;
            // ReSharper restore RedundantCheckBeforeAssignment
            return window;
        }

        /// <summary>
        /// Установить дефолтное расположение окон.
        /// </summary>
        public void SetDefaultWindowsPos()
        {
            // Отключаем на время отрисовку главного окна...
            using (new DontRedraw())
            {
                // Главное окно.
                MainForm mainWindow = MainWindow;

                // Окно глифов.
                var glyphsWindow = GetWindowAsChild<GlyphsForm>();
                glyphsWindow.Visible = true;
                glyphsWindow.Left = mainWindow.WorkRectangle.Left;
                glyphsWindow.Top = mainWindow.WorkRectangle.Top;
                glyphsWindow.Height = mainWindow.WorkRectangle.Height;

                // Окно свойств.
                var propertiesWindow = GetWindowAsChild<PropertiesForm>();
                propertiesWindow.Visible = true;
                propertiesWindow.Left = mainWindow.WorkRectangle.Left +
                                        (mainWindow.WorkRectangle.Width - propertiesWindow.Width);
                propertiesWindow.Top = mainWindow.WorkRectangle.Top;
                propertiesWindow.Height = mainWindow.WorkRectangle.Height >> 1;
            }

            // Перерисовываем основное окно.
            Redraw();
        }

        /// <summary>
        /// Перерисовать главное окно.
        /// </summary>
        public void Redraw()
        {
            // Если заблокирована перерисовка главного окна, выходим.
            if (!NeedRedraw) return;

            // Главное окно.
            MainForm wndMain = MainWindow;

            // Если размеры клиентской области равны нулю,
            // то нечего перерисовывать - выходим.
            if (wndMain.ClientSize.Width <= 0 || wndMain.ClientSize.Height <= 0)
                return;

            // Смещение листа, вызванное скроллбарами.
            Point scrollbarsOffset = UpdateScrollbars(wndMain);

            // Начинаем отрисовку.
            using (var tmp = new Bitmap(wndMain.ClientSize.Width, wndMain.ClientSize.Height))
            {
                using (Graphics gtmp = Graphics.FromImage(tmp))
                {
                    // Событие: начало перерисовки главного окна.
                    if (BeginRedraw != null)
                        BeginRedraw(gtmp, tmp.Width, tmp.Height);

                    // Очищаем область для рисования.
                    gtmp.Clear(Settings.Colors.WorkArea);

                    Rectangle clientRect = wndMain.InnerRectangle;

                    int leftOffset = 0;
                    int topOffset = 0;

                    // Рисуем лист.
                    if (IsDocumentsExists)
                        using (Bitmap sheet = CurrentDocument.DocumentSheet.Draw())
                        {
                            leftOffset = clientRect.Left + ((clientRect.Width - sheet.Width) >> 1) - scrollbarsOffset.X;
                            topOffset = clientRect.Top + ((clientRect.Height - sheet.Height) >> 1) - scrollbarsOffset.Y;

                            const int shadowSize = 3;
                            gtmp.FillRectangle(Brushes.Black, leftOffset + sheet.Width, topOffset + shadowSize,
                                               shadowSize, sheet.Height);
                            gtmp.FillRectangle(Brushes.Black, leftOffset + shadowSize, topOffset + sheet.Height,
                                               sheet.Width - shadowSize, shadowSize);

                            if (Settings.Environment.CanUseNative)
                            {
                                using (Graphics sourceGfx = Graphics.FromImage(sheet))
                                {
                                    Gdi.BitBlt(
                                        sheet, sourceGfx,
                                        0, 0,
                                        gtmp,
                                        leftOffset, topOffset,
                                        sheet.Width, sheet.Height);
                                }
                            }
                            else
                            {
                                gtmp.DrawImageUnscaled(sheet, leftOffset, topOffset);
                            }

                            gtmp.DrawRectangle(
                                Settings.Pens.SheetBorder,
                                leftOffset - 1,
                                topOffset - 1,
                                sheet.Width + 1,
                                sheet.Height + 1);
                        }

                    // Рисуем линейки.
                    DrawRulers(gtmp, clientRect, leftOffset, topOffset);

                    // Событие: конец перерисовки главного окна.
                    // TODO: как-то по-другому обозвать событие что-ли? Ведь окно ещё не отрисовано.
                    if (EndRedraw != null)
                        EndRedraw(gtmp, tmp.Width, tmp.Height);

                    gtmp.Flush();

                    // Отрисовываем получившееся изображение в окно.
                    using (Graphics gfx = wndMain.CreateGraphics())
                        if (Settings.Environment.CanUseNative)
                        {
                            Gdi.BitBlt(tmp, gtmp, 0, 0, gfx, 0, 0, tmp.Width, tmp.Height);
                        }
                        else
                        {
                            gfx.DrawImageUnscaled(tmp, 0, 0);
                        }
                }
            }
        }

        /// <summary>
        /// Добавляет документ.
        /// </summary>
        /// <param name="doc">Документ.</param>
        public void AddDocument(Document doc)
        {
            // Добавляем документ в начало списка.
            _documents.Insert(0, doc);

            // Событие: добавлен документ.
            if (DocumentAdded != null)
                DocumentAdded(doc);

            // TODO:
            MainWindow.DocumentsTabs.AddTab(doc.Name ?? "Document", _documents.Count - 1);
            _currDocIndex = MainWindow.DocumentsTabs.SelectedTab.Id;
        }

        public void SelectDocument(int idx)
        {
            bool idxChanged = false;

            // TODO: бросать другое исключение?
            if (!IsDocumentsExists)
                throw new CurrentDocumentNotExistsException();

            // TODO: бросать другое исключение?
            if (idx < 0 || idx >= _documents.Count)
                throw new CurrentDocumentNotExistsException();

            if (_currDocIndex != idx)
                idxChanged = true;

            _currDocIndex = idx;

            if (idxChanged)
                Redraw();
        }

        /// <summary>
        /// Очищает буфер обмена.
        /// </summary>
        public void ClearBuffer()
        {
            _buffer.Clear();
        }
        #endregion

        #region Events
        /// <summary>
        /// Начало перерисовки главного окна.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        public delegate void BeginRedrawDelegate(Graphics gfx, int width, int height);

        /// <summary>
        /// Начало перерисовки главного окна.
        /// </summary>
        public event BeginRedrawDelegate BeginRedraw;

        /// <summary>
        /// Конец перерисовки главного окна.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        public delegate void EndRedrawDelegate(Graphics gfx, int width, int height);

        /// <summary>
        /// Конец перерисовки главного окна.
        /// </summary>
        public event EndRedrawDelegate EndRedraw;

        /// <summary>
        /// Добавлен документ.
        /// </summary>
        /// <param name="doc">Документ.</param>
        public delegate void DocumentAddedDelegate(Document doc);

        /// <summary>
        /// Добавлен документ.
        /// </summary>
        public event DocumentAddedDelegate DocumentAdded;
        #endregion
    }
}
