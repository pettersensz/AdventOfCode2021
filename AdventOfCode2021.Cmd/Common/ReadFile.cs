using System.IO;

namespace AdventOfCode2021.Cmd.Common
{
  public static class ReadFile
  {
    public static string[] ReadLinesInTextFile(string filename)
    {
      var directory = Directory.GetCurrentDirectory();
      var path = Path.GetFullPath(Path.Combine(directory, @"..\..\..\..\inputData\" + filename));

      var fileData = File.ReadAllLines(path);
      return fileData;
    }
  }
}
