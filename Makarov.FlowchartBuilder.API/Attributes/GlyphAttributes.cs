// <copyright file="GlyphAttributes.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-II-18</date>
// <summary>Атрибуты глифа.</summary>

using System;

namespace Makarov.FlowchartBuilder.API.Attributes
{
    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Атрибут глифа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class GlyphAttribute : Attribute
    { }

    /// <summary>
    /// Дефолтная ширина глифа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphDefaultWidthAttribute : GlyphAttribute
    {
        private readonly float _widthInMM;

        /// <param name="width">Ширина в миллиметрах.</param>
        public GlyphDefaultWidthAttribute(float width)
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
    /// Дефолтная высота глифа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphDefaultHeightAttribute : GlyphAttribute
    {
        private readonly float _heightInMM;

        /// <param name="height">Высота в миллиметрах.</param>
        public GlyphDefaultHeightAttribute(float height)
        {
            _heightInMM = height;
        }

        /// <summary>
        /// Высота в миллиметрах.
        /// </summary>
        public float HeightInMM
        {
            get { return _heightInMM; }
        }
    }

    /// <summary>
    /// Минимальная ширина глиф.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphMinimalWidthAttribute : GlyphAttribute
    {
        private readonly float _widthInMM;

        /// <param name="width">Минимальная ширина в миллиметрах.</param>
        public GlyphMinimalWidthAttribute(float width)
        {
            _widthInMM = width;
        }

        /// <summary>
        /// Минимальная ширина в миллиметрах.
        /// </summary>
        public float WidthInMM
        {
            get { return _widthInMM; }
        }
    }

    /// <summary>
    /// Минимальная высота глифа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphMinimalHeightAttribute : GlyphAttribute
    {
        private readonly float _heightInMM;

        /// <param name="height">Минимальная высота в миллиметрах.</param>
        public GlyphMinimalHeightAttribute(float height)
        {
            _heightInMM = height;
        }

        /// <summary>
        /// Минимальная высота в миллиметрах.
        /// </summary>
        public float HeightInMM
        {
            get { return _heightInMM; }
        }
    }

    /// <summary>
    /// Имя глифа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphNameAttribute : GlyphAttribute
    {
        private readonly string _name;

        /// <param name="name">Имя.</param>
        public GlyphNameAttribute(string name)
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
    /// Семейство глифа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphFamilyAttribute : GlyphAttribute
    {
        private readonly string _family;

        /// <param name="family">Семейство.</param>
        public GlyphFamilyAttribute(string family)
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

    /// <summary>
    /// Порядковый номер группы глифов в палитре.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphGroupOrderAttribute : GlyphAttribute
    {
        private readonly int _order;

        /// <param name="order">Порядковый номер группы глифов в палитре.</param>
        public GlyphGroupOrderAttribute(int order)
        {
            _order = order;
        }

        /// <summary>
        /// Порядковый номер группы глифов в палитре.
        /// </summary>
        public int Order
        {
            get { return _order; }
        }
    }

    /// <summary>
    /// Порядковый номер глифа в палитре.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlyphOrderAttribute : GlyphAttribute
    {
        private readonly int _order;

        /// <param name="order">Порядковый номер глифа в палитре.</param>
        public GlyphOrderAttribute(int order)
        {
            _order = order;
        }

        /// <summary>
        /// >Порядковый номер глифа в палитре.
        /// </summary>
        public int Order
        {
            get { return _order; }
        }
    }

    /// <summary>
    /// Атрибут активного свойства глифа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ActivePropertyAttribute : GlyphAttribute
    { }

    /// <summary>
    /// Атрибут глифа, который не может быть непосредственно создан,
    /// но вызывает при попытке своего создания команду.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandGlyphAttribute : GlyphAttribute
    {
        private readonly string _cmdName;

        /// <param name="cmdName">Имя команды.</param>
        public CommandGlyphAttribute(string cmdName)
        {
            _cmdName = cmdName;
        }

        /// <summary>
        /// Имя команды.
        /// </summary>
        public string CommandName
        {
            get { return _cmdName; }
        }
    }

    /// <summary>
    /// Атрибут глифа, позволяющий указать, нужно ли отображать
    /// глиф в палитре глифов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class VisibleGlyphAttribute : GlyphAttribute
    {
        private readonly bool _visible;

        /// <param name="isVisible">Нужно ли отображать глиф в палитре глифов.</param>
        public VisibleGlyphAttribute(bool isVisible)
        {
            _visible = isVisible;
        }

        /// <summary>
        /// Нужно ли отображать глиф в палитре глифов.
        /// </summary>
        public bool IsVisible
        {
            get { return _visible; }
        }
    }

    // ReSharper restore InconsistentNaming
}
