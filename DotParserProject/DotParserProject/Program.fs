// Learn more about F# at http://fsharp.net

open System.IO
open System
open Microsoft.FSharp.Text
open Microsoft.FSharp.Reflection

open Yard.Generators.RNGLR.Parser    
open Yard.Generators.RNGLR.AST

open DotParserProject.DotParser
open System.Collections.Generic
open QuickGraph

let src = "..\\..\\..\\test_inputs\\test1.dot"
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
//    defaultAstToDot ast @"..\..\astFromSeq.dot"
//    defaultAstToDot ast @"..\..\astFromDot.dot"
//    printfn "%A" result
    
    translate args ast errors |> ignore

    graphs.[0].ToFiles()
    for gr in graphs do
        gr.PrintAllCollectedData()
        printfn "---------------------------------------------------------"
    printfn "Completed"

    let handler1 v1 v2 =
        new SUndirectedEdge<string>(v1,v2)

    let handler2 v1 v2 =
        let iv1 = Int32.Parse(v1)
        let iv2 = Int32.Parse(v2)
        new SUndirectedEdge<int>(iv1,iv2)

    let handler3 v1 v2 (tags: list<string*string>) =
        match tags.Length with
        | 0 ->
            new STaggedEdge<string, string>(v1, v2, "_no_tag_")
        | _ ->
            let _, tag = tags.[0]
            new STaggedEdge<string, string>(v1, v2, tag)

    let edges_array1 = graphs.[0].GetEdgeArray handler1
    let gr1 = edges_array1.ToAdjacencyGraph<string, SUndirectedEdge<string>>()
    for v in gr1.Vertices do
        printf "%A " v
    printfn ""
    for e in gr1.Edges do
        printfn "%A" e
    printfn ""

    let edges_array2 = graphs.[0].GetTaggedEdgeArray handler3
    let gr2 = edges_array2.ToAdjacencyGraph<string, STaggedEdge<string, string>>()
    for v in gr2.Vertices do
        printf "%A " v
    printfn ""
    for e in gr2.Edges do
        printfn "%A" e
    printfn ""

        
let key = Console.ReadKey(true)