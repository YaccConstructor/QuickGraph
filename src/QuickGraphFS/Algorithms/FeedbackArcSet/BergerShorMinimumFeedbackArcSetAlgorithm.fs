namespace QuickGraph.Algorithms

open System
open System.Linq
open System.Collections.Generic

open QuickGraph

type BergerShorMinimumFeedbackArcSetAlgorithm =
    static member private removeIncomingEdges(initial : AdjacencyGraph<string, Edge<string>>,
                                              destination : AdjacencyGraph<string, Edge<string>>,
                                              feedbackArcSet : AdjacencyGraph<string, Edge<string>>,
                                              vertex : string) =
        //Add outcoming edges to the resulting graph and remove them from initial
        for e in initial.Edges.Where(fun (e : Edge<string>) -> e.Source = vertex) do
            destination.AddEdge(e) |> ignore
        initial.RemoveEdgeIf(fun e -> e.Source = vertex) |> ignore

        //Add incoming edges to feedback arc set and remove them from initial
        for e in initial.Edges.Where(fun (e : Edge<string>) -> e.Target = vertex) do
            feedbackArcSet.AddEdge(e) |> ignore
        initial.RemoveEdgeIf(fun e -> e.Target = vertex) |> ignore
        ()

    static member private removeOutcomingEdges(initial : AdjacencyGraph<string, Edge<string>>,
                                               destination : AdjacencyGraph<string, Edge<string>>,
                                               feedbackArcSet : AdjacencyGraph<string, Edge<string>>,
                                               vertex : string) =
        //Add incoming edges to the resulting graph
        for e in initial.Edges.Where(fun (e : Edge<string>) -> e.Target = vertex) do
            destination.AddEdge(e) |> ignore
        //Remove incoming edges from initial
        initial.RemoveEdgeIf(fun e -> e.Target = vertex) |> ignore

        //Add outcoming edges to feedback arc set
        for e in initial.Edges.Where(fun (e : Edge<string>) -> e.Source = vertex) do
            feedbackArcSet.AddEdge(e) |> ignore
        //Remove outcoming edges from initial
        initial.RemoveEdgeIf(fun e -> e.Source = vertex) |> ignore
        ()

    static member private inDegree(graph : AdjacencyGraph<string, Edge<string>>, vertex : string) =
        let g = graph.Clone()
        g.RemoveEdgeIf(fun e -> e.Target = vertex)

    static member private outDegree(graph : AdjacencyGraph<string, Edge<string>>, vertex : string) =
        let g = graph.Clone()
        g.RemoveEdgeIf(fun e -> e.Source = vertex)

    static member Compute(graph : AdjacencyGraph<string, Edge<string>>) =
        let initial = graph
        let destination = new AdjacencyGraph<string, Edge<string>>()
        let feedbackArcSet = new AdjacencyGraph<string, Edge<string>>()
        //Add initial vertices
        for i in graph.Vertices do
            destination.AddVertex(i) |> ignore
            feedbackArcSet.AddVertex(i) |> ignore
        //Berger-Shor algorithm
        for i in graph.Vertices do
            if BergerShorMinimumFeedbackArcSetAlgorithm.inDegree(graph, i) > BergerShorMinimumFeedbackArcSetAlgorithm.outDegree(graph, i) 
                then BergerShorMinimumFeedbackArcSetAlgorithm.removeOutcomingEdges(initial, destination, feedbackArcSet, i)
                else BergerShorMinimumFeedbackArcSetAlgorithm.removeIncomingEdges(initial, destination, feedbackArcSet, i)
        //destination, feedbackArcSet, initial.IsEdgesEmpty
        destination