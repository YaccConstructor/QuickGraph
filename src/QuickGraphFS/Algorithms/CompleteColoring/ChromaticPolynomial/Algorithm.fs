namespace QuickGraph.Algorithms.ChromaticPolynomial

open QuickGraph

module Algorithm =
   
    let private joinVertexes (graph : UndirectedGraph<_,_>) fstVertex sndVertex =
        let edges = graph.AdjacentEdges fstVertex
        for edge in edges do
            graph.AddEdge (new UndirectedEdge<_>(edge.Source, sndVertex)) |> ignore
        graph.RemoveVertex fstVertex |> ignore


    let private sum (fstPolynomial : int array) (sndPolynomial : int array) =
        let fst : int array ref = ref null
        let snd : int array ref = ref null
        if fstPolynomial.Length < sndPolynomial.Length then
            fst := sndPolynomial
            snd := [| for i in 0..(!fst).Length-1 -> if i < fstPolynomial.Length then fstPolynomial.[i] else 0 |]
        else
            fst := fstPolynomial
            snd := [| for i in 0..(!fst).Length-1 -> if i < sndPolynomial.Length then sndPolynomial.[i] else 0 |]
        
        Array.map2 (+) !fst !snd
        
    // Multiply polinomial with (x - r) polinomial
    let private mul (polynomial : int array) (r : int) =
        let res = [| for i in 0..polynomial.Length -> if i = 0 then 0 else polynomial.[i-1] |]
        Array.map2 (+) res (Array.map (fun x -> x * r) res)

    let private countChrPolCompleteGraph degree =
        let mutable res = [| 1; 0 |]
        for i in 1..degree-1 do
            res <- mul res i
        res

    let findFirstMissingEdge (graph : UndirectedGraph<_,_>) =
        let vertexes = graph.Vertices
        let enumerator = vertexes.GetEnumerator()
        enumerator.MoveNext() |> ignore
        while (graph.AdjacentDegree enumerator.Current = graph.VertexCount - 1) do
            enumerator.MoveNext() |> ignore

        let cur = enumerator.Current
        enumerator.Reset()
        enumerator.MoveNext() |> ignore
        while ((cur.Equals enumerator.Current) && (graph.ContainsEdge (new UndirectedEdge<_>(cur, enumerator.Current)))) do
            enumerator.MoveNext() |> ignore
        (cur, enumerator.Current)
        

    let rec findChromaticPolynomial (graph : UndirectedGraph<_,_>) = 
        if graph.EdgeCount = graph.VertexCount * (graph.VertexCount - 1) / 2 then countChrPolCompleteGraph graph.VertexCount
        else 
            let fst, snd = findFirstMissingEdge graph
            let graph1 = graph.Clone()
            let graph2 = graph.Clone()
            graph1.AddEdge(new UndirectedEdge<_>(fst, snd)) |> ignore
            joinVertexes graph2 fst snd
            sum (findChromaticPolynomial graph1) (findChromaticPolynomial graph1)