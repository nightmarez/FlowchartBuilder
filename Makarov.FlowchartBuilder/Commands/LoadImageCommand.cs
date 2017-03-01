// <copyright file="LoadImageCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-II-06</date>
// <summary>Команда - загрузка изображения в качестве глифа.</summary>

using System;
using System.Drawing;
using System.Windows.Forms;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - загрузка изображения в качестве глифа.
    /// </summary>
    public sealed class LoadImageCommand : CommandedGlyphCommand
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "All Picture Files|*.bmp;*.png;*.jpg;*.jpeg|Bitmap Files|*bmp|PNG|*.png|JPEG|*.jpg;*.jpeg|All Files|*.*";
                dialog.Multiselect = false;

                if (dialog.ShowDialog(Core.Instance.MainWindow) == DialogResult.OK)
                {
                    var bmp = (Bitmap)Image.FromFile(dialog.FileName);
                    Core.Instance.CurrentDocument.DocumentSheet.DeselectAllGlyphs();
                    var glyph = (AbstractGlyph) Activator.CreateInstance(GlyphType, new object[] {bmp});
                    glyph.Selected = true;
                    Core.Instance.CurrentDocument.DocumentSheet.Add(glyph);
                }
            }
        }
    }
}