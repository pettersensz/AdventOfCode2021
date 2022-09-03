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

      var grid = new Day11Grid();
      var y = 0;
      foreach (var line in data)
      {
        var x = 0;
        var elements = line.ToCharArray();
        foreach (var element in elements)
        {
          grid.GridValues.Add(x.ToString() + "," + y.ToString(), int.Parse(element.ToString()));
          x++;
        }
        y++;
      }
    }


  }

  public class Day11Grid
  {
    public Dictionary<string, int> GridValues;

    public Day11Grid()
    {
      GridValues = new Dictionary<string, int>();
    }

  }
}
