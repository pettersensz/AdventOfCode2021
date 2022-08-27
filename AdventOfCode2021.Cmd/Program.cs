using AdventOfCode2021.Cmd.Week1;
using System;

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
      var day2 = new Day2("day2_input.txt");
      day2.DetermineNewPosition();


      Console.WriteLine("The end...");
    }


  }
}
