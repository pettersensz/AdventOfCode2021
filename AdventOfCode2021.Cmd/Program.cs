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
      //var day1File = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_test_input.txt");
      var day1File = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_input.txt");
      DetermineNumberOfIncreases(day1File);
      DetermineNumberOfIncreasesUsingSlidingSum(day1File);
      Console.WriteLine("The end...");
    }

    private static void DetermineNumberOfIncreasesUsingSlidingSum(string[] file)
    {
      var queue = new Queue<int>(3);
      var sumList = new List<int>();
      var currentValue = 0;
      var lineCounter = 0;
      foreach(var line in file)
      {
        lineCounter++;
        int.TryParse(line, out currentValue);
        if (lineCounter >= 3)
        {
          queue.Enqueue(currentValue);
          var sum = 0;
          foreach(var item in queue)
          {
            sum += item;
          }
          sumList.Add(sum);

          queue.Dequeue();
        }
        else
        {
          queue.Enqueue(currentValue);
        }
      }
      var increaseCounter = 0;
      for (int i = 1; i < sumList.Count; i++)
      {
        if (sumList[i] > sumList[i - 1]) increaseCounter++;
      }
      Console.WriteLine(sumList.Count + " sums were analyzed");
      Console.WriteLine(increaseCounter + " increases were found");
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
