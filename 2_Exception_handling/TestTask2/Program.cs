using System;
using StringToIntParser;

namespace TestTask2
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Write("\nEnter any string: ");
      string str = Console.ReadLine();
      Console.WriteLine("Result: " + StringToInt.StrToInt(str));
    }
  }
}