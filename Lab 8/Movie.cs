using System;

namespace MovieCatalog
{
    /// <summary>
    /// Модель фильма. Содержит закрытые поля и открытые свойства
    /// со строгой проверкой значений через <see cref="Validator"/>.
    /// </summary>
    [Serializable]
    public sealed class Movie
    {
        // private-поля
        private int _id;
        private string _title = null!;
        private string _director = null!;
        private string _genre = null!;
        private int _year;
        private int _durationMin;
        private double _rating;
        

        // свойства-аксессоры
        public int Id
        {
            get => _id;
            private set => _id = Validator.ValidateId(value);
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
            set => _year = Validator.ValidateYear(value);
        }

        public int DurationMin
        {
            get => _durationMin;
            set => _durationMin = Validator.ValidateDuration(value);
        }

        public double Rating
        {
            get => _rating;
            set => _rating = Validator.ValidateRating(value);
        }
        

        /// <summary>Полный конструктор фильма.</summary>
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
