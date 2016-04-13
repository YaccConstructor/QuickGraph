namespace QuickGraph.Algorithms

open QuickGraph
open System.Collections.Generic
open System.Linq

module ChromaticPolynomial =
   
    let private getAdjVertices (graph : UndirectedGraph<'TVertex,_>) vertex =
        let edges = graph.AdjacentEdges vertex
        let adjVertices : HashSet<'TVertex> = new HashSet<'TVertex>(Set.empty)
        for edge in edges do
            adjVertices.Add edge.Source |> ignore
            adjVertices.Add edge.Target |> ignore
        adjVertices

    let private joinVertices (graph : UndirectedGraph<_,_>) fstVertex sndVertex =
        let adjVertices1 = getAdjVertices graph fstVertex
        let adjVertices2 = getAdjVertices graph sndVertex
        adjVertices2.Add fstVertex |> ignore
        let verticesToAdd = adjVertices1.Except adjVertices2
        for vertex in verticesToAdd do
            graph.AddEdge (new UndirectedEdge<_>(sndVertex, vertex)) |> ignore
        graph.RemoveVertex fstVertex |> ignore


    let private sum (fstPolynomial : int list) (sndPolynomial : int list) =
        let max = max fstPolynomial.Length sndPolynomial.Length
        let fst = (List.init (max - fstPolynomial.Length) (fun _ -> 0)) @ fstPolynomial
        let snd = (List.init (max - sndPolynomial.Length) (fun _ -> 0)) @ sndPolynomial
        List.map2 (+) fst snd
        
    // Multiply polinomial with (x - r) polinomial
    let private mul (polynomial : int list) (r : int) =
        let res = polynomial @ [0]
        List.map2 (-) res (0::(List.map (fun x -> x * r) polynomial))

    let private countChrPolCompleteGraph degree =
        let mutable res = [ 1; 0 ]
        for i in 1..degree-1 do
            res <- mul res i
        res

    let private findFirstMissingEdge (graph : UndirectedGraph<_,_>) =
        let vertices = graph.Vertices
        let src = Seq.find (fun vertex -> graph.AdjacentDegree vertex <> graph.VertexCount - 1) vertices
        let adjVertices = getAdjVertices graph src
        adjVertices.Add src |> ignore
        let target = Seq.find (fun vertex -> not (adjVertices.Contains vertex)) vertices
        (src, target)

    (*let private getAdjMatrix (graph : UndirectedGraph<'TVertex,_>) =
        let size = graph.VertexCount
        let matrix = new BitArray(size * size)
        let vertices = new Dictionary<int, 'TVertex>(size)
        let vertexEnum = graph.Vertices.GetEnumerator()
        vertexEnum.MoveNext() |> ignore
        
        for i in 0..size-1 do
            vertices.
            vertexEnum.MoveNext() |> ignore

        for i in 0..size-1 do
            let v = array.[i]
            let degree = graph.AdjacentDegree()
            for j in 0..degree-1 do
                let edge = graph.AdjacentEdge v degree

        (matrix, array)*)

    let rec private findChromaticPolynomialAsList (graph : UndirectedGraph<_,_>) = 
        if graph.EdgeCount = graph.VertexCount * (graph.VertexCount - 1) / 2 then countChrPolCompleteGraph graph.VertexCount
        else 
            let fst, snd = findFirstMissingEdge graph
            let graph1 = graph.Clone()
            let graph2 = graph.Clone()
            graph1.AddEdge(new UndirectedEdge<_>(fst, snd)) |> ignore
            joinVertices graph2 fst snd
            sum (findChromaticPolynomialAsList graph1) (findChromaticPolynomialAsList graph2)

    let rec findChromaticPolynomial (graph : UndirectedGraph<_,_>) =
        List.toArray (findChromaticPolynomialAsList graph)