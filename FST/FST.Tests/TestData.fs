module YC.FST.TestData

open Microsoft.FSharp.Collections
open YC.FST.GraphBasedFST

let fst1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "1", Some "1"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "2", Some "+"), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Some "3", Some "-"), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Some "4", Some "4"), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fst2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "+", Some "1"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "1", Some "+"), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Some "2", Some "-"), 2)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let multiInitfst =
    let startState = ResizeArray<_>([| 0; 1|])
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "1", Some "1"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "2", Some "+"), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Some "3", Some "-"), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Some "4", Some "4"), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let multiFinishfst =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray<_>([| 2; 3; 4|])
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "1", Some "1"), 1)
    transitions.Add(0, new EdgeLbl<_,_>(Some "2", Some "1"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "3", Some "-"), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Some "4", Some "4"), 3)
    transitions.Add(3, new EdgeLbl<_,_>(Some "2", Some "+"), 4)
    transitions.Add(4, new EdgeLbl<_,_>(Some "2", Some "+"), 1)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 4
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "a", Some "a"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "b", None), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Some "c", None), 3)
    transitions.Add(3, new EdgeLbl<_,_>(Some "d", Some "d"), 4)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "a", Some "d"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(None, Some "e"), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Some "d", Some "a"), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos12 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "a", Some "a"), 0)
    transitions.Add(0, new EdgeLbl<_,_>(Some "a", Some "b"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "b", Some "a"), 3)
    transitions.Add(0, new EdgeLbl<_,_>(Some "b", Some "b"), 2)
    transitions.Add(2, new EdgeLbl<_,_>(Some "b", Some "b"), 3)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst

let fstCompos22 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 2
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_,_>(Some "b", Some "a"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "a", Some "d"), 1)
    transitions.Add(1, new EdgeLbl<_,_>(Some "b", Some "a"), 2)
    transitions.Add(1, new EdgeLbl<_,_>(Some "a", Some "c"), 2)
    let fst = new FST<_,_>(startState, finishState, transitions)
    fst