// <copyright file="LanguagesCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-V-30</date>
// <summary>Команда - выбор языка интерфейса.</summary>

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выбор языка интерфейса.
    /// </summary>
    public sealed class LanguagesCommand : CommandWithSubcommands
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
            Settings.Environment.Languages.CurrentLanguage = name;

            MessageBox.Show(
                Core.Instance.CurrentTranslator["Msg_Need_Restart_Application"],
                Core.Instance.CurrentTranslator["Msg_Info"],
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Подкоманды.
        /// </summary>
        public override IEnumerable<string> Commands
        {
            get
            {
                return Core.Instance.Translators.Languages.OrderBy(x => Core.Instance.CurrentTranslator[x]);
            }
        }

        /// <summary>
        /// Возвращает иконку подкоманды.
        /// </summary>
        /// <param name="cmd">Имя подкоманды.</param>
        /// <returns>Иконка подкоманды.</returns>
        public override Bitmap Icon(string cmd)
        {
            return Core.Instance.Icons[cmd];
        }
    }
}