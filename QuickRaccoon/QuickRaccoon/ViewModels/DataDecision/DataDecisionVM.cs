using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuickRaccoon.ViewModels.DataDecision
{
 public class DataDecisionVM : NotifyableBase
 {
  private Action<PersonalData.PersonalData> _createAndShowQRCode;
  private Action _startPersonalDataEntry;

  public ICommand ContinueWithoutPersonalDataCommand => new DelegateCommand(OnContinueWithoutPersonalData);
  private void OnContinueWithoutPersonalData() => _createAndShowQRCode(null);

  public ICommand ContinueWithPersonalDataCommand => new DelegateCommand(OnContinueWithPersonalData);
  private void OnContinueWithPersonalData() => _startPersonalDataEntry();

  public DataDecisionVM(Action<PersonalData.PersonalData> createAndShowQRCode, Action startPersonalDataEntry)
  {
   _createAndShowQRCode = createAndShowQRCode;
   _startPersonalDataEntry = startPersonalDataEntry;
  }
 }
}
