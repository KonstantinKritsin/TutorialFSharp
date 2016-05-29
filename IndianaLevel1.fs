module IndianaLevel1
open System

let R() = Console.In.ReadLine()
let token = R().Split [|' '|]
let W = int(token.[0])
let H = int(token.[1])
let grid = [|for i in 0 .. H - 1 do
                for j in (R().Split [|' '|]) do yield int(j)|]

let findPath token =
    match token with
    | (t, "TOP") when [|1;3;7;9|] |> Array.exists (fun i -> i = t) -> "DOWN"
    | (t, "TOP") when [|4;10|] |> Array.exists (fun i -> i = t) -> "LEFT"
    | (t, "TOP") when [|5;11|] |> Array.exists (fun i -> i = t) -> "RIGHT"
    | (t, "LEFT") when [|1;5;8;9;13|] |> Array.exists (fun i -> i = t) -> "DOWN"
    | (t, "LEFT") when [|2;6|] |> Array.exists (fun i -> i = t) -> "RIGHT"
    | (t, "RIGHT") when [|1;4;7;8;12|] |> Array.exists (fun i -> i = t) -> "DOWN"
    | (t, "RIGHT") when [|2;6|] |> Array.exists (fun i -> i = t) -> "LEFT"
    | _ -> failwith "wrong input"

let EX = int(R())

(* game loop *)
while true do
    let token1 = R().Split [|' '|]
    let XI = int(token1.[0])
    let YI = int(token1.[1])
    let nodeType = grid.[YI * W + XI]
    let POS = token1.[2]
    Console.Error.WriteLine("nodeType={0}; POS={1}", nodeType, POS)
    let direction = findPath (nodeType, POS)
    let (x, y) = match direction with
                 | "DOWN" -> (XI, YI + 1)
                 | "LEFT" -> (XI - 1, YI)
                 | "RIGHT" -> (XI + 1, YI)
                 | _ -> failwith "wrong direction"
    printfn "%d %d" x y
    ()


