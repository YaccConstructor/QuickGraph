module YC.FSA.Tests.GraphBasedFsa

open YC.FSA.FsaTable
open NUnit.Framework
open Microsoft.FSharp.Collections
open YC.FSA.GraphBasedFst
open System.Collections.Generic
open YC.FSA.Tests.GraphBasedFsaTestData

//let basePath = "../../../FST/FST/FSA.Tests/DOTfsa/"
let basePath = "C:/yc/recursive-ascent/FST/FST/FSA.Tests/DOTfsa/" 
let fullPath f = System.IO.Path.Combine(basePath, f)

let checkGraph (fsa:FSA<_>) initV finalV countE countV filePath =
    Assert.AreEqual(fsa.InitState.Count, initV, "Count of init state not equal expected number.")
    Assert.AreEqual(fsa.FinalState.Count, finalV, "Count of final state not equal expected number.")
    Assert.AreEqual(fsa.EdgeCount, countE, "Count of edges not equal expected number. ")
    Assert.AreEqual(fsa.VertexCount, countV, "Count of vertices not equal expected number. ")
    fsa.PrintToDOT <| fullPath filePath

[<TestFixture>]
type ``Graph FSA tests`` () =    
    [<Test>]
    member this.``Graph FSA. Simple test.`` () =
        checkGraph fsa1 1 1 4 4 "simple_test_graph.dot"

//[<EntryPoint>]
//let f x =
//      let t = new ``Graph FST tests`` () 
//      t.``Graph FST. Test composition FSTs.``()
//      1