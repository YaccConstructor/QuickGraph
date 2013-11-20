
# 2 "DotParser.fs"
module DotParserProject.DotParser
#nowarn "64";; // From fsyacc: turn off warnings that type variables used in production annotations are instantiated to concrete type
open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR
open Yard.Generators.RNGLR.AST

# 1 "DotGrammar.yrd"

let vrt = new ResizeArray<string>()

# 13 "DotParser.fs"
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
    | 15 -> "yard_exp_brackets_454"
    | 16 -> "yard_exp_brackets_455"
    | 17 -> "yard_exp_brackets_456"
    | 18 -> "yard_exp_brackets_457"
    | 19 -> "yard_exp_brackets_458"
    | 20 -> "yard_exp_brackets_459"
    | 21 -> "yard_exp_brackets_460"
    | 22 -> "yard_exp_brackets_461"
    | 23 -> "yard_exp_brackets_462"
    | 24 -> "yard_exp_brackets_463"
    | 25 -> "yard_many_119"
    | 26 -> "yard_many_120"
    | 27 -> "yard_many_121"
    | 28 -> "yard_many_122"
    | 29 -> "yard_opt_195"
    | 30 -> "yard_opt_196"
    | 31 -> "yard_opt_197"
    | 32 -> "yard_opt_198"
    | 33 -> "yard_opt_199"
    | 34 -> "yard_opt_200"
    | 35 -> "yard_rule_450"
    | 36 -> "yard_rule_452"
    | 37 -> "yard_rule_list_451"
    | 38 -> "yard_rule_list_453"
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
let leftSide = [|24; 23; 22; 22; 21; 20; 19; 18; 18; 18; 17; 16; 15; 15; 9; 3; 33; 33; 34; 34; 14; 32; 32; 12; 31; 31; 10; 11; 4; 4; 36; 36; 28; 28; 38; 38; 5; 35; 27; 27; 37; 37; 0; 26; 26; 1; 2; 7; 7; 7; 7; 7; 25; 25; 13; 30; 30; 29; 29; 8; 39|]
let private rules = [|57; 34; 41; 3; 41; 9; 32; 41; 3; 4; 36; 42; 35; 50; 0; 54; 47; 51; 45; 7; 55; 56; 47; 44; 48; 48; 24; 9; 33; 49; 13; 52; 23; 22; 12; 9; 31; 10; 1; 46; 43; 10; 14; 21; 28; 36; 28; 38; 1; 9; 40; 9; 20; 27; 35; 27; 37; 19; 26; 26; 18; 1; 11; 5; 2; 9; 40; 9; 14; 17; 25; 25; 9; 16; 29; 15; 30; 49; 13; 52; 55; 8|]
let private rulesStart = [|0; 2; 4; 7; 9; 11; 13; 16; 17; 18; 19; 21; 22; 23; 24; 25; 26; 26; 27; 27; 28; 32; 32; 33; 34; 34; 35; 37; 39; 40; 41; 42; 43; 43; 45; 45; 47; 49; 52; 52; 54; 54; 56; 57; 57; 59; 60; 62; 63; 64; 65; 68; 69; 69; 71; 72; 72; 73; 73; 74; 81; 82|]
let startRule = 60

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 3; 82; 4; 80; 81; 5; 6; 18; 7; 8; 9; 10; 12; 28; 46; 47; 50; 51; 52; 54; 60; 56; 61; 75; 77; 78; 79; 67; 11; 13; 14; 15; 16; 19; 17; 20; 21; 27; 22; 23; 24; 25; 26; 29; 30; 45; 32; 31; 33; 35; 38; 44; 34; 36; 37; 39; 43; 41; 40; 42; 48; 49; 55; 53; 57; 58; 59; 62; 70; 74; 72; 73; 63; 64; 65; 66; 68; 69; 71; 76|]
let private small_gotos =
        [|4; 524288; 1048577; 1900546; 3670019; 196611; 983044; 2883589; 3080198; 262147; 589831; 1966088; 3145737; 393217; 3211274; 458772; 131083; 327692; 458765; 589838; 655375; 720912; 851985; 917522; 1114131; 1179668; 1572885; 1638422; 2162711; 2359320; 2490393; 2949146; 3080219; 3145737; 3342364; 3735581; 655361; 3604510; 786437; 786463; 1441824; 2031649; 2621474; 2687011; 1048578; 589860; 3145737; 1245187; 196645; 589862; 3145767; 1376259; 1507368; 2097193; 2687018; 1572866; 196651; 3145772; 1835012; 65581; 1245230; 1703983; 3276848; 1966083; 1245230; 1703985; 3276848; 2097157; 50; 589875; 2293812; 2424885; 3145737; 2162689; 3538998; 2293761; 2621495; 2359298; 589880; 3145737; 2490371; 1310777; 1769530; 2752571; 2555907; 1310777; 1769532; 2752571; 2686979; 589875; 2293821; 3145737; 3080193; 3407934; 3145729; 3604543; 3342355; 131083; 327692; 458765; 589838; 655375; 720912; 917522; 1114131; 1179668; 1572885; 1638464; 2162711; 2359320; 2490393; 2949146; 3080219; 3145737; 3342364; 3735581; 3407876; 65601; 1245230; 1703983; 3276848; 3670017; 3211330; 3735572; 131083; 327692; 458765; 589838; 655375; 720912; 852035; 917522; 1114131; 1179668; 1572885; 1638422; 2162711; 2359320; 2490393; 2949146; 3080219; 3145737; 3342364; 3735581; 3801089; 3407940; 3997701; 262213; 1376326; 1835079; 2818120; 3014729; 4063240; 589898; 655435; 917580; 1572885; 2162711; 2359373; 3145737; 3735581; 4128772; 786463; 1441824; 2031649; 2687011; 4390915; 589902; 2228303; 3145737; 4587525; 262213; 1376326; 1835088; 2818120; 3014729; 4915204; 65617; 1245230; 1703983; 3276848|]
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
let private lists_reduces = [|[|58,1|]; [|56,1|]; [|49,1|]; [|48,1|]; [|10,2|]; [|26,1|]; [|25,1|]; [|23,1|]; [|26,2|]; [|50,3|]; [|14,1|]; [|3,2|]; [|2,2|]; [|22,1|]; [|2,3|]; [|1,2|]; [|15,1|]; [|15,1; 14,1|]; [|30,1|]; [|30,1; 27,1|]; [|27,2|]; [|44,1|]; [|44,2|]; [|6,3|]; [|37,3|]; [|41,1|]; [|39,1|]; [|39,2|]; [|5,2|]; [|41,2|]; [|42,1|]; [|45,1|]; [|47,1|]; [|59,7|]; [|31,1|]; [|51,1; 31,1|]; [|53,1|]; [|46,1|]; [|46,2|]; [|17,1|]; [|53,2|]; [|20,4|]; [|54,1|]; [|35,1|]; [|4,2|]; [|0,1|]; [|19,1|]; [|0,2|]; [|33,1|]; [|33,2|]; [|29,1|]; [|28,1|]; [|35,2|]; [|36,1|]; [|36,2|]; [|9,1|]; [|7,1|]; [|8,1|]; [|13,1|]; [|12,1|]; [|11,1|]|]
let private small_reduces =
        [|131074; 2883584; 3080192; 327681; 3211265; 524289; 3604482; 589825; 3604483; 720905; 2949124; 3080196; 3145732; 3211268; 3276804; 3342340; 3407876; 3604484; 3735556; 786436; 2818053; 3014661; 3276805; 3604485; 851972; 2818054; 3014662; 3276806; 3604486; 917508; 2818055; 3014663; 3276807; 3604487; 983044; 2818056; 3014664; 3276808; 3604488; 1114113; 3604489; 1179657; 2621450; 2686986; 2752522; 2818058; 3014666; 3211274; 3276810; 3538954; 3604490; 1310724; 2818059; 3014667; 3276811; 3604491; 1376260; 2818060; 3014668; 3276812; 3604492; 1441796; 2818061; 3014669; 3276813; 3604493; 1507332; 2818062; 3014670; 3276814; 3604494; 1638404; 2818063; 3014671; 3276815; 3604495; 1703940; 2818064; 3014672; 3276816; 3604496; 1769477; 2686986; 2818065; 3014673; 3276817; 3604497; 1835012; 2818066; 3014674; 3276818; 3604499; 1900545; 3604500; 1966081; 3604501; 2031617; 3604502; 2228226; 3276823; 3604503; 2424834; 2752536; 3538968; 2490369; 3538969; 2555905; 3538970; 2621441; 3538971; 2752514; 2752540; 3538972; 2818049; 3538973; 2883585; 3538974; 2949121; 3604511; 3014657; 3604512; 3211265; 3473441; 3276804; 2818082; 3014690; 3276834; 3604515; 3342337; 3407908; 3407873; 3604517; 3473409; 3604518; 3538945; 3211303; 3604481; 3407912; 3866628; 2818089; 3014697; 3276841; 3604521; 3932161; 3407914; 3997698; 3276843; 3604523; 4128772; 2818053; 3014661; 3276805; 3604485; 4194308; 2818066; 3014674; 3276818; 3604498; 4259844; 2818082; 3014690; 3276834; 3604514; 4325380; 2818092; 3014700; 3276844; 3604524; 4390913; 3211309; 4456449; 3211310; 4521985; 3211311; 4587522; 3276848; 3604528; 4653058; 3276849; 3604529; 4718595; 3145778; 3211314; 3735602; 4784131; 3145779; 3211315; 3735603; 4849666; 3276852; 3604532; 4915201; 3604533; 4980737; 3604534; 5046274; 3276855; 3604535; 5111810; 3276856; 3604536; 5177346; 3276857; 3604537; 5242882; 3145786; 3211322; 5308418; 3145787; 3211323; 5373954; 2883644; 3080252|]
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
let private lists_zeroReduces = [|[|57|]; [|55|]; [|16|]; [|34|]; [|54; 52|]; [|48; 36; 34|]; [|24|]; [|21|]; [|45; 43|]; [|43|]; [|42; 40|]; [|38|]; [|52|]; [|32|]; [|18|]|]
let private small_zeroReduces =
        [|2; 2883584; 3080192; 262145; 3211265; 458756; 3211266; 3276803; 3407876; 3604485; 786436; 2818054; 3014662; 3276806; 3604486; 1376260; 2818055; 3014663; 3276807; 3604487; 1835009; 3604488; 1966081; 3604489; 2097153; 3538954; 2490369; 3538955; 2555905; 3538955; 3342340; 3211266; 3276803; 3407884; 3604485; 3407873; 3604488; 3735556; 3211266; 3276803; 3407876; 3604485; 3997698; 3276813; 3604493; 4063233; 3211266; 4128772; 2818054; 3014662; 3276806; 3604486; 4390913; 3211278; 4587522; 3276813; 3604493; 4915201; 3604488|]
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

let _rnglr_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(42, new Nodes([|box (new AST(new Family(40, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(61, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(48, new Nodes([|box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([|box (new AST(new Family(52, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(52, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(43, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(38, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(32, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(24, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(21, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(16, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(18, new Nodes([||])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
let _rnglr_filtered_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(42, new Nodes([|box (new AST(new Family(40, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(61, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(48, new Nodes([|box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([|box (new AST(new Family(52, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(52, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(43, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(38, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(32, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(24, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(21, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(16, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(18, new Nodes([||])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
for x in _rnglr_filtered_epsilons do if x <> null then x.ChooseSingleAst()
let _rnglr_extra_array, _rnglr_rule_, _rnglr_concats = 
  (Array.zeroCreate 0 : array<'_rnglr_type_a_list * '_rnglr_type_attr_list * '_rnglr_type_attr_stmt * '_rnglr_type_compass_pt * '_rnglr_type_edge_operator * '_rnglr_type_edge_stmt * '_rnglr_type_error * '_rnglr_type_full_stmt * '_rnglr_type_graph * '_rnglr_type_id * '_rnglr_type_node_id * '_rnglr_type_node_stmt * '_rnglr_type_port * '_rnglr_type_stmt_list * '_rnglr_type_subgraph * '_rnglr_type_yard_exp_brackets_454 * '_rnglr_type_yard_exp_brackets_455 * '_rnglr_type_yard_exp_brackets_456 * '_rnglr_type_yard_exp_brackets_457 * '_rnglr_type_yard_exp_brackets_458 * '_rnglr_type_yard_exp_brackets_459 * '_rnglr_type_yard_exp_brackets_460 * '_rnglr_type_yard_exp_brackets_461 * '_rnglr_type_yard_exp_brackets_462 * '_rnglr_type_yard_exp_brackets_463 * '_rnglr_type_yard_many_119 * '_rnglr_type_yard_many_120 * '_rnglr_type_yard_many_121 * '_rnglr_type_yard_many_122 * '_rnglr_type_yard_opt_195 * '_rnglr_type_yard_opt_196 * '_rnglr_type_yard_opt_197 * '_rnglr_type_yard_opt_198 * '_rnglr_type_yard_opt_199 * '_rnglr_type_yard_opt_200 * '_rnglr_type_yard_rule_450 * '_rnglr_type_yard_rule_452 * '_rnglr_type_yard_rule_list_451 * '_rnglr_type_yard_rule_list_453 * '_rnglr_type_yard_start_rule>), 
  [|
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SUBGR _rnglr_val -> [_rnglr_val] | a -> failwith "SUBGR expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_200) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 54 "DotGrammar.yrd"
                                             1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_463) 
# 255 "DotParser.fs"
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
                  
# 52 "DotGrammar.yrd"
                                                   1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_462) 
# 277 "DotParser.fs"
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
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_198) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 52 "DotGrammar.yrd"
                                                          1
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_461) 
# 301 "DotParser.fs"
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
                  
# 52 "DotGrammar.yrd"
                                                   1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_461) 
# 323 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_operator) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_452) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 23 "DotGrammar.yrd"
                                                    i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_460) 
# 345 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMMA _rnglr_val -> [_rnglr_val] | a -> failwith "COMMA expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_450) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 23 "DotGrammar.yrd"
                                                    i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_459) 
# 367 "DotParser.fs"
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
                    
# 40 "DotGrammar.yrd"
                                                               l
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_458) 
# 391 "DotParser.fs"
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
                
# 27 "DotGrammar.yrd"
                                                1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_457) 
# 411 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_457) 
# 421 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_457) 
# 431 "DotParser.fs"
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
                  
# 29 "DotGrammar.yrd"
                                                    l
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_456) 
# 453 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with STRICT _rnglr_val -> [_rnglr_val] | a -> failwith "STRICT expected, but %A found" a )
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 27 "DotGrammar.yrd"
                                  1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_455) 
# 473 "DotParser.fs"
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
                
# 27 "DotGrammar.yrd"
                                                1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_454) 
# 493 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with DIGRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "DIGRAPH expected, but %A found" a )
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 27 "DotGrammar.yrd"
                                                              1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_454) 
# 513 "DotParser.fs"
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
                
# 56 "DotGrammar.yrd"
                                    i 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_id) 
# 533 "DotParser.fs"
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
                
# 56 "DotGrammar.yrd"
                                    i 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_compass_pt) 
# 553 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
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
               : '_rnglr_type_yard_opt_199) 
# 571 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_463) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 54 "DotGrammar.yrd"
                             Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_199) 
# 591 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_200) 
# 609 "DotParser.fs"
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
                
# 27 "DotGrammar.yrd"
                                                                    Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_200) 
# 629 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_199) 
             |> List.iter (fun (_) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_stmt_list) 
                 |> List.iter (fun (l) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 54 "DotGrammar.yrd"
                                                                                      l
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_subgraph) 
# 655 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 52 "DotGrammar.yrd"
                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_198) 
# 673 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_462) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 52 "DotGrammar.yrd"
                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_198) 
# 693 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_461) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 52 "DotGrammar.yrd"
                                                                                1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 713 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeId ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 50 "DotGrammar.yrd"
                                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_197) 
# 731 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeId ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_port) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 50 "DotGrammar.yrd"
                                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_197) 
# 751 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (nodeId) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_197) nodeId
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 50 "DotGrammar.yrd"
                                                  nodeId 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_node_id) 
# 773 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_id) 
             |> List.iter (fun (nId) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (l) -> 
                _rnglr_cycle_res := (
                  
# 48 "DotGrammar.yrd"
                                                       nId::l
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_node_stmt) 
# 795 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with EDGEOP _rnglr_val -> [_rnglr_val] | a -> failwith "EDGEOP expected, but %A found" a )
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 46 "DotGrammar.yrd"
                                        1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 815 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with DIEDGEOP _rnglr_val -> [_rnglr_val] | a -> failwith "DIEDGEOP expected, but %A found" a )
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 46 "DotGrammar.yrd"
                                                       1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 835 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_id) 
             |> List.iter (fun (nId) -> 
              _rnglr_cycle_res := (
                
# 44 "DotGrammar.yrd"
                                                     [nId]
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_452) 
# 855 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_452) 
# 865 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
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
               : '_rnglr_type_yard_many_122) 
# 883 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_460) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_122) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 44 "DotGrammar.yrd"
                                                                           yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_122) 
# 905 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 22 "DotGrammar.yrd"
                    [] 
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_453) 
# 923 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_452) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_122) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 23 "DotGrammar.yrd"
                                                          hd::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_453) 
# 945 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_453) 
             |> List.iter (fun (lst) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (l2) -> 
                _rnglr_cycle_res := (
                  
# 44 "DotGrammar.yrd"
                                                                                                        (List.concat lst)@l2
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_edge_stmt) 
# 967 "DotParser.fs"
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
                    
# 42 "DotGrammar.yrd"
                                                           (k + ":" + v)
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_450) 
# 991 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 42 "DotGrammar.yrd"
                                                                     []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_121) 
# 1009 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_459) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_121) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 42 "DotGrammar.yrd"
                                                                         yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_121) 
# 1031 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 22 "DotGrammar.yrd"
                    [] 
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_451) 
# 1049 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_450) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_121) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 23 "DotGrammar.yrd"
                                                          hd::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 21 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_451) 
# 1071 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_451) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 42 "DotGrammar.yrd"
                                                                               lst
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_a_list) 
# 1091 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 40 "DotGrammar.yrd"
                             []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_120) 
# 1109 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_458) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_120) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 40 "DotGrammar.yrd"
                                 yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_120) 
# 1131 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_120) 
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 40 "DotGrammar.yrd"
                                                                 List.concat l
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_attr_list) 
# 1151 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_457) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 38 "DotGrammar.yrd"
                                                                       lst
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_attr_stmt) 
# 1173 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1183 "DotParser.fs"
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
                
# 33 "DotGrammar.yrd"
                                      l
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1203 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1213 "DotParser.fs"
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
                    
# 35 "DotGrammar.yrd"
                                          [k + ":" + v]
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1237 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1247 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 29 "DotGrammar.yrd"
                                 []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 29 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_119) 
# 1265 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_456) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_119) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 29 "DotGrammar.yrd"
                                     yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 29 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_119) 
# 1287 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_119) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 29 "DotGrammar.yrd"
                                                        List.concat lst 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 29 "DotGrammar.yrd"
               : '_rnglr_type_stmt_list) 
# 1307 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_196) 
# 1325 "DotParser.fs"
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
                
# 27 "DotGrammar.yrd"
                                                                    Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_196) 
# 1345 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_195) 
# 1363 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_455) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 27 "DotGrammar.yrd"
                          Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_195) 
# 1383 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_195) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_454) 
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_196) 
                 |> List.iter (fun (_) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    ((unbox _rnglr_children.[4]) : '_rnglr_type_stmt_list) 
                     |> List.iter (fun (x) -> 
                      (match ((unbox _rnglr_children.[5]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                       |> List.iter (fun (_) -> 
                        (match ((unbox _rnglr_children.[6]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
                         |> List.iter (fun (_) -> 
                          _rnglr_cycle_res := (
                            
# 27 "DotGrammar.yrd"
                                                                                                                          x
                              )::!_rnglr_cycle_res ) ) ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_graph) 
# 1415 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          ((unbox _rnglr_children.[0]) : '_rnglr_type_graph) 
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_yard_start_rule) 
# 1425 "DotParser.fs"
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
# 1443 "DotParser.fs"
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
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_454)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_455)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_456)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_457)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_458)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_459)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_460)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_461)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_462)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_463)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_119)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_120)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_121)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_122)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_195)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_196)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun nodeId ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_197)  nodeId ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_198)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_199)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_200)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_450)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_452)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_451)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_453)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_start_rule)   ) |> List.concat));
  |] 
let translate (args : TranslateArguments<_,_>) (tree : Tree<_>) (dict : _ ) : '_rnglr_type_yard_start_rule = 
  unbox (tree.Translate _rnglr_rule_  leftSide _rnglr_concats (if args.filterEpsilons then _rnglr_filtered_epsilons else _rnglr_epsilons) args.tokenToRange args.zeroPosition args.clearAST dict) : '_rnglr_type_yard_start_rule
