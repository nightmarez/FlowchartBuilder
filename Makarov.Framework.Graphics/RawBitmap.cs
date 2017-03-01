// <copyright file="RawBitmap.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-28</date>
// <summary>Класс для работы с битмапом.</summary>

using System;
using System.Drawing;
using System.Drawing.Imaging;
using Makarov.Framework.Core;

namespace Makarov.Framework.Graphics
{
    /// <summary>
    /// Класс для работы с битмапом.
    /// </summary>
    public unsafe sealed class RawBitmap : IDisposable
    {
        #region Pixel data
        /// <summary>
        /// Пиксель.
        /// </summary>
        public struct Pixel
        {
            /// <summary>
            /// Синий цвет.
            /// </summary>
            public byte Blue;

            /// <summary>
            /// Зелёный цвет.
            /// </summary>
            public byte Green;

            /// <summary>
            /// Красный цвет.
            /// </summary>
            public byte Red;

            /// <summary>
            /// Альфа-канал.
            /// </summary>
            public byte Alpha;

            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="red">Красный цвет.</param>
            /// <param name="green">Зелёный цвет.</param>
            /// <param name="blue">Синий цвет.</param>
            /// <param name="alpha">Альфа-канал.</param>
            public Pixel(byte red, byte green, byte blue, byte alpha)
            {
                Red = red;
                Green = green;
                Blue = blue;
                Alpha = alpha;
            }

            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="red">Красный цвет.</param>
            /// <param name="green">Зелёный цвет.</param>
            /// <param name="blue">Синий цвет.</param>
            public Pixel(byte red, byte green, byte blue)
            {
                Red = red;
                Green = green;
                Blue = blue;
                Alpha = 255;
            }
        }
        #endregion

        #region Exceptions
        /// <summary>
        /// Исключение.
        /// </summary>
        public class RawBitmapException : MakarovFrameworkException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            public RawBitmapException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Неправильный формат пикселей.
        /// </summary>
        public sealed class InvalidPixelFormatException : RawBitmapException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            public InvalidPixelFormatException()
                : base("Invalid pixel format.")
            { }
        }
        #endregion

        #region Private members
        private readonly BitmapData _data;
        readonly byte* _pBase = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bmp">Битмап.</param>
        public RawBitmap(Bitmap bmp)
        {
            if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
                throw new InvalidPixelFormatException();

            InnerBitmap = bmp;
            Width = bmp.Width;
            Height = bmp.Height;

            _data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height), 
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            _pBase = (byte*)_data.Scan0;
        }

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        public void Dispose()
        {
            InnerBitmap.UnlockBits(_data);
            InnerBitmap = null;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Ширина.
        /// </summary>
        public int Width
        {
            get; private set;
        }

        /// <summary>
        /// Высота.
        /// </summary>
        public int Height
        {
            get; private set;
        }

        /// <summary>
        /// Внутренний битмап.
        /// </summary>
        public Bitmap InnerBitmap
        {
            get; private set;
        }
        #endregion

        #region Random access
        /// <summary>
        /// Доступ к пикселям.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Цвет.</returns>
        public Color this[int x, int y]
        {
            get
            {
                var pixel = (Pixel*)(_pBase + y * _data.Stride + x * 4);

                return Color.FromArgb(
                    (*pixel).Alpha,
                    (*pixel).Red,
                    (*pixel).Green,
                    (*pixel).Blue);
            }

            set
            {
                var pixel = (Pixel*)(_pBase + y * _data.Stride + x * 4);
                *pixel = new Pixel(value.R, value.G, value.B, value.A);
            }
        }
        #endregion
    }
}