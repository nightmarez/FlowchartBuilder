using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Makarov.FlowchartBuilder.Commands;
using Makarov.FlowchartBuilder.Sheets;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class SheetsForm : Form
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public SheetsForm()
        {
            // Создаём контролы на форме.
            InitializeComponent();

            // Задаём переводы контролам.
            btnOk.Text = Core.Instance.CurrentTranslator["OK"];
            btnCancel.Text = Core.Instance.CurrentTranslator["Cancel"];

            // Чекбокс Show On Startup.
            string showOnStartup = Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.NewSheetDialog.ShowOnStartup];
            cbShowOnStartup.Checked = string.IsNullOrEmpty(showOnStartup) || bool.Parse(showOnStartup);

            // Список картинок для ListView.
            lstView.LargeImageList = new ImageList
            {
                ImageSize = new Size(Sheet.ThumbnailWidth, Sheet.ThumbnailHeight),
                ColorDepth = ColorDepth.Depth32Bit
            };
            lstView.ForeColor = Settings.Colors.ThumbnailText;

            // Создаём коллекцию (семейство - имя, тип).
            var list = new List<KeyValuePair<string, List<KeyValuePair<string, Sheet>>>>();
            var asm = Assembly.GetExecutingAssembly();

            // Перебираем все типы в данной сборке.
            foreach (var cls in asm.GetTypes())
                // Если текущий тип - подходящий класс...
                if (cls.IsClass && cls.IsSubclassOf(typeof(Sheet)) && !cls.IsAbstract)
                {
                    var obj = (Sheet)Activator.CreateInstance(cls);
                    string family = obj.Family;
                    string name = obj.Name;

                    bool familyExists = false;
                    foreach (var kvp in list)
                        if (kvp.Key == family)
                        {
                            familyExists = true;
                            break;
                        }

                    if (!familyExists)
                    {
                        var kvp = new KeyValuePair<string, List<KeyValuePair<string, Sheet>>>(
                            family,
                            new List<KeyValuePair<string, Sheet>>());
                        list.Add(kvp);
                    }

                    foreach (var kvp in list)
                        if (kvp.Key == family)
                            kvp.Value.Add(new KeyValuePair<string, Sheet>(name, obj));
                }

            // Сортируем семейства.
            var sortedList = new List<KeyValuePair<string, List<KeyValuePair<string, Sheet>>>>();
            while (list.Count > 0)
            {
                var curr = list[0];

                foreach (var kvp in list)
                    if (kvp.Key.CompareTo(curr.Key) < 0)
                        curr = kvp;

                sortedList.Add(curr);
                list.Remove(curr);
            }

            // Сортируем листы в семействах.
            foreach (var kvp in sortedList)
            {
                var sortedSheetsList = new List<KeyValuePair<string, Sheet>>();

                while (kvp.Value.Count > 0)
                {
                    var curr = kvp.Value[0];

                    foreach (var kvpSh in kvp.Value)
                        if (kvpSh.Key.CompareTo(curr.Key) < 0)
                            curr = kvpSh;

                    sortedSheetsList.Add(curr);
                    kvp.Value.Remove(curr);
                }

                foreach (var kvpSh in sortedSheetsList)
                    kvp.Value.Add(kvpSh);
            }

            // Выводим все элементы в ListView.
            var groups = new List<string>();
            foreach (var kvp in sortedList)
            {
                if (!groups.Contains(kvp.Key))
                {
                    groups.Add(kvp.Key);
                    lstView.Groups.Add(kvp.Key, Core.Instance.CurrentTranslator[kvp.Key]);
                }

                foreach (var kvps in kvp.Value)
                {
                    string name = kvps.Key;
                    Sheet obj = kvps.Value;

                    string id = string.Format("[{0}] {1}", obj.Family, obj.Name);
                    lstView.LargeImageList.Images.Add(id, obj.Thumbnail);

                    string caption = Core.Instance.CurrentTranslator[name];

                    if (obj is FixedSheet)
                    {
                        var fixedSheet = (FixedSheet) obj;
                        caption += Environment.NewLine + string.Format("[{0} x {1}]",
                                                                       fixedSheet.DefaultWidthInMM,
                                                                       fixedSheet.DefaultHeightInMM);
                    }

                    var item = new ListViewItem(caption, id)
                                   {
                                       Tag = obj.GetType(),
                                       Group = lstView.Groups[kvp.Key]
                                   };

                    lstView.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Обработчик кнопки Cancel.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Список типов листов.
        /// </summary>
        public ListView SheetTypesListView
        {
            get { return lstView; }
        }

        /// <summary>
        /// Обработчик кнопки Ok.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Блокируем окно.
            Enabled = false;

            // Если выбран не один элемент, то выходим и разблокируем окно.
            if (lstView.SelectedItems.Count != 1)
            {
                Enabled = true;
                return;
            }

            // Создаём лист выбранного типа и перерисовываем окно.
            var item = lstView.SelectedItems[0];
            Core.Instance.AddDocument(new Document((Sheet)Activator.CreateInstance((Type)item.Tag)));
            Command.GetInstance("ActualSizeCommand").Run();
            Core.Instance.Redraw();

            // Закрываем окно.
            Close();
        }

        /// <summary>
        /// Обработчик изменения состояния списка доступных листов.
        /// </summary>
        private void lstView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Если выбран один элемент, то разрешаем применить его -
            // активируем кнопку Ok.
            btnOk.Enabled = lstView.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Обработчик изменения состояния флага "Show On Startup".
        /// </summary>
        private void cbShowOnStartup_CheckStateChanged(object sender, EventArgs e)
        {
            // Сохраняем значение флага в реестр.
            Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.NewSheetDialog.ShowOnStartup] =
                cbShowOnStartup.Checked.ToString();
        }

        /// <summary>
        /// Обработчик закрытия окна.
        /// </summary>
        private void FBNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохраняем значение флага "Show On Startup" в реестр - 
            // нужно ли показывать данное окно при старте программы.
            Core.Instance.CachedSettingsStorage[Settings.RegistryKeys.NewSheetDialog.ShowOnStartup] =
                cbShowOnStartup.Checked.ToString();
        }

        /// <summary>
        /// Обработчик двойного клика мышью по списку доступных листов.
        /// </summary>
        private void lstView_DoubleClick(object sender, EventArgs e)
        {
            // Если есть выбранный элемент, то применяем его,
            // вызывая обработчик нажатия кнопки Ok.
            if (lstView.SelectedItems.Count == 1)
                btnOk_Click(sender, e);
        }

        private void SheetsForm_Shown(object sender, EventArgs e)
        {
            // Если это первый запуск программы, устанавливаем стандартное расположение окон.
            if (Core.Instance.IsFirstRun)
                Core.Instance.SetDefaultWindowsPos();
        }
    }
}
