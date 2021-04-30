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
  public ICommand ContinueWithoutPersonalDataCommand => new DelegateCommand(OnContinueWithoutPersonalData);
  private void OnContinueWithoutPersonalData()
  {
   throw new NotImplementedException();
  }

  public ICommand ContinueWithPersonalDataCommand => new DelegateCommand(OnContinueWithPersonalData);
  private void OnContinueWithPersonalData()
  {
   throw new NotImplementedException();
  }
 }
}
