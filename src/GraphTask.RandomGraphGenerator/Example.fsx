// This is an example script for a RandomGraphGenerator.
// Execute it in F# Interactive. To debug do DEBUG -> Attach to Process... -> Fsi.exe.

#r "bin\Debug\YC.QuickGraph.dll"
#load "RandomGraphGenerator.fs"

open QuickGraph
open QuickGraph.RandomGraphGenerator

let printDOT (graph:IVertexAndEdgeListGraph<int,Edge<int>>) graphType dir =
    let convert (edge : Edge<int>) = 
        sprintf "%i %s %i; \n" edge.Source dir edge.Target
    let rules = graph.Edges |> Seq.map convert
    let string = 
        sprintf "%s G {\n" graphType
        + "node [shape = circle]\n"
        + String.concat "" rules
        + "}"
        
    printf "%A" string

let printDirected (graph:AdjacencyGraph<_,_>) = printDOT graph "digraph" "->"
let printUndirected (graph:BidirectionalGraph<_,_>) = printDOT graph "graph" "--"

printDirected (CompleteGraph 5)
printUndirected (RingLattice 5 2)
printDirected (UniformSpanningTree 15 5)
printDirected (ErdosRenyi 10 0.2)
printDirected (RandomComponent (ErdosRenyi 15 0.2))
printUndirected (WattsStrogatz 10 4 0.2)
printUndirected (BarabasiAlbert 50)
let weights = Weights (CompleteGraph 5) 100
printf "%A" (weights 1)