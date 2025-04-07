using System;

namespace Task2
{
    public class Time
    {
        private byte hours;
        private byte minutes;

        public byte Hours
        {
            get => hours;
            set
            {
                if (value > 23)
                    throw new ArgumentOutOfRangeException(nameof(value), "Часы должны быть в диапазоне от 0 до 23.");
                hours = value;
            }
        }

        public byte Minutes
        {
            get => minutes;
            set
            {
                if (value > 59)
                    throw new ArgumentOutOfRangeException(nameof(value), "Минуты должны быть в диапазоне от 0 до 59.");
                minutes = value;
            }
        }

        // Конструктор с параметрами
        public Time(byte hours, byte minutes)
        {
            if (hours > 23)
                throw new ArgumentOutOfRangeException(nameof(hours), "Часы должны быть в диапазоне от 0 до 23.");
            if (minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), "Минуты должны быть в диапазоне от 0 до 59.");

            this.hours = hours;
            this.minutes = minutes;
        }

        // Конструктор копирования
        public Time(Time other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "Невозможно скопировать объект, равный null.");
            hours = other.hours;
            minutes = other.minutes;
        }

        // Переопределение метода ToString() для вывода времени в формате ЧЧ:ММ
        public override string ToString()
        {
            return $"{hours:D2}:{minutes:D2}";
        }

        /// <summary>
        /// Метод добавления произвольного количества минут (uint) к текущему объекту.
        /// Результат рассчитывается по модулю 24 часа и возвращается новый объект Time.
        /// </summary>
        public Time AddMinutes(uint minutesToAdd)
        {
            int totalMinutes = hours * 60 + minutes + (int)minutesToAdd;
            totalMinutes %= (24 * 60);
            if (totalMinutes < 0)
            {
                totalMinutes += (24 * 60);
            }
            byte newHours = (byte)(totalMinutes / 60);
            byte newMinutes = (byte)(totalMinutes % 60);
            return new Time(newHours, newMinutes);
        }


        // Бинарный оператор +: Time + uint (добавление минут)
        public static Time operator +(Time t, uint minutesToAdd)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t.AddMinutes(minutesToAdd);
        }

        // Бинарный оператор + в другой форме: uint + Time
        public static Time operator +(uint minutesToAdd, Time t)
        {
            return t + minutesToAdd;
        }

        // Бинарный оператор -: Time - uint (вычитание минут)
        public static Time operator -(Time t, uint minutesToSubtract)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            int totalMinutes = t.hours * 60 + t.minutes - (int)minutesToSubtract;
            totalMinutes %= (24 * 60);
            if (totalMinutes < 0)
                totalMinutes += (24 * 60);
            byte newHours = (byte)(totalMinutes / 60);
            byte newMinutes = (byte)(totalMinutes % 60);
            return new Time(newHours, newMinutes);
        }

        // Унарный оператор ++: добавление одной минуты
        public static Time operator ++(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t + 1U;
        }

        // Унарный оператор --: вычитание одной минуты
        public static Time operator --(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t - 1U;
        }

        // Явное приведение к типу byte – возвращает количество часов (минуты отбрасываются)
        public static explicit operator byte(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t.hours;
        }

        // Неявное приведение к типу bool – возвращает true, если и часы и минуты не равны нулю
        public static implicit operator bool(Time t)
        {
            return t != null && (t.hours != 0 || t.minutes != 0);
        }
    }
}