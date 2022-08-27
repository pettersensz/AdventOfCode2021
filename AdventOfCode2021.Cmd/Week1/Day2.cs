using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  public class Day2
  {
    private readonly List<Day2Command> _commandList;

    public Day2(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);
      _commandList = GetCommandList(fileData);
    }

    private List<Day2Command> GetCommandList(string[] fileData)
    {
      var commandList = new List<Day2Command>();
      foreach (var line in fileData)
      {
        var command = new Day2Command
        {
          Direction = GetDirection(line.Split(' ')[0]),
          Amount = GetAmount(line.Split(' ')[1])
        };
        commandList.Add(command);
      }

      return commandList;
    }

    private int GetAmount(string amount)
    {
      return int.Parse(amount);
    }

    private CommandDirection GetDirection(string line)
    {
      switch (line)
      {
        case "forward":
          return CommandDirection.Forward;
        case "down":
          return CommandDirection.Down;
        case "up":
          return CommandDirection.Up;
        default:
          return CommandDirection.Unknown;
      }
    }

    public void DetermineNewPosition()
    {
      var horizontalPosition = 0;
      var verticalPosition = 0;
      Console.WriteLine("Start: " + horizontalPosition + ", " + verticalPosition);
      foreach (var command in _commandList)
      {
        switch (command.Direction)
        {
          case CommandDirection.Forward:
            horizontalPosition += command.Amount;
            break;
          case CommandDirection.Down:
            verticalPosition -= command.Amount;
            break;
          case CommandDirection.Up:
            verticalPosition += command.Amount;
            break;
        }

        Console.WriteLine("Move: " + horizontalPosition + ", " + verticalPosition);
      }
      var product = horizontalPosition * Math.Abs(verticalPosition);
      Console.WriteLine("Product: " + product);
    }

  }

  internal class Day2Command
  {
    internal CommandDirection Direction;
    internal int Amount;
  }

  internal enum CommandDirection
  {
    Forward,
    Down,
    Up,
    Unknown
  }
}
