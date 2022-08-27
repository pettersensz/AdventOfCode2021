using AdventOfCode2021.Cmd;
using FluentAssertions;
using NUnit.Framework;
using System.IO;

namespace AdventOfCode2021.Test
{
  public class Tests
  {
    private string[] _testData;

    [SetUp]
    public void Setup()
    {
      // TODO Relative file path!
      _testData = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_test_input.txt");
    }

    [Test]
    public void TestNumberOfIncreasesReturns7()
    {
      var result = Program.DetermineNumberOfIncreases(_testData);
      result.Should().Be(7);
    }

    [Test]
    public void TestSlidingSumIncreasesReturns5()
    {
      var result = Program.DetermineNumberOfIncreasesUsingSlidingSum(_testData);
      result.Should().Be(5);
    }
  }
}