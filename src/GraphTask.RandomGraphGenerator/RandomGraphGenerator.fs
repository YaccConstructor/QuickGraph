module QuickGraph.RandomGraphGenerator

open QuickGraph
open QuickGraph.Algorithms.ConnectedComponents
open System.Collections.Generic

///Generates a complete graph with given vertices count
let CompleteGraph verticeCount =
    let graph = new AdjacencyGraph<int,Edge<int>>()

    for v1 in 1 .. verticeCount do
        for v2 in 1 .. verticeCount do
            if v1 <> v2 then graph.AddVerticesAndEdge(new Edge<_>(v1, v2)) |> ignore

    graph

///Generates a simple ring lattice with given vertices count and mean degree
let RingLattice verticeCount degree =
    let graph = new BidirectionalGraph<int,Edge<int>>()

    if degree % 2 <> 0 then failwith "Degree is not even!"

    for v1 in 1 .. verticeCount do
        for v2 in v1 + 1 .. verticeCount do
            if abs(v1 - v2) % (verticeCount - degree / 2) <= (degree / 2) then
                graph.AddVerticesAndEdge(new Edge<_>(v1, v2)) |> ignore

    graph    

///Generates an uniform distributed random spanning tree
///with given vertices count and root vertice (D.B. Wilson, 1996)
let UniformSpanningTree verticeCount root = 
    let graph = new AdjacencyGraph<int,Edge<int>>()
    let random = System.Random()

    if not (graph.AddVertex(root)) then failwith "Wrong root!"

    for v in 1 .. verticeCount do
        let next = new Dictionary<_,_>()
        let rec randomWalk v =
            if not (graph.ContainsVertex(v)) then
                next.[v] <- random.Next(1, verticeCount)
                randomWalk (next.[v])
        let rec makePath v path =
            if not (graph.ContainsVertex(v)) then
                graph.AddVertex(v) |> ignore
                makePath (next.[v]) (new Edge<_>(next.[v], v)::path)
            else path
        randomWalk v
        graph.AddEdgeRange(makePath v []) |> ignore

    graph

///Generates an Erdos-Renyi model of a random graph
///with given vertices count
let ErdosRenyi verticeCount probability =
    let graph = new AdjacencyGraph<int,Edge<int>>()
    let random = System.Random()

    for v1 in 1 .. verticeCount do
        for v2 in 1 .. verticeCount do
            if v1 <> v2 && random.NextDouble() <= probability then 
                graph.AddVerticesAndEdge(new Edge<_>(v1, v2)) |> ignore

    graph

///Generates an Watts-Strogatz model of a random graph
///with given vertices count, mean degree and probability
let WattsStrogatz verticeCount degree probability =
    let graph = RingLattice verticeCount degree
    let random = System.Random()

    for v1 in 1 .. verticeCount do
        for v2 in v1 + 1 .. verticeCount do
            let rewire = random.Next(1, verticeCount)
            match graph.TryGetEdge(v1, v2) with
            | (true, edge) ->
                if (rewire <> v1 && not (graph.ContainsEdge(v1, rewire)) 
                    && random.NextDouble() <= probability) then
                    graph.RemoveEdge(edge) |> ignore
                    graph.AddEdge(new Edge<_>(v1, rewire)) |> ignore
            | (false, _) -> ()

    graph

///Generates a Barabasi-Albert model of a random graph
///with given vertices count
let BarabasiAlbert verticeCount =
    let graph = new BidirectionalGraph<int,Edge<int>>()
    let degree = new Dictionary<int,int>()
    let random = System.Random()

    for v in 1 .. verticeCount do
        graph.AddVertex(v) |> ignore
        degree.[v] <- 0

    // initialization
    let inline inc (e:Edge<_>) = 
        degree.[e.Source] <- degree.[e.Source] + 1
        degree.[e.Target] <- degree.[e.Target] + 1
    let initial = new Edge<_>(1, 2)
    graph.AddEdge(initial) |> ignore
    inc initial

    for v1 in 3 .. verticeCount do
        for v2 in 1 .. v1 - 1 do
            if random.NextDouble() <= float(degree.[v2]) / float(graph.EdgeCount * 2) then
                let edge = new Edge<_>(v1, v2)
                graph.AddEdge(edge) |> ignore
                inc edge             

    graph

///Gets a random strongly connected component of a given graph
let RandomComponent (graph:AdjacencyGraph<int,Edge<int>>) =
    let subgraph = new AdjacencyGraph<int,Edge<int>>()

    let components = new StronglyConnectedComponentsAlgorithm<int,Edge<int>>(graph)
    components.Compute()

    let random = System.Random().Next(components.ComponentCount - 1)

    for e in graph.Edges do
        if (components.Components.[e.Source] = random && 
            components.Components.[e.Target] = random) then
            subgraph.AddVerticesAndEdge(e) |> ignore

    subgraph

///Returns a random weights for a graph with given maximum number
let Weights (graph:IVertexAndEdgeListGraph<int,Edge<int>>) max =
    let random = System.Random()
    let weights = new Dictionary<int,int>()

    for v in graph.Vertices do weights.[v] <- random.Next(max)

    (fun v -> weights.[v])