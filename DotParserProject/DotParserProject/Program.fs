// Learn more about F# at http://fsharp.net

open System.IO
open System
open Microsoft.FSharp.Text
open Microsoft.FSharp.Reflection

open Yard.Generators.RNGLR.Parser    
open Yard.Generators.RNGLR.AST

open DotParserProject.DotParser
open DotParserProject.ParsingFuncs
open System.Collections.Generic
open QuickGraph

let src = "..\\..\\..\\test_inputs\\test5.dot"
let tokens = 
    let lexbuf = Lexing.LexBuffer<_>.FromTextReader <| new System.IO.StreamReader(src)
    seq { while not lexbuf.IsPastEndOfStream do
              yield DotParserProject.DotLexer.tokenize lexbuf }

match buildAst tokens with
| Error (pos, token, msg, debugFuns, _) ->
    printfn "Error on position %d, token %A: %s" pos token msg
| Success (ast, errors) ->
    //ast.PrintAst()
    let args = {
        tokenToRange = fun _ -> Unchecked.defaultof<_>, Unchecked.defaultof<_>
        zeroPosition = Unchecked.defaultof<_>
        clearAST = false
        filterEpsilons = false
    }
    
    defaultAstToDot ast "ast.dot"
//    let result:list<list<string>> = translate args ast errors
//    printfn "result = %A" result
//    defaultAstToDot ast @"..\..\astFromSeq.dot"
//    defaultAstToDot ast @"..\..\astFromDot.dot"
//    printfn "%A" result
    
    translate args ast errors |> ignore
    allCollectedDataToFile()
    printAllCollectedData()
//    CheckEdgeOperator graph_info

    let g = new AdjacencyGraph<string, SEdge<string>>()
    createAdjacencyGraph vertices_lists g |> ignore
    printfn ""
    for v in g.Vertices do
        printf "%A " v
    printfn ""
    for e in g.Edges do
        printfn "%A" e
    printfn ""
    
    let quickgraph_vert = "..\\..\\..\\test_output\\quickgraph_vert.txt"
    let quickgraph_edges = "..\\..\\..\\test_output\\quickgraph_edges.txt"
    let outp = File.CreateText quickgraph_vert
    let outp2 = File.CreateText quickgraph_edges 
    for v in g.Vertices do
        outp.WriteLine v
    for e in g.Edges do
        outp2.WriteLine e
    outp.Close()
    outp2.Close()
    
let key = Console.ReadKey(true)