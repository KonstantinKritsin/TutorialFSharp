module NetworkCabling

open System
let R() = Console.In.ReadLine()


let N = int(R())
let rect: int64 option[] = [|None;None;None;None|]
let yCoord = new System.Collections.Generic.Dictionary<int64, int>()
let points = [|for i in 0 .. N - 1 do
                    let p = R().Split [|' '|]
                    let x = int64(p.[0])
                    if rect.[1].IsNone || rect.[1].Value < x then rect.[1] <- Some(x)
                    if rect.[3].IsNone || rect.[3].Value > x then rect.[3] <- Some(x)
                    let y = int64(p.[1])
                    if rect.[0].IsNone || rect.[0].Value < y then rect.[0] <- Some(y)
                    if rect.[2].IsNone || rect.[2].Value > y then rect.[2] <- Some(y)
                    if not (yCoord.ContainsKey y) then yCoord.Add(y, 1)
                    else yCoord.[y] <- yCoord.[y] + 1
                    yield (x, y)|]
let orderedCoords = yCoord |> Seq.map (fun i -> i.Key) |> Seq.sort |> Seq.toArray
let mutable cnt = 0
let mutable y = rect.[2].Value
let mutable result = rect.[1].Value - rect.[3].Value
let mutable ind = 0

while cnt < (N / 2 + 1) && ind < orderedCoords.Length do
    cnt <- cnt + yCoord.[y]
    ind <- ind + 1
    if ind < orderedCoords.Length then
        y <- orderedCoords.[ind]
y <- orderedCoords.[ind - 1]
for h in (points |> Seq.map snd |> Seq.filter(fun p -> p <> y)) do
    result <- result + Math.Abs (y - h)

printfn "%d" result