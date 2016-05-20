(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
module Temperatures

open System

let n = int(Console.In.ReadLine()) (* the number of temperatures to analyse *)
let temps = Console.In.ReadLine() (* the n temperatures expressed as integers ranging from -273 to 5526 *)

if temps.Length = 0 then
    printfn "0"
else
    let result = 
        temps.Split([|" "|], StringSplitOptions.RemoveEmptyEntries) 
        |> Seq.map (fun x -> int(x), Math.Abs(int(x)))
        |> Seq.groupBy snd
        |> Seq.sortBy fst
        |> Seq.map (fun (key, value) -> value |> Seq.sortBy fst |> Seq.last)
        |> Seq.head
    printfn "%d" (fst result)

