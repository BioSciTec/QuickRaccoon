using System;
using System.Windows.Input;

namespace QuickRaccoon.ViewModels.ViewModelCore
{
 public class DelegateCommand : ICommand
 {
  /// <summary>
  /// The action to be taken when command is fired
  /// </summary>
  private readonly Action _action;
  private readonly Predicate<object> _canExecute;

  /// <summary>
  /// A Constructor
  /// </summary>
  /// <param name="action"></param>
  public DelegateCommand(Action action) : this(action, null)
  {

  }

  public DelegateCommand(Action action, Predicate<object> canExecute)
  {
   _action = action;
   _canExecute = canExecute;
  }

  /// <summary>
  /// Execute the set action
  /// </summary>
  /// <param name="parameter"></param>
  public void Execute(object parameter)
  {
   _action();
  }

  /// <summary>
  /// Tells if the command can be executed for a parameter. Validation is possible but not wished here
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public bool CanExecute(object parameter)
  {
   return _canExecute == null || _canExecute(parameter);
  }

  /// <summary>
  /// CanExecute state changed
  /// </summary>
#pragma warning disable 67
  public event EventHandler CanExecuteChanged;
#pragma warning restore 67
 }

}
