module YC.FST.Tests.GraphBasedFstTestData

open Microsoft.FSharp.Collections
open YC.FST.GraphBasedFst
open YC.FSA.GraphBasedFsa

let fst1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl '1', Smbl '1'), 1)
    transitions.Add(1, (Smbl '2', Smbl '+'), 2)
    transitions.Add(1, (Smbl '3', Smbl '-'), 2)
    transitions.Add(2, (Smbl '4', Smbl '4'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fst2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl '+', Smbl '1'), 1)
    transitions.Add(1, (Smbl '1', Smbl '+'), 2)
    transitions.Add(1, (Smbl '2', Smbl '-'), 2)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let multiInitfst =
    let startState = ResizeArray<_>([| 0; 1|])
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl '1', Smbl '1'), 1)
    transitions.Add(1, (Smbl '2', Smbl '+'), 2)
    transitions.Add(1, (Smbl '3', Smbl '-'), 2)
    transitions.Add(2, (Smbl '4', Smbl '4'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let multiFinishfst =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray<_>([| 2; 3; 4|])
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl '1', Smbl '1'), 1)
    transitions.Add(0, (Smbl '2', Smbl '1'), 1)
    transitions.Add(1, (Smbl '3', Smbl '-'), 2)
    transitions.Add(2, (Smbl '4', Smbl '4'), 3)
    transitions.Add(3, (Smbl '2', Smbl '+'), 4)
    transitions.Add(4, (Smbl '2', Smbl '+'), 1)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 4
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl 'a', Smbl (int 'a')), 1)
    transitions.Add(1, (Smbl 'b', Eps), 2)
    transitions.Add(2, (Smbl 'c', Eps), 3)
    transitions.Add(3, (Smbl 'd', Smbl (int 'd')), 4)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl (int 'a'), Smbl 'd'), 1)
    transitions.Add(1, (Eps, Smbl 'e'), 2)
    transitions.Add(2, (Smbl (int 'd'), Smbl 'a'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos12 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl 'a', Smbl (int 'a')), 0)
    transitions.Add(0, (Smbl 'a', Smbl (int 'b')), 1)
    transitions.Add(1, (Smbl 'b', Smbl (int 'a')), 3)
    transitions.Add(0, (Smbl 'b', Smbl (int 'b')), 2)
    transitions.Add(2, (Smbl 'b', Smbl (int 'b')), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos13 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl 'a', Smbl (int 'a')), 0)
    transitions.Add(0, (Smbl 'a', Smbl (int 'b')), 1)
    transitions.Add(1, (Smbl 'b', Smbl (int 'a')), 3)
    transitions.Add(0, (Smbl 'b', Smbl (int 'b')), 2)
    transitions.Add(2, (Smbl 'b', Smbl (int 'b')), 3)
    transitions.Add(3, (Smbl 'b', Smbl (int 'a')), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos22 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl (int 'b'), Smbl 'a'), 1)
    transitions.Add(1, (Smbl (int 'a'), Smbl 'd'), 1)
    transitions.Add(1, (Smbl (int 'b'), Smbl 'a'), 2)
    transitions.Add(1, (Smbl (int 'a'), Smbl 'c'), 2)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst