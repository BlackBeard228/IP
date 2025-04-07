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
                    throw new ArgumentOutOfRangeException(nameof(value), "���� ������ ���� � ��������� �� 0 �� 23.");
                hours = value;
            }
        }

        public byte Minutes
        {
            get => minutes;
            set
            {
                if (value > 59)
                    throw new ArgumentOutOfRangeException(nameof(value), "������ ������ ���� � ��������� �� 0 �� 59.");
                minutes = value;
            }
        }

        // ����������� � �����������
        public Time(byte hours, byte minutes)
        {
            if (hours > 23)
                throw new ArgumentOutOfRangeException(nameof(hours), "���� ������ ���� � ��������� �� 0 �� 23.");
            if (minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), "������ ������ ���� � ��������� �� 0 �� 59.");

            this.hours = hours;
            this.minutes = minutes;
        }

        // ����������� �����������
        public Time(Time other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "���������� ����������� ������, ������ null.");
            hours = other.hours;
            minutes = other.minutes;
        }

        // ��������������� ������ ToString() ��� ������ ������� � ������� ��:��
        public override string ToString()
        {
            return $"{hours:D2}:{minutes:D2}";
        }

        /// <summary>
        /// ����� ���������� ������������� ���������� ����� (uint) � �������� �������.
        /// ��������� �������������� �� ������ 24 ���� � ������������ ����� ������ Time.
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


        // �������� �������� +: Time + uint (���������� �����)
        public static Time operator +(Time t, uint minutesToAdd)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t.AddMinutes(minutesToAdd);
        }

        // �������� �������� + � ������ �����: uint + Time
        public static Time operator +(uint minutesToAdd, Time t)
        {
            return t + minutesToAdd;
        }

        // �������� �������� -: Time - uint (��������� �����)
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

        // ������� �������� ++: ���������� ����� ������
        public static Time operator ++(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t + 1U;
        }

        // ������� �������� --: ��������� ����� ������
        public static Time operator --(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t - 1U;
        }

        // ����� ���������� � ���� byte � ���������� ���������� ����� (������ �������������)
        public static explicit operator byte(Time t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            return t.hours;
        }

        // ������� ���������� � ���� bool � ���������� true, ���� � ���� � ������ �� ����� ����
        public static implicit operator bool(Time t)
        {
            return t != null && (t.hours != 0 || t.minutes != 0);
        }
    }
}