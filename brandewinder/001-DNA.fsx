(*
Teamwork with Dan and Kim, at the San Francisco F# Coding Breakfast
on March 12, 2014: http://www.meetup.com/sfsharp/events/166845672/
*)

let input = "AGCTTTTCATTCTGACTGCAACGGGCAATATGTCTCTGTGTGGATTAAAAAAAGAGTGTCTGATAGCAGC"
let output = "20 12 17 21"

let explicitVersion (dna:string) =
    input 
    |> Seq.countBy (fun x -> x)
    |> Seq.sortBy (fun (letter,_) -> letter)
    |> Seq.map (fun (_,count) -> count)
    |> Seq.map (fun x -> string x)
    |> String.concat " "

printfn "Verification: %b" (explicitVersion input = output)

let compactVersion (dna:string) =
    input 
    |> Seq.countBy id
    |> Seq.sortBy fst 
    |> Seq.map (snd >> string)
    |> String.concat " "

printfn "Verification: %b" (compactVersion input = output)