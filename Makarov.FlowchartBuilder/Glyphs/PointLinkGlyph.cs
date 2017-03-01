// <copyright file="PointLinkGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-21</date>
// <summary>Связь между точками соединения.</summary>

using System.Drawing;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Связь между точками соединения.
    /// </summary>
    public class PointLinkGlyph : LinkGlyph
    {
        #region Properties
        /// <summary>
        /// Первая соединительная точка.
        /// </summary>
        public LinkPoint FirstPoint
        {
            get; set;
        }

        /// <summary>
        /// Вторая соединительная точка.
        /// </summary>
        public LinkPoint SecondPoint
        {
            get; set;
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Отрисовать картинку глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawGlyph(Graphics gfx)
        {
            gfx.DrawLine(
                Pens.Black,
                UnitsConverter.MMToPx(FirstPoint.XInMM),
                UnitsConverter.MMToPx(FirstPoint.YInMM),
                UnitsConverter.MMToPx(SecondPoint.XInMM),
                UnitsConverter.MMToPx(SecondPoint.YInMM));
        }
        #endregion
    }
}