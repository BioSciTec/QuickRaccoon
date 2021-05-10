using System.Text.Json;

namespace QuickRaccoon.Models
{
 /// <summary>
 /// Dient der Erstellung von QR-Codes für die Integration von Schnelltests mit ohne persönlichen Daten
 /// </summary>
 public class CwaUnpersTest : CwaBaseTest
 {
  /// <summary>
  /// Standard Konstruktor.
  /// </summary>
  public CwaUnpersTest()
  {
   timestamp = GetTimeStamp();
   salt = GetSalt();
   hash = GetHash();
  }

  protected override string GetHashContent()
  {
   return timestamp.ToString() + '#' + salt;
  }

  protected override string GetJson()
  {
   return JsonSerializer.Serialize(this);//, new JsonSerializerOptions() { WriteIndented = true }).Replace("\r\n", "").Replace("  ", " ").Replace("}", " }");
  }
 }
}
