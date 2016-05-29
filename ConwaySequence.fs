module ConwaySequence

(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System
open System.Text

let R() = Console.In.ReadLine()

let mutable sb = new StringBuilder(R())
let L = int(R())

for i in 1 .. L - 1 do
    let currentSb = new StringBuilder()
    let digArray = sb.ToString().Split [|' '|] |> Array.map int
    let mutable value = digArray.[0]
    let mutable cnt = 0
    for j in 0 .. digArray.Length - 1 do 
        if digArray.[j] = value then
            cnt <- cnt + 1
        else
            if currentSb.Length > 0 then ignore (currentSb.Append(' '))
            else ignore (currentSb.AppendFormat("{0} {1}", cnt, value))
            value <- digArray.[j]
            cnt <- 1
    sb <- currentSb
(* Write an action using printfn *)
(* To debug: Console.Error.WriteLine("Debug message") *)

printfn "%s" (sb.ToString())