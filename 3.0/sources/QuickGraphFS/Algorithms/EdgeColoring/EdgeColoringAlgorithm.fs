namespace QuickGraph.Algorithms

open System
open System.Collections.Generic

open QuickGraph

type EdgeColoringAlgorithm =
  static member private equalEdges (e1 : TaggedEdge<int,int>) (e2 : TaggedEdge<int,int>) = 
        (e1.Source = e2.Source && e1.Target = e2.Target) ||
        (e1.Source = e2.Target && e1.Target = e2.Source)

  static member private anotherVertexInEdge (edge : TaggedEdge<int,int>) vert = 
       if edge.Source = vert then edge.Target else edge.Source

  static member Compute (graph : UndirectedGraph<int, TaggedEdge<int, int>>) = 
    let maxDelta = Seq.map graph.AdjacentDegree graph.Vertices |> Seq.max
    let colors = [1..maxDelta + 1]

    let isColorMissing vertex color = 
        graph.AdjacentEdges(vertex) 
        |> Seq.exists (fun e -> e.Tag = color) 
        |> not

    let firstMissingColor v =
        let colors = List.filter (isColorMissing v) colors
        colors.Head
        
    let rec makeSequence sourse target fanSeq = 
        let canAddEdge c = 
            graph.AdjacentEdges(sourse) 
            |> Seq.tryFind (fun edge -> edge.Tag = c &&
                                        not <| List.exists (EdgeColoringAlgorithm.equalEdges edge) fanSeq)
        let targetColorMissing = firstMissingColor target
        match canAddEdge targetColorMissing with
        | Some edge -> 
                let anowerVertex = EdgeColoringAlgorithm.anotherVertexInEdge edge sourse
                makeSequence sourse anowerVertex <| edge :: fanSeq
        | None   -> fanSeq

    //shift left colors in sequense. first color get 0 color
    let rec shift (listEdges : TaggedEdge<int, int> list) =
        match listEdges with
        | e1 :: e2 :: edges -> 
            e1.Tag <- e2.Tag
            shift (e2 :: edges) 
        | e1 :: edges -> e1.Tag <- 0
        | _ -> ()

    //take subsezuense of edge seq before current edge
    let rec funSeqBeforeEdge s e res =
            match s with
            | hd :: tl when not <| EdgeColoringAlgorithm.equalEdges hd e -> funSeqBeforeEdge tl e <| hd :: res
            | hd :: tl                             -> hd :: res
            | []                                   -> res

    //make subgraph with edges which have one of 2 selected colors
    let makeSubGraph color1 color2 =
        let subGraph = new UndirectedGraph<int, TaggedEdge<int, int>>();
        Seq.iter (fun v -> subGraph.AddVertex(v) |> ignore) graph.Vertices
        Seq.iter (fun (e : TaggedEdge<int, int>) -> if e.Tag = color1 || e.Tag = color2 
                                                    then subGraph.AddEdge(e) |> ignore) graph.Edges
        subGraph
        
        //make connected components in subgraph with 2 colors
    let rec makeConnectedComponent (subGraph : UndirectedGraph<int, TaggedEdge<int, int>>) vertex (comp : TaggedEdge<int, int> list) = 
        let listOfEdges = Seq.toList <| subGraph.AdjacentEdges(vertex) 
        let filteredlist = List.filter (fun e -> not <| List.exists (EdgeColoringAlgorithm.equalEdges e) comp) listOfEdges          
        match filteredlist with
        | e :: es -> 
                        let another = EdgeColoringAlgorithm.anotherVertexInEdge e vertex
                        makeConnectedComponent subGraph another <| e :: comp
        | _      -> comp

    let inverseColorInComponent (comp : TaggedEdge<int, int> list) color1 color2 = 
        Seq.iter (fun (e : TaggedEdge<int, int>) -> if e.Tag = color1 then e.Tag <- color2 else e.Tag <- color1) comp
               
    let ColoringOneEdge (edge : TaggedEdge<int, int>) = 
        let sourse = edge.Source
        let sourseMissingColor =  firstMissingColor sourse
        let target = edge.Target
        let missingColorInBoth = 
            List.tryFind (fun c -> isColorMissing sourse c &&
                                   isColorMissing target c) colors
                                     
        match missingColorInBoth with
        | Some c  -> //no conflict
                     edge.Tag <- c 
                     
        | None    -> 
            //conflict
            let fanSeq = makeSequence sourse target [edge]
            let lastEdge  =  fanSeq.Head
            let anowerVertex = EdgeColoringAlgorithm.anotherVertexInEdge lastEdge sourse

            let lastColor = firstMissingColor anowerVertex
            let edgeWithLastColor = List.tryFind (fun (e : TaggedEdge<int, int>) -> e.Tag = lastColor) fanSeq
            let reverseFanSeq = List.rev fanSeq
            match edgeWithLastColor with
            | Some e -> 
                let edgeWithLastColorTarget = EdgeColoringAlgorithm.anotherVertexInEdge e sourse
                let beforeEdge = funSeqBeforeEdge reverseFanSeq e []
                shift <| List.rev beforeEdge

                let subgraph = makeSubGraph sourseMissingColor lastColor
                let componentVj = makeConnectedComponent subgraph anowerVertex []
                let componentVk = makeConnectedComponent subgraph edgeWithLastColorTarget []

                let isComponentVkContainsV0 = List.exists (fun (e : TaggedEdge<int, int>) -> e.Source = sourse || e.Target = sourse) componentVk
                match isComponentVkContainsV0 with
                | true -> //"case 2.1"
                          let fanVkVj = funSeqBeforeEdge  fanSeq e []
                          shift fanVkVj
                          inverseColorInComponent componentVj  sourseMissingColor lastColor
                          lastEdge.Tag <- sourseMissingColor                              
                | _    -> //"case 2.2"
                          inverseColorInComponent componentVk  sourseMissingColor lastColor
                          e.Tag <- sourseMissingColor
                       
            | _      -> //"case 1"
                        shift reverseFanSeq
                        lastEdge.Tag <- lastColor

    List.iter ColoringOneEdge <| Seq.toList graph.Edges
    graph