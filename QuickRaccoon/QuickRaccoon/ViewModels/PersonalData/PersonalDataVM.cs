using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuickRaccoon.ViewModels.PersonalData
{
 public class PersonalDataVM : ErrorInfoBase
 {
  private Action<PersonalData> _createAndShowQRCode;
  private Action _cancelQRCodeGeneration;

  public string FirstName
  {
   get => _firstName;
   set { _firstName = value; RaisePropertyChangedEvent(); }
  }
  private string _firstName;

  public string LastName
  {
   get => _lastName;
   set { _lastName = value; RaisePropertyChangedEvent(); }
  }
  private string _lastName;

  public DateTime DateOfBirth
  {
   get => _dateOfBirth;
   set { _dateOfBirth = value; RaisePropertyChangedEvent(); }
  }
  private DateTime _dateOfBirth = DateTime.Today;

  public ICommand CancelQRCodeCreationCommand => new DelegateCommand(OnCancelQRCodeCreation);
  private void OnCancelQRCodeCreation()
  {
   _cancelQRCodeGeneration();
  }

  public ICommand DataEntryFinishedCommand => new DelegateCommand(OnDataEntryFinished);
  private void OnDataEntryFinished()
  {
   if ((!_allowValidation && GetErrors(null).Cast<string>().Any()) || HasErrors)
   {
    _allowValidation = true;
    RaisePropertyChangedEvent(nameof(FirstName));
    RaisePropertyChangedEvent(nameof(LastName));
    RaisePropertyChangedEvent(nameof(DateOfBirth));
    return;
   }
   _createAndShowQRCode(new PersonalData(FirstName, LastName, DateOfBirth));
  }

  public PersonalDataVM(Action<PersonalData> createAndShowQRCode, Action cancelQRCodeGeneration)
  {
   _createAndShowQRCode = createAndShowQRCode;
   _cancelQRCodeGeneration = cancelQRCodeGeneration;
  }

  private DateTime _minDate = new DateTime(1900, 1, 1);
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
