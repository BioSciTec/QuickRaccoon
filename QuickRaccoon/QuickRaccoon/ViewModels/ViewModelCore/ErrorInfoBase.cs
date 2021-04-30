using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QuickRaccoon.ViewModels.ViewModelCore
{
 public abstract class ErrorInfoBase : NotifyableBase, INotifyDataErrorInfo
 {
  /// <summary>
  /// Flag, ob Validierung aktiv
  /// </summary>
  protected bool _allowValidation = false;

  /// <summary>
  /// Überschreibung für ProeprtyChangedEvent, um Validierung durch das erstmalige Setzen einer Property zu aktivieren
  /// </summary>
  /// <param name="propertyName">Die geänderte Property</param>
  protected override void RaisePropertyChangedEvent([CallerMemberName] string propertyName = "")
  {
   if (!_allowValidation && !string.IsNullOrEmpty(propertyName))
    _allowValidation = true;
   base.RaisePropertyChangedEvent(propertyName);
  }

  /// <summary>
  /// Event für changed error status
  /// </summary>
  public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

  protected virtual void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e) => ErrorsChanged?.Invoke(sender, e);

  /// <summary>
  /// Error status für PropertyChanged
  /// </summary>
  /// <param name="propertyName"></param>
  protected void OnErrorsChanged([CallerMemberName] string propertyName = null) => OnErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));

  /// <summary>
  /// Fehler vorhanden 
  /// </summary>
  public bool HasErrors => _allowValidation && GetErrors(null).OfType<object>().Any();


  /// <summary>
  /// Alle Fehler zu property zurückgeben. Bei null werden alle zurückgegeben. Diese Methode muss überschrieben werden, und mit der Validierungslogik für jede Property ausgestattet werden. (Z.B:: yield return "KitName must not be empty!";)
  /// </summary>
  /// <param name="propertyName"></param>
  /// <returns>Fehlerliste</returns>
  public virtual IEnumerable GetErrors([CallerMemberName] string propertyName = null) => Enumerable.Empty<object>();
 }

}
