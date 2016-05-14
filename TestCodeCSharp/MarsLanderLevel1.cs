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
class Player
{
    struct Line
    {
        private Point _a;
        private Point _b;
        private double _multiplier;

        public bool IsHorizontal { get { return _a.Y == _b.Y; } }

        public Line(Point a, Point b)
        {
            _a = a;
            _b = b;
            _multiplier = (b.Y - a.Y) / (double)(b.X - a.X);
        }

        // получение координаты точки данной линии, лежащей непосредственно под запрашиваемой точкой
        public Point? GetLowerPoint(Point p)
        {
            if (p.X < _a.X || p.X > _b.X)
                return null;

            return new Point
            {
                X = p.X,
                Y = (int)((p.X - _a.X) * _multiplier + _a.Y)
            };
        }
    }

    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    static void Main(string[] args)
    {
        const double g = 3.711;// ускорение свободного падение на Марсе
        const double landSpeed = 40;// максимальная скорость, с которой марсоход может приземлиться
        const int aMarsLander = 4;// максимальное ускорение торможения, генерируемое марсоходом

        string[] inputs;
        int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        var surface = new Point[surfaceN];
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            surface[i] = new Point(landX, landY);
        }

        var surfaces = new List<Line>();

        for (int i = 1; i < surfaceN; i++)
            surfaces.Add(new Line(surface[i - 1], surface[i]));

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

            // координата поверхности Марса под марсоходом
            var lowerMarsPoint = surfaces.Select(surf => surf.GetLowerPoint(new Point(X, Y))).First(p => p != null).Value;
            Console.Error.WriteLine("lowerMarsPoint " + lowerMarsPoint);
            // вычисляем расстояние s от текущего положения корабля до поверхности:
            // это координата Y корабля за вычетом координаты Y поверхности Марса.
            var s = (double)Y - lowerMarsPoint.Y;
            Console.Error.WriteLine("s " + s);
            // расстояние, на котором необходимо начинать торможение:
            var sBreak = s - (landSpeed * landSpeed - vSpeed * vSpeed + 2 * (aMarsLander - g) * s) / (2 * g + 2 * (aMarsLander - g));
            Console.Error.WriteLine("sBreak " + sBreak);
            // координата, на которой необходимо начинать торможение:
            var controlY = (int)sBreak + lowerMarsPoint.Y;
            Console.Error.WriteLine("controlY " + controlY);

            Console.Error.WriteLine(string.Format("X:{0}|Y:{1}|hSpeed:{2}|vSpeed:{3}|fuel:{4}|power:{5}", X, Y, hSpeed, vSpeed, fuel, power));

            // т.к. увеличение мощности торможения от 0 до 4 происходит по 1 единице за секунду, а так же за каждую секунду проходим расстояние равное скорости,
            // то оценим расстояние, за которое пора начинать тормозить примерно как ((1+2+3+4)/4)*vSpeed
            if (Y + 2.5 * vSpeed < controlY)
                power = 4;


            // 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4).
            Console.WriteLine(string.Format("{0} {1}", rotate, power));
        }
    }
}