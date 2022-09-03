using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day10
  {
    private readonly char[] _allowedOpeningChars = { '(', '[', '{', '<' };
    private readonly char[] _allowedClosingChars = { ')', ']', '}', '>' };
    private readonly List<string> _incompleteLines = new List<string>();

    public Day10(string fileName)
    {
      var data = Common.ReadFile.ReadLinesInTextFile(fileName);
      var badCharacters = new List<char>();
      for (var i = 0; i < data.Length; i++)
      {
        var badLine = false;
        var line = data[i];
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
        else
        {
          _incompleteLines.Add(line.ToString());
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

    public void CompleteTheLines()
    {
      Console.WriteLine("------");
      var scores = new List<long>();
      foreach (var incompleteLine in _incompleteLines)
      {
        //Console.WriteLine(incompleteLine);
        var charStack = new Stack<char>();
        foreach (var character in incompleteLine)
        {
          if (_allowedOpeningChars.Contains(character))
          {
            charStack.Push(character);
          }
          // Have already checked the lines so pairs should always match
          else if (_allowedClosingChars.Contains(character))
          {
            charStack.Pop();
          }
        }

        var additionalCharacters = "";
        while (charStack.Count > 0)
        {
          var openingChar = charStack.Pop();
          if (openingChar == '(') additionalCharacters += ")";
          else if (openingChar == '[') additionalCharacters += "]";
          else if (openingChar == '{') additionalCharacters += "}";
          else if (openingChar == '<') additionalCharacters += ">";
        }
        Console.Write(additionalCharacters);
        long score = CalculateScoreForLine(additionalCharacters);
        scores.Add(score);
        Console.WriteLine(" | Score: " + score);
      }

      var sortedScores = scores.OrderBy(s => s).ToList();
      if(sortedScores.Count() % 2 == 0) Console.WriteLine("Warning, even number of scores");
      else
      {
        var middleIndex = (sortedScores.Count() - 1) / 2;
        var middleScore = sortedScores[middleIndex];
        Console.WriteLine("Middle score: " + middleScore);
      }
    }

    private long CalculateScoreForLine(string additionalCharacters)
    {
      long score = 0;
      foreach (var c in additionalCharacters)
      {
        //Console.WriteLine("Score coming in: " + score);
        score *= 5;
        //Console.WriteLine("Score after *: " + score);
        switch (c)
        {
          case ')':
            score += 1;
            break;
          case ']':
            score += 2;
            break;
          case '}':
            score += 3;
            break;
          case '>':
            score += 4;
            break;
        }
        //Console.WriteLine("Score after char: " + score);
      }

      return score;
    }
  }
}
