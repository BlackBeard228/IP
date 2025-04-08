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
                    throw new ArgumentOutOfRangeException(nameof(value), "×àñû äîëæíû áûòü â äèàïàçîíå îò 0 äî 23.");
                hours = value;
            }
        }

        public byte Minutes
        {
            get => minutes;
            set
            {
                if (value > 59)
                    throw new ArgumentOutOfRangeException(nameof(value), "Ìèíóòû äîëæíû áûòü â äèàïàçîíå îò 0 äî 59.");
                minutes = value;
            }
        }

        // Êîíñòðóêòîð ñ ïàðàìåòðàìè
        public Time(byte hours, byte minutes)
        {
            if (hours > 23)
                throw new ArgumentOutOfRangeException(nameof(hours), "×àñû äîëæíû áûòü â äèàïàçîíå îò 0 äî 23.");
            if (minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), "Ìèíóòû äîëæíû áûòü â äèàïàçîíå îò 0 äî 59.");

            this.hours = hours;
            this.minutes = minutes;
        }

        // Êîíñòðóêòîð êîïèðîâàíèÿ
        public Time(Time other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "Íåâîçìîæíî ñêîïèðîâàòü îáúåêò, ðàâíûé null.");
            hours = other.hours;
            minutes = other.minutes;
        }

        // Ïåðåîïðåäåëåíèå ìåòîäà ToString() äëÿ âûâîäà âðåìåíè â ôîðìàòå ××:ÌÌ
        public override string ToString()
        {
            return $"{hours:D2}:{minutes:D2}";
        }

        /// <summary>
        /// Ìåòîä äîáàâëåíèÿ ïðîèçâîëüíîãî êîëè÷åñòâà ìèíóò (uint) ê òåêóùåìó îáúåêòó.
        /// Ðåçóëüòàò ðàññ÷èòûâàåòñÿ ïî ìîäóëþ 24 ÷àñà è âîçâðàùàåòñÿ íîâûé îáúåêò Time.
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


        // Áèíàðíûé îïåðàòîð +: Time + uint (äîáàâëåíèå ìèíóò)
        public static Time operator +(Time t, uint minutesToAdd)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t.AddMinutes(minutesToAdd);
        }

        // Áèíàðíûé îïåðàòîð + â äðóãîé ôîðìå: uint + Time
        public static Time operator +(uint minutesToAdd, Time t)
        {
            return t + minutesToAdd;
        }

        // Áèíàðíûé îïåðàòîð -: Time - uint (âû÷èòàíèå ìèíóò)
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

        // Óíàðíûé îïåðàòîð ++: äîáàâëåíèå îäíîé ìèíóòû
        public static Time operator ++(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t + 1U;
        }

        // Óíàðíûé îïåðàòîð --: âû÷èòàíèå îäíîé ìèíóòû
        public static Time operator --(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t - 1U;
        }

        // ßâíîå ïðèâåäåíèå ê òèïó byte – âîçâðàùàåò êîëè÷åñòâî ÷àñîâ (ìèíóòû îòáðàñûâàþòñÿ)
        public static explicit operator byte(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t.hours;
        }

        // Íåÿâíîå ïðèâåäåíèå ê òèïó bool – âîçâðàùàåò true, åñëè è ÷àñû è ìèíóòû íå ðàâíû íóëþ
        public static implicit operator bool(Time t)
        {
            return t != null && (t.hours != 0 || t.minutes != 0);
        }
    }
}
