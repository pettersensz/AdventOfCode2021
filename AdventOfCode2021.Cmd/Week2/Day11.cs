using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day11
  {
    private readonly Day11Dict _dict;
    private readonly int _gridSize;
    public Day11(string filename, int gridSize = 10)
    {
      var data = Common.ReadFile.ReadLinesInTextFile(filename);

      _gridSize = gridSize;
      _dict = new Day11Dict();
      var y = 0;
      foreach (var line in data)
      {
        var x = 0;
        var elements = line.ToCharArray();
        foreach (var element in elements)
        {
          _dict.AddPoint(x, y, int.Parse(element.ToString()));
          x++;
        }
        y++;
      }

      Console.WriteLine("Initial Grid");
      PrintDict();
    }

    private void PrintDict()
    {
      for (var y = 0; y < _gridSize; y++)
      {
        for (var x = 0; x < _gridSize; x++)
        {
          var value = _dict.GetPoint(x, y);
          if (value == 0) Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(value);
          Console.ForegroundColor = ConsoleColor.White;
        }
        Console.Write("\n");
      }
    }


    public void DoSteps(int numberOfSteps)
    {
      var totalPops = 0;
      for (var step = 1; step <= numberOfSteps; step++)
      {
        // Do initial step up
        var numberOfPops = 0;
        for (var y = 0; y < _gridSize; y++)
        {
          for (var x = 0; x < _gridSize; x++)
          {
            var oldValue = _dict.GetPoint(x, y);
            var newValue = oldValue + 1;
            if (newValue > 9)
            {
              newValue = 0;
              numberOfPops++;
            }

            _dict.UpdatePoint(x, y, newValue);
          }
        }
        //Console.WriteLine("After initial step:");
        //PrintDict();

        var popsInStep = numberOfPops;
        // Increase neighbors
        while (numberOfPops > 0)
        {
          numberOfPops = IncreaseNeigborValues();
          popsInStep += numberOfPops;
          //Console.WriteLine("POP");
          //PrintDict();
        }

        ResetPoppedPoints();

        Console.WriteLine($"After step {step}:");
        PrintDict();
        Console.WriteLine("Number of pops: " + popsInStep);
        totalPops += popsInStep;
      }
      Console.WriteLine("Total Pops: " + totalPops);
    }

    private void ResetPoppedPoints()
    {
      for (var y = 0; y < _gridSize; y++)
      {
        for (var x = 0; x < _gridSize; x++)
        {
          if (_dict.GetPoint(x, y) == -1)
          {
            _dict.IncreasePoint(x, y);
          }
        }
      }
    }

    private int CountNumberOfPops()
    {
      var numberOfPops = 0;
      for (var y = 0; y < _gridSize; y++)
      {
        for (var x = 0; x < _gridSize; x++)
        {
          var value = _dict.GetPoint(x, y);
          if (value > 9)
          {
            _dict.UpdatePoint(x, y, 0);
            numberOfPops++;
          }
          else if (value == 0)
          {
            // Set already popped points to -1 so they don't pop neighbors multiple times
            _dict.UpdatePoint(x, y, -1);
          }
        }
      }

      return numberOfPops;
    }

    private int IncreaseNeigborValues()
    {
      // Increase all neighbors for all zeroes
      for (var y = 0; y < _gridSize; y++)
      {
        for (var x = 0; x < _gridSize; x++)
        {
          var oldValue = _dict.GetPoint(x, y);
          if (oldValue == 0)
          {
            if (x < _gridSize - 1 && !_dict.PointHasPopped(x + 1, y)) _dict.IncreasePoint(x + 1, y);
            if (x > 0 && !_dict.PointHasPopped(x - 1, y)) _dict.IncreasePoint(x - 1, y);
            if (y > 0 && !_dict.PointHasPopped(x, y - 1)) _dict.IncreasePoint(x, y - 1);
            if (y < _gridSize - 1 && !_dict.PointHasPopped(x, y + 1)) _dict.IncreasePoint(x, y + 1);
            if (x > 0 && y > 0 && !_dict.PointHasPopped(x - 1, y - 1)) _dict.IncreasePoint(x - 1, y - 1);
            if (x < _gridSize - 1 && y < _gridSize - 1 && !_dict.PointHasPopped(x + 1, y + 1)) _dict.IncreasePoint(x + 1, y + 1);
            if (x > 0 && y < _gridSize - 1 && !_dict.PointHasPopped(x - 1, y + 1)) _dict.IncreasePoint(x - 1, y + 1);
            if (x < _gridSize - 1 && y > 0 && !_dict.PointHasPopped(x + 1, y - 1)) _dict.IncreasePoint(x + 1, y - 1);
          }
        }
      }

      var numberOfPops = CountNumberOfPops();
      return numberOfPops;
    }
  }

  public class Day11Dict
  {
    public Dictionary<string, int> Levels;

    public Day11Dict()
    {
      Levels = new Dictionary<string, int>();
    }

    public bool AddPoint(int x, int y, int value)
    {
      var coords = GetCoordString(x, y);
      if (Levels.ContainsKey(coords)) return false;
      Levels.Add(coords, value);
      return true;
    }

    public int GetPoint(int x, int y)
    {
      var key = GetCoordString(x, y);
      if (Levels.ContainsKey(key)) return Levels[key];
      return -1;
    }

    public bool UpdatePoint(int x, int y, int value)
    {
      var key = GetCoordString(x, y);
      if (!Levels.ContainsKey(key)) return false;
      Levels[key] = value;
      return true;
    }

    public bool IncreasePoint(int x, int y)
    {
      var key = GetCoordString(x, y);
      if (!Levels.ContainsKey(key)) return false;
      var newValue = Levels[key] + 1;
      Levels[key] = newValue;
      return true;
    }

    public bool PointHasPopped(int x, int y)
    {
      var key = GetCoordString(x, y);
      if (Levels[key] == 0) return true;
      if (Levels[key] == -1) return true;
      return false;
    }

    private string GetCoordString(int x, int y)
    {
      return $"({x}, {y})";
    }
  }

}
