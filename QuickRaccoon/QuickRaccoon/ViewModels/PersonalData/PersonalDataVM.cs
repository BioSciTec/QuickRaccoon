using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace QuickRaccoon.ViewModels.PersonalData
{
 public class PersonalDataVM : ErrorInfoBase
 {
  private readonly Action<PersonalData> _createAndShowQRCode;
  private readonly Action _cancelQRCodeGeneration;
  private readonly DateTime _minDate = new DateTime(1900, 1, 1);

  /// <summary>
  /// Vorname des Patienten
  /// </summary>
  public string FirstName
  {
   get => _firstName;
   set { _firstName = value; RaisePropertyChangedEvent(); }
  }
  private string _firstName;

  /// <summary>
  /// Nachname des Patienten
  /// </summary>
  public string LastName
  {
   get => _lastName;
   set { _lastName = value; RaisePropertyChangedEvent(); }
  }
  private string _lastName;

  /// <summary>
  /// Geburtsdatum des Patienten. Initialisiert auf "Heute"
  /// </summary>
  public DateTime DateOfBirth
  {
   get => _dateOfBirth;
   set { _dateOfBirth = value; RaisePropertyChangedEvent(); }
  }
  private DateTime _dateOfBirth = DateTime.Today;

  /// <summary>
  /// QR-Code-Erstellung abbrechen
  /// </summary>
  public ICommand CancelQRCodeCreationCommand => new DelegateCommand(OnCancelQRCodeCreation);
  private void OnCancelQRCodeCreation() => _cancelQRCodeGeneration();

  /// <summary>
  /// Dateneingabe beenden und in QR-Code Ansicht wechseln
  /// </summary>
  public ICommand DataEntryFinishedCommand => new DelegateCommand(OnDataEntryFinished);
  private void OnDataEntryFinished()
  {
   if ((!_allowValidation && GetErrors(null).Cast<string>().Any()) || HasErrors)//Prüfen, ob noch keine Validierung statt gefunden hat, oder Fehler vorliegen
   {
    _allowValidation = true;//Automatische Validierung erlauben
    RaisePropertyChangedEvent(nameof(FirstName));//GetError für Vorname auslösen
    RaisePropertyChangedEvent(nameof(LastName));//GetError für Nachname auslösen
    RaisePropertyChangedEvent(nameof(DateOfBirth));//GetError für Geburtsdatum auslösen
    return;//Command abbrechen
   }
   _createAndShowQRCode(new PersonalData(FirstName, LastName, DateOfBirth));//Alles okay => wechseln
  }

  /// <summary>
  /// Konstruktor
  /// </summary>
  /// <param name="createAndShowQRCode">Action um in QR-Code Ansicht zu wechseln</param>
  /// <param name="cancelQRCodeGeneration">Action um QR-Code Erstellung abzubrechen</param>
  public PersonalDataVM(Action<PersonalData> createAndShowQRCode, Action cancelQRCodeGeneration)
  {
   _createAndShowQRCode = createAndShowQRCode;
   _cancelQRCodeGeneration = cancelQRCodeGeneration;
  }

  /// <summary>
  /// Validierung des VM vornehmen
  /// </summary>
  /// <param name="propertyName">Zu prüfende Property. Wenn null, werden alle geprüft</param>
  /// <returns>Fehlermeldungen</returns>
  public override IEnumerable GetErrors([CallerMemberName] string propertyName = null)
  {
   if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(FirstName))
   {
    if (string.IsNullOrEmpty(FirstName))
     yield return "Firstname must not be empty";
   }
   if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(LastName))
   {
    if (string.IsNullOrEmpty(LastName))
     yield return "Lastname must not be empty";
   }
   if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(DateOfBirth))
   {
    if (DateOfBirth >= DateTime.Now)
     yield return "Date of birth must be in the past";
    if (DateOfBirth < _minDate)
     yield return string.Format("Date of birth must be after {0}!", _minDate.ToString());
   }
  }
 }

 public class PersonalData
 {
  public string FirstName { get; }

  public string LastName { get; }

  public DateTime DateOfBirth { get; }

  public PersonalData(string firstName, string lastName, DateTime dateOfBirth)
  {
   FirstName = firstName;
   LastName = lastName;
   DateOfBirth = dateOfBirth;
  }
 }
}
