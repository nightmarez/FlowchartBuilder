// <copyright file="Document.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-18</date>
// <summary>Документ.</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using Makarov.FlowchartBuilder.Glyphs;
using Makarov.FlowchartBuilder.Sheets;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Документ.
    /// </summary>
    public sealed partial class Document
    {
        #region Constructors
        /// <param name="sheet">Лист.</param>
        public Document(Sheet sheet)
        {
            // Текущий лист.
            DocumentSheet = sheet;

            // Создаём историю и сохраняем в ней глифы листа.
            var historyEvents = new List<HistoryEvent>();

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (AbstractGlyph glyph in sheet.Glyphs)
            {
                var snapshot = new GlyphSnapshot(glyph);
                historyEvents.Add(new HistoryEventCreateGlyph(snapshot));
            }
            // ReSharper restore LoopCanBeConvertedToQuery

            DocumentHistory = new History(historyEvents);

            // Сохраняем снимки глифов.
            SaveSnapshots(GetSnapshotsFromSheet());
        }

        public Document()
        {
            throw new NotImplementedException();
        }

        /// <param name="fileName">Имя файла.</param>
        public Document(string fileName)
        {
            using (new DontRedraw())
                Load(fileName);
        }
        #endregion

        #region Private members
        /// <summary>
        /// Снимки глифов последней сохранённой версии документа.
        /// </summary>
        private readonly List<GlyphSnapshot> _snapshots = new List<GlyphSnapshot>();
        #endregion

        #region Properties
        /// <summary>
        /// Сохранён ли документ.
        /// </summary>
        public bool Saved
        {
            get { return CompareSnapshots(GetSnapshotsFromSheet()); }
        }

        /// <summary>
        /// Лист документа.
        /// </summary>
        public Sheet DocumentSheet { get; private set; }

        /// <summary>
        /// История документа.
        /// </summary>
        public History DocumentHistory { get; private set; }

        /// <summary>
        /// Имя документа.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Private methods
        /// <summary>
        /// Возвращает снимки всех глифов листа.
        /// </summary>
        private IEnumerable<GlyphSnapshot> GetSnapshotsFromSheet()
        {
            return DocumentSheet.Glyphs.Select(glyph => new GlyphSnapshot(glyph));
        }

        /// <summary>
        /// Сохраняет снимки глифов как последние сохранённые.
        /// <remarks>На их основании затем делается вывод, находится ли документ в сохранённом
        /// состоянии или нет.</remarks>
        /// </summary>
        /// <param name="snapshots">Снимки глифов.</param>
        private void SaveSnapshots(IEnumerable<GlyphSnapshot> snapshots)
        {
            _snapshots.Clear();

            foreach (GlyphSnapshot snapshot in snapshots)
                _snapshots.Add(snapshot);
        }

        /// <summary>
        /// Сравнивает переданные ему снимки глифов с хранимыми в документе снимками.
        /// Если различий нет, возвращает true.
        /// </summary>
        /// <param name="snapshots">Снимки глифов.</param>
        private bool CompareSnapshots(IEnumerable<GlyphSnapshot> snapshots)
        {
            // Новые снимки.
            var sshots = new List<GlyphSnapshot>(snapshots);

            // Если количество глифов различно,
            // значит состояния документа различны.
            if (sshots.Count != _snapshots.Count)
                return false;

            // Проходим по старым снимкам...
            foreach (GlyphSnapshot currSshot in _snapshots)
            {
                GlyphSnapshotsDiff diff = null;

                // Проходим по новым снимкам...
                foreach (GlyphSnapshot sshot in sshots)
                    if (sshot.GlyphId == currSshot.GlyphId &&
                        sshot.GlyphType == currSshot.GlyphType)
                    {
                        // Ищем соответствующие глифы, чтобы посмотреть различие в них.
                        diff = new GlyphSnapshotsDiff(currSshot, sshot);
                    }

                // Если каких-то глифов не хватает в одном из списков,
                // значит состояния документа различны.
                if (diff == null)
                    return false;

                // Если найдены различия в значениях свойств глифов,
                // значит состояния документа различны.
                if (diff.Count > 0)
                    return false;
            }

            // ...если никаких различий не нашли,
            // значит старое и новое состояние документа эквивалентны.
            return true;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Добавить текущий лист в историю.
        /// </summary>
        public void UpdateHistory()
        {
            //History.Add((Sheet) CurrentSheet.Clone());

            // Обновляем команды.
            //Command.GetInstance<UndoCommand>().Enabled = History.IsPrevExists;
            //Command.GetInstance<RedoCommand>().Enabled = History.IsNextExists;
        }

        /// <summary>
        /// Назад по истории.
        /// </summary>
        public void HistoryBack()
        {
            // Если предыдущий элемент истории существует, переходим к нему.
            //if (History.IsPrevExists)
            //{
            //    History.Prev();
            //    CurrentSheet = (Sheet)History.CurrentSheet.Clone();
            //}

            // Обновляем команды.
            //Command.GetInstance<UndoCommand>().Enabled = History.IsPrevExists;
            //Command.GetInstance<RedoCommand>().Enabled = History.IsNextExists;
        }

        /// <summary>
        /// Вперёд по истории.
        /// </summary>
        public void HistoryForward()
        {
            // Если следующий элемент истории существует, переходим к нему.
            //if (History.IsNextExists)
            //{
            //    History.Next();
            //    CurrentSheet = (Sheet)History.CurrentSheet.Clone();
            //}

            // Обновляем команды.
            //Command.GetInstance<UndoCommand>().Enabled = History.IsPrevExists;
            //Command.GetInstance<RedoCommand>().Enabled = History.IsNextExists;
        }
        #endregion
    }
}