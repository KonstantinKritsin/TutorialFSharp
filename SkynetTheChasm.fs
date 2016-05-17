(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

let road = int(Console.In.ReadLine()) (* the length of the road before the gap. *)
let gap = int(Console.In.ReadLine()) (* the length of the gap. *)
let platform = int(Console.In.ReadLine()) (* the length of the landing platform. *)

let getCommand v = 
    match v with
    | (0, _) -> "SPEED"
    | (_, coordX) when road - 1 = coordX -> "JUMP"
    | (_, coordX) when coordX >= road + gap -> "SLOW"
    | (speed, coordX) when (road - coordX - 1) % (gap + 1) <> 0 || speed <> gap + 1 -> if speed < gap+1 then "SPEED" else "SLOW"
    | _ -> "WAIT"

(* game loop *)
while true do
    let speed = int(Console.In.ReadLine()) (* the motorbike's speed. *)
    let coordX = int(Console.In.ReadLine()) (* the position on the road of the motorbike. *)

    printfn "%s" (getCommand (speed, coordX))
    ()
