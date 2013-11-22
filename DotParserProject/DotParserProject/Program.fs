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

//my functions
let PrintAdjList (adj_list: Dictionary<string, Dictionary<string, int>>) =
    for list in adj_list do
        printf "%s : " list.Key
        for vertice in list.Value do
            printf "%A " vertice
        printfn ""

//not my)

let src = "..\\..\\..\\test_inputs\\test3.dot"
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
//    vrt |> ResizeArray.iter (printfn "vrt = %A;")
//    printfn "result = %A" result
//    defaultAstToDot ast @"..\..\astFromSeq.dot"
//    defaultAstToDot ast @"..\..\astFromDot.dot"
//    printfn "%A" result
    translate args ast errors |> ignore
    PrintAdjList adj_list
    
let key = Console.ReadKey(true)