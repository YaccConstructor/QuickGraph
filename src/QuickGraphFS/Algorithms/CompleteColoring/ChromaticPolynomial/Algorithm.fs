namespace QuickGraph.Algorithms

open QuickGraph
open System.Collections.Generic
open System.Linq

module ChromaticPolynomial =
    let private joinVertices (graph : UndirectedGraph<_,_>) fstVertex sndVertex =
        let tempAdjVertices1 = graph.AdjacentVertices fstVertex
        let adjVertices2 = graph.AdjacentVertices sndVertex
        let adjVertices1 = tempAdjVertices1.Concat [fstVertex]
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
        let tempAdjVertices = graph.AdjacentVertices src
        let adjVertices = tempAdjVertices.Concat [src]
        let target = Seq.find (fun vertex -> not (adjVertices.Contains vertex)) vertices
        (src, target)

    type BaseNode(graph : UndirectedGraph<_,_>, parent) =
        member this.Graph : UndirectedGraph<_,_> = graph
        member this.Parent : BaseNode option = parent
        abstract member CromaticPolinomial : int list
        default this.CromaticPolinomial = []

    type Leaf(graph, parent)  =
        inherit BaseNode(graph, parent)
        override this.CromaticPolinomial = countChrPolCompleteGraph graph.VertexCount

    type Node(graph, parent) =
        inherit BaseNode(graph, parent)
        let mutable leftChild : BaseNode option = None
        let mutable rightChild : BaseNode option = None
        let getChild child =
            match child with
                | Some x -> x
                | None -> failwith "Attempt to get child before init"
        member this.LeftChild
            with public get() = getChild leftChild
            and public set value = leftChild <- Some value
        member this.RightChild
            with public get() = getChild rightChild
            and public set value = rightChild <- Some value
        override this.CromaticPolinomial = sum this.LeftChild.CromaticPolinomial this.RightChild.CromaticPolinomial
           
    let buildTree (graph : UndirectedGraph<_,_>) =
        let rec buildTreeNode (graph : UndirectedGraph<_,_>) (parent : BaseNode option) : BaseNode =
            if graph.EdgeCount = graph.VertexCount * (graph.VertexCount - 1) / 2 then upcast new Leaf(graph, parent)
            else 
                let fst, snd = findFirstMissingEdge graph
                let graph1 = graph.Clone()
                let graph2 = graph.Clone()
                graph1.AddEdge(new UndirectedEdge<_>(fst, snd)) |> ignore
                joinVertices graph2 fst snd
                let mutable node = new Node(graph, parent)
                node.LeftChild <- buildTreeNode graph1 (Some (upcast node))
                node.RightChild <- buildTreeNode graph2 (Some (upcast node))
                upcast node
        buildTreeNode graph None

    let findChromaticPolynomial (graph : UndirectedGraph<_,_>) =
        let tree = buildTree graph
        List.toArray (tree.CromaticPolinomial)