using AdventOfCode2021.Cmd.Week1;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2021.Test
{
  public class Day01Tests
  {
    private Day1 _day1Test;
    private Day1 _day1Complete;

    [SetUp]
    public void Setup()
    {
      _day1Test = new Day1("day1_test_input.txt");
      _day1Complete = new Day1("day1_input.txt");
    }

    [Test]
    public void TestNumberOfIncreases()
    {
      var testResult = _day1Test.DetermineNumberOfIncreases();
      testResult.Should().Be(7);

      var completeResult = _day1Complete.DetermineNumberOfIncreases();
      completeResult.Should().Be(1228);
    }

    [Test]
    public void TestSlidingSumIncreases()
    {
      var testResult = _day1Test.DetermineNumberOfIncreasesUsingSlidingSum();
      testResult.Should().Be(5);

      var completeResult = _day1Complete.DetermineNumberOfIncreasesUsingSlidingSum();
      completeResult.Should().Be(1257);
    }
  }
}