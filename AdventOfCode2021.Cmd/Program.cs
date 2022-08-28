using AdventOfCode2021.Cmd.Week1;
using System;

namespace AdventOfCode2021.Cmd
{
  public class Program
  {
    static void Main(string[] args)
    {
      // Day 1
      //var day1 = new Day1("day1_input.txt");
      //day1.DetermineNumberOfIncreases();
      //day1.DetermineNumberOfIncreasesUsingSlidingSum();

      // Day 2
      //var day2 = new Day2("day2_input.txt");
      //day2.DetermineNewPosition();
      //day2.DetermineNewPositionWithAim();

      // Day 3
      //var day3 = new Day3("day3_input.txt");
      //day3.DeterminePowerConsumption();
      //day3.DetermineLifeSupportRating();

      // Day 4
      //var day4 = new Day4("day4_input.txt");
      //day4.Play(Day4.PlayGoal.Win);
      //day4.Play(Day4.PlayGoal.Lose);

      // Day 5
      //var day5 = new Day5("day5_input.txt");

      // Day 6
      var day6 = new Day6("day6_test_input.txt");
      day6.SimulateFishGrowth(18);


      Console.WriteLine("The end...");
    }


  }
}
