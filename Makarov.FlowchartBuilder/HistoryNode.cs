// <copyright file="HistoryNode.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-16</date>
// <summary>Нода истории.</summary>

using System.Collections.Generic;
using System.Linq;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Нода истории.
    /// </summary>
    public sealed class HistoryNode
    {
        #region Exceptions
        /// <summary>
        /// Исключение ноды истории.
        /// </summary>
        public abstract class HistoryNodeException : History.HistoryException
        {
            /// <param name="message">Сообщение.</param>
            protected HistoryNodeException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Следующая нода не существует.
        /// </summary>
        public sealed class NextNodeNotExistsException : HistoryNodeException
        {
            public NextNodeNotExistsException()
                : base(@"Next node not exists.")
            { }
        }

        /// <summary>
        /// Предыдущая нода не существует.
        /// </summary>
        public sealed class PrevNodeNotExistsException : HistoryNodeException
        {
            public PrevNodeNotExistsException()
                : base(@"Previous node not exist.")
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Предыдущая нода.
        /// </summary>
        private HistoryNode _prevNode;

        /// <summary>
        /// Следующая нода.
        /// </summary>
        private HistoryNode _nextNode;

        /// <summary>
        /// События истории.
        /// </summary>
        private readonly List<HistoryEvent> _events;
        #endregion

        #region Properties
        /// <summary>
        /// Есть ли предыдущая нода.
        /// </summary>
        public bool IsPrevNodeExists
        {
            get { return _prevNode != null; }
        }

        /// <summary>
        /// Есть ли следующая нода.
        /// </summary>
        public bool IsNextNodeExists
        {
            get { return _nextNode != null; }
        }

        /// <summary>
        /// Следующая нода.
        /// </summary>
        public HistoryNode NextNode
        {
            get
            {
                if (_nextNode == null)
                    throw new NextNodeNotExistsException();

                return _nextNode;
            }

            set { _nextNode = value; }
        }

        /// <summary>
        /// Предыдущая нода.
        /// </summary>
        public HistoryNode PrevNode
        {
            get
            {
                if (_prevNode == null)
                    throw new PrevNodeNotExistsException();

                return _prevNode;
            }

            set { _prevNode = value; }
        }

        /// <summary>
        /// События истории.
        /// </summary>
        public IEnumerable<HistoryEvent> Events
        {
            get
            {
                // Сначала возвращаем события по созданию глифов...
                foreach (HistoryEvent @event in _events)
                    if (@event is HistoryEventCreateGlyph)
                        yield return @event;

                // ...потом события по изменению глифов...
                foreach (HistoryEvent @event in _events)
                    if (@event is HistoryEventChangeGlyph)
                        yield return @event;

                // ...потом события по удалению глифов...
                foreach (HistoryEvent @event in _events)
                    if (@event is HistoryEventDeleteGlyph)
                        yield return @event;

                // ...и в конце те, которые остались (если таковые есть вообще).
                foreach (HistoryEvent @event in _events)
                    if (!(@event is HistoryEventCreateGlyph ||
                          @event is HistoryEventChangeGlyph ||
                          @event is HistoryEventDeleteGlyph))
                        yield return @event;
            }
        }

        /// <summary>
        /// События истории в обратном порядке.
        /// </summary>
        public IEnumerable<HistoryEvent> EventsReverse
        {
            get { return Events.Reverse(); }
        }

        /// <summary>
        /// Количество событий истории.
        /// </summary>
        public int EventsCount
        {
            get { return _events.Count; }
        }
        #endregion

        #region Constructors
        /// <param name="historyEvents">События истории.</param>
        public HistoryNode(IEnumerable<HistoryEvent> historyEvents)
        {
            _events = new List<HistoryEvent>(historyEvents);
        }

        /// <param name="historyEvent">Событие истории.</param>
        public HistoryNode(HistoryEvent historyEvent)
        {
            _events = new List<HistoryEvent> {historyEvent};
        }
        #endregion
    }
}