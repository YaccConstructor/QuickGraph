namespace QuickGraph.Algorithms

open QuickGraph
open QuickGraph.Algorithms
open System.Collections.Generic

type public MaxCardinality<'Vertex, 'Edge when 'Edge :> IEdge<'Vertex> and 'Vertex: comparison and 'Edge: equality>(firstGraph : BidirectionalGraph<'Vertex, 'Edge>, secondGraph : BidirectionalGraph<'Vertex, 'Edge>, similarityMat : Dictionary<_,_>, threshold : double, createEdge) =
    //inherit AlgorithmBase<IVertexAndEdgeListGraph<'Vertex, 'Edge>>(null) /// no double algo base

    let trimMatching (v, u) ((H1Prev:Map<'Vertex,Set<'Vertex>>), H1Post) H2 (HGood, HMinus) =

        let intersectKeys m1 m2 = Map.fold (fun s k _ -> if Map.containsKey k m1 then Set.add k s else s) Set.empty m2

        let HKeys = Map.fold (fun s k _ -> Set.add k s ) Set.empty HGood

        let prevIntersection = Set.intersect (Map.find v H1Prev) HKeys
        let postIntersection = Set.intersect (Map.find v H1Post) HKeys

        let filterIntersection p sets =
          Set.fold (fun (HGood, HMinus) v'  ->
                let HGood_v' = Map.find v' HGood
                let HMinus_v' = Map.find v' HMinus
                let (HGood_v' , HMinus_v') =
                    Set.fold (fun ((HGood_v', HMinus_v') as state) u' ->
                    if p u' then Set.remove u' HGood_v', Set.add u' HMinus_v'
                    else state) (HGood_v' , HMinus_v') HGood_v' ///??? partition?
                Map.add v' HGood_v' HGood, Map.add v' HMinus_v' HMinus
             ) sets


        let (HGood,HMinus) =
            let p1 x = not <| Set.contains (x,u) H2
            let sets = prevIntersection |> filterIntersection p1 (HGood,HMinus)
            let p2 x = not <| Set.contains (u,x) H2
            postIntersection |> filterIntersection p2 sets

        (HGood,HMinus)

    let rec greedyMatch ((H1Prev:Map<'Vertex,Set<'Vertex>>), H1Post) H2 ((HGood:Map<'Vertex,Set<'Vertex>>), HMinus) =
        if Map.isEmpty HGood && Map.isEmpty HMinus then (Set.empty,Set.empty) else

        let found = HGood |> Map.tryPick (fun k v -> if Set.isEmpty v then None else Some (k,v))

        if found.IsNone then (Set.empty,Set.empty) else

        let v, G2Vertices = found.Value
        let u = G2Vertices.MaximumElement

        let G2Vertices = Set.remove u G2Vertices
        let HMinus_v = Set.union G2Vertices (Map.find v HMinus)
        let HMinus = Map.add v HMinus_v HMinus
        let HGood = Map.add v Set.empty HGood

        let (HGood, HMinus) = trimMatching (v,u) (H1Prev,H1Post) H2 (HGood,HMinus)

        let HPosGood = HGood |> Map.filter (fun _ s -> not <| Set.isEmpty s)
        let HPosMinus = HPosGood |> Map.map (fun _ _ -> Set.empty)


        let HNegGood = HMinus |> Map.filter (fun _ s -> not <| Set.isEmpty s)
        let HNegMinus = HNegGood |> Map.map (fun _ _ -> Set.empty)


        let (sigma1, I1) = greedyMatch (H1Prev, H1Post) H2 (HPosGood, HPosMinus)
        let (sigma2, I2) = greedyMatch (H1Prev, H1Post) H2 (HNegGood, HNegMinus)


        let maxByCount s1 s2 = if Set.count s1 > Set.count s2 then s1 else s2
        let sigma1 = sigma1 |> Set.add (v,u)
        let sigma = maxByCount sigma1 sigma2

        let I2 = I2 |> Set.add (v,u)
        let I = maxByCount I1 I2
        (sigma, I)


    member this.compMaxCardinality() =

        let (H1Prev, H1Post, HGood, HMinus) =
            firstGraph.Vertices |> Seq.fold (fun (H1Prev, H1Post, HGood, HMinus) v ->

                let H1Post =
                    let outEdges = firstGraph.OutEdges(v)
                    let t = outEdges |> Seq.map (fun e -> e.Target) |> Set.ofSeq
                    H1Prev |> Map.add v t


                let H1Prev =
                    let inEdges = firstGraph.InEdges(v)
                    let t = inEdges |> Seq.map (fun e -> e.Source) |> Set.ofSeq
                    H1Post |> Map.add v t


                let similarity =
                    secondGraph.Vertices
                    |> Seq.fold (fun s u ->
                        if similarityMat.[(v, u)] >= threshold then Set.add u s else s
                    ) Set.empty

                let HGood = HGood |> Map.add v similarity

                let HMinus =  HMinus |> Map.add v Set.empty

                (H1Prev, H1Post, HGood, HMinus)

            ) (Map.empty, Map.empty, Map.empty, Map.empty)

        let transClosure = secondGraph.ComputeTransitiveClosure(createEdge)

        let H2 =
            seq { for x in secondGraph.Vertices do
                    for y in secondGraph.Vertices do
                      if transClosure.ContainsEdge(x, y)
                      then yield (x,y) } |> Set.ofSeq


        let rec loop (sigmaM : Set<'Vertex * 'Vertex>) (H1Prev, H1Post, H2, HGood : Map<'Vertex, Set<'Vertex>>, HMinus : Map<'Vertex, Set<'Vertex>>) =
            if HGood.Count + HMinus.Count > sigmaM.Count then
                let (sigma, I) = greedyMatch (H1Prev, H1Post) H2 (HGood, HMinus)

                let (HGood, HMinus) =
                    I |> Set.fold (fun s (v, u)  ->
                        let HGood = HGood |> Map.remove v
                        let HMinus = HMinus |> Map.remove u
                        (HGood, HMinus)
                    ) (HGood,HMinus)

                loop
                    (if sigma.Count > sigmaM.Count then sigma else sigmaM)
                    (H1Prev, H1Post, H2, HGood, HMinus)
            else
                sigmaM

        let sigmaM = loop Set.empty (H1Prev, H1Post, H2, HGood, HMinus)

        sigmaM

