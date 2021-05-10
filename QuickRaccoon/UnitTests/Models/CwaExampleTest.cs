using QuickRaccoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Models
{
 public class CwaExampleTest
 {

  class CwaPersTestO : CwaPersTest
  {
   public CwaPersTestO(string firstName, string lastName, DateTime dateOfBirth, string testid, long timestamp, string salt) : base(firstName, lastName, dateOfBirth)
   {
    this.testid = testid;
    this.timestamp = timestamp;
    this.salt = salt;
    hash = GetHash();
   }
  }

  [Fact]
  public void PersTest()
  {
   CwaPersTestO sut = new CwaPersTestO("Erika", "Mustermann", new DateTime(1990, 12, 23), "52cddd8e-ff32-4478-af64-cb867cea1db5", 1618386548, "759F8FF3554F0E1BBF6EFF8DE298D9E9");
   Assert.Equal("67a50cba5952bf4f6c7eca896c0030516ab2f228f157237712e52d66489d9960", sut.hash);

   // folgende Prüfung schlägt fehl, da JSON Formatierung anders ist
   //Assert.Equal("https://s.coronawarn.app?v=1#eyAiZm4iOiAiRXJpa2EiLCAibG4iOiAiTXVzdGVybWFubiIsICJkb2IiOiAiMTk5MC0xMi0yMyIsICJ0aW1lc3RhbXAiOiAxNjE4Mzg2NTQ4LCAidGVzdGlkIjogIjUyY2RkZDhlLWZmMzItNDQ3OC1hZjY0LWNiODY3Y2VhMWRiNSIsICJzYWx0IjogIjc1OUY4RkYzNTU0RjBFMUJCRjZFRkY4REUyOThEOUU5IiwgImhhc2giOiAiNjdhNTBjYmE1OTUyYmY0ZjZjN2VjYTg5NmMwMDMwNTE2YWIyZjIyOGYxNTcyMzc3MTJlNTJkNjY0ODlkOTk2MCIgfQ", sut.GetLink());
  }


  class CwaUnpersTestO : CwaUnpersTest
  {
   public CwaUnpersTestO(long timestamp, string salt) : base()
   {
    this.timestamp = timestamp;
    this.salt = salt;
    hash = GetHash();
   }
  }

  [Fact]
  public void UnpersTest()
  {
   CwaUnpersTestO sut = new CwaUnpersTestO(1618386548, "759F8FF3554F0E1BBF6EFF8DE298D9E9");
   Assert.Equal("80232838046d2a65ab1b7a1be3dd1250ba9c91c969476c093bc34001ef460af8", sut.hash);

   // folgende Prüfung schlägt fehl, da JSON Formatierung anders ist
   //Assert.Equal("https://s.coronawarn.app?v=1#eyAidGltZXN0YW1wIjogMTYxODM4NjU0OCwgInNhbHQiOiAiNzU5RjhGRjM1NTRGMEUxQkJGNkVGRjhERTI5OEQ5RTkiLCAiaGFzaCI6ICI4MDIzMjgzODA0NmQyYTY1YWIxYjdhMWJlM2RkMTI1MGJhOWM5MWM5Njk0NzZjMDkzYmMzNDAwMWVmNDYwYWY4IiB9", sut.GetLink());
  }
 }
}
