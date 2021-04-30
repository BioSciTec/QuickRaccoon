using QuickRaccoon.Models;
using QuickRaccoon.ViewModels.ViewModelCore;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuickRaccoon.ViewModels.QRCode
{

 public class QRCodeVM : NotifyableBase
 {
  private bool _qrCodeHasBeenPrinted = false;
  private Action _startDecision;
  private CwaBaseTest _qRSource;

  public ImageSource QRCode
  {
   get => _qRCode;
   set { _qRCode = value; RaisePropertyChangedEvent(); }
  }
  private ImageSource _qRCode;

  public ICommand CancelQRCodeCreationCommand => new DelegateCommand(OnCancelQRCodeCreation);
  private void OnCancelQRCodeCreation()
  {
   _startDecision();
  }

  public ICommand PrintQRCodeCommand => new DelegateCommand(OnPrintQRCode);
  private void OnPrintQRCode()
  {
   _qrCodeHasBeenPrinted = true;
   Report report = new Report(_qRSource);
   report.Print();
  }

  public ICommand QRCodeGenerationFinishedCommand => new DelegateCommand(OnQRCodeGenerationFinished);
  private void OnQRCodeGenerationFinished()
  {
   if (_qrCodeHasBeenPrinted)
    _startDecision();
  }

  public QRCodeVM(Action startDecision, CwaBaseTest qrCode)
  {
   _startDecision = startDecision;
   _qRSource = qrCode;
   CreateImage();
  }

  private void CreateImage()
  {
   using (MemoryStream memory = new MemoryStream())
   {
    _qRSource.GetCwaBarcodeImage().Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
    memory.Position = 0;
    BitmapImage bitmapimage = new BitmapImage();
    bitmapimage.BeginInit();
    bitmapimage.StreamSource = memory;
    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
    bitmapimage.EndInit();

    QRCode = bitmapimage;
   }
  }
 }
}
