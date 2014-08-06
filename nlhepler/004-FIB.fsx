
let f (n : int) (k : int) =
    // st : (# young, # reproductive)
    let rec f_ (n : int) st =
        let (y, r) = st
        match n with
        | 1 -> y + r 
        | _ ->
            let st' = (r * k, r + y)
            f_ (n - 1) st'
    f_ n (1, 0)

let test = f 5 3 = 19
