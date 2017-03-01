// <copyright file="A.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-29</date>
// <summary>Семейство листов формата A.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetFamily("ISO A (Vertical layout)")]
    public abstract class VerticalA : BaseIso
    {
    }

    [SheetFamily("ISO A (Horizontal layout)")]
    public abstract class HorizontalA : BaseIso
    {
    }
}