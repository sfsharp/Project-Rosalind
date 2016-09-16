(*
Project Rosalind, "Calculating Protein Mass"
http://rosalind.info/problems/prtm/

Pairing with @panesofglass on 9/14/2016 at the F# Coding Breakfast
http://www.meetup.com/sfsharp/events/233833109/

Solving it, then toying around with performance :)
*)

open System

// http://rosalind.info/glossary/monoisotopic-mass-table/
let raw = """A   71.03711
C   103.00919
D   115.02694
E   129.04259
F   147.06841
G   57.02146
H   137.05891
I   113.08406
K   128.09496
L   113.08406
M   131.04049
N   114.04293
P   97.05276
Q   128.05858
R   156.10111
S   87.03203
T   101.04768
V   99.06841
W   186.07931
Y   163.06333"""

let alphabet,weights = 
    raw.Split '\n'
    |> Array.map (fun line -> 
        let [|a;b|] = line.Replace("   "," ").Split(' ')
        char a, float b)
    |> Array.unzip

// version 1
let weight1 (input:string) =
    input.ToCharArray()
    |> Array.fold (fun acc x ->
        let index = alphabet |> Array.findIndex ((=) x)
        let weight = weights.[index]
        acc + weight
    ) 0.

let testExample = "SKADYEK"
let expected = 821.392

// fails because of rounding
weight1 testExample = expected


// version 2: directly access weights by index,
// using the char index as index

let size = alphabet |> Seq.map int |> Seq.max |> (+) 1
let indexedWeights = Array.init size (fun _ -> 0.)

alphabet
|> Array.iteri (fun i c ->
    let w = weights.[i]
    printfn "%i" (int c)
    indexedWeights.[int c] <- w
    )

let weight2 (input:string) =
    input
    |> Seq.sumBy (fun c -> indexedWeights.[int c])

weight2 testExample

let weight3 (input:string) =
    let mutable total = 0.
    for c in input do
        total <- total + indexedWeights.[int c]
    total

weight3 testExample

// a less tiny sample

let sample = "HVKPSPFHSRDSCWLHTCYASWIEFDAMNAKGTDWALGQTDWSIRTWHHLPFEGCETPPSPEHWLYTAKHFVPKPTHGMSVATIEKGMLMHITFPYILPMQFWVCSSFRGFYWVPKLVMECGNAGPTISQYYVMWDGHTSACGETWALPVHFWLGDKVVCIMDDFCWNHCSKNTFYTSRVMMIASHVRINMYQQCGMNHFRGGNKYKEILLLSAIGIRHLCLAPTTHGWSTRTHCTVVQRECWKMTYVGWAREWDDSMKACIDDIQKEEYWQWCKAPPPYVTGGRTNLYCLGIIWVLKGIIQWPRVRRLHTVPAVIQMLKFSAYVSCILDDCKPPSWGGVVKLAAHEFFGKVHFMEVGYGMTMRCPHSYHPRQPEDTAPSMMCDCEGNMSKCDGTCSVAISQPQLDWMQPICRWELAYAQHPDDIGQTQSAHSAYFNIAMFTRGKPVTRDSAKCIIDNQVEVGIQPNGCECTGRDEMLVSGESNHMQFANEQAKGVEYTCKKPIWMVRALRDYKIILEVTIPRVKTRNGVYMNPYQSLQMYGDYSMPHKAGAYTPYFNTCMEVTCPMKSLLSGLHFPKWFSFAASFKHDHKENWNTGKVMDCPERWSQYIFPYNIAFWAPWEHYSWNMNLESMERASKGATWPKDAARIYRLFYRGEWWPDDEDRFSKKEQFGPEEPRRRHMKRRTIVIEHMLLYDAYRRDCQCENKMLQDEMQKTPSMMKFMNMFGVKKSNRTRPMVEGSWVRNRHMQGHHAMPHALYLVQKLNHHTYYMSYRECRGRCYQAWHGSKYLWFYCHCVFDVVGCLYHSWMPIRAVCWQNPPEPMSMGPNYSILVDETYRPLHRLDVWFEADPNTRLELHQLYGLNLPFDWQMECWFGFCEDDDWGIQACSDCFIMGQNWCPPVVLPWIGHKCEIYIKGFPIFPCLQSRSTRGVIPGGNMEMKQAKFCTIEFSLI"

#time "on"

[0..99999]
|> List.iter (fun _ -> weight1 sample |> ignore)
// Real: 00:00:05.174, CPU: 00:00:05.140, GC gen0: 1182, gen1: 1, gen2: 0

[0..99999]
|> List.iter (fun _ -> weight2 sample |> ignore)
// Real: 00:00:00.772, CPU: 00:00:00.750, GC gen0: 1, gen1: 0, gen2: 0

[0..99999]
|> List.iter (fun _ -> weight3 sample |> ignore)
// Real: 00:00:00.136, CPU: 00:00:00.187, GC gen0: 0, gen1: 0, gen2: 0

let time1 = TimeSpan(0,0,0,5,174)
let time2 = TimeSpan(0,0,0,0,772)
let time3 = TimeSpan(0,0,0,0,136)

printfn "Pass 1: %.2f percent faster (speedup x%.2f)" (1. - time2.TotalSeconds / time1.TotalSeconds) (time1.TotalSeconds / time2.TotalSeconds)
printfn "Pass 2: %.2f percent faster (speedup x%.2f)" (1. - time3.TotalSeconds / time1.TotalSeconds) (time1.TotalSeconds / time3.TotalSeconds)
