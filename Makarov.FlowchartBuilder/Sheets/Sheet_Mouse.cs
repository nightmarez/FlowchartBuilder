// <copyright file="Sheet_Mouse.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-05-09</date>
// <summary>Лист.</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Sheets
{
    public abstract partial class Sheet
    {
        /// <summary>
        /// Нажатие мыши.
        /// </summary>
        /// <param name="pt">Координаты в пикселях.</param>
        public void MouseDown(Point pt)
        {
            // Если выбран тип глифа для добавления...
            if (Core.Instance.CurrGlyphTypeSelected && Core.Instance.CurrentGlyphType != null)
            {
                // Если тип глифа - блочный глиф...
                if (Core.Instance.CurrentGlyphType == typeof(BlockGlyph) ||
                    Core.Instance.CurrentGlyphType.IsSubclassOf(typeof(BlockGlyph)))
                {
                    // Создаём глиф.
                    var glyph = (BlockGlyph)Activator.CreateInstance(Core.Instance.CurrentGlyphType);

                    // Сбрасываем выбранный для добавления тип глифа.
                    Core.Instance.CurrentGlyphType = null;

                    // Координаты глифа <- координаты курсора мыши.
                    glyph.ScaledX = pt.X;
                    glyph.ScaledY = pt.Y;

                    // Сбрасываем выделение.
                    DeselectAllGlyphs();

                    // Выделяем текущий глиф.
                    glyph.Selected = true;

                    // Добавляем глиф на лист.
                    Add(glyph);

                    // Перерисовываем окно.
                    Core.Instance.Redraw();
                }
                else
                {
                    // TODO
                }
            }

            // Сбрасываем начало выделения.
            _selectionStart = null;

            // Старые координаты курсора <- текущие координаты.
            _oldPt = pt;

            // Анлочим глифы.
            UnlockGlyphs();

            // Манипулировать можно только незафиксированными глифами,
            // потому проходим именно по ним...
            foreach (var nonFixedGlyph in NonFixedGlyphs)
            {
                // Если это блочный глиф...
                if (nonFixedGlyph is BlockGlyph)
                {
                    var blockGlyph = (BlockGlyph)nonFixedGlyph;

                    // Если текущий глиф выбран, проверяем, не хочет ли пользователь
                    // использовать одну из точек манипулирования...
                    if (blockGlyph.Selected)
                    {
                        float halfManipPointSize = blockGlyph.ManipulationPointSize / 2f;

                        // Проходим по всем доступным точкам манипулирования для
                        // текущего глифа, и, если курсор попадает на точку, то...
                        foreach (var mpt in blockGlyph.ManipulationPoints)
                            if (pt.X >= mpt.Coords.X - halfManipPointSize &&
                                pt.X <= mpt.Coords.X + halfManipPointSize &&
                                pt.Y >= mpt.Coords.Y - halfManipPointSize &&
                                pt.Y <= mpt.Coords.Y + halfManipPointSize)
                            {
                                // Устанавливаем направление расширения (сжатия) глифа.
                                blockGlyph.DirectionsAngle = mpt.ArrowDirection.CalculateAngle(mpt.MoveDirection);
                                blockGlyph.Direction = mpt.MoveDirection;

                                // Убираем выделение со всех глифов, кроме текущего.
                                DeselectAllGlyphs(new AbstractGlyph[] { blockGlyph });

                                // Выбираем соответствующий направлению курсор.
                                switch (mpt.ArrowDirection)
                                {
                                    case MoveDirection.Up:
                                    case MoveDirection.Down:
                                        Core.Instance.MainWindow.Cursor = Cursors.SizeNS;
                                        break;

                                    case MoveDirection.Left:
                                    case MoveDirection.Right:
                                        Core.Instance.MainWindow.Cursor = Cursors.SizeWE;
                                        break;

                                    case MoveDirection.DownAndLeft:
                                    case MoveDirection.UpAndRight:
                                        Core.Instance.MainWindow.Cursor = Cursors.SizeNESW;
                                        break;

                                    case MoveDirection.DownAndRight:
                                    case MoveDirection.UpAndLeft:
                                        Core.Instance.MainWindow.Cursor = Cursors.SizeNWSE;
                                        break;
                                }

                                // Перерисовываем окно и выходим.
                                Core.Instance.Redraw();
                                return;
                            }

                        // Если пользователь кликнул по точке вращения...
                        PointF rotPt = blockGlyph.RotationPoints[3];
                        float halfRotationPointSize = blockGlyph.RotationPointSize / 2f;

                        if (pt.X >= rotPt.X - halfRotationPointSize &&
                            pt.X <= rotPt.X + halfRotationPointSize &&
                            pt.Y >= rotPt.Y - halfRotationPointSize &&
                            pt.Y <= rotPt.Y + halfRotationPointSize)
                        {
                            // Устанавливаем направление расширения (сжатия) глифа.
                            blockGlyph.Direction = MoveDirection.Rotate;

                            // Убираем выделение со всех глифов, кроме текущего.
                            DeselectAllGlyphs(new AbstractGlyph[] { blockGlyph });

                            // Выбираем соответствующий направлению курсор.
                            Core.Instance.MainWindow.Cursor = Cursors.Hand;

                            // Перерисовываем окно и выходим.
                            Core.Instance.Redraw();
                            return;
                        }
                    }

                    int halfScaledWidth = blockGlyph.ScaledWidth / 2;
                    int haltScaledHeight = blockGlyph.ScaledHeight / 2;

                    // Если пользователь кликнул в области глифа...
                    if (pt.X >= blockGlyph.ScaledX - halfScaledWidth &&
                        pt.X <= blockGlyph.ScaledX + halfScaledWidth &&
                        pt.Y >= blockGlyph.ScaledY - haltScaledHeight &&
                        pt.Y <= blockGlyph.ScaledY + haltScaledHeight)
                    {
                        // Делаем глифы видимыми (если они закрыты другими глифами).
                        // Если глиф принадлежит группе, делаем видимой всю группу.
                        if (!blockGlyph.Selected)
                        {
                            if (blockGlyph.InGroup)
                            {
                                // Вставляем глифы в начало списка глифов.
                                foreach (var g in blockGlyph.Group)
                                    _glyphs.Remove(g);

                                foreach (var g in blockGlyph.Group)
                                    _glyphs.Insert(0, g);
                            }
                            else
                            {
                                // Вставляем глиф в начало списка глифов.
                                _glyphs.Remove(nonFixedGlyph);
                                _glyphs.Insert(0, nonFixedGlyph);
                            }
                        }

                        // Нужно сделать глиф выделенным.
                        // Если он принадлежит группе, нужно выбрать всю группу,
                        // иначе - только этот глиф.
                        if (blockGlyph.InGroup)
                        {
                            // Если текущий глиф уже выделен, то для всех выделенных глифов и
                            // глифов, принадлежащих той же группе, что и текущий глиф,
                            // нужно разрешить перемещение...
                            if (blockGlyph.Selected)
                            {
                                // Разрешаем перемещение для всех выделенных глифов.
                                foreach (var g in SelectedGlyphs)
                                    if (!g.Fixed && g is BlockGlyph)
                                    {
                                        var bg = (BlockGlyph)g;
                                        bg.Direction = MoveDirection.Move;
                                    }

                                // Выделяем все глифы группы текущего глифа и
                                // разрешаем для них перемещение.
                                foreach (var g in blockGlyph.Group)
                                {
                                    g.Selected = true;
                                    g.Direction = MoveDirection.Move;
                                }
                            }
                            else
                            {
                                // Выделяем все глифы группы текущего глифа и
                                // разрешаем для них перемещение.
                                foreach (var g in blockGlyph.Group)
                                {
                                    g.Selected = true;
                                    g.Direction = MoveDirection.Move;
                                }

                                // Сбрасываем выделение со всех глифов, кроме
                                // глифов группы текущего глифа.
                                DeselectAllGlyphs(blockGlyph.Group);
                            }
                        }
                        else
                        {
                            // Если текущий глиф уже выделен, то для всех выделенных глифов,
                            // нужно разрешить перемещение...
                            if (blockGlyph.Selected)
                            {
                                foreach (var g in SelectedGlyphs)
                                    if (!g.Fixed && g is BlockGlyph)
                                    {
                                        var bg = (BlockGlyph)g;
                                        bg.Direction = MoveDirection.Move;
                                    }
                            }
                            else
                            {
                                // Выделяем глиф.
                                blockGlyph.Selected = true;

                                // Разрешаем перемещение глифа.
                                blockGlyph.Direction = MoveDirection.Move;

                                // Оставляем выделенным только этот глиф.
                                DeselectAllGlyphs(new AbstractGlyph[] { blockGlyph });
                            }
                        }

                        // Устанавливаем курсор перемещения.
                        Core.Instance.MainWindow.Cursor = Cursors.SizeAll;

                        // Отображаем свойства выделенных глифов в окне свойств.
                        ShowProperties();

                        // Перерисовываем окно, апдейтим состояние команд. и выходим.
                        Core.Instance.Redraw();
                        UpdateGlyphsCommands();
                        return;
                    }
                }
            }

            // Начало выделения <- текущие координаты курсора мыши.
            _selectionStart = pt;

            // Конец выделения <- текущие координаты курсора мыши.
            _selectionEnd = pt;

            // Убираем выделение со всех глифов.
            DeselectAllGlyphs();

            // Апдейтим состояние команд.
            UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        /// <summary>
        /// Отжатие мыши.
        /// </summary>
        public void MouseUp()
        {
            // Если есть область, выделенная мышью...
            if (SelectionExists)
            {
                // Убираем выделение со всех глифов.
                DeselectAllGlyphs();

                // Неотрицательные координаты начала области выделения.
                int x = _selectionStart.Value.X > _selectionEnd.X
                            ? _selectionEnd.X
                            : _selectionStart.Value.X;
                int y = _selectionStart.Value.Y > _selectionEnd.Y
                            ? _selectionEnd.Y
                            : _selectionStart.Value.Y;

                // Размеры выделения.
                int width = Math.Abs(_selectionEnd.X - _selectionStart.Value.X);
                int height = Math.Abs(_selectionEnd.Y - _selectionStart.Value.Y);

                // Проходим по всем глифам и задаём выделение всем,
                // кому можно из тех, кто попал в область выделенную мышью.
                foreach (var glyph in NonFixedGlyphs)
                    if (glyph is BlockGlyph)
                    {
                        var vg = (BlockGlyph)glyph;
                        if (vg.InRegion(new Rectangle(x, y, width, height)))
                            vg.Selected = true;
                    }

                // Проходим по всем выделенным глифам и добавляем к ним
                // все глифы, которые состоят в группах выделенных.
                var addGlyphs = new List<BlockGlyph>();
                foreach (var glyph in SelectedGlyphs)
                    if (glyph is BlockGlyph && !glyph.Fixed)
                    {
                        var bg = (BlockGlyph)glyph;
                        if (bg.InGroup)
                        {
                            foreach (var glyphInGroup in bg.Group)
                                if (!addGlyphs.Contains(glyphInGroup))
                                {
                                    addGlyphs.Add(glyphInGroup);
                                    glyphInGroup.Selected = true;
                                }
                        }
                    }
            }

            // Убираем область, выделенную мышью.
            _selectionStart = null;
            Core.Instance.MainWindow.Cursor = Cursors.Arrow;

            // Смотрим, не бросили ли глифы в контейнер...
            //var lockedGlyphs = new List<AbstractGlyph>(LockedGlyphs);
            //if (lockedGlyphs.Count > 0)
            //    foreach (AbstractGlyph glyph in NonFixedGlyphs)
            //        if (glyph is ContainerGlyph)
            //        {
            //            var cg = (ContainerGlyph)glyph;

            //            if (_oldPt.X >= cg.ScaledX - cg.ScaledWidth / 2 &&
            //                _oldPt.X <= cg.ScaledX + cg.ScaledWidth / 2 &&
            //                _oldPt.Y >= cg.ScaledY - cg.ScaledHeight / 2 &&
            //                _oldPt.Y <= cg.ScaledY + cg.ScaledHeight / 2)
            //            {
            //                cg.Add(lockedGlyphs);
            //                break;
            //            }
            //        }

            // Анлочим глифы.
            UnlockGlyphs();

            // Апдейтим состояние команд.
            UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }

        /// <summary>
        /// Движение мыши.
        /// </summary>
        /// <param name="pt">Координаты в пикселях.</param>
        public void MouseMove(Point pt)
        {
            // Если начато выделение, нужно просто обновить координаты конца
            // выделения и перерисовать окно.
            if (_selectionStart.HasValue)
            {
                _selectionEnd = pt;
                Core.Instance.Redraw();
                return;
            }

            // Перерисовка нужна лишь тогда, когда были какие-либо изменения.
            bool needRedraw = false;

            // Проходим по всем незафиксированным глифам...
            foreach (AbstractGlyph glyph in NonFixedGlyphs)
                if (glyph is BlockGlyph)
                {
                    var vg = (BlockGlyph)glyph;

                    // Если нужно глиф двигать, двигаем...
                    if (vg.Direction == MoveDirection.Move)
                    {
                        vg.ScaledXInMM += UnitsConverter.PxToMM(pt.X - _oldPt.X);
                        vg.ScaledYInMM += UnitsConverter.PxToMM(pt.Y - _oldPt.Y);
                        needRedraw = true;
                    }
                    else if (vg.Direction != MoveDirection.None)
                    {
                        float angle = vg.DirectionsAngle;
                        float radians = Framework.Core.Math.DegToRad(angle);
                        var cos = (float) Math.Cos(radians);
                        var sin = (float) Math.Sin(radians);

                        // Если нужно глиф масштабировать, масштабируем...
                        switch (vg.Direction)
                        {
                            case MoveDirection.Up:
                                vg.ScaledHeightInMM +=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * sin -
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * cos;
                                break;

                            case MoveDirection.Down:
                                vg.ScaledHeightInMM -=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * sin -
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * cos;
                                break;

                            case MoveDirection.Left:
                                vg.ScaledWidthInMM -=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * cos +
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * sin;
                                break;

                            case MoveDirection.Right:
                                vg.ScaledWidthInMM +=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * cos +
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * sin;
                                break;

                            case MoveDirection.DownAndLeft:
                                vg.ScaledHeightInMM -=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * sin -
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * cos;

                                vg.ScaledWidthInMM -=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * cos +
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * sin;
                                break;

                            case MoveDirection.UpAndRight:
                                vg.ScaledHeightInMM +=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * sin -
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * cos;

                                vg.ScaledWidthInMM +=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * cos +
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * sin;
                                break;

                            case MoveDirection.DownAndRight:
                                vg.ScaledHeightInMM -=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * sin -
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * cos;

                                vg.ScaledWidthInMM +=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * cos +
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * sin;
                                break;

                            case MoveDirection.UpAndLeft:
                                vg.ScaledHeightInMM +=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * sin -
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * cos;

                                vg.ScaledWidthInMM -=
                                    UnitsConverter.PxToMM((pt.X - _oldPt.X) << 1) * cos +
                                    UnitsConverter.PxToMM((pt.Y - _oldPt.Y) << 1) * sin;
                                break;

                            case MoveDirection.Rotate:
                                vg.Angle += (float)(
                                    (pt.X - _oldPt.X) * Math.Cos(Framework.Core.Math.DegToRad(vg.Angle)) +
                                    (pt.Y - _oldPt.Y) * Math.Sin(Framework.Core.Math.DegToRad(vg.Angle)));
                                break;
                        }

                        needRedraw = true;
                    }
                }
                else
                {
                    // TODO
                }

            // Старые координаты курсора мыши <- текущий координаты.
            _oldPt = pt;

            // Если были какие-либо изменения, перерисовываем окно.
            if (needRedraw)
                Core.Instance.Redraw();
        }
    }
}