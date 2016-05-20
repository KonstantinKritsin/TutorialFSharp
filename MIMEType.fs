(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
module MIMEType

open System

let N = int(Console.In.ReadLine()) (* Number of elements which make up the association table. *)
let Q = int(Console.In.ReadLine()) (* Number Q of file names to be analyzed. *)
let map = dict [for i in 0 .. N - 1 do
                let token = (Console.In.ReadLine()).Split [|' '|]
                yield (token.[0].ToLower(), token.[1])]



for i in 0 .. Q - 1 do
    let file = (Console.In.ReadLine().Split [|'.'|])
    if file.Length > 1 && map.ContainsKey(file.[file.Length - 1].ToLower()) then
        printfn "%s" map.[file.[file.Length - 1].ToLower()]
    else
        printfn "UNKNOWN"
    ()
