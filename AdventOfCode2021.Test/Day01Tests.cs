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
      // TODO Relative file path!
      _testData = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_test_input.txt");
      _completeData = File.ReadAllLines(@"C:\kode\AdventOfCode2021\day1\day1_input.txt");
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