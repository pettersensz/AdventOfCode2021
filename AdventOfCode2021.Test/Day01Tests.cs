using AdventOfCode2021.Cmd;
using FluentAssertions;
using NUnit.Framework;
using System.IO;

namespace AdventOfCode2021.Test
{
  public class Tests
  {
    private string[] _testData;
    private string[] _completeData;

    [SetUp]
    public void Setup()
    {
      var directory = Directory.GetCurrentDirectory();
      var testPath = Path.GetFullPath(Path.Combine(directory, @"..\..\..\..\inputData\day1_test_input.txt"));
      var completePath = Path.GetFullPath(Path.Combine(directory, @"..\..\..\..\inputData\day1_input.txt"));
      _testData = File.ReadAllLines(testPath);
      _completeData = File.ReadAllLines(completePath);
    }

    [Test]
    public void TestNumberOfIncreases()
    {
      var result = Program.DetermineNumberOfIncreases(_testData);
      result.Should().Be(7);

      var completeResult = Program.DetermineNumberOfIncreases(_completeData);
      completeResult.Should().Be(1228);
    }

    [Test]
    public void TestSlidingSumIncreases()
    {
      var result = Program.DetermineNumberOfIncreasesUsingSlidingSum(_testData);
      result.Should().Be(5);

      var completeResult = Program.DetermineNumberOfIncreasesUsingSlidingSum(_completeData);
      completeResult.Should().Be(1257);
    }
  }
}