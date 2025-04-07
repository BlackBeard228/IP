using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ввод данных с клавиатуры для создания объекта Book
            Console.WriteLine("Введите данные для создания объекта Book:");
            string title = InputValidator.ReadValidString("Введите название книги: ");
            string author = InputValidator.ReadValidString("Введите имя автора: ");
            int year = InputValidator.ReadValidInteger("Введите год издания книги: ");

            Book book = new Book(title, author, year);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню действий:");
                Console.WriteLine("1 - Вывести результат работы метода GetFirstAndLastCharacters");
                Console.WriteLine("2 - Вывести данные объекта (ToString)");
                Console.WriteLine("3 - Вывести информацию о книге (GetBookInfo)");
                Console.WriteLine("4 - Проверить, является ли книга классикой (IsClassic)");
                Console.WriteLine("5 - Вывести сводку (GetTitleAndAuthorSummary)");
                Console.WriteLine("6 - Создать копию объекта и вывести данные копии");
                Console.WriteLine("7 - Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Результат GetFirstAndLastCharacters: " + book.GetFirstAndLastCharacters());
                        break;
                    case "2":
                        Console.WriteLine("Результат ToString: " + book.ToString());
                        break;
                    case "3":
                        Console.WriteLine("Информация о книге: " + book.GetBookInfo());
                        break;
                    case "4":
                        Console.WriteLine(book.IsClassic() ? "Книга является классикой." : "Книга не является классикой.");
                        break;
                    case "5":
                        Console.WriteLine("Сводка (название и автор): " + book.GetTitleAndAuthorSummary());
                        break;
                    case "6":
                        Book copy = new Book(book);
                        Console.WriteLine("Копия объекта:");
                        Console.WriteLine(copy.ToString());
                        break;
                    case "7":
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