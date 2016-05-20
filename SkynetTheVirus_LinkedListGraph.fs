module SkynetTheVirus

open System

type Node(id: int) = 
    let mutable links: Node list = []
    let rec remove link lst = 
        match lst with
        | h::t when h = link -> t
        | h::t -> h::remove link t
        | _ -> []

    static member CreateLink (n1: Node) (n2: Node) = 
        n1.Add(n2)
        n2.Add(n1)

    static member RemoveLink (n1: Node) (n2: Node) =
        n1.Remove(n2)
        n2.Remove(n1)

    member this.Id = id

    member this.Links = links

    member this.Remove (link: Node) =
        links <- remove link links
        
    member this.Add (link: Node) =
        links <- link::links

type ResultStat = {mutable Visit: bool; mutable Dist: int option; mutable Node: int}

let findPath (nodes: System.Collections.Generic.IDictionary<int,Node>) fstNode = 
    let result = dict [for i in nodes.Keys do yield (i, {Visit = false; Dist = None; Node = 0})]
    result.[fstNode].Dist <- Some(0)
    while result |> Seq.exists(fun n -> not <| n.Value.Visit && n.Value.Dist.IsSome) do
        let currentNode = result |> Seq.filter (fun n -> not <| n.Value.Visit && n.Value.Dist.IsSome) |> Seq.sortBy(fun n -> n.Value.Dist.Value) |> Seq.head
        currentNode.Value.Visit <- true
        for node in nodes.[currentNode.Key].Links  do
            if result.[node.Id].Dist.IsNone || result.[node.Id].Dist.Value > currentNode.Value.Dist.Value + 1 then
                result.[node.Id].Dist <- Some(currentNode.Value.Dist.Value + 1)
                result.[node.Id].Node <- currentNode.Key
    result

let findLink (paths: System.Collections.Generic.IDictionary<int,ResultStat>) fromN toN = 
    let mutable current = toN
    while paths.[current].Node <> fromN do
        current <- paths.[current].Node
    (fromN, current)

(* N: the total number of nodes in the level, including the gateways *)
(* L: the number of links *)
(* E: the number of exit gateways *)
let token = (Console.In.ReadLine()).Split [|' '|]
let N = int(token.[0])
let nodes = dict [for i in 0 .. N - 1 do yield (i, new Node(i))]
let L = int(token.[1])
let E = int(token.[2])
for i in 0 .. L - 1 do
    (* N1: N1 and N2 defines a link between these nodes *)
    let token1 = (Console.In.ReadLine()).Split [|' '|]
    Node.CreateLink nodes.[int(token1.[0])] nodes.[int(token1.[1])]
    ()

(* the index of a gateway node *)
let gateways = [for i in 0 .. E - 1 do yield int(Console.In.ReadLine())]

(* game loop *)
while true do
    let SI = int(Console.In.ReadLine()) (* The index of the node on which the Skynet agent is positioned this turn *)
    //Console.Error.WriteLine("<-------------begin calculating------------------>")
    let res = findPath nodes SI
    //Console.Error.WriteLine("<-------------end calculating-------------------->")
    (* Write an action using printfn *)
    //Console.Error.WriteLine("total nodes count = {0}", nodes.Count)
    //for i in res do Console.Error.WriteLine("node {0}: distance from {3} is {1}. Prev node is {2}", i.Key, i.Value.Dist, i.Value.Node, SI)
    let geteway = gateways |> Seq.filter (fun g -> res.[g].Dist.IsSome) |> Seq.map (fun g -> (g, res.[g].Dist.Value)) |> Seq.sortBy snd |> Seq.head
    //Console.Error.WriteLine("geteway = {0}", geteway)
    let link = findLink res SI (fst geteway)
    //Console.Error.WriteLine("link = {0}", link)
    
    Node.RemoveLink nodes.[(fst link)] nodes.[(snd link)]
    (* Example: 0 1 are the indices of the nodes you wish to sever the link between *)
    printfn "%d %d" (fst link) (snd link)
    ()
