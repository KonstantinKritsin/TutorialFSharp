module TeadsSponsoredContest
open System
open System.Collections.Generic

let R() = Console.In.ReadLine()

type Node(id: int) = 
    let mutable links: Node list = []
    member this.Id = id
    member this.Links = links
    member this.Add (link: Node) =
        links <- link::links

let rec GetMaxDistance (from: Node) (a: Node) =
    a.Links |> Seq.filter (fun l -> l <> from) |> Seq.map (fun l -> 1 + (GetMaxDistance a l)) |> Seq.append [0] |> Seq.max

let linkCount = int(R()) (* the number of adjacency relations *)
let nodes = new Dictionary<int, Node>()

for i in 0 .. linkCount - 1 do
    let inputs = R().Split [|' '|]
    let xi = int(inputs.[0])
    let yi = int(inputs.[1]); // the ID of a person which is adjacent to xi
    let mutable nodeX:Node = Unchecked.defaultof<Node>
    let mutable nodeY:Node = Unchecked.defaultof<Node>
    if not <| nodes.TryGetValue(xi, &nodeX) then
        nodeX <- new Node(xi)
        nodes.Add(xi, nodeX)
    if not <| nodes.TryGetValue(yi, &nodeY) then
        nodeY <- new Node(yi)
        nodes.Add(yi, nodeY)
    nodeX.Add nodeY
    nodeY.Add nodeX
    ()

let leaf = nodes.Values |> Seq.find (fun n -> n.Links.Length = 1)
let maxDist = GetMaxDistance leaf leaf

(* The minimal amount of steps required to completely propagate the advertisement *)
printfn "%d" <| int(Math.Round(float(maxDist)/float(2),  MidpointRounding.AwayFromZero))