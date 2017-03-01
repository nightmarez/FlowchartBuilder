// <copyright file="UnitsManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-16</date>
// <summary>Менеджер единиц измерения (синглетон).</summary>

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using attrs = Makarov.FlowchartBuilder.Settings.Xml.Attributes;
using tags = Makarov.FlowchartBuilder.Settings.Xml.Tags;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Менеджер единиц измерения (синглетон).
    /// </summary>
    public sealed class UnitsManager
    {
        #region Exceptions
        /// <summary>
        /// Исключение менеджера единиц измерения.
        /// </summary>
        public class UnitsManagerException : FlowchartBuilderException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            public UnitsManagerException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Менеджер единиц измерения уже существует.
        /// </summary>
        public sealed class UnitManagerAlreadyExistsException : SingletonObjectAlreayExistsException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            public UnitManagerAlreadyExistsException()
                : base("UnitsManagerException")
            { }
        }

        /// <summary>
        /// Единицы измерения с данным именем не найдены.
        /// </summary>
        public sealed class UnitsNotFoundException : UnitsManagerException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="name">Имя.</param>
            public UnitsNotFoundException(string name)
                : base(string.Format("Units '{0}' not found.", name))
            { }
        }

        /// <summary>
        /// Единицы измерения с данными именем уже существуют.
        /// </summary>
        public sealed class UnitsAlreadyExistsException : UnitsManagerException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="name">Имя.</param>
            public UnitsAlreadyExistsException(string name)
                : base(string.Format("Units '{0}' already exists.", name))
            { }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        public UnitsManager()
        {
            var doc = new XmlDocument();
            doc.Load(Settings.Files.UnitsDefinition);
            doc.Schemas.Add(Settings.Xml.Namespaces.Base, Settings.Files.UnitsSchema);
            doc.Validate(delegate(object sender, ValidationEventArgs args) { throw args.Exception; });
            var mainNode = doc.GetElementsByTagName(Settings.Xml.Tags.UnitsTypes)[0];

            var nfi = new NumberFormatInfo { NumberDecimalSeparator = "." };
            const NumberStyles nstyle = NumberStyles.Float;

            foreach (XmlNode node in mainNode)
            {
                string name = node.Attributes[Settings.Xml.Attributes.Name].Value;
                string shortName = node.Attributes[Settings.Xml.Attributes.ShortName].Value;
                float InMM = float.Parse(node.Attributes[Settings.Xml.Attributes.InMM].Value, nstyle, nfi);
                int BigStep = int.Parse(node.Attributes[Settings.Xml.Attributes.BigStep].Value, nstyle, nfi);
                int SmallStep = int.Parse(node.Attributes[Settings.Xml.Attributes.SmallStep].Value, nstyle, nfi);

                AddUnits(new Units(
                    name,
                    shortName,
                    InMM,
                    BigStep,
                    SmallStep));
            }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Список единиц измерения.
        /// </summary>
        private readonly Dictionary<string, Units> _units =
            new Dictionary<string, Units>();
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает единицы измерения по имени.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <returns>Единицы измерения.</returns>
        public Units this[string name]
        {
            get { return GetUnits(name); }
        }

        /// <summary>
        /// Количество известных единиц измерения.
        /// </summary>
        public int Count
        {
            get { return _units.Count; }
        }

        /// <summary>
        /// Доступные единицы измерения.
        /// </summary>
        public IEnumerable<string> LoadedUnits
        {
            get { return _units.Select(kvp => kvp.Key); }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Возвращает единицы измерения по имени.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <returns>Единицы измерения.</returns>
        public Units GetUnits(string name)
        {
            if (!_units.ContainsKey(name))
                throw new UnitsNotFoundException(name);

            return _units[name];
        }

        /// <summary>
        /// Устанавливает единицы измерения.
        /// </summary>
        /// <param name="units">Единицы измерения.</param>
        public void AddUnits(Units units)
        {
            if (_units.ContainsKey(units.Name))
                throw new UnitsAlreadyExistsException(units.Name);

            _units.Add(units.Name, units);
        }

        /// <summary>
        /// Удалить все единицы измерения.
        /// </summary>
        public void Clear()
        {
            _units.Clear();
        }
        #endregion
    }
}