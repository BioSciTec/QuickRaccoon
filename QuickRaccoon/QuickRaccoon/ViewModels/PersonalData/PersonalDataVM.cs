using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
  private DateTime _dateOfBirth;


  public override IEnumerable GetErrors([CallerMemberName] string propertyName = null)
  {
   if (propertyName == nameof(FirstName) && string.IsNullOrEmpty(FirstName))
    yield return "Firstname must not be empty";

   if (propertyName == nameof(LastName) && string.IsNullOrEmpty(LastName))
    yield return "Lastname must not be empty";

   if (propertyName == nameof(DateOfBirth) && DateOfBirth >= DateTime.Now)
    yield return "Date of birth must be in the past";

   yield return base.GetErrors(propertyName);
  }

 }
}
