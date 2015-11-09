module YC.FSA.GraphBasedFsa

open QuickGraph
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.Algorithms.Search
open QuickGraph.Algorithms
open QuickGraph.Collections
open Microsoft.FSharp.Text
open HelperTypes

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
            (let rec go i = (i >= n) || xr.[i] = yr.[i] && go (i+1) 
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

type Symb<'a> =
    | Smbl of 'a
    | Eps

type EdgeFSA<'a>(s,e,t)=
    inherit TaggedEdge<int, Symb<'a>>(s,e,t)

type FsaParams<'a, 'b when 'b: equality> = {
    Alphabet: HashSet<'a>
    NewSymbol: 'a -> Symb<'b>
    GetChar: Symb<'b> -> 'a
    SymbolsAreEqual: 'b -> 'b -> bool
    SeparatorSmbl1: 'a
    SeparatorSmbl2: 'a }

[<Class>]
type FSA<'a when 'a : equality>(initial, final, transitions) as this =
    inherit AdjacencyGraph<int, EdgeFSA<'a>>()
    do
        transitions |> ResizeArray.map (fun (f,l,t) -> new EdgeFSA<_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore
        this.AddVertexRange(initial) |> ignore
        this.AddVertexRange(final) |> ignore

    let printFSAtoDOT filePrintPath printSmb =
        let strs = 
            let getVal s printSmb = 
                match s with
                | Smbl y -> (match printSmb with Some x -> x y | None -> y.ToString()).Replace("\"","\\\"")
                | Eps -> "Eps"

            this.Edges
            |> Seq.map (fun edge ->
                sprintf "%i -> %i [label=\"%s\"]; \n" edge.Source edge.Target (getVal edge.Tag printSmb))
        
        fsaToDot strs this.InitState this.FinalState filePrintPath
    
    ///for FSAs
    static let concat (fsa1:FSA<_>) (fsa2:FSA<_>) =
        if not (fsa1.IsEmpty || fsa2.IsEmpty) 
        then 
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
                new EdgeFSA<_>(fsa2Dict.[e.Source], fsa2Dict.[e.Target], e.Tag) |> resFSA.AddVerticesAndEdge |> ignore
        
            resFSA.InitState <- fsa1.InitState
            for v in fsa2.FinalState do
                resFSA.FinalState.Add(fsa2Dict.[v])  
        
            for v in fsa1.FinalState do
                new EdgeFSA<_>(v, fsa2Dict.[fsa2.InitState.[0]], Eps) |> resFSA.AddVerticesAndEdge  |> ignore
                                    
            resFSA
        else
            if fsa1.IsEmpty
            then fsa2 // not important what fsa2
            else fsa1

    ///for FSAs
    static let union (fsa1:FSA<_>) (fsa2:FSA<_>) =
        if not (fsa1.IsEmpty || fsa2.IsEmpty)
        then
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
                new EdgeFSA<_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFSA.AddVerticesAndEdge |> ignore
        
            resFSA.InitState.Add(!i)
            resFSA.FinalState.Add(!i + 1)

            for v in fsa1.InitState do
                new EdgeFSA<_>(!i, v, Eps) |> resFSA.AddVerticesAndEdge  |> ignore

            for v in fsa2.InitState do
                new EdgeFSA<_>(!i, fst2Dict.[v], Eps) |> resFSA.AddVerticesAndEdge  |> ignore  
                       
            for v in fsa1.FinalState do
                new EdgeFSA<_>(v, !i + 1, Eps) |> resFSA.AddVerticesAndEdge  |> ignore

            for v in fsa2.FinalState do
                new EdgeFSA<_>(fst2Dict.[v], !i + 1, Eps) |> resFSA.AddVerticesAndEdge  |> ignore
              
            resFSA
        else 
            if fsa1.IsEmpty
            then fsa2 // not important what fsa2
            else fsa1
  
    ///for FSA
    let nfaToDfa (inGraph: FSA<'a>):FSA<'a> =
        if not(inGraph.IsEmpty) then 
            reset ()
            let numNfaNodes = inGraph.VertexCount
            let rec EClosure1 (acc:NfaNodeIdSetBuilder) n = 
                if not (acc.Contains n) then 
                    acc.Add n |> ignore
                    let epsTransitions = (inGraph.OutEdges n) |> List.ofSeq |> List.filter (fun x -> x.Tag = Eps (*Option.isNone x.Tag*)) |> List.map (fun e -> e.Target)
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
                        if e.Tag <> Eps then AddToMultiMap moves e.Tag e.Target)
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
                            assert (inp <> Eps)
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
            |> List.collect (fun n -> n.Transitions |> List.map (fun (l,t) -> new EdgeFSA<_>(n.Id, t.Id, l)))
            |> graph.AddVerticesAndEdgeRange
            |> ignore

            graph.InitState <- ResizeArray.singleton (ruleNodes |> List.find (fun x -> x.IsStart)).Id
            graph.AddVertex((ruleNodes |> List.find (fun x -> x.IsStart)).Id) |> ignore

            for el in ruleNodes do
                if el.IsFinal then 
                    graph.FinalState.Add(el.Id)
                    graph.AddVertex(el.Id) |> ignore
               
            graph
        else inGraph

    ///not path from start state to any final state 
    static let isEmptyFSA (fsa:FSA<'a>) =
        if fsa.EdgeCount > 0 then
            let vRemove = setVertexRemoved fsa fsa.InitState.[0]
            let isRemove v = Seq.exists ((=) v) vRemove
            ResizeArray.forall (isRemove) fsa.FinalState
        else
            if (fsa.FinalState.Count = 1 && fsa.InitState.[0] = fsa.FinalState.[0])
            then false
            else true     
    
    ///for FSA 
    static let complementation (fsa:FSA<'a>) (alphabet:HashSet<_>) newSmb getChar =
        if not (fsa.IsEmpty) then 
            let (dfa:FSA<'a>) = fsa.NfaToDfa
            let maxV = Seq.max dfa.Vertices
            let resFSA = new FSA<_>()
            resFSA.InitState <- dfa.InitState
            resFSA.AddVertexRange(dfa.InitState) |> ignore
            resFSA.AddVertex(maxV + 1) |> ignore //sink-state
                                                                 
            let currDict v =
                let deleteCh = new HashSet<_>()
                for edge in dfa.OutEdges(v) do
                    deleteCh.Add (getChar edge.Tag) |> ignore
                let curChDict = new HashSet<_>()
                for ch in alphabet do
                    if not (deleteCh.Contains(ch))
                    then curChDict.Add(ch) |> ignore
                curChDict

            resFSA.AddVerticesAndEdgeRange dfa.Edges |> ignore

            for v in dfa.Vertices do
                for ch in currDict v do
                    new EdgeFSA<_>(v, maxV + 1, newSmb ch) |> resFSA.AddVerticesAndEdge  |> ignore

            for ch in alphabet do
                new EdgeFSA<_>(maxV + 1, maxV + 1, newSmb ch) |> resFSA.AddVerticesAndEdge  |> ignore
        
            for st in resFSA.Vertices do
                if not (ResizeArray.exists ((=) st) dfa.FinalState) 
                then resFSA.FinalState.Add(st)
            resFSA

        else 
            let resFSA = new FSA<_>()
            resFSA.InitState.Add(0) 
            resFSA.FinalState.Add(0) 

            for ch in alphabet do
                new EdgeFSA<_>(0, 0, newSmb ch) |> resFSA.AddVerticesAndEdge  |> ignore
            resFSA

 
    static let complementationForReplace (fsa:FSA<'a>) (alphabet:HashSet<_>) newSmb getChar =
        if not (fsa.IsEmpty) then 
            let (dfa:FSA<'a>) = fsa.NfaToDfa
            let resFSA = new FSA<_>()
            resFSA.InitState <- dfa.InitState
                                                                  
            let currDict v =
                let deleteCh = new HashSet<_>()
                for edge in dfa.OutEdges(v) do
                    deleteCh.Add (getChar edge.Tag) |> ignore
                let curChDict = new HashSet<_>()
                for ch in alphabet do
                    if not (deleteCh.Contains(ch))
                    then curChDict.Add(ch) |> ignore
                curChDict

            resFSA.AddVerticesAndEdgeRange dfa.Edges |> ignore

            for v in dfa.Vertices do
                for ch in currDict v do
                    new EdgeFSA<_>(v, v, newSmb ch) |> resFSA.AddVerticesAndEdge  |> ignore
        
            for st in resFSA.Vertices do
                if not (ResizeArray.exists ((=) st) dfa.FinalState) 
                then resFSA.FinalState.Add(st)
        
            resFSA

        else 
            let resFSA = new FSA<_>()
            resFSA.InitState.Add(0) 
            resFSA.FinalState.Add(0) 

            for ch in alphabet do
                new EdgeFSA<_>(0, 0, newSmb ch) |> resFSA.AddVerticesAndEdge  |> ignore
            resFSA

    static let removeExtraPaths (fsa:FSA<'a>):FSA<'a> =
        if fsa.EdgeCount > 0 && not(fsa.IsEmpty) then
            let maxV = Seq.max fsa.Vertices                     
            for v in fsa.FinalState do
                new EdgeFSA<_>(v, maxV + 1, Eps) |> fsa.AddVerticesAndEdge  |> ignore

            let vRemove1 = setVertexRemoved fsa fsa.InitState.[0]

            let FSAtmp = new FSA<_>()
            for edge in fsa.Edges do
                new EdgeFSA<_>(edge.Target, edge.Source, edge.Tag) |>  FSAtmp.AddVerticesAndEdge |> ignore
            fsa.Vertices |> Seq.iter (fun v -> FSAtmp.AddVertex v |> ignore)

            let vRemove2 = setVertexRemoved FSAtmp (maxV + 1)

            for v in vRemove1 do
                fsa.RemoveVertex(v) |> ignore
                fsa.FinalState.Remove(v) |> ignore

            for v in vRemove2 do
                fsa.RemoveVertex(v) |> ignore
                fsa.InitState.Remove(v) |> ignore

            fsa.RemoveVertex(maxV + 1) |> ignore
            fsa
        else
            fsa

    ///for DFAs
    static let intersection (dfa1:FSA<_>) (dfa2:FSA<_>) equalSmbl =
        if not (dfa1.IsEmpty || dfa2.IsEmpty) then 
            let resFSA = new FSA<_>()
            let fsaDict = new Dictionary<_,_>()
            let i = ref 0
            for v1 in dfa1.Vertices do
                for v2 in dfa2.Vertices do
                    fsaDict.Add((v1, v2), !i)
                    i := !i + 1
        
            let isEqual s1 s2 =               
                match s1,s2 with
                | Smbl x, Smbl y -> equalSmbl x y
                | x, y -> false

            for edge1 in dfa1.Edges do
                for edge2 in dfa2.Edges do
                    if isEqual edge1.Tag edge2.Tag
                    then
                        new EdgeFSA<_>(fsaDict.[(edge1.Source, edge2.Source)], fsaDict.[(edge1.Target, edge2.Target)], edge1.Tag)
                        |> resFSA.AddVerticesAndEdge  |> ignore 
        
            for v1 in dfa1.InitState do
                for v2 in dfa2.InitState do
                    resFSA.AddVertex fsaDict.[(v1, v2)] |> ignore
                    resFSA.InitState.Add(fsaDict.[(v1, v2)])

            for v1 in dfa1.FinalState do
                for v2 in dfa2.FinalState do
                    resFSA.AddVertex fsaDict.[(v1, v2)] |> ignore
                    resFSA.FinalState.Add(fsaDict.[(v1, v2)])
        
            removeExtraPaths resFSA
        else 
            if dfa1.IsEmpty then dfa1
            else dfa2

    ///for FSAs
    ///TODO: handle of FSA_2 which accept only empty string -> return FSA_1 which after every transition insert FSA_3
    static let replace (fsa1_in:FSA<_>) (fsa2_in:FSA<_>) (fsa3_in:FSA<_>) smb1 smb2 getChar newSmb equalSmbl = 
        if (fsa1_in.IsEmpty || fsa2_in.IsEmpty || fsa3_in.IsEmpty)
        then fsa1_in
        else
            let fsa1 = (removeExtraPaths fsa1_in).NfaToDfa
            let fsa2 = (removeExtraPaths fsa2_in).NfaToDfa
            let fsa3 = (removeExtraPaths fsa3_in).NfaToDfa
            //#1 = ~ smb1     #2 = ^ smb2
            
            if fsa1.EdgeCount = 0 && fsa1.FinalState.Count = 1 && fsa1.FinalState.[0] = fsa1.InitState.[0] //FSA1 accept only empty string
            then 
                if ResizeArray.exists ((=) fsa2.InitState.[0]) fsa2.FinalState   //FSA2 accept empty string
                then fsa3_in
                else fsa1_in
            else 
                //Step 1. Construct fsa1_tmp from fsa1
                let fsa1_tmp = new FSA<_>()
                fsa1_tmp.InitState <- fsa1.InitState
                fsa1_tmp.FinalState <- fsa1.FinalState
                fsa1_tmp.AddVertexRange(fsa1.FinalState) |> ignore
                fsa1_tmp.AddVertexRange(fsa1.InitState) |> ignore
                fsa1_tmp.AddVerticesAndEdgeRange(fsa1.Edges) |> ignore
        
                let maxVert = Seq.max fsa1.Vertices

                let fsa1Dict = new Dictionary<_,_>()
                let i = ref (maxVert + 1)
                for v in fsa1.Vertices do
                    fsa1Dict.Add(v, !i)
                    i := !i + 1
                        
                for edge in fsa1.Edges do
                    new EdgeFSA<_>(fsa1Dict.[edge.Source], fsa1Dict.[edge.Target], edge.Tag) |> fsa1_tmp.AddVerticesAndEdge |> ignore            
        
                for v in fsa1.Vertices do
                    new EdgeFSA<_>(v, fsa1Dict.[v], newSmb smb1) |> fsa1_tmp.AddVerticesAndEdge |> ignore
                    new EdgeFSA<_>(fsa1Dict.[v], v, newSmb smb2) |> fsa1_tmp.AddVerticesAndEdge |> ignore

                //Step 2. Construct fsa2_tmp from fsa2       
                let alphabetFSAs = new HashSet<_>() //alphabets of fsa1 and fsa2

                for edge in fsa1.Edges do
                    alphabetFSAs.Add(getChar edge.Tag) |> ignore

                for edge in fsa2.Edges do
                    alphabetFSAs.Add(getChar edge.Tag) |> ignore
                
                let (fsa_compl:FSA<_>) = complementationForReplace fsa2 alphabetFSAs newSmb getChar
                //fsa_compl.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa_compl.dot"
                //construct fsa2_tmp
                let fsa2_tmp = new FSA<_>()
                fsa2_tmp.InitState <- fsa_compl.InitState
                fsa2_tmp.FinalState <- fsa_compl.FinalState
                fsa2_tmp.AddVertexRange(fsa_compl.FinalState) |> ignore
                fsa2_tmp.AddVertexRange(fsa_compl.InitState) |> ignore
                fsa2_tmp.AddVerticesAndEdgeRange(fsa_compl.Edges) |> ignore

                //перенумеровать вершины fsa2
                let maxVertFsa2Step2 = Seq.max fsa_compl.Vertices
                let fsa2DictStep2 = new Dictionary<_,_>()
                let iStep2 = ref (maxVertFsa2Step2 + 1)
                for v in fsa2.Vertices do
                    fsa2DictStep2.Add(v, !iStep2)
                    iStep2 := !iStep2 + 1

                for edge in fsa2.Edges do
                    new EdgeFSA<_>(fsa2DictStep2.[edge.Source], fsa2DictStep2.[edge.Target], edge.Tag) |> fsa2_tmp.AddVerticesAndEdge |> ignore 

                for v in fsa_compl.FinalState do
                    new EdgeFSA<_>(v, fsa2DictStep2.[fsa2.InitState.[0]], newSmb smb1) |> fsa2_tmp.AddVerticesAndEdge |> ignore
            
                for v in fsa2.FinalState do
                    new EdgeFSA<_>(fsa2DictStep2.[v], fsa_compl.InitState.[0], newSmb smb2) |> fsa2_tmp.AddVerticesAndEdge |> ignore      
                      
                //Step 3. Generate fsa_tmp as intersection of fsa1_tmp and fsa2_tmp
                //fsa1_tmp.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa1_tmp.dot"
                //fsa2_tmp.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa2_tmp.dot"
                let (fsa_tmp:FSA<_>) = FSA<_>.Intersection(fsa1_tmp, fsa2_tmp, equalSmbl)
                //fsa_tmp.PrintToDOT "../../../FST/FST/FSA.Tests/DOTfsa/fsa_tmp.dot"

                let resFSA = 
                    if not (fsa_tmp.IsEmpty) //result of intersection is not empty?
                    then 
                        //Step 4. Construct resFSA from fsa_tmp, delete #1, #2
            
                        //search 'reachable' state which is source vertex for edge with tag #1
                        let reach = new HashSet<_>()

                        for edge in fsa_tmp.Edges do
                            if getChar edge.Tag = smb1
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
                                        if getChar v.Tag = smb2
                                        then 
                                            targetAct.Add v.Target |> ignore 
                                            verticesSearchStr.Add v.Source |> ignore                             
                                        else 
                                            queueV.Enqueue v.Target
                                            verticesSearchStr.Add v.Target |> ignore
                                            verticesSearchStr.Add v.Source |> ignore
                                                                                       
                            visited.Clear()                                   
                            (targetAct, verticesSearchStr)
    
                        let findVert = new ResizeArray<_>()  

                        let getVert v =
                            let vert = HashSet<_>()
                            for edge in fsa_tmp.OutEdges(v) do
                                if getChar edge.Tag = smb1
                                then vert.Add(edge.Target) |> ignore
                            vert

                        for vReach in reach do
                            let vSet = getVert vReach
                            for outVReach in vSet do              
                                let searchStr = bfs outVReach fsa_tmp
                                let vertSearchStr = new VerticesSearchString(vReach, fst searchStr, snd searchStr)
                                findVert.Add vertSearchStr
                   
                        //перенумеровывать вершины FSA3 при каждом добавлении их в FSA1
                        let maxVertCurr = Seq.max fsa_tmp.Vertices
                        let iCurr = ref (maxVertCurr + 1)

                        let changeNumerationFSA3() = 
                            let fsa3Dict = new Dictionary<_,_>()
                            for v in fsa3.Vertices do
                                fsa3Dict.Add(v, !iCurr)
                                iCurr := !iCurr + 1
                            fsa3Dict

                        let mutable fsa3DictCurr = changeNumerationFSA3()

                        for vReach in reach do                    
                            let findInfoForVReach = findVert.Find(fun x -> x.startAct = vReach)
                            for finalVReach in findInfoForVReach.endActs do
                                new EdgeFSA<_>(vReach, fsa3DictCurr.[fsa3.InitState.[0]], Eps) |> fsa_tmp.AddVerticesAndEdge |> ignore
                                for finalVFSA3 in fsa3.FinalState do
                                    new EdgeFSA<_>(fsa3DictCurr.[finalVFSA3], finalVReach, Eps) |> fsa_tmp.AddVerticesAndEdge |> ignore
                                for edgeFsa3 in fsa3.Edges do
                                    new EdgeFSA<_>(fsa3DictCurr.[edgeFsa3.Source], fsa3DictCurr.[edgeFsa3.Target], edgeFsa3.Tag) |> fsa_tmp.AddVerticesAndEdge |> ignore
                                fsa3DictCurr <- changeNumerationFSA3()

                        for vReach in reach do 
                            let findInfoForVReach = findVert.Find(fun x -> x.startAct = vReach)
                            //delete verts from fsa_tmp
                            for vUnused in findInfoForVReach.verticesSearchStr do
                                fsa_tmp.RemoveVertex(vUnused) |> ignore
                                fsa_tmp.FinalState.Remove(vUnused) |> ignore
                                fsa_tmp.InitState.Remove(vUnused) |> ignore

                        fsa_tmp.NfaToDfa
                    else fsa1

                resFSA

    static let isSubFsa (a1: FSA<_>) (a2: FSA<_>) (fsaParams: FsaParams<_,_>) = 
        if a1.IsEmpty
        then true
        elif not <| a2.IsEmpty
        then
            let a2Complement = complementation a2 fsaParams.Alphabet fsaParams.NewSymbol fsaParams.GetChar
            let intersFsa = FSA<_>.Intersection (a1, a2Complement, fsaParams.SymbolsAreEqual)
            intersFsa.IsEmpty
        else false

    // Widening operator implementation
    static let initsOrFinalsProblemMsg = 
        "Can't define initial or final states for widened FSA, states may be computed incorrectly"
    static let stateMultipleTransitionsMsg =
        "Current automaton is NFA, is must be converted to DFA before widening"
    static let eqClassMultipleTransitionsMsg =
        "Widened FSA under construction is NFA"
    static let epsTransitionInWideningMsg = stateMultipleTransitionsMsg

    static let dfsCollectingEdges (state: int) (getNextEdges: int -> list<EdgeFSA<'a>>) getNextState =
        let rec dfs state visited (edges: list<EdgeFSA<'a>>) =
            if not <| Set.contains state visited
            then
                let visited = Set.add state visited
                let nextEdges = getNextEdges state
                let edges = nextEdges @ edges
                nextEdges
                |> List.map getNextState
                |> List.fold (fun (v, e) succ -> dfs succ v e) (visited, edges)
            else visited, edges
        dfs state Set.empty []

    static let buildFsaParts state vertices (edges: list<EdgeFSA<'a>>) filterStates =
        let states1 = ResizeArray.singleton(state)
        let filterSet = Set.ofSeq filterStates
        let states2 = 
            vertices
            |> Set.filter (fun s -> Set.contains s filterSet)
            |> ResizeArray.ofSeq
        let transitions = 
            edges 
            |> List.map (fun e -> e.Source, e.Tag, e.Target) 
            |> ResizeArray.ofList
        states1, states2, transitions

    /// Builds the sub automaton that generates the language consisting of words 
    /// accepted by the original automaton with q being the initial state
    static let subFsaFrom (fsa: FSA<_>) q =
        let getOutEdges st = List.ofSeq <| fsa.OutEdges(st)
        let getTarget (e: Edge<_>) = e.Target
        let vertices, edges = dfsCollectingEdges q getOutEdges getTarget
        let inits, finals, trans = buildFsaParts q vertices edges fsa.FinalState
        let fsa = 
            let fsa = FSA (inits, finals, trans)
            // here we add vertices because 'trans' list can be empty
            vertices |> Set.iter (ignore << fsa.AddVertex)
            fsa
        fsa

    /// Builds the sub automaton that generates the language consisting of words 
    /// accepted by the original automaton with q being the only final state
    static let subFsaTo (fsa: FSA<_>) q =
        let bidirectionalFsa = fsa.ToBidirectionalGraph()
        let getInEdges st = List.ofSeq <| bidirectionalFsa.InEdges(st)
        let getSource (e: Edge<_>) = e.Source
        let vertices, edges = dfsCollectingEdges q getInEdges getSource
        let finals, inits, trans = buildFsaParts q vertices edges fsa.InitState
        let fsa = 
            let fsa = FSA (inits, finals, trans)
            // here we add vertices because 'trans' list can be empty
            vertices |> Set.iter (ignore << fsa.AddVertex)
            fsa
        fsa

    /// Checks if q1 from fsa1 is equivalent to q2 from fsa2
    /// in the sense of relation assumed by widening operator 
    static let isEquivalent q1 (fsa1: FSA<_>) q2 (fsa2: FSA<_>) (fsaParams: FsaParams<_,_>) =
        let fsaFromQ1 = subFsaFrom fsa1 q1
        let fsaFromQ2 = subFsaFrom fsa2 q2
        if isSubFsa fsaFromQ1 fsaFromQ2 fsaParams && 
            isSubFsa fsaFromQ2 fsaFromQ1 fsaParams
        then true
        else
            let fsaToQ1 = subFsaTo fsa1 q1
            let fsaToQ2 = subFsaTo fsa2 q2
            let intersFsa = FSA<_>.Intersection(fsaToQ1, fsaToQ2, fsaParams.SymbolsAreEqual)
            not <| intersFsa.IsEmpty

    static let findRelations (fsa1: FSA<_>) (fsa2: FSA<_>) (fsaParams: FsaParams<_,_>) =
        let createLoop1 st = 
            let sf = StateFromFsaFuns.fromFsa1 st
            Edge(sf, sf)
        let createLoop2 st = 
            let sf = StateFromFsaFuns.fromFsa2 st
            Edge(sf, sf)
        let tryCreateEdge12 src dst =
            if isEquivalent src fsa1 dst fsa2 fsaParams
            then
                let sf1 = StateFromFsaFuns.fromFsa1 src
                let sf2 = StateFromFsaFuns.fromFsa2 dst
                Some(Edge(sf1, sf2))
            else None 
        let fsa1States = fsa1.Vertices
        let fsa2States = fsa2.Vertices
        let trivialRelations = 
            Seq.append (fsa1States |> Seq.map createLoop1) (fsa2States |> Seq.map createLoop2)
        let relations =
            fsa1States
            |> Seq.map
                (fun st1 -> fsa2States |> Seq.choose (fun st2 -> tryCreateEdge12 st1 st2))
            |> Seq.concat
        let allRelations = Seq.append trivialRelations relations
        let inverseRelations = allRelations |> Seq.map (fun e -> Edge(e.Target, e.Source))
        allRelations, inverseRelations

    /// Builds equivalence classees using FSA.isEquivalent function
    static let buildEquivalenceClasses (fsa1: FSA<_>) (fsa2: FSA<_>) (fsaParams: FsaParams<_,_>) =
        // find relations
        let relations, inverseRelations = findRelations fsa1 fsa2 fsaParams
        // build relations graph and find connected components
        let relationsGraph = AdjacencyGraph<StateFromFsa,Edge<StateFromFsa>>()
        do relationsGraph.AddVerticesAndEdgeRange relations |> ignore
        do relationsGraph.AddVerticesAndEdgeRange inverseRelations |> ignore
        let _, stateToEqClassIdMap = relationsGraph.StronglyConnectedComponents()
        // create alternative components representation
        stateToEqClassIdMap
        |> Seq.fold
            (
                fun acc pair -> 
                    let componentNumber = pair.Value
                    let state = pair.Key
                    let eqClass = defaultArg (Map.tryFind componentNumber acc) EqClassFuns.empty
                    Map.add componentNumber (EqClassFuns.add state eqClass) acc
            )
            Map.empty

    static let symbolsToCheck (eqClass: EqClass) (fsa1: FSA<_>) (fsa2: FSA<_>) =
        let getOutEdges states (fsa: FSA<_>) =
            states
            |> Set.toList
            |> List.map (fun s -> fsa.OutEdges(s) |> List.ofSeq)
            |> List.concat
        let outEdges = 
            let e1 = getOutEdges (EqClassFuns.fsa1States eqClass) fsa1
            let e2 = getOutEdges (EqClassFuns.fsa2States eqClass) fsa2
            List.append e1 e2
        outEdges 
        |> List.map (fun e -> e.Tag) 
        |> Seq.distinctBy 
            (
                function 
                    | Smbl(ch) -> ch
                    | _ -> failwith epsTransitionInWideningMsg
            )

    static let filterByContainsWithQuantifier quantifier elems (eqClasses: Map<int, EqClass>) =
        eqClasses
        |> Map.filter 
            (fun _ ec -> quantifier (fun e -> EqClassFuns.contains e ec) elems)

    static let createTransition eqClassId sym (eqClasses: Map<int, EqClass>) fsa1 fsa2 =
        let isSink state (fsa: FSA<_>) =
            let isNotFinal = ResizeArray.tryFind ((=) state) fsa.FinalState |> Option.isNone
            isNotFinal && (fsa.OutEdges(state) |> Seq.forall (fun e -> e.Target = e.Source))
        let getDstStates srcStates (fsa: FSA<_>) =
            srcStates
            |> Set.toList
            |> List.map
                (fun st ->  fsa.OutEdges(st) |> Seq.filter (fun e -> e.Tag = sym) |> List.ofSeq)
            |> List.choose 
                (
                    function 
                        // means that transition from the state by the sym is not definded,
                        // so we have not completely defined FSA and it is assumed that
                        // transition from this state by sym leads to sink state
                        | [] -> None 
                        // transition from the state is defined and deterministic
                        // so we only need to check that it leads to not sink state
                        | x :: [] when not <| isSink x.Target fsa -> Some(x.Target) 
                        | x :: [] -> None
                        // transition is non deterministic, in current implementation
                        // it is assumed that input FSAs must be deterministic
                        | _ -> failwith stateMultipleTransitionsMsg
                )
            |> Set.ofList
        let tryCreateTransition srcStates (fsa: FSA<_>) =
            let dstStates = getDstStates srcStates fsa
            if Set.isEmpty dstStates
            then None
            else
                let dstEqClasses = 
                    filterByContainsWithQuantifier Set.forall dstStates eqClasses
                    |> List.ofSeq
                match dstEqClasses with
                | [] -> None
                | h :: [] -> Some(h.Key)
                | _ -> failwith eqClassMultipleTransitionsMsg

        let srcEqClass = Map.find eqClassId eqClasses
        let dstEqClassId1 = tryCreateTransition (EqClassFuns.fsa1States srcEqClass) fsa1
        let dstEqClassId2 = tryCreateTransition (EqClassFuns.fsa2States srcEqClass) fsa2
        match dstEqClassId1, dstEqClassId2 with
        | Some(id1), Some(id2) when id1 = id2 -> Some(id1)
        | opt1, None -> opt1
        | None, opt2 -> opt2
        | _ -> None

    static let createTransitions (eqClasses: Map<int, EqClass>) (fsa1: FSA<_>) (fsa2: FSA<_>) =
        let symbolsMap = Map.map (fun _ v -> symbolsToCheck v fsa1 fsa2) eqClasses
        eqClasses
        |> Map.toList
        |> List.map
            (   
                fun (classId, eqClass) ->
                    Map.find classId symbolsMap
                    |> Seq.choose 
                        (
                            fun sym -> 
                                createTransition classId sym eqClasses fsa1 fsa2 
                                |> Option.map (fun dst -> classId, sym, dst)
                        )
            )
        |> Seq.concat
        |> ResizeArray.ofSeq
        
    static let widen (fsa1: FSA<_>) (fsa2: FSA<_>) (fsaParams: FsaParams<_,_>) =
        let dfa1 = fsa1.NfaToDfa
        let dfa2 = fsa2.NfaToDfa
        let eqClasses = buildEquivalenceClasses dfa1 dfa2 fsaParams
        let wTransitions = createTransitions eqClasses dfa1 dfa2
        let wInits = 
            let unitedInits = ResizeArray.append dfa1.InitState dfa2.InitState
            filterByContainsWithQuantifier ResizeArray.forall unitedInits eqClasses
            |> ResizeArray.ofSeq |> ResizeArray.map (fun kvp -> kvp.Key)
        let wFinals = 
            let unitedFinals = ResizeArray.append dfa1.FinalState dfa2.FinalState
            filterByContainsWithQuantifier ResizeArray.exists unitedFinals eqClasses
            |> ResizeArray.ofSeq |> ResizeArray.map (fun kvp -> kvp.Key)
        if ResizeArray.isEmpty wInits || ResizeArray.isEmpty wFinals
        then failwith initsOrFinalsProblemMsg
        FSA (wInits, wFinals, wTransitions)
         
    new () = 
        FSA<_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<_>())
    
    member this.NfaToDfa = nfaToDfa this
    member this.RemoveExtraPaths = removeExtraPaths this
    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.PrintToDOT(filePath, ?printSmb) = printFSAtoDOT filePath printSmb
    member this.Concat fsa2 = concat this fsa2
    static member Concat(fsa1, fsa2) = concat fsa1 fsa2
    member this.Union fsa2 = union this fsa2
    static member Union(fsa1, fsa2) = union fsa1 fsa2
    ///fsa1 -- original strings; fsa2 -- match strings; fsa3 -- replacement strings, FSAs are not empty
    static member Replace(fsa1, fsa2, fsa3, smb1, smb2, getChar, newSmb, equalSmbl) = replace fsa1 fsa2 fsa3  smb1 smb2 getChar newSmb equalSmbl
    member this.Complementation(alphabet, newSmb,  getChar) = complementation this alphabet newSmb getChar 
    static member Intersection(fsa1, fsa2, equalSmbl) = intersection fsa1 fsa2 equalSmbl
    member this.IsEmpty =  isEmptyFSA this

    /// Checks if the language accepted by FSA a1 is a sublanguage 
    /// of the language accepted by FSA a2. 
    /// Expects any fsa
    static member IsSubFsa (a1: FSA<_>, a2: FSA<_>, fsaParams: FsaParams<_,_>) = 
        isSubFsa a1 a2 fsaParams

    /// Creates FSA accepting any word over the alphabet, passed as a parameter through FsaParams
    static member CreateAnyWordsFsa (fsaParams: FsaParams<_,_>) = 
        let inits = ResizeArray.singleton 0
        let finals = ResizeArray.singleton 0
        let trans = 
            fsaParams.Alphabet 
            |> Seq.map (fun ch -> (0, fsaParams.NewSymbol ch, 0))
            |> ResizeArray.ofSeq
        FSA(inits, finals, trans)

    /// Widening operator for FSA
    static member Widen (fsa1: FSA<_>, fsa2: FSA<_>, fsaParams: FsaParams<_,_>) = 
        widen fsa1 fsa2 fsaParams
