module DotLangParser

open Yard.Generators.RNGLR.Parser    
open Yard.Generators.RNGLR.AST

open DotParserProject.DotParser
open DotParserProject.GraphDataContainer
open System.Collections

let parse tokens = 
    graphs.Clear()
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
    let mutable lst = []  
    let mutable arr = [||]
    let mutable str = ""
    let vrtArr = List.toArray <| main_graph.GetVerticeArray()
    for i in vrtArr do
        arr <- List.toArray <| main_graph.VerticeTags("[" + i + "]")
        lst <- List.append lst [(i,arr)]    
    let mutable lstvertx = List.toArray <| lst    
    let simpleHandler v1 v2 (v3: list<string*string>) = (v1,v2,List.toArray <| v3)
    let edges_array = main_graph.GetTaggedEdgeArray simpleHandler
    let mutable L = (lstvertx, edges_array)
    L
     
     
     
let VertAndEdgesStr (str: string) =
    graphs.Clear()
    let tokens = 
            let lexbuf = Lexing.LexBuffer<_>.FromString <| str
            seq { while not lexbuf.IsPastEndOfStream do yield DotParserProject.DotLexer.tokenize lexbuf }
    parse tokens
    
let VertAndEdges (file: string) =
    graphs.Clear()
    let tokens = 
        let lexbuf = Lexing.LexBuffer<_>.FromTextReader <| new System.IO.StreamReader(file)
        seq { while not lexbuf.IsPastEndOfStream do yield DotParserProject.DotLexer.tokenize lexbuf }
    parse tokens
