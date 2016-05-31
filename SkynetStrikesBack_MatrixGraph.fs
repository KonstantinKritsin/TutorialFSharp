module SkynetStrikesBack_MatrixGraph

open System

type ResultStat = {mutable Visit: bool; mutable Dist: int option; mutable Node: int}
type CriticalNode = {Node: int; mutable ExtraCritical: bool; mutable GateLinks: int list; mutable LinksCount: int}
let R() = Console.In.ReadLine()

let rec remove link lst = 
        match lst with
        | h::t when h = link -> t
        | h::t -> h::remove link t
        | _ -> []

let token = R().Split [|' '|]
let N = int(token.[0]) (* N: the total number of nodes in the level, including the gateways *)
let L = int(token.[1]) (* L: the number of links *)
let E = int(token.[2]) (* E: the number of exit gateways *)
let nodes = Array.create (N * N) 0
let getNodeLinks (node: int) = [|0 .. N - 1|] |> Array.filter(fun l -> nodes.[node * N + l] > 0)
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

(* links *)
[for i in 0 .. L - 1 do yield R().Split [|' '|]] |> Seq.iter (fun t -> (createLink (int(t.[0])) (int(t.[1])) 1))

let gateways = [|for i in 0 .. E - 1 do yield int(R())|]
let agentInDangerZone (node: CriticalNode) (agent: int) =
    let nodeLinks = getNodeLinks node.Node
    nodeLinks |> Array.filter(fun l -> not (gateways |> Array.exists(fun g -> g = l))) |> Array.exists(fun l -> l = agent)

let hasNearestGates (node: CriticalNode) = 
    let nodeLinks = getNodeLinks node.Node
    node.LinksCount <- nodeLinks.Length

    let dangerLinks = nodeLinks |> Array.filter(fun l -> not (gateways |> Array.exists(fun g -> g = l)))
    for link in dangerLinks do
        if (getNodeLinks link) |> Array.exists(fun nl -> gateways |> Array.exists(fun g -> g = nl)) then
            node.ExtraCritical <- true

let criticalNodes = [|for i in 0 .. N - 1 do
                        let node = {Node = i; GateLinks = []; ExtraCritical = false; LinksCount = 0}
                        for j in 0 .. N - 1 do
                            if nodes.[i * N + j] > 0 && (gateways |> Array.exists (fun g -> g = j)) then
                                node.GateLinks <- j::node.GateLinks
                        if node.GateLinks.Length > 1 then 
                            hasNearestGates node
                            yield node|]

while true do
    let SI = int(R()) (* The index of the node on which the Skynet agent is positioned this turn *)
    let res = findPath SI

    let nearestGateway = gateways |> Seq.filter (fun g -> res.[g].Dist.IsSome) 
                                  |> Seq.minBy (fun g -> res.[g].Dist.Value)
    let linkedCritical  = criticalNodes |> Array.filter (fun n -> n.GateLinks.Length > 1 && res.[n.Node].Dist.IsSome)


    if res.[nearestGateway].Dist.Value > 1 && linkedCritical.Length > 0 then
        let mutable filteredCritical = linkedCritical |> Array.filter(fun c -> agentInDangerZone c SI)
        if filteredCritical.Length = 0 then filteredCritical <- linkedCritical |> Array.filter(fun c -> c.ExtraCritical)
        if filteredCritical.Length = 0 then filteredCritical <- linkedCritical
        let maxNodeLinks = filteredCritical |> Array.maxBy(fun c -> c.LinksCount) |> fun c -> c.LinksCount
        filteredCritical <- filteredCritical |> Array.filter(fun c -> c.LinksCount = maxNodeLinks)
        let nearestCritical = filteredCritical |> Array.minBy(fun n -> res.[n.Node].Dist.Value)

        removeLink nearestCritical.Node nearestCritical.GateLinks.Head
        printfn "%d %d" nearestCritical.Node nearestCritical.GateLinks.Head
        nearestCritical.GateLinks <- nearestCritical.GateLinks.Tail
    else
        removeLink nearestGateway (res.[nearestGateway].Node)
        printfn "%d %d" nearestGateway (res.[nearestGateway].Node)
    ()
