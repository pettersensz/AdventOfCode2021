﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Cmd.Week1
{
  public class Day6
  {
    private List<LanternFish> _fishList;
    public Day6(string filename)
    {
      var fileData = Common.ReadFile.ReadLinesInTextFile(filename);
      _fishList = CreateInitialFishListFromInput(fileData);
    }

    private List<LanternFish> CreateInitialFishListFromInput(string[] fileData)
    {
      var fishList = new List<LanternFish>();
      foreach (var s in fileData)
      {
        var elements = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var element in elements)
        {
          fishList.Add(new LanternFish(int.Parse(element)));
        }
      }

      return fishList;
    }

    public void SimulateFishGrowth(int daysToSimulate)
    {
      for (var day = 0; day < daysToSimulate; day++)
      {
        foreach (var fish in _fishList)
        {
          bool newFish = fish.Age();
          if(newFish) _fishList.Add(new LanternFish(8));
        }
      }
    }
  }

  public class LanternFish
  {
    public bool Fresh { get; set; }
    public int Timer { get; set; }

    public LanternFish(int timer)
    {
      Timer = timer;
      Fresh = true;
    }

    public bool Age()
    {
      if (Timer == 0)
      {
        Timer = 6;
        Fresh = false;
        return true;
      }
      Timer--;
      return false;
    }
  }
}
