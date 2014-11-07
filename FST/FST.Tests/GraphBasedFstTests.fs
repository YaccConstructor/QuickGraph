module YC.FST.Tests.GraphBasedFst

open YC.FST.FstTable
open NUnit.Framework
open Microsoft.FSharp.Collections
open YC.FST.GraphBasedFst
open YC.FST.Tests.GraphBasedFstTestData
open System.Collections.Generic

let basePath = "../../../../DOTfst/"
let fullPath f = System.IO.Path.Combine(basePath, f)

let checkGraph (fst:FST<_,_>) initV finalV countE countV filePath =
    Assert.AreEqual(fst.InitState.Count, initV, "Count of init state not equal expected number.")
    Assert.AreEqual(fst.FinalState.Count, finalV, "Count of final state not equal expected number.")
    Assert.AreEqual(fst.EdgeCount, countE, "Count of edges not equal expected number. ")
    Assert.AreEqual(fst.VertexCount, countV, "Count of vertices not equal expected number. ")
    fst.PrintToDOT <| fullPath filePath

let CompositionTest (fst1:FST<_,_>) (fst2:FST<_,_>) alphabet initV finalV countE countV filePath  =
    let res = FST.Compos(fst1, fst2, alphabet)
    match res with
    | Success res ->
        checkGraph res initV finalV countE countV filePath  
    | Error e -> Assert.Fail(sprintf "Tokenization problem: %A" e)

[<TestFixture>]
type ``Graph FST tests`` () =    
    [<Test>]
    member this.``Graph FST. Simple test.`` () =
        checkGraph fst1 1 1 4 4 "simple_test_graph.dot"

    [<Test>]
    member this.``Graph FST. Concat test.`` () =
        let resFST = fst1.Concat fst2
        checkGraph resFST 1 1 9 8 "concat_test_graph.dot"

    [<Test>]
    member this.``Graph FST. Concat test with multi finit and init.`` () =
        let resFST = multiInitfst.Concat multiFinishfst
        checkGraph resFST 2 3 12 10 "concat_test_graph_multi.dot"

    [<Test>]
    member this.``Graph FST. Concat test with multi finit and init 1.`` () =
        let resFST = multiFinishfst.Concat multiInitfst
        checkGraph resFST 1 1 15 10 "concat_test_graph_multi_1.dot"
        
    [<Test>]
    member this.``Graph FST. Union test.`` () =
        let resFST = fst1.Union fst2
        checkGraph resFST 1 1 11 9 "union_test_graph.dot"

    [<Test>]
    member this.``Graph FST. Union test with multi finit and init.`` () =
        let resFST = multiFinishfst.Union multiInitfst
        checkGraph resFST 1 1 17 11 "union_test_graph_multi.dot"

    [<Test>]
    member this.``Graph FST. Test composition FSTs.`` () =
        let alphabet = new HashSet<_>()
        for edge in fstCompos2.Edges do
            alphabet.Add(edge.Tag.InSymb) |> ignore
        
        CompositionTest fstCompos1 fstCompos2 alphabet 1 1 11 8 "compos_test.dot"

    [<Test>]
    member this.``Graph FST. Test composition FSTs 1.`` () =
        let alphabet = new HashSet<_>()
        for edge in fstCompos2.Edges do
            alphabet.Add(edge.Tag.InSymb) |> ignore

        CompositionTest fstCompos12 fstCompos22 alphabet 1 1 4 4 "compos_test_1.dot"

//[<EntryPoint>]
//let f x =
//      let t = new ``Graph FST tests`` () 
//      t.``Graph FST. Test composition FSTs.``()
//      1