using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Commands;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Forms
{
    public partial class GlyphsForm : DockingForm
    {
        public GlyphsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Имя окна (для хранения настроек в реестре).
        /// </summary>
        protected override string RegistrySettingsName
        {
            get { return "GlyphsForm"; }
        }

        private void FBGlyphs_Resize(object sender, EventArgs e)
        {
            lstView.Left = ClientRectangle.Left;
            lstView.Top = ClientRectangle.Top;
            lstView.Width = ClientRectangle.Width;
            lstView.Height = ClientRectangle.Height;
        }

        private void FBGlyphs_Shown(object sender, EventArgs e)
        {
            FBGlyphs_Resize(sender, new EventArgs());

            // Создаём список иконок.
            lstView.LargeImageList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

            // Устанавливаем цвет текста.
            lstView.ForeColor = Settings.Colors.ThumbnailText;

            // Группы глифов (имя группы - сортированные глифы, несортированные глифы).
            var groups = new List<KeyValuePair<string, KeyValuePair<List<Type>, List<Type>>>>();

            // Загружаем найденные глифы...
            foreach (var glyphType in PluginsManager.Instance.VisibleGlyphs)
            {
                // Создаём экземпляр глифа.
                var glyph = (AbstractGlyph)Activator.CreateInstance(glyphType);

                // Если нет данной группы, создаём её.
                if (!groups.Any(x => x.Key == glyph.Family))
                {
                    groups.Add(new KeyValuePair<string, KeyValuePair<List<Type>, List<Type>>>(
                        glyph.Family,
                        new KeyValuePair<List<Type>, List<Type>>(new List<Type>(), new List<Type>())));
                }

                // Проверяем, в какой список добавлять глиф.
                if (glyph.OrderExists)
                {
                    bool added = false;

                    for (int i = 0; i < groups.First(x => x.Key == glyph.Family).Value.Value.Count; i++)
                    {
                        // Создаём экземпляр глифа.
                        var tg = (AbstractGlyph)Activator.CreateInstance(groups.First(x => x.Key == glyph.Family).Value.Value[i]);

                        if (glyph.Order <= tg.Order)
                        {
                            groups.First(x => x.Key == glyph.Family).Value.Value.Insert(i, glyphType);
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                        groups.First(x => x.Key == glyph.Family).Value.Value.Add(glyphType);
                }
                else
                {
                    groups.First(x => x.Key == glyph.Family).Value.Value.Add(glyphType);
                }
            }

            // TODO: сортировать правильно. Нужно учитывать GlyphGroupOrderAttribute.

            // Загружаем глифы в окно.
            foreach (var group in groups.OrderBy(x => x.Key))
            {
                lstView.Groups.Add(group.Key, group.Key);

                foreach (var glyphType in group.Value.Key)
                {
                    var glyph = (AbstractGlyph)Activator.CreateInstance(glyphType);

                    if (glyph.ThumbnailWidth > lstView.LargeImageList.ImageSize.Width)
                        lstView.LargeImageList.ImageSize = new Size(
                            glyph.ThumbnailWidth,
                            lstView.LargeImageList.ImageSize.Height);

                    if (glyph.ThumbnailHeight > lstView.LargeImageList.ImageSize.Height)
                        lstView.LargeImageList.ImageSize = new Size(
                            lstView.LargeImageList.ImageSize.Width,
                            glyph.ThumbnailHeight);

                    lstView.LargeImageList.Images.Add(glyph.Name, glyph.Thumbnail);
                    var item = new ListViewItem(glyph.Name, glyph.Name)
                    {
                        Tag = glyphType,
                        Group = lstView.Groups[glyph.Family]
                    };
                    lstView.Items.Add(item);
                }

                foreach (var glyphType in group.Value.Value)
                {
                    var glyph = (AbstractGlyph) Activator.CreateInstance(glyphType);

                    if (glyph.ThumbnailWidth > lstView.LargeImageList.ImageSize.Width)
                        lstView.LargeImageList.ImageSize = new Size(
                            glyph.ThumbnailWidth,
                            lstView.LargeImageList.ImageSize.Height);

                    if (glyph.ThumbnailHeight > lstView.LargeImageList.ImageSize.Height)
                        lstView.LargeImageList.ImageSize = new Size(
                            lstView.LargeImageList.ImageSize.Width,
                            glyph.ThumbnailHeight);

                    string imageKey = glyph.Family + "::" + glyph.Name;
                    lstView.LargeImageList.Images.Add(imageKey, glyph.Thumbnail);
                    var item = new ListViewItem(glyph.Name, imageKey)
                                   {
                                       Tag = glyphType,
                                       Group = lstView.Groups[glyph.Family]
                                   };
                    lstView.Items.Add(item);
                }
            }
        }

        private void FBGlyphs_Load(object sender, EventArgs e)
        {
            // Заголовок окна.
            Text = Core.Instance.CurrentTranslator["Wnd_Palette_Title"];
        }

        private void lstView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // Если нет выбранных элементов, выходим.
            if (!e.IsSelected)
                return;

            // Получаем тип глифа.
            var glyphType = (Type)e.Item.Tag;

            // Есть ли атрибут, указывающий, что нужно не создавать глиф,
            // а вызвать определённую команду.
            var cmdAttrs = glyphType.GetCustomAttributes(typeof (CommandGlyphAttribute), true);
            bool isCmdGlyph = cmdAttrs.Length > 0;

            if (isCmdGlyph)
            {
                foreach (var cmdAttr in cmdAttrs)
                {
                    // Имя команды.
                    string cmd = ((CommandGlyphAttribute)cmdAttr).CommandName;

                    // Экземпляр класса команды.
                    var command = Command.GetInstance(cmd);

                    if (command is NoneCommand)
                    {
                        ClearGlyphsSelection();
                        break;
                    }
                    
                    var glyphCommand = (CommandedGlyphCommand)Command.GetInstance(cmd);

                    // Задаём команде тип вызвавшего её глифа.
                    glyphCommand.GlyphType = glyphType;

                    // Выполняем команду.
                    glyphCommand.Run();
                }

                Core.Instance.CurrentGlyphType = null;
            }
            else
            {
                Core.Instance.CurrentGlyphType = glyphType;
            }

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        private void lstView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Если нет выбранного глифа, выходим.
            if (!Core.Instance.CurrGlyphTypeSelected)
                return;

            // Кидаем экземпляр выбранного глифа на лист.
            Core.Instance.CurrentDocument.DocumentSheet.DeselectAllGlyphs();
            var glyph = (AbstractGlyph) Activator.CreateInstance(Core.Instance.CurrentGlyphType);
            glyph.Selected = true;
            Core.Instance.CurrentDocument.DocumentSheet.Add(glyph);
            Core.Instance.CurrentGlyphType = null;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        private void FBGlyphs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Core.Instance.MainWindow.IsClosing)
                return;

            // Не закрываем окно, а скрываем его.
            e.Cancel = true;
            Hide();

            // Включаем команду отображения окна.
            Command.GetInstance("PaletteWindowCommand").Enabled = true;
        }

        /// <summary>
        /// Снимает выделение глифов.
        /// </summary>
        public void ClearGlyphsSelection()
        {
            // Снимаем выделение.
            lstView.SelectedIndices.Clear();
        }
    }
}
