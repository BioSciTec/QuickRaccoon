using System.Text.Json;

namespace QuickRaccoon.Models
{
 public class CwaUnpersTest : CwaBaseTest
 {
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
