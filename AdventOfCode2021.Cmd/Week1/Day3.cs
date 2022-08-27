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


    private int DetermineMostCommonBit(int position)
    {
      var zeroCount = 0;
      var oneCount = 0;
      foreach (var line in _fileData)
      {
        if (line.Substring(position, 1) == "0") zeroCount++;
        else if(line.Substring(position,1) == "1") oneCount++;
      }

      return zeroCount > oneCount ? 0 : 1;
    }
  }
}
