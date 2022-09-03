using System;
using System.Collections.Generic;
using System.Linq;
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
          List<int> neighborList = GetNeighborListForPoint(x, y);

          //Console.Write($"({y},{x}), value: {element}. neighbors: ");
          foreach (var number in neighborList)
          {
            //Console.Write(number + ", ");
          }
          bool lowest = DetermineIfLowestNumber(element, neighborList);
          if (lowest)
          {
            //Console.Write("This is lower than all neighbors");
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
        Console.WriteLine("-----");
      }
      Console.WriteLine("Sum of risk values: " + sum);

      IdentifyThreeLargestBasins(basinList);

    }

    private void IdentifyThreeLargestBasins(List<Basin> basinList)
    {
      var ordered = basinList.OrderByDescending(b => b.Points.Count);
      var top3 = ordered.Take(3);
      var product = 1;
      foreach (var basin in top3)
      {
        product *= basin.Points.Count;
      }
      Console.WriteLine("Basin Size Product = " + product);

    }

    private Basin DetermineBasinSize(MapPoint lowPoint)
    {
      Console.WriteLine("New basin around " + lowPoint.GetKeyString());
      var basin = new Basin()
      {
        Points = new Dictionary<string, MapPoint>()
      };
      basin.AddPoint(lowPoint);
      //Console.WriteLine("Point: " + lowPoint);
      var neighbors = GetBasinNeighborListForPoint(lowPoint);
      foreach (var neighbor in neighbors)
      {
        AddNeighborsRecursive(basin, neighbor);
      }
      Console.WriteLine("Basin size: " + basin.GetSize());
      return basin;
    }

    private void AddNeighborsRecursive(Basin basin, MapPoint neighbor)
    {
      List<MapPoint> neighbors;
      basin.AddPoint(neighbor);
      //Console.WriteLine("neighbor: " + neighbor);
      neighbors = GetBasinNeighborListForPoint(neighbor);
      foreach (var newneighbor in neighbors)
      {
        if (basin.Contains(newneighbor)) continue;
        AddNeighborsRecursive(basin, newneighbor);
      }
    }

    private List<MapPoint> GetBasinNeighborListForPoint(MapPoint point)
    {
      var neighborList = new List<MapPoint>();
      // All rows until last row can add number below
      if (point.Y < _yMax - 1) 
      {
        var x = point.X;
        var y = point.Y + 1;
        CheckHeightAndAddIfBelow9(y, x, neighborList);
      }
      // All columns until last column can add number after
      if (point.X < _xMax - 1)
      {
        var x = point.X + 1;
        var y = point.Y;
        CheckHeightAndAddIfBelow9(y, x, neighborList);
      }
      // All columns after first column can add number before
      if (point.X > 0)
      {
        var x = point.X - 1;
        var y = point.Y;
        CheckHeightAndAddIfBelow9(y, x, neighborList);
      }
      // All rows after first can add number above
      if (point.Y > 0)
      {
        var x = point.X;
        var y = point.Y - 1;
        CheckHeightAndAddIfBelow9(y, x, neighborList);
      }

      return neighborList;
    }

    private void CheckHeightAndAddIfBelow9(int y, int x, List<MapPoint> neighborList)
    {
      var height = _array[y, x];
      var mapPoint = new MapPoint(x, y, height);
      //Console.Write("Identified neighbor: " + mapPoint + ". ");
      if (height < 9) neighborList.Add(mapPoint);
      //else Console.Write("Not added due to height 9");
      //Console.Write("\n");
    }

    private List<int> GetNeighborListForPoint(int x, int y)
    {
      var neighborList = new List<int>();
      // All rows until last row can add number below
      if (y < _yMax - 1) neighborList.Add(_array[y + 1, x]);
      // All columns until last column can add number after
      if (x < _xMax - 1) neighborList.Add(_array[y, x + 1]);
      // All columns after first column can add numver before
      if (x > 0) neighborList.Add(_array[y, x - 1]);
      // All rows after first can add number above
      if (y > 0) neighborList.Add(_array[y - 1, x]);
      return neighborList;
    }

    private bool DetermineIfLowestNumber(int element, List<int> neighborList)
    {
      var lowest = true;
      foreach(var number in neighborList)
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
      return Y + "," + X;
    }
  }

  public class Basin
  {
    public Dictionary<string, MapPoint> Points;

    public bool AddPoint(MapPoint point)
    {
      var keyString = point.GetKeyString();
      if (Points.ContainsKey(keyString))
      {
        //Console.WriteLine(keyString + " not added because it's already added earlier.");
        return false;
      }
      Points.Add(keyString, point);
      //Console.WriteLine(keyString + " added.");
      return true;
    }

    public bool Contains(MapPoint point)
    {
      var keyString = point.GetKeyString(); ;
      if (Points.ContainsKey(keyString)) return true;
      return false;
    }

    public string GetSize()
    {
      return Points.Count.ToString();
    }

  }
}
