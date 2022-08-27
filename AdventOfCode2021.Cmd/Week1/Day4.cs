using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  public class Day4
  {
    private readonly Queue<int> _numberQueue;
    private readonly List<Board> _boardsList;

    public Day4(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);

      _numberQueue = PopulateNumberQueue(fileData[0]);

      _boardsList = CreateBoardList(fileData);
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
          boardCounter++;
          board = new Board("Board " + boardCounter);
          continue;
        }

        board.AddRow(fileData[i]);
      }
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
  }

  public class Board
  {
    public Board(string name)
    {
      Name = name;
      Rows = new List<int[]>();
    }

    public string Name { get; set; }

    public List<int[]> Rows { get; set; }


    public void AddRow(string line)
    {
      var rowString = line.Split(',');
      var row = new int[5];
      for (var i = 0; i < 5; i++)
      {
        
      }
      Rows.Add(row);
    }
  }
}
