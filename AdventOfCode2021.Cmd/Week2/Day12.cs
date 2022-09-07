using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Cmd.Week2
{
  public class Day12
  {
    private readonly string[] _data;
    private readonly Dictionary<string, Cave> _caves;
    private readonly List<Channel> _channels;
    private readonly List<Route> _routes;
    public Day12(string filename)
    {
      _data = Common.ReadFile.ReadLinesInTextFile(filename);
      _caves = IdentifyCaves();
      _channels = IdentifyChannels();
      _routes = IdentifyRoutes();
    }

    private List<Channel> IdentifyChannels()
    {
      var channels = new List<Channel>();
      foreach (var line in _data)
      {
        var parts = line.Split('-');
        var startCave = _caves[parts[0]];
        var endCave = _caves[parts[1]];
        var channel = new Channel()
        {
          Name = line,
          StartCave = startCave,
          EndCave = endCave
        };
        channels.Add(channel);
        startCave.ChannelsOut.Add(channel);
      }


      return channels;
    }

    private List<Route> IdentifyRoutes()
    {
      var routes = new List<Route>();

      var startCave = _caves.Values.First(c => c.StartCave);
      

      foreach (var channel in startCave.ChannelsOut)
      {
        var route = new Route();
        route.Caves.Add(startCave);
        route.Channels.Add(channel);
        AnalyzeChannelRecursive(channel, route, routes);
        routes.Add(route);
      }

      return routes;
    }

    private void AnalyzeChannelRecursive(Channel channel, Route route, List<Route> routes)
    {
      var endCave = _caves.Values.First(c => c.EndCave);
      foreach (var newChannel in channel.EndCave.ChannelsOut)
      {
        if (newChannel.EndCave == endCave)
        {
          route.Caves.Add(endCave);
          route.Channels.Add(newChannel);
          continue;
        }
        if(newChannel.EndCave.ChannelsOut.Count == 0) continue;
      }
    }

    private Dictionary<string, Cave> IdentifyCaves()
    {
      var caves = new Dictionary<string, Cave>();
      foreach (var line in _data)
      {
        var parts = line.Split('-');
        foreach (var part in parts)
        {
          if (caves.ContainsKey(part)) continue;
          var big = char.IsUpper(part[0]);
          var start = part == "start";
          var end = part == "end";
          var cave = new Cave() { Big = big, Name = part, EndCave = end, StartCave = start};
          caves.Add(part, cave);
        }
      }

      return caves;
    }
  }

  public class Cave
  {
    public string Name;
    public bool Big;
    public bool StartCave;
    public bool EndCave;
    public List<Channel> ChannelsOut = new List<Channel>();
  }

  public class Channel
  {
    public string Name;
    public Cave StartCave;
    public Cave EndCave;
  }

  public class Route
  {
    public List<Channel> Channels = new List<Channel>();
    public List<Cave> Caves = new List<Cave>();
  }
}
