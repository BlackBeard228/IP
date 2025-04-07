using System;

namespace Task1
{
    public class BaseStringClass
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        // Конструктор с параметром
        public BaseStringClass(string content)
        {
            _content = content;
        }

        // Конструктор копирования
        public BaseStringClass(BaseStringClass other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "Невозможно скопировать объект, так как он равен null");
            }
            _content = other._content;
        }

        // Метод для создания строки из первого и последнего символа поля _content
        public string GetFirstAndLastCharacters()
        {
            if (string.IsNullOrEmpty(_content))
            {
                return string.Empty;
            }

            if (_content.Length == 1)
            {
                return _content;
            }

            return _content[0].ToString() + _content[_content.Length - 1].ToString();
        }

        // Перегрузка метода ToString() для формирования строки из поля
        public override string ToString()
        {
            return $"Content: {_content}";
        }
    }
}