// <copyright file="History.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-XII-16</date>
// <summary>История.</summary>


using System.Collections.Generic;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// История.
    /// </summary>
    public sealed class History
    {
        #region Exceptions
        /// <summary>
        /// Исключение истории.
        /// </summary>
        public abstract class HistoryException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            protected HistoryException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Текущей ноды истории не существует.
        /// </summary>
        public sealed class CurrentHistoryNodeNotExistsException : HistoryException
        {
            public CurrentHistoryNodeNotExistsException()
                : base(@"Current history node not exists.")
            { }
        }

        /// <summary>
        /// Следующей ноды истории не существует.
        /// </summary>
        public sealed class NextHistoryNodeNotExistsException : HistoryException
        {
            public NextHistoryNodeNotExistsException()
                : base(@"Next history node not exists.")
            { }
        }

        /// <summary>
        /// Предыдущей ноды истории не существует.
        /// </summary>
        public sealed class PrevHistoryNodeNotExistsException : HistoryException
        {
            public PrevHistoryNodeNotExistsException()
                : base(@"Previous history node not exists.")
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Текущая нода истории.
        /// </summary>
        private HistoryNode _node;

        #endregion

        #region Constructors
        /// <param name="historyEvents">События истории.</param>
        public History(IEnumerable<HistoryEvent> historyEvents)
        {
            _node = new HistoryNode(historyEvents);
        }

        /// <param name="historyEvent">Событие истории.</param>
        public History(HistoryEvent historyEvent)
        {
            _node = new HistoryNode(historyEvent);
        }

        public History()
        {
            _node = null;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Существует ли текущая нода истории.
        /// </summary>
        public bool IsCurrentExists
        {
            get { return _node != null; }
        }

        /// <summary>
        /// Текущая нода истории.
        /// </summary>
        public HistoryNode Current
        {
            get
            {
                if (!IsCurrentExists)
                    throw new CurrentHistoryNodeNotExistsException();

                return _node;
            }
        }

        /// <summary>
        /// Существует ли предыдущая нода.
        /// </summary>
        public bool PrevExists
        {
            get { return IsCurrentExists && Current.IsPrevNodeExists; }
        }

        /// <summary>
        /// Существует ли следующая нода.
        /// </summary>
        public bool NextExists
        {
            get { return IsCurrentExists && Current.IsNextNodeExists; }
        }

        /// <summary>
        /// Перейти на следующую ноду.
        /// </summary>
        public void Redo()
        {
            if (!NextExists)
                throw new NextHistoryNodeNotExistsException();

            _node = _node.NextNode;
        }

        /// <summary>
        /// Перейти на предыдущую ноду.
        /// </summary>
        public void Undo()
        {
            if (!PrevExists)
                throw new PrevHistoryNodeNotExistsException();

            _node = _node.PrevNode;
        }
        #endregion

        #region Public methods
        /// <param name="historyEvents">События истории.</param>
        public void Add(IEnumerable<HistoryEvent> historyEvents)
        {
            if (_node == null)
                _node = new HistoryNode(historyEvents);
            else
            {
                var tmp = new HistoryNode(historyEvents) {PrevNode = _node};
                _node.NextNode = tmp;
                _node = tmp;
            }
        }

        /// <param name="historyEvent">Событие истории.</param>
        public void Add(HistoryEvent historyEvent)
        {
            Add(new List<HistoryEvent> {historyEvent});
        }
        #endregion
    }
}
