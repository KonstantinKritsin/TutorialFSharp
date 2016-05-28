module StockExchangeLosses

open System

let R() = Console.In.ReadLine()

let n = int(R())
let words = R().Split [|' '|] |> Array.mapi (fun i w -> (i, int(w)))
let min = words |> Array.minBy snd
if (fst min) = 0 then printfn "0"
else
    let mutable loss = 0
    for i in 0 .. n - 2 do
        let currentloss = 
    let max = [|0 .. (fst min)|] |> Array.map (fun i -> words.[i]) |> Array.maxBy snd
    printfn "%d" ((snd min) - (snd max))
