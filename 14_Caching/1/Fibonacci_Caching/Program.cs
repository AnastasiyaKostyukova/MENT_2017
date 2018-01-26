using System;
using System.Runtime.Caching;

namespace Fibonacci_Caching
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("The Fibonacci numbers sequence:\n 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, etc.");
      while (true)
      {
        Console.WriteLine("\nEnter index of the Fibonacci number. To end the program click \"e\"");
        var value = Console.ReadLine();

        if (value == "e") 
        {
          break;
        }

        int index;
        try
        {
          index = Convert.ToInt32(value);
          if (index <= 0)
          {
            throw new Exception("Can't use negative index");
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          continue;
        }

        Console.WriteLine($"Entered Fibonacci index = {index}; result: {GetFibonacciNumber(index)}");
        Console.WriteLine();
      }
    }

    private static int GetFibonacciNumber(int index)
    {
      var cache = MemoryCache.Default;

      var previous = 0;
      var result = 1;

      cache.AddOrGetExisting("1", 1, DateTimeOffset.Now.AddMinutes(5));

      for (var i = 2; i <= index; i++)
      {
        var newResult = result + previous;
        previous = result;
        Console.WriteLine(
          cache.AddOrGetExisting(i.ToString(), newResult, DateTimeOffset.Now.AddMinutes(5)) == null
            ? $"Index {i} NOT exists. Added value: {newResult}"
            : $"Index {i} exists. Value is {newResult}");
        result = (int)(cache.AddOrGetExisting(i.ToString(), newResult, DateTimeOffset.Now.AddMinutes(5)) ?? newResult);
      }

      return result;
    }
  }
}