// <copyright file="Command.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Базовый класс команды.</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using Makarov.Framework.Components;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Базовый класс команды.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Исключение в команде.
        /// </summary>
        public class CommandException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            public CommandException(string message)
                : base(message ?? string.Empty)
            { }
        }

        /// <summary>
        /// Команда не существует.
        /// </summary>
        public class CommandNotExistsException : CommandException
        {
            /// <param name="commandTypeName">Имя типа команды.</param>
            public CommandNotExistsException(string commandTypeName)
                : base(string.Format("Commad type '{0}' not exists.", commandTypeName ?? string.Empty))
            { }
        }

        /// <summary>
        /// Кэши команд.
        /// </summary>
        private static readonly Dictionary<string, KeyValuePair<int, Command>> Commands1 =
            new Dictionary<string, KeyValuePair<int, Command>>();
        private static readonly Dictionary<string, KeyValuePair<int, Command>> Commands2 =
            new Dictionary<string, KeyValuePair<int, Command>>();
        private static readonly Dictionary<string, KeyValuePair<int, Command>> Commands3 =
            new Dictionary<string, KeyValuePair<int, Command>>();

        /// <summary>
        /// Очистить кэш команд.
        /// </summary>
        public static void FlushCache()
        {
            Commands1.Clear();
            Commands2.Clear();
            Commands3.Clear();
        }

        /// <summary>
        /// Сколько раз нужно вызвать команду, чтобы она перешла в другой кэш.
        /// </summary>
        private const int IntercacheCounter = 20;

        /// <summary>
        /// Возвращает экземпляр команды.
        /// </summary>
        /// <param name="command">Тип команды.</param>
        /// <returns>Экземпляр команды.</returns>
        public static Command GetInstance(string command)
        {
            return GetInstance(command, null);
        }

        /// <summary>
        /// Возвращает экземпляр команды.
        /// </summary>
        /// <param name="command">Тип команды.</param>
        /// <param name="control">Контрол.</param>
        /// <returns>Экземпляр команды.</returns>
        public static Command GetInstance(string command, IActiveControl control)
        {
            // Если команда есть в кэше третьего уровня, возвращаем её.
            if (Commands3.ContainsKey(command))
            {
                // Добаляем контрол в команду.
                Commands3[command].Value.AddControl(control);

                // Возвращаем команду.
                return Commands3[command].Value;
            }

            // Если команда есть в кэше второго уровня...
            if (Commands2.ContainsKey(command))
            {
                // Текущее количество обращений к команде.
                int i = Commands2[command].Key + 1;

                // Команда.
                Command cmd = Commands2[command].Value;

                // Добаляем контрол в команду.
                cmd.AddControl(control);

                // Если количество обращений к команде больше порогового значения...
                if (i >= IntercacheCounter)
                {
                    // Переводим команду в кэш третьего уровня.
                    Commands3.Add(command, new KeyValuePair<int, Command>(0, cmd));
                    Commands2.Remove(command);
                }
                else
                {
                    // Инкрементируем счётчик обращений к команде.
                    Commands2[command] = new KeyValuePair<int, Command>(i, cmd);
                }

                // Возвращаем команду.
                return cmd;
            }

            // Если команда есть в кэше первого уровня...
            if (Commands1.ContainsKey(command))
            {
                // Текущее количество обращений к команде.
                int i = Commands1[command].Key + 1;

                // Команда.
                Command cmd = Commands1[command].Value;

                // Добаляем контрол в команду.
                cmd.AddControl(control);

                // Если количество обращений к команде больше порогового значения...
                if (i >= IntercacheCounter)
                {
                    // Переводим команду в кэш второго уровня.
                    Commands2.Add(command, new KeyValuePair<int, Command>(0, cmd));
                    Commands1.Remove(command);
                }
                else
                {
                    // Инкрементируем счётчик обращений к команде.
                    Commands1[command] = new KeyValuePair<int, Command>(i, cmd);
                }

                // Возвращаем команду.
                return cmd;
            }

            // Если команда не найдена ни в одном из кэшей...

            // Имя типа команды.
            string cmdTypeName = @"Makarov.FlowchartBuilder.Commands." + command;

            // Определяем тип команды.
            Type cmdType = Type.GetType(cmdTypeName);

            // Если нет такого типа, бросаем исключение.
            if (cmdType == null)
                throw new CommandNotExistsException(cmdTypeName);

            // Создаём экземпляр данного типа.
            var newCommand = (Command) Activator.CreateInstance(cmdType);

            // Добавляем команду в кэш первого уровня.
            Commands1.Add(command, new KeyValuePair<int, Command>(0, newCommand));

            // Добаляем контрол в команду.
            newCommand.AddControl(control);

            // Возвращаем команду.
            return newCommand;
        }

        /// <summary>
        /// Возвращает экземпляр команды.
        /// </summary>
        /// <typeparam name="T">Тип команды</typeparam>
        /// <returns>Экземпляр команды.</returns>
        public static Command GetInstance<T>()
        {
            string[] commandName = typeof(T).Name.Split('.');
            return GetInstance(commandName[commandName.Length - 1]);
        }

        /// <summary>
        /// Возвращает экземпляр команды.
        /// </summary>
        /// <typeparam name="T">Тип команды.</typeparam>
        /// <param name="control">Контрол.</param>
        /// <returns>Экземпляр команды.</returns>
        public static Command GetInstance<T>(IActiveControl control)
        {
            string[] commandName = typeof(T).Name.Split('.');
            return GetInstance(commandName[commandName.Length - 1], control);
        }

        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Связанные контролы.
        /// </summary>
        private readonly List<IActiveControl> _assignedControls = new List<IActiveControl>();

        /// <summary>
        /// Добавить контрол.
        /// </summary>
        /// <param name="control">Контрол.</param>
        public void AddControl(IActiveControl control)
        {
            // Если контрол не задан, выходим.
            if (control == null)
                return;
            
            // Если контрол ещё не добавлен, добавляем.
            if (!_assignedControls.Contains(control))
                _assignedControls.Add(control);
        }

        /// <summary>
        /// Очистить контролы.
        /// </summary>
        public void FlushControls()
        {
            _assignedControls.Clear();
        }

        /// <summary>
        /// Активны ли контролы.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _assignedControls.Select(control => control.ActiveEnabled).FirstOrDefault();
            }

            set
            {
                foreach (var control in _assignedControls)
                    control.ActiveEnabled = value;
            }
        }

        /// <summary>
        /// Нажаты ли контролы.
        /// </summary>
        public bool Checked
        {
            get
            {
                return _assignedControls.Select(control => control.ActiveChecked).FirstOrDefault();
            }

            set
            {
                foreach (var control in _assignedControls)
                    control.ActiveChecked = value;
            }
        }
    }
}