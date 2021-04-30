using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRaccoon.Models
{
 public class CwaPersTest : CwaBaseTest
 {
  protected string fn { get; set; }
  protected string ln { get; set; }
  protected string dob { get; set; }
  protected string testid { get; set; }

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
 }
}
