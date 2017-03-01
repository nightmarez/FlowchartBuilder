// <copyright file="Sheet.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-18</date>
// <summary>Лист.</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Makarov.FlowchartBuilder.Commands;
using Makarov.FlowchartBuilder.Forms;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Sheets
{
    /// <summary>
    /// Лист.
    /// </summary>
    public abstract partial class Sheet: ICloneable
    {
        #region Exceptions
        /// <summary>
        /// Исключение листа.
        /// </summary>
        public class SheetException : FlowchartBuilderException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            public SheetException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Неправильный размер листа.
        /// </summary>
        public sealed class InvalidSheetSizeException : InvalidValueException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="width">Ширина в миллиметрах.</param>
            /// <param name="height">Высота в миллиметрах.</param>
            public InvalidSheetSizeException(float width, float height)
                : base("Sheet", string.Format("[{0}, {1}]", width, height))
            { }
        }

        /// <summary>
        /// Не удалось получить имя листа.
        /// </summary>
        public sealed class UnknownNameException : SheetException
        {
            /// <param name="type">Тип глифа.</param>
            public UnknownNameException(Type type)
                : base(string.Format(@"Unknown sheet '{0}' name.", type.Name))
            { }
        }

        /// <summary>
        /// Не удалось получить семейство листа.
        /// </summary>
        public sealed class UnknownFamilyException : SheetException
        {
            /// <param name="type">Тип глифа.</param>
            public UnknownFamilyException(Type type)
                : base(string.Format(@"Unknown sheet '{0}' family.", type.Name))
            { }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        protected Sheet()
        {
            // Создаём группы глифов.
            Groups = new List<List<BlockGlyph>>();

            // Включаем возможность экспорта изображений.
            Command.GetInstance("ExportImageCommand").Enabled = true;
        }
        #endregion

        #region Private members
        /// <summary>
        /// Коэффициент масштабирования.
        /// </summary>
        private float _scaleFactor = 0.5f;

        /// <summary>
        /// Глифы.
        /// </summary>
        private readonly List<AbstractGlyph> _glyphs = new List<AbstractGlyph>();

        /// <summary>
        /// Старые координаты мыши.
        /// </summary>
        protected Point _oldPt;

        /// <summary>
        /// Начало выделения в пикселях.
        /// </summary>
        protected Point? _selectionStart;

        /// <summary>
        /// Конец выделения в пикселях.
        /// </summary>
        protected Point _selectionEnd;
        #endregion

        #region Protected methods
        /// <summary>
        /// Апдейтит состояние команд, связанных с глифами.
        /// </summary>
        public void UpdateGlyphsCommands()
        {
            // Если среди выбранных глифов есть глифы, состоящие в группах,
            // нужно сделать активной команду разгруппирования,
            // иначе её нужно отключить.
            bool groupsFinded = false;
            foreach (var glyph in SelectedGlyphs)
                if (glyph is BlockGlyph)
                    if (((BlockGlyph) glyph).InGroup)
                    {
                        Command.GetInstance("UngroupCommand").Enabled = true;
                        groupsFinded = true;
                        break;
                    }

            if (!groupsFinded)
                Command.GetInstance("UngroupCommand").Enabled = false;

            // Команда группировки.
            if (SelectedGlyphsExists)
            {
                // Найденная группа.
                List<BlockGlyph> currGroup = null;

                // Найден ли свободный глиф (не принадлежащий какой-либо группе).
                bool freeGlyphFounded = false;

                // Проходим по всем выделенным глифам...
                foreach (var glyph in SelectedGlyphs)
                    if (glyph is BlockGlyph)
                    {
                        var bg = (BlockGlyph) glyph;

                        if (freeGlyphFounded)
                        {
                            Command.GetInstance("GroupCommand").Enabled = true;
                            break;
                        }

                        if (bg.InGroup)
                        {
                            if (currGroup == null)
                            {
                                currGroup = bg.Group;
                            }
                            else if (currGroup != bg.Group)
                            {
                                Command.GetInstance("GroupCommand").Enabled = true;
                                break;
                            }
                        }
                        else
                        {
                            freeGlyphFounded = true;

                            if (currGroup != null)
                            {
                                Command.GetInstance("GroupCommand").Enabled = true;
                                break;
                            }
                        }
                    }
            }
            else
                Command.GetInstance("GroupCommand").Enabled = false;

            if (SelectedGlyphsExists)
            {
                // Команда удаления.
                Command.GetInstance("DeleteCommand").Enabled = true;

                // Команда вырезания.
                Command.GetInstance("CutCommand").Enabled = true;

                // Команда копирования.
                Command.GetInstance("CopyCommand").Enabled = true;

                // Команда фиксирования.
                Command.GetInstance("FixSelectedCommand").Enabled = true;

                // Команда отмены выделения.
                Command.GetInstance("DeselectCommand").Enabled = true;
            }
            else
            {
                // Команда удаления.
                Command.GetInstance("DeleteCommand").Enabled = false;

                // Команда вырезания.
                Command.GetInstance("CutCommand").Enabled = false;

                // Команда копирования.
                Command.GetInstance("CopyCommand").Enabled = false;

                // Команда фиксирования.
                Command.GetInstance("FixSelectedCommand").Enabled = false;

                // Команда отмены выделения.
                Command.GetInstance("DeselectCommand").Enabled = false;
            }

            // Команда фиксирования всех глифов.
            Command.GetInstance("FixAllCommand").Enabled = GlyphsExists;

            // Команда расфиксирования глифов.
            Command.GetInstance("UnfixAllCommand").Enabled = FixedGlyphsExists;
        }
        #endregion
    }
}