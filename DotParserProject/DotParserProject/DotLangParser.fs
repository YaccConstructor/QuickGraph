module DotLangParser

open Yard.Generators.RNGLR.Parser    
open Yard.Generators.RNGLR.AST

open DotParserProject.DotParser
open DotParserProject.GraphDataContainer
open System.Collections

let parse (lexbuf: Lexing.LexBuffer<char>) =
    //graphs.Clear()
    let tokens = seq {while not lexbuf.IsPastEndOfStream do yield DotParserProject.DotLexer.tokenize lexbuf }
    let gr =       
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
    let main_graph = gr.[0] 
    let vrtArr = List.toArray <| main_graph.GetVerticeArray()
    let mutable lstvertx =
        [|for i in vrtArr -> (i,List.toArray <| main_graph.VerticeTags("[" + i + "]"))|]
    let simpleHandler v1 v2 (v3: list<string*string>) = (v1,v2,List.toArray <| v3)
    let edges_array = main_graph.GetTaggedEdgeArray simpleHandler
    (lstvertx, edges_array)     
     
let VertAndEdgesStr (str: string) =
    parse (Lexing.LexBuffer<_>.FromString <| str)
        
let VertAndEdges (file: string) =
    parse (Lexing.LexBuffer<_>.FromTextReader <| new System.IO.StreamReader(file))
