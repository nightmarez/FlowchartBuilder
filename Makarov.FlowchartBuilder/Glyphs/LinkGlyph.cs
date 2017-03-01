// <copyright file="PointLinkGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-25</date>
// <summary>Связь.</summary>

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Связь между точками соединения.
    /// </summary>
    public abstract class LinkGlyph : AbstractGlyph
    {
        #region Properties
        ///// <summary>
        ///// Координаты первой точки в миллиметрах.
        ///// </summary>
        //public abstract PointF FirstPoint { get; }

        ///// <summary>
        ///// Координаты второй точки в миллиметрах.
        ///// </summary>
        //public abstract PointF SecondPoint { get; }
        #endregion

        #region Public methods
        ///// <summary>
        ///// Отрисовать картинку глифа.
        ///// </summary>
        ///// <param name="gfx">Graphics.</param>
        //protected override void DrawGlyph(Graphics gfx)
        //{
        //    using (var pen = new Pen(Settings.Colors.SelectionBorder))
        //        gfx.DrawLine(pen, FirstPoint, SecondPoint);
        //}
        #endregion
    }
}