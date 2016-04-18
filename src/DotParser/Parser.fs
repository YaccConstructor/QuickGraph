// Eugene Auduchinok, 2016

module DotParser

open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR.AST
open DotParserProject.DotParser // todo: replace with Gen.DotParser
open DotParserProject.Gen.DotLexer
open DotParserProject.GraphData
open System.Collections.Generic
open QuickGraph

let parse (v: string -> Dictionary<_,_> -> _) str : IMutableVertexAndEdgeSet<_,SEdge<string>> = (* todo: should be some generic *)
    let translateArgs = {
            tokenToRange = fun _ -> Unchecked.defaultof<_>, Unchecked.defaultof<_>
            zeroPosition = Unchecked.defaultof<_>
            clearAST = false
            filterEpsilons = false
        }

    let lexbuf = Lexing.LexBuffer<_>.FromString str
    let tokens = seq { while not lexbuf.IsPastEndOfStream do yield tokenize lexbuf }

    let parsedGraphDataList : GraphData list =
        match buildAst tokens with
        | Error (pos, token, msg, _, _) -> failwithf "Error on position %d, token %A: %s" pos token msg
        | Success (ast, errors) -> translate translateArgs ast errors

    let graphData =
        match parsedGraphDataList with
        | data :: _ -> data
        | [] -> failwith "Parser returned no data"

    let graph = if graphData.IsDirected
                then BidirectionalGraph<_,_> (not graphData.IsStrict) :> IMutableVertexAndEdgeSet<_,_>
                else UndirectedGraph<_,_> (not graphData.IsStrict) :> IMutableVertexAndEdgeSet<_,_>

    for n in graphData.Nodes do graph.AddVertex n.Key |> ignore

    for e in graphData.Edges do
        let n1, n2 = e.Key
        for attr in e.Value do
            graph.AddEdge (SEdge<_> (n1, n2)) |> ignore (* todo: add attrs here *)

    graph