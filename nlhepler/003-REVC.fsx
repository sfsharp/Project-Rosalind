
let f (s : string) =
    let c a =
        match a with
        | 'A' -> "T"
        | 'C' -> "G"
        | 'G' -> "C"
        | 'T' -> "A"
        | _ -> "X"
    s |> Seq.toList |> List.rev |> Seq.map c |> String.concat ""
