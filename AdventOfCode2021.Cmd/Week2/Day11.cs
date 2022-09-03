using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day11
  {
    public Day11(string filename)
    {
      var data = Common.ReadFile.ReadLinesInTextFile(filename);

      var grid = new Day11Grid(10);
      var y = 0;
      foreach (var line in data)
      {
        var x = 0;
        var elements = line.ToCharArray();
        foreach (var element in elements)
        {
          grid.Levels[x][y] = int.Parse(element.ToString());
          x++;
        }
        y++;
      }

      Console.WriteLine("Initial Grid");
      PrintGrid(grid);
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
      Console.WriteLine("---- ---- ---- ----");
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
}
