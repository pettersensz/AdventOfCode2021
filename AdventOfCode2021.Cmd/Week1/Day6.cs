using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  public class Day6
  {
    private List<LanternFish> _fishList;
    private long[] _fishArray;
    public Day6(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);
      _fishList = CreateInitialFishListFromInput(fileData);

      // Fish can have age 0, 1, 2, 3, 4, 5, 6, 7, 8
      _fishArray = new long[9];
      FillInitialFishArray(fileData);
    }

    private void FillInitialFishArray(string[] fileData)
    {
      foreach (var s in fileData)
      {
        var elements = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var element in elements)
        {
          var age = int.Parse(element);
          _fishArray[age] += 1;
        }
      }
      
    }

    private List<LanternFish> CreateInitialFishListFromInput(string[] fileData)
    {
      var fishList = new List<LanternFish>();
      foreach (var s in fileData)
      {
        var elements = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var element in elements)
        {
          fishList.Add(new LanternFish(int.Parse(element)));
        }
      }

      return fishList;
    }

    public void SimulateFishGrowth(int daysToSimulate)
    {
      // TODO need more efficient solution for 256 days
      Console.Write("Initial state:  ");
      PrintFishTimerValues();

      for (var day = 1; day < daysToSimulate + 1; day++)
      {
        var addCounter = 0;
        foreach (var fish in _fishList)
        {
          bool addNewFish = fish.Age();
          if (addNewFish) addCounter++;
        }

        for (var i = 0; i < addCounter; i++)
        {
          var newFish = new LanternFish(8);
          _fishList.Add(newFish);
        }

        Console.Write("After ");
        if (day == 1) Console.Write("  " + day + " day:  ");
        else if (day < 10) Console.Write("  " + day + " days: ");
        else if (day < 100) Console.Write(" " + day + " days: ");
        else Console.Write(day + " days: ");
        //PrintFishTimerValues();
        Console.Write("\n");
        Console.WriteLine("Total number of fish: " + _fishList.Count);
      }
      Console.WriteLine("Total number of fish: " + _fishList.Count);
    }

    private void PrintFishTimerValues()
    {
      foreach (var fish in _fishList)
      {
        if (_fishList.IndexOf(fish) == _fishList.Count - 1) Console.Write(fish.Timer);
        else Console.Write(fish.Timer + ",");
      }
      Console.Write("\n");
    }

    public void SimulateFishGrowthEfficient(int daysToSimulate)
    {
      for (var day = 1; day < daysToSimulate + 1; day++)
      {
        var oldArray = _fishArray;
        var newArray = new long[9];
        for (var i = 8; i > 0; i--)
        {
          newArray[i - 1] = oldArray[i];
        }

        newArray[6] += oldArray[0];
        newArray[8] = oldArray[0];

        _fishArray = newArray;

        Console.Write("After ");
        if (day == 1) Console.Write("  " + day + " day:  ");
        else if (day < 10) Console.Write("  " + day + " days: ");
        else if (day < 100) Console.Write(" " + day + " days: ");
        else Console.Write(day + " days: ");
        //foreach (var element in _fishArray)    
        //{
        //  Console.Write(element + ", ");
        //}

        long sum = 0;
        foreach (var i in _fishArray)
        {
          sum += i;
        }
        Console.Write("Sum: " + sum);
        Console.Write("\n");
      }
    }
  }

  public class LanternFish
  {
    public bool Fresh { get; set; }
    public int Timer { get; set; }

    public LanternFish(int timer)
    {
      Timer = timer;
      Fresh = true;
    }

    public bool Age()
    {
      if (Timer == 0)
      {
        Timer = 6;
        Fresh = false;
        return true;
      }
      Timer--;
      return false;
    }
  }
}
