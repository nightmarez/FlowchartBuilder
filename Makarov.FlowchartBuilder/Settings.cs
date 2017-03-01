// <copyright file="Settings.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-16</date>
// <summary>Настройки программы.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Настройки программы.
    /// </summary>
    public static class Settings
    {
        #region Files
        /// <summary>
        /// Файлы.
        /// </summary>
        public static class Files
        {
            /// <summary>
            /// Описание главного меню.
            /// </summary>
            public static string MainMenuDefinition
            {
                get { return Path.Combine(Directories.Data, "MainMenu.xml"); }
            }

            /// <summary>
            /// Схема главного меню.
            /// </summary>
            public static string MainMenuSchema
            {
                get { return Path.Combine(Directories.Data, "MainMenu.xsd"); }
            }

            /// <summary>
            /// Описание вторичного меню.
            /// </summary>
            public static string SecondaryMenuDefinition
            {
                get { return Path.Combine(Directories.Data, "SecondaryMenu.xml"); }
            }

            /// <summary>
            /// Схема вторичного меню.
            /// </summary>
            public static string SecondaryMenuSchema
            {
                get { return Path.Combine(Directories.Data, "SecondaryMenu.xsd"); }
            }

            /// <summary>
            /// Описание единиц измерения.
            /// </summary>
            public static string UnitsDefinition
            {
                get { return Path.Combine(Directories.Data, "Units.xml"); }
            }

            /// <summary>
            /// Схема описания единиц измерения.
            /// </summary>
            public static string UnitsSchema
            {
                get { return Path.Combine(Directories.Data, "Units.xsd"); }
            }

            /// <summary>
            /// Схема переводов.
            /// </summary>
            public static string TranslationsSchema
            {
                get { return Path.Combine(Directories.Translations, "Translations.xsd"); }
            }

            /// <summary>
            /// Сборка, реализующая сериализацию.
            /// </summary>
            public static string SerializationDll
            {
                get { return Path.Combine(Directories.Main, @"Makarov.Framework.Serialization.dll"); }
            }

            /// <summary>
            /// Имя файла, в котором хранятся настройки, когда нет доступа к реестру
            /// (например, под Mono).
            /// </summary>
            public static string XmlSettingsFile
            {
                get { return @"FlowchartBuilderSettings.xml"; }
            }
        }
        #endregion

        #region Directories
        /// <summary>
        /// Директории.
        /// </summary>
        public static class Directories
        {
            /// <summary>
            /// Главная директория приложения.
            /// </summary>
            public static string Main
            {
                get
                {
                    if (Application.StartupPath.ToLower().Contains("nunit") ||
                        Application.StartupPath.ToLower().Contains("sharper"))
                        return @"C:\MakarovProject\Makarov\FlowchartBuilder\FlowchartBuilder";

                    return Application.StartupPath;
                }
            }

            /// <summary>
            /// Временная директория для файлов приложения.
            /// </summary>
            public static string TempDirectory
            {
                get
                {
                    return Path.Combine(System.Environment.GetFolderPath(
                      System.Environment.SpecialFolder.LocalApplicationData),
                      "FlowchartBuilderTemp");
                }
            }

            /// <summary>
            /// Директория для хранения информации о файлах, которые
            /// нужно загрузить.
            /// </summary>
            public static string AddFilesDirectory
            {
                get
                {
                    return Path.Combine(
                        TempDirectory,
                        "FilesToAdd");
                }
            }

            /// <summary>
            /// Данные.
            /// </summary>
            public static string Data
            {
                get { return Path.Combine(Main, "Data"); }
            }

            /// <summary>
            /// Иконки.
            /// </summary>
            public static string Icons
            {
                get { return Path.Combine(Main, "Icons"); }
            }

            /// <summary>
            /// Графика.
            /// </summary>
            public static string Graphics
            {
                get { return Path.Combine(Main, "Graphics"); }
            }

            /// <summary>
            /// Плугины.
            /// </summary>
            public static string Plugins
            {
                get { return Path.Combine(Main, "Plugins"); }
            }

            /// <summary>
            /// Переводы.
            /// </summary>
            public static string Translations
            {
                get { return Path.Combine(Main, "Translations"); }
            }
        }
        #endregion

        #region Registry keys
        /// <summary>
        /// Ключи реестра.
        /// </summary>
        public static class RegistryKeys
        {
            /// <summary>
            /// Ключ реестра для хранения всех настроек.
            /// </summary>
            public static string ProgramSettingsKey
            {
                get { return @"Software\MakarovFlowchartBuilder"; }
            }

            /// <summary>
            /// Дата первого запуска программы.
            /// </summary>
            public static string FirstStart
            {
                get { return "FirstStart"; }
            }

            /// <summary>
            /// Дата последнего запуска программы.
            /// </summary>
            public static string LastStart
            {
                get { return "LastStart"; }
            }

            /// <summary>
            /// Язык.
            /// </summary>
            public static string Language
            {
                get { return "Language"; }
            }

            /// <summary>
            /// Язык, который нужно применить при следующей загрузке.
            /// </summary>
            public static string NextLanguage
            {
                get { return "NextLanguage"; }
            }

            /// <summary>
            /// Линейки.
            /// </summary>
            public static string Rulers
            {
                get { return "Rulers"; }
            }

            /// <summary>
            /// Сетка.
            /// </summary>
            public static string Grid
            {
                get { return "Grid"; }
            }

            /// <summary>
            /// Единицы измерения.
            /// </summary>
            public static string Units
            {
                get { return "Units"; }
            }

            /// <summary>
            /// Показывать ли информацию над изменяемым глифом.
            /// </summary>
            public static string SizeLocationLabel
            {
                get { return "SizeAndLocationLabel"; }
            }

            /// <summary>
            /// Параметр вторичного окна.
            /// </summary>
            public static string SecondaryWindowParams
            {
                get { return "SecWndParams"; }
            }

            /// <summary>
            /// Диалог создания нового листа.
            /// </summary>
            public static class NewSheetDialog
            {
                /// <summary>
                /// Показывать ли при старте программы.
                /// </summary>
                public static string ShowOnStartup
                {
                    get { return "ShowDialogOnStartup"; }
                }
            }

            /// <summary>
            /// Ключи главного окна.
            /// </summary>
            public static class MainWindow
            {
                /// <summary>
                /// Координата X.
                /// </summary>
                public static string X
                {
                    get { return "X"; }
                }

                /// <summary>
                /// Координата Y.
                /// </summary>
                public static string Y
                {
                    get { return "Y"; }
                }

                /// <summary>
                /// Ширина.
                /// </summary>
                public static string Width
                {
                    get { return "Width"; }
                }

                /// <summary>
                /// Высота.
                /// </summary>
                public static string Height
                {
                    get { return "Height"; }
                }

                /// <summary>
                /// Состояние окна.
                /// </summary>
                public static string WindowState
                {
                    get { return "WindowState"; }
                }
            }

            /// <summary>
            /// Ограничена ли история.
            /// </summary>
            public static string LimitedHistory
            {
                get { return "LimitedHistory"; }
            }

            /// <summary>
            /// Нужно ли кешировать программные ресурсы.
            /// </summary>
            public static string CacheResources
            {
                get { return "CacheResources"; }
            }

            /// <summary>
            /// Нужно ли кешировать изображения.
            /// </summary>
            public static string CacheImages
            {
                get { return "CacheImages"; }
            }

            /// <summary>
            /// Нужно ли использовать сглаживание.
            /// </summary>
            public static string Antialiasing
            {
                get { return "UseAntialiasing"; }
            }
        }
        #endregion

        #region Brushes
        /// <summary>
        /// Кисти.
        /// </summary>
        public static class Brushes
        {
            /// <summary>
            /// Кисть текста иконки.
            /// </summary>
            public static Brush ThumbnailText
            {
                get
                {
                    if (!Core.Instance.CachedBrushes.KeyExists("ThumbnailText"))
                        return Core.Instance.CachedBrushes["ThumbnailText"] = new SolidBrush(Colors.ThumbnailText);

                    return Core.Instance.CachedBrushes["ThumbnailText"];
                }
            }

            /// <summary>
            /// Кисть выделения.
            /// </summary>
            public static Brush Selection
            {
                get
                {
                    if (!Core.Instance.CachedBrushes.KeyExists("Selection"))
                        return Core.Instance.CachedBrushes["Selection"] = new SolidBrush(Colors.Selection);

                    return Core.Instance.CachedBrushes["Selection"];
                }
            }

            /// <summary>
            /// Кисть выделения группы.
            /// </summary>
            public static Brush GroupSelection
            {
                get
                {
                    if (!Core.Instance.CachedBrushes.KeyExists("GroupSelection"))
                        return Core.Instance.CachedBrushes["GroupSelection"] = new SolidBrush(Colors.GroupSelection);

                    return Core.Instance.CachedBrushes["GroupSelection"];
                }
            }
        }
        #endregion

        #region Pens
        /// <summary>
        /// Перья.
        /// </summary>
        public static class Pens
        {
            /// <summary>
            /// Перо рамки листа.
            /// </summary>
            public static Pen SheetBorder
            {
                get
                {
                    if (!Core.Instance.CachedPens.KeyExists("SheetBorder"))
                        return Core.Instance.CachedPens["SheetBorder"] = new Pen(Colors.SheetBorder);

                    return Core.Instance.CachedPens["SheetBorder"];
                }
            }

            /// <summary>
            /// Перо сетки на листе.
            /// </summary>
            public static Pen SheetGrid
            {
                get
                {
                    if (!Core.Instance.CachedPens.KeyExists("SheetGrid"))
                        return Core.Instance.CachedPens["SheetGrid"] = new Pen(Colors.SheetGrid);

                    return Core.Instance.CachedPens["SheetGrid"];
                }
            }

            /// <summary>
            /// Перо рамки линеек.
            /// </summary>
            public static Pen RulersBorder
            {
                get
                {
                    if (!Core.Instance.CachedPens.KeyExists("RulersBorder"))
                        return Core.Instance.CachedPens["RulersBorder"] = new Pen(Colors.RulersBorder);

                    return Core.Instance.CachedPens["RulersBorder"];
                }
            }

            /// <summary>
            /// Перо рамки иконки.
            /// </summary>
            public static Pen ThumbnailBorder
            {
                get
                {
                    if (!Core.Instance.CachedPens.KeyExists("ThumbnailBorder"))
                    {
                        var pen = new Pen(Colors.ThumbnailBorder) {LineJoin = LineJoin.Bevel};
                        return Core.Instance.CachedPens["ThumbnailBorder"] = pen;
                    }

                    return Core.Instance.CachedPens["ThumbnailBorder"];
                }
            }

            /// <summary>
            /// Перо рамки выделения.
            /// </summary>
            public static Pen SelectionBorder
            {
                get
                {
                    if (!Core.Instance.CachedPens.KeyExists("SelectionBorder"))
                    {
                        var pen = new Pen(Colors.SelectionBorder) {LineJoin = LineJoin.Bevel};
                        return Core.Instance.CachedPens["SelectionBorder"] = pen;
                    }

                    return Core.Instance.CachedPens["SelectionBorder"];
                }
            }

            /// <summary>
            /// Перо рамки выделения группы.
            /// </summary>
            public static Pen GroupSelectionBorder
            {
                get
                {
                    if (!Core.Instance.CachedPens.KeyExists("GroupSelectionBorder"))
                        return Core.Instance.CachedPens["GroupSelectionBorder"] = new Pen(Colors.GroupSelectionBorder);

                    return Core.Instance.CachedPens["GroupSelectionBorder"];
                }
            }
        }
        #endregion

        #region Colors
        /// <summary>
        /// Цвета.
        /// </summary>
        public static class Colors
        {
            /// <summary>
            /// Цвет рабочей области.
            /// </summary>
            public static Color WorkArea
            {
                get { return Color.FromArgb(255, 128, 128, 128); }
            }

            /// <summary>
            /// Цвет листа.
            /// </summary>
            public static Color Sheet
            {
                get { return Color.White; }
            }

            /// <summary>
            /// Цвет рамки листа.
            /// </summary>
            public static Color SheetBorder
            {
                get { return Color.Black; }
            }

            /// <summary>
            /// Цвет сетки на листе.
            /// </summary>
            public static Color SheetGrid
            {
                get 
                {
                    return Color.FromArgb(24, SelectionBorder);
                }
            }

            /// <summary>
            /// Начало градиента листа.
            /// </summary>
            public static Color SheetGradient
            {
                get { return Color.FromArgb(255, 220, 220, 230); }
            }

            /// <summary>
            /// Фон линеек.
            /// </summary>
            public static Color RulersBackground
            {
                get { return Color.White; }
            }

            /// <summary>
            /// Рамка линеек.
            /// </summary>
            public static Color RulersBorder
            {
                get { return Color.Black; }
            }

            /// <summary>
            /// Рамка иконки.
            /// </summary>
            public static Color ThumbnailBorder
            {
                get { return Color.FromArgb(255, 60, 70, 160); }
            }

            /// <summary>
            /// Стартовый цвет градиента иконок.
            /// </summary>
            public static Color ThumbnailGradientStart
            {
                get { return Color.FromArgb(255, 200, 200, 225); }
            }

            /// <summary>
            /// Конечный цвет градиента иконок.
            /// </summary>
            public static Color ThumbnailGradientEnd
            {
                get { return Color.White; }
            }

            /// <summary>
            /// Стартовый цвет градиента объектов иконок.
            /// </summary>
            public static Color ThumbnailObjectGradientStart
            {
                get { return Color.FromArgb(255, 228, 228, 240); }
            }

            /// <summary>
            /// Конечный цвет градиента объектов иконок.
            /// </summary>
            public static Color ThumbnailObjectGradientEnd
            {
                get { return Color.FromArgb(255, 228, 228, 240); }
            }

            /// <summary>
            /// Цвет текста иконки.
            /// </summary>
            public static Color ThumbnailText
            {
                get { return ThumbnailBorder; }
            }

            /// <summary>
            /// Цвет текста.
            /// </summary>
            public static Color Text
            {
                get { return Color.FromArgb(255, 60, 70, 160); }
            }

            /// <summary>
            /// Цвет рамки выделения.
            /// </summary>
            public static Color SelectionBorder
            {
                get { return Color.FromArgb(128, 0, 0, 255); }
            }

            /// <summary>
            /// Цвет рамки выделения группы.
            /// </summary>
            public static Color GroupSelectionBorder
            {
                get { return Color.FromArgb(128, 255, 0, 0); }
            }

            /// <summary>
            /// Цвет выделения.
            /// </summary>
            public static Color Selection
            {
                get { return Color.FromArgb(32, SelectionBorder); }
            }

            /// <summary>
            /// Цвет выделения группы.
            /// </summary>
            public static Color GroupSelection
            {
                get { return Color.FromArgb(32, GroupSelectionBorder); }
            }
        }
        #endregion

        #region Environment
        /// <summary>
        /// Настройки окружения.
        /// </summary>
        public static class Environment
        {
            /// <summary>
            /// Находится ли приложение в режиме отладки.
            /// </summary>
            public static bool IsDebug
            {
                get
                {
#if DEBUG
                    return true;
#else
                    return false;
#endif
                }
            }

            /// <summary>
            /// Можно ли использовать нативные функции Windows.
            /// </summary>
            public static bool CanUseNative
            {
                get { return !Framework.Core.Settings.Environment.IsMono; }
            }

            /// <summary>
            /// Находимся в  Design-time.
            /// <remarks>Хак, для того, чтобы отображались формы в Design-time.</remarks>
            /// </summary>
            public static bool InDesignTime
            {
                get { return !Core.IsInstanceExists && IsDebug; }
            }

            /// <summary>
            /// Единицы измерения по умолчанию.
            /// </summary>
            public static string DefaultUnitsName
            {
                get { return @"Millimeters"; }
            }

            /// <summary>
            /// Текущие единицы измерения.
            /// </summary>
            public static Units CurrentUnits
            {
                get
                {
                    string unitsName = Core.Instance.CachedSettingsStorage[RegistryKeys.Units];

                    if (string.IsNullOrEmpty(unitsName))
                    {
                        Units units = Core.Instance.Units[DefaultUnitsName];
                        CurrentUnits = units;
                        return units;
                    }

                    return Core.Instance.Units[unitsName];
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.Units] = value.Name;
                }
            }

            /// <summary>
            /// Включены ли линейки.
            /// </summary>
            public static bool Rulers
            {
                get
                {
                    var rulers = Core.Instance.CachedSettingsStorage[RegistryKeys.Rulers];
                    bool result;
                    if (bool.TryParse(rulers, out result))
                        return result;
                    return Rulers = true;
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.Rulers] = value.ToString();
                }
            }

            /// <summary>
            /// Ширина линеек.
            /// </summary>
            public static int RulersSize
            {
                get { return 20; }
            }

            /// <summary>
            /// Включена ли сетка.
            /// </summary>
            public static bool Grid
            {
                get
                {
                    var grid = Core.Instance.CachedSettingsStorage[RegistryKeys.Grid];
                    bool result;
                    if (bool.TryParse(grid, out result))
                        return result;
                    return Grid = true;
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.Grid] = value.ToString();
                }
            }

            /// <summary>
            /// Ширина сетки.
            /// </summary>
            public static int GridSize
            {
                get { return 20; }
            }

            /// <summary>
            /// Количество пикселей в дюйме.
            /// </summary>
            // ReSharper disable InconsistentNaming
            public static float DPI
            {
                get { return 96f; }
            }
            // ReSharper restore InconsistentNaming

            /// <summary>
            /// Показывать ли информацию над изменяемым глифом.
            /// </summary>
            public static bool SizeLocationLabel
            {
                get
                {
                    var sizeLocationLabel = Core.Instance.CachedSettingsStorage[RegistryKeys.SizeLocationLabel];
                    bool result;
                    if (bool.TryParse(sizeLocationLabel, out result))
                        return result;
                    return SizeLocationLabel = true;
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.SizeLocationLabel] = value.ToString();
                }
            }

            /// <summary>
            /// Рабочее имя приложения.
            /// </summary>
            public static string ApplicationName
            {
                get { return "MakarovFlowchartBuilder"; }
            }

            public static string ServerPort
            {
                get { return "MakarovFlowchartBuilderServer"; }
            }

            public static string ClientPort
            {
                get { return "MakarovFlowchartBuilderClient"; }
            }

            /// <summary>
            /// Версия программы.
            /// </summary>
            public static string Version
            {
                get { return "1.0 beta"; }
            }

            /// <summary>
            /// Зарегистрирована ли программа.
            /// </summary>
            public static bool Registered
            {
                get { return true; }
            }

            /// <summary>
            /// Адрес электронной почты, на который нужно слать отчёты об ошибках.
            /// </summary>
            public static string ErrorReportMail
            {
                get { return @"m.m.makarov@gmail.com"; }
            }

            /// <summary>
            /// Заголовок письма отчёта об ошибке.
            /// </summary>
            public static string ErrorReportTitle
            {
                get { return @"FlowchartBuilder error report"; }
            }

            /// <summary>
            /// Прилипание окон.
            /// </summary>
            public static class Docking
            {
                /// <summary>
                /// Расстояние, на котором работает прилипание окон.
                /// </summary>
                public static int DockDistance
                {
                    get { return 10; }
                }

                /// <summary>
                /// Расстояние, на котором работает отлипание окон.
                /// </summary>
                public static int UndockDistance
                {
                    get { return 50; }
                }
            }

            /// <summary>
            /// Загруженный документ.
            /// </summary>
            /// <remarks>Имя документа, переданного в качестве параметра
            /// командной строки приложению.</remarks>
            public static string LoadedDocumentName
            {
                get; set;
            }

            /// <summary>
            /// Ограничена ли история.
            /// </summary>
            public static bool LimitedHistory
            {
                get
                {
                    var limitedHistory = Core.Instance.CachedSettingsStorage[RegistryKeys.LimitedHistory];
                    bool result;
                    if (bool.TryParse(limitedHistory, out result))
                        return result;
                    return LimitedHistory = false;
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.LimitedHistory] = value.ToString();
                }
            }

            /// <summary>
            /// Нужно ли кешировать программные ресурсы.
            /// </summary>
            public static bool CacheResources
            {
                get
                {
                    var cacheResources = Core.Instance.CachedSettingsStorage[RegistryKeys.CacheResources];
                    bool result;
                    if (bool.TryParse(cacheResources, out result))
                        return result;
                    return CacheResources = true;
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.CacheResources] = value.ToString();
                }
            }

            /// <summary>
            /// Нужно ли кешировать изображения.
            /// </summary>
            public static bool CacheImages
            {
                get
                {
                    var cacheImages = Core.Instance.CachedSettingsStorage[RegistryKeys.CacheImages];
                    bool result;
                    if (bool.TryParse(cacheImages, out result))
                        return result;
                    return CacheImages = true;
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.CacheImages] = value.ToString();
                }
            }

            /// <summary>
            /// Нужно ли использовать сглаживание.
            /// </summary>
            public static bool Antialiasing
            {
                get
                {
                    var antialiasing = Core.Instance.CachedSettingsStorage[RegistryKeys.Antialiasing];
                    bool result;
                    if (bool.TryParse(antialiasing, out result))
                        return result;
                    return Antialiasing = true;
                }

                set
                {
                    Core.Instance.CachedSettingsStorage[RegistryKeys.Antialiasing] = value.ToString();
                }
            }

            /// <summary>
            /// Расширение файлов приложения.
            /// </summary>
            public static string AppFileExtension
            {
                get { return "mfb"; }
            }

            /// <summary>
            /// Расширение файлов настроек приложения.
            /// </summary>
            public static string AppSettingsExtension
            {
                get { return "fbsettings"; }
            }

            /// <summary>
            /// Языки.
            /// </summary>
            public static class Languages
            {
                /// <summary>
                /// Была ли уже проверка: нужно ли установить другой язык.
                /// </summary>
                private static bool _currLangUpdated;

                /// <summary>
                /// Текущий язык.
                /// </summary>
                public static string CurrentLanguage
                {
                    get
                    {
                        if (!_currLangUpdated)
                        {
                            var nextLang = Core.Instance.CachedSettingsStorage[RegistryKeys.NextLanguage];

                            if (!string.IsNullOrEmpty(nextLang))
                            {
                                Core.Instance.CachedSettingsStorage[RegistryKeys.NextLanguage] = "";
                                Core.Instance.CachedSettingsStorage[RegistryKeys.Language] = nextLang;
                            }

                            _currLangUpdated = true;
                        }

                        var currLang = Core.Instance.CachedSettingsStorage[RegistryKeys.Language];

                        if (string.IsNullOrEmpty(currLang))
                        {
                            CurrentLanguage = "English";
                            return "English";
                        }

                        return currLang;
                    }

                    set
                    {
                        Core.Instance.CachedSettingsStorage[RegistryKeys.NextLanguage] = value;
                    }
                }
            }
        }
        #endregion

        #region Xml
        /// <summary>
        /// Всё, что связано с Xml.
        /// </summary>
        public static class Xml
        {
            #region Xml Namespaces
            /// <summary>
            /// Пространства имён xml.
            /// </summary>
            public static class Namespaces
            {
                /// <summary>
                /// Базовое пространство имён приложения.
                /// </summary>
                public const string Base = @"http://flowchartbuilder.blogspot.com";
            }
            #endregion

            #region Xml Tags
            /// <summary>
            /// Имена тегов xml.
            /// </summary>
            public static class Tags
            {
                /// <summary>
                /// Главное меню.
                /// </summary>
                public const string MainMenu = @"fb:MainMenu";

                /// <summary>
                /// Вторичное меню.
                /// </summary>
                public const string SecondaryMenu = @"fb:SecondaryMenu";

                /// <summary>
                /// Меню.
                /// </summary>
                public const string Menu = @"fb:Menu";

                /// <summary>
                /// Команда.
                /// </summary>
                public const string Command = @"fb:Command";

                /// <summary>
                /// Разделитель.
                /// </summary>
                public const string Separator = @"fb:Separator";

                /// <summary>
                /// Главная нода файла с описанием единиц измерения.
                /// </summary>
                public const string UnitsTypes = @"fb:UnitsTypes";

                /// <summary>
                /// Главная нода документа.
                /// </summary>
                public const string FlowchartBuilderDocument = @"fb:FlowchartBuilderDocument";

                /// <summary>
                /// Нода листа.
                /// </summary>
                public const string Sheet = @"fb:Sheet";

                /// <summary>
                /// Глиф.
                /// </summary>
                public const string Glyph = @"fb:Glyph";

                /// <summary>
                /// Свойство.
                /// </summary>
                public const string Property = @"fb:Property";

                /// <summary>
                /// Настройки.
                /// </summary>
                public const string Settings = @"fb:Settings";
            }
            #endregion

            #region Xml Attributes
            /// <summary>
            /// Имена аттрибутов xml.
            /// </summary>
            public static class Attributes
            {
                /// <summary>
                /// Надпись.
                /// </summary>
                public const string Caption = @"Caption";

                /// <summary>
                /// Класс.
                /// </summary>
                public const string Class = @"Class";

                /// <summary>
                /// Иконка.
                /// </summary>
                public const string Icon = @"Icon";

                /// <summary>
                /// Горячие клавиши.
                /// </summary>
                public const string Shortcut = @"Shortcut";

                /// <summary>
                /// Всплывающая подсказка.
                /// </summary>
                public const string ToolTip = @"ToolTip";

                /// <summary>
                /// Активен ли элемент.
                /// </summary>
                public const string Enabled = @"Enabled";

                /// <summary>
                /// Выбран ли элемент.
                /// </summary>
                public const string Checked = @"Checked";

                /// <summary>
                /// Имя.
                /// </summary>
                public const string Name = @"Name";

                /// <summary>
                /// Краткое имя.
                /// </summary>
                public const string ShortName = @"ShortName";

                /// <summary>
                /// Количество единиц измерения в миллиметре.
                /// </summary>
                public const string InMM = @"InMM";

                /// <summary>
                /// Большой шаг.
                /// </summary>
                public const string BigStep = @"BigStep";

                /// <summary>
                /// Маленький шаг.
                /// </summary>
                public const string SmallStep = @"SmallStep";

                /// <summary>
                /// Имя xml-ноды, описывающей слово с переводом.
                /// </summary>
                public static string Word = @"Word";

                /// <summary>
                /// Имя xml-атрибута, содержащего слово.
                /// </summary>
                public static string Key = @"Key";

                /// <summary>
                /// Значение.
                /// </summary>
                public const string Value = @"fb:Value";

                /// <summary>
                /// Имя класса листа.
                /// </summary>
                public const string SheetClassName = @"fb:ClassName";

                /// <summary>
                /// Имя класса глифа.
                /// </summary>
                public const string GlyphClassName = @"fb:ClassName";

                /// <summary>
                /// Ширина листа в миллиметрах.
                /// </summary>
                public static string SheetWidth
                {
                    get { return "Width"; }
                }

                /// <summary>
                /// Высота листа в миллиметрах.
                /// </summary>
                public static string SheetHeight
                {
                    get { return "Height"; }
                }

                /// <summary>
                /// Версия программы.
                /// </summary>
                public static string ProgramVersion
                {
                    get { return "Version"; }
                }
            }
            #endregion
        }
        #endregion
    }
}
