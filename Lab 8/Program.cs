using System;
using System.Globalization;
using MovieCatalog;
using MovieCatalogCli;

namespace MovieCatalogCli
{
    /// <summary>Точка входа консольного каталога фильмов.</summary>
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

            Console.Write("Файл БД (Enter = movies.bin): ");
            var file = Console.ReadLine();
            file = string.IsNullOrWhiteSpace(file) ? "movies.bin" : file.Trim();

            var repo = new MovieRepository(file);
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

                switch (Console.ReadLine())
                {
                    case "1": ShowAll(repo); break;
                    case "2": AddMovie(repo); break;
                    case "3": DeleteMovie(repo); break;
                    case "4": Queries(repo); break;
                    case "0": repo.Save(); return;
                    default: Console.WriteLine("Неизвестный пункт меню."); break;
                }
            }
        }

        private static void ShowAll(MovieRepository repo)
        {
            Console.WriteLine("\nID | Название                       | Режиссёр            | Жанр         | Год | Мин | Рейтинг");
            Console.WriteLine(new string('-', 108));
            foreach (var m in repo.GetAll()) Console.WriteLine(m);
        }

        private static void AddMovie(MovieRepository repo)
        {
            int id = InputHelper.ReadInt("Id: ", 0);
            Console.Write("Название: "); var t = Console.ReadLine()!;
            Console.Write("Режиссёр: "); var d = Console.ReadLine()!;
            Console.Write("Жанр: "); var g = Console.ReadLine()!;
            int year = InputHelper.ReadInt("Год: ", 0);
            int duration = InputHelper.ReadInt("Длительность (мин): ", 1);
            double rating = InputHelper.ReadDouble("Рейтинг (0–10): ", 0, 10);

            var movie = new Movie(id, t, d, g, year, duration, rating);
            Console.WriteLine(repo.Add(movie) ? "Фильм добавлен." : "Такой Id уже существует!");
        }

        private static void DeleteMovie(MovieRepository repo)
        {
            int id = InputHelper.ReadInt("Введите Id для удаления: ", 0);
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

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Жанр: ");
                    foreach (var m in repo.GetByGenre(Console.ReadLine()!)) Console.WriteLine(m);
                    break;

                case "2":
                    double thr = InputHelper.ReadDouble("Порог рейтинга: ", 0, 10);
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
