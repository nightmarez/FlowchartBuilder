// <copyright file="TabsPanel.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-10</date>
// <summary>Панель табиков.</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Makarov.Framework.Core;

namespace Makarov.Framework.Components
{
    /// <summary>
    /// Панель табиков.
    /// </summary>
    public class TabsPanel : Control
    {
        #region Exceptions
        /// <summary>
        /// Исключение панели табиков.
        /// </summary>
        public class TabsPanelException : MakarovFrameworkException
        {
            /// <param name="message">Сообщение.</param>
            public TabsPanelException(string message)
                : base(message ?? string.Empty)
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Список табиков.
        /// </summary>
        private readonly List<Tab> _tabs = new List<Tab>();

        /// <summary>
        /// Ширина табиков.
        /// </summary>
        private int _tabWidth = 100;

        /// <summary>
        /// Высота табиков.
        /// </summary>
        private int _tabHeight = 20;

        /// <summary>
        /// Выбранный табик.
        /// </summary>
        private int? _selectedIndex;
        #endregion

        #region Constructors
        public TabsPanel()
        {
            Offset = 2;
            CornerSize = 6;
            SelectedIndex = null;
        }
        #endregion

        #region Public methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            const int offset = 2;

            int i = 0;

            foreach (KeyValuePair<string, GraphicsPath> kvp in Pathes)
            {
                bool isActive = SelectedIndex.HasValue && i == SelectedIndex.Value;

                using (kvp.Value)
                {
                    e.Graphics.TranslateTransform(kvp.Value.GetBounds().Width, 0);
                }

                ++i;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            int i = 0;

            foreach (KeyValuePair<string, GraphicsPath> kvp in Pathes)
            {
                using (kvp.Value)
                {
                    RectangleF bounds = kvp.Value.GetBounds();

                    if (bounds.Contains(e.X, e.Y))
                    {
                        SelectedIndex = i;
                        Invalidate();
                    }
                }

                ++i;
            }
        }

        /// <summary>
        /// Добавить табик.
        /// </summary>
        /// <param name="tab">Название.</param>
        /// <param name="id">Идентификатор.</param>
        public void AddTab(string tab, int id)
        {
            _tabs.Add(new Tab(id, tab));
            SelectedIndex = _tabs.Count - 1;
            Invalidate();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Ширина табиков.
        /// </summary>
        public int TabWidth
        {
            get { return _tabWidth; }

            set
            {
                if (value <= 0)
                    throw new TabsPanelException(
                        string.Format("Invalid tab width: {0}.", value));

                _tabWidth = value;
            }
        }

        /// <summary>
        /// Высота табиков.
        /// </summary>
        public int TabHeight
        {
            get { return _tabHeight; }

            set
            {
                if (value <= 0)
                    throw new TabsPanelException(
                        string.Format("Invalid tab height: {0}", value));

                _tabHeight = value;
            }
        }

        /// <summary>
        /// Выбранный табик.
        /// </summary>
        public Tab SelectedTab
        {
            get
            {
                int? idx = SelectedIndex;

                if (idx.HasValue)
                    return _tabs[idx.Value];

                return null;
            }
        }

        /// <summary>
        /// Выбранный табик.
        /// </summary>
        public int? SelectedIndex
        {
            get { return _selectedIndex; }

            private set
            {
                if (_selectedIndex.HasValue != value.HasValue ||
                    (_selectedIndex.HasValue && value.HasValue && _selectedIndex.Value != value.Value))
                {
                    _selectedIndex = value;

                    if (SelectedTabChanged != null)
                        SelectedTabChanged(this, new EventArgs());
                }
                else
                {
                    _selectedIndex = value;
                }
            }
        }

        public int Offset { get; set; }

        public int CornerSize { get; set; }

        public event EventHandler SelectedTabChanged;

        private IEnumerable<KeyValuePair<string, GraphicsPath>> Pathes
        {
            get
            {
                var list = new List<KeyValuePair<string, GraphicsPath>>();

                int i = 0;

                using (Graphics gfx = CreateGraphics())
                using (var font = new Font("Arial", 8))
                foreach (Tab tab in _tabs)
                {
                    //int requiredWidth = System.Math.Max(TabWidth, tab.RequiredWidth(gfx, font));

                    //var pts = new[]
                    //              {
                    //                  new Point(i*requiredWidth + Offset, Height + Offset),
                    //                  new Point(i*requiredWidth + Offset, Height - TabHeight + Offset + CornerSize),
                    //                  new Point(i*requiredWidth + Offset + CornerSize, Height - TabHeight + Offset),
                    //                  new Point((i + 1)*requiredWidth - Offset, Height - TabHeight + Offset),
                    //                  new Point((i + 1)*requiredWidth - Offset, Height + Offset)
                    //              };

                    //var path = new GraphicsPath();
                    //path.StartFigure();
                    //path.AddLines(pts);
                    //path.CloseFigure();

                    var path = tab.CreatePath(gfx, font, 200, null, Height, Height);

                    list.Add(new KeyValuePair<string, GraphicsPath>(tab.Caption, path));
                    ++i;
                }

                return list.ToArray();
            }
        }
        #endregion
    }
}
