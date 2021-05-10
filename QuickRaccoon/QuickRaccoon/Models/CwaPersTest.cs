using System;
using System.Text.Json;

namespace QuickRaccoon.Models
{
 /// <summary>
 /// Dient der Erstellung von QR-Codes für die Integration von Schnelltests mit persönlichen Daten
 /// </summary>
 public class CwaPersTest : CwaBaseTest
 {
  /// <summary>
  /// Vorname der zu testenden Person
  /// </summary>
  public string fn { get; protected set; }

  /// <summary>
  /// Nachname der zu testenden Person
  /// </summary>
  public string ln { get; protected set; }

  /// <summary>
  /// Geburtsdatum der zu testenden Person im Format YYYY-MM-DD mit fester Länge von 10 Zeichen (Beispiel: 2000-01-01)
  /// </summary>
  public string dob { get; protected set; }

  /// <summary>
  /// Test-Datum/Uhrzeit im Unix Epoch Timestamp Format (Sekunden) (Hier mit new einfügt um Reihenfolge des offiziellen Beispiels zu erfüllen)
  /// </summary>
  public new long timestamp { get; protected set; }

  /// <summary>
  /// generierte UUID Typ 4
  /// </summary>
  public string testid { get; protected set; }

  /// <summary>
  /// Standard Konstruktor
  /// </summary>
  /// <param name="firstName">Vorname der zu testenden Person</param>
  /// <param name="lastName">Nachname der zu testenden Person</param>
  /// <param name="dateOfBirth">Geburtsdatum der zu testenden Person</param>
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
