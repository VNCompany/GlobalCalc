using System;
using System.Windows.Input;

namespace GlobalCalc.UI.Infrastructure;

class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object?>? _canExecute;
    
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public void Execute(object? parameter)
    {
        _execute.Invoke(parameter);
    }

    public bool CanExecute(object? parameter)
        => _canExecute?.Invoke(parameter) ?? true;
}