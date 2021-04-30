using QuickRaccoon.Models;
using QuickRaccoon.ViewModels.DataDecision;
using QuickRaccoon.ViewModels.PersonalData;
using QuickRaccoon.ViewModels.QRCode;
using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRaccoon.ViewModels.MainWindow
{
 public class MainVM : NotifyableBase
 {
  public NotifyableBase ActiveView
  {
   get => _activeView;
   set
   {
    _activeView = value;
    RaisePropertyChangedEvent();
   }
  }
  private NotifyableBase _activeView;

  public MainVM()
  {
   StartDecision();
  }

  private void StartDecision()
  {
   ActiveView = new DataDecisionVM(CreateAndShowQRCode, StartPersonalDataEntry);
  }

  private void StartPersonalDataEntry()
  {
   ActiveView = new PersonalDataVM(CreateAndShowQRCode);
  }

  private void CreateAndShowQRCode(PersonalData.PersonalData personalData = null)
  {
   CwaBaseTest qrCode;
   if (personalData != null)
    qrCode = new CwaPersTest(personalData.FirstName, personalData.LastName, personalData.DateOfBirth);
   else
    qrCode = new CwaBaseTest();
    ActiveView = new QRCodeVM(StartDecision, qrCode);
  }
 }
}
