module YC.FST.Tests.GraphBasedFstTestData

open Microsoft.FSharp.Collections
open YC.FST.GraphBasedFst
open YC.FSA.GraphBasedFsa

let fst1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl '1', Smbl '1'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl '2', Smbl '+'), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl '3', Smbl '-'), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Smbl '4', Smbl '4'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fst2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl '+', Smbl '1'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl '1', Smbl '+'), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl '2', Smbl '-'), 2)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let multiInitfst =
    let startState = ResizeArray<_>([| 0; 1|])
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl '1', Smbl '1'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl '2', Smbl '+'), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl '3', Smbl '-'), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Smbl '4', Smbl '4'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let multiFinishfst =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray<_>([| 2; 3; 4|])
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl '1', Smbl '1'), 1)
    transitions.Add(0, new EdgeLbl<_,_>(Smbl '2', Smbl '1'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl '3', Smbl '-'), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Smbl '4', Smbl '4'), 3)
    transitions.Add(3, new EdgeLbl<_,_>(Smbl '2', Smbl '+'), 4)
    transitions.Add(4, new EdgeLbl<_,_>(Smbl '2', Smbl '+'), 1)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 4
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl 'a', Smbl 'a'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl 'b', Eps), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Smbl 'c', Eps), 3)
    transitions.Add(3, new EdgeLbl<_,_>(Smbl 'd', Smbl 'd'), 4)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl 'a', Smbl 'd'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Eps, Smbl 'e'), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Smbl 'd', Smbl 'a'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos12 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl 'a', Smbl 'a'), 0)
    transitions.Add(0, new EdgeLbl<_,_>(Smbl 'a', Smbl 'b'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl 'b', Smbl 'a'), 3)
    transitions.Add(0, new EdgeLbl<_,_>(Smbl 'b', Smbl 'b'), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Smbl 'b', Smbl 'b'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos22 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Smbl 'b', Smbl 'a'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl 'a', Smbl 'd'), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl 'b', Smbl 'a'), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Smbl 'a', Smbl 'c'), 2)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst