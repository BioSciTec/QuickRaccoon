using QuickRaccoon.ViewModels.DataDecision;
using QuickRaccoon.ViewModels.PersonalData;
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
   ActiveView = new DataDecisionVM();
  }
 }
}
