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
  /// <summary>
  /// Das gerade  angezeigte ViewModel
  /// </summary>
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

  /// <summary>
  /// Öffnet die Start Ansicht( Auswahl ob mit oder ohne persönlichen Daten
  /// </summary>
  private void StartDecision()
  {
   ActiveView = new DataDecisionVM(CreateAndShowQRCode, StartPersonalDataEntry);
  }

  /// <summary>
  /// Persönliche Dateneingabe starten
  /// </summary>
  private void StartPersonalDataEntry()
  {
   ActiveView = new PersonalDataVM(CreateAndShowQRCode, StartDecision);
  }

  /// <summary>
  /// QR-Code Anzeige starten
  /// </summary>
  /// <param name="personalData">Ggf. Die persönlichen Daten</param>
  private void CreateAndShowQRCode(PersonalData.PersonalData personalData = null)
  {
   CwaBaseTest qrCode;
   if (personalData != null)
    qrCode = new CwaPersTest(personalData.FirstName, personalData.LastName, personalData.DateOfBirth);
   else
    qrCode = new CwaUnpersTest();
   ActiveView = new QRCodeVM(StartDecision, qrCode);
  }
 }
}
