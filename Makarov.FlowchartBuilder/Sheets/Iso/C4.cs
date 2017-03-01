// <copyright file="C4.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-30</date>
// <summary>Лист формата C4.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(229)]
    [SheetHeight(324)]
    [SheetName("C4")]
    public class VerticalC4 : VerticalC
    {
    }

    [SheetWidth(324)]
    [SheetHeight(229)]
    [SheetName("C4")]
    public class HorizontalC4 : HorizontalC
    {
    }
}