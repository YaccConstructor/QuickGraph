// Eugene Auduchinok, 2016

module DotParserProject.GraphData

open Option
open System.Collections.Generic


type Attributes = Map<string, string>
type GraphData =
    {
        IsDirected : bool
        IsStrict   : bool
        Nodes : Map<string,          Attributes>
        Edges : Map<string * string, Attributes list>
        GraphAttributes : Attributes
        NodeAttributes  : Attributes
        EdgeAttributes  : Attributes
    }

let emptyGraph d s =
    {
        IsDirected = d
        IsStrict   = s
        Nodes = Map.empty
        Edges = Map.empty
        GraphAttributes = Map.empty
        NodeAttributes  = Map.empty
        EdgeAttributes  = Map.empty
    }

let copyAttrs (g : GraphData) =
    {
        IsDirected = g.IsDirected
        IsStrict   = g.IsStrict
        Nodes = Map.empty
        Edges = Map.empty
        GraphAttributes = g.GraphAttributes
        NodeAttributes  = g.NodeAttributes
        EdgeAttributes  = g.EdgeAttributes
    }

let merge m1 m2 = Map.fold (fun acc key value -> Map.add key value acc) m1 m2

let addAttributes (g : GraphData) (key : string) (a : Attributes) =
    match key with
    | "graph" -> { g with GraphAttributes = merge g.GraphAttributes a }
    | "node"  -> { g with NodeAttributes  = merge g.NodeAttributes a }
    | "edge"  -> { g with EdgeAttributes  = merge g.EdgeAttributes a }
    | _ -> failwithf "DotParser: parser error: wrong attribute key: %s" key


let addNode (g : GraphData) (n : string) =
    { g with Nodes = Map.add n g.NodeAttributes g.Nodes }, [n]


let addEdge (g : GraphData) (n1 : string) (n2 : string) =
    let edge = if (not <| g.IsDirected) && n2 < n1 then n2, n1 else n1, n2
    let newEdges =
        match g.Edges.TryFind edge, g.IsStrict with
        | Some oldEdges, true  -> [merge (List.head oldEdges) g.EdgeAttributes]
        | Some oldEdges, false -> g.EdgeAttributes :: oldEdges
        | _ -> [g.EdgeAttributes]

    { g with Edges = Map.add edge newEdges g.Edges } (* todo: merge attrs when strict *)


let addEdges (g : GraphData) (ns1 : string list) (ns2 : string list) =
    (List.fold (fun acc n1 -> List.fold (fun acc n2 -> addEdge acc n1 n2) acc ns2) g ns1), ns2


let addSubgraph (g : GraphData) (s : GraphData) =
    let addNodes g = Map.fold (fun acc n attr -> fst <| addNode acc n) g s.Nodes
    let addEdges g = Map.fold (fun acc (n1, n2) attr -> addEdge acc n1 n2) g s.Edges

    (g |> addNodes |> addEdges), (s.Nodes |> Map.toList |> List.map fst)