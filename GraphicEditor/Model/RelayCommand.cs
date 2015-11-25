using System;
using System.Windows.Input;

namespace GraphicEditor.Model
{
    public class RelayCommand : ICommand
    {
        private Action<object> f_execute;

        private Predicate<object> f_canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException(nameof(canExecute));
            }

            f_execute = execute;
            f_canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return f_canExecute != null && f_canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            f_execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChangedInternal;
            //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
            handler?.Invoke(this, EventArgs.Empty);
        }

        public void Destroy()
        {
            f_canExecute = _ => false;
            f_execute = _ => { };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
