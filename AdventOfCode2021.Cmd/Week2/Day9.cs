using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week2
{
    public class Day9
  {
    private readonly int[,] _array;
    private readonly int _xMax;
    private readonly int _yMax;
    public Day9(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);

      _xMax = fileData[0].Length;
      _yMax = fileData.Length;

      var array = new int[_yMax, _xMax];

      for(var row = 0; row < fileData.Length; row++)
      {
        var rowData = fileData[row];
        var charArray = rowData.ToCharArray();
        for(var element = 0; element < charArray.Length; element++)
        {
          var number = int.Parse(charArray[element].ToString());
          array[row, element] = number;
        }
      }
      _array = array;


    }

    internal void CalculateSumOfRiskLevels()
    {
      // Identify low points
      var lowPoints = new List<MapPoint>();
      for(var x = 0; x < _xMax; x++)
      {
        for (var y = 0; y < _yMax; y++)
        {
          var element = _array[y, x];
          List<int> neighbourList = GetNeighbourListForPoint(x, y);

          //Console.Write($"({y},{x}), value: {element}. Neighbours: ");
          foreach (var number in neighbourList)
          {
            //Console.Write(number + ", ");
          }
          bool lowest = DetermineIfLowestNumber(element, neighbourList);
          if (lowest)
          {
            //Console.Write("This is lower than all neighbours");
            var point = new MapPoint(x, y, element);
            lowPoints.Add(point);
          }
          //Console.Write("\n");
        }
      }
      var sum = 0;
      var basinList = new List<Basin>();
      foreach (var lowPoint in lowPoints)
      {
        sum += lowPoint.Height + 1;
        basinList.Add(DetermineBasinSize(lowPoint));
      }
      Console.WriteLine("Sum of risk values: " + sum);

    }

    private Basin DetermineBasinSize(MapPoint lowPoint)
    {
      Console.WriteLine("New basin around " + lowPoint.GetKeyString());
      var basin = new Basin()
      {
        Points = new Dictionary<string, MapPoint>()
      };
      basin.AddPoint(lowPoint);
      Console.WriteLine("Point: " + lowPoint.ToString());
      var neighbours = GetBasinNeighbourListForPoint(lowPoint);
      foreach (var neighbour in neighbours)
      {
        AddNeighboursRecursive(basin, neighbour);
      }
      return basin;
    }

    private void AddNeighboursRecursive(Basin basin, MapPoint neighbour)
    {
      List<MapPoint> neighbours;
      basin.AddPoint(neighbour);
      Console.WriteLine("Neighbour: " + neighbour.ToString());
      neighbours = GetBasinNeighbourListForPoint(neighbour);
      foreach (var newNeighbour in neighbours)
      {
        if (basin.Contains(newNeighbour)) continue;
        AddNeighboursRecursive(basin, newNeighbour);
      }
    }

    private List<MapPoint> GetBasinNeighbourListForPoint(MapPoint point)
    {
      var neighbourList = new List<MapPoint>();
      // All rows until last row can add number below
      if (point.Y < _yMax - 1) 
      {
        var x = point.X;
        var y = point.Y + 1;
        var height = _array[y, x];
        if (height < 8) neighbourList.Add(new MapPoint(x, y, height));
      }
      // All columns until last column can add number after
      if (point.X < _xMax - 1)
      {
        var x = point.X + 1;
        var y = point.Y;
        var height = _array[y, x];
        if (height < 8) neighbourList.Add(new MapPoint(x, y, height));
      }
      // All columns after first column can add number before
      if (point.X > 0)
      {
        var x = point.X - 1;
        var y = point.Y;
        var height = _array[y, x];
        if (height < 8) neighbourList.Add(new MapPoint(x, y, height));
      }
      // All rows after first can add number above
      if (point.Y > 0)
      {
        var x = point.X;
        var y = point.Y - 1;
        var height = _array[y, x];
        if (height < 8) neighbourList.Add(new MapPoint(x, y, height));
      }

      return neighbourList;
    }

    private List<int> GetNeighbourListForPoint(int x, int y)
    {
      var neighbourList = new List<int>();
      // All rows until last row can add number below
      if (y < _yMax - 1) neighbourList.Add(_array[y + 1, x]);
      // All columns until last column can add number after
      if (x < _xMax - 1) neighbourList.Add(_array[y, x + 1]);
      // All columns after first column can add numver before
      if (x > 0) neighbourList.Add(_array[y, x - 1]);
      // All rows after first can add number above
      if (y > 0) neighbourList.Add(_array[y - 1, x]);
      return neighbourList;
    }

    private bool DetermineIfLowestNumber(int element, List<int> neighbourList)
    {
      var lowest = true;
      foreach(var number in neighbourList)
      {
        if (number <= element) lowest = false;
      }
      return lowest;
    }
  }

  public class MapPoint
  {
    public int X;
    public int Y;
    public int Height;

    public MapPoint(int x, int y, int height)
    {
      X = x;
      Y = y;
      Height = height;
    }

    public override string ToString()
    {
      return "(" + GetKeyString() + ") " + Height;
    }

    public string GetKeyString()
    {
      return Y.ToString() + "," + X.ToString();
    }
  }

  public class Basin
  {
    public Dictionary<string, MapPoint> Points;

    public bool AddPoint(MapPoint point)
    {
      var keyString = point.GetKeyString();
      if (Points.ContainsKey(keyString)) return false;
      Points.Add(keyString, point);
      return true;
    }

    public bool Contains(MapPoint point)
    {
      var keyString = point.GetKeyString(); ;
      if (Points.ContainsKey(keyString)) return true;
      return false;
    }

  }
}
