namespace QuickGraph.Algorithms

open System
open System.Collections.Generic

open QuickGraph

type EdgeColoringAlgorithm<'Vertex, 'Edge when 'Edge :> TaggedEdge<'Vertex, int> and 'Vertex: equality>() =
  let colored = new Event<_>()

  [<CLIEvent>]
  member this.Colored = colored.Publish

  static member private equalEdges (e1 : 'Edge) (e2 : 'Edge) =
    (e1.Source = e2.Source && e1.Target = e2.Target) ||
        (e1.Source = e2.Target && e1.Target = e2.Source)

  static member private anotherVertexInEdge (edge : 'Edge) vert =
    if edge.Source = vert then edge.Target else edge.Source

  member this.Compute (graph : UndirectedGraph<'Vertex, 'Edge>) =
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
    let rec shift (listEdges : 'Edge list) =
        match listEdges with
        | e1 :: e2 :: edges -> e1.Tag <- e2.Tag
                               colored.Trigger(e1)
                               shift (e2 :: edges)
        | e1 :: edges -> e1.Tag <- 0
                         colored.Trigger(e1)
        | _ -> ()

    //take subsezuense of edge seq before current edge
    let rec funSeqBeforeEdge s e res =
            match s with
            | hd :: tl when not <| EdgeColoringAlgorithm.equalEdges hd e -> funSeqBeforeEdge tl e <| hd :: res
            | hd :: tl                                                   -> hd :: res
            | []                                                         -> res

    //make subgraph with edges which have one of 2 selected colors
    let makeSubGraph color1 color2 =
        let subGraph = new UndirectedGraph<'Vertex, 'Edge>();
        Seq.iter (fun v -> subGraph.AddVertex(v) |> ignore) graph.Vertices
        Seq.iter (fun (e : 'Edge) -> if e.Tag = color1 || e.Tag = color2
                                     then subGraph.AddEdge(e) |> ignore) graph.Edges
        subGraph

        //make connected components in subgraph with 2 colors
    let rec makeConnectedComponent (subGraph : UndirectedGraph<'Vertex, 'Edge>) vertex (comp : 'Edge list) =
        let listOfEdges = Seq.toList <| subGraph.AdjacentEdges(vertex)
        let filteredlist = List.filter (fun e -> not <| List.exists (EdgeColoringAlgorithm.equalEdges e) comp) listOfEdges
        match filteredlist with
        | e :: es -> let another = EdgeColoringAlgorithm.anotherVertexInEdge e vertex
                     makeConnectedComponent subGraph another <| e :: comp
        | _      -> comp

    let inverseColorInComponent (comp : 'Edge list) color1 color2 =
        Seq.iter (fun (e : 'Edge) -> if e.Tag = color1 then e.Tag <- color2 else e.Tag <- color1
                                     colored.Trigger(e)
                                     ) comp

    let ColoringOneEdge (edge : 'Edge) =
        let sourse = edge.Source
        let sourseMissingColor =  firstMissingColor sourse
        let target = edge.Target
        let missingColorInBoth =
            List.tryFind (fun c -> isColorMissing sourse c &&
                                   isColorMissing target c) colors

        match missingColorInBoth with
        | Some c  -> //no conflict
                     edge.Tag <- c
                     colored.Trigger(edge)
        | None    ->
            //conflict
            let fanSeq = makeSequence sourse target [edge]
            let lastEdge  =  fanSeq.Head
            let anowerVertex = EdgeColoringAlgorithm.anotherVertexInEdge lastEdge sourse

            let lastColor = firstMissingColor anowerVertex
            let edgeWithLastColor = List.tryFind (fun (e : 'Edge) -> e.Tag = lastColor) fanSeq
            let reverseFanSeq = List.rev fanSeq
            match edgeWithLastColor with
            | Some e ->
                let edgeWithLastColorTarget = EdgeColoringAlgorithm.anotherVertexInEdge e sourse
                let beforeEdge = funSeqBeforeEdge reverseFanSeq e []
                shift <| List.rev beforeEdge

                let subgraph = makeSubGraph sourseMissingColor lastColor
                let componentVj = makeConnectedComponent subgraph anowerVertex []
                let componentVk = makeConnectedComponent subgraph edgeWithLastColorTarget []

                let isComponentVkContainsV0 = List.exists (fun (e : 'Edge) -> e.Source = sourse || e.Target = sourse) componentVk
                match isComponentVkContainsV0 with
                | true -> //"case 2.1"
                          let fanVkVj = funSeqBeforeEdge  fanSeq e []
                          shift fanVkVj
                          inverseColorInComponent componentVj  sourseMissingColor lastColor
                          lastEdge.Tag <- sourseMissingColor
                          colored.Trigger(lastEdge)
                | _    -> //"case 2.2"
                          inverseColorInComponent componentVk  sourseMissingColor lastColor
                          e.Tag <- sourseMissingColor
                          colored.Trigger(e)

            | _      -> //"case 1"
                        shift reverseFanSeq
                        lastEdge.Tag <- lastColor
                        colored.Trigger(lastEdge)

    List.iter ColoringOneEdge <| Seq.toList graph.Edges
    graph