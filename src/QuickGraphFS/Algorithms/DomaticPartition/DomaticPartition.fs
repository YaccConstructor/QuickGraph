(*
    Graph domatic partition & domatic number
    Reference: 
        Combinatorial Bounds via Measure and Conquer: Bounding Minimal Dominating Sets and Applications
        Fomin et al.
*)

namespace QuickGraph.Algorithms

open System
open QuickGraph

type public DomaticPartition<'Vertex, 'Edge when 'Edge :> IEdge<'Vertex> and 'Vertex: comparison>(graph: IUndirectedGraph<'Vertex, 'Edge>) =  
    let frequency (s: Set<Set<'Vertex>>) (v: 'Vertex) = 
        Set.fold (fun acc (subset: Set<'Vertex>) -> if subset.Contains v then acc + 1 else acc) 0 s

    let containingSets (s: Set<Set<'Vertex>>) (v: 'Vertex) =
        Set.fold (fun acc (subset: Set<'Vertex>) -> if subset.Contains v then (Set.add subset acc) else acc) Set.empty s

    let tryFindForPairs func set =
        let pairs = Set(set |> Seq.collect (fun x -> Set.remove x set |> Seq.map (fun y -> x, y)))
        pairs |> Seq.tryFind func

    let closedNeigh (v: 'Vertex) = 
        graph.AdjacentEdges v |> Seq.map (fun (e: 'Edge) -> 
            if e.Target = v then e.Source else e.Target)
        |> (fun seqOpenNeigh -> Set.add v <| Set(seqOpenNeigh))

    let closedNeighs() = 
        graph.Vertices 
        |> Seq.map closedNeigh
        |> set

    // Getting all the minimal set covers of universe U from set of sets S accumulating in C
    member this.MSCs (u: Set<'Vertex>) (sSrc: Set<Set<'Vertex>>) =
        let mscList = ref []

        let rec computeMSC (u: Set<'Vertex>) (sSrc: Set<Set<'Vertex>>) (c: Set<Set<'Vertex>>) = 
            let card set = (Set.intersect u set).Count

            let s = Set.filter (fun subset -> card subset <> 0) sSrc

            if u.IsEmpty then   
                mscList := c :: (!mscList)
            elif s.IsEmpty then failwith "No set cover"
            else 
                let uniqueElem = u |> Seq.tryFind (fun elem -> frequency s elem = 1)
                match uniqueElem with
                | Some uniqueElemVal ->
                    let uniqueElemSet = s |> Seq.find (fun (set: Set<'Vertex>) -> set.Contains uniqueElemVal)
                    computeMSC (Set.difference u uniqueElemSet) (Set.remove uniqueElemSet s) (Set.add uniqueElemSet c)
                | None ->
                    let pairSameContained = u |> tryFindForPairs (fun (elem1, elem2) -> containingSets s elem1 = containingSets s elem2)
                    match pairSameContained with
                    | Some pairSameContainedVal -> computeMSC (Set.remove (fst pairSameContainedVal) u) s c
                    | None ->
                        let s' = ref Set.empty
                        let singletonsElem = u |> Seq.tryFind (fun elem ->
                            let uContainingSets = containingSets s elem
                            if Set.forall (fun (subset: Set<'Vertex>) -> card subset = 1) uContainingSets then
                                s' := uContainingSets
                                true
                            else false)
                        match singletonsElem with   
                        | Some singletonsElemVal ->                  
                            !s' |> Set.iter (fun elem -> 
                                computeMSC <| Set.remove singletonsElemVal u
                                           <| Set.difference s !s'
                                           <| Set.add elem c)
                        | None ->
                            let sMaxCard = card <| Seq.maxBy (fun (subset: Set<'Vertex>) -> card subset) s
                            let sMax = Set.filter (fun (subset: Set<'Vertex>) -> card subset = sMaxCard) s
                            let freq2subsetElem = Seq.tryPick (fun subset -> 
                                let elem = Seq.tryFind (fun elem -> frequency s elem = 2 && u.Contains elem) subset
                                elem |> Option.map (fun elemVal -> subset, elemVal)) sMax

                            match freq2subsetElem with
                            | Some freq2subsetElemVal ->
                                let freq2subset, freq2elem = freq2subsetElemVal
                                let s' = Set.remove freq2subset (containingSets s freq2elem)

                                // Select set
                                computeMSC <| Set.difference u freq2subset
                                           <| Set.remove freq2subset s
                                           <| Set.add freq2subset c
                            
                                // Discard set
                                computeMSC <| Set.difference u (Set.unionMany s')
                                           <| Set.difference s (Set.add freq2subset s')
                                           <| Set.union c s'
                            | None ->
                                let biggerSubset = Seq.tryFind (fun (subset: Set<'Vertex>) -> card subset >= 3) sMax
                                match biggerSubset with
                                | Some biggerSubsetVal ->
                                    let biggerSubsetVal = biggerSubset.Value

                                    // Select set
                                    computeMSC <| Set.difference u biggerSubsetVal
                                               <| Set.remove biggerSubsetVal s
                                               <| Set.add biggerSubsetVal c

                                    // Discard set
                                    computeMSC u (Set.remove biggerSubsetVal s) c
                                | None ->
                                    let containingContained = Seq.tryPick (fun maxSubset -> 
                                        let contained = Seq.tryFind (fun (subset: Set<'Vertex>) -> 
                                            subset <> maxSubset && 
                                            subset.Count = (Set.intersect subset maxSubset).Count) s
                                        if contained.IsSome then Some (maxSubset, contained.Value) else None) sMax

                                    match containingContained with
                                    | Some (containing, contained) ->
                                        // Select set
                                        computeMSC <| Set.difference u containing
                                                   <| Set.difference s (Set([containing; contained]))
                                                   <| Set.add containing c

                                        // Discard set
                                        computeMSC u (Set.remove containing s) c
                                    | None ->
                                        let maxFreqElem = Seq.maxBy (fun x -> frequency s x) u
                                        let uvSet = Seq.find (fun (subset: Set<'Vertex>) -> subset.Contains maxFreqElem) s
                                        let otherUSets = Set.remove uvSet <| containingSets s maxFreqElem
                                        let v = Seq.find (fun elem -> elem <> maxFreqElem) uvSet
                                        let otherVSets = Set.remove uvSet <| containingSets s v

                                        // Su set
                                        computeMSC <| Set.difference u uvSet
                                                   <| Set.difference s (Set.add uvSet otherUSets)
                                                   <| Set.add uvSet c

                                        // Sv set
                                        computeMSC <| Set.difference u uvSet
                                                   <| Set.difference s (Set.add uvSet otherVSets)
                                                   <| Set.add uvSet c

                                        // Discard set
                                        computeMSC u (Set.remove uvSet s) c

        computeMSC u sSrc (Set.empty)
        set !mscList

    member this.MinimalDominatingSets() = 
        let mscs = this.MSCs <| Set(graph.Vertices) <| closedNeighs()

//        printfn "Min set covers:"
//        mscs |> Seq.iter (printfn "%A")
//        printfn "[end] Min set covers"

        let mdss = 
            mscs 
            |> Seq.collect (fun cover -> 
                let vertSets = cover |> Set.map (fun subset -> 
                    graph.Vertices |> Seq.filter (fun v -> closedNeigh v = subset)
                                    |> set)

                vertSets |> Set.fold (fun (acc: Set<Set<'Vertex>>) vset -> 
                    if acc.IsEmpty then
                        Set(Set.map (fun (v: 'Vertex) -> Set.singleton v) vset)
                    else
                        acc |> Seq.collect (fun (accElem: Set<'Vertex>) -> 
                                Set.map (fun (v: 'Vertex) -> accElem.Add v) vset)
                            |> set) Set.empty)

//        printfn "Min dominating sets:"
//        mdss |> Seq.iter (printfn "%A")
//        printfn "[end] Min dominating sets"

        set mdss


    // Minimal dominating set partition is returned if it exists for a given graph
    // Otherwise some other max size domatic partition is returned
    member this.GetMaxSizePartition() = 
        let subsets s = 
                let max_bits x = 
                    let rec loop acc = if (1 <<< acc) > x then acc else loop (acc + 1)
                    loop 0

                let bit_setAt i x = ((1 <<< i) &&& x) <> 0

                let a = Set.toArray s in
                let len = Array.length a
                let as_set x =  set [for i in 0 .. (max_bits x) do 
                                        if (bit_setAt i x) && (i < len) then yield a.[i]]
        
                seq { for i in 0 .. (1 <<< len) - 1 -> as_set i }

        let sortedSubsets = 
            subsets <| set graph.Vertices
            |> Seq.toArray
            |> Array.sortBy (fun (subset: Set<'Vertex>) -> subset.Count)

        let mdss = this.MinimalDominatingSets()
        let runningDomaticNums = new System.Collections.Generic.Dictionary<Set<'Vertex>, int * List<Set<'Vertex>>>(sortedSubsets.Length)
        sortedSubsets |> Array.iter (fun subset -> runningDomaticNums.Add(subset, (0, [])))

        let allMaxWithMaxBy f (pairs: seq<_ * _>) =
            let maxVal = f <| Seq.maxBy f pairs
            maxVal, Seq.fold (fun acc elem -> if f elem = maxVal then (snd elem) :: acc else acc) [] pairs
        
        for subset in sortedSubsets do
            let curMdss = Set.filter (fun (cover: Set<'Vertex>) -> cover.Count = (Set.intersect subset cover).Count) mdss
            let newElem = 
                curMdss 
                |> Set.map (fun (domSet: Set<'Vertex>) -> (fst runningDomaticNums.[Set.difference subset domSet]) + 1, domSet)
                |> fun pairs -> if pairs.IsEmpty then (0, [])
                                else allMaxWithMaxBy fst pairs
            runningDomaticNums.[subset] <- newElem

        let partitionFound = ref false

        let rec revProc (setList: List<Set<'Vertex>>) curSet = 
            match setList with 
            | [] -> []
            | hd :: tl -> 
                let p = getMinSetPartition <| Set.difference curSet hd
                if !partitionFound then hd :: p else revProc tl curSet
        and getMinSetPartition (curSet: Set<'Vertex>) =
            if curSet.IsEmpty then partitionFound := true; []
            else
                let partSetList = snd runningDomaticNums.[curSet]
                if partSetList = [] then []
                else revProc partSetList curSet

        let rec getPartition (curSet: Set<'Vertex>) =
            if curSet.IsEmpty then []
            else
                let partSetList = snd runningDomaticNums.[curSet]
                if partSetList = [] then []
                else
                    let partSet = (snd runningDomaticNums.[curSet]).Head
                    let p = getPartition <| Set.difference curSet partSet
                    if p = [] then curSet :: p else partSet :: p

        let minSetPart = getMinSetPartition (set graph.Vertices)
        if !partitionFound then
//            printfn "Min set domatic partition: %A" <| minSetPart
            minSetPart
        else
//            printfn "Graph not divisable into minimum dominating sets partition"
            let part = getPartition (set graph.Vertices)
//            printfn "Domatic partition: %A" <| part
            part
        |> List.map (fun domSet -> Set.toSeq domSet)
        |> List.toSeq