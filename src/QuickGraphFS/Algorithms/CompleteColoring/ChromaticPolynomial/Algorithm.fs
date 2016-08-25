namespace QuickGraph.Algorithms

open QuickGraph
open System.Linq
open System

module ChromaticPolynomial =
    let private joinVertices (graph : UndirectedGraph<_,_>) fstVertex sndVertex (createEdge : Func<_,_,_>) =
        let tempAdjVertices1 = graph.AdjacentVertices fstVertex
        let adjVertices2 = graph.AdjacentVertices sndVertex
        let adjVertices1 = tempAdjVertices1.Concat [fstVertex]
        let verticesToAdd = adjVertices1.Except adjVertices2
        for vertex in verticesToAdd do
            graph.AddEdge (createEdge.Invoke(sndVertex, vertex)) |> ignore
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

    type BaseNode<'TVertex, 'TEdge when 'TEdge :> IEdge<'TVertex>>(graph : UndirectedGraph<'TVertex, 'TEdge>, parent, verticesToPaintIfChild) =
        let mutable isVisited = false
        member this.Graph : UndirectedGraph<_,_> = graph
        member this.Parent : BaseNode<'TVertex, 'TEdge> option = parent
        member this.IsVisited
            with public get() = isVisited
            and public set value = isVisited <- value
        abstract member ChromaticPolinomial : int list
        default this.ChromaticPolinomial = []
        member this.VerticesToPaintIfChild : 'TVertex list = verticesToPaintIfChild

    type Leaf<'TVertex, 'TEdge when 'TEdge :> IEdge<'TVertex>>(graph, parent, verticesToPaintIfChild) as this =
        inherit BaseNode<'TVertex, 'TEdge>(graph, parent, verticesToPaintIfChild)
        do this.IsVisited <- true
        override this.ChromaticPolinomial = countChrPolCompleteGraph graph.VertexCount


    type Node<'TVertex, 'TEdge when 'TEdge :> IEdge<'TVertex>>(graph, parent, verticesToPaintIfChild) =
        inherit BaseNode<'TVertex, 'TEdge>(graph, parent, verticesToPaintIfChild)
        let mutable verticesToPaintIfParent : 'TVertex list = []
        let mutable leftChild : BaseNode<'TVertex, 'TEdge> option = None
        let mutable rightChild : BaseNode<'TVertex, 'TEdge> option = None
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
        member this.VerticesToPaintIfParent
            with public get() = verticesToPaintIfParent
            and public set value = verticesToPaintIfParent <- value
        override this.ChromaticPolinomial = sum this.LeftChild.ChromaticPolinomial this.RightChild.ChromaticPolinomial

    let buildTree (graph : UndirectedGraph<_,_>) (createEdge : Func<_,_,_>) =
        let rec buildTreeNode (graph : UndirectedGraph<_,_>) (parent : BaseNode<_,_> option) (verticesToPaintIfChild : 'Tvertex list) : BaseNode<_,_> =
            if graph.EdgeCount = graph.VertexCount * (graph.VertexCount - 1) / 2 then upcast new Leaf<_,_>(graph, parent, verticesToPaintIfChild)
            else
                let fst, snd = findFirstMissingEdge graph
                let graph1 = graph.Clone()
                let graph2 = graph.Clone()
                graph1.AddEdge(createEdge.Invoke(fst, snd)) |> ignore
                joinVertices graph2 fst snd createEdge
                let mutable node = new Node<_,_>(graph, parent, verticesToPaintIfChild)
                node.VerticesToPaintIfParent <- [fst; snd]
                node.LeftChild <- buildTreeNode graph1 (Some (upcast node)) [fst; snd]
                node.RightChild <- buildTreeNode graph2 (Some (upcast node)) [snd]
                upcast node
        buildTreeNode graph None []

    (*let findChromaticPolynomial (graph : UndirectedGraph<_,_>) (createEdge : Func<_,_,_>) =
        let tree = buildTree graph createEdge
        List.toArray (tree.CromaticPolinomial)*)

    let next (node : Node<_,_>) =
        node.IsVisited <- true
        if node.LeftChild.IsVisited then
            if node.RightChild.IsVisited then
                match node.Parent with
                | Some x -> x
                | None -> failwith "Next called on root when whole tree visited"
            else node.RightChild
        else node.LeftChild

    let prev (node : Node<_,_>) =
        if node.RightChild.IsVisited && not (node.RightChild :? Leaf<_,_>) then
            node.RightChild
        else
            if node.LeftChild.IsVisited && not (node.LeftChild :? Leaf<_,_>) then
                node.LeftChild
            else
                node.IsVisited <- false
                match node.Parent with
                | Some x -> x
                | None -> failwith "Prev called on root node"

    let isFirstNode (node : BaseNode<_,_>) =
        match node.Parent with
        | None -> (node :? Node<_,_>) && not (node :?> Node<_,_>).LeftChild.IsVisited
        | Some _ -> false

    let isLastNode (node : BaseNode<_,_>) =
        match node.Parent with
        | None -> (node :? Node<_,_>) && (node :?> Node<_,_>).RightChild.IsVisited
        | Some _ -> false

    let rec private findChromaticPolynomialAsList (graph : UndirectedGraph<_,_>) (createEdge : Func<_,_,_>) =
        if graph.EdgeCount = graph.VertexCount * (graph.VertexCount - 1) / 2 then countChrPolCompleteGraph graph.VertexCount
        else
            let fst, snd = findFirstMissingEdge graph
            let graph1 = graph.Clone()
            let graph2 = graph.Clone()
            graph1.AddEdge(new UndirectedEdge<_>(fst, snd)) |> ignore
            joinVertices graph2 fst snd createEdge
            sum (findChromaticPolynomialAsList graph1 createEdge) (findChromaticPolynomialAsList graph2 createEdge)

    let rec findChromaticPolynomial (graph : UndirectedGraph<_,_>) (createEdge : Func<_,_,_>) =
        List.toArray (findChromaticPolynomialAsList graph createEdge)