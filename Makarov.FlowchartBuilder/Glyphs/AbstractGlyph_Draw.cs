// <copyright file="AbstractGlyph_Serialization.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-12-11</date>
// <summary>Абстрактный глиф.</summary>

using System.Drawing;
using System.Drawing.Text;
using Makarov.Framework.Graphics;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    public abstract partial class AbstractGlyph
    {
        /// <summary>
        /// Отрисовать глиф.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        public void Draw(Graphics gfx)
        {
            // Событие - начало перерисовки глифа.
            if (BeginRedraw != null)
                BeginRedraw(this, gfx);

            using (new TempSmoothingMode(gfx, SmoothMode))
            {
                bool rotated = false;

                if (this is BlockGlyph)
                {
                    var blockGlyph = (BlockGlyph)this;

                    if (blockGlyph.CanRotate && blockGlyph.Angle != 0)
                    {
                        rotated = true;
                        gfx.TranslateTransform(blockGlyph.ScaledX, blockGlyph.ScaledY);
                        gfx.RotateTransform(blockGlyph.Angle);
                        gfx.TranslateTransform(-blockGlyph.ScaledX, -blockGlyph.ScaledY);
                    }
                }

                // Рисуем глиф.
                if (Selected)
                    DrawSelectedGlyph(gfx);
                else
                    DrawGlyph(gfx);

                // Если было вращение, убираем его.
                if (rotated)
                    gfx.ResetTransform();
            }

            using (new TempTextRenderingHint(gfx, Settings.Environment.Antialiasing
                                                      ? TextRenderingHint.ClearTypeGridFit
                                                      : TextRenderingHint.SystemDefault))
            {
                // Рисуем текст.
                DrawText(gfx);
            }

            // Если глиф выбран...
            if (Selected)
            {
                using (new TempSmoothingMode(gfx, SmoothMode))
                {
                    // Рисуем точки изменения размеров.
                    DrawDots(gfx);
                }

                // Рисуем подсказки.
                DrawHelpers(gfx);
            }
            else
            {
                // Рисуем точки соединения.
                DrawLinkPoints(gfx);
            }

            // Если глиф залочен, рисуем замок.
            if (Fixed && this is BlockGlyph)
            {
                Bitmap lockImg = Core.Instance.Images["Lock"];
                const int offset = 2;

                var blockGlyph = (BlockGlyph) this;
                gfx.DrawImageUnscaled(
                    lockImg,
                    blockGlyph.ScaledX + (blockGlyph.ScaledWidth >> 1) - lockImg.Width - offset,
                    blockGlyph.ScaledY - (blockGlyph.ScaledWidth >> 1) + offset);
            }

            // Событие - конец перерисовки глифа.
            if (EndRedraw != null)
                EndRedraw(this, gfx);
        }

        /// <summary>
        /// Отрисовать картинку глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected virtual void DrawGlyph(Graphics gfx) { }

        /// <summary>
        /// Отрисовать картинку выделенного глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        /// <remarks>Этот метод нужно перегружать, если выделенный глиф должен
        /// выглядеть как-нибуть по-другому.</remarks>
        protected virtual void DrawSelectedGlyph(Graphics gfx)
        {
            DrawGlyph(gfx);
        }

        /// <summary>
        /// Отрисовать заголовок.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected virtual void DrawText(Graphics gfx) { }

        /// <summary>
        /// Отрисовать точки манипулирования.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected virtual void DrawDots(Graphics gfx) { }

        /// <summary>
        /// Отрисовать точки связи.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected virtual void DrawLinkPoints(Graphics gfx) { }

        /// <summary>
        /// Отрисовать вспомогательные элементы.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected virtual void DrawHelpers(Graphics gfx) { }
    }
}