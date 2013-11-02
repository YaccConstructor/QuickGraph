
# 2 "DotParser.fs"
module DotParserProject.DotParser
#nowarn "64";; // From fsyacc: turn off warnings that type variables used in production annotations are instantiated to concrete type
open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR
open Yard.Generators.RNGLR.AST
type Token =
    | ASSIGN of (string)
    | COL of (string)
    | COMMA of (string)
    | COMPASS of (string)
    | DIEDGEOP of (string)
    | DIGRAPH of (string)
    | EDGE of (string)
    | EDGEOP of (string)
    | GRAPH of (string)
    | ID of (string)
    | LCURBRACE of (string)
    | LSQBRACE of (string)
    | NODE of (string)
    | RCURBRACE of (string)
    | RNGLR_EOF of (string)
    | RSQBRACE of (string)
    | SEP of (string)
    | STRICT of (string)
    | SUBGR of (string)

let genLiteral (str : string) posStart posEnd =
    match str.ToLower() with
    | x -> failwithf "Literal %s undefined" x
let tokenData = function
    | ASSIGN x -> box x
    | COL x -> box x
    | COMMA x -> box x
    | COMPASS x -> box x
    | DIEDGEOP x -> box x
    | DIGRAPH x -> box x
    | EDGE x -> box x
    | EDGEOP x -> box x
    | GRAPH x -> box x
    | ID x -> box x
    | LCURBRACE x -> box x
    | LSQBRACE x -> box x
    | NODE x -> box x
    | RCURBRACE x -> box x
    | RNGLR_EOF x -> box x
    | RSQBRACE x -> box x
    | SEP x -> box x
    | STRICT x -> box x
    | SUBGR x -> box x

let numToString = function
    | 0 -> "a_list"
    | 1 -> "attr_list"
    | 2 -> "attr_stmt"
    | 3 -> "compass_pt"
    | 4 -> "edgeRHS"
    | 5 -> "edge_operator"
    | 6 -> "edge_stmt"
    | 7 -> "error"
    | 8 -> "graph"
    | 9 -> "id"
    | 10 -> "node_id"
    | 11 -> "node_stmt"
    | 12 -> "port"
    | 13 -> "stmt"
    | 14 -> "stmt_list"
    | 15 -> "subgraph"
    | 16 -> "yard_exp_brackets_1"
    | 17 -> "yard_exp_brackets_2"
    | 18 -> "yard_exp_brackets_3"
    | 19 -> "yard_exp_brackets_4"
    | 20 -> "yard_exp_brackets_5"
    | 21 -> "yard_exp_brackets_6"
    | 22 -> "yard_exp_brackets_7"
    | 23 -> "yard_opt_1"
    | 24 -> "yard_opt_10"
    | 25 -> "yard_opt_11"
    | 26 -> "yard_opt_12"
    | 27 -> "yard_opt_13"
    | 28 -> "yard_opt_14"
    | 29 -> "yard_opt_15"
    | 30 -> "yard_opt_2"
    | 31 -> "yard_opt_3"
    | 32 -> "yard_opt_4"
    | 33 -> "yard_opt_5"
    | 34 -> "yard_opt_6"
    | 35 -> "yard_opt_7"
    | 36 -> "yard_opt_8"
    | 37 -> "yard_opt_9"
    | 38 -> "yard_start_rule"
    | 39 -> "ASSIGN"
    | 40 -> "COL"
    | 41 -> "COMMA"
    | 42 -> "COMPASS"
    | 43 -> "DIEDGEOP"
    | 44 -> "DIGRAPH"
    | 45 -> "EDGE"
    | 46 -> "EDGEOP"
    | 47 -> "GRAPH"
    | 48 -> "ID"
    | 49 -> "LCURBRACE"
    | 50 -> "LSQBRACE"
    | 51 -> "NODE"
    | 52 -> "RCURBRACE"
    | 53 -> "RNGLR_EOF"
    | 54 -> "RSQBRACE"
    | 55 -> "SEP"
    | 56 -> "STRICT"
    | 57 -> "SUBGR"
    | _ -> ""

let tokenToNumber = function
    | ASSIGN _ -> 39
    | COL _ -> 40
    | COMMA _ -> 41
    | COMPASS _ -> 42
    | DIEDGEOP _ -> 43
    | DIGRAPH _ -> 44
    | EDGE _ -> 45
    | EDGEOP _ -> 46
    | GRAPH _ -> 47
    | ID _ -> 48
    | LCURBRACE _ -> 49
    | LSQBRACE _ -> 50
    | NODE _ -> 51
    | RCURBRACE _ -> 52
    | RNGLR_EOF _ -> 53
    | RSQBRACE _ -> 54
    | SEP _ -> 55
    | STRICT _ -> 56
    | SUBGR _ -> 57

let isLiteral = function
    | ASSIGN _ -> false
    | COL _ -> false
    | COMMA _ -> false
    | COMPASS _ -> false
    | DIEDGEOP _ -> false
    | DIGRAPH _ -> false
    | EDGE _ -> false
    | EDGEOP _ -> false
    | GRAPH _ -> false
    | ID _ -> false
    | LCURBRACE _ -> false
    | LSQBRACE _ -> false
    | NODE _ -> false
    | RCURBRACE _ -> false
    | RNGLR_EOF _ -> false
    | RSQBRACE _ -> false
    | SEP _ -> false
    | STRICT _ -> false
    | SUBGR _ -> false

let getLiteralNames = []
let mutable private cur = 0
let leftSide = [|22; 21; 20; 20; 19; 19; 18; 18; 18; 17; 16; 16; 9; 3; 28; 28; 29; 29; 15; 27; 27; 12; 12; 26; 26; 10; 25; 25; 11; 5; 5; 24; 24; 4; 37; 37; 6; 36; 36; 35; 35; 0; 34; 34; 33; 33; 1; 2; 13; 13; 13; 13; 13; 31; 31; 32; 32; 14; 30; 30; 23; 23; 8; 38|]
let private rules = [|57; 29; 40; 3; 10; 15; 10; 15; 47; 51; 45; 13; 55; 32; 47; 44; 48; 42; 22; 9; 28; 49; 14; 52; 21; 40; 9; 27; 40; 3; 12; 9; 26; 1; 10; 25; 46; 43; 4; 5; 20; 24; 1; 19; 4; 37; 0; 41; 9; 39; 9; 35; 36; 1; 0; 50; 33; 54; 34; 18; 1; 11; 6; 2; 9; 39; 9; 15; 17; 14; 31; 9; 56; 23; 16; 30; 49; 14; 52; 8|]
let private rulesStart = [|0; 2; 4; 5; 6; 7; 8; 9; 10; 11; 14; 15; 16; 17; 18; 18; 19; 19; 20; 24; 24; 25; 28; 30; 30; 31; 33; 33; 34; 36; 37; 38; 38; 39; 42; 42; 43; 46; 46; 47; 47; 48; 53; 53; 54; 54; 55; 59; 61; 62; 63; 64; 67; 68; 68; 69; 69; 70; 71; 71; 72; 72; 73; 79; 80|]
let startRule = 63

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 77; 3; 75; 76; 4; 5; 14; 6; 7; 8; 9; 23; 39; 40; 73; 43; 44; 45; 47; 60; 61; 65; 66; 67; 68; 69; 10; 11; 12; 15; 13; 16; 17; 22; 18; 19; 20; 21; 24; 25; 26; 27; 28; 35; 29; 30; 31; 34; 32; 33; 36; 37; 38; 41; 42; 72; 46; 48; 51; 58; 59; 49; 50; 52; 53; 54; 55; 56; 57; 62; 63; 64; 70; 71; 74|]
let private small_gotos =
        [|3; 524288; 1507329; 3670018; 131075; 1048579; 2883588; 3080197; 196611; 589830; 1966087; 3145736; 327681; 3211273; 393235; 131082; 393227; 589836; 655373; 720910; 851983; 917520; 983057; 1114130; 1179667; 1245204; 1441813; 1835030; 2031639; 2949144; 3080217; 3145736; 3342362; 3735579; 589828; 786460; 1703965; 2555934; 2621471; 786434; 589856; 3145736; 983044; 196641; 589858; 2752547; 3145736; 1114115; 1376292; 1769509; 2621478; 1310722; 196647; 2752547; 1507331; 65576; 1638441; 3276842; 1703940; 43; 589868; 2162733; 3145736; 1835009; 2555950; 1900546; 589871; 3145736; 1966082; 2293808; 2687025; 2031620; 50; 589868; 2359347; 3145736; 2293761; 3538996; 2359299; 65589; 2228278; 3276842; 2621441; 3604535; 2686996; 131082; 393227; 589836; 655373; 720910; 851983; 917560; 983057; 1114130; 1179667; 1245204; 1441813; 1835030; 2031639; 2097209; 2949144; 3080217; 3145736; 3342362; 3735579; 2949122; 65594; 3276842; 3080196; 262203; 327740; 2818109; 3014718; 3145731; 65599; 2424896; 3276842; 3342344; 589889; 655426; 983107; 1310788; 1441813; 1835030; 3145736; 3735579; 3407875; 786460; 1703965; 2621471; 3604485; 262213; 327740; 1572934; 2818109; 3014718; 3997697; 3211335; 4063251; 131082; 393227; 589836; 655373; 720910; 851983; 917576; 983057; 1114130; 1179667; 1245204; 1441813; 1835030; 2031639; 2949144; 3080217; 3145736; 3342362; 3735579; 4128769; 3407945; 4521987; 589898; 1900619; 3145736; 4784129; 3407948|]
let gotos = Array.zeroCreate 78
for i = 0 to 77 do
        gotos.[i] <- Array.zeroCreate 58
cur <- 0
while cur < small_gotos.Length do
    let i = small_gotos.[cur] >>> 16
    let length = small_gotos.[cur] &&& 65535
    cur <- cur + 1
    for k = 0 to length-1 do
        let j = small_gotos.[cur + k] >>> 16
        let x = small_gotos.[cur + k] &&& 65535
        gotos.[i].[j] <- lists_gotos.[x]
    cur <- cur + length
let private lists_reduces = [|[|59,1|]; [|50,1|]; [|49,1|]; [|25,1|]; [|24,1|]; [|25,2|]; [|51,3|]; [|12,1|]; [|22,2|]; [|21,2|]; [|20,1|]; [|21,3|]; [|1,2|]; [|13,1|]; [|4,1|]; [|28,1|]; [|27,1|]; [|28,2|]; [|45,1|]; [|41,3|]; [|41,4|]; [|38,1|]; [|41,5|]; [|40,1|]; [|46,3|]; [|43,1|]; [|46,4|]; [|48,1|]; [|9,2|]; [|56,1|]; [|5,1|]; [|52,1|]; [|54,1|]; [|47,2|]; [|36,2|]; [|35,1|]; [|36,3|]; [|2,1|]; [|3,1|]; [|33,2|]; [|32,1|]; [|33,3|]; [|30,1|]; [|29,1|]; [|15,1|]; [|18,4|]; [|57,1|]; [|8,1|]; [|6,1|]; [|7,1|]; [|0,1|]; [|17,1|]; [|0,2|]; [|9,3|]; [|62,6|]; [|11,1|]; [|10,1|]; [|61,1|]|]
let private small_reduces =
        [|262145; 3211264; 458753; 3604481; 524289; 3604482; 589828; 2818051; 3014659; 3276803; 3604483; 655364; 2818052; 3014660; 3276804; 3604484; 720900; 2818053; 3014661; 3276805; 3604485; 851969; 3604486; 917514; 2555911; 2621447; 2686983; 2818055; 3014663; 3145735; 3211271; 3276807; 3538951; 3604487; 1048580; 2818056; 3014664; 3276808; 3604488; 1114116; 2818057; 3014665; 3276809; 3604489; 1179652; 2818058; 3014666; 3276810; 3604490; 1245188; 2818059; 3014667; 3276811; 3604491; 1376260; 2818060; 3014668; 3276812; 3604492; 1441796; 2818061; 3014669; 3276813; 3604493; 1507331; 2818062; 3014670; 3604495; 1572865; 3604496; 1638401; 3604497; 1769473; 3538962; 1966081; 3538963; 2031617; 3538964; 2097153; 3538965; 2162689; 3538966; 2228226; 3145751; 3538967; 2359297; 3604504; 2424833; 3604505; 2490369; 3604506; 2555905; 3604507; 2686977; 3407900; 2752513; 3407901; 2818051; 2818078; 3014686; 3604511; 2883585; 3407904; 3014657; 3604513; 3145729; 3604514; 3211265; 3604515; 3276801; 3604516; 3407876; 2818051; 3014659; 3276803; 3604483; 3473412; 2818085; 3014693; 3276837; 3604517; 3538948; 2818086; 3014694; 3276838; 3604518; 3604482; 3276839; 3604519; 3670018; 3276840; 3604520; 3735554; 3276841; 3604521; 3801091; 3145770; 3211306; 3735594; 3866627; 3145771; 3211307; 3735595; 3932161; 3211308; 4194308; 2818093; 3014701; 3276845; 3604525; 4259841; 3407918; 4325377; 3276847; 4390913; 3276848; 4456449; 3276849; 4521985; 3211314; 4587521; 3211315; 4653057; 3211316; 4718593; 3407925; 4849665; 3473462; 4915202; 3145783; 3211319; 4980738; 3145784; 3211320; 5046274; 2883641; 3080249|]
let reduces = Array.zeroCreate 78
for i = 0 to 77 do
        reduces.[i] <- Array.zeroCreate 58
cur <- 0
while cur < small_reduces.Length do
    let i = small_reduces.[cur] >>> 16
    let length = small_reduces.[cur] &&& 65535
    cur <- cur + 1
    for k = 0 to length-1 do
        let j = small_reduces.[cur + k] >>> 16
        let x = small_reduces.[cur + k] &&& 65535
        reduces.[i].[j] <- lists_reduces.[x]
    cur <- cur + length
let private lists_zeroReduces = [|[|60|]; [|58|]; [|14|]; [|57; 53|]; [|23|]; [|19|]; [|26|]; [|44|]; [|39|]; [|37|]; [|42|]; [|57; 56; 55; 53|]; [|34|]; [|31|]; [|16|]|]
let private small_zeroReduces =
        [|2; 2883584; 3080192; 196609; 3211265; 393218; 3211266; 3407875; 589828; 2818052; 3014660; 3276804; 3604484; 1114116; 2818053; 3014661; 3276805; 3604485; 1507329; 3604486; 1703937; 3538951; 1966082; 3145736; 3538952; 2031617; 3538953; 2359297; 3604490; 2686978; 3211266; 3407883; 3145729; 3604492; 3342337; 3211266; 3407876; 2818052; 3014660; 3276804; 3604484; 3604482; 3276813; 3604493; 4063234; 3211266; 3407875; 4521985; 3211278|]
let zeroReduces = Array.zeroCreate 78
for i = 0 to 77 do
        zeroReduces.[i] <- Array.zeroCreate 58
cur <- 0
while cur < small_zeroReduces.Length do
    let i = small_zeroReduces.[cur] >>> 16
    let length = small_zeroReduces.[cur] &&& 65535
    cur <- cur + 1
    for k = 0 to length-1 do
        let j = small_zeroReduces.[cur + k] >>> 16
        let x = small_zeroReduces.[cur + k] &&& 65535
        zeroReduces.[i].[j] <- lists_zeroReduces.[x]
    cur <- cur + length
let private small_acc = [1]
let private accStates = Array.zeroCreate 78
for i = 0 to 77 do
        accStates.[i] <- List.exists ((=) i) small_acc
let eofIndex = 53
let errorIndex = 7
let errorRulesExists = false
let private parserSource = new ParserSource<Token> (gotos, reduces, zeroReduces, accStates, rules, rulesStart, leftSide, startRule, eofIndex, tokenToNumber, acceptEmptyInput, numToString, errorIndex, errorRulesExists)
let buildAst : (seq<Token> -> ParseResult<Token>) =
    buildAst<Token> parserSource

let _rnglr_epsilons : Tree<Token>[] = [|null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(64, new Nodes([||])), null)), null); null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(57, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(60, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(31, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(26, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(23, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(14, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(16, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(58, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(53, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), [|new Family(56, new Nodes([|box (new AST(new Family(57, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null))|]))|])), null); new Tree<_>(null,box (new AST(new Family(44, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(42, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(39, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(37, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
let _rnglr_filtered_epsilons : Tree<Token>[] = [|null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(64, new Nodes([||])), null)), null); null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(57, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(60, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(31, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(26, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(23, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(14, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(16, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(58, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(53, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), [|new Family(56, new Nodes([|box (new AST(new Family(57, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null))|]))|])), null); new Tree<_>(null,box (new AST(new Family(44, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(42, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(39, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(37, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
for x in _rnglr_filtered_epsilons do if x <> null then x.ChooseSingleAst()
let _rnglr_extra_array, _rnglr_rule_, _rnglr_concats = 
  (Array.zeroCreate 0 : array<'_rnglr_type_a_list * '_rnglr_type_attr_list * '_rnglr_type_attr_stmt * '_rnglr_type_compass_pt * '_rnglr_type_edgeRHS * '_rnglr_type_edge_operator * '_rnglr_type_edge_stmt * '_rnglr_type_error * '_rnglr_type_graph * '_rnglr_type_id * '_rnglr_type_node_id * '_rnglr_type_node_stmt * '_rnglr_type_port * '_rnglr_type_stmt * '_rnglr_type_stmt_list * '_rnglr_type_subgraph * '_rnglr_type_yard_exp_brackets_1 * '_rnglr_type_yard_exp_brackets_2 * '_rnglr_type_yard_exp_brackets_3 * '_rnglr_type_yard_exp_brackets_4 * '_rnglr_type_yard_exp_brackets_5 * '_rnglr_type_yard_exp_brackets_6 * '_rnglr_type_yard_exp_brackets_7 * '_rnglr_type_yard_opt_1 * '_rnglr_type_yard_opt_10 * '_rnglr_type_yard_opt_11 * '_rnglr_type_yard_opt_12 * '_rnglr_type_yard_opt_13 * '_rnglr_type_yard_opt_14 * '_rnglr_type_yard_opt_15 * '_rnglr_type_yard_opt_2 * '_rnglr_type_yard_opt_3 * '_rnglr_type_yard_opt_4 * '_rnglr_type_yard_opt_5 * '_rnglr_type_yard_opt_6 * '_rnglr_type_yard_opt_7 * '_rnglr_type_yard_opt_8 * '_rnglr_type_yard_opt_9 * '_rnglr_type_yard_start_rule>), 
  [|
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_7) 
# 241 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_6) 
# 251 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_5) 
# 261 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_5) 
# 271 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_4) 
# 281 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_4) 
# 291 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_3) 
# 301 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_3) 
# 311 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_3) 
# 321 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_2) 
# 331 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_1) 
# 341 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_1) 
# 351 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with ID _rnglr_val -> [_rnglr_val] | a -> failwith "ID expected, but %A found" a )
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 43 "DotGrammar.yrd"
                         
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 43 "DotGrammar.yrd"
               : '_rnglr_type_id) 
# 371 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMPASS _rnglr_val -> [_rnglr_val] | a -> failwith "COMPASS expected, but %A found" a )
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 41 "DotGrammar.yrd"
                                      
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 41 "DotGrammar.yrd"
               : '_rnglr_type_compass_pt) 
# 391 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 39 "DotGrammar.yrd"
                           None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_14) 
# 409 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_7) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 39 "DotGrammar.yrd"
                             Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_14) 
# 429 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 15 "DotGrammar.yrd"
                                                       None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_15) 
# 447 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 15 "DotGrammar.yrd"
                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_15) 
# 467 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_14) 
             |> List.iter (fun (_) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_stmt_list) 
                 |> List.iter (fun (_) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 39 "DotGrammar.yrd"
                                                                                 
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_subgraph) 
# 493 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 37 "DotGrammar.yrd"
                     None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 37 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_13) 
# 511 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_6) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 37 "DotGrammar.yrd"
                       Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 37 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_13) 
# 531 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 37 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 541 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COL _rnglr_val -> [_rnglr_val] | a -> failwith "COL expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_compass_pt) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 37 "DotGrammar.yrd"
                                                                     
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 37 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 563 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 35 "DotGrammar.yrd"
                             None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_12) 
# 581 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_port) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 35 "DotGrammar.yrd"
                               Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_12) 
# 601 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_12) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 35 "DotGrammar.yrd"
                                         
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_node_id) 
# 623 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 21 "DotGrammar.yrd"
                                                 None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_11) 
# 641 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_attr_list) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 21 "DotGrammar.yrd"
                                                   Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_11) 
# 661 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_id) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_11) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 33 "DotGrammar.yrd"
                                                     
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 33 "DotGrammar.yrd"
               : '_rnglr_type_node_stmt) 
# 683 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 693 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 703 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 27 "DotGrammar.yrd"
                                                 None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_10) 
# 721 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edgeRHS) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 27 "DotGrammar.yrd"
                                                   Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_10) 
# 741 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_operator) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_5) 
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_10) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 29 "DotGrammar.yrd"
                                                                               
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 29 "DotGrammar.yrd"
               : '_rnglr_type_edgeRHS) 
# 765 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 21 "DotGrammar.yrd"
                                                 None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_9) 
# 783 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_attr_list) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 21 "DotGrammar.yrd"
                                                   Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_9) 
# 803 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_4) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_edgeRHS) 
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_9) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 27 "DotGrammar.yrd"
                                                                              
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_edge_stmt) 
# 827 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 23 "DotGrammar.yrd"
                                     None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_8) 
# 845 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_a_list) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 23 "DotGrammar.yrd"
                                       Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_8) 
# 865 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 25 "DotGrammar.yrd"
                                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_7) 
# 883 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMMA _rnglr_val -> [_rnglr_val] | a -> failwith "COMMA expected, but %A found" a )
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 25 "DotGrammar.yrd"
                                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_7) 
# 903 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (_) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with ASSIGN _rnglr_val -> [_rnglr_val] | a -> failwith "ASSIGN expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_id) 
                 |> List.iter (fun (_) -> 
                  ((unbox _rnglr_children.[3]) : '_rnglr_type_yard_opt_7) 
                   |> List.iter (fun (_) -> 
                    ((unbox _rnglr_children.[4]) : '_rnglr_type_yard_opt_8) 
                     |> List.iter (fun (_) -> 
                      _rnglr_cycle_res := (
                        
# 25 "DotGrammar.yrd"
                                                                    
                          )::!_rnglr_cycle_res ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_a_list) 
# 931 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 21 "DotGrammar.yrd"
                                                 None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_6) 
# 949 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_attr_list) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 21 "DotGrammar.yrd"
                                                   Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_6) 
# 969 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 23 "DotGrammar.yrd"
                                     None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_5) 
# 987 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_a_list) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 23 "DotGrammar.yrd"
                                       Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_5) 
# 1007 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with LSQBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LSQBRACE expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_5) 
               |> List.iter (fun (_) -> 
                (match ((unbox _rnglr_children.[2]) : Token) with RSQBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RSQBRACE expected, but %A found" a )
                 |> List.iter (fun (_) -> 
                  ((unbox _rnglr_children.[3]) : '_rnglr_type_yard_opt_6) 
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 23 "DotGrammar.yrd"
                                                                              
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_attr_list) 
# 1033 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_3) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 21 "DotGrammar.yrd"
                                                                
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_attr_stmt) 
# 1055 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 19 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1065 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 19 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1075 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 19 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1085 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 19 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1095 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_subgraph) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 19 "DotGrammar.yrd"
                                                                                    
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 19 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1115 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 17 "DotGrammar.yrd"
                            None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 17 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_3) 
# 1133 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_2) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 17 "DotGrammar.yrd"
                              Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 17 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_3) 
# 1153 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 15 "DotGrammar.yrd"
                                                                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_4) 
# 1171 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_stmt_list) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 15 "DotGrammar.yrd"
                                                                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_4) 
# 1191 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_3) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 17 "DotGrammar.yrd"
                                                        
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 17 "DotGrammar.yrd"
               : '_rnglr_type_stmt_list) 
# 1211 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 15 "DotGrammar.yrd"
                                                       None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_2) 
# 1229 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 15 "DotGrammar.yrd"
                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_2) 
# 1249 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 15 "DotGrammar.yrd"
                        None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_1) 
# 1267 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with STRICT _rnglr_val -> [_rnglr_val] | a -> failwith "STRICT expected, but %A found" a )
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 15 "DotGrammar.yrd"
                          Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_1) 
# 1287 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_1) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_1) 
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_2) 
                 |> List.iter (fun (_) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    ((unbox _rnglr_children.[4]) : '_rnglr_type_stmt_list) 
                     |> List.iter (fun (_) -> 
                      (match ((unbox _rnglr_children.[5]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                       |> List.iter (fun (_) -> 
                        _rnglr_cycle_res := (
                          
# 15 "DotGrammar.yrd"
                                                                                                       
                            )::!_rnglr_cycle_res ) ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_graph) 
# 1317 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          ((unbox _rnglr_children.[0]) : '_rnglr_type_graph) 
            )
# 15 "DotGrammar.yrd"
               : '_rnglr_type_yard_start_rule) 
# 1327 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              

              parserRange
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_error) 
# 1345 "DotParser.fs"
      );
  |] , [|
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_a_list)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_attr_list)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_attr_stmt)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_compass_pt)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edgeRHS)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_operator)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_stmt)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_error)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_graph)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_id)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_node_id)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_node_stmt)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_port)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_stmt)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_stmt_list)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_subgraph)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_2)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_3)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_4)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_5)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_6)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_7)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_1)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_10)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_11)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_12)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_13)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_14)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_15)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_2)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_3)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_4)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_5)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_6)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_7)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_8)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_9)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_start_rule)   ) |> List.concat));
  |] 
let translate (args : TranslateArguments<_,_>) (tree : Tree<_>) (dict : _ ) : '_rnglr_type_yard_start_rule = 
  unbox (tree.Translate _rnglr_rule_  leftSide _rnglr_concats (if args.filterEpsilons then _rnglr_filtered_epsilons else _rnglr_epsilons) args.tokenToRange args.zeroPosition args.clearAST dict) : '_rnglr_type_yard_start_rule
