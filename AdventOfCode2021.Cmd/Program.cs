using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2021.Cmd.Week1.Day1;

namespace AdventOfCode2021.Cmd
{
  public class Program
  {
    static void Main(string[] args)
    {
      // Day 1
      var day1 = new Day1("day1_input.txt");
      day1.DetermineNumberOfIncreases();
      day1.DetermineNumberOfIncreasesUsingSlidingSum();

      // Day 2


      Console.WriteLine("The end...");
    }

    
  }
}
