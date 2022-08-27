using AdventOfCode2021.Cmd.Week1;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2021.Test
{
  public class Day02Tests
  {
    private Day2 _day2Test;
    private Day2 _day2Complete;

    [SetUp]
    public void Setup()
    {
      _day2Test = new Day2("day2_test_input.txt");
      _day2Complete = new Day2("day2_input.txt");
    }

    [Test]
    public void TestNumberOfIncreases()
    {
      var testResult = _day2Test.DetermineNewPosition();
      testResult.Should().Be(150);

      var completeResult = _day2Complete.DetermineNewPosition();
      completeResult.Should().Be(2036120);
    }

    [Test]
    public void TestSlidingSumIncreases()
    {
      var testResult = _day2Test.DetermineNewPositionWithAim();
      testResult.Should().Be(900);

      var completeResult = _day2Complete.DetermineNewPositionWithAim();
      completeResult.Should().Be(2015547716);
    }
  }
}