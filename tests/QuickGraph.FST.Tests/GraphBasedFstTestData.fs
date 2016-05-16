module QuickGraph.FST.Tests.GraphBasedFstTestData

open Microsoft.FSharp.Collections
open QuickGraph.FST.GraphBasedFst
open QuickGraph.FSA.GraphBasedFsa

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

let firstFstCompos0 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl 'a', Smbl (int 'b')), 1)
    transitions.Add(1, (Smbl 'a', Smbl (int 'b')), 0)
    transitions.Add(1, (Smbl 'b', Smbl (int 'b')), 2)
    transitions.Add(1, (Smbl 'b', Smbl (int 'b')), 3)
    transitions.Add(2, (Smbl 'a', Smbl (int 'b')), 3)
    transitions.Add(3, (Smbl 'a', Smbl (int 'a')), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let secondFstCompos0 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl (int 'b'), Smbl 'b'), 1)
    transitions.Add(1, (Smbl (int 'b'), Smbl 'a'), 1)
    transitions.Add(1, (Smbl (int 'a'), Smbl 'b'), 2)
    transitions.Add(1, (Smbl (int 'a'), Smbl 'b'), 3)
    transitions.Add(2, (Smbl (int 'b'), Smbl 'a'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let resultFstCompos0 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl 'a', Smbl 'b'), 5)
    transitions.Add(5, (Smbl 'a', Smbl 'a'), 1)
    transitions.Add(5, (Smbl 'b', Smbl 'a'), 13)
    transitions.Add(5, (Smbl 'b', Smbl 'a'), 9)
    transitions.Add(1, (Smbl 'a', Smbl 'a'), 5)
    transitions.Add(9, (Smbl 'a', Smbl 'a'), 13)
    transitions.Add(13, (Smbl 'b', Smbl 'a'), 15)
    transitions.Add(13, (Smbl 'a', Smbl 'c'), 14)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let firstFstCompos1 =
    let startState = ResizeArray.singleton 0
    let finishState = new ResizeArray<_>([1; 2])
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl 'a', Smbl (int 'q')), 1)
    transitions.Add(0, (Smbl 'a', Smbl (int 'r')), 2)
    transitions.Add(1, (Smbl 'c', Smbl (int 's')), 1)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let secondFstCompos1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl (int 'q'), Smbl 'f'), 1)
    transitions.Add(0, (Smbl (int 'r'), Smbl 'h'), 2)
    transitions.Add(1, (Smbl (int 's'), Smbl 'g'), 2)
    transitions.Add(2, (Smbl (int 's'), Smbl 'j'), 2)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let resultFstCompos1 =
    let startState = ResizeArray.singleton 0
    let finishState = new ResizeArray<_>([2;3])
    let transitions = new ResizeArray<_>()
    transitions.Add(0, (Smbl 'a', Smbl 'f'), 1)
    transitions.Add(0, (Smbl 'a', Smbl 'h'), 2)
    transitions.Add(1, (Smbl 'c', Smbl 'g'), 3)
    transitions.Add(3, (Smbl 'c', Smbl 'j'), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst