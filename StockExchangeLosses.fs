module StockExchangeLosses

open System

let R() = Console.In.ReadLine()

let n = int(R())
let t =  R()
let words = t.Split [|' '|] |> Array.map int
let mutable max = 0
let mutable minAfterMax = 0
let mutable maxDiff = 0

for w in words do
    if w > max then
        max <- w
        minAfterMax <- w
    else
        if w < minAfterMax then minAfterMax <- w
        if minAfterMax - max < maxDiff then maxDiff <- minAfterMax - max
printfn "%d" maxDiff