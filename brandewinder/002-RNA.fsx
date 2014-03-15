(*
Teamwork with Dan and Kim, at the San Francisco F# Coding Breakfast
on March 12, 2014: http://www.meetup.com/sfsharp/events/166845672/
*)

let input = "GATGGAACTTGACTACGTAAATT"
let output = "GAUGGAACUUGACUACGUAAAUU"

open System
let firstVersion (rna:string) = rna.Replace('T','U')

printfn "Verification: %b" (firstVersion input = output)

// Now without using System - not quite correct just yet.
let secondVersion (rna:string) = 
    rna 
    |> Seq.map (fun x -> if x = 'T' then 'U' else x) 
    |> string

printfn "Verification: %b" (secondVersion  input = output)