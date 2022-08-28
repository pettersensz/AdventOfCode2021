using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  public class Day5
  {
    private readonly List<Line> _lines;
    public Day5(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);
      _lines = new List<Line>();
      foreach (var textLine in fileData)
      {
        var coords = textLine.Split("->");
        var startCoordinate = GetCoordinate(coords[0]);
        var endCoordinate = GetCoordinate(coords[1]);

        var line = new Line(startCoordinate, endCoordinate);
        _lines.Add(line);
      }

      // Todo Assign values based on size of input data
      var xMax = 1000;
      var yMax = 1000;

      var grid = new Grid(xMax, yMax);

      AssignValuesToGrid(grid);

      //PrintGrid(grid);

      var result = CountCoordinatesWithMultipleLines(grid);
      Console.WriteLine(result + " cells have more than 1 line coordinate");

    }

    private int CountCoordinatesWithMultipleLines(Grid grid)
    {
      var counter = 0;
      foreach (var row in grid.Rows)
      {
        foreach (var element in row)
        {
          if (element > 1) counter++;
        }
      }

      return counter;
    }

    private void AssignValuesToGrid(Grid grid)
    {
      foreach (var line in _lines)
      {
        if(line.LineType == LineType.Diagonal) continue;
        foreach (var coordinate in line.Coordinates)
        {
          var currentValue = grid.Rows[coordinate.Y][coordinate.X];
          var newValue = currentValue + 1;
          grid.Rows[coordinate.Y][coordinate.X] = newValue;
        }
      }
    }

    private void PrintGrid(Grid grid)
    {
      foreach (var gridRow in grid.Rows)
      {
        foreach (var element in gridRow)
        {
          if(element == 0) Console.Write(".");
          else Console.Write(element);
        }
        Console.Write("\n");
      }
      Console.WriteLine("---------");
    }

    private Coordinate GetCoordinate(string xCommaYasString)
    {
      
      var parts = xCommaYasString.Split(',', StringSplitOptions.RemoveEmptyEntries);
      var startX = int.Parse(parts[0]);
      var startY = int.Parse(parts[1]);
      var coordinate = new Coordinate(startX, startY);
      return coordinate;
    }
  }

  public class Coordinate
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
      X = x;
      Y = y;
    }
  }

  public class Line
  {
    public Coordinate StartCoordinate { get; }
    public Coordinate EndCoordinate { get; }
    public LineType LineType { get; set; }
    public List<Coordinate> Coordinates { get; }

    public Line(Coordinate startCoordinate, Coordinate endCoordinate)
    {
      StartCoordinate = startCoordinate;
      EndCoordinate = endCoordinate;
      Coordinates = new List<Coordinate>();
      if (startCoordinate.X == endCoordinate.X)
      {
        AddVerticalCoordinates(startCoordinate, endCoordinate);
      }
      else if (startCoordinate.Y == endCoordinate.Y)
      {
        AddHorizontalCoordinates(startCoordinate, endCoordinate);
      }
      else
      {
        LineType = LineType.Diagonal;
      }
    }

    private void AddHorizontalCoordinates(Coordinate startCoordinate, Coordinate endCoordinate)
    {
      LineType = LineType.Horizontal;
      Coordinates.Add(startCoordinate);
      if (startCoordinate.X < endCoordinate.X)
      {
        for (var x = startCoordinate.X + 1; x < endCoordinate.X; x++)
        {
          var coordinate = new Coordinate(x, startCoordinate.Y);
          Coordinates.Add(coordinate);
        }
      }
      else if (startCoordinate.X > endCoordinate.X)
      {
        for (var x = startCoordinate.X - 1; x > endCoordinate.X; x--)
        {
          var coordinate = new Coordinate(x, startCoordinate.Y);
          Coordinates.Add(coordinate);
        }
      }
      Coordinates.Add(endCoordinate);
    }

    private void AddVerticalCoordinates(Coordinate startCoordinate, Coordinate endCoordinate)
    {
      LineType = LineType.Vertical;
      Coordinates.Add(startCoordinate);
      if (startCoordinate.Y < endCoordinate.Y)
      {
        for (var y = startCoordinate.Y + 1; y < endCoordinate.Y; y++)
        {
          var coordinate = new Coordinate(startCoordinate.X, y);
          Coordinates.Add(coordinate);
        }
      }
      else if (startCoordinate.Y > endCoordinate.Y)
      {
        for (var y = startCoordinate.Y - 1; y > endCoordinate.Y; y--)
        {
          var coordinate = new Coordinate(startCoordinate.X, y);
          Coordinates.Add(coordinate);
        }
      }
      Coordinates.Add(endCoordinate);
    }
  }

  public enum LineType
  {
    Horizontal,
    Vertical,
    Diagonal
  }

  public class Grid
  {
    public List<int[]> Rows { get; }

    public Grid(int x, int y)
    {
      Rows = new List<int[]>();
      for (var i = 0; i < y; i++)
      {
        var row = new int[x];
        for (var j = 0; j < row.Length; j++)
        {
          row[j] = 0;
        }
        Rows.Add(row);
      }
    }
  }
}
