module TheGift

open System
let R() = Console.In.ReadLine()

let N = int(R())
let mutable price = int(R())
let mutable sum = 0
let budgets = [|for i in 0 .. N - 1 do
                    let bud = int(R())
                    sum <- sum + bud
                    yield bud|] |> Array.sort
if sum < price then printfn "IMPOSSIBLE"
else
    for i in 0 .. N - 1 do
        if price/(N - i) >= budgets.[i] then
            price <- price - budgets.[i]
            printfn "%d" (budgets.[i])
        else
            printfn "%d" (price/(N - i))
            price <- price - price/(N - i)
