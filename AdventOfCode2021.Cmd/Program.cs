using System;
using System.IO;

namespace AdventOfCode2021.Cmd
{
  class Program
  {
    static void Main(string[] args)
    {
      // Day 1
      DetermineNumberOfIncreases();
      Console.WriteLine("The end...");
    }

    private static void DetermineNumberOfIncreases()
    {
      var file = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_test_input.txt");
      //var file = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_input.txt");
      var lineCounter = 0;
      var increaseCounter = 0;
      var currentValue = 0;
      var previousValue = 0;
      foreach(var line in file)
      {
        lineCounter++;
        int.TryParse(line, out currentValue);
        if (lineCounter != 1)
        {
          if (currentValue > previousValue) increaseCounter++;
        }

        previousValue = currentValue;
      }

      Console.WriteLine(lineCounter + " lines were read");
      Console.WriteLine(increaseCounter + " increases were found");
    }
  }
}
