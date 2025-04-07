using System;

namespace Task2
{
    // Класс, реализующий интерфейс IInputValidator для проверки ввода с клавиатуры
    public class InputValidator : IInputValidator
    {
        public byte ReadValidByte(string prompt, byte min, byte max)
        {
            byte value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (byte.TryParse(input, out value))
                {
                    if (value >= min && value <= max)
                        break;
                    else
                        Console.WriteLine($"Значение должно быть от {min} до {max}. Повторите ввод.");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Введите числовое значение.");
                }
            }
            return value;
        }

        public uint ReadValidUInt(string prompt)
        {
            uint value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (uint.TryParse(input, out value))
                    break;
                else
                    Console.WriteLine("Некорректный ввод. Введите неотрицательное целое число.");
            }
            return value;
        }
    }
}