// <copyright file="ActiveToolStripButton.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-04</date>
// <summary>Активный кнопка меню.</summary>

using System.Drawing;
using System.Windows.Forms;

namespace Makarov.Framework.Components
{
    public sealed class ActiveToolStripButton : ToolStripButton, IActiveControl
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="command">Комманда.</param>
        public ActiveToolStripButton(string text, string command)
            : base(text)
        {
            Tag = command;
        }

        /// <summary>
        /// Заголовок.
        /// </summary>
        public string ActiveCaption
        {
            get { return Text ?? ""; } 
            set { Text = value ?? ""; }
        }

        /// <summary>
        /// Текст всплывающей подсказки.
        /// </summary>
        public string ActiveBaloon
        {
            get { return ToolTipText ?? ""; }
            set { ToolTipText = value ?? ""; }
        }

        /// <summary>
        /// Иконка.
        /// </summary>
        public Bitmap ActiveIcon
        {
            get { return Image == null ? null : (Bitmap)Image; }
            set { Image = value; }
        }

        /// <summary>
        /// Активен ли контрол.
        /// </summary>
        public bool ActiveEnabled
        {
            get { return Enabled; }
            set { Enabled = value; }
        }

        /// <summary>
        /// Нажат ли контрол.
        /// </summary>
        public bool ActiveChecked
        {
            get { return Checked; }
            set { Checked = value; }
        }
    }
}