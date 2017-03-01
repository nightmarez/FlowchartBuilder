// <copyright file="Units.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-05</date>
// <summary>Единицы измерения.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Единицы измерения.
    /// </summary>
    public sealed class Units
    {
        #region Exceptions
        /// <summary>
        /// Неправильное значение в единицах измерения.
        /// </summary>
        public class UnitsInvalidValueException : InvalidValueException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="entity">Сущность с неправильным начением.</param>
            /// <param name="entityValue">Значение.</param>
            public UnitsInvalidValueException(string entity, string entityValue)
                : base(string.Format("Units.{0}", entity), entityValue)
            { }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Имя единиц измерения.</param>
        /// <param name="ShortName">Коротное имя (аббревиатура).</param>
        /// <param name="InMM">Количество данных единиц в миллиметре.</param>
        /// <param name="BigStep">Большой шаг.</param>
        /// <param name="SmallStep">Маленький шаг.</param>
        public Units(string Name, string ShortName, float InMM, int BigStep, int SmallStep)
        {
            if (string.IsNullOrEmpty(Name)) throw new UnitsInvalidValueException("Name", "null");
            this.Name = Name;

            if (string.IsNullOrEmpty(ShortName)) throw new UnitsInvalidValueException("ShortName", "null");
            this.ShortName = ShortName;

            if (InMM <= 0) throw new UnitsInvalidValueException("InMM", InMM.ToString());
            this.InMM = InMM;

            if (BigStep <= 0) throw new UnitsInvalidValueException("BigStep", BigStep.ToString());
            this.BigStep = BigStep;

            if (SmallStep <= 0) throw new UnitsInvalidValueException("SmallStep", SmallStep.ToString());
            this.SmallStep = SmallStep;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Имя единиц измерения.
        /// </summary>
        public string Name
        {
            get; private set;
        }

        /// <summary>
        /// Коротное имя (аббревиатура).
        /// </summary>
        public string ShortName
        {
            get; private set;
        }

        /// <summary>
        /// Количество данных единиц в миллиметре.
        /// </summary>
        public float InMM
        {
            get; private set;
        }

        /// <summary>
        /// Количество миллиментов в данном юните.
        /// </summary>
        public float MMInThis
        {
            get { return 1f / InMM; }
        }

        /// <summary>
        /// Большой шаг.
        /// </summary>
        public int BigStep
        {
            get; private set;
        }

        /// <summary>
        /// Маленький шаг.
        /// </summary>
        public int SmallStep
        {
            get; private set;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Возвращает строковое представление объекта.
        /// </summary>
        /// <returns>Строковое представление объекта.</returns>
        public override string ToString()
        {
            return string.Format("Units '{0}' (short name:'{1}', InMM:{2}, big step: {3}, small step: {4})",
                                 Name, ShortName, InMM, BigStep, SmallStep);
        }

        /// <summary>
        /// Перевести данные единицы в миллиметры.
        /// </summary>
        /// <param name="units">Единицы.</param>
        /// <returns>Миллиметры.</returns>
        public float ToMM(float units)
        {
            return units / InMM;
        }

        /// <summary>
        /// Перевести миллиметры в данные единицы.
        /// </summary>
        /// <param name="mm">Миллиметры.</param>
        /// <returns>Единицы.</returns>
        public float MMToThis(float mm)
        {
            return mm / MMInThis;
        }
        #endregion

        #region Operators
        public static implicit operator float(Units units)
        {
            return units.MMInThis;
        }

        public static implicit operator string(Units units)
        {
            return units.ToString();
        }
        #endregion
    }
}
