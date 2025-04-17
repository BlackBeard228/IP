using System;

namespace TasksProject
{
    /// <summary>
    /// ����� ��� �������� ������������ �������� ������ � �������.
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// ��������� � ������� ����� ����� � ��������� [min; max].
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
                        Console.WriteLine($"��������� ����� ������ ���� �� {min} �� {max}. ��������� ����.");
                }
                else
                {
                    Console.WriteLine("������������ ����. ���������� �����.");
                }
            }
            return value;
        }

        /// <summary>
        /// ��������� � ������� ����� ����� (��� ������ ���������).
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
                Console.WriteLine("������������ ����. ���������� �����.");
            }
            return value;
        }

        /// <summary>
        /// ������������� ���������� ��������. ���� ��������� �������� ����� ������������� � int ��� double,
        /// ������������ ��������������� ���; ����� � ������������ ������.
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
