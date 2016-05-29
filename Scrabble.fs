module Scrabble

open System
let R() = Console.In.ReadLine()
let Alf = dict [ 
            'a', 1;
            'b', 3;
            'c', 3;
            'd', 2;
            'e', 1;
            'f', 4;
            'g', 2;
            'h', 4;
            'i', 1;
            'j', 8;
            'k', 5;
            'l', 1;
            'm', 3;
            'n', 1;
            'o', 1;
            'p', 3;
            'q', 10;
            'r', 1;
            's', 1;
            't', 1;
            'u', 1;
            'v', 4;
            'w', 4;
            'x', 8;
            'y', 4;
            'z', 10]

let N = int(R())
let words = [|for i in 0 .. N - 1 do
                let w = R()
                yield (w, w |> Seq.map(fun ch -> Alf.[ch]) |> Seq.sum)|]

let letters = R()
let acceptableWords = 
    words 
    |> Seq.mapi (fun i w -> (i, w)) 
    |> Seq.filter (fun (i, w) -> 
        let lettDic = new System.Collections.Generic.Dictionary<char, int>(letters |> Seq.groupBy(fun ch -> ch) |> Seq.map (fun (k, v) -> (k, (Seq.length v))) |> dict);
        (fst w) |> Seq.forall(fun wordCh ->
            if lettDic.ContainsKey wordCh && lettDic.[wordCh] > 0 then
                lettDic.[wordCh] <- lettDic.[wordCh] - 1
                true
            else false))
let maxCost = acceptableWords |> Seq.map snd |> Seq.maxBy snd |> snd
let word = acceptableWords |> Seq.filter(fun (i, w) -> (snd w) = maxCost) |> Seq.minBy fst |> snd |> fst
printfn "%s" word