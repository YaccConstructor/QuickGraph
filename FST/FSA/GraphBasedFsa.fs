module YC.FSA.GraphBasedFsa

open QuickGraph
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.Algorithms.Search
open QuickGraph.Algorithms
open QuickGraph.Collections
open YC.FSA.FsaApproximation
open Microsoft.FSharp.Text

[<Struct>]
type VerticesSearchString =
    val startAct: int
    val endActs: HashSet<int>
    val verticesSearchStr: HashSet<int>
    new (sa, ea, vs) = {startAct = sa; endActs = ea; verticesSearchStr = vs}   

type DfaNode<'a> = 
    { Id: int;
      Name: string;
      mutable Transitions: ('a * DfaNode<'a>) list;
      IsFinal: bool
      IsStart: bool }

type MultiMap<'a,'b> = Dictionary<'a,ResizeArray<'b>>
//let LookupMultiMap (trDict:MultiMap<_,_>) a  =
//    if trDict.ContainsKey(a) then trDict.[a] else new ResizeArray<_>(20)

let AddToMultiMap (trDict:MultiMap<_,_>) a b =
    if not <| trDict.ContainsKey(a)
    then trDict.Add(a, new ResizeArray<_>(10)) 
    trDict.[a].Add b

let setVertexRemoved (fsa:#IVertexListGraph<_,_>) startV =
        let dfs = DepthFirstSearchAlgorithm<_,_>(fsa)
        dfs.Compute(startV)
        let vertexRemoved = dfs.VertexColors |> Seq.filter(fun x -> x.Value = GraphColor.White) |> Seq.map(fun x -> x.Key)
        vertexRemoved

// TODO: consider a better representation here.
type internal NfaNodeIdSetBuilder = HashSet<int>

type internal NfaNodeIdSet(nodes: NfaNodeIdSetBuilder) = 
    // BEWARE: the next line is performance critical
    let s = nodes |> Seq.toArray |> (fun arr -> Array.sortInPlaceWith compare arr; arr) // 19

    // These are all surprisingly slower:
    //let s = nodes |> Seq.toArray |> Array.sort 
    //let s = nodes |> Seq.toArray |> Array.sortWith compare // 76
    //let s = nodes |> Seq.toArray |> (fun arr -> Array.sortInPlace arr; arr) // 76

    member x.Representation = s
    member x.Elements = s 
    member x.Fold f z = Array.fold f z s
    interface System.IComparable with 
        member x.CompareTo(y:obj) = 
            let y = (y :?> NfaNodeIdSet)
            let xr = x.Representation
            let yr = y.Representation
            let c = compare xr.Length yr.Length
            if c <> 0 then c else 
            let n = yr.Length
            let rec go i = 
                if i >= n then 0 else
                let c = compare xr.[i] yr.[i]
                if c <> 0 then c else
                go (i+1) 
            go 0

    override x.Equals(y:obj) = 
        match y with 
        | :? NfaNodeIdSet as y -> 
            let xr = x.Representation
            let yr = y.Representation
            let n = yr.Length
            xr.Length = n && 
            (let rec go i = (i < n) && xr.[i] = yr.[i] && go (i+1) 
             go 0)
        | _ -> false

    override x.GetHashCode() = hash s

    member x.IsEmpty = (s.Length = 0)
    member x.Iterate f = s |> Array.iter f

type NodeSetSet = Set<NfaNodeIdSet>

let newDfaNodeId, reset = 
    let i = ref 0 
    fun () -> let res = !i in incr i; res
    , fun () -> i := 0
   

let fsaToDot strs initState finalState filePrintPath =
    let rank s l =
        "{ rank=" + s + "; " + (l |> ResizeArray.map string |> ResizeArray.toArray |> String.concat " ") + " }\n"
    let s = 
        "digraph G {\n" 
        + "rankdir = LR\n"
        + "node [shape = circle]\n"
        + (initState |> ResizeArray.map(fun x -> sprintf "%i[style=filled, fillcolor=green]\n" x) |> ResizeArray.toArray |> String.concat "")
        + (finalState |> ResizeArray.map(fun x -> sprintf "%i[shape = doublecircle, style=filled, fillcolor=red]\n" x) |> ResizeArray.toArray |> String.concat "")
        + rank "same" initState 
        + rank "min" initState 
        + rank "same" finalState 
        + rank "max" finalState 
                          
    System.IO.File.WriteAllText(filePrintPath, s + (String.concat "" strs) + "\n}")

type SmblFSA<'br> =
    | SmblFSA of char * 'br
    | EpsFSA

[<Struct>]
type EdgeLbl<'br> =
    val Symb : SmblFSA<'br>
    new symb = {Symb = symb}

[<Class>]
type FSA<'br when 'br : equality>(initial, final, transitions) as this =
    inherit AdjacencyGraph<int,TaggedEdge<int,EdgeLbl<'br>>>()
    do
        transitions |> ResizeArray.map (fun (f,l,t) -> new TaggedEdge<_,_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore

    let printFSAtoDOT filePrintPath printSmb =
        let strs = 
            let getVal s printSmb = 
                match s with
                | SmblFSA(y, br) -> (match printSmb with Some x -> x y | None -> y.ToString()).Replace("\"","\\\"")
                | EpsFSA -> "Eps"

            this.Edges
            |> Seq.map (fun edge ->
                sprintf "%i -> %i [label=\"%s\"]; \n" edge.Source edge.Target (getVal edge.Tag.Symb printSmb))
        
        fsaToDot strs this.InitState this.FinalState filePrintPath

    static let concat (fsa1:FSA<_>) (fsa2:FSA<_>) =
        let maxVert = Seq.max fsa1.Vertices
        let fsa2Dict = new Dictionary<_, _>()
        let i = ref (maxVert + 1)
        for v in fsa2.Vertices do
            if not <| fsa2Dict.ContainsKey(v)
            then fsa2Dict.Add(v, !i)
            i := !i + 1
             
        let resFSA =  new FSA<_>() 
        resFSA.AddVerticesAndEdgeRange fsa1.Edges |> ignore
        for e in fsa2.Edges do 
            new TaggedEdge<_,_>(fsa2Dict.[e.Source], fsa2Dict.[e.Target], e.Tag) |> resFSA.AddVerticesAndEdge |> ignore
        
        resFSA.InitState <- fsa1.InitState
        for v in fsa2.FinalState do
            resFSA.FinalState.Add(fsa2Dict.[v])  
        
        for v in fsa1.FinalState do
            new TaggedEdge<_,_>(v, fsa2Dict.[fsa2.InitState.[0]], new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore
                                    
        resFSA

    static let union (fsa1:FSA<_>) (fsa2:FSA<_>) =
        let maxVert = Seq.max fsa1.Vertices
        let fst2Dict = new Dictionary<_, _>()
        let i = ref (maxVert + 1)
        for v in fsa2.Vertices do
            if not <| fst2Dict.ContainsKey(v)
            then fst2Dict.Add(v, !i)
            i := !i + 1
             
        let resFSA =  new FSA<_>() 
        resFSA.AddVerticesAndEdgeRange fsa1.Edges |> ignore
        for e in fsa2.Edges do 
            new TaggedEdge<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFSA.AddVerticesAndEdge |> ignore
        
        resFSA.InitState.Add(!i)
        resFSA.FinalState.Add(!i + 1)

        for v in fsa1.InitState do
            new TaggedEdge<_,_>(!i, v, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore

        for v in fsa2.InitState do
            new TaggedEdge<_,_>(!i, fst2Dict.[v], new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore  
                       
        for v in fsa1.FinalState do
            new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore

        for v in fsa2.FinalState do
            new TaggedEdge<_,_>(fst2Dict.[v], !i + 1, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore
              
        resFSA
  
    let nfaToDfa (inGraph: FSA<'br>):FSA<'br> = 
        reset ()
        let numNfaNodes = inGraph.VertexCount
        let rec EClosure1 (acc:NfaNodeIdSetBuilder) n = 
            if not (acc.Contains n) then 
                acc.Add n |> ignore
                let epsTransitions = (inGraph.OutEdges n) |> List.ofSeq |> List.filter (fun x -> x.Tag.Symb = EpsFSA (*Option.isNone x.Tag*)) |> List.map (fun e -> e.Target)
                match epsTransitions with 
                | [] -> () // this Clause is an optimization - the list is normally empty
                | tr -> 
                    //printfn "n.Id = %A, #Epsilon = %d" n.Id tr.Length
                    tr |> List.iter (EClosure1 acc) 

        let EClosure (moves:ResizeArray<_>) = 
            let acc = new NfaNodeIdSetBuilder(HashIdentity.Structural)
            for i in moves do
                EClosure1 acc i
            new NfaNodeIdSet(acc)

        // Compute all the immediate one-step moves for a set of NFA states, as a dictionary
        // mapping inputs to destination lists
        let ComputeMoves (nset:NfaNodeIdSet) = 
            let moves = new MultiMap<_,_>()
            nset.Iterate(fun nodeId -> 
                for e in inGraph.OutEdges nodeId do
                    if e.Tag.Symb <> EpsFSA then AddToMultiMap moves e.Tag e.Target)
                        //match dests with 
                        //| [] -> ()  // this Clause is an optimization - the list is normally empty
                        //| tr -> tr |> List.iter(fun dest -> AddToMultiMap moves e.Tag dest.Id))
            moves

        let acc = new NfaNodeIdSetBuilder(HashIdentity.Structural)
        EClosure1 acc inGraph.InitState.[0]
        let nfaSet0 = new NfaNodeIdSet(acc)

        let dfaNodes = ref (Map.empty<NfaNodeIdSet,DfaNode<_>>)

        let GetDfaNode nfaSet = 
            if (!dfaNodes).ContainsKey(nfaSet) then 
                (!dfaNodes).[nfaSet]
            else 
                let dfaNode =
                    { Id= newDfaNodeId()
                      Name = nfaSet.Fold (fun s nid -> string nid + "-" + s) ""
                      Transitions=[]
                      IsFinal= nfaSet.Fold (fun s nid -> s || ResizeArray.exists ((=) nid) inGraph.FinalState) false
                      IsStart = nfaSet.Fold (fun s nid -> s || inGraph.InitState.[0] = nid) false
                                 }
                //Printf.printfn "id = %d" dfaNode.Id;

                dfaNodes := (!dfaNodes).Add(nfaSet,dfaNode)
                dfaNode
            
        let workList = ref [nfaSet0]
        let doneSet = ref Set.empty

        //let count = ref 0 
        let rec Loop () = 
            match !workList with 
            | [] -> ()
            | nfaSet ::t -> 
                workList := t
                if (!doneSet).Contains(nfaSet) then 
                    Loop () 
                else
                    let moves = ComputeMoves nfaSet
                    for (KeyValue(inp,movesForInput)) in moves do
                        assert (inp.Symb <> EpsFSA)
                        let moveSet = EClosure movesForInput
                        if not moveSet.IsEmpty then 
                            //incr count
                            let dfaNode = GetDfaNode nfaSet
                            dfaNode.Transitions <- (inp, GetDfaNode moveSet) :: dfaNode.Transitions
                            (* Printf.printf "%d (%s) : %s --> %d (%s)\n" dfaNode.Id dfaNode.Name (match inp with EncodeChar c -> String.make 1 c | LEof -> "eof") moveSetDfaNode.Id moveSetDfaNode.Name;*)
                            workList := moveSet :: !workList

                    doneSet := (!doneSet).Add(nfaSet)


                    Loop()
        Loop();
        //Printf.printfn "count = %d" !count;
        let ruleStartNode = GetDfaNode nfaSet0
        let ruleNodes = 
            (!dfaNodes) 
            |> Seq.map (fun kvp -> kvp.Value) 
            |> Seq.toList
            |> List.sortBy (fun s -> s.Id)
       
        let graph = new FSA<_>()

        ruleNodes 
        |> List.collect (fun n -> n.Transitions |> List.map (fun (l,t) -> new TaggedEdge<_,_>(n.Id, t.Id, new EdgeLbl<_>(l.Symb))))
        |> graph.AddVerticesAndEdgeRange
        |> ignore

        graph.InitState <- ResizeArray.singleton (ruleNodes |> List.find (fun x -> x.IsStart)).Id
        for el in ruleNodes do
            if el.IsFinal then graph.FinalState.Add(el.Id)
               
        graph
    
    static let complementation (fsa:FSA<'br>) =  
        let (dfa:FSA<'br>) = fsa.NfaToDfa
        let resFSA = new FSA<'br>()
        resFSA.InitState <- dfa.InitState
        
        for st in dfa.Vertices do
            if not (ResizeArray.exists ((=) st) dfa.FinalState) 
            then resFSA.FinalState.Add(st)
        
        resFSA.AddVerticesAndEdgeRange(dfa.Edges) |> ignore
        resFSA
    
    static let intersection (dfa1:FSA<_>) (dfa2:FSA<_>) =
        //let dfa1 = fsa1.NfaToDfa
        //let dfa2 = fsa2.NfaToDfa
        
        let resFSA = new FSA<_>()
        let fsaDict = new Dictionary<_,_>()
        let i = ref 0
        for v1 in dfa1.Vertices do
            for v2 in dfa2.Vertices do
                fsaDict.Add((v1, v2), !i)
                i := !i + 1
        
        let isEqual s1 s2 =               
            match s1,s2 with
            | SmblFSA(x, _), SmblFSA(y, _) -> x = y
            | x,y -> false //failwithf "Cannot be compared %A and %A" x y

        for edge1 in dfa1.Edges do
            for edge2 in dfa2.Edges do
                if isEqual edge1.Tag.Symb edge2.Tag.Symb
                then
                    new TaggedEdge<_,_>(fsaDict.[(edge1.Source, edge2.Source)], fsaDict.[(edge1.Target, edge2.Target)], new EdgeLbl<_>(edge1.Tag.Symb))
                    |> resFSA.AddVerticesAndEdge  |> ignore 
        
        //TODO: one initial state??
        for v1 in dfa1.InitState do
            for v2 in dfa2.InitState do
                resFSA.InitState.Add(fsaDict.[(v1, v2)])

        for v1 in dfa1.FinalState do
            for v2 in dfa2.FinalState do
                resFSA.FinalState.Add(fsaDict.[(v1, v2)])
        
        //delete unreachable state
        for v in resFSA.FinalState do
            new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore

        let vRemove1 = setVertexRemoved resFSA resFSA.InitState.[0]
        for v in vRemove1 do
            resFSA.RemoveVertex(v) |> ignore
            resFSA.FinalState.Remove(v) |> ignore

        let FSAtmp = new FSA<_>()
        for edge in resFSA.Edges do
            new TaggedEdge<_,_>(edge.Target, edge.Source, edge.Tag) |>  FSAtmp.AddVerticesAndEdge |> ignore

        let vRemove2 = setVertexRemoved FSAtmp (!i + 1)

        for v in vRemove2 do
            resFSA.RemoveVertex(v) |> ignore
            resFSA.FinalState.Remove(v) |> ignore

        resFSA.RemoveVertex(!i + 1) |> ignore
        resFSA

    static let replace (fsa1_in:FSA<_>) (fsa2_in:FSA<_>) (fsa3_in:FSA<_>) = 
        let fsa1 = fsa1_in.NfaToDfa
        fsa1.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa1.dot"
        let fsa2 = fsa2_in.NfaToDfa
        let fsa3 = fsa3_in.NfaToDfa
        //#1 = ~     #2 = ^

        //Step 1. Construct fsa1_tmp from fsa1
        let fsa1_tmp = new FSA<_>()
        fsa1_tmp.InitState <- fsa1.InitState
        fsa1_tmp.FinalState <- fsa1.FinalState
        fsa1_tmp.AddVerticesAndEdgeRange(fsa1.Edges) |> ignore
        
        let maxVert = Seq.max fsa1.Vertices

        let fsa1Dict = new Dictionary<_,_>()
        let i = ref (maxVert + 1)
        for v in fsa1.Vertices do
            fsa1Dict.Add(v, !i)
            i := !i + 1
                        
        for edge in fsa1.Edges do
            new TaggedEdge<_,_>(fsa1Dict.[edge.Source], fsa1Dict.[edge.Target], edge.Tag) |> fsa1_tmp.AddVerticesAndEdge |> ignore            
        
        for v in fsa1.Vertices do
            new TaggedEdge<_,_>(v, fsa1Dict.[v], new EdgeLbl<_>(SmblFSA('~', Unchecked.defaultof<'br>))) |> fsa1_tmp.AddVerticesAndEdge |> ignore
            new TaggedEdge<_,_>(fsa1Dict.[v], v, new EdgeLbl<_>(SmblFSA('^', Unchecked.defaultof<'br>))) |> fsa1_tmp.AddVerticesAndEdge |> ignore
 
        fsa1_tmp.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa1_tmp.dot"

        //Step 2. Construct fsa2_tmp from fsa2       
        // construct dfa* above alphabet of fsa1 and fsa2 ??
        let alphabetFSAs = new HashSet<_>() //alphabets of fsa1 and fsa2 
        let getChar x = 
            match x with
            | SmblFSA(y, _) -> y
            | _ -> failwith "Unexpected symb in alphabet of FSA!"

        for edge in fsa1.Edges do
            alphabetFSAs.Add(getChar edge.Tag.Symb) |> ignore

        for edge in fsa2.Edges do
            alphabetFSAs.Add(getChar edge.Tag.Symb) |> ignore

        let fsaAllStrings = new FSA<_>()        
        fsaAllStrings.InitState <- ResizeArray.singleton 0
        fsaAllStrings.FinalState <- fsaAllStrings.InitState

        for ch in alphabetFSAs do
            new TaggedEdge<_,_>(0, 0, new EdgeLbl<_>(SmblFSA(ch, Unchecked.defaultof<'br>))) |> fsaAllStrings.AddVerticesAndEdge |> ignore    

        fsaAllStrings.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa_star.dot"

        //construct complementation to fsa2
        //let (fsa_tmp_1:FSA<_>) = fsaAllStrings.Concat(fsa2)
        let fsa2DictConcat1 = new Dictionary<_, _>()
        let iConcat1 = ref 1
        for v in fsa2.Vertices do
            if not <| fsa2DictConcat1.ContainsKey(v)
            then fsa2DictConcat1.Add(v, !iConcat1)
            iConcat1 := !iConcat1 + 1
             
        let fsa_tmp_1 =  new FSA<_>() 
        //fsa_tmp_1.AddVerticesAndEdgeRange fsaAllStrings.Edges |> ignore       
        for e in fsa2.Edges do 
            new TaggedEdge<_,_>(fsa2DictConcat1.[e.Source], fsa2DictConcat1.[e.Target], e.Tag) |> fsa_tmp_1.AddVerticesAndEdge |> ignore
        
        fsa_tmp_1.InitState <- fsaAllStrings.InitState

        for v in fsa2.FinalState do
            fsa_tmp_1.FinalState.Add(fsa2DictConcat1.[v])  
        
        for v in fsaAllStrings.FinalState do
            for edge in fsa_tmp_1.OutEdges(fsa2DictConcat1.[fsa2.InitState.[0]]) do
                new TaggedEdge<_,_>(v, edge.Target, edge.Tag) |> fsa_tmp_1.AddVerticesAndEdge  |> ignore

        fsa_tmp_1.RemoveVertex(fsa2DictConcat1.[fsa2.InitState.[0]]) |> ignore

        //let fsa_tmp_2 = fsa_tmp_1.Concat(fsaAllStrings)
        for v in fsa_tmp_1.FinalState do
            for edge in fsaAllStrings.Edges do
                new TaggedEdge<_,_>(v, v, edge.Tag) |> fsa_tmp_1.AddVerticesAndEdge  |> ignore 
      
//        for edge in fsa_tmp_1.OutEdges(fsa_tmp_1.InitState.[0]) do
//            if not (edge.Source = edge.Target)
//            then fsa_tmp_1.RemoveEdge(new TaggedEdge<_,_>(fsa_tmp_1.InitState.[0], fsa_tmp_1.InitState.[0], new EdgeLbl<_>(edge.Tag.Symb))) |> ignore
    
        let isFinish v = 
            fsa2.FinalState.Exists (fun x -> x = v)       
        
        let currDict v =
            let deleteCh = new HashSet<_>()
            for edge in fsa_tmp_1.OutEdges(v) do
                deleteCh.Add (getChar edge.Tag.Symb) |> ignore
            let curChDict = new HashSet<_>()
            for ch in alphabetFSAs do
                if not (deleteCh.Contains(ch))
                then curChDict.Add(ch) |> ignore
            curChDict

        let fsa_tmp_2 = new FSA<_>()
        fsa_tmp_2.InitState <- fsa_tmp_1.InitState
        fsa_tmp_2.FinalState <- fsa_tmp_1.FinalState
        fsa_tmp_2.AddVerticesAndEdgeRange fsa_tmp_1.Edges |> ignore

        for edge in fsa_tmp_1.Edges do            
            if not(isFinish edge.Target) || edge.Source = fsa_tmp_1.InitState.[0]
            then 
                for ch in currDict edge.Source do
                    new TaggedEdge<_,_>(edge.Source, edge.Source, new EdgeLbl<_>(SmblFSA(ch, Unchecked.defaultof<'br>))) |> fsa_tmp_2.AddVerticesAndEdge  |> ignore 
                
        let (fsa_compl:FSA<_>) = fsa_tmp_2.Complementation
        //in paper fsa_compl MINIMIZATION
        fsa_compl.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa_compl.dot"

        //construct fsa2_tmp
        let fsa2_tmp = new FSA<_>()
        fsa2_tmp.InitState <- fsa_compl.InitState
        fsa2_tmp.FinalState <- fsa_compl.FinalState

        fsa2_tmp.AddVerticesAndEdgeRange(fsa_compl.Edges) |> ignore
        //перенумеровать вершины fsa2
        let maxVertFsa2Step2 = Seq.max fsa_compl.Vertices

        let fsa2DictStep2 = new Dictionary<_,_>()
        let iStep2 = ref (maxVertFsa2Step2 + 1)
        for v in fsa2.Vertices do
            fsa2DictStep2.Add(v, !iStep2)
            iStep2 := !iStep2 + 1

        for edge in fsa2.Edges do
            new TaggedEdge<_,_>(fsa2DictStep2.[edge.Source], fsa2DictStep2.[edge.Target], edge.Tag) |> fsa2_tmp.AddVerticesAndEdge |> ignore 

        for v in fsa_compl.FinalState do
            new TaggedEdge<_,_>(v, fsa2DictStep2.[fsa2.InitState.[0]], new EdgeLbl<_>(SmblFSA('~', Unchecked.defaultof<'br>))) |> fsa2_tmp.AddVerticesAndEdge |> ignore
            
        for v in fsa2.FinalState do
            new TaggedEdge<_,_>(fsa2DictStep2.[v], fsa_compl.InitState.[0], new EdgeLbl<_>(SmblFSA('^', Unchecked.defaultof<'br>))) |> fsa2_tmp.AddVerticesAndEdge |> ignore
        
        fsa2_tmp.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa2_tmp.dot"  
                      
        //Step 3. Generate fsa_tmp as intersection of fsa1_tmp and fsa2_tmp
        let (fsa_tmp:FSA<_>) = FSA<_>.Intersection(fsa1_tmp, fsa2_tmp)

        fsa_tmp.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa_tmp.dot"
        
        //===================================================================
        let resFSA = 
            if fsa_tmp.EdgeCount > 0 //result of intersection is not empty?
            then 
                //Step 4. Construct resFSA from fsa_tmp, delete #1, #2
            
                //search 'reachable' state which is source vertex for edge with tag #1
                let reach = new HashSet<_>()

                for edge in fsa_tmp.Edges do
                    if getChar edge.Tag.Symb = '~'
                    then reach.Add(edge.Source) |> ignore
        
                let visited = new HashSet<_>()

                let bfs vertex (graphFsa: FSA<_>) =
                    let targetAct = new HashSet<_>()
                    let verticesSearchStr = new HashSet<_>()            
                    let queueV = new Queue<_>()
                    queueV.Enqueue(vertex)               

                    while queueV.Count > 0 do
                        let topV = queueV.Dequeue()
                        if not <| visited.Contains(topV) 
                        then
                            visited.Add(topV) |> ignore
                            for v in graphFsa.OutEdges(topV) do
                                if getChar v.Tag.Symb = '^' 
                                then 
                                    targetAct.Add v.Target |> ignore 
                                    verticesSearchStr.Add v.Source |> ignore                             
                                else 
                                    queueV.Enqueue v.Target
                                    verticesSearchStr.Add v.Target |> ignore
                                    if not (v.Source = vertex) then verticesSearchStr.Add v.Source |> ignore
                                                                                       
                    visited.Clear()                                   
                    (targetAct, verticesSearchStr)
    
                let findVert = new ResizeArray<_>()  

                for vReach in reach do            
                    let searchStr = bfs vReach fsa_tmp
                    //printf "%A %A %A" vReach (fst searchStr) (snd searchStr)
                    let vertSearchStr = new VerticesSearchString(vReach, fst searchStr, snd searchStr)
                    findVert.Add vertSearchStr
                   
                //перенумеровывать вершины FSA3 при каждом добавлении их в FSA1!! FUCK\
                let maxVertCurr = Seq.max fsa_tmp.Vertices
                let iCurr = ref (maxVertCurr + 1)

                let changeNumerationFSA3 = 
                    let fsa3Dict = new Dictionary<_,_>()
                    for v in fsa3.Vertices do
                        fsa3Dict.Add(v, !iCurr)
                        iCurr := !iCurr + 1
                    fsa3Dict

                for vReach in reach do
                    let mutable fsa3DictCurr = changeNumerationFSA3
                    printf "%A " fsa3DictCurr
                    new TaggedEdge<_,_>(vReach, fsa3DictCurr.[fsa3.InitState.[0]], new EdgeLbl<_>(EpsFSA)) |> fsa_tmp.AddVerticesAndEdge |> ignore
                    let findInfoForVReach = findVert.Find(fun x -> x.startAct = vReach)
                    for finalVReach in findInfoForVReach.endActs do
                        for finalVFSA3 in fsa3.FinalState do
                            new TaggedEdge<_,_>(fsa3DictCurr.[finalVFSA3], finalVReach, new EdgeLbl<_>(EpsFSA)) |> fsa_tmp.AddVerticesAndEdge |> ignore
                        for edgeFsa3 in fsa3.Edges do
                            new TaggedEdge<_,_>(fsa3DictCurr.[edgeFsa3.Source], fsa3DictCurr.[edgeFsa3.Target], new EdgeLbl<_>(edgeFsa3.Tag.Symb)) |> fsa_tmp.AddVerticesAndEdge |> ignore
                        fsa3DictCurr <- changeNumerationFSA3
                    //delete verts from fsa_tmp
                    for vUnused in findInfoForVReach.verticesSearchStr do
                        fsa_tmp.RemoveVertex(vUnused) |> ignore

                    fsa_tmp.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa_tmp_afterReplace.dot"
                    
                    let res = fsa_tmp.NfaToDfa
                    res.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa_tmp_afterReplace_woEPS.dot" 

                fsa_tmp.NfaToDfa
            else fsa1

        resFSA
         
    new () = 
        FSA<_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<_>())
    
    member this.NfaToDfa = nfaToDfa this
    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.PrintToDOT(filePath, ?printSmb) = printFSAtoDOT filePath printSmb
    member this.Concat fsa2 = concat this fsa2
    static member Concat(fsa1, fsa2) = concat fsa1 fsa2
    member this.Union fsa2 = union this fsa2
    static member Union(fsa1, fsa2) = union fsa1 fsa2
    ///fsa1 -- original strings; fsa2 -- match strings; fsa3 -- replacement strings
    static member Replace(fsa1, fsa2, fsa3) = replace fsa1 fsa2 fsa3
    member this.Complementation = complementation this
    static member Intersection(fsa1, fsa2) = intersection fsa1 fsa2
    static member ApprToFSA(appr:Appr<_>) = 
        let resFSA = new FSA<_>()
        resFSA.InitState <- appr.InitState
        resFSA.FinalState <- appr.FinalState
        
        let counter = appr.Vertices |> Seq.max |> ref

        let splitEdge (edg:TaggedEdge<int, EdgeLblAppr<'br>>) str br =
            let start = edg.Source
            let _end = edg.Target
            let pos = ref 0

            match str with
            | Some("") -> failwith "We don't support eps-transition!" //[|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_>(Eps))|]
            | None -> [|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_>(EpsFSA))|]
            | Some(s) ->
                let l = s.Length
                let ss = s.ToCharArray()
                Array.init l
                    (fun i ->
                        match i with
                        | 0 when (l = 1)     -> new TaggedEdge<_,_>(start, _end, new EdgeLbl<_>(SmblFSA (ss.[i], new Position<'br>(0,1,br))))
                        | 0                  -> new TaggedEdge<_,_>(start, (incr counter; !counter), new EdgeLbl<_>(SmblFSA (ss.[i], new Position<_>(!pos,(incr pos; !pos),br))))
                        | i when (i = l - 1) -> new TaggedEdge<_,_>(!counter, _end, new EdgeLbl<_>(SmblFSA (ss.[i], new Position<_>(!pos,(incr pos; !pos),br))))
                        | i                  -> new TaggedEdge<_,_>(!counter, (incr counter; !counter), new EdgeLbl<_>(SmblFSA (ss.[i], new Position<_>(!pos,(incr pos; !pos),br))))
                    )

        let rec go (approximation:Appr<_>) =
            for edge in approximation.Edges do
                match edge.Tag with
                | Smb (str, br) ->
                    let x = splitEdge edge (Some str) br
                    x |> resFSA.AddVerticesAndEdgeRange
                    |> ignore
        go appr

        resFSA
   