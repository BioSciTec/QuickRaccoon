using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace QuickRaccoon.ViewModels.QRCode
{
 public class QRCodeVM : NotifyableBase
 {
  public ImageSource QRCode
  {
   get => _qRCode;
   set { _qRCode = value; RaisePropertyChangedEvent(); }
  }
  private ImageSource _qRCode;

  public ICommand PrintQRCodeCommand => new DelegateCommand(OnPrintQRCode);
  private void OnPrintQRCode()
  {
   throw new NotImplementedException();
  }
 }
}
