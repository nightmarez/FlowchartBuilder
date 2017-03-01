// <copyright file="PaletteWindowCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-22</date>
// <summary>Команда - показать/скрыть окно палитры.</summary>

using Makarov.FlowchartBuilder.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - показать/скрыть окно палитры.
    /// </summary>
    public sealed class PaletteWindowCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Отключаем команду.
            Enabled = false;

            // Показываем/прячем окно палитры.
            var glyphsFormWindow = Core.Instance.Windows.GetWindow<GlyphsForm>();
            glyphsFormWindow.Visible = !glyphsFormWindow.Visible;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}