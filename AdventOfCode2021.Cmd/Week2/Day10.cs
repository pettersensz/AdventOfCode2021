using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day10
  {
    private char[] _allowedOpeningChars = new char[] { '(', '[', '{', '<' };
    private char[] _allowedClosingChars = new char[] { ')', ']', '}', '>' };

    public Day10(string fileName)
    {
      var data = Common.ReadFile.ReadLinesInTextFile(fileName);
      for (var i = 0; i < 1; i++)
      {
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
            var indexOpening = Array.IndexOf(openingChar, )
          }
        }
      }
    }
  }
}
