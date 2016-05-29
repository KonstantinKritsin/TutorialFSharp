module TelephoneNumbers
open System
type Node(dig: int option) =
    let mutable _dig = if dig.IsNone then -1 else dig.Value
    let _dic = new System.Collections.Generic.Dictionary<int, Node>()

    member this.Dig() = _dig

    member this.AddNumber = function
        | null | "" -> ()
        | num ->
            let d = int(num.[0].ToString())
            match _dic.TryGetValue(d) with
                | true, n -> n.AddNumber num.[1..]
                | _ -> let n = new Node(Some(d))
                       n.AddNumber num.[1..]
                       _dic.Add(d, n)
    member this.NodeCount() =
        let current = if _dig >= 0 then 1 else 0
        if _dic.Count = 0 then current
        else (_dic.Values |> Seq.sumBy(fun n -> n.NodeCount())) + current

let R() = Console.In.ReadLine()

let root = new Node(None)
let N = int(R())
for i in 0 .. N - 1 do
    root.AddNumber (R())
    ()

printfn "%d" (root.NodeCount())