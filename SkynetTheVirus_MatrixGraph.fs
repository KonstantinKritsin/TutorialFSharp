module SkynetTheVirus_MatrixGraph

open System

type ResultStat = {mutable Visit: bool; mutable Dist: int option; mutable Node: int}
let R() = Console.In.ReadLine()

let token = R().Split [|' '|]
let N = int(token.[0]) (* N: the total number of nodes in the level, including the gateways *)
let L = int(token.[1]) (* L: the number of links *)
let E = int(token.[2]) (* E: the number of exit gateways *)
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

(* nearest to skynet virus *)
let findLink (paths: ResultStat []) fromN toN = 
    let mutable current = toN
    while paths.[current].Node <> fromN do
        current <- paths.[current].Node
    (fromN, current)

(* links *)
[for i in 0 .. L - 1 do yield R().Split [|' '|]] |> Seq.iter (fun t -> (createLink (int(t.[0])) (int(t.[1])) 1))

let gateways = [for i in 0 .. E - 1 do yield int(R())]

while true do
    let SI = int(R()) (* The index of the node on which the Skynet agent is positioned this turn *)
    let res = findPath SI
    let geteway = gateways |> Seq.filter (fun g -> res.[g].Dist.IsSome) |> Seq.map (fun g -> (g, res.[g].Dist.Value)) |> Seq.minBy snd
    let link = findLink res SI (fst geteway)
    
    removeLink (fst link) (snd link)
    printfn "%d %d" (fst link) (snd link)
    ()
