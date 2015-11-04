module AppoximationTests

open NUnit.Framework
open Microsoft.FSharp.Collections
open System.Collections.Generic
open YC.FSA.GraphBasedFsa
open YC.FSA.Tests.GraphBasedFsaTestData
open YC.FSA.FsaApproximation
open YC.FST.GraphBasedFst

let basePath = "../../../YC.FST/FST/FST.Tests/DOTfst/"
//let basePath = "C:/yc/recursive-ascent/FST/FST/FST.Tests/DOTfst/" 
let fullPath f = System.IO.Path.Combine(basePath, f)

let checkGraph (fst:FST<_,_>) initV finalV countE countV =
    Assert.AreEqual(fst.InitState.Count, initV, "Count of init state not equal expected number.")
    Assert.AreEqual(fst.FinalState.Count, finalV, "Count of final state not equal expected number.")
    Assert.AreEqual(fst.EdgeCount, countE, "Count of edges not equal expected number. ")
    Assert.AreEqual(fst.VertexCount, countV, "Count of vertices not equal expected number. ")

let printSmbInt (x:char*Position<_>) = 
        match x with
        | (y, _) when y = char 65535 -> "eof"  
        | _ -> //(fst x).ToString() + "_br: " + (snd x).back_ref.ToString() + "(" + (snd x).start_offset.ToString() + "," + (snd x).end_offset.ToString() + ")"
            let symbol, position = x
            sprintf "%c_br: %s(%d, %d)" symbol <| position.back_ref.ToString() <| position.start_offset <| position.end_offset

let transform x = (x, match x with |Smbl(y, _) -> Smbl y |_ -> Eps)
let smblEOF = Smbl(char 65535,  Unchecked.defaultof<Position<_>>)

[<TestFixture>]
type ``Approximation tests`` () =    
    [<Test>]
    member this.``Approximation tests. Simple test.`` () =
        let startState = ResizeArray.singleton 0
        let finishState = ResizeArray.singleton 3
        let transitions = new ResizeArray<_>()
        transitions.Add(0, ("123+", "123+"), 1)
        transitions.Add(1, ("*", "*"), 2)
        transitions.Add(2, ("*", "*"), 1)
        transitions.Add(2, ("+45", "+45"), 3)
        let appr = new Appr<_>(startState, finishState, transitions)
        let fsa = appr.ApprToFSA()
        fsa.PrintToDOT(fullPath "branch22_test.dot")
        let fstInputLexer = FST<_,_>.FSAtoFST(fsa, transform, smblEOF)
        fstInputLexer.PrintToDOT(fullPath "branch2_test.dot", printSmbInt)
        checkGraph fstInputLexer 1 1 10 10

//[<EntryPoint>]
//let f x =
//      let t = new ``Approximation tests`` () 
//      t.``Approximation tests. Simple test.``()
//      1