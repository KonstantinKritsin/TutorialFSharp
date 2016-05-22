module HeatDetector

open System
let R() = Console.In.ReadLine()
(* W: width of the building. *)
(* H: height of the building. *)
let token = R().Split [|' '|]
let W = int(token.[0])
let H = int(token.[1])
let N = int(R()) (* maximum number of turns before game over. *)
let token1 = R().Split [|' '|]
let mutable X0 = int(token1.[0])
let mutable Y0 = int(token1.[1])
let direction = [|"D"; "R"|]
let prevCoord = [|0; 0|]
let border = [|0; 0; W; H|]

(* game loop *)
while true do
    let bombDir = Console.In.ReadLine().ToUpper() (* the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL) *)
    if bombDir.Contains("U") then
        Y0 <- Y0 - int(Math.Round(float(Y0 - border.[1])/float(2), MidpointRounding.AwayFromZero))
        if prevCoord.[1] <> 0 then border.[3] <- prevCoord.[1]
        direction.[0] <- "U"
        prevCoord.[1] <- Y0
    if bombDir.Contains("D") then
        Y0 <- Y0 + int(Math.Round(float(border.[3] - 1 - Y0)/float(2), MidpointRounding.AwayFromZero))
        if prevCoord.[1] <> 0 then border.[1] <- prevCoord.[1]
        direction.[0] <- "D"
        prevCoord.[1] <- Y0
    if bombDir.Contains("L") then
        X0 <- X0 - int(Math.Round(float(X0 - border.[0])/float(2), MidpointRounding.AwayFromZero))
        if prevCoord.[0] <> 0 then border.[2] <- prevCoord.[0]
        direction.[1] <- "L"
        prevCoord.[0] <- X0
    if bombDir.Contains("R") then 
        X0 <- X0 + int(Math.Round(float(border.[2] - 1 - X0)/float(2), MidpointRounding.AwayFromZero))
        if prevCoord.[0] <> 0 then border.[0] <- prevCoord.[0]
        direction.[1] <- "R"
        prevCoord.[0] <- X0
    printfn "%d %d" X0 Y0
    ()
