module QuickGraph.FST.Tests.FstTable

open QuickGraph.FST.FstTable
open NUnit.Framework
open Microsoft.FSharp.Collections

let basePath = "tests/QuickGraph.FST.Tests/DOTfst/"
let fullPath f = System.IO.Path.Combine(basePath, f)

[<TestFixture>]
type ``FST tests`` () =    
    
    [<Test>]
    member this.``FST. Simple test.`` () =
        let startState = ResizeArray.singleton 0
        let finishState = ResizeArray.singleton 3
        let edges = [|new Edge<_,_>(0, "1", "1", 1); new Edge<_,_>(1, "2", "+", 2); new Edge<_,_>(1, "3", "-", 2); new Edge<_,_>(2, "4\"7", "4", 3)|]
        let simpleFST = new  SimpleFST<_, _>(startState, ResizeArray.ofArray edges, finishState)
        let fst = new FST<_,_>()
        fst.LoadFromSimpleFST simpleFST
        Assert.AreEqual(fst.InitState.Count, 1, "Count of init state not equal expected number.")
        Assert.AreEqual(fst.FinalState.Count, 1, "Count of final state not equal expected number.")
        fst.printFST <| fullPath "simple_test.dot"

    [<Test>]
    member this.``FST. Cicles.`` () =
        let startState = ResizeArray.singleton 0
        let finishState = ResizeArray.singleton 3
        let edges = 
            [|
                new Edge<_,_>(0, "1", "1", 1); new Edge<_,_>(1, "2", "+", 2); new Edge<_,_>(1, "3", "-", 2); new Edge<_,_>(2, "4\"7", "4", 3);       
                new Edge<_,_>(4, "2", "+", 5); new Edge<_,_>(5, "3", "-", 3); new Edge<_,_>(3, "3", "-", 4)
            |]
        let simpleFST = new  SimpleFST<_, _>(startState, ResizeArray.ofArray edges, finishState)
        let fst = new FST<_,_>()
        fst.LoadFromSimpleFST simpleFST
        Assert.AreEqual(fst.InitState.Count, 1, "Count of init state not equal expected number.")
        Assert.AreEqual(fst.FinalState.Count, 1, "Count of final state not equal expected number.")
        fst.printFST <| fullPath "test_cicle.dot"

    [<Test>]
    member this.``FST. Multiple initial state test.`` () =
        let startState = ResizeArray<_>([| 0; 1|])
        let finishState = ResizeArray.singleton 3
        let edges = [|new Edge<_,_>(0, "1", "1", 1); new Edge<_,_>(1, "2", "+", 2); new Edge<_,_>(1, "3", "-", 2); new Edge<_,_>(2, "4\"7", "4", 3)|]
        let simpleFST = new  SimpleFST<_, _>(startState, ResizeArray.ofArray edges, finishState)
        let fst = new FST<_,_>()
        fst.LoadFromSimpleFST simpleFST
        Assert.AreEqual(fst.InitState.Count, 2, "Count of init state not equal expected number.")
        Assert.AreEqual(fst.FinalState.Count, 1, "Count of final state not equal expected number.")
        fst.printFST <| fullPath "multi_init_test.dot"

    [<Test>]
    member this.``FST. Multiple final state test.`` () =
        let startState = ResizeArray.singleton 0
        let finishState = ResizeArray<_>([| 2; 3; 4|])
        let edges = [|
                      new Edge<_,_>(0, "1", "1", 1); new Edge<_,_>(1, "2", "+", 2); new Edge<_,_>(1, "3", "-", 2);
                      new Edge<_,_>(2, "4\"7", "4", 3); new Edge<_,_>(1, "2", "+", 4);
                     |]
        let simpleFST = new  SimpleFST<_, _>(startState, ResizeArray.ofArray edges, finishState)
        let fst = new FST<_,_>()
        fst.LoadFromSimpleFST simpleFST
        Assert.AreEqual(fst.InitState.Count, 1, "Count of init state not equal expected number.")
        Assert.AreEqual(fst.FinalState.Count, 3, "Count of final state not equal expected number.")
        fst.printFST <| fullPath "multi_finish_test.dot"

    //[<Test>]
    member this.``FST. Cicles_1.`` () =
        let startState = ResizeArray.singleton 1
        let finishState = ResizeArray<_>([| 2; 3; 4|])
        let edges = [|
                      new Edge<_,_>(0, "1", "1", 1); new Edge<_,_>(0, "2", "1", 1); new Edge<_,_>(1, "2", "+", 2); 
                      new Edge<_,_>(1, "3", "-", 2); new Edge<_,_>(2, "4", "4", 3); new Edge<_,_>(1, "2", "+", 4);
                      new Edge<_,_>(1, "2", "+", 5);
                     |]
        let simpleFST = new  SimpleFST<_, _>(startState, ResizeArray.ofArray edges, finishState)
        let fst = new FST<_,_>()
        fst.LoadFromSimpleFST simpleFST
        Assert.AreEqual(fst.InitState.Count, 1, "Count of init state not equal expected number.")
        Assert.AreEqual(fst.FinalState.Count, 3, "Count of final state not equal expected number.")
        fst.printFST <| fullPath "test_cicle_1.dot"

//[<EntryPoint>]
//let f x =
//      let t = new ``FST tests`` () 
//      t.``FST. Simple test.``()
//      1