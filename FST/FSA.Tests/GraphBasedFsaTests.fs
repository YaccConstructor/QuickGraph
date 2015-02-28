module YC.FSA.Tests.GraphBasedFsa

open NUnit.Framework
open Microsoft.FSharp.Collections
open System.Collections.Generic
open YC.FSA.GraphBasedFsa
open YC.FSA.Tests.GraphBasedFsaTestData

let basePath = "../../../FST/FST/FSA.Tests/DOTfsa/"
//let basePath = "C:/yc/recursive-ascent/FST/FST/FSA.Tests/DOTfsa/" 
let fullPath f = System.IO.Path.Combine(basePath, f)

let checkGraph (fsa:FSA<_>) initV finalV countE countV filePath =
    fsa.PrintToDOT <| fullPath filePath
    Assert.AreEqual(fsa.InitState.Count, initV, "Count of init state not equal expected number.")
    Assert.AreEqual(fsa.FinalState.Count, finalV, "Count of final state not equal expected number.")
    Assert.AreEqual(fsa.EdgeCount, countE, "Count of edges not equal expected number. ")
    Assert.AreEqual(fsa.VertexCount, countV, "Count of vertices not equal expected number. ")
    //fsa.PrintToDOT <| fullPath filePath
     
[<TestFixture>]
type ``Graph FSA tests`` () =    
    [<Test>]
    member this.``Graph FSA. Simple test.`` () =
        checkGraph fsa1 1 1 4 4 "simple_test_graph.dot"

    [<Test>]
    member this.``Graph FSA. Intersection test.`` () =
        let resFSA = FSA<_>.Intersection(fsaInters1.NfaToDfa, fsaInters2.NfaToDfa)
        checkGraph resFSA 1 1 3 4 "simple_intersection_test.dot"

    [<Test>]
    member this.``Graph FSA. Simple replace test.`` () =
        let resFSA = FSA<_>.Replace(fsaInters1, fsaRepl2, fsaRepl3)
        checkGraph resFSA 1 1 5 5 "simple_replace_test.dot"

//[<EntryPoint>]
//let f x =
//      let t = new ``Graph FSA tests`` () 
//      t.``Graph FSA. Simple replace test.``()
//      1