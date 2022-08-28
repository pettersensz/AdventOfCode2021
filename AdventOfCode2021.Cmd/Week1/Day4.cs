using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  public class Day4
  {
    private readonly Queue<int> _numberQueue;
    private readonly List<Board> _boardList;

    public Day4(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);

      _numberQueue = PopulateNumberQueue(fileData[0]);

      _boardList = CreateBoardList(fileData);
    }

    private List<Board> CreateBoardList(string[] fileData)
    {
      var boardList = new List<Board>();
      var boardCounter = 0;
      var board = new Board("Board 0");
      for (var i = 0; i < fileData.Length; i++)
      {
        if(i == 0) continue;
        if (fileData[i].Length < 2)
        {
          if (i > 3)
          {
            boardList.Add(board);
          }
          boardCounter++;
          board = new Board("Board " + boardCounter);
          continue;
        }

        board.AddRow(fileData[i]);
      }
      boardList.Add(board);

      return boardList;
    }

    private Queue<int> PopulateNumberQueue(string numbers)
    {
      var numberArray = numbers.Split(',');
      var numberQueue = new Queue<int>();
      foreach (var number in numberArray)
      {
        var value = int.Parse(number);
        numberQueue.Enqueue(value); 
      }

      return numberQueue;
    }

    public int Play(PlayGoal goal)
    {
      Console.WriteLine("Boards at start:");
      //PrintBoards();

      Board winningBoard = null;
      var rounds = _numberQueue.Count;
      var numberFromQueue = 0;

      for(var i = 1; i < rounds; i++)
      {
        Console.WriteLine("Draw: " + i);
        numberFromQueue = _numberQueue.Dequeue();
        Console.WriteLine("New number: " + numberFromQueue);
        SetDrawnStatusOnBoards(numberFromQueue);
        //PrintBoards();
        winningBoard = CheckForCompleteRowsOrColumns();
        while (winningBoard != null)
        {
          Console.WriteLine("We have a winner: " + winningBoard.Name);
          if (goal == PlayGoal.Win)
          {
            //Ensure we break out of for loop by setting i higher than limit
            i = rounds + 1;
            break;
          }
          if (goal == PlayGoal.Lose)
          {
            if (_boardList.Count == 1)
            {
              //Ensure we break out of for loop by setting i higher than limit
              i = rounds + 1;
              break;
            }
            Console.WriteLine("Removing " + winningBoard.Name);
            _boardList.Remove(winningBoard);
            Console.WriteLine("Checking for further winning boards");
            winningBoard = CheckForCompleteRowsOrColumns();
          }
        }
      }

      var score = CalculateScoreForWinningBoard(winningBoard, numberFromQueue);
      Console.WriteLine("Final score: " + score);
      return score;
    }

    public enum PlayGoal
    {
      Win,
      Lose
    }

    private int CalculateScoreForWinningBoard(Board winningBoard, int lastNumber)
    {
      var sum = GetSumOfUnmarkedNumbers(winningBoard);
      Console.WriteLine("Sum of unmarked numbers: " + sum);
      var score = sum * lastNumber;
      return score;
    }

    private int GetSumOfUnmarkedNumbers(Board winningBoard)
    {
      var sum = 0;

      foreach (var row in winningBoard.Rows)   
      {
        foreach (var boardEntry in row)
        {
          if (boardEntry.Drawn == false) sum += boardEntry.Value;
        }
      }

      return sum;
    }

    private Board CheckForCompleteRowsOrColumns()
    {
      foreach (var board in _boardList)
      {
        var winningBoard = CheckForCompleteRows(board);
        if (winningBoard != null) return winningBoard;
        winningBoard = CheckForCompleteColumns(board);
        if (winningBoard != null) return winningBoard;
      }
      return null;
    }

    private Board CheckForCompleteColumns(Board board)
    {
      for (var column = 0; column < board.Rows[0].Length; column++)
      {
        var winningColumn = true;
        foreach (var row in board.Rows)
        {
          var boardEntry = row[column];
          if (boardEntry.Drawn == false) winningColumn = false;
        }
        if(winningColumn == false) continue;
        return board;
      }
      return null;
    }

    private static Board CheckForCompleteRows(Board board)
    {
      // Check Rows
      foreach (var boardRow in board.Rows)
      {
        var winningRow = true;
        foreach (var boardEntry in boardRow)
        {
          if (boardEntry.Drawn == false) winningRow = false;
        }

        if (winningRow == false) continue;
        return board;
      }

      return null;
    }

    private void SetDrawnStatusOnBoards(int numberFromQueue)
    {
      foreach (var board in _boardList)
      {
        foreach (var boardRow in board.Rows)
        {
          foreach (var boardEntry in boardRow)
          {
            if (boardEntry.Value == numberFromQueue)
            {
              boardEntry.Drawn = true;
            }
          }
        }
      }
    }

    private void PrintBoards()
    {
      Console.ForegroundColor = ConsoleColor.White;
      foreach (var board in _boardList)
      {
        Console.Write(board.Name + "             ");
      }
      Console.Write("\n");

      for (var row = 0; row < _boardList[0].Rows.Count; row++)
      {
        for (var board = 0; board < _boardList.Count; board++)
        {
          for (var index = 0; index < _boardList[board].Rows[row].Length; index++)
          {
            var boardEntry = _boardList[board].Rows[row][index];
            if(boardEntry.Drawn) Console.ForegroundColor = ConsoleColor.Blue;
            if (boardEntry.Value.ToString().Length < 2) Console.Write(" " + boardEntry.Value);
            else Console.Write(boardEntry.Value);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ");
          }
          Console.Write("     ");
        }
        Console.Write("\n");
      }
      Console.WriteLine("--- --- ---");
    }
  }

  public class Board
  {
    public Board(string name)
    {
      Name = name;
      Rows = new List<BoardEntry[]>();
    }

    public string Name { get; set; }

    public List<BoardEntry[]> Rows { get; set; }


    public void AddRow(string line)
    {
      var rowString = line.Split(' ',StringSplitOptions.RemoveEmptyEntries);
      var row = new BoardEntry[5];
      for (var i = 0; i < 5; i++)
      {
        var value = int.Parse(rowString[i]);
        var entry = new BoardEntry()
        {
          Value = value,
          Drawn = false
        };
        //if (value == 22) entry.Drawn = true;
        row[i] = entry;
      }
      Rows.Add(row);
    }
  }

  public class BoardEntry
  {
    public int Value { get; set; }
    public bool Drawn { get; set; }
  }
}
