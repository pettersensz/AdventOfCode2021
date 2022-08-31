using System;
using System.Collections.Generic;
using System.Linq;
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
      for (var i = 0 ; i < _outputList.Count; i++)
      {
        var outputList = _outputList[i];
        // First need to determine how digits are encoded based on input...
        Console.WriteLine("Entry " + i );
        var dictionary = FigureOutHowItWorks(_inputList[_outputList.IndexOf(outputList)]);
        sum += DetermineSumOfOutputList(dictionary, outputList);
      }
      Console.WriteLine($"Total sum: {sum}");
    }

    private int DetermineSumOfOutputList(Dictionary<int, string> dictionary, List<string> outputList)
    {
      var sum = 0;
      for (var i = 0; i < outputList.Count; i++)
      {
        var output = outputList[i];
        var outputArray = output.ToCharArray();
        var number = 0;
        foreach (var kvp in dictionary)
        {
          var charArray = kvp.Value.ToCharArray();
          if(outputArray.Length != charArray.Length) continue;
          if (outputArray.All(element => charArray.Contains(element)))
          {
            number = kvp.Key;
          }
        }

        if (i == 0) number = number * 1000;
        if (i == 1) number = number * 100;
        if (i == 2) number = number * 10;
        sum += number;
      }
      Console.WriteLine("Sum: " + sum);
      return sum;
    }

    private Dictionary<int,string> FigureOutHowItWorks(List<string> inputList)
    {
      var numberAndStringDict = new Dictionary<int, string>();
      foreach (var input in inputList)
      {
        var digit = 22;
        if (input.Length == 2)
        {
          digit = 1;
          numberAndStringDict.Add(1, input);
        }
        else if (input.Length == 7)
        {
          digit = 8;
          numberAndStringDict.Add(8, input);
        }
        else if (input.Length == 4)
        {
          digit = 4;
          numberAndStringDict.Add(4, input);
        }
        else if (input.Length == 3)
        {
          digit = 7;
          numberAndStringDict.Add(7, input);
        }
        else if (input.Length == 5)
        {
          // 5 or 2 or 3
        }

        //Console.Write(digit + " is written: " + input);

        //Console.Write("\n");
      }

      var characterDict = new Dictionary<string, char>();
      // By now we have 8, 1, 7 and 4 in dict
      characterDict["topLine"] = FindTopLineCharacter(numberAndStringDict);
      characterDict["topRight"] = FindTopRightCharacter(numberAndStringDict, inputList);
      // We have 8,1,7,4 and 6 in dict and know 2 of 7 positions
      characterDict["bottomRight"] = FindBottomRightCharacter(numberAndStringDict, characterDict["topRight"]);
      // we know 3 of 7 positions
      characterDict["bottomLeft"] = FindBottomLeftCharacter(numberAndStringDict, characterDict["topRight"], inputList);
      // we have 1,4,5,6,7,8 in dict and know 4 of 7 positions
      Add2And3ToDict(numberAndStringDict, characterDict["bottomRight"], inputList);
      // We have 1,2,3,4,5,6,7,8 in dict, missing 0 and 9
      // Can use info about 4 and 5 plus top line to identify bottom line
      characterDict["bottomLine"] = FindBottomLineCharacter(numberAndStringDict, characterDict["topLine"]);
      // Can now find center line, it's the only unknown in 2
      characterDict["centerLine"] = FindCenterLineCharacter(numberAndStringDict, characterDict);
      characterDict["topLeft"] = FindTopLeftCharacter(numberAndStringDict, characterDict);

      foreach (var kvp in numberAndStringDict)
      {
        //Console.WriteLine(kvp.Key + " is written " + kvp.Value);
      }

      foreach (var input in inputList)
      {
        if(numberAndStringDict.ContainsValue(input)) continue;
        if (input.Contains(characterDict["centerLine"]))
        {
          numberAndStringDict[9] = input;
        }
        else numberAndStringDict[0] = input;
      }

      // 4 contains two elements that 7 and 1 does not contain, center line and top left
      // 8 contains top line (we know this) and additionally two elements 4 does not contain, bottom left and bottom line
      // We know that top right is one of the two elements of 1
      // Top right is the only difference between 6 and 8
      // Center is the only difference between 8 and o

      return numberAndStringDict;
    }

    private char FindTopLeftCharacter(Dictionary<int, string> numberAndStringDict, Dictionary<string, char> characterDict)
    {
      var elements8 = numberAndStringDict[8].ToCharArray();
      var topRight = 'X';
      foreach (var c in elements8)
      {
        if (characterDict.ContainsValue(c)) continue;
        topRight = c;
      }

      //Console.WriteLine("Top right: " + topRight);
      return topRight;
    }

    private char FindCenterLineCharacter(Dictionary<int, string> numberAndStringDict, Dictionary<string, char> characterDict)
    {
      var elements2 = numberAndStringDict[2].ToCharArray();
      var centerLine = 'X';
      foreach (var c in elements2)
      {
        // TODO can maybe use containsValue here also
        if (c == characterDict["topLine"]) continue;
        if (c == characterDict["topRight"]) continue;
        if (c == characterDict["bottomLeft"]) continue;
        if (c == characterDict["bottomLine"]) continue;
        centerLine = c;
      }
      //Console.WriteLine("Center line: " + centerLine);
      return centerLine;
    }

    private char FindBottomLineCharacter(Dictionary<int, string> numberAndStringDict, char topLine)
    {
      var elements4 = numberAndStringDict[4].ToCharArray();
      var elements5 = numberAndStringDict[5].ToCharArray();
      var bottomLine = 'X';
      foreach (var charIn5 in elements5)
      {
        if(charIn5 == topLine) continue;
        if(elements4.Contains(charIn5)) continue;
        bottomLine = charIn5;
      }
      //Console.WriteLine("Bottom line: " + bottomLine);
      return bottomLine;
    }

    private void Add2And3ToDict(Dictionary<int, string> dict, char bottomRight, List<string> inputList)
    {
      foreach (var input in inputList)
      {
        if(input.Length != 5) continue;
        if(input == dict[5]) continue;
        if (input.ToCharArray().Contains(bottomRight)) dict[3] = input;
        else dict[2] = input;
      }
    }

    private char FindBottomLeftCharacter(Dictionary<int, string> dict, char topRight, List<string> inputList)
    {
      // Bottom left is the only difference between 6 and 5. 6 will have it, 5 does not
      // The other two numbers with length 5 will contain topRight
      var elements6 = dict[6].ToCharArray();
      var bottomLeft = 'X';
      foreach (var input in inputList)
      {
        if (input.Length == 5)
        {
          var elements = input.ToCharArray();
          if (elements.Contains(topRight)) continue; // either 3 or 2
          dict[5] = input;
          foreach (var c in elements6)
          {
            if (elements.Contains(c)) continue;
            bottomLeft = c;
          }
        }
      }
      //Console.WriteLine("Bottom left: " + bottomLeft);
      return bottomLeft;
    }

    private char FindBottomRightCharacter(Dictionary<int, string> dict, char topRight)
    {
      var elements1 = dict[1].ToCharArray();
      var bottomRight = 'X';
      foreach (var c in elements1)
      {
        if (c != topRight) bottomRight = c;
      }
      //Console.WriteLine("Bottom right: " + bottomRight);
      return bottomRight;
    }

    private char FindTopRightCharacter(Dictionary<int, string> dict, List<string> inputList)
    {
      // 6 is the only element with length 6 that does not contain both elements in 1
      // We can find top right and add 6 to dict
      var elements1 = dict[1].ToCharArray();
      char topRight = 'X';
      foreach (var input in inputList) 
      {
        if (input.Length == 6)
        {
          // 6 or 9 or 0
          var elements = input.ToCharArray();
          foreach (var character in elements1)
          {
            if(elements.Contains(character)) continue;
            topRight = character;
            dict[6] = input;
          }
        }
      }
      //Console.WriteLine("Top right: " + topRight);
      return topRight;
    }

    private static char FindTopLineCharacter(Dictionary<int, string> dict)
    {
      var elements7 = dict[7].ToCharArray();
      var elements1 = dict[1].ToCharArray();
      var topLine = 'X';
      foreach (var s in elements7)
      {
        if (!elements1.Contains(s)) topLine = s;
      }

      //Console.WriteLine("Top line: " + topLine);
      return topLine;
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
