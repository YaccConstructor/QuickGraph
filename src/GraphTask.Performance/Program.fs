module GraphTask.Performance

open System
open QuickGraph
open QuickGraph.Tests
open QuickGraph.RandomGraphGenerator

[<EntryPoint>]
let main argv = 
    let runTest name action samples =
        let benchmark = new Benchmark(name)
        for i in 1 .. samples do benchmark.Run(Action (action()))
        printf "%A, %A on average\n" benchmark (benchmark.Seconds / float benchmark.Samples)
    
    let ``Remove vertex in AdjacencyGraph`` =
        let test () =
            // Use this section for a before each hook 
            let graph = ErdosRenyi 1000 0.2
            let vertices = [for v in graph.Vertices do yield v]
            // The actual evaluated action
            (fun _ -> for v in vertices do graph.RemoveVertex(v) |> ignore)
        runTest "Remove vertex in AdjacencyGraph" test 2

    let ``Remove edge with predicate in AdjacencyGraph`` =
        let test () =
            let graph = ErdosRenyi 1000 0.2
            let edges = [for e in graph.Edges do yield e]
            (fun _ -> for e in edges do graph.RemoveEdgeIf(fun _ -> true) |> ignore)
        runTest "Remove edge with predicate in AdjacencyGraph" test 2

    0
