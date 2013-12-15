module DotLangParser

open Yard.Generators.RNGLR.Parser    
open Yard.Generators.RNGLR.AST

open DotParserProject.DotParser
open DotParserProject.GraphDataContainer

let parse (input_file: string) =
    let tokens = 
        let lexbuf = Lexing.LexBuffer<_>.FromTextReader <| new System.IO.StreamReader(input_file)
        seq { while not lexbuf.IsPastEndOfStream do
                  yield DotParserProject.DotLexer.tokenize lexbuf }

    match buildAst tokens with
    | Error (pos, token, msg, debugFuns, _) ->
        printfn "Error on position %d, token %A: %s" pos token msg
        new ResizeArray<GraphDataContainer>()
    | Success (ast, errors) ->
        let args = {
            tokenToRange = fun _ -> Unchecked.defaultof<_>, Unchecked.defaultof<_>
            zeroPosition = Unchecked.defaultof<_>
            clearAST = false
            filterEpsilons = false
        }
        defaultAstToDot ast "ast.dot"    
        translate args ast errors |> ignore
        graphs