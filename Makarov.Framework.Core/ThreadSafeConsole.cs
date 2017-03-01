// <copyright file="ThreadSafeConsole.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-21</date>
// <summary>Потокобезопасная консоль.</summary>

using System;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Потокобезопасная консоль.
    /// </summary>
    public static class ThreadSafeConsole
    {
        private static readonly object SyncObject = new object();

        public static void Write(string message)
        {
            lock (SyncObject)
            {
                Console.Write(message);
            }
        }

        public static void Write(string message, ConsoleColor color)
        {
            lock (SyncObject)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ForegroundColor = oldColor;
            }
        }

        public static void WriteLine(string message)
        {
            lock (SyncObject)
            {
                Console.WriteLine(message);
            }
        }

        public static void WriteLine(string message, ConsoleColor color)
        {
            lock (SyncObject)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ForegroundColor = oldColor;
            }
        }

        public static void Write(string format, params string[] args)
        {
            lock (SyncObject)
            {
                Console.Write(format, args);
            }
        }

        public static void Write(string format, ConsoleColor color, params string[] args)
        {
            lock (SyncObject)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.Write(format, args);
                Console.ForegroundColor = oldColor;
            }
        }

        public static void WriteLine(string format, params string[] args)
        {
            lock (SyncObject)
            {
                Console.WriteLine(format, args);
            }
        }

        public static void WriteLine(string format, ConsoleColor color, params string[] args)
        {
            lock (SyncObject)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(format, args);
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
