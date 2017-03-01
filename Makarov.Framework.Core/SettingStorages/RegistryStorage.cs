// <copyright file="RegistryStorage.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-25</date>
// <summary>Класс для хранения настроек в реестре.</summary>

using Microsoft.Win32;

namespace Makarov.Framework.Core.SettingStorages
{
    /// <summary>
    /// Класс для хранения настроек в реестре.
    /// </summary>
    public sealed class RegistryStorage : IDict<string>
    {
        #region Exceptions
        /// <summary>
        /// Базовое исключение при работе с реестром.
        /// </summary>
        public class RegistryStorageException : MakarovFrameworkException
        {
            /// <param name="message">Сообщение.</param>
            public RegistryStorageException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Не удалось создать ключ реестра.
        /// </summary>
        public sealed class CantCreateKeyException : RegistryStorageException
        {
            /// <param name="key">Имя ключа.</param>
            public CantCreateKeyException(string key)
                : base(string.Format("Can't create registry key '{0}'.", key))
            { }
        }

        /// <summary>
        /// Не удалось удалить значение из реестра.
        /// </summary>
        public sealed class CantDeleteKeyException : RegistryStorageException
        {
            /// <param name="key">Имя ключа.</param>
            public CantDeleteKeyException(string key)
                : base(string.Format("Can't delete value from registry key '{0}'.", key))
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Ключ реестра, с которым ведётся работа.
        /// </summary>
        private RegistryKey _key;
        #endregion

        #region Constructors
        /// <param name="registryKey">Ключ реестра с настройками.</param>
        public RegistryStorage(string registryKey)
        {
            // Открываем ключ. Если ключа нет, создаём его.
            _key = Registry.CurrentUser.OpenSubKey(
                registryKey,
                RegistryKeyPermissionCheck.ReadWriteSubTree);

            if (_key == null)
            {
                _key = Registry.CurrentUser.CreateSubKey(
                    registryKey,
                    RegistryKeyPermissionCheck.ReadWriteSubTree);
            }

            if (!IsRegistryAccessible)
                throw new CantCreateKeyException(registryKey);
        }

        /// <summary>
        /// Деструктор.
        /// </summary>
        ~RegistryStorage()
        {
            Dispose();
        }

        /// <summary>
        /// Сохранить настройки и освободить ресурсы.
        /// </summary>
        public void Dispose()
        {
            if (_key != null)
            {
                _key.Flush();
                _key.Close();
                _key = null;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Возвращает значение подключа.
        /// </summary>
        /// <param name="subKey">Имя подключа.</param>
        /// <returns>Значение подключа</returns>
        public string GetRegistryValue(string subKey)
        {
            // Если есть доступ к реестру, читаем значение из реестра.
            if (IsRegistryAccessible)
                return (string)_key.GetValue(subKey);

            return null;
        }

        /// <summary>
        /// Устанавливает значение подключа.
        /// </summary>
        /// <param name="subKey">Имя подключа.</param>
        /// <param name="val">Новое значение подключа.</param>
        public void SetRegistryValue(string subKey, string val)
        {
            // Если есть доступ к реестру, пишем значение в реестр.
            if (IsRegistryAccessible)
            {
                _key.SetValue(subKey, val);
                return;
            }
        }

        /// <summary>
        /// Удаляет подключ.
        /// </summary>
        /// <param name="subKey">Имя подключа.</param>
        public void DeleteRegistryValue(string subKey)
        {
            // Если есть доступ к реестру, удаляем ключ из реестра.
            if (IsRegistryAccessible)
            {
                _key.DeleteSubKey(subKey);
                return;
            }

            // Ключ не найден, бросаем исключение.
            throw new CantDeleteKeyException(subKey);
        }

        /// <summary>
        /// Задано ли значение подключа.
        /// </summary>
        /// <param name="subKey">Имя подключа.</param>
        /// <returns>Задано ли значение подключа.</returns>
        public bool IsKeyExists(string subKey)
        {
            if (IsRegistryAccessible)
            {
                object obj = _key.GetValue(subKey);
                if (obj == null)
                    return false;

                return !string.IsNullOrEmpty((string)obj);
            }

            return false;
        }
        #endregion


        #region Properties
        /// <summary>
        /// Устанавливает и получает значение настроек в реестре.
        /// </summary>
        /// <param name="subKey">Имя подключа.</param>
        /// <returns>Значение настроек в реестре.</returns>
        public string this[string subKey]
        {
            get { return GetRegistryValue(subKey); }
            set { SetRegistryValue(subKey, value); }
        }

        /// <summary>
        /// Доступен ли реестр.
        /// </summary>
        public bool IsRegistryAccessible
        {
            get { return _key != null; }
        }
        #endregion
    }
}