using System;
using System.Text.Json;

namespace QuickRaccoon.Models
{
 public class CwaPersTest : CwaBaseTest
 {
  public string fn { get; protected set; }
  public string ln { get; protected set; }
  public string dob { get; protected set; }
  public new long timestamp { get; protected set; }
  public string testid { get; protected set; }

  public CwaPersTest(string firstName, string lastName, DateTime dateOfBirth)
  {
   fn = firstName;
   ln = lastName;
   dob = dateOfBirth.ToString("yyyy-MM-dd");
   testid = GetTestId();
   timestamp = GetTimeStamp();
   salt = GetSalt();
   hash = GetHash();
  }

  protected string GetTestId()
  {
   return Guid.NewGuid().ToString();
  }

  protected override string GetHashContent()
  {
   return dob + '#' + fn + '#' + ln + '#' + timestamp.ToString() + '#' + testid + '#' + salt;
  }

  protected override string GetJson()
  {
   return JsonSerializer.Serialize(this);//, new JsonSerializerOptions() { WriteIndented = true }).Replace("\r\n", "").Replace("  ", " ").Replace("}", " }");
  }
 }
}
