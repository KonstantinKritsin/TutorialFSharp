module Chesscavalry

open System

type ResultStat = {mutable Visit: bool; mutable Dist: int option; mutable Node: int}
type Point(sign: char, x: int, y: int) =
    member val X = x
    member val Y = y
    member val CanGo = sign <> '#'
    member val Sign = sign

let R() = Console.In.ReadLine()

let token = R().Split [|' '|]
let W = int(token.[0])
let H = int(token.[1])
let N = W * H
let nodes = Array.create (N * N) 0

let createLink n1 n2 weight = 
    nodes.[n1 * N + n2] <- weight
    nodes.[n2 * N + n1] <- weight

let removeLink n1 n2 =
    nodes.[n1 * N + n2] <- 0
    nodes.[n2 * N + n1] <- 0

(* Dijkstra's algorithm *)
let findPath fstNode = 
    let result = [|for i in 0 .. N-1 do yield {Visit = false; Dist = None; Node = 0}|]
    result.[fstNode].Dist <- Some(0)
    while result |> Array.exists(fun n -> not <| n.Visit && n.Dist.IsSome) do
        let (index, currentNode) = result |> Array.mapi (fun i n -> (i, n))
                                   |> Array.filter (fun n -> not <| (snd n).Visit && (snd n).Dist.IsSome) 
                                   |> Array.minBy(fun n -> (snd n).Dist.Value)
        currentNode.Visit <- true
        for i in 0 .. N - 1 do
            if nodes.[index * N + i] > 0 && (result.[i].Dist.IsNone || result.[i].Dist.Value > currentNode.Dist.Value + 1) then
                result.[i].Dist <- Some(currentNode.Dist.Value + 1)
                result.[i].Node <- index
    result


let mutable startNode = 0
let mutable exitNode = 0

let map = [|for i in 0 .. H - 1 do
                let line = R()
                for j in 0 .. W - 1 do
                    let p = new Point(line.[j], j, i)
                    if p.Sign = 'B' then
                        startNode <- i * W + j
                    else if p.Sign = 'E' then
                        exitNode <- i * W + j
                    yield p|]

let getAvailable (x:int, y:int) = 
    [|(x+2,y+1);(x+2,y-1);(x-2,y+1);(x-2,y-1);(x+1,y+2);(x+1,y-2);(x-1,y+2);(x-1,y-2)|]
    |> Seq.filter(fun (x1, y1) -> x1 >= 0 && x1 < W && y1 >= 0 && y1 < H && match map.[y1 * W + x1] with | p -> p.CanGo)
    |> Seq.map(fun (x1, y1) -> map.[y1 * W + x1])

for n1 in (map |> Seq.filter(fun p -> p.CanGo)) do
    let avail = getAvailable (n1.X, n1.Y)
    for n2 in avail do createLink (n1.Y * W + n1.X) (n2.Y * W + n2.X) 1

    
    

//
//
//let rec getMinPath (coord: int * int) =
//    let (x, y) = coord
//    Console.Error.WriteLine("coord = {0}", coord)
//    let p = map.[y * W + x]
//    Console.Error.WriteLine("sign {0}; visited {1} canGo {2}", p.Sign, p.Visited, p.CanGo)
//    if p.Sign = 'E' then Some(0)
//    else
//        p.Visited <- true
//        let avail = getAvailable coord
//        Console.Error.WriteLine("avail.Length = {0}", avail.Length)
//        if avail.Length = 0 then None
//        else
//            let arrivals = avail |> Seq.map (fun c -> getMinPath c) |> Seq.filter (fun c -> c.IsSome) |> Seq.toArray
//            Console.Error.WriteLine("arrivals.Length = {0}", arrivals.Length)
//            if arrivals.Length = 0 then None
//            else (arrivals |> Seq.minBy(fun a -> a.Value) |> fun min -> Some(min.Value + 1))

//Console.Error.WriteLine("startPoint = {0}", startPoint)
let res = findPath startNode

(* Write an action using printfn *)
(* To debug: Console.Error.WriteLine("Debug message") *)
if res.[exitNode].Dist.IsNone then printfn "Impossible"
else printfn "%d" res.[exitNode].Dist.Value
