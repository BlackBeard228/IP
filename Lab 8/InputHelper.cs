using System;
using MovieCatalog;

namespace MovieCatalogCli
{
    /// <summary>Утилиты безопасного чтения числовых значений с консоли.</summary>
    internal static class InputHelper
    {
        /// <summary>Считывает целое число ≥ <paramref name="min"/>.</summary>
        public static int ReadInt(string prompt, int min)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out var v) && v >= min)
                    return v;
                Console.WriteLine($"Введите целое число ≥ {min}!");
            }
        }

        /// <summary>Считывает число с плавающей точкой в указанном диапазоне.</summary>
        public static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out var v) && v >= min && v <= max)
                    return v;
                Console.WriteLine($"Введите число в диапазоне {min}–{max}!");
            }
        }
    }
}
