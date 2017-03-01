// <copyright file="DisposableStringDictionary.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-25</date>
// <summary>Строковой словарь с освобождением ресурсов.</summary>

using System;
using System.Collections.Generic;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Строковой словарь с освобождением ресурсов.
    /// </summary>
    public class DisposableStringDictionary<T> : StringDictionary<T>, IDisposable
        where T: IDisposable
    {
        public void Dispose()
        {
            foreach (KeyValuePair<string, T> kvp in this)
                kvp.Value.Dispose();

            Clear();
        }
    }
}
