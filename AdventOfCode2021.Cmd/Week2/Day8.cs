using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day8
  {
    private readonly List<List<string>> _inputList;
    private readonly List<List<string>> _outputList;

    public Day8(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);

      _inputList = new List<List<string>>();
      _outputList = new List<List<string>>();
      foreach (var line in fileData)
      {
        var inputList = new List<string>();
        var inputLine = line.Split('|', StringSplitOptions.RemoveEmptyEntries)[0];
        var inputs = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        foreach (var input in inputs)
        {
          inputList.Add(input);
        }
        _inputList.Add(inputList);

        var outputList = new List<string>();
        var outputLine = line.Split('|', StringSplitOptions.RemoveEmptyEntries)[1];
        var outputs = outputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        foreach (var output in outputs)
        {
          outputList.Add(output);
        }
        _outputList.Add(outputList);
      }
      
    }

    public void FindUniqueOutputNumberCount()
    {
      // digit 1 has length 2
      var ones = FindDigitCountInOutput(1);
      // digit 4 has length 4
      var fours = FindDigitCountInOutput(4);
      // digit 7 has length 3
      var sevens = FindDigitCountInOutput(7);
      // digit 8 has length 7
      var eights = FindDigitCountInOutput(8);

      var total = ones + fours + sevens + eights;
      Console.WriteLine($"A total of {total} unique digits were found.");
    }

    private int FindDigitCountInOutput(int digit)
    {
      var length = 0;
      var count = 0;
      switch (digit)
      {
        case 1:
          length = 2;
          break;
        case 4:
          length = 4;
          break;
        case 7:
          length = 3;
          break;
        case 8:
          length = 7;
          break;
      }

      foreach (var outputList in _outputList)
      {
        foreach (var output in outputList)
        {
          if (output.Length == length) count++;
        }
      }

      return count;
    }
  }
}
