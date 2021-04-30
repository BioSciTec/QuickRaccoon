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
  private Action<PersonalData> _createAndShowQRCode;

  public PersonalDataVM(Action<PersonalData> createAndShowQRCode)
  {
   _createAndShowQRCode = createAndShowQRCode;
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
