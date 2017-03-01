// <copyright file="RulersCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-04</date>
// <summary>Команда - линейки.</summary>

using Makarov.FlowchartBuilder.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - линейки.
    /// </summary>
    public sealed class RulersCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            using (new DontRedraw())
            {
                // Сохраняем новые настройки линеек.
                Settings.Environment.Rulers = !Settings.Environment.Rulers;

                // Корректируем положение дочерних окон.
                foreach (var wnd in Core.Instance.Windows.Windows)
                    if (wnd is DockingForm)
                        ((DockingForm) wnd).UpdateDockedWindowPosition();
            }

            Checked = Settings.Environment.Rulers;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}