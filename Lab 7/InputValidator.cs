using System;

namespace TasksProject
{
    /// <summary>
    /// Класс для проверки корректности вводимых данных с консоли.
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// Считывает с консоли целое число в диапазоне [min; max].
        /// </summary>
        public static int ReadValidInt(string prompt, int min, int max)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value))
                {
                    if (value >= min && value <= max)
                        break;
                    else
                        Console.WriteLine($"Введенное число должно быть от {min} до {max}. Повторите ввод.");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }
            }
            return value;
        }

        /// <summary>
        /// Считывает с консоли целое число (без явного диапазона).
        /// </summary>
        public static int ReadValidInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value))
                    break;
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
            return value;
        }

        /// <summary>
        /// Универсальное считывание элемента. Если введенное значение можно преобразовать в int или double,
        /// возвращается соответствующий тип; иначе – возвращается строка.
        /// </summary>
        public static object ReadValidElement(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int intValue))
                return intValue;
            else if (double.TryParse(input, out double doubleValue))
                return doubleValue;
            else
                return input;
        }
    }
}
