module GraphTask.Performance

open System
open QuickGraph
open QuickGraph.Tests
open QuickGraph.RandomGraphGenerator

[<EntryPoint>]
let main argv = 
    let runTest name action =
        let benchmark = new Benchmark(name)
        benchmark.Run(Action action)
        printf "%A\n" benchmark
    
    let ``Remove vertex in AdjacencyGraph`` =
        let graph = ErdosRenyi 1000 0.2
        let vertices = [for v in graph.Vertices do yield v]
        runTest "Remove vertex in AdjacencyGraph" (fun _ -> for v in vertices do graph.RemoveVertex(v) |> ignore)

    let ``Remove edge with predicate in AdjacencyGraph`` =
        let graph = ErdosRenyi 1000 0.2
        let edges = [for e in graph.Edges do yield e]
        runTest "Remove edge with predicate in AdjacencyGraph" (fun _ -> for e in edges do graph.RemoveEdgeIf(fun _ -> true) |> ignore)

    0
