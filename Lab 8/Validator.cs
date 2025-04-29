using System;

namespace MovieCatalog
{
    /// <summary>
    /// Набор статических методов для проверки входных значений
    /// </summary>
    public static class Validator
    {
        /// <summary>Проверка, что значение целое.</summary>
        private static void EnsureWhole(double number, string param)
        {
            if (number % 1 != 0)
                throw new ArgumentException("Значение должно быть целым", param);
        }
        /// <summary>
        /// Проверка, что значение больше 0 и целое (ID)
        /// </summary>
        public static int ValidateId(double value)
        {
            EnsureWhole(value, nameof(value));
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Id ≥ 0");
            return (int)value;
        }
        /// <summary>
        /// Проверка, что значение больше 0 и целое (Year)
        /// </summary>
        public static int ValidateYear(double value)
        {
            EnsureWhole(value, nameof(value));
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Year ≥ 0");
            return (int)value;
        }
        /// <summary>
        /// Проверка, что значение больше 0 и целое (Duration)
        /// </summary>
        public static int ValidateDuration(double value)
        {
            EnsureWhole(value, nameof(value));
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Duration > 0");
            return (int)value;
        }
        /// <summary>
        /// Проверка, что значение больше 0 и меньше 10 (Rating)
        /// </summary>
        public static double ValidateRating(double value)
        {
            if (value is < 0 or > 10)
                throw new ArgumentOutOfRangeException(nameof(value), "Rating 0–10");
            return value;
        }
    }
}
