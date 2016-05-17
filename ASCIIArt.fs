(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System
open System.Text

let Alf = dict [ 
            'A', 0;
            'B', 1;
            'C', 2;
            'D', 3;
            'E', 4;
            'F', 5;
            'G', 6;
            'H', 7;
            'I', 8;
            'J', 9;
            'K', 10;
            'L', 11;
            'M', 12;
            'N', 13;
            'O', 14;
            'P', 15;
            'Q', 16;
            'R', 17;
            'S', 18;
            'T', 19;
            'U', 20;
            'V', 21;
            'W', 22;
            'X', 23;
            'Y', 24;
            'Z', 25;
            '_', 26]

let L = int(Console.In.ReadLine())
let H = int(Console.In.ReadLine())
let T = Console.In.ReadLine().ToUpper()

let dic = Array.init H (fun i -> "")

for i in 0 .. H - 1 do
    dic.[i] <- Console.In.ReadLine()
    ()

let result = Array.init H (fun i -> "")

for i in 0 .. H - 1 do
    let tmp = new StringBuilder(T.Length * L)
    for ch in T do
        let ind = if Alf.ContainsKey(ch) then Alf.[ch] else Alf.['_']
        ignore(tmp.Append(dic.[i].[ind*L .. ind*L+L-1]))
        ()
    result.[i] <- tmp.ToString()
    ()
    
Console.Error.WriteLine(result.Length)
for i in 0 .. H - 1 do
    printfn "%s" result.[i]
