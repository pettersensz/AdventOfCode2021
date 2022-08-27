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

    public void DeterminePowerConsumption()
    {
      var numberOfBits = _fileData[0].Length;
      var uncommonBit = 0;
      var commonBinaryNumber = "";
      var uncommonBinaryNumber = "";
      for (var i = 0; i < numberOfBits; i++)
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
    }

    internal void DetermineLifeSupportRating()
    {
      Console.WriteLine("Day 3 Part 2 Life Support Rating");
      var oxygenGeneratorRating = DetermineOxygenGeneratorRating();
      var scrubberRating = DetermineScrubberRating();
      
      var lifeSupportRating = oxygenGeneratorRating * scrubberRating;
      Console.WriteLine("Life Support Rating: " + lifeSupportRating);
    }

    private int DetermineScrubberRating()
    {
      var workArray = _fileData;
      for (var i = 0; i < _fileData[0].Length; i++)
      {
        var zeroCount = DetermineBitCountAtIndex("0", i, workArray);
        var oneCount = DetermineBitCountAtIndex("1", i, workArray);
        var leastCommonBit = zeroCount <= oneCount ? 0 : 1;
        workArray = GetUpdatedArray(workArray, leastCommonBit, i);
        Console.WriteLine("Index " + i + ", least common bit: " + leastCommonBit + ", result:");
        foreach (var s in workArray)
        {
          Console.WriteLine(s);
        }

        if (workArray.Length != 1) continue;
        Console.WriteLine("Only one number left");
        Console.WriteLine(workArray[0]);
        break;
      }
      var scrubberRating = Convert.ToInt32(workArray[0], 2);
      Console.WriteLine("CO2 Scrubber Rating: " + scrubberRating);
      return scrubberRating;
    }

    private int DetermineOxygenGeneratorRating()
    {
      var workArray = _fileData;
      for (var i = 0; i < _fileData[0].Length; i++)
      {
        var zeroCount = DetermineBitCountAtIndex("0", i, workArray);
        var oneCount = DetermineBitCountAtIndex("1", i, workArray);
        var mostCommonBit = zeroCount > oneCount ? 0 : 1;
        workArray= GetUpdatedArray(workArray, mostCommonBit, i);
        Console.WriteLine("Index " + i +", most common bit: " + mostCommonBit + ", result:");
        foreach (var s in workArray)
        {
          Console.WriteLine(s);
        }

        if (workArray.Length == 1)
        {
          Console.WriteLine("Only one number left");
          Console.WriteLine(workArray[0]);
          break;
        }
      }
      var oxygenGeneratorRating = Convert.ToInt32(workArray[0], 2);
      Console.WriteLine("Oxygen Generator Rating: " + oxygenGeneratorRating);
      return oxygenGeneratorRating;
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
