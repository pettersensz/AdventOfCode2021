using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1.Day1
{
  public class Day1
  {
    private string[] _fileData;
    public Day1(string filename)
    {
      var directory = Directory.GetCurrentDirectory();
      var path = Path.GetFullPath(Path.Combine(directory, @"..\..\..\..\inputData\" + filename));

      _fileData = File.ReadAllLines(path);
    }

    public int DetermineNumberOfIncreasesUsingSlidingSum()
    {
      var numberList = ConvertFileDataToNumberList(_fileData);
      var sumList = GetListWithSlidingSums(numberList, 3);

      var increases = DetermineNumberOfIncreasesInList(sumList);
      Console.WriteLine(sumList.Count + " sums were analyzed");
      Console.WriteLine(increases + " increases were found");

      return increases;
    }

    public int DetermineNumberOfIncreases()
    {
      var numberList = ConvertFileDataToNumberList(_fileData);

      var increases = DetermineNumberOfIncreasesInList(numberList);

      Console.WriteLine(numberList.Count + " lines were read");
      Console.WriteLine(increases + " increases were found");

      return increases;
    }

    private List<int> GetListWithSlidingSums(List<int> numberList, int interval)
    {
      var queue = new Queue<int>(interval);
      var sumList = new List<int>();
      var lineCounter = 0;
      foreach (var currentValue in numberList)
      {
        lineCounter++;
        if (lineCounter >= 3)
        {
          queue.Enqueue(currentValue);
          var sum = 0;
          foreach (var item in queue)
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
      return sumList;
    }

    private int DetermineNumberOfIncreasesInList(List<int> numberList)
    {
      var increaseCounter = 0;
      for (int i = 1; i < numberList.Count; i++)
      {
        if (numberList[i] > numberList[i - 1]) increaseCounter++;
      }
      return increaseCounter;
    }

    private List<int> ConvertFileDataToNumberList(string[] fileData)
    {
      var numberList = new List<int>();
      foreach (var line in fileData)
      {
        var currentValue = 0;
        int.TryParse(line, out currentValue);
        numberList.Add(currentValue);
      }
      return numberList;
    }
  }
}
