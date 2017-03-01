// <copyright file="PropertiesWindowCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-22</date>
// <summary>Команда - показать/скрыть окно свойств.</summary>

using Makarov.FlowchartBuilder.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - показать/скрыть окно свойств.
    /// </summary>
    public sealed class PropertiesWindowCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Отключаем команду.
            Enabled = false;

            // Показываем/прячем окно свойств.
            var propWindow = Core.Instance.Windows.GetWindow<PropertiesForm>();
            propWindow.Visible = !propWindow.Visible;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}