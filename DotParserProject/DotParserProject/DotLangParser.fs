module DotLangParser

open Yard.Generators.RNGLR.Parser    
open Yard.Generators.RNGLR.AST
open DotParserProject.DotParser
open DotParserProject.GraphDataContainer
open System.Collections

let parse (str) =
    graphs.Clear()
    graphs.Add <| new GraphDataContainer()

    let lexbuf = Lexing.LexBuffer<_>.FromString str

    let tokens =
        seq {
            while not lexbuf.IsPastEndOfStream do 
                yield DotParserProject.DotLexer.tokenize lexbuf
        }

    let graph =       
        match buildAst tokens with
        | Error (pos, token, msg, debugFuns, _) ->
            printfn "Error on position %d, token %A: %s" pos token msg
            new GraphDataContainer()
        | Success (ast, errors) ->
            let args = {
                tokenToRange = fun _ -> Unchecked.defaultof<_>, Unchecked.defaultof<_>
                zeroPosition = Unchecked.defaultof<_>
                clearAST = false
                filterEpsilons = false
            }
            ast.EliminateCycles()
            ast.ChooseSingleAst()
            translate args ast errors |> ignore
            graphs.[0] // todo: add subgraph support
    graph
