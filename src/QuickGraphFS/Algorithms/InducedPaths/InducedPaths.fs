namespace QuickGraph.Algorithms

open QuickGraph
open System.Collections.Generic

module InducedPathAlgorithm =
    let findInducedPaths(graph : UndirectedGraph<_,_>) =
        let isEdge (x, y) = graph.ContainsEdge (x, y)

        let vertices = Set.ofSeq graph.Vertices

        let chooseAdj (edge: IEdge<_>) v = 
            let (x, y) = edge.Target, edge.Source
            if x <> v then x else y
        let adjVertices v = Set.ofSeq <| Seq.map (fun edge -> chooseAdj edge v) (graph.AdjacentEdges v)

        let createPaths map filter list = List.map map (List.filter filter list)

        let paths2 = Seq.fold (fun acc (edge: IEdge<_>) -> [edge.Target; edge.Source]::acc) [] graph.Edges
        let paths3 = 
            let rec pairwise list = 
                match list with
                | hd::tail -> [ for v in tail -> (hd, v)] @ (pairwise tail)
                | [] -> []

            let buildPaths list x y  = createPaths (fun z -> [y; z; x]) (fun z -> isEdge(x, z)) list
            let pairs = pairwise <| Set.toList vertices
            List.concat <| createPaths (fun (x, y) -> buildPaths (Set.toList <| adjVertices y) x y) (not << isEdge) pairs

        let rec listPaths k =
            let chooseMax (paths1: _ list list) (paths2: _ list list) = 
                match paths1, paths2 with
                | [], _ -> paths2
                | _, [] -> paths1
                | hd1::_, hd2::_ -> if hd1.Length > hd2.Length then paths1
                                    else paths2

            let nonAdjVert list = vertices - List.fold (fun acc v -> acc + adjVertices v) Set.empty list
            let adjToHead (list: _ list) = Set.intersect (nonAdjVert list.Tail) (adjVertices list.Head)
            let adjToLast list = adjToHead <| List.rev list

            match k with
            | 2 -> paths2
            | 3 -> paths3
            | 4 | 5 ->
                let paths = if k = 4 then listPaths 2
                            else listPaths 3

                if paths.Length > 0 && paths.Head.Length = k - 2 then 
                    let findPaths path =
                        let a = adjToHead path
                        let b = adjToLast path
                        let pairs = [ for x in a do for y in b do yield (x, y) ]
                        createPaths (fun (x, y) -> x::path @ [y]) (not << isEdge) pairs

                    chooseMax paths (List.collect findPaths paths)
                else paths
            | _ -> 
                let paths = listPaths (k - 4)

                if paths.Length > 0 && paths.Head.Length = k - 4 then 
                    let findPaths path =
                        let a = adjToHead path
                        let b = adjToLast path
                        let c = nonAdjVert path
                                       
                        let findPaths' (x, y) = 
                            let a' = Set.filter (fun s -> isEdge(s, x) && not <| isEdge(s, y)) c
                            let b' = Set.filter (fun s -> isEdge(s, y) && not <| isEdge(s, x)) c
                            let pairs = [ for u in a' do for v in b' do yield (u, v) ]

                            createPaths (fun (u, v) -> u::x::path @ [y; v]) (not << isEdge) pairs
     
                        let pairs = [ for x in a do for y in b do yield (x, y) ]

                        List.concat <| createPaths findPaths' (not << isEdge) pairs

                    chooseMax paths (List.collect findPaths paths)
                else paths

        let res =
            let paths = [ for k in max 2 (graph.VertexCount - 4) .. graph.VertexCount -> listPaths k]
            List.filter (fun (x: _ list) -> x.Length > 0) paths

        let toList list =
            let newList = new System.Collections.Generic.List<_>()
            List.iter (fun x -> newList.Add(x)) list
            newList
                
        let maxPaths = if res.Length = 0 then []
                       else List.maxBy (fun (paths: _ list list) -> paths.Head.Length) res
        toList <| List.map (fun path -> toList path) maxPaths
