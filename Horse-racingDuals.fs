(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
module HorseRacingDuals

open System

let N = int(Console.In.ReadLine())
let arr = [|for i in 0 .. N - 1 do yield int(Console.In.ReadLine())|] |> Array.sort
let mutable min = Int32.MaxValue
for i in 0 .. N - 2 do
    let diff = Math.Abs (arr.[i] - arr.[i + 1])
    if diff < min then min <- diff
()
    

(* Write an action using printfn *)
(* To debug: Console.Error.WriteLine("Debug message") *)

printfn "%d" min