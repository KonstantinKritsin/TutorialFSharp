(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System
open System.Collections
open System.Collections.Generic

type Point =
    struct
        val public X: double
        val public Y: double
        new(x: double, y: double) = {X = x; Y = y}
    end

type Line =
    struct
        val private _a: Point
        val private _b: Point
        val private _multiplier: double
        new(a: Point, b: Point) = { _a = a; _b = b; _multiplier = (b.Y - a.Y)/(b.X - a.X) }
        member this.GetLowerPoint(p: Point) = 
            if p.X < this._a.X || p.X > this._b.X then
                None
            else
                Some(Point(p.X, (p.X - this._a.X) * this._multiplier + this._a.Y))

    end

let g = 3.711;// ускорение свободного падение на Марсе
let landSpeed = 40.0;// максимальная скорость, с которой марсоход может приземлиться
let aMarsLander = 4.0;// максимальное ускорение торможения, генерируемое марсоходом

let surfaceN = int(Console.In.ReadLine()) (* the number of points used to draw the surface of Mars. *)
(* landX: X coordinate of a surface point. (0 to 6999) *)
(* landY: Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars. *)
let surface = 
    [|for i in 0 .. surfaceN - 1 do yield (Console.In.ReadLine()).Split [|' '|]|]
    |> Array.map(fun token -> Point(double(token.[0]), double(token.[1])))

let surfaces = [for i in 1 .. surfaceN - 1 do yield Line(surface.[i-1], surface.[i])]

(* game loop *)
while true do
    (* hSpeed: the horizontal speed (in m/s), can be negative. *)
    (* vSpeed: the vertical speed (in m/s), can be negative. *)
    (* fuel: the quantity of remaining fuel in liters. *)
    (* rotate: the rotation angle in degrees (-90 to 90). *)
    (* power: the thrust power (0 to 4). *)
    let token1 = (Console.In.ReadLine()).Split [|' '|]
    let X = double(token1.[0])
    let Y = double(token1.[1])
    let hSpeed = double(token1.[2])
    let vSpeed = double(token1.[3])
    let fuel = double(token1.[4])
    let rotate = int(token1.[5])
    let mutable power = int(token1.[6])
    
    // координата поверхности Марса под марсоходом
    let lowerMarsPoint = surfaces |> Seq.map(fun surf -> surf.GetLowerPoint(Point(X, Y))) |> Seq.find (fun p -> p.IsSome)
    // вычисляем расстояние s от текущего положения корабля до поверхности:
    // это координата Y корабля за вычетом координаты Y поверхности Марса.
    let s = Y - lowerMarsPoint.Value.Y
    // расстояние, на котором необходимо начинать торможение:
    let sBreak = s - (landSpeed * landSpeed - vSpeed * vSpeed + 2. * (aMarsLander - g) * s) / (2. * g + 2. * (aMarsLander - g))
    // координата, на которой необходимо начинать торможение:
    let controlY = sBreak + lowerMarsPoint.Value.Y

    // т.к. увеличение мощности торможения от 0 до 4 происходит по 1 единице за секунду, а так же за каждую секунду проходим расстояние равное скорости,
    // то оценим расстояние, за которое пора начинать тормозить примерно как ((1+2+3+4)/4)*vSpeed
    if Y + 2.5 * vSpeed < controlY then
        power <- 4;

    (* 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4). *)
    printfn "%d %d" rotate power
    ()
