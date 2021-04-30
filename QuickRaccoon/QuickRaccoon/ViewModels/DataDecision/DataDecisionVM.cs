using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Windows.Input;

namespace QuickRaccoon.ViewModels.DataDecision
{
 public class DataDecisionVM : NotifyableBase
 {
  private readonly Action<PersonalData.PersonalData> _createAndShowQRCode;
  private readonly Action _startPersonalDataEntry;

  /// <summary>
  /// Erstellt einen QR-Code ohne Patientendaten
  /// </summary>
  public ICommand ContinueWithoutPersonalDataCommand => new DelegateCommand(OnContinueWithoutPersonalData);
  private void OnContinueWithoutPersonalData() => _createAndShowQRCode(null);

  /// <summary>
  /// Wechselt in die Dateneingabe
  /// </summary>
  public ICommand ContinueWithPersonalDataCommand => new DelegateCommand(OnContinueWithPersonalData);
  private void OnContinueWithPersonalData() => _startPersonalDataEntry();

  /// <summary>
  /// Konstruktor
  /// </summary>
  /// <param name="createAndShowQRCode">Action um in QR-Code Ansicht zu wechseln</param>
  /// <param name="startPersonalDataEntry">Action um Dateneingabe zu starten</param>
  public DataDecisionVM(Action<PersonalData.PersonalData> createAndShowQRCode, Action startPersonalDataEntry)
  {
   _createAndShowQRCode = createAndShowQRCode;
   _startPersonalDataEntry = startPersonalDataEntry;
  }
 }
}
