using Calculator.Model;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculation calculation;
        public MainWindow()
        {
            InitializeComponent();
            calculation = new Calculation();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            calculation.UpdateText(button.Content.ToString());
            History.Text = calculation.CalculationText;
            Current.Text = calculation.ResultText;
        }
    }
}
