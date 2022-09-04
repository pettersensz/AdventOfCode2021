using System;
using System.Collections.Generic;
using System.Text;

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
          //_grid.Levels[x][y] = int.Parse(element.ToString());
          _dict.AddPoint(x, y, int.Parse(element.ToString()));
          x++;
        }
        y++;
      }

      Console.WriteLine("Initial Grid");
      //PrintGrid(_grid);
      PrintDict(_dict);
    }

    private void PrintDict(Day11Dict dict)
    {
      for (var y = 0; y < _gridSize; y++)
      {
        for (var x = 0; x < _gridSize; x++)
        {
          Console.Write(dict.GetPoint(x,y));
        }
        Console.Write("\n");
      }
    }

    private void PrintGrid(Day11Grid grid)
    {
      for (var y = 0; y < grid.Levels.Length; y++)
      {
        for (var x = 0; x < grid.Levels[y].Length; x++)
        {
          Console.Write(grid.Levels[x][y]);
        }
        Console.Write("\n");
      }
      //Console.WriteLine("---- ---- ---- ----");
    }

    public void DoSteps(int numberOfSteps)
    {
      for (var step = 1; step <= numberOfSteps; step++)
      {
        // Do initial step up
        var numberOfPops = 0;
        for (var y = 0; y < _gridSize; y++)
        {
          for (var x = 0; x < _gridSize; x++)
          {
            var oldValue = _dict.GetPoint(x,y);
            var newValue = oldValue + 1;
            if (newValue > 9)
            {
              newValue = 0;
              numberOfPops++;
            }

            _grid.Levels[x][y] = newValue;
          }
        }
        Console.WriteLine("After initial step:");
        PrintGrid(_grid);

        var totalPops = numberOfPops;
        // Increase neighbors
        while (numberOfPops > 0)
        {
          numberOfPops = IncreaseNeigborValues();
          totalPops += numberOfPops;
        }

        Console.WriteLine($"After step {step}:");
        PrintGrid(_grid);
        Console.WriteLine("Number of pops: " + totalPops);
      }
    }

    private int CountNumberOfPops()
    {
      var numberOfPops = 0;
      for (var y = 0; y < _grid.Levels.Length; y++)
      {
        for (var x = 0; x < _grid.Levels[y].Length; x++)
        {
          var value = _grid.Levels[x][y];
          if (value > 9)
          {
            _grid.Levels[x][y] = 0;
            numberOfPops++;
          }
        }
      }

      return numberOfPops;
    }

    private int IncreaseNeigborValues()
    {
      // Increase all neighbors for all zeroes
      for (var y = 0; y < _grid.Levels.Length; y++)
      {
        for (var x = 0; x < _grid.Levels[y].Length; x++)
        {
          var oldValue = _grid.Levels[x][y];
          if (oldValue == 0)
          {
            if (x < _grid.Levels[y].Length - 1) if (_grid.Levels[x + 1][y] != 0) _grid.Levels[x + 1][y] += 1;
            if (x > 0) if (_grid.Levels[x-1][y] != 0) _grid.Levels[x - 1][y] += 1;
            if (y > 0) if(_grid.Levels[x][y - 1] != 0) _grid.Levels[x][y - 1] += 1;
            if (y < _grid.Levels.Length - 1) if (_grid.Levels[x][y + 1] != 0) _grid.Levels[x][y + 1] += 1;
            if (x > 0 && y > 0) if(_grid.Levels[x - 1][y - 1] != 0) _grid.Levels[x - 1][y - 1] += 1;
            if (x < _grid.Levels[y].Length - 1 && y < _grid.Levels.Length - 1) if(_grid.Levels[x + 1][y + 1] != 0) _grid.Levels[x + 1][y + 1] += 1;
            if (x > 0 && y < _grid.Levels.Length - 1) if(_grid.Levels[x - 1][y + 1] != 0) _grid.Levels[x - 1][y + 1] += 1;
            if (x < _grid.Levels[y].Length - 1 && y > 0) if(_grid.Levels[x + 1][y - 1] != 0) _grid.Levels[x + 1][y - 1] += 1;
          }
        }
      }

      var numberOfPops = CountNumberOfPops();
      return numberOfPops;
    }
  }

  public class Day11Grid
  {
    public int[][] Levels;

    public Day11Grid(int size)
    {
      Levels = new int[size][];
      for (var i = 0; i < size; i++)
      {
        Levels[i] = new int[size];
      }
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

    private string GetCoordString(int x, int y)
    {
      return $"({x}, {y})";
    }
  }

}
