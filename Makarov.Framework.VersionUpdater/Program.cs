using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Makarov.Framework.VersionUpdater
{
    public sealed class Program
    {
        static void Main()
        {
            // Текущая папка, из которой нужно начинать поиск файлов.
            string currentDirectory = Application.StartupPath;

            // Ищем файлы, в которых есть информация о версии.
            string[] files = Directory.GetFiles(currentDirectory, @"AssemblyInfo.cs", SearchOption.AllDirectories);

            // Проходим по всем найденным файлам...
            foreach (string file in files)
            {
                Console.WriteLine(@"File '{0}'...", file);

                // Ищем атрибут с версией.
                var regex = new Regex("Version\\(\"[0-9]*\\.[0-9]*\\.[0-9]*\\.[0-9]*\"\\)");
                string text = File.ReadAllText(file);
                MatchCollection matches = regex.Matches(text);

                // Проходим по всем найденным атрибутам...
                foreach (Match match in matches)
                {
                    // Текущая строка, указывающая версию.
                    string oldValue = match.ToString();

                    // Разбиваем её на фрагменты.
                    int idx0 = oldValue.LastIndexOf('.');
                    int idx1 = oldValue.LastIndexOf('"');
                    string firstPart = oldValue.Remove(idx0 + 1);
                    string secondPart = oldValue.Remove(0, idx0 + 1).Remove(idx1 - idx0 - 1);
                    string thirdPart = oldValue.Remove(0, idx1);

                    // Увеличиваем номер версии.
                    int ver = int.Parse(secondPart);
                    int newVersion = ver + 1;
                    Console.WriteLine("Version changed {0} -> {1}", ver, newVersion);

                    // Собираем новую строку с информацией о версии.
                    string newValue = firstPart + newVersion + thirdPart;
                    Console.WriteLine("Old: {0}", oldValue);
                    Console.WriteLine("New: {0}", newValue);
                    text = text.Replace(oldValue, newValue);
                }

                // Записываем результат.
                File.WriteAllText(file, text);
            }
        }
    }
}
