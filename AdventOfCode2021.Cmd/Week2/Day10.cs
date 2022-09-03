using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day10
  {
    public Day10(string fileName)
    {
      var allowedOpeningChars = new char[] { '(', '[', '{', '<' };
      var allowedClosingChars = new char[] { ')', ']', '}', '>' };
      var data = Common.ReadFile.ReadLinesInTextFile(fileName);
      for (var i = 0; i < 1; i++)
      {
        var line = data[i];
        var chunkStack = new Stack();

      }
    }
  }
}
