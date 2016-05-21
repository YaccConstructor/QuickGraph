module QuickGraph.FSA.Tests.GraphBasedFsa

open NUnit.Framework
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.FSA.GraphBasedFsa
open QuickGraph.FSA.Tests.GraphBasedFsaTestData

let basePath = "tests/QuickGraph.FSA.Tests/DOTfsa/"
let fullPath f = System.IO.Path.Combine(basePath, f)

let checkGraph (fsa:FSA<_>) initV finalV countE countV filePath =
    fsa.PrintToDOT <| fullPath filePath
    Assert.AreEqual(initV, fsa.InitState.Count, "Count of init state not equal expected number.")
    Assert.AreEqual(finalV, fsa.FinalState.Count, "Count of final state not equal expected number.")
    Assert.AreEqual(countE, fsa.EdgeCount, "Count of edges not equal expected number. ")
    Assert.AreEqual(countV, fsa.VertexCount, "Count of vertices not equal expected number. ")

let equalSmbl x y = (fst x) = (fst y)

let getChar x = 
    match x with
    | Smbl(y, _) -> y
    | _ -> failwith "Unexpected symb in alphabet of FSA!"

let newSmb x =  Smbl(x, Unchecked.defaultof<_>)

[<TestFixture>]
type ``Graph FSA tests`` () =    
    [<Test>]
    member this.``Graph FSA. Simple test.`` () =
        checkGraph fsa1 1 1 4 4 "simple_test_graph.dot"

    [<Test>]
    member this.``Graph FSA. Intersection test.`` () =
        let resFSA = FSA<_>.Intersection(fsaInters1.NfaToDfa(), fsaInters2.NfaToDfa(), equalSmbl)
        checkGraph resFSA 1 1 3 4 "simple_intersection_test.dot"

    [<Test>]
    member this.``Graph FSA. Simple replace test.`` () =
        let resFSA = FSA<_>.Replace(fsaInters1, fsaRepl2, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 5 5 "simple_replace_test.dot"

    [<Test>]
    member this.``Graph FSA. Intersection is empty.`` () =
        let resFSA = FSA<_>.Intersection(fsawoInters1, fsawoInters2, equalSmbl)
        checkGraph resFSA 1 1 0 2 "intersection_is_empty.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 1.`` () =
        let resFSA = FSA<_>.Replace(fsaRepl1C, fsaRepl2C, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 6 6 "replace_test_1.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 2.`` () =
        let resFSA = FSA<_>.Replace(fsaRepl1C, fsaRepl2C2, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 5 5 "replace_test_2.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 3. Intersection is partly.`` () =
        let resFSA = FSA<_>.Replace(fsaRepl1C, fsaRepl2C3, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 6 6 "replace_test_3.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 4. Intersection is empty.`` () =
        let resFSA = FSA<_>.Replace(fsaRepl1C, fsaRepl2C4, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 7 7 "replace_test_4.dot"

    //[<Test>] 
    member this.``Graph FSA. Replace test 5. With simple cycle.`` () =
        let resFSA = FSA<_>.Replace(fsaRepl1C5, fsaRepl2C5, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 7 7 "replace_test_5.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 6. Example from paper.`` () =
        let resFSA = FSA<_>.Replace(fsaRepl1C6, fsaRepl2C6, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 5 5 "replace_test_6.dot"

    //[<Test>]
    member this.``Graph FSA. Replace test 7. FSA 2 with one state.`` () =
        let fsa = FSA<_>(
                    ResizeArray.singleton 0, 
                    ResizeArray.singleton 0, 
                    ResizeArray.ofList [
                        (0, Eps, 0)])
        
        Assert.AreEqual(fsa.IsEmpty, false)
        let resFSA = FSA<_>.Replace(fsaRepl1C6, fsa, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 7 8 "replace_test_7.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 8. FSA 1 with one state. FSA 2 accept empty string.`` () =
        let fsa = FSA<_>(
                    ResizeArray.singleton 0, 
                    ResizeArray.singleton 0, 
                    ResizeArray.ofList [
                        (0, Eps, 0)])
        
        let resFSA = FSA<_>.Replace(fsa, fsa, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 1 2 "replace_test_8.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 9. FSA 1 with one state. FSA 2 accept empty string.`` () =
        let fsa1 = FSA<_>(
                    ResizeArray.singleton 0, 
                    ResizeArray.singleton 0, 
                    ResizeArray.ofList [
                        (0, Eps, 0)])

        let fsa2 = FSA<_>(
                    ResizeArray.singleton 0, 
                    ResizeArray.singleton 0, 
                    ResizeArray.ofList [
                        (0, Smbl('a', 11), 1);
                        (1, Smbl('b', 11), 2);])
                                
        let resFSA = FSA<_>.Replace(fsa1, fsa2, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 1 2 "replace_test_9.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 10. FSA 1 with one state. FSA 2 is not accept empty string.`` () =
        let fsa1 = FSA<_>(
                    ResizeArray.singleton 0, 
                    ResizeArray.singleton 0, 
                    ResizeArray.ofList [
                        (0, Eps, 0)])

        let fsa2 = FSA<_>(
                    ResizeArray.singleton 0, 
                    ResizeArray.singleton 2, 
                    ResizeArray.ofList [
                        (0, Smbl('a', 11), 1);
                        (1, Smbl('b', 11), 2);])
                                
        let resFSA = FSA<_>.Replace(fsa1, fsa2, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 1 1 "replace_test_10.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 11. ComplemetationForReplace test.`` () =
        let resFSA = FSA<_>.Replace(fsaRepl1C7, fsaRepl2C7, fsaRepl3C7, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 5 6 "replace_test_11.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 12. Greedy replace.`` () =
        let resFSA = FSA<_>.GreedyReplace(fsaRepl1C8, fsaRepl2C8, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 2 2 3 "replace_test_12.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 13. Another greedy replace.`` () =
        let resFSA = FSA<_>.GreedyReplace(fsaRepl1C9, fsaRepl2C9, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 4 8 5 "replace_test_13.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 14. Complicated greedy replace.`` () =
        let resFSA = FSA<_>.GreedyReplace(fsaRepl1C10, fsaRepl2C10, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 3 19 11 "replace_test_14.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 14a. Empty handle list for greedy replace.`` () =
        let resFSA = FSA<_>.GreedyReplace(fsaRepl1C10a, fsaRepl2C10a, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 3 7 6 "replace_test_14a.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 15. Reluctant replace.`` () =
        let resFSA = FSA<_>.ReluctantReplace(fsaRepl1C6, fsaRepl2C6, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 1 4 5 "replace_test_15.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 16. Complicated reluctant replace.`` () =
        let resFSA = FSA<_>.ReluctantReplace(fsaRepl1C9, fsaRepl2C9, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl)
        checkGraph resFSA 1 4 12 5 "replace_test_16.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 17. Leftmost replace.`` () =
        let resFSA = FSA<_>.CustomizableReplace(fsaRepl1C11, fsaRepl2C11, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl) (Declarative, Leftmost, All)
        checkGraph resFSA 1 2 2 3 "replace_test_17.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 18. Leftmost greedy replace.`` () =
        let resFSA = FSA<_>.CustomizableReplace(fsaRepl1C11, fsaRepl2C11, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl) (Greedy, Leftmost, All)
        checkGraph resFSA 1 1 1 2 "replace_test_18.dot"

    [<Test>]
    member this.``Graph FSA. Replace test 19. Leftmost reluctant replace.`` () =
        let resFSA = FSA<_>.CustomizableReplace(fsaRepl1C11, fsaRepl2C11, fsaRepl3, '~', '^', getChar, newSmb, equalSmbl) (Reluctant, Leftmost, All)
        checkGraph resFSA 1 1 2 3 "replace_test_19.dot"

    [<Test>]
    member this.``Graph FSA. FSA is empty.`` () =
        Assert.AreEqual(fsaEmpty.IsEmpty, true)
        Assert.AreEqual(fsaRepl1C6.IsEmpty, false)

    [<Test>]
    member this.``Graph FSA. FSA accept only empty string and do nfaToDfa.`` () =
        let fsa = FSA<_>(
                    ResizeArray.singleton 0, 
                    ResizeArray.singleton 0, 
                    ResizeArray.ofList [
                        (0, Eps, 0)])
        
        Assert.AreEqual(fsa.NfaToDfa().IsEmpty, false)
        Assert.AreEqual(fsa.IsEmpty, false)

let checkFsa (fsa: FSA<_>) (expected: list<int * list<int * Symb<_>>>) symbEquals = 
    let expectedMap = Map.ofList expected
    fsa.Edges
    |> Seq.forall 
        (
            fun e ->
                match Map.tryFind e.Source expectedMap with
                | None -> false
                | Some(targets) -> 
                    let filtered = 
                        targets 
                        |> List.filter (fun (tar, sym) -> tar = e.Target && symbEquals e.Tag sym)
                    List.length filtered = 1
        )
    |> fun res -> Assert.IsTrue(res, "fsa structure differs from expected one")

let symbEquals eqData (sym1: Symb<'a>) (sym2: Symb<'a>) =
    match sym1, sym2 with
    | Smbl(a), Smbl(b) -> eqData a b
    | Eps, Eps -> true
    | _ -> false

[<TestFixture>]
type ``Additional FSA tests`` () =  
    [<Test>]
    member this.``Intersection with self complement test`` () =
        let alphabet = new HashSet<_>(['a'])
        let comp = fsaAcceptingOneLetter.Complementation(alphabet, newSmb, getChar).NfaToDfa()
        fsaAcceptingOneLetter.PrintToDOT <| fullPath "inters_with_comp_orig.dot"
        comp.PrintToDOT <| fullPath "inters_with_comp_comp.dot"
        let res = FSA<_>.Intersection (fsaAcceptingOneLetter, comp, equalSmbl)
        //checkFsa res [] (symbEquals equalSmbl)
        Assert.AreEqual(res.IsEmpty, true)
        
//    [<Test>]
//    member this.``Empty FSA determinization`` () =
//        let emptyFsa = FSA.CreateEmpty ()
//        Assert.IsTrue (FSA<_>.IsEmpty emptyFsa, "FSA must be empty")
//        let res = emptyFsa.NfaToDfa
//        checkFsa res [] (symbEquals equalSmbl)
//        checkGraph res 1 1 0 1 "empty_fsa_determ.dot"

    [<Test>]
    member this.``Intersection of any non empty word FSA and it's complementation`` () =
        let alphabet = new HashSet<_>(['a'])
        let res = FSA<_>.Intersection (anyNonEmptyWordsFsa, anyNonEmptyWordsFsa.Complementation(alphabet, newSmb, getChar), equalSmbl)
        Assert.AreEqual(res.IsEmpty, true)

    [<Test>]
    member this.``Intersection of AB fsa and B fsa`` () =
        let abFsa = 
            FSA<_>(
                ResizeArray.singleton 0, 
                ResizeArray.singleton 2, 
                ResizeArray.ofList [
                    (0, Smbl('a', 11), 1);
                    (1, Smbl('b', 12), 2) ])
        let bFsa = 
            FSA<_>(
                ResizeArray.singleton 0, 
                ResizeArray.singleton 1, 
                ResizeArray.ofList [ (0, Smbl('b', 11), 1) ])
        let res = FSA<_>.Intersection (abFsa, bFsa, equalSmbl)
        Assert.AreEqual(res.IsEmpty, true)

//[<EntryPoint>]
//let f x =
//      let t = new ``Graph FSA tests`` () 
//      t.``Graph FSA. Replace test 1.``()
//      1