using System;

namespace Task2
{
    // �����, ����������� ��������� IInputValidator ��� �������� ����� � ����������
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
                        Console.WriteLine($"�������� ������ ���� �� {min} �� {max}. ��������� ����.");
                }
                else
                {
                    Console.WriteLine("������������ ����. ������� �������� ��������.");
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
                    Console.WriteLine("������������ ����. ������� ��������������� ����� �����.");
            }
            return value;
        }
    }
}