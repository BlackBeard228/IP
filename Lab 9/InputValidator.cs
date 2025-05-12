using System.Globalization;
using System.Windows;

namespace TimeWpfApp.Validators
{
    internal sealed class InputValidator
    {
        public bool TryByte(string txt, byte min, byte max, out byte v)
        {
            return byte.TryParse(txt, NumberStyles.None,
                                 CultureInfo.InvariantCulture, out v)
                   && v >= min && v <= max;
        }

        public bool TryUInt(string txt, out uint v)
        {
            return uint.TryParse(txt, NumberStyles.None,
                                 CultureInfo.InvariantCulture, out v);
        }

        public void Error(string msg)
        {
            MessageBox.Show(msg, "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
