using System;

namespace TimeWpfApp
{
    public class Time
    {
        private byte hours;   // 0-23
        private byte minutes; // 0-59

        public byte Minutes
        {
            get { return minutes; }
            set
            {
                if (value > 59)
                    throw new ArgumentOutOfRangeException(nameof(value));
                minutes = value;
            }
        }

        public Time(byte h, byte m)
        {
            if (h > 23) throw new ArgumentOutOfRangeException(nameof(h));
            if (m > 59) throw new ArgumentOutOfRangeException(nameof(m));
            hours = h; minutes = m;
        }
        public Time(Time other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            hours = other.hours; minutes = other.minutes;
        }

        public override string ToString()
        {
            return string.Format("{0:D2}:{1:D2}", hours, minutes);
        }

        // добавление произвольного количества минут
        public Time AddMinutes(uint plus)
        {
            int total = hours * 60 + minutes + (int)plus;
            total = (total % (24 * 60) + 24 * 60) % (24 * 60);
            return new Time((byte)(total / 60), (byte)(total % 60));
        }

        /* ----- перегрузки ----- */
        public static Time operator +(Time t, uint m) { return t.AddMinutes(m); }
        public static Time operator +(uint m, Time t) { return t + m; }
        public static Time operator -(Time t, uint m) { return t + (uint)-(int)m; }
        public static Time operator ++(Time t) { return t + 1U; }
        public static Time operator --(Time t) { return t - 1U; }
        public static explicit operator byte(Time t) { return t.hours; }
        public static implicit operator bool(Time t)
        {
            return t != null && (t.hours != 0 || t.minutes != 0);
        }
    }
}
