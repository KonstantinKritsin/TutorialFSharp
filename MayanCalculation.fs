module MayanCalculation

open System
let R() = Console.In.ReadLine()

let token = R().Split [|' '|]
let L = int(token.[0])
let H = int(token.[1])
let digs = Array2D.init<string> H 20 (fun i j -> "")
let readWord S =
    let word = Array.zeroCreate<int> (S/H)
    for i in 0 .. (S/H) - 1 do
        let mutable indexes = {0..19}
        for j in 0 .. H - 1 do
            let num1Line = R()
            indexes <- indexes |> Seq.filter (fun k -> digs.[j,k] = num1Line)
        word.[(S/H) - 1 - i] <- indexes |> Seq.head
    word
let rec intPow d p =
    if p = 0 then 1
    else d * intPow d (p - 1)

let getNum (word: int[]) =
    let mutable res = 0
    for i in 0..word.Length - 1 do res <- res + word.[i] * (intPow 20 i)
    res

for i in 0 .. H - 1 do
    let num = R()
    for j in 0 .. 19 do digs.[i, j] <- num.[j*L .. (j+1)*L-1]

let rec slide (dig:int64) (b:int) =
    if dig < int64(b) then 0
    else 1 + slide (dig/int64(b)) b

let rec printResult dig pow =
    let mult = intPow 20 pow
    if mult = 0 then [0..H - 1] |> Seq.map (fun i -> digs.[i,0]) |> Seq.iter (printfn "%s")
    if mult = 1 then [0..H - 1] |> Seq.map (fun i -> digs.[i,int(dig)]) |> Seq.iter (printfn "%s")
    else
        [0..H - 1] |> Seq.map (fun i -> digs.[i,int(dig/int64(mult))]) |> Seq.iter (printfn "%s")
        printResult (dig % int64(mult)) (pow - 1)

let S1 = int(R())
let word1 = readWord S1
let num1 = getNum word1

let S2 = int(R())
let word2 = readWord S2
let num2 = getNum word2

let opResult = match R() with
                | "+" -> int64(num1) + int64(num2)
                | "*" -> int64(num1) * int64(num2)
                | "/" -> int64(num1) / int64(num2)
                | "-" -> int64(num1) - int64(num2)
                | _ -> failwith "wrong operation"

printResult opResult (slide opResult 20)
