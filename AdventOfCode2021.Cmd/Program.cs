using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2021.Cmd
{
  class Program
  {
    static void Main(string[] args)
    {
      // Day 1
      var day1File = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_test_input.txt");
      //var day1File = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_input.txt");
      DetermineNumberOfIncreases(day1File);
      DetermineNumberOfIncreasesUsingSlidingSum(day1File);
      Console.WriteLine("The end...");
    }

    private static void DetermineNumberOfIncreasesUsingSlidingSum(string[] file)
    {
      var lineCounter = 0;
      var sumList = new List<int>();
      var valueNow = 0;
      var value1before = 0;
      var value2before = 0;
      var currentSum = 0;
      foreach(var line in file)
      {
        int.TryParse(line, out valueNow);
        lineCounter++;
        if (lineCounter == 1) value2before = valueNow;
        else if (lineCounter == 2) value1before = valueNow;
        else if (lineCounter == 3)
        {
          currentSum = valueNow + value1before + value2before;
          sumList.Add(currentSum);
        }
        else
        {
          // TODO sums are not correct...
          currentSum = currentSum - value2before + valueNow;
          sumList.Add(currentSum);
          value2before = value1before;
          value1before = valueNow;
        }
      }
      foreach(var sum in sumList)
      {
        Console.WriteLine(sum);
      }

    }

    private static void DetermineNumberOfIncreases(string[] file)
    {
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
