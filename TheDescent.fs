(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
module TheDescent

open System

let init n = Array.init n (fun i -> int(Console.In.ReadLine()))

(* game loop *)
while true do
    let ind = init 8 |> Seq.mapi (fun i x -> i, x) |> Seq.maxBy snd |> fst

    printfn "%d" ind (* The number of the mountain to fire on. *)
    ()
