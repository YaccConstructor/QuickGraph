module YC.FSA.Tests.GraphBasedFsaTestData

open Microsoft.FSharp.Collections
open YC.FSA.GraphBasedFsa

let fsa1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_>(SmblFSA('1', 11)), 1)
    transitions.Add(1, new EdgeLbl<_>(SmblFSA('2', 12)), 2)
    transitions.Add(1, new EdgeLbl<_>(SmblFSA('3', 12)), 2)
    transitions.Add(2, new EdgeLbl<_>(SmblFSA('4', 14)), 3)
    new FSA<_>(startState, finishState, transitions)

let fsaInters1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 5
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_>(SmblFSA('a', 11)), 1)
    transitions.Add(1, new EdgeLbl<_>(SmblFSA('b', 12)), 2)
    transitions.Add(2, new EdgeLbl<_>(SmblFSA('c', 12)), 3)
    transitions.Add(3, new EdgeLbl<_>(SmblFSA('d', 14)), 4)
    transitions.Add(4, new EdgeLbl<_>(SmblFSA('e', 11)), 5)
    transitions.Add(1, new EdgeLbl<_>(SmblFSA('b', 12)), 4)
    new FSA<_>(startState, finishState, transitions)

let fsaInters2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_>(SmblFSA('a', 11)), 1)
    transitions.Add(1, new EdgeLbl<_>(SmblFSA('b', 12)), 2)
    transitions.Add(2, new EdgeLbl<_>(SmblFSA('e', 12)), 3)
    transitions.Add(1, new EdgeLbl<_>(SmblFSA('c', 14)), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_>(SmblFSA('a', 11)), 1)
    transitions.Add(1, new EdgeLbl<_>(SmblFSA('b', 12)), 2)
    new FSA<_>(startState, finishState, transitions)

let fsaRepl3 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_>(SmblFSA('m', 11)), 1)
    new FSA<_>(startState, finishState, transitions)