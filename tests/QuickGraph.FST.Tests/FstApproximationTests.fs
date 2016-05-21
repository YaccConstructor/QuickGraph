module  YC.FST.Tests.FstApproximation

open YC.FST.FstApproximation
open NUnit.Framework
open YC.FST.Tests.FstApproximationTestData
open Microsoft.FSharp.Collections
 
let basePath = "tests/QuickGraph.FST.Tests/DOTfst/"
let fullPath f = System.IO.Path.Combine(basePath, f)

let printSmbInt (x:char*Position<_>) = 
        (fst x).ToString() + "_br: " + (snd x).back_ref.ToString() + "(" + (snd x).start_offset.ToString() + "," + (snd x).end_offset.ToString() + ")"

let printSmbString (x:char*Position<_>) = 
        (fst x).ToString() + "_br: " + (snd x).back_ref + "(" + (snd x).start_offset.ToString() + "," + (snd x).end_offset.ToString() + ")"


[<TestFixture>]
type ``Graph FST Approximation tests`` () =    

    [<Test>]
    member this.``Graph FST Approximation. Simple test.`` () =
        let fst = appr1.ToFST()
        fst.PrintToDOT(fullPath "appr1_test.dot", printSmbInt)

    [<Test>]
    member this.``Graph FST Approximation. Branch test.`` () =
        let fst = appr2.ToFST()
        fst.PrintToDOT(fullPath "branch1_test.dot", printSmbInt)

    [<Test>]
    member this.``Graph FST Approximation. Test.`` () =
        let startState = ResizeArray.singleton 0
        let finishState = ResizeArray.singleton 3
        let transitions = new ResizeArray<_>()
        transitions.Add(0, Smb("123+", "123+"), 1)
        transitions.Add(1, Smb("*", "*"), 2)
        transitions.Add(2, Smb("*", "*"), 1)
        transitions.Add(2, Smb("+45", "+45"), 3)
        let appr = new Appr<_>(startState, finishState, transitions)
        let fstInputLexer = appr.ToFST()
        fstInputLexer.PrintToDOT(fullPath "branch2_test.dot", printSmbString)

//[<EntryPoint>]
//let f x =
//      let t = new ``Graph FST Approximation tests`` () 
//      let a = t.``Graph FST Approximation. Test.``()
//      //printfn "%A" a      
//      1
