using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace MovieCatalog
{
    /// <summary>Хранение фильмов в бинарном файле + LINQ-запросы.</summary>
    public sealed class MovieRepository
    {
        private readonly string _fileName;
        private List<Movie> _movies = new();

        public MovieRepository(string fileName) => _fileName = fileName;

        //файл
        public void Load()
        {
            if (!File.Exists(_fileName)) { _movies = new(); return; }

            try
            {
#pragma warning disable SYSLIB0011
                using var fs = File.OpenRead(_fileName);
                var bf = new BinaryFormatter
                {
                    Binder = new CrossAssemblyBinder()   
                };
                _movies = (List<Movie>)bf.Deserialize(fs);
#pragma warning restore SYSLIB0011
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
                _movies = new();
            }
        }

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

        
        public IEnumerable<Movie> GetAll() => _movies;

        public bool Add(Movie movie)
        {
            if (_movies.Any(m => m.Id == movie.Id)) return false;
            _movies.Add(movie);
            return true;
        }

        public bool Remove(int id)
        {
            var victim = _movies.FirstOrDefault(m => m.Id == id);
            if (victim == null) return false;

            _movies.Remove(victim);
            return true;
        }

        //LINQ-запросы
        public IEnumerable<Movie> GetByGenre(string genre) =>
            _movies.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                   .OrderBy(m => m.Year);

        public IEnumerable<Movie> GetByRating(double threshold) =>
            _movies.Where(m => m.Rating > threshold)
                   .OrderByDescending(m => m.Rating);

        public double GetAverageDuration() =>
            _movies.Any() ? _movies.Average(m => m.DurationMin) : 0.0;

        public double GetMaxRating() =>
            _movies.Any() ? _movies.Max(m => m.Rating) : 0.0;
    }
}
