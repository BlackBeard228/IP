using System;

namespace TasksProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите номер задания для проверки (1-10):");
                Console.WriteLine(" 1 - Задание 1 (Разность сумм: текстовый файл, одно число на строке)");
                Console.WriteLine(" 2 - Задание 2 (Сумма чисел из текстового файла, несколько чисел в строке)");
                Console.WriteLine(" 3 - Задание 3 (Копирование самой короткой и самой длинной строки из существующего текстового файла)");
                Console.WriteLine(" 4 - Задание 4 (Бинарный файл: выбор чётных чисел)");
                Console.WriteLine(" 5 - Задание 5 (Бинарный файл и XML-сериализация: обработка багажа пассажиров)");
                Console.WriteLine(" 6 - Задание 6 (List: удаление всех вхождений элемента из списка)");
                Console.WriteLine(" 7 - Задание 7 (LinkedList: вывод элементов в обратном порядке, ввод вручную)");
                Console.WriteLine(" 8 - Задание 8 (HashSet: анализ закупок учебных заведений)");
                Console.WriteLine(" 9 - Задание 9 (HashSet: вывод звонких согласных из уже созданного текстового файла)");
                Console.WriteLine("10 - Задание 10 (Dictionary: генерация логинов учеников из файла)");
                Console.WriteLine(" 0 - Выход");
                Console.WriteLine();

                int choice = InputValidator.ReadValidInt("Ваш выбор: ", 0, 10);
                Console.WriteLine();

                if (choice == 0)
                {
                    exit = true;
                    break;
                }

                TaskSolver.RunTask(choice);

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}
