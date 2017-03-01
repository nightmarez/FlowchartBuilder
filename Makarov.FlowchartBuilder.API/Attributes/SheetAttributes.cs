// <copyright file="SheetAttributes.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-II-18</date>
// <summary>Атрибуты листа.</summary>

using System;

namespace Makarov.FlowchartBuilder.API.Attributes
{
    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Аттрибут листа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class SheetAttribute : Attribute
    { }

    /// <summary>
    /// Ширина листа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SheetWidthAttribute : SheetAttribute
    {
        private readonly float _widthInMM;

        /// <param name="width">Ширина в миллиметрах.</param>
        public SheetWidthAttribute(float width)
        {
            _widthInMM = width;
        }

        /// <summary>
        /// Ширина в миллиметрах.
        /// </summary>
        public float WidthInMM
        {
            get { return _widthInMM; }
        }
    }

    /// <summary>
    /// Высота листа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SheetHeightAttribute : SheetAttribute
    {
        private readonly float _heightInMM;

        /// <param name="width">Ширина в миллиметрах.</param>
        public SheetHeightAttribute(float width)
        {
            _heightInMM = width;
        }

        /// <summary>
        /// Ширина в миллиметрах.
        /// </summary>
        public float HeightInMM
        {
            get { return _heightInMM; }
        }
    }

    /// <summary>
    /// Имя листа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SheetNameAttribute : SheetAttribute
    {
        private readonly string _name;

        /// <param name="name">Имя.</param>
        public SheetNameAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
    }

    /// <summary>
    /// Семейство листа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SheetFamilyAttribute : SheetAttribute
    {
        private readonly string _family;

        /// <param name="family">Семейство.</param>
        public SheetFamilyAttribute(string family)
        {
            _family = family;
        }

        /// <summary>
        /// Семейство.
        /// </summary>
        public string Family
        {
            get { return _family; }
        }
    }

    // ReSharper restore InconsistentNaming
}