// <copyright file="ProxyManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-VIII-14</date>
// <summary>Менеджер прокси-классов.</summary>

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Makarov.Framework.Core.Managers
{
    /// <summary>
    /// Менеджер прокси-классов.
    /// </summary>
    public static class ProxyManager
    {
        #region Private members
        /// <summary>
        /// Счётчик сборок (используется в имени сборки).
        /// </summary>
        private static int _asmI;

        /// <summary>
        /// Имя сборки.
        /// </summary>
        private static AssemblyName AsmName
        {
            get { return new AssemblyName("MakarovProxyAssembly" + (++_asmI) + ".dll"); }
        }

        private static AssemblyBuilder AsmBuilder
        {
            get { return AppDomain.CurrentDomain.DefineDynamicAssembly(AsmName, AssemblyBuilderAccess.Run); }
        }

        private static ModuleBuilder ModBuilder
        {
            get { return AsmBuilder.DefineDynamicModule(AsmName.Name); }
        }

        /// <summary>
        /// Счётчик типов (используется в имени типа прокси-объекта).
        /// </summary>
        private static int _i;

        /// <summary>
        /// Кеш типов прокси.
        /// </summary>
        // ReSharper disable InconsistentNaming
        private static readonly Dictionary<string, Type> _cache = new Dictionary<string, Type>();
        // ReSharper restore InconsistentNaming
        #endregion

        #region Public methods
        /// <summary>
        /// Создаёт прокси-объект для указанного объекта.
        /// </summary>
        /// <typeparam name="TA">Атрибут, которым помечены свойства для добавления в прокси-объект.</typeparam>
        /// <param name="obj">Исходный объект.</param>
        /// <returns>Прокси-объект для указанного объекта.</returns>
        public static object CreateProxy<TA>(object obj)
        {
            // Получаем тип объекта.
            Type currType = obj.GetType();

            // Имя типа объекта.
            string currTypeS = currType.ToString();

            // Если в кеше прокси для такого типа уже есть,
            // создаём и возвращаем экземпляр прокси.
            if (_cache.ContainsKey(currTypeS))
                return Activator.CreateInstance(_cache[currTypeS], new[] { obj });

            // Прокси-класс.
            TypeBuilder typeBuilder = ModBuilder.DefineType(
                "ProxyObject_" + (_i++) + "_" + Guid.NewGuid(),
                TypeAttributes.Class | TypeAttributes.Public,
                typeof(object));

            // Приватное поле, содержащее ссылку на объект, с которым связан данный прокси.
            FieldBuilder ownerField = typeBuilder.DefineField("_owner", currType, FieldAttributes.Private);

            // Ищем все поля в данном объекте, помеченные атрибутом ActivePropertyAttribute
            // и создаём соответствующие поля в прокси объекте.
            foreach (var prop in currType.GetProperties())
                foreach (var attrib in prop.GetCustomAttributes(true))
                    if (attrib is TA)
                    {
                        // Свойство.
                        PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(prop.Name,
                             PropertyAttributes.HasDefault,
                             prop.PropertyType,
                             null);

                        // Геттер.
                        MethodBuilder methodGet = typeBuilder.DefineMethod("Get_" + prop.Name,
                                    MethodAttributes.Public,
                                    prop.PropertyType,
                                    new Type[] { });

                        // Код геттера: берём значение свойства из связанного объекта.
                        ILGenerator ilGet = methodGet.GetILGenerator();

                        ilGet.Emit(OpCodes.Ldarg_0);
                        ilGet.Emit(OpCodes.Ldfld, ownerField);
                        ilGet.EmitCall(OpCodes.Callvirt, prop.GetGetMethod(), null);
                        ilGet.Emit(OpCodes.Ret);

                        // Сеттер.
                        MethodBuilder methodSet = typeBuilder.DefineMethod("Set_" + prop.Name,
                                    MethodAttributes.Public,
                                    null,
                                    new[] { prop.PropertyType });

                        // Код сеттера: сохраняем значение свойства в связанный объект.
                        ILGenerator ilSet = methodSet.GetILGenerator();

                        ilSet.Emit(OpCodes.Ldarg_0);
                        ilSet.Emit(OpCodes.Ldfld, ownerField);
                        ilSet.Emit(OpCodes.Ldarg_1);
                        ilSet.EmitCall(OpCodes.Callvirt, prop.GetSetMethod(), new[] { prop.PropertyType });
                        ilSet.Emit(OpCodes.Ret);

                        // Назначем геттер и сеттер свойству.
                        propertyBuilder.SetGetMethod(methodGet);
                        propertyBuilder.SetSetMethod(methodSet);

                        break;
                    }

            // Конструктор прокси-класса.
            var ctorParams = new[] { currType };
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, ctorParams);
            ILGenerator ilgen = constructorBuilder.GetILGenerator();

            // Вызываем конструктор родителя.
            ilgen.Emit(OpCodes.Ldarg_0);
            ilgen.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));

            // Загружаем ссылку на объект, с которым связан данный прокси в приватное поле.
            ilgen.Emit(OpCodes.Ldarg_0);
            ilgen.Emit(OpCodes.Ldarg_1);
            ilgen.Emit(OpCodes.Stfld, ownerField);

            ilgen.Emit(OpCodes.Ret);

            // Создаём и возвращаем тип прокси-объекта.
            Type type = typeBuilder.CreateType();
            _cache.Add(currTypeS, type);
            return Activator.CreateInstance(type, new[] { obj });
        }
        #endregion
    }
}