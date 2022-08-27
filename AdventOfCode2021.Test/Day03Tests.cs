using AdventOfCode2021.Cmd.Week1;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2021.Test
{
  public class Day03Tests
  {
    private Day3 _day3Test;
    private Day3 _day3Complete;

    [SetUp]
    public void Setup()
    {
      _day3Test = new Day3("day3_test_input.txt");
      _day3Complete = new Day3("day3_input.txt");
    }

    [Test]
    public void TestPowerConsumption()
    {
      var testResult = _day3Test.DeterminePowerConsumption();
      testResult.Should().Be(198);

      var completeResult = _day3Complete.DeterminePowerConsumption();
      completeResult.Should().Be(4103154);
    }

    [Test]
    public void TestLifeSupportRating()
    {
      var testResult = _day3Test.DetermineLifeSupportRating();
      testResult.Should().Be(230);

      var completeResult = _day3Complete.DetermineLifeSupportRating();
      completeResult.Should().Be(4245351);
    }
  }
}