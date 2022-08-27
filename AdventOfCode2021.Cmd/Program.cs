using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2021.Cmd
{
  public class Program
  {
    static void Main(string[] args)
    {
      // Day 1
      // TODO Relative file path
      var day1File = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_test_input.txt");
      //var day1File = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_input.txt");
      DetermineNumberOfIncreases(day1File);
      DetermineNumberOfIncreasesUsingSlidingSum(day1File);
      Console.WriteLine("The end...");
    }

    public static int DetermineNumberOfIncreasesUsingSlidingSum(string[] file)
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
      var increases = DetermineNumberOfIncreasesInList(sumList);
      Console.WriteLine(sumList.Count + " sums were analyzed");
      Console.WriteLine(increases + " increases were found");

      return increases;
    }

    public static int DetermineNumberOfIncreases(string[] fileData)
    {
      var numberList = ConvertFileDataToNumberList(fileData);

      var increases = DetermineNumberOfIncreasesInList(numberList);

      Console.WriteLine(numberList.Count + " lines were read");
      Console.WriteLine(increases + " increases were found");

      return increases;
    }

    private static int DetermineNumberOfIncreasesInList(List<int> numberList)
    {
      var increaseCounter = 0;
      for (int i = 1; i < numberList.Count; i++)
      {
        if (numberList[i] > numberList[i - 1]) increaseCounter++;
      }
      return increaseCounter;
    }

    private static List<int> ConvertFileDataToNumberList(string[] fileData)
    {
      var numberList = new List<int>();
      foreach(var line in fileData)
      {
        var currentValue = 0;
        int.TryParse(line, out currentValue);
        numberList.Add(currentValue);
      }
      return numberList;
    }
  }
}
