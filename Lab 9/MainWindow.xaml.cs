using System;
using System.Windows;
using TimeWpfApp.Validators;

namespace TimeWpfApp
{
    public partial class MainWindow : Window
    {
        private readonly InputValidator _v = new InputValidator();
        private Time _time = null;   // ещё не задано

        public MainWindow()
        {
            InitializeComponent();
        }

        /* ---------- ввод ---------- */
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            byte h, m;
            if (!_v.TryByte(tbHours.Text, 0, 23, out h))
            { _v.Error("Часы 0–23"); return; }
            if (!_v.TryByte(tbMinutes.Text, 0, 59, out m))
            { _v.Error("Минуты 0–59"); return; }

            _time = new Time(h, m);
            Log("Установлено " + _time);
        }

        /* ---------- операции ---------- */
        private void Show_Click(object s, RoutedEventArgs e) { if (Ok()) Log(_time.ToString()); }
        private void Copy_Click(object s, RoutedEventArgs e) { if (Ok()) Log("Копия " + new Time(_time)); }

        private void Add_Click(object s, RoutedEventArgs e)
        {
            uint v;
            if (!Ok()) return;
            if (!Ask("Сколько минут добавить?", out v)) return;
            _time = _time + v;
            Log("+" + v + " → " + _time);
        }
        private void Sub_Click(object s, RoutedEventArgs e)
        {
            uint v;
            if (!Ok()) return;
            if (!Ask("Сколько минут вычесть?", out v)) return;
            _time = _time - v;
            Log("-" + v + " → " + _time);
        }

        private void Inc_Click(object s, RoutedEventArgs e) { if (Ok()) { _time = ++_time; Log("++ → " + _time); } }
        private void Dec_Click(object s, RoutedEventArgs e) { if (Ok()) { _time = --_time; Log("-- → " + _time); } }
        private void CastByte_Click(object s, RoutedEventArgs e) { if (Ok()) Log("(byte) → " + (byte)_time); }
        private void CastBool_Click(object s, RoutedEventArgs e) { if (Ok()) Log("(bool) → " + (_time ? "true" : "false")); }

        /* ---------- вспомогательные ---------- */
        private bool Ok()
        {
            if (_time != null) return true;
            _v.Error("Сначала задайте время."); return false;
        }

        private void Log(string msg)
        {
            log.Text = DateTime.Now.ToLongTimeString() + " " + msg +
                       Environment.NewLine + log.Text;
        }

        private bool Ask(string caption, out uint value)
        {
            PromptWindow dlg = new PromptWindow(caption) { Owner = this };
            if (dlg.ShowDialog() == true && _v.TryUInt(dlg.Value, out value)) return true;
            _v.Error("Введите неотрицательное целое.");
            value = 0;
            return false;
        }
    }
}
