using System;
using System.Globalization;
using MovieCatalog;

namespace MovieCatalogCli
{
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

            Console.Write("Введите имя бинарного файла (Enter = movies.bin): ");
            var fileName = Console.ReadLine();
            fileName = string.IsNullOrWhiteSpace(fileName) ? "movies.bin" : fileName.Trim();

            var repo = new MovieRepository(fileName);
            repo.Load();

            while (true)
            {
                Console.WriteLine("\n=== КАТАЛОГ ФИЛЬМОВ ===");
                Console.WriteLine("1. Просмотреть все фильмы");
                Console.WriteLine("2. Добавить фильм");
                Console.WriteLine("3. Удалить фильм");
                Console.WriteLine("4. LINQ-запросы");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": ShowAll(repo); break;
                        case "2": AddMovie(repo); break;
                        case "3": DeleteMovie(repo); break;
                        case "4": Queries(repo); break;
                        case "0": repo.Save(); return;
                        default: Console.WriteLine("Неизвестный пункт меню."); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            }
        }

        // вспомогательные методы ввода 
        private static int ReadInt(string prompt, int minValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= minValue)
                    return value;
                Console.WriteLine($"Введите целое число ≥ {minValue}!");
            }
        }

        private static double ReadDouble(string prompt, double minValue, double maxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) &&
                    value >= minValue && value <= maxValue)
                    return value;
                Console.WriteLine($"Введите число в диапазоне {minValue}–{maxValue}!");
            }
        }

        // пункты меню
        private static void ShowAll(MovieRepository repo)
        {
            Console.WriteLine("\nID | Название                       | Режиссёр            | Жанр         | Год | Мин | Рейтинг");
            Console.WriteLine(new string('-', 108));
            foreach (var m in repo.GetAll()) Console.WriteLine(m);
        }

        private static void AddMovie(MovieRepository repo)
        {
            int id = ReadInt("Id: ", 0);
            Console.Write("Название: "); string t = Console.ReadLine()!;
            Console.Write("Режиссёр: "); string d = Console.ReadLine()!;
            Console.Write("Жанр: "); string g = Console.ReadLine()!;
            int year = ReadInt("Год: ", 0);
            int duration = ReadInt("Длительность (мин): ", 1);
            double rating = ReadDouble("Рейтинг (0–10): ", 0, 10);

            var movie = new Movie(id, t, d, g, year, duration, rating);
            Console.WriteLine(repo.Add(movie) ? "Фильм добавлен." : "Такой Id уже существует!");
        }

        private static void DeleteMovie(MovieRepository repo)
        {
            int id = ReadInt("Введите Id для удаления: ", 0);
            Console.WriteLine(repo.Remove(id) ? "Фильм удалён." : "Фильм не найден.");
        }

        private static void Queries(MovieRepository repo)
        {
            Console.WriteLine("\n=== Запросы ===");
            Console.WriteLine("1. Фильмы указанного жанра");
            Console.WriteLine("2. Фильмы с рейтингом выше порога");
            Console.WriteLine("3. Средняя длительность всех фильмов");
            Console.WriteLine("4. Максимальный рейтинг");
            Console.Write("Ваш выбор: ");
            var q = Console.ReadLine();

            switch (q)
            {
                case "1":
                    Console.Write("Жанр: ");
                    foreach (var m in repo.GetByGenre(Console.ReadLine()!)) Console.WriteLine(m);
                    break;

                case "2":
                    double thr = ReadDouble("Порог рейтинга: ", 0, 10);
                    foreach (var m in repo.GetByRating(thr)) Console.WriteLine(m);
                    break;

                case "3":
                    Console.WriteLine($"Средняя длительность: {repo.GetAverageDuration():F1} мин");
                    break;

                case "4":
                    Console.WriteLine($"Максимальный рейтинг: {repo.GetMaxRating():F1}");
                    break;

                default:
                    Console.WriteLine("Неизвестный запрос.");
                    break;
            }
        }
    }
}
