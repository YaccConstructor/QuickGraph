(* 
    Maximum Independent Set Algorithm O(2^0.245n)
    Author: Robson J.M.
    Implemented by Matvey Bryksin
*)
namespace QuickGraph.Algorithms

open QuickGraph

type MaximumIndependentSetAlgorithm private () = 
    static member private cleanGraph(original_graph: UndirectedGraph<int, Edge<int>>,vertices,vertexlist_list) =
        let new_graph = MaximumIndependentSetAlgorithm.createGraph(original_graph.Vertices, original_graph.Edges) :> UndirectedGraph<int, Edge<int>>
        Seq.iter (fun v -> if Seq.exists ((=)v) new_graph.Vertices then new_graph.RemoveVertex(v) |> ignore) vertices
        Seq.iter (fun l -> Seq.iter (fun v -> if Seq.exists ((=)v) new_graph.Vertices then new_graph.RemoveVertex(v) |> ignore) l ) vertexlist_list
        new_graph

    static member private createGraph(vertices, edges) =
        let graph = new UndirectedGraph<int, Edge<int>>()
        Seq.iter (fun e -> graph.AddVerticesAndEdge(e) |> ignore) edges
        Seq.iter (fun v -> graph.AddVertex(v) |> ignore) vertices
        graph

    static member private addUnique(l1, l2) =
        Seq.fold ( fun acc x -> if Seq.exists ((=)x) acc then acc else acc |> Seq.append [x]) l1 l2

    static member private neighbors(v, (graph:UndirectedGraph<int, Edge<int>>)) = 
        graph.AdjacentEdges(v) |> Seq.fold (fun acc e -> if e.Target = v then Seq.append [e.Source] acc else Seq.append [e.Target] acc) Seq.empty

    static member private dominates(A: int, B: int, graph: UndirectedGraph<int, Edge<int>>) =
        let Na = (MaximumIndependentSetAlgorithm.neighbors(A,graph)) |> Seq.append [A]
        let Nb = (MaximumIndependentSetAlgorithm.neighbors(B,graph)) |> Seq.append [B]
        if (Seq.length Na) > (Seq.length Nb)
            then
                Seq.fold (fun acc x -> (Seq.exists ((=)x) Na) && acc) true Nb    
            else false
    
    static member private sortpairs(gr: UndirectedGraph<int, Edge<int>>, vertexset: seq<int>):seq<int*int> =
        let rec qsort(l:list<int*int>) =
            match l with
            | [] -> []
            | x::xs -> qsort(xs |> List.filter(fun (v,d) -> d < (snd x) ) )
                       @[x]
                       @qsort(xs |> List.filter(fun (v,d) -> d >= (snd x) ) )

        let pair_degrees = Seq.map (fun v -> (v,gr.AdjacentDegree(v)) ) vertexset
        let result = (Seq.toList pair_degrees) |> qsort :> seq<int*int>
        result

    static member private ms2(graph: UndirectedGraph<int, Edge<int>>, vertexset: seq<int>):seq<int> = 
        let edges2(vj,vk) =
            let n1 = MaximumIndependentSetAlgorithm.neighbors(fst vj,graph)
            let n2 = MaximumIndependentSetAlgorithm.neighbors(fst vk,graph) 
            let g5 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst vj; fst vk],[n1; n2])
            MaximumIndependentSetAlgorithm.ms(g5) |> Seq.append [(fst vj);(fst vk)]

        let edge1(vi,vj,vk) =
            let ns_vk = MaximumIndependentSetAlgorithm.neighbors(fst vk,graph)
            let g5 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst vk],[ns_vk])
            MaximumIndependentSetAlgorithm.ms1(g5,[vi;vj]) |> Seq.append [(fst vk)]

        let vertexN(si,sj) =
            let nSi = MaximumIndependentSetAlgorithm.neighbors(si,graph)
            let nSj = MaximumIndependentSetAlgorithm.neighbors(sj,graph)
            let nSi_Sj = Seq.fold (fun acc x -> if (Seq.exists ((=)x) nSj) then (Seq.append acc [x]) else acc) Seq.empty nSi
            let NSiSj = Seq.toList nSi_Sj
            match NSiSj with
            | x::xs -> Some x
            | [] -> None
        let returnIS(v, set) =
            let g5 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[v],[[]])
            MaximumIndependentSetAlgorithm.ms2(g5, set)


        let pair_sorted = MaximumIndependentSetAlgorithm.sortpairs(graph,vertexset)
        let lenght = Seq.length pair_sorted
        match (lenght) with
        | 0 -> Seq.empty

        | 1 -> Seq.empty

        | 2 -> let take2 = Seq.take 2 pair_sorted
               let s1 = Seq.head take2
               let s2 = Seq.last take2

               let np_1 = MaximumIndependentSetAlgorithm.neighbors(fst s1,graph)
               let np_2 = MaximumIndependentSetAlgorithm.neighbors(fst s2,graph)
               let g3 = MaximumIndependentSetAlgorithm.cleanGraph(graph, [fst s1; fst s2], [np_1; np_2])


               if graph.ContainsEdge(fst s1,fst s2)
                   then Seq.empty
                   else MaximumIndependentSetAlgorithm.ms(g3) |> Seq.append [(fst s1);(fst s2)]

        | 3 -> let take2 = Seq.take 2 pair_sorted
               let s1 = Seq.head take2
               let s2 = Seq.last take2
               let s3 = Seq.last pair_sorted
               
               if (snd s1) = 0
                    then
                        let g3 = MaximumIndependentSetAlgorithm.cleanGraph(graph, [fst s1], [])
                        MaximumIndependentSetAlgorithm.ms1(g3, Seq.filter ((<>)(fst s1)) vertexset) |> Seq.append [(fst s1)]
                    else
                        let n1 = MaximumIndependentSetAlgorithm.neighbors(fst s1,graph)
                        let g1 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst s1],[n1])
                        if graph.ContainsEdge(fst s1,fst s2) && graph.ContainsEdge(fst s2,fst s3) && graph.ContainsEdge(fst s1,fst s3)
                            then Seq.empty
                            else
                                if graph.ContainsEdge(fst s1,fst s2) && graph.ContainsEdge(fst s1,fst s3)
                                    then edges2(s2,s3)
                                    else
                                        if graph.ContainsEdge(fst s2,fst s1) && graph.ContainsEdge(fst s2,fst s3)
                                            then edges2(s1,s3)
                                            else
                                                 if graph.ContainsEdge(fst s1,fst s3) && graph.ContainsEdge(fst s3,fst s2)
                                                    then edges2(s1,s2)
                                                    else 
                                                        if graph.ContainsEdge(fst s3,fst s1)
                                                            then edge1(fst s3,fst s1, s2)   
                                                            else
                                                                if graph.ContainsEdge(fst s2,fst s1)
                                                                    then edge1(fst s2,fst s1, s3)    
                                                                    else
                                                                        if graph.ContainsEdge(fst s2,fst s3)
                                                                            then edge1(fst s2,fst s3, s1)    
                                                                            else 
                                                                                let res1 = vertexN(fst s1,fst s2)
                                                                                match res1 with
                                                                                | Some x1 -> returnIS(x1, vertexset)
                                                                                | None ->   let res2 = vertexN(fst s2,fst s3)
                                                                                            match res2 with
                                                                                            | Some x2 -> returnIS(x2, vertexset)
                                                                                            | None ->   let res3 = vertexN(fst s1,fst s3)
                                                                                                        match res3 with
                                                                                                        | Some x3 -> returnIS(x3, vertexset)
                                                                                                        | None ->    if (snd s1) = 1
                                                                                                                        then 
                                                                                                                            MaximumIndependentSetAlgorithm.ms1(g1, Seq.filter ((<>)(fst s1)) vertexset) |> Seq.append [(fst s1)]
                                                                                                                        else
                                                                                                                            let max1 = MaximumIndependentSetAlgorithm.ms1(g1,Seq.filter ((<>)(fst s1)) vertexset) |> Seq.append [(fst s1)]

                                                                                                                            let ns_3 = MaximumIndependentSetAlgorithm.neighbors(fst s3,graph)
                                                                                                                            let ns_2 = MaximumIndependentSetAlgorithm.neighbors(fst s2,graph)
                                                                                                                            let gr1 = MaximumIndependentSetAlgorithm.cleanGraph(graph, [fst s1;fst s2;fst s3], [ns_2;ns_3])
                                                                                                                            
                                                                                                                            let max2 = MaximumIndependentSetAlgorithm.ms2(gr1, n1)
                                                                                                                            if (Seq.length max1) + 1 > (Seq.length max2) + 2
                                                                                                                                then max1 |> Seq.append [(fst s1)]
                                                                                                                                else max2 |> Seq.append [(fst s2);(fst s3)]
        
        | 4 -> if Seq.exists (fun v -> graph.AdjacentDegree(v) <= 3) graph.Vertices
                   then MaximumIndependentSetAlgorithm.ms(graph)
                   else
                       let s1 = Seq.head pair_sorted
                       let nn_1 = MaximumIndependentSetAlgorithm.neighbors(fst s1,graph)
                       let ge1 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst s1],[nn_1])

                       let max1 = MaximumIndependentSetAlgorithm.ms(ge1)
               
                       let ge2 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst s1],[])

                       let max2 = MaximumIndependentSetAlgorithm.ms2(ge2,Seq.filter ((<>)(fst s1)) vertexset)

                       if (Seq.length max1) + 1 > (Seq.length max2)
                           then max1 |> Seq.append [(fst s1)]
                           else max2
                           
         | _ -> MaximumIndependentSetAlgorithm.ms(graph) 

    static member private ms1(graph: UndirectedGraph<int, Edge<int>>, vertexset: seq<int>):seq<int> =
        let pair_sorted = MaximumIndependentSetAlgorithm.sortpairs(graph,vertexset)
        let s1 = Seq.head pair_sorted

        if snd(s1) <= 1 
            then MaximumIndependentSetAlgorithm.ms(graph)
            else let take2 = Seq.take 2 pair_sorted
                 let s2 = Seq.last take2
                 let neighborsS1 = MaximumIndependentSetAlgorithm.neighbors(fst s1,graph)
                 let neighborsS2 = MaximumIndependentSetAlgorithm.neighbors(fst s2,graph)

                 let g1 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst s1],[neighborsS1])
                 let g2 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst s2],[neighborsS2])
                 
                 if graph.ContainsEdge(fst s1,fst s2)
                    then if (snd s1) <= 3
                            then MaximumIndependentSetAlgorithm.ms(graph)
                            else 
                                let max1 = MaximumIndependentSetAlgorithm.ms(g1)
                                let max2 = MaximumIndependentSetAlgorithm.ms(g2)
                                if (Seq.length max1) > (Seq.length max2)
                                    then max1 |> Seq.append [(fst s1)]
                                    else max2 |> Seq.append [(fst s2)]
                    else let Ns1_Ns2 = Seq.fold (fun acc v -> if Seq.exists ((=)v) neighborsS1 then (Seq.append [v] acc) else acc ) Seq.empty neighborsS2 
                         if (Seq.length Ns1_Ns2) <> 0
                            then 
                                 let g3 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[],[Ns1_Ns2])
                                 MaximumIndependentSetAlgorithm.ms1(g3, vertexset)
                            else
                                if snd(s2) = 2
                                    then
                                        let E = Seq.head neighborsS1
                                        let F = Seq.last neighborsS1
                                        if graph.ContainsEdge(E,F)
                                            then MaximumIndependentSetAlgorithm.ms(g1) |> Seq.append [(fst s1)]
                                            else
                                                let ns_E = MaximumIndependentSetAlgorithm.neighbors(E,graph)
                                                let ns_F = MaximumIndependentSetAlgorithm.neighbors(F,graph)
                                                let N_EF = MaximumIndependentSetAlgorithm.addUnique(ns_E, ns_F) |>
                                                           Seq.filter ((<>) (fst s1))
                                                let more = Seq.fold (fun acc x -> (Seq.exists ((=)x) N_EF) && acc) true neighborsS2

                                                if ((Seq.length N_EF) > (Seq.length neighborsS2)) && more
                                                    then 
                                                         let g3 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst s1; fst s2],[neighborsS1;neighborsS2])
                                                         MaximumIndependentSetAlgorithm.ms(g3) |> Seq.append [E;F;(fst s2)]
                                                    else
                                                        let nm_2 = MaximumIndependentSetAlgorithm.neighbors((fst s2),graph)
                                                        let g4 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[fst s2;E;F],[ns_E;ns_F;nm_2])

                                                        let max1 = MaximumIndependentSetAlgorithm.ms(g1)
                                                        let max2 = MaximumIndependentSetAlgorithm.ms(g4)
                                
                                                        if (Seq.length max1) + 1 > (Seq.length max2) + 3
                                                            then max1 |> Seq.append [(fst s1)]
                                                            else max2 |> Seq.append [E;F;(fst s2)]       
                                    else
                                        let max1 = MaximumIndependentSetAlgorithm.ms(g2)

                                        if Seq.exists ((=) (fst s2)) g1.Vertices then g1.RemoveVertex((fst s2)) |> ignore
                                        
                                        let max2 = MaximumIndependentSetAlgorithm.ms2(g1, neighborsS2)
                                        if (Seq.length max1) > (Seq.length max2)
                                            then max1 |> Seq.append [(fst s2)]
                                            else max2 |> Seq.append [(fst s1)] 

        


    static member private ms(graph: UndirectedGraph<int, Edge<int>>):seq<int> =
        if graph.VertexCount <= 1 
            then graph.Vertices
            else 
                let degrees = Seq.map (fun v -> graph.AdjacentDegree(v) ) graph.Vertices
                let minDegree = Seq.min degrees
                let A = Seq.find (fun v -> graph.AdjacentDegree(v) = minDegree) graph.Vertices
                let neighborsA = MaximumIndependentSetAlgorithm.neighbors(A,graph)
                
                let g1 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[A],[neighborsA])

                match graph.AdjacentDegree(A) with
                | 0 -> MaximumIndependentSetAlgorithm.ms(g1) |> Seq.append [A]

                | 1 -> MaximumIndependentSetAlgorithm.ms(g1) |> Seq.append [A]

                | 2 -> let maxDegree = Seq.map (fun v -> graph.AdjacentDegree(v) ) neighborsA |> Seq.max
                       let B = Seq.find (fun v -> graph.AdjacentDegree(v) = maxDegree) neighborsA
                       let gB = MaximumIndependentSetAlgorithm.cleanGraph(graph,[B],[])

                       let B2 = Seq.filter ((<>)B) neighborsA |> Seq.head
                       if graph.ContainsEdge(B,B2)
                           then MaximumIndependentSetAlgorithm.ms(g1) |> Seq.append [A]
                           else 
                                let ns_B1 = MaximumIndependentSetAlgorithm.neighbors(B, graph)
                                let ns_B2 = MaximumIndependentSetAlgorithm.neighbors(B2, graph)
                                let g2 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[B;B2],[ns_B1;ns_B2])
                                let ms_res = MaximumIndependentSetAlgorithm.ms(g2)

                                let N2_A = Seq.fold (fun acc x -> MaximumIndependentSetAlgorithm.addUnique(MaximumIndependentSetAlgorithm.neighbors(x,graph), acc) ) Seq.empty neighborsA |>
                                           Seq.filter ((<>)A)
                                let ms2_res = MaximumIndependentSetAlgorithm.ms2(g1,N2_A)

                                if (Seq.length ms_res) + 2 > (Seq.length ms2_res) + 1
                                    then
                                        ms_res |> Seq.append [B;B2]
                                    else
                                        ms2_res |> Seq.append [A]

                | 3 -> let g2 = MaximumIndependentSetAlgorithm.cleanGraph(graph,[A],[])

                       let ms2_res = MaximumIndependentSetAlgorithm.ms2(g2, neighborsA)
                       let ms_res = MaximumIndependentSetAlgorithm.ms(g1)
                       if (Seq.length ms_res) + 1 > (Seq.length ms2_res)
                           then
                               ms_res |> Seq.append [A]
                           else
                               ms2_res

                | _ -> let maxDegree = Seq.map (fun v -> graph.AdjacentDegree(v) ) neighborsA |> Seq.max
                       let B = Seq.find (fun v -> graph.AdjacentDegree(v) = maxDegree) neighborsA
                       let gB = MaximumIndependentSetAlgorithm.cleanGraph(graph,[B],[])

                       if MaximumIndependentSetAlgorithm.dominates(A,B,graph) 
                           then MaximumIndependentSetAlgorithm.ms(gB)
                           else                   
                               let gb_res1 = MaximumIndependentSetAlgorithm.ms(gB)
                               let neighborsB = MaximumIndependentSetAlgorithm.neighbors(B, graph)
                               let gB_2 = MaximumIndependentSetAlgorithm.cleanGraph(gB,[],[neighborsB])
                               let gb_res2 = MaximumIndependentSetAlgorithm.ms(gB_2)
                               if (Seq.length gb_res1) > (Seq.length gb_res2) + 1
                                   then gb_res1
                                   else gb_res2 |> Seq.append [B]


    static member Compute(vertices: int list, edges:Edge<int> list) =
        let graph = MaximumIndependentSetAlgorithm.createGraph(vertices, edges)
        MaximumIndependentSetAlgorithm.ms(graph)

    static member Compute(graph: UndirectedGraph<int, Edge<int>>) =
        MaximumIndependentSetAlgorithm.ms(graph)