module YC.FST.Tests.FstApproximationTestData

open Microsoft.FSharp.Collections
open YC.FST.FstApproximation

let appr1 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smb("abc",1), 1)
    new Appr<_>(startState, finishState, transitions)
    
let appr2 =
    let startState = ResizeArray.singleton 0
    let finishState = ResizeArray.singleton 1
    let transitions = new ResizeArray<_>()
    transitions.Add(0, Smb("abc",1), 1)
    transitions.Add(0, Smb("qw",2), 1)
    new Appr<_>(startState, finishState, transitions)

