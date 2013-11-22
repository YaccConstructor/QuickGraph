
# 2 "DotParser.fs"
module DotParserProject.DotParser
#nowarn "64";; // From fsyacc: turn off warnings that type variables used in production annotations are instantiated to concrete type
open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR
open Yard.Generators.RNGLR.AST

# 1 "DotGrammar.yrd"

open System.Collections.Generic
open DotParserProject.CollectDataFuncs

let adj_list = new Dictionary<string, Dictionary<string, int>>()
let graph_info = new Dictionary<string, string>()

//constants
let type_key = "type"
let strict_key = "is_strict"
let name_key = "name"

# 22 "DotParser.fs"
type Token =
    | ASSIGN of (string)
    | COL of (string)
    | COMMA of (string)
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
    | 4 -> "edge_operator"
    | 5 -> "edge_stmt"
    | 6 -> "error"
    | 7 -> "full_stmt"
    | 8 -> "graph"
    | 9 -> "id"
    | 10 -> "node_id"
    | 11 -> "node_stmt"
    | 12 -> "port"
    | 13 -> "stmt_list"
    | 14 -> "subgraph"
    | 15 -> "yard_exp_brackets_1327"
    | 16 -> "yard_exp_brackets_1328"
    | 17 -> "yard_exp_brackets_1329"
    | 18 -> "yard_exp_brackets_1330"
    | 19 -> "yard_exp_brackets_1331"
    | 20 -> "yard_exp_brackets_1332"
    | 21 -> "yard_exp_brackets_1333"
    | 22 -> "yard_exp_brackets_1334"
    | 23 -> "yard_exp_brackets_1335"
    | 24 -> "yard_exp_brackets_1336"
    | 25 -> "yard_many_385"
    | 26 -> "yard_many_386"
    | 27 -> "yard_many_387"
    | 28 -> "yard_many_388"
    | 29 -> "yard_opt_577"
    | 30 -> "yard_opt_578"
    | 31 -> "yard_opt_579"
    | 32 -> "yard_opt_580"
    | 33 -> "yard_opt_581"
    | 34 -> "yard_opt_582"
    | 35 -> "yard_rule_1323"
    | 36 -> "yard_rule_1325"
    | 37 -> "yard_rule_list_1324"
    | 38 -> "yard_rule_list_1326"
    | 39 -> "yard_start_rule"
    | 40 -> "ASSIGN"
    | 41 -> "COL"
    | 42 -> "COMMA"
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
    | ASSIGN _ -> 40
    | COL _ -> 41
    | COMMA _ -> 42
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
let leftSide = [|24; 23; 22; 22; 21; 21; 20; 19; 18; 17; 17; 17; 16; 15; 15; 9; 3; 33; 33; 34; 34; 14; 32; 32; 12; 31; 31; 10; 11; 4; 36; 36; 28; 28; 38; 38; 5; 35; 27; 27; 37; 37; 0; 26; 26; 1; 2; 7; 7; 7; 7; 7; 25; 25; 13; 30; 30; 29; 29; 8; 39|]
let private rules = [|57; 34; 41; 3; 41; 9; 32; 41; 3; 46; 43; 4; 36; 42; 35; 50; 0; 54; 47; 51; 45; 7; 55; 47; 44; 48; 48; 24; 9; 33; 49; 13; 52; 23; 22; 12; 9; 31; 10; 1; 21; 10; 14; 20; 28; 36; 28; 38; 1; 9; 40; 9; 19; 27; 35; 27; 37; 18; 26; 26; 17; 1; 11; 5; 2; 9; 40; 9; 14; 16; 25; 25; 9; 56; 29; 15; 30; 49; 13; 52; 55; 8|]
let private rulesStart = [|0; 2; 4; 7; 9; 10; 11; 13; 15; 18; 19; 20; 21; 23; 24; 25; 26; 27; 27; 28; 28; 29; 33; 33; 34; 35; 35; 36; 38; 40; 41; 42; 43; 43; 45; 45; 47; 49; 52; 52; 54; 54; 56; 57; 57; 59; 60; 62; 63; 64; 65; 68; 69; 69; 71; 72; 72; 73; 73; 74; 81; 82|]
let startRule = 60

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 82; 3; 80; 81; 4; 5; 17; 6; 7; 8; 9; 11; 27; 45; 46; 49; 50; 51; 53; 59; 55; 60; 75; 77; 78; 79; 66; 10; 12; 13; 14; 15; 18; 16; 19; 20; 26; 21; 22; 23; 24; 25; 28; 29; 44; 31; 30; 32; 34; 37; 43; 33; 35; 36; 38; 42; 40; 39; 41; 47; 48; 54; 52; 56; 57; 58; 61; 69; 70; 74; 72; 73; 62; 63; 64; 65; 67; 68; 71; 76|]
let private small_gotos =
        [|3; 524288; 1900545; 3670018; 131075; 983043; 2883588; 3080197; 196611; 589830; 1966087; 3145736; 327681; 3211273; 393236; 131082; 327691; 458764; 589837; 655374; 720911; 851984; 917521; 1048594; 1114131; 1572884; 1638421; 2162710; 2359319; 2490392; 2949145; 3080218; 3145736; 3342363; 3735580; 589825; 3604509; 720901; 786462; 1441823; 2031648; 2621473; 2687010; 983042; 589859; 3145736; 1179651; 196644; 589861; 3145766; 1310723; 1507367; 2097192; 2687017; 1507330; 196650; 3145771; 1769476; 65580; 1179693; 1703982; 3276847; 1900547; 1179693; 1703984; 3276847; 2031621; 49; 589874; 2293811; 2424884; 3145736; 2097153; 3538997; 2228225; 2621494; 2293762; 589879; 3145736; 2424835; 1245240; 1769529; 2752570; 2490371; 1245240; 1769531; 2752570; 2621443; 589874; 2293820; 3145736; 3014657; 3407933; 3080193; 3604542; 3276819; 131082; 327691; 458764; 589837; 655374; 720911; 917521; 1048594; 1114131; 1572884; 1638463; 2162710; 2359319; 2490392; 2949145; 3080218; 3145736; 3342363; 3735580; 3342340; 65600; 1179693; 1703982; 3276847; 3604481; 3211329; 3670036; 131082; 327691; 458764; 589837; 655374; 720911; 852034; 917521; 1048594; 1114131; 1572884; 1638421; 2162710; 2359319; 2490392; 2949145; 3080218; 3145736; 3342363; 3735580; 3735553; 3407939; 3932166; 262212; 1310789; 1376326; 1835079; 2818120; 3014729; 3997704; 589898; 655435; 917580; 1572884; 2162710; 2359373; 3145736; 3735580; 4063236; 786462; 1441823; 2031648; 2687010; 4325379; 589902; 2228303; 3145736; 4521990; 262212; 1310789; 1376326; 1835088; 2818120; 3014729; 4915204; 65617; 1179693; 1703982; 3276847|]
let gotos = Array.zeroCreate 83
for i = 0 to 82 do
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
let private lists_reduces = [|[|56,1|]; [|49,1|]; [|48,1|]; [|12,2|]; [|27,1|]; [|26,1|]; [|24,1|]; [|27,2|]; [|50,3|]; [|15,1|]; [|3,2|]; [|2,2|]; [|23,1|]; [|2,3|]; [|1,2|]; [|16,1|]; [|16,1; 15,1|]; [|30,1|]; [|30,1; 28,1|]; [|28,2|]; [|44,1|]; [|44,2|]; [|8,3|]; [|37,3|]; [|41,1|]; [|39,1|]; [|39,2|]; [|7,2|]; [|41,2|]; [|42,1|]; [|45,1|]; [|47,1|]; [|59,7|]; [|31,1|]; [|51,1; 31,1|]; [|53,1|]; [|46,1|]; [|46,2|]; [|18,1|]; [|53,2|]; [|21,4|]; [|54,1|]; [|35,1|]; [|6,2|]; [|0,1|]; [|20,1|]; [|0,2|]; [|33,1|]; [|29,1|]; [|33,2|]; [|5,1|]; [|4,1|]; [|35,2|]; [|36,1|]; [|36,2|]; [|11,1|]; [|9,1|]; [|10,1|]; [|14,1|]; [|13,1|]; [|58,1|]|]
let private small_reduces =
        [|262145; 3211264; 458753; 3604481; 524289; 3604482; 655369; 2949123; 3080195; 3145731; 3211267; 3276803; 3342339; 3407875; 3604483; 3735555; 720900; 2818052; 3014660; 3276804; 3604484; 786436; 2818053; 3014661; 3276805; 3604485; 851972; 2818054; 3014662; 3276806; 3604486; 917508; 2818055; 3014663; 3276807; 3604487; 1048577; 3604488; 1114121; 2621449; 2686985; 2752521; 2818057; 3014665; 3211273; 3276809; 3538953; 3604489; 1245188; 2818058; 3014666; 3276810; 3604490; 1310724; 2818059; 3014667; 3276811; 3604491; 1376260; 2818060; 3014668; 3276812; 3604492; 1441796; 2818061; 3014669; 3276813; 3604493; 1572868; 2818062; 3014670; 3276814; 3604494; 1638404; 2818063; 3014671; 3276815; 3604495; 1703941; 2686985; 2818064; 3014672; 3276816; 3604496; 1769476; 2818065; 3014673; 3276817; 3604498; 1835009; 3604499; 1900545; 3604500; 1966081; 3604501; 2162690; 3276822; 3604502; 2359298; 2752535; 3538967; 2424833; 3538968; 2490369; 3538969; 2555905; 3538970; 2686978; 2752539; 3538971; 2752513; 3538972; 2818049; 3538973; 2883585; 3604510; 2949121; 3604511; 3145729; 3473440; 3211268; 2818081; 3014689; 3276833; 3604514; 3276801; 3407907; 3342337; 3604516; 3407873; 3604517; 3473409; 3211302; 3538945; 3407911; 3801092; 2818088; 3014696; 3276840; 3604520; 3866625; 3407913; 3932162; 3276842; 3604522; 4063236; 2818052; 3014660; 3276804; 3604484; 4128772; 2818065; 3014673; 3276817; 3604497; 4194308; 2818081; 3014689; 3276833; 3604513; 4259844; 2818091; 3014699; 3276843; 3604523; 4325377; 3211308; 4390913; 3211309; 4456449; 3211310; 4521986; 3276847; 3604527; 4587523; 3145776; 3211312; 3735600; 4653058; 3276849; 3604529; 4718595; 3145778; 3211314; 3735602; 4784131; 3145779; 3211315; 3735603; 4849666; 3276852; 3604532; 4915201; 3604533; 4980737; 3604534; 5046274; 3276855; 3604535; 5111810; 3276856; 3604536; 5177346; 3276857; 3604537; 5242882; 3145786; 3211322; 5308418; 3145787; 3211323; 5373954; 2883644; 3080252|]
let reduces = Array.zeroCreate 83
for i = 0 to 82 do
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
let private lists_zeroReduces = [|[|57|]; [|55|]; [|17|]; [|34|]; [|54; 52|]; [|48; 36; 34|]; [|25|]; [|22|]; [|45; 43|]; [|43|]; [|42; 40|]; [|38|]; [|52|]; [|32|]; [|19|]|]
let private small_zeroReduces =
        [|2; 2883584; 3080192; 196609; 3211265; 393220; 3211266; 3276803; 3407876; 3604485; 720900; 2818054; 3014662; 3276806; 3604486; 1310724; 2818055; 3014663; 3276807; 3604487; 1769473; 3604488; 1900545; 3604489; 2031617; 3538954; 2424833; 3538955; 2490369; 3538955; 3276804; 3211266; 3276803; 3407884; 3604485; 3342337; 3604488; 3670020; 3211266; 3276803; 3407876; 3604485; 3932162; 3276813; 3604493; 3997697; 3211266; 4063236; 2818054; 3014662; 3276806; 3604486; 4325377; 3211278; 4521986; 3276813; 3604493; 4915201; 3604488|]
let zeroReduces = Array.zeroCreate 83
for i = 0 to 82 do
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
let private accStates = Array.zeroCreate 83
for i = 0 to 82 do
        accStates.[i] <- List.exists ((=) i) small_acc
let eofIndex = 53
let errorIndex = 6
let errorRulesExists = false
let private parserSource = new ParserSource<Token> (gotos, reduces, zeroReduces, accStates, rules, rulesStart, leftSide, startRule, eofIndex, tokenToNumber, acceptEmptyInput, numToString, errorIndex, errorRulesExists)
let buildAst : (seq<Token> -> ParseResult<Token>) =
    buildAst<Token> parserSource

let _rnglr_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(42, new Nodes([|box (new AST(new Family(40, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(61, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(48, new Nodes([|box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([|box (new AST(new Family(52, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(52, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(43, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(38, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(32, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
let _rnglr_filtered_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(42, new Nodes([|box (new AST(new Family(40, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(61, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(48, new Nodes([|box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([|box (new AST(new Family(52, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(52, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(43, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(38, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(32, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
for x in _rnglr_filtered_epsilons do if x <> null then x.ChooseSingleAst()
let _rnglr_extra_array, _rnglr_rule_, _rnglr_concats = 
  (Array.zeroCreate 0 : array<'_rnglr_type_a_list * '_rnglr_type_attr_list * '_rnglr_type_attr_stmt * '_rnglr_type_compass_pt * '_rnglr_type_edge_operator * '_rnglr_type_edge_stmt * '_rnglr_type_error * '_rnglr_type_full_stmt * '_rnglr_type_graph * '_rnglr_type_id * '_rnglr_type_node_id * '_rnglr_type_node_stmt * '_rnglr_type_port * '_rnglr_type_stmt_list * '_rnglr_type_subgraph * '_rnglr_type_yard_exp_brackets_1327 * '_rnglr_type_yard_exp_brackets_1328 * '_rnglr_type_yard_exp_brackets_1329 * '_rnglr_type_yard_exp_brackets_1330 * '_rnglr_type_yard_exp_brackets_1331 * '_rnglr_type_yard_exp_brackets_1332 * '_rnglr_type_yard_exp_brackets_1333 * '_rnglr_type_yard_exp_brackets_1334 * '_rnglr_type_yard_exp_brackets_1335 * '_rnglr_type_yard_exp_brackets_1336 * '_rnglr_type_yard_many_385 * '_rnglr_type_yard_many_386 * '_rnglr_type_yard_many_387 * '_rnglr_type_yard_many_388 * '_rnglr_type_yard_opt_577 * '_rnglr_type_yard_opt_578 * '_rnglr_type_yard_opt_579 * '_rnglr_type_yard_opt_580 * '_rnglr_type_yard_opt_581 * '_rnglr_type_yard_opt_582 * '_rnglr_type_yard_rule_1323 * '_rnglr_type_yard_rule_1325 * '_rnglr_type_yard_rule_list_1324 * '_rnglr_type_yard_rule_list_1326 * '_rnglr_type_yard_start_rule>), 
  [|
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SUBGR _rnglr_val -> [_rnglr_val] | a -> failwith "SUBGR expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_582) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 58 "DotGrammar.yrd"
                                             1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1336) 
# 264 "DotParser.fs"
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
                  
# 56 "DotGrammar.yrd"
                                                   1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1335) 
# 286 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COL _rnglr_val -> [_rnglr_val] | a -> failwith "COL expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_id) 
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_580) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 56 "DotGrammar.yrd"
                                                          1
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1334) 
# 310 "DotParser.fs"
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
                  
# 56 "DotGrammar.yrd"
                                                   1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1334) 
# 332 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with EDGEOP _rnglr_val -> [_rnglr_val] | a -> failwith "EDGEOP expected, but %A found" a )
             |> List.iter (fun (p) -> 
              _rnglr_cycle_res := (
                
# 50 "DotGrammar.yrd"
                                              p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1333) 
# 352 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with DIEDGEOP _rnglr_val -> [_rnglr_val] | a -> failwith "DIEDGEOP expected, but %A found" a )
             |> List.iter (fun (p) -> 
              _rnglr_cycle_res := (
                
# 50 "DotGrammar.yrd"
                                                               p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1333) 
# 372 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_operator) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_1325) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 27 "DotGrammar.yrd"
                                                                              i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1332) 
# 394 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMMA _rnglr_val -> [_rnglr_val] | a -> failwith "COMMA expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_1323) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 27 "DotGrammar.yrd"
                                                                              i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1331) 
# 416 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with LSQBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LSQBRACE expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_a_list) 
               |> List.iter (fun (l) -> 
                (match ((unbox _rnglr_children.[2]) : Token) with RSQBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RSQBRACE expected, but %A found" a )
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 44 "DotGrammar.yrd"
                                                               
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1330) 
# 440 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with GRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "GRAPH expected, but %A found" a )
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 42 "DotGrammar.yrd"
                                    1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1329) 
# 460 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_1329) 
# 470 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_1329) 
# 480 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_full_stmt) 
             |> List.iter (fun (l) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 33 "DotGrammar.yrd"
                                                    l
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1328) 
# 502 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with GRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "GRAPH expected, but %A found" a )
             |> List.iter (fun (t) -> 
              _rnglr_cycle_res := (
                
# 30 "DotGrammar.yrd"
                                               t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1327) 
# 522 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with DIGRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "DIGRAPH expected, but %A found" a )
             |> List.iter (fun (t) -> 
              _rnglr_cycle_res := (
                
# 30 "DotGrammar.yrd"
                                                               t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_1327) 
# 542 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with ID _rnglr_val -> [_rnglr_val] | a -> failwith "ID expected, but %A found" a )
             |> List.iter (fun (i) -> 
              _rnglr_cycle_res := (
                
# 62 "DotGrammar.yrd"
                           i
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 62 "DotGrammar.yrd"
               : '_rnglr_type_id) 
# 562 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with ID _rnglr_val -> [_rnglr_val] | a -> failwith "ID expected, but %A found" a )
             |> List.iter (fun (i) -> 
              _rnglr_cycle_res := (
                
# 60 "DotGrammar.yrd"
                                   
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 60 "DotGrammar.yrd"
               : '_rnglr_type_compass_pt) 
# 582 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 58 "DotGrammar.yrd"
                           None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_581) 
# 600 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1336) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 58 "DotGrammar.yrd"
                             Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_581) 
# 620 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 30 "DotGrammar.yrd"
                                                                       None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_582) 
# 638 "DotParser.fs"
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
                
# 30 "DotGrammar.yrd"
                                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_582) 
# 658 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_581) 
             |> List.iter (fun (_) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_stmt_list) 
                 |> List.iter (fun (l) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 58 "DotGrammar.yrd"
                                                                                      
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_subgraph) 
# 684 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 56 "DotGrammar.yrd"
                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_580) 
# 702 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1335) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 56 "DotGrammar.yrd"
                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_580) 
# 722 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1334) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 56 "DotGrammar.yrd"
                                                                                1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 742 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeID ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 54 "DotGrammar.yrd"
                                    None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_579) 
# 760 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeID ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_port) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 54 "DotGrammar.yrd"
                                      Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_579) 
# 780 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (nodeID) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_579) nodeID
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 54 "DotGrammar.yrd"
                                                nodeID
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_node_id) 
# 802 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_id) 
             |> List.iter (fun (nodeID) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (l) -> 
                _rnglr_cycle_res := (
                  
# 52 "DotGrammar.yrd"
                                                          nodeID
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_node_stmt) 
# 824 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1333) 
             |> List.iter (fun (op) -> 
              _rnglr_cycle_res := (
                
# 50 "DotGrammar.yrd"
                                                                    CollectDataFuncs.AddInfo graph_info [op, ""]
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 844 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_id) 
             |> List.iter (fun (nodeID) -> 
              _rnglr_cycle_res := (
                
# 48 "DotGrammar.yrd"
                                                        nodeID
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_1325) 
# 864 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_1325) 
# 874 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 48 "DotGrammar.yrd"
                                                                          []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_388) 
# 892 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1332) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_388) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 48 "DotGrammar.yrd"
                                                                              yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_388) 
# 914 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 27 "DotGrammar.yrd"
                                      []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_1326) 
# 932 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_1325) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_388) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 27 "DotGrammar.yrd"
                                                                                    hd::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_1326) 
# 954 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_1326) 
             |> List.iter (fun (edges) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 48 "DotGrammar.yrd"
                                                                                                        CollectDataFuncs.AddEdges adj_list edges
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_edge_stmt) 
# 976 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (k) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with ASSIGN _rnglr_val -> [_rnglr_val] | a -> failwith "ASSIGN expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_id) 
                 |> List.iter (fun (v) -> 
                  _rnglr_cycle_res := (
                    
# 39 "DotGrammar.yrd"
                                          
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_1323) 
# 1000 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 46 "DotGrammar.yrd"
                                                      []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_387) 
# 1018 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1331) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_387) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 46 "DotGrammar.yrd"
                                                          yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_387) 
# 1040 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 27 "DotGrammar.yrd"
                                      []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_1324) 
# 1058 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_1323) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_387) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 27 "DotGrammar.yrd"
                                                                                    hd::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_1324) 
# 1080 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_1324) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 46 "DotGrammar.yrd"
                                                                
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_a_list) 
# 1100 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 44 "DotGrammar.yrd"
                             []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_386) 
# 1118 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1330) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_386) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 44 "DotGrammar.yrd"
                                 yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_386) 
# 1140 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_386) 
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 44 "DotGrammar.yrd"
                                                                
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_attr_list) 
# 1160 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1329) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 42 "DotGrammar.yrd"
                                                                       
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_attr_stmt) 
# 1182 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1192 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_stmt) 
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 37 "DotGrammar.yrd"
                                      l
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1212 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1222 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (k) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with ASSIGN _rnglr_val -> [_rnglr_val] | a -> failwith "ASSIGN expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_id) 
                 |> List.iter (fun (v) -> 
                  _rnglr_cycle_res := (
                    
# 39 "DotGrammar.yrd"
                                          
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1246 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1256 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 33 "DotGrammar.yrd"
                                 []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 33 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_385) 
# 1274 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_1328) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_385) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 33 "DotGrammar.yrd"
                                     yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 33 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_385) 
# 1296 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_385) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 33 "DotGrammar.yrd"
                                                        
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 33 "DotGrammar.yrd"
               : '_rnglr_type_stmt_list) 
# 1316 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 30 "DotGrammar.yrd"
                                                                       None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_578) 
# 1334 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 30 "DotGrammar.yrd"
                                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_578) 
# 1354 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 30 "DotGrammar.yrd"
                         None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_577) 
# 1372 "DotParser.fs"
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
                
# 30 "DotGrammar.yrd"
                           Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_577) 
# 1392 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_577) 
             |> List.iter (fun (s) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_1327) s
               |> List.iter (fun (g) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_578) s g
                 |> List.iter (fun (name) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    ((unbox _rnglr_children.[4]) : '_rnglr_type_stmt_list) 
                     |> List.iter (fun (x) -> 
                      (match ((unbox _rnglr_children.[5]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                       |> List.iter (fun (_) -> 
                        (match ((unbox _rnglr_children.[6]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
                         |> List.iter (fun (_) -> 
                          _rnglr_cycle_res := (
                            
# 31 "DotGrammar.yrd"
                             CollectDataFuncs.AddInfo graph_info [strict_key, (Utils.OptToStr s); type_key, g; name_key, (Utils.OptToStr name)]
                              )::!_rnglr_cycle_res ) ) ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_graph) 
# 1424 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          ((unbox _rnglr_children.[0]) : '_rnglr_type_graph) 
            )
# 30 "DotGrammar.yrd"
               : '_rnglr_type_yard_start_rule) 
# 1434 "DotParser.fs"
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
# 1452 "DotParser.fs"
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
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_operator)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_stmt)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_error)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_full_stmt)   ) |> List.concat));
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
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_stmt_list)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_subgraph)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1327)  s ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1328)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1329)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1330)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1331)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1332)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1333)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1334)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1335)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_1336)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_385)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_386)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_387)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_388)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_577)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s -> fun g ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_578)  s g ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun nodeID ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_579)  nodeID ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_580)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_581)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_582)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_1323)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_1325)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_1324)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_1326)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_start_rule)   ) |> List.concat));
  |] 
let translate (args : TranslateArguments<_,_>) (tree : Tree<_>) (dict : _ ) : '_rnglr_type_yard_start_rule = 
  unbox (tree.Translate _rnglr_rule_  leftSide _rnglr_concats (if args.filterEpsilons then _rnglr_filtered_epsilons else _rnglr_epsilons) args.tokenToRange args.zeroPosition args.clearAST dict) : '_rnglr_type_yard_start_rule
