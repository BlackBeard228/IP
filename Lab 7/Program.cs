using System;
using System.Threading.Tasks;

namespace TasksProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nВыберите задание (1-10) или 0 для выхода:");
                Console.WriteLine("1. Текстовый файл (разность сумм половин)");
                Console.WriteLine("2. Текстовый файл (сумма элементов)");
                Console.WriteLine("3. Текстовый файл (переписать самую короткую и самую длинную строки)");
                Console.WriteLine("4. Бинарный файл (фильтрация четных чисел)");
                Console.WriteLine("5. Бинарный файл с данными структур (багаж, XML-сериализация)");
                Console.WriteLine("6. List – Удаление из списка всех элементов E");
                Console.WriteLine("7. LinkedList<T> – Вывод в обратном порядке элементов");
                Console.WriteLine("8. HashSet – Анализ закупок учебными заведениями");
                Console.WriteLine("9. HashSet – Вывод звонких согласных в алфавитном порядке");
                Console.WriteLine("10. Dictionary/SortedList – Формирование уникальных логинов учеников");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        exit = true;
                        break;
                    case "1":
                        Tasks.ExecuteTask1();
                        break;
                    case "2":
                        Tasks.ExecuteTask2();
                        break;
                    case "3":
                        Tasks.ExecuteTask3();
                        break;
                    case "4":
                        Tasks.ExecuteTask4();
                        break;
                    case "5":
                        Tasks.ExecuteTask5();
                        break;
                    case "6":
                        Tasks.ExecuteTask6();
                        break;
                    case "7":
                        Tasks.ExecuteTask7();
                        break;
                    case "8":
                        Tasks.ExecuteTask8();
                        break;
                    case "9":
                        Tasks.ExecuteTask9();
                        break;
                    case "10":
                        Tasks.ExecuteTask10();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
