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
  private readonly Action _startDecision;
  private readonly CwaBaseTest _qRSource;

  /// <summary>
  /// Der QR-Code als Bild
  /// </summary>
  public ImageSource QRCode
  {
   get => _qRCode;
   set { _qRCode = value; RaisePropertyChangedEvent(); }
  }
  private ImageSource _qRCode;

  /// <summary>
  /// Ohne drucken, wieder in Startansicht wechseln
  /// </summary>
  public ICommand CancelQRCodeCreationCommand => new DelegateCommand(OnCancelQRCodeCreation);
  private void OnCancelQRCodeCreation() => _startDecision();

  /// <summary>
  /// Ausdruck mit QR-Code für CWA und für Testkassette generieren
  /// </summary>
  public ICommand PrintQRCodeCommand => new DelegateCommand(OnPrintQRCode);
  private void OnPrintQRCode()
  {
   _qrCodeHasBeenPrinted = true;
   Report report = new(_qRSource);
   report.Print();
  }

  /// <summary>
  /// Nach Druck des Qr-Codes die Ansicht verlassen
  /// </summary>
  public ICommand QRCodeGenerationFinishedCommand => new DelegateCommand(OnQRCodeGenerationFinished);
  private void OnQRCodeGenerationFinished()
  {
   if (_qrCodeHasBeenPrinted)
    _startDecision();
  }

  /// <summary>
  /// Konstruktor
  /// </summary>
  /// <param name="startDecision">Action um wieder in Start-Ansicht zu gelangen</param>
  /// <param name="qrCode">Der erzeugte Qr-Code</param>
  public QRCodeVM(Action startDecision, CwaBaseTest qrCode)
  {
   _startDecision = startDecision;
   _qRSource = qrCode;
   CreateImage();
  }

  /// <summary>
  /// Aus QR-Code das Bild für die Anzeige generieren
  /// </summary>
  private void CreateImage()
  {
   using (MemoryStream memory = new())
   {
    _qRSource.GetCwaBarcodeImage().Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
    memory.Position = 0;
    BitmapImage bitmapimage = new();
    bitmapimage.BeginInit();
    bitmapimage.StreamSource = memory;
    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
    bitmapimage.EndInit();

    QRCode = bitmapimage;
   }
  }
 }
}
