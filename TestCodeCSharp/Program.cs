using System;
using System.Text;

class Player
{
    static void Main(string[] args)
    {
        var road = int.Parse(Console.ReadLine()); // the length of the road before the gap.
        var gap = int.Parse(Console.ReadLine()); // the length of the gap.
        var platform = int.Parse(Console.ReadLine()); // the length of the landing platform.
                                                      // game loop
        while (true)
        {
            var speed = int.Parse(Console.ReadLine()); // the motorbike's speed.
            var coordX = int.Parse(Console.ReadLine()); // the position on the road of the motorbike.
            var command = "WAIT";

            var rest = road - coordX - 1;

            if (speed == 0)
                command = "SPEED";
            else if (road - 1 == coordX)
                command = "JUMP";
            else if (coordX >= road + gap)
                command = "SLOW";
            else if (rest % (gap + 1) != 0 || speed != gap + 1)
            {
                command = speed < gap + 1 ? "SPEED" : "SLOW";
            }

            // A single line containing one of 4 keywords: SPEED, SLOW, JUMP, WAIT.
            Console.WriteLine(command);
        }
        var s = new StringBuilder();

        var init = "CC";
        foreach (var ch in init)
        {

            for (int i = 0; i < 128; i = i<<1)
            {
                
            }
        }

    }
}