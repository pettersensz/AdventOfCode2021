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
      var oxygenGeneratorRating = DetermineOxygenGeneratorRating();
      var scrubberRating = DetermineScrubberRating();
      
      var lifeSupportRating = oxygenGeneratorRating * scrubberRating;
      Console.WriteLine("Life Support Rating: " + lifeSupportRating);
    }

    private int DetermineScrubberRating()
    {
      return 1;
    }

    private int DetermineOxygenGeneratorRating()
    {
      // start only with first bit
      var zeroCount = DetermineBitCountAtIndex("0", 0, _fileData);
      var oneCount = DetermineBitCountAtIndex("1", 0, _fileData);
      int mostCommonBit;
      if (zeroCount > oneCount) mostCommonBit = 0;
      else mostCommonBit = 1;
      var updatedArray = GetUpdatedArray(_fileData, mostCommonBit, 0);

      return 9;
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
