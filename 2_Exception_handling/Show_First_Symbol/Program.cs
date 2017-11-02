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
      DateTime date = DateTime.Now;

      Console.WriteLine("\nToday is {0:d} at {0:T}.", date);

      Console.Write("\n1. Enter any string: ");
      var str1 = Console.ReadLine();
      var res = string.IsNullOrEmpty(str1) ? null : str1[0].ToString();
      Console.WriteLine("Your string is: {0} \nThank you =)", res);

      Console.WriteLine("\n\n2. Enter any string.");
      var str2 = Console.ReadLine();
      try
      {
        Console.WriteLine(str2[0]);
      }
      catch (IndexOutOfRangeException)
      {
        Console.WriteLine("You entered empty string.");
      }
    }
  }
}
