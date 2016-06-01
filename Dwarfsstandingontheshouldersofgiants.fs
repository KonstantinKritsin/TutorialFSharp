module Dwarfsstandingontheshouldersofgiants
open System
open System.Collections.Generic

let R() = Console.In.ReadLine()

type Node(id: int) = 
    let mutable links: Node list = []
    member this.Id = id
    member this.Links = links
    member this.Add (link: Node) =
        links <- link::links

let rec getMaxDistance (from: Node) (a: Node) =
    a.Links |> Seq.filter (fun l -> l <> from) |> Seq.map (fun l -> 1 + (getMaxDistance a l)) |> Seq.append [0] |> Seq.max

let linkCount = int(R()) (* the number of relations *)
let nodes = new Dictionary<int, Node>()

let addValue x =
    match nodes.TryGetValue(x) with
    | true, n -> n
    | _ -> let n = new Node(x)
           nodes.Add(x, n)
           n

for i in 0 .. linkCount - 1 do
    let inputs = R().Split [|' '|]
    let x = int(inputs.[0])
    let y = int(inputs.[1]);
    let p1 = addValue x
    let p2 = addValue y
    p1.Add p2
    ()

let mentors = (set [for k in nodes.Keys do yield k]) - (set [for v in (nodes.Values |> Seq.collect(fun v -> v.Links |> Seq.map(fun n -> n.Id))) do yield v])
let maxDist = mentors |> Seq.map(fun m -> getMaxDistance (nodes.[m]) (nodes.[m])) |> Seq.max

printfn "%d" (maxDist + 1)