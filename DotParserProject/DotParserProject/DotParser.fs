
# 2 "DotParser.fs"
module DotParserProject.DotParser
#nowarn "64";; // From fsyacc: turn off warnings that type variables used in production annotations are instantiated to concrete type
open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR
open Yard.Generators.RNGLR.AST

# 1 "DotGrammar.yrd"

open System.Collections.Generic
open DotParserProject.GraphDataContainer

//containers for graph's data
let vertices_lists = new ResizeArray<string list>()
let graph_info = new Dictionary<string, string>()
let vertices_attrs = new Dictionary<string, (string*string) list>()
let edges_attrs = new Dictionary<string, (string*string) list>()
let general_attrs = new Dictionary<string, (string*string) list>()
let assign_stmt_list = new ResizeArray<string*string>()

//array of GraphDataContainer
let graphs = new ResizeArray<GraphDataContainer>()
graphs.Add(new GraphDataContainer())

//constants
let type_key = "type"
let strict_key = "is_strict"
let name_key = "name"

# 31 "DotParser.fs"
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
    | 15 -> "yard_exp_brackets_216"
    | 16 -> "yard_exp_brackets_217"
    | 17 -> "yard_exp_brackets_218"
    | 18 -> "yard_exp_brackets_219"
    | 19 -> "yard_exp_brackets_220"
    | 20 -> "yard_exp_brackets_221"
    | 21 -> "yard_exp_brackets_222"
    | 22 -> "yard_exp_brackets_223"
    | 23 -> "yard_exp_brackets_224"
    | 24 -> "yard_exp_brackets_225"
    | 25 -> "yard_many_85"
    | 26 -> "yard_many_86"
    | 27 -> "yard_many_87"
    | 28 -> "yard_many_88"
    | 29 -> "yard_many_89"
    | 30 -> "yard_many_90"
    | 31 -> "yard_opt_85"
    | 32 -> "yard_opt_86"
    | 33 -> "yard_opt_87"
    | 34 -> "yard_opt_88"
    | 35 -> "yard_opt_89"
    | 36 -> "yard_opt_90"
    | 37 -> "yard_rule_211"
    | 38 -> "yard_rule_213"
    | 39 -> "yard_rule_214"
    | 40 -> "yard_rule_list_212"
    | 41 -> "yard_rule_list_215"
    | 42 -> "yard_start_rule"
    | 43 -> "ASSIGN"
    | 44 -> "COL"
    | 45 -> "COMMA"
    | 46 -> "DIEDGEOP"
    | 47 -> "DIGRAPH"
    | 48 -> "EDGE"
    | 49 -> "EDGEOP"
    | 50 -> "GRAPH"
    | 51 -> "ID"
    | 52 -> "LCURBRACE"
    | 53 -> "LSQBRACE"
    | 54 -> "NODE"
    | 55 -> "RCURBRACE"
    | 56 -> "RNGLR_EOF"
    | 57 -> "RSQBRACE"
    | 58 -> "SEP"
    | 59 -> "STRICT"
    | 60 -> "SUBGR"
    | _ -> ""

let tokenToNumber = function
    | ASSIGN _ -> 43
    | COL _ -> 44
    | COMMA _ -> 45
    | DIEDGEOP _ -> 46
    | DIGRAPH _ -> 47
    | EDGE _ -> 48
    | EDGEOP _ -> 49
    | GRAPH _ -> 50
    | ID _ -> 51
    | LCURBRACE _ -> 52
    | LSQBRACE _ -> 53
    | NODE _ -> 54
    | RCURBRACE _ -> 55
    | RNGLR_EOF _ -> 56
    | RSQBRACE _ -> 57
    | SEP _ -> 58
    | STRICT _ -> 59
    | SUBGR _ -> 60

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
let leftSide = [|24; 23; 22; 22; 21; 21; 20; 19; 18; 17; 17; 17; 16; 15; 15; 9; 3; 35; 35; 36; 36; 14; 34; 34; 12; 33; 33; 10; 11; 4; 38; 38; 39; 30; 30; 41; 41; 5; 37; 29; 29; 40; 40; 0; 28; 28; 1; 2; 7; 7; 7; 7; 7; 27; 27; 13; 26; 26; 25; 25; 32; 32; 31; 31; 8; 42|]
let private rules = [|60; 36; 44; 3; 44; 9; 34; 44; 3; 49; 46; 39; 38; 45; 37; 53; 0; 57; 50; 54; 48; 7; 58; 50; 47; 51; 51; 24; 9; 35; 52; 13; 55; 23; 22; 12; 9; 33; 10; 1; 21; 10; 14; 4; 20; 30; 38; 30; 41; 1; 9; 43; 9; 19; 29; 37; 29; 40; 18; 28; 28; 17; 1; 11; 5; 2; 9; 43; 9; 14; 16; 27; 27; 58; 26; 58; 25; 9; 59; 31; 15; 32; 25; 52; 13; 55; 26; 8|]
let private rulesStart = [|0; 2; 4; 7; 9; 10; 11; 13; 15; 18; 19; 20; 21; 23; 24; 25; 26; 27; 27; 28; 28; 29; 33; 33; 34; 35; 35; 36; 38; 40; 41; 42; 43; 44; 44; 46; 46; 48; 50; 53; 53; 55; 55; 57; 58; 58; 60; 61; 63; 64; 65; 66; 69; 70; 70; 72; 73; 73; 75; 75; 77; 77; 78; 78; 79; 87; 88|]
let startRule = 65

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 88; 3; 86; 87; 4; 5; 18; 6; 84; 7; 8; 9; 10; 12; 28; 46; 47; 52; 53; 54; 56; 62; 58; 63; 79; 81; 82; 83; 73; 11; 13; 14; 15; 16; 19; 17; 20; 21; 27; 22; 23; 24; 25; 26; 29; 30; 45; 32; 31; 33; 35; 38; 44; 34; 36; 37; 39; 43; 41; 40; 42; 48; 49; 50; 51; 57; 55; 59; 60; 61; 64; 65; 66; 78; 68; 76; 77; 67; 69; 70; 71; 72; 74; 75; 80; 85|]
let private small_gotos =
        [|3; 524288; 2031617; 3866626; 131075; 983043; 3080196; 3276805; 196611; 589830; 2097159; 3342344; 327682; 1638409; 3801098; 393217; 3407883; 458772; 131084; 327693; 458766; 589839; 655376; 720913; 851986; 917523; 1048596; 1114133; 1572886; 1769495; 2293784; 2490393; 2687002; 3145755; 3276828; 3342344; 3538973; 3932190; 655361; 3801119; 786437; 786464; 1441825; 2162722; 2818083; 2883620; 1048578; 589861; 3342344; 1245187; 196646; 589863; 3342376; 1376259; 1507369; 2228266; 2883627; 1572866; 196652; 3342381; 1835012; 65582; 1179695; 1835056; 3473457; 1966083; 1179695; 1835058; 3473457; 2097157; 51; 589876; 2424885; 2621494; 3342344; 2162689; 3735607; 2293761; 2818104; 2359298; 589881; 3342344; 2490371; 1245242; 1900603; 2949180; 2555907; 1245242; 1900605; 2949180; 2686979; 589876; 2424894; 3342344; 3080193; 3604543; 3145730; 1704000; 3801153; 3276802; 1704002; 3801153; 3473427; 131084; 327693; 458766; 589839; 655376; 720913; 917523; 1048596; 1114133; 1572886; 1769539; 2293784; 2490393; 2687002; 3145755; 3276828; 3342344; 3538973; 3932190; 3538948; 65604; 1179695; 1835056; 3473457; 3801089; 3407941; 3866644; 131084; 327693; 458766; 589839; 655376; 720913; 852038; 917523; 1048596; 1114133; 1572886; 1769495; 2293784; 2490393; 2687002; 3145755; 3276828; 3342344; 3538973; 3932190; 3932161; 3604551; 4128775; 262216; 1310793; 1376330; 1966155; 2555980; 3014733; 3211342; 4259847; 262216; 1310793; 1376330; 1966159; 2555980; 3014733; 3211342; 4456456; 589904; 655441; 917586; 1572886; 2293784; 2490451; 3342344; 3932190; 4521988; 786464; 1441825; 2162722; 2883620; 4784131; 589908; 2359381; 3342344; 5177348; 65622; 1179695; 1835056; 3473457; 5505026; 1638487; 3801098|]
let gotos = Array.zeroCreate 89
for i = 0 to 88 do
        gotos.[i] <- Array.zeroCreate 61
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
let private lists_reduces = [|[|61,1|]; [|50,1|]; [|49,1|]; [|12,2|]; [|27,1|]; [|26,1|]; [|24,1|]; [|27,2|]; [|51,3|]; [|15,1|]; [|3,2|]; [|2,2|]; [|23,1|]; [|2,3|]; [|1,2|]; [|16,1|]; [|16,1; 15,1|]; [|30,1|]; [|30,1; 28,1|]; [|28,2|]; [|45,1|]; [|45,2|]; [|8,3|]; [|38,3|]; [|42,1|]; [|40,1|]; [|40,2|]; [|7,2|]; [|42,2|]; [|43,1|]; [|46,1|]; [|48,1|]; [|64,7|]; [|64,8|]; [|57,1|]; [|57,2|]; [|31,1|]; [|52,1; 31,1|]; [|54,1|]; [|47,1|]; [|47,2|]; [|18,1|]; [|54,2|]; [|21,4|]; [|55,1|]; [|36,1|]; [|32,1|]; [|34,1|]; [|29,1|]; [|34,2|]; [|6,2|]; [|0,1|]; [|20,1|]; [|0,2|]; [|5,1|]; [|4,1|]; [|36,2|]; [|37,1|]; [|37,2|]; [|11,1|]; [|9,1|]; [|10,1|]; [|59,1|]; [|59,2|]; [|14,1|]; [|13,1|]; [|63,1|]|]
let private small_reduces =
        [|262146; 3407872; 3801088; 524289; 3801089; 589825; 3801090; 720905; 3145731; 3276803; 3342339; 3407875; 3473411; 3538947; 3604483; 3801091; 3932163; 786436; 3014660; 3211268; 3473412; 3801092; 851972; 3014661; 3211269; 3473413; 3801093; 917508; 3014662; 3211270; 3473414; 3801094; 983044; 3014663; 3211271; 3473415; 3801095; 1114113; 3801096; 1179657; 2818057; 2883593; 2949129; 3014665; 3211273; 3407881; 3473417; 3735561; 3801097; 1310724; 3014666; 3211274; 3473418; 3801098; 1376260; 3014667; 3211275; 3473419; 3801099; 1441796; 3014668; 3211276; 3473420; 3801100; 1507332; 3014669; 3211277; 3473421; 3801101; 1638404; 3014670; 3211278; 3473422; 3801102; 1703940; 3014671; 3211279; 3473423; 3801103; 1769477; 2883593; 3014672; 3211280; 3473424; 3801104; 1835012; 3014673; 3211281; 3473425; 3801106; 1900545; 3801107; 1966081; 3801108; 2031617; 3801109; 2228226; 3473430; 3801110; 2424834; 2949143; 3735575; 2490369; 3735576; 2555905; 3735577; 2621441; 3735578; 2752514; 2949147; 3735579; 2818049; 3735580; 2883585; 3735581; 2949121; 3801118; 3014657; 3801119; 3145729; 3670048; 3211265; 3670049; 3276801; 3670050; 3342337; 3670051; 3407876; 3014692; 3211300; 3473444; 3801125; 3473409; 3604518; 3538945; 3801127; 3604481; 3801128; 3670017; 3407913; 3735553; 3604522; 3997700; 3014699; 3211307; 3473451; 3801131; 4063233; 3604524; 4128770; 3473453; 3801133; 4194307; 3342382; 3407918; 3932206; 4259842; 3473455; 3801135; 4325379; 3342384; 3407920; 3932208; 4390914; 3473457; 3801137; 4521988; 3014660; 3211268; 3473412; 3801092; 4587524; 3014673; 3211281; 3473425; 3801105; 4653060; 3014692; 3211300; 3473444; 3801124; 4718596; 3014706; 3211314; 3473458; 3801138; 4784129; 3407923; 4849665; 3407924; 4915201; 3407925; 4980739; 3342390; 3407926; 3932214; 5046275; 3342391; 3407927; 3932215; 5111810; 3473464; 3801144; 5177345; 3801145; 5242881; 3801146; 5308418; 3473467; 3801147; 5373954; 3473468; 3801148; 5439490; 3473469; 3801149; 5505025; 3407934; 5570561; 3407935; 5636099; 3342400; 3407936; 3801152; 5701635; 3342401; 3407937; 3801153; 5767170; 3080258; 3276866|]
let reduces = Array.zeroCreate 89
for i = 0 to 88 do
        reduces.[i] <- Array.zeroCreate 61
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
let private lists_zeroReduces = [|[|62|]; [|60|]; [|58|]; [|17|]; [|35|]; [|55; 53|]; [|49; 37; 35|]; [|25|]; [|22|]; [|46; 44|]; [|44|]; [|43; 41|]; [|39|]; [|56|]; [|53|]; [|33|]; [|19|]|]
let private small_zeroReduces =
        [|2; 3080192; 3276800; 196610; 3407873; 3801089; 327681; 3407874; 458756; 3407875; 3473412; 3604485; 3801094; 786436; 3014663; 3211271; 3473415; 3801095; 1376260; 3014664; 3211272; 3473416; 3801096; 1835009; 3801097; 1966081; 3801098; 2097153; 3735563; 2490369; 3735564; 2555905; 3735564; 3145729; 3670029; 3276801; 3670029; 3473412; 3407875; 3473412; 3604494; 3801094; 3538945; 3801097; 3866628; 3407875; 3473412; 3604485; 3801094; 4128770; 3473423; 3801103; 4259842; 3473423; 3801103; 4456449; 3407875; 4521988; 3014663; 3211271; 3473415; 3801095; 4784129; 3407888; 5177345; 3801097; 5505025; 3407874|]
let zeroReduces = Array.zeroCreate 89
for i = 0 to 88 do
        zeroReduces.[i] <- Array.zeroCreate 61
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
let private accStates = Array.zeroCreate 89
for i = 0 to 88 do
        accStates.[i] <- List.exists ((=) i) small_acc
let eofIndex = 56
let errorIndex = 6
let errorRulesExists = false
let private parserSource = new ParserSource<Token> (gotos, reduces, zeroReduces, accStates, rules, rulesStart, leftSide, startRule, eofIndex, tokenToNumber, acceptEmptyInput, numToString, errorIndex, errorRulesExists)
let buildAst : (seq<Token> -> ParseResult<Token>) =
    buildAst<Token> parserSource

let _rnglr_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(43, new Nodes([|box (new AST(new Family(41, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(66, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(49, new Nodes([|box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(55, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(58, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(56, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(53, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(44, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(39, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(33, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(62, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(60, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(41, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(35, new Nodes([||])), null)), null); null|]
let _rnglr_filtered_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(43, new Nodes([|box (new AST(new Family(41, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(66, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(49, new Nodes([|box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(55, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(58, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(56, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(53, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(44, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(39, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(33, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(62, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(60, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(41, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(35, new Nodes([||])), null)), null); null|]
for x in _rnglr_filtered_epsilons do if x <> null then x.ChooseSingleAst()
let _rnglr_extra_array, _rnglr_rule_, _rnglr_concats = 
  (Array.zeroCreate 0 : array<'_rnglr_type_a_list * '_rnglr_type_attr_list * '_rnglr_type_attr_stmt * '_rnglr_type_compass_pt * '_rnglr_type_edge_operator * '_rnglr_type_edge_stmt * '_rnglr_type_error * '_rnglr_type_full_stmt * '_rnglr_type_graph * '_rnglr_type_id * '_rnglr_type_node_id * '_rnglr_type_node_stmt * '_rnglr_type_port * '_rnglr_type_stmt_list * '_rnglr_type_subgraph * '_rnglr_type_yard_exp_brackets_216 * '_rnglr_type_yard_exp_brackets_217 * '_rnglr_type_yard_exp_brackets_218 * '_rnglr_type_yard_exp_brackets_219 * '_rnglr_type_yard_exp_brackets_220 * '_rnglr_type_yard_exp_brackets_221 * '_rnglr_type_yard_exp_brackets_222 * '_rnglr_type_yard_exp_brackets_223 * '_rnglr_type_yard_exp_brackets_224 * '_rnglr_type_yard_exp_brackets_225 * '_rnglr_type_yard_many_85 * '_rnglr_type_yard_many_86 * '_rnglr_type_yard_many_87 * '_rnglr_type_yard_many_88 * '_rnglr_type_yard_many_89 * '_rnglr_type_yard_many_90 * '_rnglr_type_yard_opt_85 * '_rnglr_type_yard_opt_86 * '_rnglr_type_yard_opt_87 * '_rnglr_type_yard_opt_88 * '_rnglr_type_yard_opt_89 * '_rnglr_type_yard_opt_90 * '_rnglr_type_yard_rule_211 * '_rnglr_type_yard_rule_213 * '_rnglr_type_yard_rule_214 * '_rnglr_type_yard_rule_list_212 * '_rnglr_type_yard_rule_list_215 * '_rnglr_type_yard_start_rule>), 
  [|
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SUBGR _rnglr_val -> [_rnglr_val] | a -> failwith "SUBGR expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_90) (gr7:GraphDataContainer)
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 72 "DotGrammar.yrd"
                                                                      1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_225) 
# 276 "DotParser.fs"
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
                  
# 70 "DotGrammar.yrd"
                                                  1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_224) 
# 298 "DotParser.fs"
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
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_88) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 70 "DotGrammar.yrd"
                                                         1
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_223) 
# 322 "DotParser.fs"
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
                  
# 70 "DotGrammar.yrd"
                                                  1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_223) 
# 344 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr5:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with EDGEOP _rnglr_val -> [_rnglr_val] | a -> failwith "EDGEOP expected, but %A found" a )
             |> List.iter (fun (p) -> 
              _rnglr_cycle_res := (
                
# 61 "DotGrammar.yrd"
                                                                          p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_222) 
# 364 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr5:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with DIEDGEOP _rnglr_val -> [_rnglr_val] | a -> failwith "DIEDGEOP expected, but %A found" a )
             |> List.iter (fun (p) -> 
              _rnglr_cycle_res := (
                
# 61 "DotGrammar.yrd"
                                                                                           p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_222) 
# 384 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_214) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_213) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 36 "DotGrammar.yrd"
                                                                                 i 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_221) 
# 406 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMMA _rnglr_val -> [_rnglr_val] | a -> failwith "COMMA expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_211) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 36 "DotGrammar.yrd"
                                                                                 i 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_220) 
# 428 "DotParser.fs"
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
                    
# 54 "DotGrammar.yrd"
                                                               l
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_219) 
# 452 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr3:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with GRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "GRAPH expected, but %A found" a )
             |> List.iter (fun (n) -> 
              _rnglr_cycle_res := (
                
# 51 "DotGrammar.yrd"
                                                                            n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_218) 
# 472 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr3:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with NODE _rnglr_val -> [_rnglr_val] | a -> failwith "NODE expected, but %A found" a )
             |> List.iter (fun (n) -> 
              _rnglr_cycle_res := (
                
# 51 "DotGrammar.yrd"
                                                                                         n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_218) 
# 492 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr3:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with EDGE _rnglr_val -> [_rnglr_val] | a -> failwith "EDGE expected, but %A found" a )
             |> List.iter (fun (n) -> 
              _rnglr_cycle_res := (
                
# 51 "DotGrammar.yrd"
                                                                                                      n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_218) 
# 512 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_full_stmt) gr
             |> List.iter (fun (l) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 42 "DotGrammar.yrd"
                                                                                     l
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_217) 
# 534 "DotParser.fs"
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
                
# 39 "DotGrammar.yrd"
                                               t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_216) 
# 554 "DotParser.fs"
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
                
# 39 "DotGrammar.yrd"
                                                               t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_216) 
# 574 "DotParser.fs"
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
                
# 76 "DotGrammar.yrd"
                            i 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 76 "DotGrammar.yrd"
               : '_rnglr_type_id) 
# 594 "DotParser.fs"
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
                
# 74 "DotGrammar.yrd"
                                   
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 74 "DotGrammar.yrd"
               : '_rnglr_type_compass_pt) 
# 614 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 72 "DotGrammar.yrd"
                                                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 72 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_89) 
# 632 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_225) (gr7:GraphDataContainer)
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 72 "DotGrammar.yrd"
                                                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 72 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_89) 
# 652 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
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
               : '_rnglr_type_yard_opt_90) 
# 670 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 39 "DotGrammar.yrd"
                                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_90) 
# 690 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_89) (gr7:GraphDataContainer)
             |> List.iter (fun (_) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_stmt_list) gr7
                 |> List.iter (fun (l) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 72 "DotGrammar.yrd"
                                                                                                                       "tmp_subgr_name" 
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 72 "DotGrammar.yrd"
               : '_rnglr_type_subgraph) 
# 716 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 70 "DotGrammar.yrd"
                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 70 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_88) 
# 734 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_224) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 70 "DotGrammar.yrd"
                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 70 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_88) 
# 754 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_223) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 70 "DotGrammar.yrd"
                                                                               1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 70 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 774 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeID ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 67 "DotGrammar.yrd"
                                   None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 67 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_87) 
# 792 "DotParser.fs"
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
                
# 67 "DotGrammar.yrd"
                                     Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 67 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_87) 
# 812 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (nodeID) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_87) nodeID
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 68 "DotGrammar.yrd"
                    nodeID 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 67 "DotGrammar.yrd"
               : '_rnglr_type_node_id) 
# 834 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr6:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_id) 
             |> List.iter (fun (nodeID) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 65 "DotGrammar.yrd"
                    gr6.AddNodeStmtData [nodeID] lst 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 64 "DotGrammar.yrd"
               : '_rnglr_type_node_stmt) 
# 856 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr5:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_222) (gr5:GraphDataContainer)
             |> List.iter (fun (op) -> 
              _rnglr_cycle_res := (
                
# 62 "DotGrammar.yrd"
                  gr5.AddGeneralInfo [(op, "")] 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 61 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 876 "DotParser.fs"
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
                
# 58 "DotGrammar.yrd"
                                                                                     nodeID 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_213) 
# 896 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_subgraph) (new GraphDataContainer())
             |> List.iter (fun (sub) -> 
              _rnglr_cycle_res := (
                
# 58 "DotGrammar.yrd"
                                                                                                                                             sub 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_213) 
# 916 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_214) 
# 926 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 58 "DotGrammar.yrd"
                                                                                                                                                   []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_90) 
# 944 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_221) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_90) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 58 "DotGrammar.yrd"
                                                                                                                                                       yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_90) 
# 966 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 36 "DotGrammar.yrd"
                                       [] 
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 36 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_215) 
# 984 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_213) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_90) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 36 "DotGrammar.yrd"
                                                                                         hd::tl 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 36 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_215) 
# 1006 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr4:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_215) 
             |> List.iter (fun (edges) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 59 "DotGrammar.yrd"
                    gr4.AddEdgeStmtData edges lst 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_edge_stmt) 
# 1028 "DotParser.fs"
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
                    
# 56 "DotGrammar.yrd"
                                                         (k,v)
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_211) 
# 1052 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 56 "DotGrammar.yrd"
                                                           []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_89) 
# 1070 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_220) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_89) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 56 "DotGrammar.yrd"
                                                               yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_89) 
# 1092 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 36 "DotGrammar.yrd"
                                       [] 
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 36 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_212) 
# 1110 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_211) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_89) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 36 "DotGrammar.yrd"
                                                                                         hd::tl 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 36 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_212) 
# 1132 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_212) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 56 "DotGrammar.yrd"
                                                                      lst 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 56 "DotGrammar.yrd"
               : '_rnglr_type_a_list) 
# 1152 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 54 "DotGrammar.yrd"
                             []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_88) 
# 1170 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_219) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_88) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 54 "DotGrammar.yrd"
                                 yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_88) 
# 1192 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_88) 
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 54 "DotGrammar.yrd"
                                                                  List.concat l 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_attr_list) 
# 1212 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr3:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_218) (gr3:GraphDataContainer)
             |> List.iter (fun (attr_name) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 52 "DotGrammar.yrd"
                    gr3.AddGeneralAttrs attr_name lst 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 51 "DotGrammar.yrd"
               : '_rnglr_type_attr_stmt) 
# 1234 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr2:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_stmt) gr2
             |> List.iter (fun (ns) -> 
              _rnglr_cycle_res := (
                
# 45 "DotGrammar.yrd"
                                            
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1254 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr2:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_stmt) gr2
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 46 "DotGrammar.yrd"
                                             
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1274 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr2:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_attr_stmt) gr2
             |> List.iter (fun (attr_s) -> 
              _rnglr_cycle_res := (
                
# 47 "DotGrammar.yrd"
                                             
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1294 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr2:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (k) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with ASSIGN _rnglr_val -> [_rnglr_val] | a -> failwith "ASSIGN expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_id) 
                 |> List.iter (fun (v) -> 
                  _rnglr_cycle_res := (
                    
# 48 "DotGrammar.yrd"
                                           gr2.AddAssignStmt k v 
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1318 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr2:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_subgraph) (new GraphDataContainer())
             |> List.iter (fun (sub) -> 
              _rnglr_cycle_res := (
                
# 49 "DotGrammar.yrd"
                                                                
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1338 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
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
               : '_rnglr_type_yard_many_87) 
# 1356 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_217) (gr:GraphDataContainer)
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_87) (gr:GraphDataContainer)
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 42 "DotGrammar.yrd"
                                                                yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_87) 
# 1378 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_87) (gr:GraphDataContainer)
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 42 "DotGrammar.yrd"
                                                                                         
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_stmt_list) 
# 1398 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name -> fun x ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 39 "DotGrammar.yrd"
                                                                            []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_86) 
# 1416 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name -> fun x ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_86) s g name x
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 39 "DotGrammar.yrd"
                                                                                yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_86) 
# 1438 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 39 "DotGrammar.yrd"
                                                                            []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_85) 
# 1456 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_85) s g name
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 39 "DotGrammar.yrd"
                                                                                yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_85) 
# 1478 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g ->
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
               : '_rnglr_type_yard_opt_86) 
# 1496 "DotParser.fs"
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
                
# 39 "DotGrammar.yrd"
                                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_86) 
# 1516 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_85) 
# 1534 "DotParser.fs"
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
                
# 39 "DotGrammar.yrd"
                           Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_85) 
# 1554 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_85) 
             |> List.iter (fun (s) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_216) s
               |> List.iter (fun (g) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_86) s g
                 |> List.iter (fun (name) -> 
                  ((unbox _rnglr_children.[3]) : '_rnglr_type_yard_many_85) s g name
                   |> List.iter (fun (_) -> 
                    (match ((unbox _rnglr_children.[4]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
                     |> List.iter (fun (_) -> 
                      ((unbox _rnglr_children.[5]) : '_rnglr_type_stmt_list) (graphs.[0])
                       |> List.iter (fun (x) -> 
                        (match ((unbox _rnglr_children.[6]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                         |> List.iter (fun (_) -> 
                          ((unbox _rnglr_children.[7]) : '_rnglr_type_yard_many_86) s g name x
                           |> List.iter (fun (_) -> 
                            _rnglr_cycle_res := (
                              
# 40 "DotGrammar.yrd"
                                graphs.[0].AddGeneralInfo [strict_key, (Utils.optToStr s); type_key, g; name_key, (Utils.optToStr name)] 
                                )::!_rnglr_cycle_res ) ) ) ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_graph) 
# 1588 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          ((unbox _rnglr_children.[0]) : '_rnglr_type_graph) 
            )
# 39 "DotGrammar.yrd"
               : '_rnglr_type_yard_start_rule) 
# 1598 "DotParser.fs"
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
# 1616 "DotParser.fs"
      );
  |] , [|
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_a_list)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_attr_list)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr3:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_attr_stmt)  (gr3:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_compass_pt)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr5:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_operator)  (gr5:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr4:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_stmt)  (gr4:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_error)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr2:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_full_stmt)  (gr2:GraphDataContainer) ) |> List.concat));
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
      box ( fun (gr6:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_node_stmt)  (gr6:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_port)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_stmt_list)  (gr:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_subgraph)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_216)  s ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_217)  (gr:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr3:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_218)  (gr3:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_219)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_220)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_221)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr5:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_222)  (gr5:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_223)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_224)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_225)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s -> fun g -> fun name ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_85)  s g name ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s -> fun g -> fun name -> fun x ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_86)  s g name x ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_87)  (gr:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_88)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_89)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_90)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_85)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s -> fun g ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_86)  s g ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun nodeID ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_87)  nodeID ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_88)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_89)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_90)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_211)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_213)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_214)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_212)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_215)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_start_rule)   ) |> List.concat));
  |] 
let translate (args : TranslateArguments<_,_>) (tree : Tree<_>) (dict : _ ) : '_rnglr_type_yard_start_rule = 
  unbox (tree.Translate _rnglr_rule_  leftSide _rnglr_concats (if args.filterEpsilons then _rnglr_filtered_epsilons else _rnglr_epsilons) args.tokenToRange args.zeroPosition args.clearAST dict) : '_rnglr_type_yard_start_rule
