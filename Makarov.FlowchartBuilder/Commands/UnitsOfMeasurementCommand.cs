// <copyright file="UnitsOfMeasurementCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-VIII-16</date>
// <summary>Команда - выбор единиц измерения.</summary>

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выбор языка интерфейса.
    /// </summary>
    public sealed class UnitsOfMeasurementCommand : CommandWithSubcommands
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        { }

        /// <summary>
        /// Выполнить подкоманду.
        /// </summary>
        /// <param name="name">Имя подкоманды.</param>
        public override void Run(string name)
        {
            // Устанавливаем новые единицы измерения.
            Settings.Environment.CurrentUnits = Core.Instance.Units[name];

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        /// <summary>
        /// Подкоманды.
        /// </summary>
        public override IEnumerable<string> Commands
        {
            get
            {
                return Core.Instance.Units.LoadedUnits.OrderBy(x => Core.Instance.CurrentTranslator[x]);
            }
        }

        /// <summary>
        /// Возвращает иконку подкоманды.
        /// </summary>
        /// <param name="cmd">Имя подкоманды.</param>
        /// <returns>Иконка подкоманды.</returns>
        public override Bitmap Icon(string cmd)
        {
            //return BaseSettings.Objects.Icons[cmd];
            return null;
        }
    }
}