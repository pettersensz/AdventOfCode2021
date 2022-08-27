using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  public class Day3
  {
    private readonly string[] _fileData;

    public Day3(string filename)
    {
      _fileData = Common.ReadFile.ReadLinesInTextFile(filename);
    }

    public int DeterminePowerConsumption()
    {
      var uncommonBit = 0;
      var commonBinaryNumber = "";
      var uncommonBinaryNumber = "";
      for (var i = 0; i < _fileData[0].Length; i++)
      {
        var commonBit = DetermineMostCommonBit(i);
        if (commonBit == 0) uncommonBit = 1;
        if (commonBit == 1) uncommonBit = 0;

        commonBinaryNumber += commonBit.ToString();
        uncommonBinaryNumber += uncommonBit.ToString();
      }
      Console.WriteLine(commonBinaryNumber);
      var value = Convert.ToInt32(commonBinaryNumber, 2);
      Console.WriteLine(value);

      Console.WriteLine(uncommonBinaryNumber);
      var uncommonValue = Convert.ToInt32(uncommonBinaryNumber, 2);
      Console.WriteLine(uncommonValue);

      Console.WriteLine("Power Consumption");
      var powerConsumption = value * uncommonValue;
      Console.WriteLine(powerConsumption);
      return powerConsumption;
    }

    public int DetermineLifeSupportRating()
    {
      Console.WriteLine("Day 3 Part 2 Life Support Rating");
      var oxygenGeneratorRating = DetermineRating(RatingType.OxygenGenerator);
      var scrubberRating = DetermineRating(RatingType.Scrubber);
      
      var lifeSupportRating = oxygenGeneratorRating * scrubberRating;
      Console.WriteLine("Life Support Rating: " + lifeSupportRating);
      return lifeSupportRating;
    }
    
    private enum RatingType
    {
      OxygenGenerator,
      Scrubber
    }

    private int DetermineBitForArrayReduction(int zeroCount, int oneCount, RatingType ratingType)
    {
      if (ratingType == RatingType.Scrubber)
      {
        return zeroCount <= oneCount ? 0 : 1;
      }

      if (ratingType == RatingType.OxygenGenerator)
      {
        return zeroCount > oneCount ? 0 : 1;
      }

      return 9;
    }

    private int DetermineRating(RatingType ratingType)
    {
      var workArray = _fileData;
      for (var i = 0; i < _fileData[0].Length; i++)
      {
        var zeroCount = DetermineBitCountAtIndex("0", i, workArray);
        var oneCount = DetermineBitCountAtIndex("1", i, workArray);
        var valueToKeep = DetermineBitForArrayReduction(zeroCount, oneCount, ratingType);
        workArray = GetUpdatedArray(workArray, valueToKeep, i);
        Console.WriteLine("Index " + i + ", keeping values with value: " + valueToKeep + ", result:");
        foreach (var s in workArray)
        {
          Console.WriteLine(s);
        }

        if (workArray.Length != 1) continue;
        Console.WriteLine("Only one number left");
        Console.WriteLine(workArray[0]);
        break;
      }
      var rating = Convert.ToInt32(workArray[0], 2);
      Console.WriteLine("Rating: " + rating);
      return rating;
    }

    private string[] GetUpdatedArray(string[] inputData, int mostCommonBit, int index)
    {
      var tempList = new List<string>();
      foreach(var line in inputData)
      {
        if (line.Substring(index, 1) == mostCommonBit.ToString()) tempList.Add(line);
      }
      var newArray = tempList.ToArray();
      return newArray;
    }

    private int DetermineMostCommonBit(int index)
    {
      var zeroCount = DetermineBitCountAtIndex("0", index, _fileData);
      var oneCount = DetermineBitCountAtIndex("1", index, _fileData);

      return zeroCount > oneCount ? 0 : 1;
    }

    private int DetermineBitCountAtIndex(string bitAsString, int index, string[] dataLines)
    {
      var count = 0;
      foreach(var line in dataLines)
      {
        if (line.Substring(index, 1) == bitAsString) count++;
      }
      return count;
    }
  }
}
