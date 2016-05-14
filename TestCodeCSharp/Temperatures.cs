using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
        string temps = Console.ReadLine(); // the n temperatures expressed as integers ranging from -273 to 5526
        Console.Error.WriteLine(string.Format("temps = '{0}'", temps));
        var result = (temps ?? "").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => new { O = int.Parse(s), M = Math.Abs(int.Parse(s)) })
        .GroupBy(o => o.M)
        .OrderBy(g => g.Key)
        .Select(g => g.FirstOrDefault(o => o.O > 0) ?? g.First())
        .FirstOrDefault();
        //Console.Error.WriteLine(result.Length);
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(result == null ? 0 : result.O);
    }
}