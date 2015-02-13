module YC.FSA.Tests.GraphBasedFsaTestData

open Microsoft.FSharp.Collections
open YC.FSA.GraphBasedFst

let fsa1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 3
    let transitions = new ResizeArray<_>()
    transitions.Add(0, new EdgeLbl<_>(Smbl("1", 11)), 1)
    transitions.Add(1, new EdgeLbl<_>(Smbl("2", 12)), 2)
    transitions.Add(1, new EdgeLbl<_>(Smbl("3", 12)), 2)
    transitions.Add(2, new EdgeLbl<_>(Smbl("4", 14)), 3)
    let fsa = new FSA<_>(startState, finishState, transitions)
    fsa