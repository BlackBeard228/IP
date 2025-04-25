using System;

namespace MovieCatalog
{
    /// <summary>Класс-модель фильма.</summary>
    [Serializable]
    public sealed class Movie
    {
        
        private int _id;
        private string _title = null!;
        private string _director = null!;
        private string _genre = null!;
        private int _year;
        private int _durationMin;
        private double _rating;

        
        private static void EnsureWholeNumber(double number, string paramName)
        {
            if (number % 1 != 0)
                throw new ArgumentException("Значение должно быть целым числом", paramName);
        }

        //свойства
        public int Id
        {
            get => _id;
            private set
            {
                EnsureWholeNumber(value, nameof(Id));
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id), "Id не может быть отрицательным");
                _id = value;
            }
        }

        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException(nameof(Title));
        }

        public string Director
        {
            get => _director;
            set => _director = value ?? throw new ArgumentNullException(nameof(Director));
        }

        public string Genre
        {
            get => _genre;
            set => _genre = value ?? throw new ArgumentNullException(nameof(Genre));
        }

        public int Year
        {
            get => _year;
            set
            {
                EnsureWholeNumber(value, nameof(Year));
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Year), "Год не может быть отрицательным");
                _year = value;
            }
        }

        public int DurationMin
        {
            get => _durationMin;
            set
            {
                EnsureWholeNumber(value, nameof(DurationMin));
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(DurationMin), "Длительность должна быть > 0");
                _durationMin = value;
            }
        }

        public double Rating
        {
            get => _rating;
            set
            {
                if (value is < 0 or > 10)
                    throw new ArgumentOutOfRangeException(nameof(Rating), "Рейтинг должен быть в диапазоне 0–10");
                _rating = value;
            }
        }

        //конструктор
        public Movie(int id, string title, string director, string genre,
                     int year, int durationMin, double rating)
        {
            Id = id;          
            Title = title;
            Director = director;
            Genre = genre;
            Year = year;
            DurationMin = durationMin;
            Rating = rating;
        }

        public override string ToString() =>
            $"{Id,3} | {Title,-30} | {Director,-20} | {Genre,-12} | {Year,4} | {DurationMin,4} мин | {Rating,4:F1}";
    }
}
