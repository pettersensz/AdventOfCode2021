using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day10
  {
    private char[] _allowedOpeningChars = new char[] { '(', '[', '{', '<' };
    private char[] _allowedClosingChars = new char[] { ')', ']', '}', '>' };

    public Day10(string fileName)
    {
      var data = Common.ReadFile.ReadLinesInTextFile(fileName);
      var badCharacters = new List<char>();
      for (var i = 0; i < data.Length; i++)
      {
        var badLine = false;
        var line = data[i].ToCharArray();
        var chunkStack = new Stack<char>();
        foreach (var c in line)
        {
          if (_allowedOpeningChars.Contains(c))
          {
            chunkStack.Push(c);
          }
          else if (_allowedClosingChars.Contains(c))
          {
            var openingChar = chunkStack.Pop();
            var indexOpening = Array.IndexOf(_allowedOpeningChars, openingChar);
            var indexClosing = Array.IndexOf(_allowedClosingChars, c);
            if (indexOpening != indexClosing)
            {
              badLine = true;
              badCharacters.Add(c);
              break;
            }
          }
        }

        if (badLine)
        {
          Console.WriteLine($"Line with index {i} is corrupted!");
        }
      }
      var score = 0;
      foreach (var badCharacter in badCharacters)
      {
        if (badCharacter == ')') score += 3;
        else if (badCharacter == ']') score += 57;
        else if (badCharacter == '}') score += 1197;
        else if (badCharacter == '>') score += 25137;
      }
      Console.WriteLine("Total score: " + score);
    }
  }
}
