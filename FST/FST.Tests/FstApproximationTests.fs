module  YC.FST.Tests.FstApproximation

open YC.FST.FstApproximation
open NUnit.Framework
open YC.FST.Tests.FstApproximationTestData
 
let basePath = "../../../../DOTfst/"
let fullPath f = System.IO.Path.Combine(basePath, f)

[<TestFixture>]
type ``Graph FST Approximation tests`` () =    

    [<Test>]
    member this.``Graph FST Approximation. Simple test.`` () =
        let fst = appr1.ToFST()
        fst.PrintToDOT(fullPath "appr1_test.dot")

    [<Test>]
    member this.``Graph FST Approximation. Branch test.`` () =
        let fst = appr2.ToFST()
        fst.PrintToDOT(fullPath "branch1_test.dot")