module QuickGraph.FSA.Tests.GraphBasedFsaTestData

open Microsoft.FSharp.Collections
open QuickGraph.FSA.GraphBasedFsa

let fsa1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('1', 11), 1)
    transitions.Add(1, Smbl('2', 12), 2)
    transitions.Add(1, Smbl('3', 12), 2)
    transitions.Add(2, Smbl('4', 14), 3)
    new FSA<_>(startState, finishState, transitions)

let fsaInters1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 5
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('b', 12), 2)
    transitions.Add(2, Smbl('c', 12), 3)
    transitions.Add(3, Smbl('d', 14), 4)
    transitions.Add(4, Smbl('e', 11), 5)
    transitions.Add(1, Smbl('b', 12), 4)
    new FSA<_>(startState, finishState, transitions)

let fsaInters2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('b', 12), 2)
    transitions.Add(2, Smbl('e', 12), 3)
    transitions.Add(1, Smbl('c', 14), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('b', 12), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl3 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('m', 11), 1)
    new FSA<_>(startState, finishState, transitions)

let fsawoInters1 = fsaRepl2

let fsawoInters2 = 
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('e', 11), 1)
    transitions.Add(1, Smbl('f', 12), 2)
    transitions.Add(1, Smbl('g', 12), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl1C =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 6
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('b', 12), 2)
    transitions.Add(2, Smbl('c', 12), 3)
    transitions.Add(3, Smbl('d', 14), 4)
    transitions.Add(1, Smbl('f', 11), 5)
    transitions.Add(5, Smbl('g', 12), 4)
    transitions.Add(4, Smbl('e', 12), 6)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2C =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('f', 11), 1)
    transitions.Add(0, Smbl('b', 12), 1)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2C2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('f', 11), 1)
    transitions.Add(0, Smbl('b', 12), 2)
    transitions.Add(2, Smbl('c', 12), 1)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2C3 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('r', 11), 1)
    transitions.Add(0, Smbl('b', 12), 2)
    transitions.Add(2, Smbl('c', 12), 1)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2C4 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('r', 11), 1)
    transitions.Add(0, Smbl('e', 12), 2)
    transitions.Add(2, Smbl('c', 12), 1)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl1C5 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('b', 12), 1)
    transitions.Add(1, Smbl('c', 12), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2C5 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('c', 12), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl1C6 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 4
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('b', 11), 1)
    transitions.Add(1, Smbl('a', 12), 2)
    transitions.Add(2, Smbl('a', 12), 3)
    transitions.Add(3, Smbl('b', 12), 4)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2C6 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('a', 12), 1)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl1C7 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 6
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('b', 12), 2)
    transitions.Add(2, Smbl('a', 11), 3)
    transitions.Add(3, Smbl('b', 12), 4)
    transitions.Add(4, Smbl('a', 11), 5)
    transitions.Add(5, Smbl('a', 11), 6)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl2C7 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('a', 11), 2)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl3C7 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('c', 13), 1)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl1C8 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 4
    finishState.Add 5 |> ignore
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('c', 11), 1)
    transitions.Add(1, Smbl('a', 12), 2)
    transitions.Add(2, Smbl('a', 12), 3)
    transitions.Add(3, Smbl('a', 12), 4)
    transitions.Add(3, Smbl('b', 12), 5)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl2C8 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('c', 11), 1)
    transitions.Add(1, Smbl('a', 12), 1)
    new FSA<_>(startState, finishState, transitions)
    
let fsaRepl1C9 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 0)
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(0, Smbl('c', 13), 1)
    transitions.Add(1, Smbl('b', 12), 0)
    transitions.Add(1, Smbl('b', 12), 1)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl2C9 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 0)
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('b', 12), 0)
    transitions.Add(1, Smbl('b', 12), 1)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl1C10 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 5
    finishState.Add 6 |> ignore
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 11), 1)
    transitions.Add(1, Smbl('c', 12), 2)
    transitions.Add(0, Smbl('x', 13), 3)
    transitions.Add(3, Smbl('y', 14), 2)
    transitions.Add(2, Smbl('d', 05), 4)
    transitions.Add(4, Smbl('k', 15), 4)
    transitions.Add(4, Smbl('a', 16), 5)
    transitions.Add(4, Smbl('c', 17), 6)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl2C10 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    finishState.Add 3 |> ignore
    finishState.Add 4 |> ignore
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('d', 18), 1)
    transitions.Add(1, Smbl('k', 08), 4)
    transitions.Add(0, Smbl('a', 22), 2)
    transitions.Add(2, Smbl('k', 23), 5)
    transitions.Add(5, Smbl('k', 24), 2)
    transitions.Add(2, Smbl('c', 20), 3)
    transitions.Add(3, Smbl('d', 21), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl1C10a =
    let startState = ResizeArray.singleton 2
    let finishState = ResizeArray.singleton 5
    finishState.Add 6 |> ignore
    let transitions = new ResizeArray<_>()
    transitions.Add(2, Smbl('d', 05), 4)
    transitions.Add(4, Smbl('k', 15), 4)
    transitions.Add(4, Smbl('a', 16), 5)
    transitions.Add(4, Smbl('c', 17), 6)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl2C10a =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('d', 22), 2)
    transitions.Add(2, Smbl('k', 23), 5)
    transitions.Add(5, Smbl('k', 24), 2)
    transitions.Add(2, Smbl('c', 20), 3)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl1C11 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 05), 1)
    transitions.Add(1, Smbl('b', 15), 2)
    transitions.Add(2, Smbl('b', 16), 3)
    new FSA<_>(startState, finishState, transitions)
   
let fsaRepl2C11 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('a', 22), 1)
    transitions.Add(1, Smbl('b', 23), 2)
    transitions.Add(0, Smbl('b', 24), 2)
    transitions.Add(2, Smbl('b', 24), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaAcceptingOneLetter =
    let initSt = ResizeArray.singleton 0
    let finalSt = ResizeArray.singleton 1
    let transitions = ResizeArray<_>()
    do transitions.Add(0, Smbl('a', 11), 1)
    FSA<_>(initSt, finalSt, transitions)

let anyNonEmptyWordsFsa =
    let initSt = ResizeArray.singleton 0
    let finalSt = ResizeArray.singleton 1
    let transitions = [
        (0, Smbl('a', 11), 1); 
        (1, Smbl('a', 11), 1) ]
    FSA<_>(initSt, finalSt, ResizeArray.ofList transitions)

let fsaEmpty =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 4
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smbl('b', 11), 1)
    transitions.Add(1, Smbl('a', 12), 2)
    transitions.Add(3, Smbl('b', 12), 4)
    new FSA<_>(startState, finishState, transitions)