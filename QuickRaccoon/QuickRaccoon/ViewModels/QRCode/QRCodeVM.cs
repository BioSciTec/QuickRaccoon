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
  private bool _qrCodeHasBeenPrinted = false;

  public ImageSource QRCode
  {
   get => _qRCode;
   set { _qRCode = value; RaisePropertyChangedEvent(); }
  }
  private ImageSource _qRCode;
  private Action _startDecision;

  public ICommand PrintQRCodeCommand => new DelegateCommand(OnPrintQRCode);
  private void OnPrintQRCode()
  {
   _qrCodeHasBeenPrinted = true;
  }

  public ICommand QRCodeGenerationFinishedCommand => new DelegateCommand(OnQRCodeGenerationFinished);
  private void OnQRCodeGenerationFinished()
  {
   if (_qrCodeHasBeenPrinted)
    _startDecision();
  }

  public QRCodeVM(Action startDecision)
  {
   _startDecision = startDecision;
  }
 }
}
