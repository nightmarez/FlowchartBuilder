// <copyright file="StringExtensions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-28</date>
// <summary>Расширитель класса строк.</summary>

using System.Drawing;
using Makarov.Framework.Core;
using Makarov.Framework.Graphics;

namespace Makarov.FlowchartBuilder.Extensions
{
    /// <summary>
    /// Расширитель класса строк.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Возвращает размер области, необходимой для вывода текста.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        /// <param name="font">Шрифт.</param>
        /// <param name="str">Строка.</param>
        /// <returns>Размер области, необходимой для вывода текста.</returns>
        public static Size CalcSize(this string str, Graphics gfx, Font font)
        {
            var fSize = gfx.MeasureString(str, font);
            var startSize = new Size((int)fSize.Width * 2, (int)fSize.Height * 2);
            using (var bmp = new Bitmap(startSize.Width, startSize.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Black);

                    var pt = new PointF(
                        (bmp.Width - fSize.Width) / 2f,
                        (bmp.Height - fSize.Height) / 2f);
                    g.DrawString(str, font, Brushes.White, pt);
                }

                int left = 0;
                int top = 0;
                int right = bmp.Width;
                int bottom = bmp.Height;

                using (var raw = new RawBitmap(bmp))
                {
                    for (int i = 0; i < raw.Width; i++)
                    {
                        for (int j = 0; j < raw.Height; j++)
                        {
                            if (raw[i, j].R != 0)
                            {
                                left = i;
                                goto label0;
                            }
                        }
                    }

                    label0:
                    for (int i = raw.Width - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < raw.Height; j++)
                        {
                            if (raw[i, j].R != 0)
                            {
                                right = i;
                                goto label1;
                            }
                        }
                    }

                    label1:
                    for (int j = 0; j < raw.Height; j++)
                    {
                        for (int i = 0; i < raw.Width; i++)
                        {
                            if (raw[i, j].R != 0)
                            {
                                top = j;
                                goto label2;
                            }
                        }
                    }

                    label2:
                    for (int j = raw.Height - 1; j >= 0; j--)
                    {
                        for (int i = 0; i < raw.Width; i++)
                        {
                            if (raw[i, j].R != 0)
                            {
                                bottom = j;
                                goto label3;
                            }
                        }
                    }

                    label3:
                    int width = right - left;
                    int height = bottom - top;

                    if (width < 0) width = 0;
                    if (height < 0) height = 0;

                    return new Size(width, height);
                }
            }
        }

        /// <summary>
        /// Безопасное создание шрифтов.
        /// </summary>
        /// <param name="familyName">Имя семейства шрифтов.</param>
        /// <param name="emSize">Размер.</param>
        /// <returns>Шрифт.</returns>
        public static Font SafeCreateFont(this string familyName, float emSize)
        {
            return Fonts.SafeCreateFont(familyName, emSize);
        }
    }
}