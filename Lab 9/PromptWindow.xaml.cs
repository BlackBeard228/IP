using System.Windows;

namespace TimeWpfApp
{
    public partial class PromptWindow : Window
    {
        public string Value { get { return tb.Text; } }

        public PromptWindow(string caption)
        {
            InitializeComponent();
            lbl.Text = caption;
            tb.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
