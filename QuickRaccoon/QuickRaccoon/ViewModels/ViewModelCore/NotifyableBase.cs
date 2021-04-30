using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuickRaccoon.ViewModels.ViewModelCore
{
 public abstract class NotifyableBase : INotifyPropertyChanged
 {
  /// <summary>
  /// PropertyChanged Event
  /// </summary>
  public event PropertyChangedEventHandler PropertyChanged;

  /// <summary>
  /// This method is called by the Set accessor of each property.  
  /// The CallerMemberName attribute that is applied to the optional propertyName  
  /// parameter causes the property name of the caller to be substituted as an argument.
  /// </summary>
  /// <param name="propertyName"></param>
  protected virtual void RaisePropertyChangedEvent([CallerMemberName] string propertyName = "")
  {
   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
 }

}
