using QRCoder;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace QuickRaccoon.Models
{
 /// <summary>
 /// Basis-Klasse für die Erstellung von QR-Codes für die Integration von Schnelltests
 /// </summary>
 public abstract class CwaBaseTest
 {
  /// <summary>
  /// Test-Datum/Uhrzeit im Unix Epoch Timestamp Format (Sekunden) (Hier mit new einfügt um Reihenfolge des offiziellen Beispiels zu erfüllen)
  /// </summary>
  public long timestamp { get; protected set; }

  /// <summary>
  /// Generierte 128-Bit Zufallszahl in Hexadezimal-Darstellung, nur mit Großbuchstaben und fester Breite von 32 Stellen
  /// </summary>
  public string salt { get; protected set; }

  /// <summary>
  /// "CWA Test ID" (SHA256-Hashwert für alle anderen Attribute des Objekts)
  /// </summary>
  public string hash { get; protected set; }

  protected long GetTimeStamp()
  {
   return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
  }

  protected string GetSalt()
  {
   var salt = new byte[16];
   using (var random = new RNGCryptoServiceProvider())
    random.GetNonZeroBytes(salt);
   return BitConverter.ToString(salt).Replace("-", "");
  }

  protected abstract string GetHashContent();

  protected string GetHash()
  {
   byte[] hash;
   using (SHA256 sha256Hash = SHA256.Create())
    hash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(GetHashContent()));
   return BitConverter.ToString(hash).Replace("-", "").ToLower();
  }

  /// <summary>
  /// Gibt die letzten 6 Zeichen von "CWA Test ID" (hash) zurück
  /// </summary>
  /// <returns></returns>
  public string GetHashLast6()
  {
   return hash.Substring(58);
  }

  protected abstract string GetJson();

  /// <summary>
  /// Generiert den CWA Link.
  /// </summary>
  /// <returns>CWA Link</returns>
  public string GetLink()
  {
   return "https://s.coronawarn.app?v=1#" + Convert.ToBase64String(Encoding.UTF8.GetBytes(GetJson()));
  }

  /// <summary>
  /// Generiert QR-Code mit dem Link für die CWA-App
  /// </summary>
  /// <returns>QR-Code als Bild</returns>
  public Bitmap GetCwaBarcodeImage()
  {
   QRCodeGenerator qrGenerator = new QRCodeGenerator();
   QRCodeData qrCodeData = qrGenerator.CreateQrCode(GetLink(), QRCodeGenerator.ECCLevel.L);
   QRCode qrCode = new QRCode(qrCodeData);
   return qrCode.GetGraphic(20);
  }

  /// <summary>
  /// Generiert QR-Code mit der "CWA Test ID" (hash)
  /// </summary>
  /// <returns>QR-Code als Bild</returns>
  public Bitmap GetCwaTestIdBarcodeImage()
  {
   QRCodeGenerator qrGenerator = new QRCodeGenerator();
   QRCodeData qrCodeData = qrGenerator.CreateQrCode(hash, QRCodeGenerator.ECCLevel.L);
   QRCode qrCode = new QRCode(qrCodeData);
   return qrCode.GetGraphic(20);//, Color.Black, Color.White, false);
  }
 }
}
