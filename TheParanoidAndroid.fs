module TheParanoidAndroid

(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

(* nbFloors: number of floors *)
(* width: width of the area *)
(* nbRounds: maximum number of rounds *)
(* exitFloor: floor on which the exit is found *)
(* exitPos: position of the exit on its floor *)
(* nbTotalClones: number of generated clones *)
(* nbAdditionalElevators: ignore (always zero) *)
(* nbElevators: number of elevators *)
let token = (Console.In.ReadLine()).Split [|' '|]
let nbFloors = int(token.[0])
let width = int(token.[1])
let nbRounds = int(token.[2])
let exitFloor = int(token.[3])
let exitPos = int(token.[4])
let nbTotalClones = int(token.[5])
let nbAdditionalElevators = int(token.[6])
let nbElevators = int(token.[7])
(* elevatorFloor: floor on which this elevator is found *)
(* elevatorPos: position of the elevator on its floor *)
let elevatorCoords = [|for i in 0 .. nbElevators - 1 do
                        let token1 = (Console.In.ReadLine()).Split [|' '|]
                        yield (int(token1.[0]), int(token1.[1]))|]


(* game loop *)
while true do
    (* cloneFloor: floor of the leading clone *)
    (* clonePos: position of the leading clone on its floor *)
    (* direction: direction of the leading clone: LEFT or RIGHT *)
    let token2 = (Console.In.ReadLine()).Split [|' '|]
    let cloneFloor = int(token2.[0])
    let clonePos = int(token2.[1])
    let direction = token2.[2]

    let neededDirection = 
        match cloneFloor with
        | -1 -> direction
        | floor when floor = exitFloor -> if clonePos < exitPos then "RIGHT" else "LEFT"
        | _ -> 
            let elevPos = elevatorCoords |> Seq.filter(function
                        | (elFloor, pos) when elFloor = cloneFloor -> true
                        | _ -> false) |> Seq.head |> snd
            if clonePos < elevPos then "RIGHT" else if clonePos > elevPos then "LEFT" else direction

    if direction <> neededDirection then printfn "BLOCK"
    else printfn "WAIT" (* action: WAIT or BLOCK *)
    ()
