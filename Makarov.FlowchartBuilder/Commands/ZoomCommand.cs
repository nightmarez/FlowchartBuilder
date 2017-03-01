// <copyright file="ZoomCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - масштабировать.</summary>

using System.Collections.Generic;
using System.Drawing;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - масштабировать.
    /// </summary>
    public sealed class ZoomCommand : CommandWithSubcommands
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
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            // Изменяем масштаб.
            float val = int.Parse(name.Substring(0, name.Length - 1)) / 100f;
            Core.Instance.CurrentDocument.DocumentSheet.ScaleFactor = val;

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
                var vals = new List<int>
                                {
                                    400,
                                    200,
                                    150,
                                    125,
                                    100,
                                    75,
                                    50,
                                    25
                                };
                
                foreach (int val in vals)
                    yield return val + "%";
            }
        }

        /// <summary>
        /// Возвращает иконку подкоманды.
        /// </summary>
        /// <param name="cmd">Имя подкоманды.</param>
        /// <returns>Иконка подкоманды.</returns>
        public override Bitmap Icon(string cmd)
        {
            return null;
        }
    }
}