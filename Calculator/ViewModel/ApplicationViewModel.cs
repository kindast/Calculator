using Calculator.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string calculationText;
        public string CalculationText
        {
            get { return calculationText; }
            set { calculationText = value; OnPropertyChanged(); }
        }

        private string resultText;
        public string ResultText
        {
            get { return resultText; }
            set { resultText = value; OnPropertyChanged(); }
        }

        private Calculation calculation;
        public ApplicationViewModel()
        {
            calculation = new Calculation();
        }

        private Command updateCommand;
        public Command UpdateCommand
        {
            get
            {
                return updateCommand ?? new Command((obj) =>
                {
                    calculation.UpdateText(obj.ToString());
                    CalculationText = calculation.CalculationText;
                    ResultText = calculation.ResultText;
                });
            }
        }
    }
    public class Command : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}