module QuickGraph.FST.Tests.GraphBasedFst

open QuickGraph.FST.FstTable
open NUnit.Framework
open Microsoft.FSharp.Collections
open QuickGraph.FST.GraphBasedFst
open QuickGraph.FST.Tests.GraphBasedFstTestData
open System.Collections.Generic
open QuickGraph.FSA.GraphBasedFsa

let basePath = "tests/QuickGraph.FST.Tests/DOTfst/"
let fullPath f = System.IO.Path.Combine(basePath, f)

let getAlphabet (fst:FST<_,_>) = 
    let alphabet = new HashSet<_>()
    for edge in fst.Edges do
        alphabet.Add(snd edge.Tag) |> ignore
    alphabet

let printFST (fst:FST<_,_>) = fullPath >> fst.PrintToDOT

let checkGraph (fst:FST<_,_>) initV finalV countE countV =
    Assert.AreEqual(fst.InitState.Count, initV, "Count of init state not equal expected number.")
    Assert.AreEqual(fst.FinalState.Count, finalV, "Count of final state not equal expected number.")
    Assert.AreEqual(fst.EdgeCount, countE, "Count of edges not equal expected number. ")
    Assert.AreEqual(fst.VertexCount, countV, "Count of vertices not equal expected number. ")    

let oldCompositionTest (fst1:FST<_,_>) (fst2:FST<_,_>) alphabet initV finalV countE countV filePath  =
    let res = FST.oldCompose(fst1, fst2, alphabet)
    match res with
    | Success res ->
        printFST res filePath
        checkGraph res initV finalV countE countV 
    | Error e -> Assert.Fail(sprintf "Tokenization problem: %A" e)

let compositionTest (fst1:FST<_,_>) (fst2:FST<_,_>) (expected:FST<_,_>)  =
    let res = FST.Compose(fst1, fst2, getAlphabet fst1);
    match res with
    | Success res ->
        checkGraph res res.InitState.Count res.FinalState.Count res.EdgeCount res.VertexCount
    | Error e -> Assert.Fail(sprintf "Tokenization problem: %A" e)
    

[<TestFixture>]
type ``Graph FST tests`` () =    
    [<Test>]
    member this.``Graph FST. Simple test.`` () =
        printFST fst1 "simple_test_graph.dot"
        checkGraph fst1 1 1 4 4

    [<Test>]
    member this.``Graph FST. Concat test.`` () =
        let resFST = fst1.Concat fst2
        printFST resFST "concat_test_graph.dot"
        checkGraph resFST 1 1 9 8

    [<Test>]
    member this.``Graph FST. Concat test with multi finit and init.`` () =
        let resFST = multiInitfst.Concat multiFinishfst
        printFST resFST "concat_test_graph_multi.dot"
        checkGraph resFST 2 3 12 10

    [<Test>]
    member this.``Graph FST. Concat test with multi finit and init 1.`` () =
        let resFST = multiFinishfst.Concat multiInitfst
        printFST resFST "concat_test_graph_multi_1.dot"
        checkGraph resFST 1 1 15 10
        
    [<Test>]
    member this.``Graph FST. Union test.`` () =
        let resFST = fst1.Union fst2
        printFST resFST "union_test_graph.dot"
        checkGraph resFST 1 1 11 9

    [<Test>]
    member this.``Graph FST. Union test with multi finit and init.`` () =
        let resFST = multiFinishfst.Union multiInitfst
        printFST resFST "union_test_graph_multi.dot"
        checkGraph resFST 1 1 17 11

    //[<Test>]  test with eps
    member this.``Graph FST. Test composition FSTs.`` () =
        let alphabet = getAlphabet fstCompos1
        oldCompositionTest fstCompos1 fstCompos2 alphabet 1 1 11 8 "compos_test.dot"

    //[<Test>]
    member this.``Graph FST. Test composition FSTs 1.`` () =
        let alphabet = getAlphabet fstCompos12
        oldCompositionTest fstCompos12 fstCompos22 alphabet 1 1 4 4 "compos_test_1.dot"
    
    [<Test>]
    member this.``Graph FST. Test optimal composition FSTs 1.`` () =
        compositionTest firstFstCompos0 secondFstCompos0 resultFstCompos0

    [<Test>]
    member this.``Graph FST. Test optimal composition FSTs 2.`` () =
        compositionTest firstFstCompos1 secondFstCompos1 resultFstCompos1

//[<EntryPoint>]
//let f x =
//      let t = new ``Graph FST tests`` () 
//      t.``Graph FST. Test composition FSTs.``()
//      1