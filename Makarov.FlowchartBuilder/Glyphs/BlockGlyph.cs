// <copyright file="BlockGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-15</date>
// <summary>Визуальный глиф.</summary>

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Sheets;
using Makarov.Framework.Core;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Глиф, имеющий размер и координаты.
    /// </summary>
    [GlyphDefaultWidth(30f)]
    [GlyphDefaultHeight(20f)]
    [GlyphMinimalWidth(15f)]
    [GlyphMinimalHeight(15f)]
    public class BlockGlyph : AbstractGlyph
    {
        // ReSharper disable InconsistentNaming
        #region Constructors
        /// <param name="x">Координата центра X в миллиметрах.</param>
        /// <param name="y">Координата центра Y в миллиметрах.</param>
        /// <param name="width">Ширина в миллиметрах.</param>
        /// <param name="height">Высота в миллиметрах.</param>
        protected BlockGlyph(int x, int y, int width, int height)
        {
            XInMM = x;
            YInMM = y;
            WidthInMM = width;
            HeightInMM = height;

            Selected = false;
            Caption = Name;
            ForegroundColor = Color.Black;
            BorderColor = Color.Black;
            BorderWidthInMM = 0.5f;
            TextFont = "Arial".SafeCreateFont(8);
        }

        protected BlockGlyph()
        {
            var range = new Range(50, 100);
            XInMM = RandomGenerator.Instance.Next(range);
            YInMM = RandomGenerator.Instance.Next(range);
            WidthInMM = DefaultWidthInMM;
            HeightInMM = DefaultHeightInMM;

            Selected = false;
            Caption = Name;
            ForegroundColor = Color.Black;
            BorderColor = Color.Black;
            BorderWidthInMM = 0.5f;
            TextFont = "Arial".SafeCreateFont(8);
        }
        #endregion

        #region Private members
        /// <summary>
        /// Угол вращения.
        /// </summary>
        private float _angle;

        /// <summary>
        /// Ширина в миллиметрах.
        /// </summary>
        private float _width;

        /// <summary>
        /// Высота в миллиметрах.
        /// </summary>
        private float _height;
        #endregion

        #region Minimal size properties
        /// <summary>
        /// Минимальная ширина в миллиметрах.
        /// </summary>

        protected float MinimalWidthInMM
        {
            get
            {
                return ((GlyphMinimalWidthAttribute)(GetType().GetCustomAttributes(typeof(GlyphMinimalWidthAttribute), true)[0])).WidthInMM;
            }
        }

        /// <summary>
        /// Минимальная ширина в пикселях.
        /// </summary>
        protected int MinimalWidth
        {
            get { return UnitsConverter.MMToPx(MinimalWidthInMM); }
        }

        /// <summary>
        /// Минимальная ширина в дюймах.
        /// </summary>
        protected float MinimalWidthInInches
        {
            get { return UnitsConverter.MMToInch(MinimalWidthInMM); }
        }

        /// <summary>
        /// Минимальная ширина в текущих единицах.
        /// </summary>
        protected float MinimalWidthInCurr
        {
            get { return UnitsConverter.MMToCurr(MinimalWidthInMM); }
        }

        /// <summary>
        /// Минимальная высота в миллиметрах.
        /// </summary>
        protected float MinimalHeightInMM
        {
            get
            {
                return ((GlyphMinimalHeightAttribute)(GetType().GetCustomAttributes(typeof(GlyphMinimalHeightAttribute), true)[0])).HeightInMM;
            }
        }

        /// <summary>
        /// Минимальная высота в пикселях.
        /// </summary>
        protected int MinimalHeight
        {
            get { return UnitsConverter.MMToPx(MinimalHeightInMM); }
        }

        /// <summary>
        /// Минимальная высота в дюймах.
        /// </summary>
        protected float MinimalHeightInInches
        {
            get { return UnitsConverter.MMToInch(MinimalHeightInMM); }
        }

        /// <summary>
        /// Минимальная высота в текущих единицах.
        /// </summary>
        protected float MinimalHeightInCurr
        {
            get { return UnitsConverter.MMToCurr(MinimalHeightInMM); }
        }
        #endregion

        #region InnerRegion
        /// <summary>
        /// Внутренний регион в пикселях.
        /// </summary>
        /// <remarks>Масштабированный, в нецентрированных координатах.</remarks>
        protected virtual Rectangle InnerRegion
        {
            get 
            { 
                return new Rectangle(
                    ScaledX - (ScaledWidth >> 1), 
                    ScaledY - (ScaledHeight >> 1), 
                    ScaledWidth, 
                    ScaledHeight);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Коэффициент масштабирования.
        /// </summary>
        public float ScaleFactor
        {
            get { return Core.Instance.CurrentDocument.DocumentSheet.ScaleFactor; }
        }

        /// <summary>
        /// Владелец данного глифа.
        /// </summary>
        public ContainerGlyph Owner
        {
            get;
            set;
        }

        /// <summary>
        /// Шрифт.
        /// </summary>
        [Description(@"Text font.")]
        [Category(@"Appearance")]
        [DisplayName(@"Font")]
        [ActiveProperty]
        public Font TextFont
        {
            get;
            set;
        }

        /// <summary>
        /// Толщина рамки в пикселях.
        /// </summary>
        public int BorderWidth
        {
            get { return BorderWidthInMM.MMToPx(); }
            set { BorderWidthInMM = value.PxToMM(); }
        }

        /// <summary>
        /// Масштабированная толщина рамки в пикселях.
        /// </summary>
        public int ScaledBorderWidth
        {
            get { return (BorderWidthInMM * ScaleFactor).MMToPx(); }
            set { BorderWidthInMM = value.PxToMM() / ScaleFactor; }
        }

        /// <summary>
        /// Толщина рамки в миллиметрах.
        /// </summary>
        [Description(@"Border width in mm.")]
        [Category(@"Appearance")]
        [DisplayName(@"Border Width")]
        [ActiveProperty]
        public float BorderWidthInMM
        {
            get; set;
        }

        /// <summary>
        /// Цвет рамки.
        /// </summary>
        [Description(@"Border color.")]
        [Category(@"Appearance")]
        [DisplayName(@"Border Color")]
        [ActiveProperty]
        public Color BorderColor
        {
            get; set;
        }

        /// <summary>
        /// Фоновый цвет.
        /// </summary>
        [Description(@"Background glyph color.")]
        [Category(@"Appearance")]
        [DisplayName(@"Background Color")]
        [ActiveProperty]
        public Color BackgroundColor
        {
            get; set;
        }

        /// <summary>
        /// Передний цвет.
        /// </summary>
        [Description(@"Foreground glyph color.")]
        [Category(@"Appearance")]
        [DisplayName(@"Foreground Color")]
        [ActiveProperty]
        public Color ForegroundColor
        {
            get; set;
        }

        /// <summary>
        /// Принадлежит ли глиф какой-либо группе.
        /// </summary>
        public bool InGroup
        {
            get { return Group != null; }
        }

        /// <summary>
        /// Группа, к которой принадлежит глиф.
        /// </summary>
        public List<BlockGlyph> Group
        {
            get
            {
                return Core.Instance.CurrentDocument.DocumentSheet.Groups.FirstOrDefault(group => group.Contains(this));
            }
        }

        /// <summary>
        /// Левая граница группы в миллиметрах.
        /// </summary>
        public float GroupLeft
        {
            get
            {
                // Если глиф не принадлежит группе, бросаем исключение.
                if (!InGroup)
                    throw new InvalidContextException();

                Sheet sheet = Core.Instance.CurrentDocument.DocumentSheet;

                // Левая граница группы.
                float left = sheet is FixedSheet
                               ? ((FixedSheet)sheet).Width
                               : Core.Instance.MainWindow.Width.PxToMM();

                // Находим новые границы.
                foreach (var glyph in Group)
                    if (glyph.XInMM - glyph.WidthInMM / 2 < left)
                        left = glyph.XInMM - glyph.WidthInMM / 2;

                return left;
            }
        }

        /// <summary>
        /// Правая граница группы в миллиметрах.
        /// </summary>
        public float GroupRight
        {
            get
            {
                // Если глиф не принадлежит группе, бросаем исключение.
                if (!InGroup)
                    throw new InvalidContextException();

                // Правая граница группы.
                float right = 0;

                // Находим новые границы.
                foreach (var glyph in Group)
                    if (glyph.XInMM + glyph.WidthInMM / 2 > right)
                        right = glyph.XInMM + glyph.WidthInMM / 2;

                return right;
            }
        }

        /// <summary>
        /// Верхняя граница группы в миллиметрах.
        /// </summary>
        public float GroupTop
        {
            get
            {
                // Если глиф не принадлежит группе, бросаем исключение.
                if (!InGroup)
                    throw new InvalidContextException();

                Sheet sheet = Core.Instance.CurrentDocument.DocumentSheet;

                // Верхняя граница группы.
                float top = sheet is FixedSheet
                               ? ((FixedSheet)sheet).Height
                               : Core.Instance.MainWindow.Height.PxToMM();

                // Находим новые границы.
                foreach (var glyph in Group)
                    if (glyph.YInMM - glyph.HeightInMM / 2 < top)
                        top = glyph.YInMM - glyph.HeightInMM / 2;

                return top;
            }
        }

        /// <summary>
        /// Нижняя граница группы в миллиметрах.
        /// </summary>
        public float GroupBottom
        {
            get
            {
                // Если глиф не принадлежит группе, бросаем исключение.
                if (!InGroup)
                    throw new InvalidContextException();

                // Правая граница группы.
                float bottom = 0;

                // Находим новые границы.
                foreach (var glyph in Group)
                    if (glyph.YInMM + glyph.HeightInMM / 2 > bottom)
                        bottom = glyph.YInMM + glyph.HeightInMM / 2;

                return bottom;
            }
        }

        /// <summary>
        /// Координата X центра группы в миллиметрах.
        /// </summary>
        public float GroupX
        {
            get { return (GroupRight + GroupLeft) / 2; }

            set
            {
                // Разница нового значения и текущего.
                float dx = value - GroupX;

                // Смещаем все глифы на разницу.
                foreach (var glyph in Group)
                    glyph.XInMM += dx;
            }
        }

        /// <summary>
        /// Координата Y центра группы в миллиметрах.
        /// </summary>
        public float GroupY
        {
            get { return (GroupBottom + GroupTop) / 2; }

            set
            {
                // Разница нового значения и текущего.
                float dy = value - GroupY;

                // Смещаем все глифы на разницу.
                foreach (var glyph in Group)
                    glyph.YInMM += dy;
            }
        }

        /// <summary>
        /// Координаты центра группы в миллиметрах.
        /// </summary>
        public PointF GroupCenter
        {
            get { return new PointF(GroupX, GroupY); }

            set
            {
                GroupX = value.X;
                GroupY = value.Y;
            }
        }

        /// <summary>
        /// Ширина группы в миллиметрах.
        /// </summary>
        public float GroupWidth
        {
            get { return GroupRight - GroupLeft; }
        }

        /// <summary>
        /// Высота группы в миллиметрах.
        /// </summary>
        public float GroupHeight
        {
            get { return GroupBottom - GroupTop; }
        }
        #endregion

        #region Manipulation properties
        /// <summary>
        /// Куда можно двигать в данный момент.
        /// </summary>
        public MoveDirection Direction
        {
            get;
            set;
        }

        /// <summary>
        /// Угол между направлением движения и направлением стрелок.
        /// </summary>
        public float DirectionsAngle
        {
            get;
            set;
        }

        /// <summary>
        /// Размер точки манипулирования в пикселях.
        /// </summary>
        public virtual float ManipulationPointSize
        {
            get { return 7f; }
        }

        /// <summary>
        /// Размер точки вращения в пикселях.
        /// </summary>
        public virtual float RotationPointSize
        {
            get { return ManipulationPointSize * 1.5f; }
        }

        /// <summary>
        /// Смещение точки вращения от глифа в пикселях.
        /// </summary>
        public virtual float RotationPointOffset
        {
            get { return 40f; }
        }

        /// <summary>
        /// Точка вращения.
        /// </summary>
        public PointF[] RotationPoints
        {
            get
            {
                float angle = Angle.DegToRad();
                var cos = (float) System.Math.Cos(angle);
                var sin = (float) System.Math.Sin(angle);

                var startPoint = new PointF(
                    ScaledX,
                    ScaledY - (ScaledHeight >> 1));

                var pt0 = new PointF(
                    startPoint.X,
                    startPoint.Y - ManipulationPointSize / 2f);

                var endPoint = new PointF(
                    ScaledX,
                    ScaledY - (ScaledHeight >> 1) - RotationPointOffset);

                var pt1 = new PointF(
                    endPoint.X,
                    endPoint.Y + RotationPointSize/2f);

                var point0 = new PointF(
                    (startPoint.X - ScaledX) * cos - (startPoint.Y - ScaledY) * sin + ScaledX,
                    (startPoint.Y - ScaledY) * cos + (startPoint.X - ScaledX) * sin + ScaledY);

                var point1 = new PointF(
                    (pt0.X - ScaledX) * cos - (pt0.Y - ScaledY) * sin + ScaledX,
                    (pt0.Y - ScaledY) * cos + (pt0.X - ScaledX) * sin + ScaledY);

                var point2 = new PointF(
                    (pt1.X - ScaledX) * cos - (pt1.Y - ScaledY) * sin + ScaledX,
                    (pt1.Y - ScaledY) * cos + (pt1.X - ScaledX) * sin + ScaledY);

                var point3 = new PointF(
                    (endPoint.X - ScaledX) * cos - (endPoint.Y - ScaledY) * sin + ScaledX,
                    (endPoint.Y - ScaledY) * cos + (endPoint.X - ScaledX) * sin + ScaledY);

                return new[] { point0, point1, point2, point3 };
            }
        }

        /// <summary>
        /// Точки манипулирования.
        /// </summary>
        public virtual IEnumerable<ManipPoint> ManipulationPoints
        {
            get
            {
                float halfScaledWidth = ScaledWidth/2f;
                float halfScaledHeight = ScaledHeight/2f;

                var pts = new[]
                              {
                                  // Up
                                  new PointF(
                                      ScaledX,
                                      ScaledY - halfScaledHeight),

                                  // Up-Right
                                  new PointF(
                                      ScaledX + halfScaledWidth,
                                      ScaledY - halfScaledHeight),

                                  // Right
                                  new PointF(
                                      ScaledX + halfScaledWidth,
                                      ScaledY),

                                  // Bottom-Right
                                  new PointF(
                                      ScaledX + halfScaledWidth,
                                      ScaledY + halfScaledHeight),

                                  // Bottom
                                  new PointF(
                                      ScaledX,
                                      ScaledY + halfScaledHeight),

                                  // Bottom-Left
                                  new PointF(
                                      ScaledX - halfScaledWidth,
                                      ScaledY + halfScaledHeight),

                                  // Left
                                  new PointF(
                                      ScaledX - halfScaledWidth,
                                      ScaledY),

                                  // Up-Left
                                  new PointF(
                                      ScaledX - halfScaledWidth,
                                      ScaledY - halfScaledHeight)
                              };

                var directions = MoveDirectionHelper.GetDirectionsArray();

                // Применяем вращение.
                float angle = Angle;
                float radians = angle.DegToRad();
                var cos = (float) System.Math.Cos(radians);
                var sin = (float) System.Math.Sin(radians);

                var directionOffset = angle > 0
                    ? (int)System.Math.Truncate((angle + (45 / 2f)) / 45f)
                    : (int)System.Math.Truncate((angle - (45 / 2f)) / 45f);

                for (int i = 0; i < pts.Length; i++)
                {
                    PointF pt = pts[i];

                    int j = i + directionOffset;
                    if (j < 0) j = directions.Length + j;
                    if (j >= directions.Length) j %= directions.Length;
                    MoveDirection direction = directions[j];

                    yield return new ManipPoint(
                        new PointF(
                            (pt.X - ScaledX) * cos - (pt.Y - ScaledY) * sin + ScaledX,
                            (pt.Y - ScaledY) * cos + (pt.X - ScaledX) * sin + ScaledY),
                        directions[i],
                        direction);
                }
            }
        }
        #endregion

        #region Link properties
        /// <summary>
        /// Размер точки соединения в пикселях.
        /// </summary>
        public virtual float LinkPointSize
        {
            get { return 7f; }
        }

        /// <summary>
        /// Точки соединения.
        /// </summary>
        public virtual IEnumerable<LinkPoint> LinkPoints
        {
            get { return new List<LinkPoint>(); }
        }
        #endregion

        #region Default values properties
        /// <summary>
        /// Дефолтная координата X в миллиметрах.
        /// </summary>
        protected float DefaultXInMM
        {
            get
            {
                if (Core.Instance.CurrentDocument.DocumentSheet is ISize)
                {
                    var sizedSheet = (ISize)Core.Instance.CurrentDocument.DocumentSheet;
                    float sheetWidth = sizedSheet.Width;
                    return RandomGenerator.Instance.NextFloat(WidthInMM/2, sheetWidth - WidthInMM/2);
                }

                throw new InvalidContextException();
            }
        }

        /// <summary>
        /// Дефолтная координта X в пикселях.
        /// </summary>
        protected int DefaultX
        {
            get { return UnitsConverter.MMToPx(DefaultXInMM); }
        }

        /// <summary>
        /// Дефолтная координата X в дюймах.
        /// </summary>
        protected float DefaultXInInches
        {
            get { return UnitsConverter.MMToInch(DefaultXInMM); }
        }

        /// <summary>
        /// Дефолтная координата X в текущих единицах.
        /// </summary>
        protected float DefaultXInCurr
        {
            get { return UnitsConverter.MMToCurr(DefaultXInMM); }
        }

        /// <summary>
        /// Дефолтная координата Y в миллиметрах.
        /// </summary>
        protected float DefaultYInMM
        {
            get
            {
                if (Core.Instance.CurrentDocument.DocumentSheet is ISize)
                {
                    var sizedSheet = (ISize)Core.Instance.CurrentDocument.DocumentSheet;
                    float sheetHeight = sizedSheet.Height;
                    return RandomGenerator.Instance.NextFloat(HeightInMM/2, sheetHeight - HeightInMM/2);
                }

                throw new InvalidContextException();
            }
        }

        /// <summary>
        /// Дефолтная координта Y в пикселях.
        /// </summary>
        protected int DefaultY
        {
            get { return UnitsConverter.MMToPx(DefaultYInMM); }
        }

        /// <summary>
        /// Дефолтная координата Y в дюймах.
        /// </summary>
        protected float DefaultYInInches
        {
            get { return UnitsConverter.MMToInch(DefaultYInMM); }
        }

        /// <summary>
        /// Дефолтная координата Y в текущих единицах.
        /// </summary>
        protected float DefaultYInCurr
        {
            get { return UnitsConverter.MMToCurr(DefaultYInMM); }
        }

        /// <summary>
        /// Дефолтная ширина в миллиметрах.
        /// </summary>
        public float DefaultWidthInMM
        {
            get
            {
                return ((GlyphDefaultWidthAttribute)(GetType().GetCustomAttributes(typeof(GlyphDefaultWidthAttribute), true)[0])).WidthInMM;
            }
        }

        /// <summary>
        /// Дефолтная ширина в пикселях.
        /// </summary>
        protected int DefaultWidth
        {
            get { return UnitsConverter.MMToPx(DefaultWidthInMM); }
        }

        /// <summary>
        /// Дефолтная ширина в дюймах.
        /// </summary>
        protected float DefaultWidthInInches
        {
            get { return UnitsConverter.MMToInch(DefaultWidthInMM); }
        }

        /// <summary>
        /// Дефолтная ширина в текущих единицах.
        /// </summary>
        protected float DefaultWidthInCurr
        {
            get { return UnitsConverter.MMToCurr(DefaultWidthInMM); }
        }

        /// <summary>
        /// Дефолтная высота в миллиметрах.
        /// </summary>
        public float DefaultHeightInMM
        {
            get
            {
                return ((GlyphDefaultHeightAttribute)(GetType().GetCustomAttributes(typeof(GlyphDefaultHeightAttribute), true)[0])).HeightInMM;
            }
        }

        /// <summary>
        /// Дефолтная высота в пикселях.
        /// </summary>
        protected int DefaultHeight
        {
            get { return UnitsConverter.MMToPx(DefaultHeightInMM); }
        }

        /// <summary>
        /// Дефолтная высота в дюймах.
        /// </summary>
        protected float DefaultHeightInInches
        {
            get { return UnitsConverter.MMToInch(DefaultHeightInMM); }
        }

        /// <summary>
        /// Дефолтная высота в текущих единицах.
        /// </summary>
        protected float DefaultHeightInCurr
        {
            get { return UnitsConverter.MMToCurr(DefaultHeightInMM); }
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Отрисовать картинку глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawGlyph(Graphics gfx)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(10, Color.Blue)))
            {
                gfx.FillRectangle(
                    brush,
                    ScaledX - (ScaledWidth >> 1), ScaledY - (ScaledHeight >> 1),
                    ScaledWidth, ScaledHeight);
            }

            using (var pen = new Pen(Color.Silver))
            {
                pen.DashPattern = new float[] { 4, 2 };
                gfx.DrawRectangle(
                    pen,
                    ScaledX - (ScaledWidth >> 1), ScaledY - (ScaledHeight >> 1),
                    ScaledWidth, ScaledHeight);
            }

            const int crossWidth = 40;

            gfx.DrawLine(
                Pens.Silver, 
                ScaledX - (crossWidth >> 2), ScaledY, 
                ScaledX + (crossWidth >> 2), ScaledY);

            gfx.DrawLine(
                Pens.Silver, 
                ScaledX, ScaledY - (crossWidth >> 2), 
                ScaledX, ScaledY + (crossWidth >> 2));
        }

        /// <summary>
        /// Отрисовать заголовок.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawText(Graphics gfx)
        {
            using (var font = "Arial".SafeCreateFont(8))
            using (var brush = new SolidBrush(ForegroundColor))
                gfx.DrawString(Caption, font, brush, InnerRegion);
        }

        /// <summary>
        /// Отрисовать точки манипулирования.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawDots(Graphics gfx)
        {
            Pen pen;
            Brush brush;

            if (InGroup)
            {
                pen = Settings.Pens.GroupSelectionBorder;
                brush = Settings.Brushes.GroupSelection;
            }
            else
            {
                pen = Settings.Pens.SelectionBorder;
                brush = Settings.Brushes.Selection;
            }

            var halfManipulationPointSize = (float)System.Math.Ceiling(ManipulationPointSize / 2f);
            float angle = Angle;

            // Рисуем точки масштабирования.
            foreach (var pt in ManipulationPoints)
            {
                Matrix mx = gfx.Transform;

                gfx.TranslateTransform(pt.Coords.X, pt.Coords.Y);
                gfx.RotateTransform(angle);
                gfx.TranslateTransform(-pt.Coords.X, -pt.Coords.Y);

                gfx.FillRectangle(
                    brush,
                    (int) (pt.Coords.X - halfManipulationPointSize),
                    (int) (pt.Coords.Y - halfManipulationPointSize),
                    (int) ManipulationPointSize,
                    (int) ManipulationPointSize);

                gfx.DrawRectangle(
                    pen,
                    (int) (pt.Coords.X - halfManipulationPointSize),
                    (int) (pt.Coords.Y - halfManipulationPointSize),
                    (int) ManipulationPointSize,
                    (int) ManipulationPointSize);

                gfx.Transform = mx;
            }

            // Рисуем точку вращения.
            if (CanRotate)
            {
                var pts = RotationPoints;
                float halfRotationPointSize = RotationPointSize / 2f;

                gfx.DrawEllipse(
                    pen,
                    pts[3].X - halfRotationPointSize, pts[3].Y - halfRotationPointSize,
                    RotationPointSize, RotationPointSize);

                gfx.FillEllipse(
                    brush,
                    pts[3].X - halfRotationPointSize, pts[3].Y - halfRotationPointSize,
                    RotationPointSize, RotationPointSize);

                using (var dashPen = (Pen) pen.Clone())
                {
                    dashPen.DashPattern = new[] {3f, 3f};
                    gfx.DrawLine(
                        dashPen,
                        pts[1].X, pts[1].Y,
                        pts[2].X, pts[2].Y);
                }
            }
        }

        /// <summary>
        /// Отрисовать точки связи.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawLinkPoints(Graphics gfx)
        {
            if (Core.Instance.CurrGlyphTypeSelected &&
                Core.Instance.CurrentGlyphType.IsSubclassOf(typeof(LinkGlyph)))
            {
                float halfLinkPointSize = LinkPointSize / 2f;

                foreach (var pt in LinkPoints)
                {
                    gfx.FillPie(
                        Settings.Brushes.Selection,
                        pt.Coords.X - halfLinkPointSize,
                        pt.Coords.Y - halfLinkPointSize,
                        LinkPointSize,
                        LinkPointSize,
                        0f, 360f);

                    gfx.DrawPie(
                        Settings.Pens.SelectionBorder,
                        pt.Coords.X - halfLinkPointSize,
                        pt.Coords.Y - halfLinkPointSize,
                        LinkPointSize,
                        LinkPointSize,
                        0f, 360f);
                }
            }
        }

        /// <summary>
        /// Отрисовать вспомогательные элементы.
        /// </summary>
        protected override void DrawHelpers(Graphics gfx)
        {
            Color color = InGroup
                              ? Settings.Colors.GroupSelectionBorder
                              : Settings.Colors.SelectionBorder;

            using (Brush brush = new SolidBrush(color))
                if (Settings.Environment.SizeLocationLabel && Direction != MoveDirection.None)
                    if (Direction == MoveDirection.Move)
                    {
                        using (var font = "Arial".SafeCreateFont(7))
                            gfx.DrawString(
                                string.Format("[x:{0}{2}, y:{1}{2}]",
                                              XInMM.MMToCurr().ToString("F1"),
                                              YInMM.MMToCurr().ToString("F1"),
                                              Core.Instance.CurrentTranslator[
                                                  Settings.Environment.CurrentUnits.ShortName]),
                                font,
                                brush,
                                ScaledX - (ScaledWidth >> 1) - ManipulationPointSize,
                                ScaledY + (ScaledHeight >> 1) + ManipulationPointSize);
                    }
                    else
                    {
                        using (var font = "Arial".SafeCreateFont(7))
                            gfx.DrawString(
                                string.Format("[{0}:{1}{4}, {2}:{3}{4}]",
                                              Core.Instance.CurrentTranslator["width"],
                                              WidthInMM.MMToCurr().ToString("F1"),
                                              Core.Instance.CurrentTranslator["height"],
                                              HeightInMM.MMToCurr().ToString("F1"),
                                              Core.Instance.CurrentTranslator[
                                                  Settings.Environment.CurrentUnits.ShortName]),
                                font,
                                brush,
                                ScaledX - (ScaledWidth >> 1) - ManipulationPointSize,
                                ScaledY + (ScaledHeight >> 1) + ManipulationPointSize);
                    }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Попадает ли глиф в регион.
        /// </summary>
        /// <param name="rect">Регион в пикселях.</param>
        /// <returns>Попадает ли глиф в регион.</returns>
        public bool InRegion(Rectangle rect)
        {
            int ix = ScaledX - (ScaledWidth >> 1);
            int iy = ScaledY - (ScaledHeight >> 1);

            if (ix + ScaledWidth < rect.X) return false;
            if (ix > rect.X + rect.Width) return false;

            if (iy + ScaledHeight < rect.Y) return false;
            if (iy > rect.Y + rect.Height) return false;

            return true;
        }
        #endregion

        #region ISize
        /// <summary>
        /// Ширина в пикселях.
        /// </summary>
        public int Width
        {
            get { return WidthInMM.MMToPx(); }
            set { WidthInMM = value.PxToMM(); }
        }

        /// <summary>
        /// Высота в пикселях.
        /// </summary>
        public int Height
        {
            get { return HeightInMM.MMToPx(); }
            set { HeightInMM = value.PxToMM(); }
        }

        /// <summary>
        /// Ширина в миллиметрах.
        /// </summary>
        [Description(@"Width in mm.")]
        [Category(@"Layout")]
        [DisplayName(@"Width")]
        [ActiveProperty]
        public float WidthInMM
        {
            get { return _width; }

            set
            {
                if (value < MinimalWidthInMM)
                    value = MinimalWidthInMM;

                if (value <= 0)
                    throw new InvalidValueException(@"Sheet.Width", value.ToString());

                _width = value;
            }
        }

        /// <summary>
        /// Высота в миллиметрах.
        /// </summary>
        [Description(@"Height in mm.")]
        [Category(@"Layout")]
        [DisplayName(@"Height")]
        [ActiveProperty]
        public float HeightInMM
        {
            get { return _height; }

            set
            {
                if (value < MinimalHeightInMM)
                    value = MinimalHeightInMM;

                if (value <= 0)
                    throw new InvalidValueException(@"Sheet.Height", value.ToString());

                _height = value;
            }
        }

        /// <summary>
        /// Ширина в дюймах.
        /// </summary>
        public float WidthInInches
        {
            get { return WidthInMM.MMToInch(); }
            set { WidthInMM = value.InchToMM(); }
        }

        /// <summary>
        /// Высота в дюймах.
        /// </summary>
        public float HeightInInches
        {
            get { return HeightInMM.MMToInch(); }
            set { HeightInMM = value.InchToMM(); }
        }

        /// <summary>
        /// Ширина в текущих единицах.
        /// </summary>
        public float WidthInCurr
        {
            get { return WidthInMM.MMToCurr(); }
            set { WidthInMM = value.CurrToMM(); }
        }

        /// <summary>
        /// Высота в текущих единицах.
        /// </summary>
        public float HeightInCurr
        {
            get { return HeightInMM.MMToCurr(); }
            set { HeightInMM = value.CurrToMM(); }
        }
        #endregion

        #region IScaledSize
        /// <summary>
        /// Масштабированная ширина в пикселях.
        /// </summary>
        public int ScaledWidth
        {
            get { return Width.Scale(); }
            set { Width = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная высота в пикселях.
        /// </summary>
        public int ScaledHeight
        {
            get { return Height.Scale(); }
            set { Height = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная ширина в миллиметрах.
        /// </summary>
        public float ScaledWidthInMM
        {
            get { return WidthInMM.Scale(); }
            set { WidthInMM = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная высота в миллиметрах.
        /// </summary>
        public float ScaledHeightInMM
        {
            get { return HeightInMM.Scale(); }
            set { HeightInMM = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная ширина в дюймах.
        /// </summary>
        public float ScaledWidthInInches
        {
            get { return WidthInInches.Scale(); }
            set { WidthInInches = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная высота в дюймах.
        /// </summary>
        public float ScaledHeightInInches
        {
            get { return HeightInInches.Scale(); }
            set { HeightInInches = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная ширина в текущих единицах.
        /// </summary>
        public float ScaledWidthInCurr
        {
            get { return WidthInCurr.Scale(); }
            set { WidthInCurr = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная высота в текущих единицах.
        /// </summary>
        public float ScaledHeightInCurr
        {
            get { return HeightInCurr.Scale(); }
            set { HeightInCurr = value.Unscale(); }
        }
        #endregion

        #region ILocation
        /// <summary>
        /// Координата X в пикселях.
        /// </summary>
        public int X
        {
            get { return XInMM.MMToPx(); }
            set { XInMM = value.PxToMM(); }
        }

        /// <summary>
        /// Координата Y в пикселях.
        /// </summary>
        public int Y
        {
            get { return YInMM.MMToPx(); }
            set { YInMM = value.PxToMM(); }
        }

        /// <summary>
        /// Координата X в миллиметрах.
        /// </summary>
        [Description(@"X coordinate in mm.")]
        [Category(@"Layout")]
        [DisplayName(@"X")]
        [ActiveProperty]
        public float XInMM { get; set; }

        /// <summary>
        /// Координата Y в миллиметрах.
        /// </summary>
        [Description(@"Y coordinate in mm.")]
        [Category(@"Layout")]
        [DisplayName(@"Y")]
        [ActiveProperty]
        public float YInMM { get; set; }

        /// <summary>
        /// Координата X в дюймах.
        /// </summary>
        public float XInInches
        {
            get { return XInMM.MMToInch(); }
            set { XInMM = value.InchToMM(); }
        }

        /// <summary>
        /// Координата Y в дюймах.
        /// </summary>
        public float YInInches
        {
            get { return YInMM.MMToInch(); }
            set { YInMM = value.InchToMM(); }
        }

        /// <summary>
        /// Координата X в текущих единицах.
        /// </summary>
        public float XInCurr
        {
            get { return XInMM.MMToCurr(); }
            set { XInMM = value.CurrToMM(); }
        }

        /// <summary>
        /// Координата Y в текущих единицах.
        /// </summary>
        public float YInCurr
        {
            get { return YInMM.MMToCurr(); }
            set { YInMM = value.CurrToMM(); }
        }
        #endregion

        #region IScaledLocation
        /// <summary>
        /// Масштабированная координата X в пикселях.
        /// </summary>
        public int ScaledX
        {
            get { return X.Scale(); }
            set { X = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная координата Y в пикселях.
        /// </summary>
        public int ScaledY
        {
            get { return Y.Scale(); }
            set { Y = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная координата X в миллиметрах.
        /// </summary>
        public float ScaledXInMM
        {
            get { return XInMM.Scale(); }
            set { XInMM = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная координата Y в миллиметрах.
        /// </summary>
        public float ScaledYInMM
        {
            get { return YInMM.Scale(); }
            set { YInMM = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная координата X в дюймах.
        /// </summary>
        public float ScaledXInInches
        {
            get { return XInInches.Scale(); }
            set { XInInches = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная координата Y в дюймах.
        /// </summary>
        public float ScaledYInInches
        {
            get { return YInInches.Scale(); }
            set { YInInches = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная координата X в текущих единицах.
        /// </summary>
        public float ScaledXInCurr
        {
            get { return XInCurr.Scale(); }
            set { XInCurr = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная координата Y в текущих единицах.
        /// </summary>
        public float ScaledYInCurr
        {
            get { return YInCurr.Scale(); }
            set { YInCurr = value.Unscale(); }
        }
        #endregion

        #region Rotation
        /// <summary>
        /// Угол вращения.
        /// </summary>
        [Description(@"Angle.")]
        [Category(@"Layout")]
        [DisplayName(@"Angle")]
        [ActiveProperty]
        public float Angle
        {
            get
            {
                if (System.Math.Abs(_angle) > 360)
                    _angle = (float)(_angle - System.Math.Truncate(_angle / 360) * 360);

                return _angle;
            }

            set
            {
                _angle = value;

                if (System.Math.Abs(_angle) > 360)
                    _angle = (float)(_angle - System.Math.Truncate(_angle / 360) * 360);
            }
        }

        /// <summary>
        /// Можно ли вращать глиф.
        /// </summary>
        public bool CanRotate
        {
            get
            {
                // TODO
                return true;
            }
        }
        #endregion
        // ReSharper restore InconsistentNaming
    }
}