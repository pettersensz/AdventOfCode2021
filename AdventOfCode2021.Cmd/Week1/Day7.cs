using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  class Day7
  {
    public Day7(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);

      var positionsString = fileData[0].Split(',', StringSplitOptions.RemoveEmptyEntries);

      var crabPositions = new int[positionsString.Length];
      for (var i = 0; i < positionsString.Length; i++)
      {
        crabPositions[i] = int.Parse(positionsString[i]);
      }

      var minPos = crabPositions.Min();
      var maxPos = crabPositions.Max();

      //var costDict = CalculateMovementCostLinear(minPos, maxPos, crabPositions);

      var costDict = CalculateMovementCostIncreasing(minPos, maxPos, crabPositions);

      // https://stackoverflow.com/questions/2805703/good-way-to-get-the-key-of-the-highest-value-of-a-dictionary-in-c-sharp
      var cheapestPosition = costDict.Aggregate((lastResult, currentElement) => lastResult.Value < currentElement.Value ? lastResult : currentElement);

      Console.WriteLine($"The cheapest position is {cheapestPosition.Key} with a cost of {cheapestPosition.Value}" );
    }

    private Dictionary<int, int> CalculateMovementCostIncreasing(int minPos, int maxPos, int[] crabPositions)
    {
      var costDict = new Dictionary<int, int>();
      for (var finalPosition = minPos; finalPosition <= maxPos; finalPosition++)
      {
        var cost = 0;

        foreach (var crabPosition in crabPositions)
        {
          cost += CalculateOneMovementIncreasing(crabPosition, finalPosition);
        }

        costDict.Add(finalPosition, cost);
      }

      return costDict;
    }

    private int CalculateOneMovementIncreasing(int crabPosition, int finalPosition)
    {
      // 1 move = 1 cost
      // 2 move = 1+2 = 3 cost
      // 3 move = 1+2+3 = 6 cost
      // Triangular numbers

      var diff = Math.Abs(finalPosition - crabPosition);

      var cost = (diff * (diff + 1)) / 2;

      return cost;
    }

    private static Dictionary<int, int> CalculateMovementCostLinear(int minPos, int maxPos, int[] crabPositions)
    {
      var costDict = new Dictionary<int, int>();
      for (var finalPosition = minPos; finalPosition <= maxPos; finalPosition++)
      {
        var cost = 0;

        foreach (var crabPosition in crabPositions)
        {
          cost += Math.Abs(crabPosition - finalPosition);
        }

        costDict.Add(finalPosition, cost);
      }

      return costDict;
    }
  }
}
