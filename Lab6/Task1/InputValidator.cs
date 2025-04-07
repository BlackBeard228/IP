using System;

namespace Task1
{
    public class InputValidator : IInputValidator
    {
        public bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static string ReadValidString(string prompt)
        {
            InputValidator validator = new InputValidator();
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!validator.IsValid(input))
                {
                    Console.WriteLine("������� ������������ ��������. ����������, ���������� �����.");
                }
            } while (!validator.IsValid(input));

            return input;
        }

        public static int ReadValidInteger(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("������� ������������ ��������. ����������, ������� ����� �����.");
                }
            }
            return result;
        }
    }
}