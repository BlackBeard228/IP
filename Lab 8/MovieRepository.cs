using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace MovieCatalog
{
    /// <summary>
    /// Хранит фильмы в памяти, умеет сохранять/загружать их в бинарный файл
    /// и предоставляет набор LINQ-запросов к коллекции.
    /// </summary>
    public sealed class MovieRepository
    {
        private readonly string _fileName;
        private List<Movie> _movies = new();

        public MovieRepository(string fileName) => _fileName = fileName;

        #region Файл
        /// <summary>Чтение файла в коллекцию (с подменой сборки).</summary>
        public void Load()
        {
            if (!File.Exists(_fileName)) { _movies = new(); return; }

            try
            {
#pragma warning disable SYSLIB0011
                using var fs = File.OpenRead(_fileName);
                var bf = new BinaryFormatter { Binder = new CrossAssemblyBinder() };
                _movies = (List<Movie>)bf.Deserialize(fs);
#pragma warning restore SYSLIB0011
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
                _movies = new();
            }
        }

        /// <summary>Сохраняет текущий список фильмов в бинарный файл.</summary>
        public void Save()
        {
            try
            {
#pragma warning disable SYSLIB0011
                using var fs = File.Create(_fileName);
                new BinaryFormatter().Serialize(fs, _movies);
#pragma warning restore SYSLIB0011
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка записи файла: {ex.Message}"); }
        }
        #endregion

        
        public IEnumerable<Movie> GetAll() => _movies;
        /// <summary>
        /// Добавление фильма
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public bool Add(Movie movie)
        {
            if (_movies.Any(m => m.Id == movie.Id)) return false;
            _movies.Add(movie);
            return true;
        }
        /// <summary>
        /// Удаление фильма
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            var victim = _movies.FirstOrDefault(m => m.Id == id);
            if (victim == null) return false;
            _movies.Remove(victim);
            return true;
        }
        

        // LINQ-запросы
        /// <summary>Фильмы выбранного жанра, упорядоченные по году.</summary>
        public IEnumerable<Movie> GetByGenre(string genre) =>
            from m in _movies
            where m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)
            orderby m.Year
            select m;

        /// <summary>Фильмы, у которых рейтинг выше заданного порога.</summary>
        public IEnumerable<Movie> GetByRating(double threshold) =>
            from m in _movies
            where m.Rating > threshold
            orderby m.Rating descending
            select m;

        /// <summary>Средняя длительность всех фильмов (0, если список пуст).</summary>
        public double GetAverageDuration()
        {
            var durations =
                from m in _movies
                select m.DurationMin;

            return durations.Any() ? durations.Average() : 0.0;
        }

        /// <summary>Максимальный рейтинг (0, если список пуст).</summary>
        public double GetMaxRating()
        {
            var ratings =
                from m in _movies
                select m.Rating;

            return ratings.Any() ? ratings.Max() : 0.0;
        }
        
    }
}
