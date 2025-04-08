using System;

namespace Task1
{
    public class Book : BaseStringClass
    {
        /// <summary>
        /// Поле Content (наследуемое) используется для хранения названия книги.
        /// Дополнительные поля:
        /// Author – имя автора книги;
        /// Year – год издания книги.
        /// </summary>
        private string Author { get; set; }
        private int Year { get; set; }

        // Конструктор с параметрами
        public Book(string title, string author, int year)
            : base(title) // название книги хранится в базовом классе
        {
            Author = author;
            Year = year;
        }

        // Конструктор копирования для Book
        public Book(Book other)
            : base(other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "Невозможно скопировать объект, так как он равен null");
            }
            Author = other.Author;
            Year = other.Year;
        }

        // Метод для получения полной информации о книге
        public string GetBookInfo()
        {
            return $"Title: {Content}, Author: {Author}, Year: {Year}";
        }

        // Метод, определяющий, является ли книга классикой (условно, если год выпуска меньше 1970)
        public bool IsClassic()
        {
            return Year < 1970;
        }

        // Метод для получения резюме, составленного из первой и последней буквы названия и автора
        public string GetTitleAndAuthorSummary()
        {
            string titleSummary = GetFirstAndLastCharacters();
            string authorSummary = string.Empty;
            if (!string.IsNullOrEmpty(Author))
            {
                authorSummary = Author[0].ToString();
                if (Author.Length > 1)
                {
                    authorSummary += Author[Author.Length - 1].ToString();
                }
            }
            return $"Title Summary: {titleSummary}, Author Summary: {authorSummary}";
        }

        // Перегрузка метода ToString()
        public override string ToString()
        {
            return $"{base.ToString()}, Author: {Author}, Year: {Year}";
        }
    }
}
