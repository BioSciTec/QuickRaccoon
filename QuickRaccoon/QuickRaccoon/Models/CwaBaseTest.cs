using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickRaccoon.Models
{
 public class CwaBaseTest
 {
  protected long timestamp { get; set; }
  protected string salt { get; set; }
  protected string hash { get; set; }

  public CwaBaseTest()
  {
   timestamp = GetTimeStamp();
   salt = GetSalt();
   hash = GetHash();
  }

  protected long GetTimeStamp()
  {
   return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
  }

  protected string GetSalt()
  {
   var salt = new byte[32];
   using (var random = new RNGCryptoServiceProvider())
    random.GetNonZeroBytes(salt);
   return BitConverter.ToString(salt).Replace("-", "");
  }

  protected virtual string GetHashContent()
  {
   return timestamp.ToString() + '#' + salt;
  }

  protected string GetHash()
  {
   var hash = new byte[256];
   using (SHA256 sha256Hash = SHA256.Create())
    sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(GetHashContent()));
   return BitConverter.ToString(hash).Replace("-", "").ToLower();
  }

  public string GetLink()
  {
   return "https://s.coronawarn.app?v=1#" + Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this)));
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
   return qrCode.GetGraphic(20);
  }
 }
}
