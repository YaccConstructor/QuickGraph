module YC.FSA.Tests.GraphBasedFsaTestData

open Microsoft.FSharp.Collections
open YC.FSA.GraphBasedFsa

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

let fsaAcceptingOneLetter =
    let initSt = ResizeArray.singleton 0
    let finalSt = ResizeArray.singleton 1
    let transitions = ResizeArray<_>()
    do transitions.Add(0, Smbl('a', 11), 1)
    FSA<_>.Create(initSt, finalSt, transitions)