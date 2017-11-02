using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Show_First_Symbol
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Clear();

      DateTime dat = DateTime.Now;

      Console.WriteLine("\nToday is {0:d} at {0:T}.", dat);
      Console.Write("\nEnter any string: ");
      string s = null;
      s = Console.ReadLine();
      string res = string.IsNullOrEmpty(s) ? null : s[0].ToString();
      Console.WriteLine("Your string is: {0} \nThank you =)", s[0]);
    }
  }
}
