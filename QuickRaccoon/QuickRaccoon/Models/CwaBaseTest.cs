using QRCoder;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace QuickRaccoon.Models
{
 public abstract class CwaBaseTest
 {
  public long timestamp { get; protected set; }
  public string salt { get; protected set; }
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

  protected abstract string GetJson();

  public string GetLink()
  {
   return "https://s.coronawarn.app?v=1#" + Convert.ToBase64String(Encoding.UTF8.GetBytes(GetJson()));
  }

  public Bitmap GetCwaBarcodeImage()
  {
   QRCodeGenerator qrGenerator = new QRCodeGenerator();
   QRCodeData qrCodeData = qrGenerator.CreateQrCode(GetLink(), QRCodeGenerator.ECCLevel.Q);
   QRCode qrCode = new QRCode(qrCodeData);
   return qrCode.GetGraphic(20);
  }

  public Bitmap GetCwaTestIdBarcodeImage()
  {
   QRCodeGenerator qrGenerator = new QRCodeGenerator();
   QRCodeData qrCodeData = qrGenerator.CreateQrCode(hash, QRCodeGenerator.ECCLevel.Q);
   QRCode qrCode = new QRCode(qrCodeData);
   return qrCode.GetGraphic(20, Color.Black, Color.White, false);
  }
 }
}
