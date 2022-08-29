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

    public void DetermineSumOfOutputValues()
    {
      var sum = 0;
      for (var i = 0; i < _outputList.Count; i++)
      {
        var outputList = _outputList[i];
        // First need to determine how digits are encoded based on input...
        Console.WriteLine("Entry " + i );
        FigureOutHowItWorks(_inputList[_outputList.IndexOf(outputList)]);
        sum += DetermineSumOfOutputList(outputList);
      }
      Console.WriteLine($"Total sum: {sum}");
    }

    private void FigureOutHowItWorks(List<string> inputList)
    {
      
      foreach (var input in inputList)
      {
        var digit = 22;
        if (input.Length == 2) digit = 1;

        Console.WriteLine(digit + " is written: " + input);
      }
    }

    private int DetermineSumOfOutputList(List<string> outputList)
    {
      var number = 0;
      var numberOfDigits = outputList.Count;
      for (var i = 0; i < numberOfDigits; i++)
      {
        var entry = outputList[i];
        var digit = 0;
        switch (entry.Length)
        {
          case 2:
            digit = 1;
            break;
          case 3:
            digit = 7;
            break;
          case 4:
            digit = 4;
            break;
          case 7:
            digit = 8;
            break;
          default:
            if (entry.Length == 6) // 0 or 9 or 6
            {
              if (!entry.Contains('f')) digit = 0;
              else if (!entry.Contains('g')) digit = 9;
              else if (!entry.Contains('a')) digit = 6;
            }
            else if (entry.Length == 5) // 5 or 2 or 3
            {
              if (!entry.Contains('e') && !entry.Contains('g')) digit = 3;
              else if (!entry.Contains('e')) digit = 2;
              else digit = 5;
            }
            break;
        }


        switch (i)
        {
          case 0:
            digit = digit * 1000;
            break;
          case 1:
            digit = digit * 100;
            break;
          case 2:
            digit = digit * 10;
            break;
        }

        number += digit;
      }

      return number;
    }
  }
}
