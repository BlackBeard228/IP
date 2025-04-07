using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputValidator validator = new InputValidator();

            Console.WriteLine("Создание объекта Time:");
            byte hours = validator.ReadValidByte("Введите часы (0-23): ", 0, 23);
            byte minutes = validator.ReadValidByte("Введите минуты (0-59): ", 0, 59);

            Time time = new Time(hours, minutes);
            Console.WriteLine("\nИсходное время: " + time);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1 - Вывести текущее время (ToString)");
                Console.WriteLine("2 - Добавить минуты (оператор + и метод AddMinutes)");
                Console.WriteLine("3 - Вычесть минуты (оператор -)");
                Console.WriteLine("4 - Инкремент (оператор ++, добавить 1 минуту)");
                Console.WriteLine("5 - Декремент (оператор --, вычесть 1 минуту)");
                Console.WriteLine("6 - Приведение к byte (извлечь часы)");
                Console.WriteLine("7 - Приведение к bool (проверка, что время не 00:00)");
                Console.WriteLine("8 - Создать копию объекта Time и вывести её");
                Console.WriteLine("9 - Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    // Вывод текущего времени
                    case "1":
                        Console.WriteLine("Текущее время: " + time);
                        break;

                    // Добавление минут (выбор через оператор +)
                    case "2":
                        uint addMinutes = validator.ReadValidUInt("Введите количество минут для добавления: ");
                        Time addedTime = time + addMinutes;
                        Console.WriteLine($"После добавления {addMinutes} минут: {addedTime}");
                        break;

                    // Вычитание минут (оператор -)
                    case "3":
                        uint subtractMinutes = validator.ReadValidUInt("Введите количество минут для вычитания: ");
                        Time subtractedTime = time - subtractMinutes;
                        Console.WriteLine($"После вычитания {subtractMinutes} минут: {subtractedTime}");
                        break;

                    // Инкремент (оператор ++)
                    case "4":
                        time = ++time;
                        Console.WriteLine("После инкремента (добавление 1 минуты): " + time);
                        break;

                    // Декремент (оператор --)
                    case "5":
                        time = --time;
                        Console.WriteLine("После декремента (вычитание 1 минуты): " + time);
                        break;

                    // Приведение к byte (извлекается количество часов)
                    case "6":
                        byte extractedHours = (byte)time;
                        Console.WriteLine("Извлечённые часы (byte): " + extractedHours);
                        break;

                    // Приведение к bool (проверяется, что время не 00:00)
                    case "7":
                        bool nonZeroTime = time;
                        Console.WriteLine($"Преобразование Time в bool: {nonZeroTime}");
                        break;

                    // Создание копии объекта Time
                    case "8":
                        Time copyTime = new Time(time);
                        Console.WriteLine("Копия объекта: " + copyTime);
                        break;

                    // Выход
                    case "9":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите Enter для продолжения...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Выход из программы. Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}