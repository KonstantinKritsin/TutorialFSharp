module Test

open System

let throwException =
    function
    | 0 -> failwithf "Default exception"
    | 1 -> raise <| new ArgumentException("Invalid N")
    | 2 -> raise <| new ArgumentNullException()
    | 3 | 4 | 5 -> raise <| new ArgumentOutOfRangeException()
    | _ -> raise <| new ApplicationException()

let getExitCode =
    try
        let random = new Random(DateTime.Now.Millisecond)
        throwException <| random.Next 8

        0
    with
    // combine patterns
    | :? ArgumentNullException
    | :? ArgumentOutOfRangeException
        -> printfn "Argument was either null or out of range"
           1
    | :? ArgumentException as argEx
        -> printfn "Argument exception was caught: %s" argEx.Message
           2
    | _ as ex // wildcard match, 'ex' will be of type System.Exception
        -> printfn "Generic exception was caught: %s" ex.Message
           3

let detectValue point target =
    match point with
    | (a, b) when a = target && b = target -> printfn "Both values match target %d." target
    | (a, b) when a = target -> printfn "First value matched target in (%d, %d)" target b
    | (a, b) when b = target -> printfn "Second value matched target in (%d, %d)" a target
    | _ -> printfn "Neither value matches target."

detectValue (0, 0) 0
detectValue (1, 0) 0
detectValue (0, 10) 0
detectValue (10, 15) 0