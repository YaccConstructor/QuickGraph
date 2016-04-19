namespace QuickGraph.Algorithms

open QuickGraph
open System.Collections.Generic

module InducedPathAlgorithm =
    let findInducedPaths(graph : UndirectedGraph<_,_>) =
        let isEdge (x, y) = graph.ContainsEdge (x, y)

        let vertices = Set.ofSeq graph.Vertices

        let adjVertices = Set.ofSeq << graph.AdjacentVertices 

        let createPaths map pairs = List.map map (List.filter (not << isEdge) pairs)

        let paths2 = Seq.fold (fun acc (edge: IEdge<_>) -> [edge.Target; edge.Source]::acc) [] graph.Edges
        let paths3 = 
            let rec pairwise list = 
                match list with
                | hd::tail -> [ for v in tail -> (hd, v)] @ (pairwise tail)
                | [] -> []

            let findPaths x = createPaths (fun (y, z) -> [y; x; z]) (pairwise << Set.toList <| adjVertices x)

            List.collect findPaths (Set.toList vertices)

        let rec listPaths k =
            let pairwise set1 set2 = [ for x in set1 do for y in set2 do yield (x, y) ]

            let chooseMax paths1 paths2: list<list<_>> = 
                match paths1, paths2 with
                | [], _ -> paths2
                | _, [] -> paths1
                | hd1::_, hd2::_ -> if hd1.Length > hd2.Length then paths1
                                    else paths2

            let nonAdjVert list = vertices - List.fold (fun acc v -> acc + adjVertices v) Set.empty list
            let adjToHead (list: list<_>) = Set.intersect (nonAdjVert list.Tail) (adjVertices list.Head)
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
  
                        createPaths (fun (x, y) -> x::path @ [y]) <| pairwise a b

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

                            createPaths (fun (u, v) -> u::x::path @ [y; v]) <| pairwise a' b'

                        List.concat <| createPaths findPaths' (pairwise a b)
                        
                    chooseMax paths (List.collect findPaths paths)
                else paths

        let res =
            let paths = [ for k in max 2 (graph.VertexCount - 4) .. graph.VertexCount -> listPaths k]
            List.filter (fun (x: list<_>) -> x.Length > 0) paths

        let toList list =
            let newList = new System.Collections.Generic.List<_>()
            List.iter newList.Add list
            newList
                
        let maxPaths = if res.Length = 0 then []
                       else List.maxBy (List.head >> List.length) res
        toList <| List.map (fun path -> toList path) maxPaths
