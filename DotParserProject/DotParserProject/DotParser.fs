
# 2 "DotParser.fs"
module DotParserProject.DotParser
#nowarn "64";; // From fsyacc: turn off warnings that type variables used in production annotations are instantiated to concrete type
open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR
open Yard.Generators.RNGLR.AST

# 1 "DotGrammar.yrd"

open System.Collections.Generic
open DotParserProject.CollectDataFuncs

//containers for graph's data
let vertices_lists = new ResizeArray<string list>()
let graph_info = new Dictionary<string, string>()
let vertices_attrs = new Dictionary<string, (string*string) list>()
let edges_attrs = new Dictionary<string, (string*string) list>()
let general_attrs = new Dictionary<string, (string*string) list>()
let assign_stmt_list = new ResizeArray<string*string>()

//constants
let type_key = "type"
let strict_key = "is_strict"
let name_key = "name"

# 27 "DotParser.fs"
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
    | 15 -> "yard_exp_brackets_131"
    | 16 -> "yard_exp_brackets_132"
    | 17 -> "yard_exp_brackets_133"
    | 18 -> "yard_exp_brackets_134"
    | 19 -> "yard_exp_brackets_135"
    | 20 -> "yard_exp_brackets_136"
    | 21 -> "yard_exp_brackets_137"
    | 22 -> "yard_exp_brackets_138"
    | 23 -> "yard_exp_brackets_139"
    | 24 -> "yard_exp_brackets_140"
    | 25 -> "yard_many_55"
    | 26 -> "yard_many_56"
    | 27 -> "yard_many_57"
    | 28 -> "yard_many_58"
    | 29 -> "yard_many_59"
    | 30 -> "yard_many_60"
    | 31 -> "yard_opt_55"
    | 32 -> "yard_opt_56"
    | 33 -> "yard_opt_57"
    | 34 -> "yard_opt_58"
    | 35 -> "yard_opt_59"
    | 36 -> "yard_opt_60"
    | 37 -> "yard_rule_127"
    | 38 -> "yard_rule_129"
    | 39 -> "yard_rule_list_128"
    | 40 -> "yard_rule_list_130"
    | 41 -> "yard_start_rule"
    | 42 -> "ASSIGN"
    | 43 -> "COL"
    | 44 -> "COMMA"
    | 45 -> "DIEDGEOP"
    | 46 -> "DIGRAPH"
    | 47 -> "EDGE"
    | 48 -> "EDGEOP"
    | 49 -> "GRAPH"
    | 50 -> "ID"
    | 51 -> "LCURBRACE"
    | 52 -> "LSQBRACE"
    | 53 -> "NODE"
    | 54 -> "RCURBRACE"
    | 55 -> "RNGLR_EOF"
    | 56 -> "RSQBRACE"
    | 57 -> "SEP"
    | 58 -> "STRICT"
    | 59 -> "SUBGR"
    | _ -> ""

let tokenToNumber = function
    | ASSIGN _ -> 42
    | COL _ -> 43
    | COMMA _ -> 44
    | DIEDGEOP _ -> 45
    | DIGRAPH _ -> 46
    | EDGE _ -> 47
    | EDGEOP _ -> 48
    | GRAPH _ -> 49
    | ID _ -> 50
    | LCURBRACE _ -> 51
    | LSQBRACE _ -> 52
    | NODE _ -> 53
    | RCURBRACE _ -> 54
    | RNGLR_EOF _ -> 55
    | RSQBRACE _ -> 56
    | SEP _ -> 57
    | STRICT _ -> 58
    | SUBGR _ -> 59

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
let leftSide = [|24; 23; 22; 22; 21; 21; 20; 19; 18; 17; 17; 17; 16; 15; 15; 9; 3; 35; 35; 36; 36; 14; 34; 34; 12; 33; 33; 10; 11; 4; 38; 38; 30; 30; 40; 40; 5; 37; 29; 29; 39; 39; 0; 28; 28; 1; 2; 7; 7; 7; 7; 7; 27; 27; 13; 26; 26; 25; 25; 32; 32; 31; 31; 8; 41|]
let private rules = [|59; 36; 43; 3; 43; 9; 34; 43; 3; 48; 45; 4; 38; 44; 37; 52; 0; 56; 49; 53; 47; 7; 57; 49; 46; 50; 50; 24; 9; 35; 51; 13; 54; 23; 22; 12; 9; 33; 10; 1; 21; 10; 14; 20; 30; 38; 30; 40; 1; 9; 42; 9; 19; 29; 37; 29; 39; 18; 28; 28; 17; 1; 11; 5; 2; 9; 42; 9; 14; 16; 27; 27; 57; 26; 57; 25; 9; 58; 31; 15; 32; 25; 51; 13; 54; 26; 8|]
let private rulesStart = [|0; 2; 4; 7; 9; 10; 11; 13; 15; 18; 19; 20; 21; 23; 24; 25; 26; 27; 27; 28; 28; 29; 33; 33; 34; 35; 35; 36; 38; 40; 41; 42; 43; 43; 45; 45; 47; 49; 52; 52; 54; 54; 56; 57; 57; 59; 60; 62; 63; 64; 65; 68; 69; 69; 71; 72; 72; 74; 74; 76; 76; 77; 77; 78; 86; 87|]
let startRule = 64

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 87; 3; 85; 86; 4; 5; 18; 6; 83; 7; 8; 9; 10; 12; 28; 46; 47; 52; 53; 54; 56; 62; 58; 63; 78; 80; 81; 82; 69; 11; 13; 14; 15; 16; 19; 17; 20; 21; 27; 22; 23; 24; 25; 26; 29; 30; 45; 32; 31; 33; 35; 38; 44; 34; 36; 37; 39; 43; 41; 40; 42; 48; 49; 50; 51; 57; 55; 59; 60; 61; 64; 72; 73; 77; 75; 76; 65; 66; 67; 68; 70; 71; 74; 79; 84|]
let private small_gotos =
        [|3; 524288; 2031617; 3801090; 131075; 983043; 3014660; 3211269; 196611; 589830; 2097159; 3276808; 327682; 1638409; 3735562; 393217; 3342347; 458772; 131084; 327693; 458766; 589839; 655376; 720913; 851986; 917523; 1048596; 1114133; 1572886; 1769495; 2293784; 2490393; 2621466; 3080219; 3211292; 3276808; 3473437; 3866654; 655361; 3735583; 786437; 786464; 1441825; 2162722; 2752547; 2818084; 1048578; 589861; 3276808; 1245187; 196646; 589863; 3276840; 1376259; 1507369; 2228266; 2818091; 1572866; 196652; 3276845; 1835012; 65582; 1179695; 1835056; 3407921; 1966083; 1179695; 1835058; 3407921; 2097157; 51; 589876; 2424885; 2555958; 3276808; 2162689; 3670071; 2293761; 2752568; 2359298; 589881; 3276808; 2490371; 1245242; 1900603; 2883644; 2555907; 1245242; 1900605; 2883644; 2686979; 589876; 2424894; 3276808; 3080193; 3539007; 3145730; 1704000; 3735617; 3276802; 1704002; 3735617; 3473427; 131084; 327693; 458766; 589839; 655376; 720913; 917523; 1048596; 1114133; 1572886; 1769539; 2293784; 2490393; 2621466; 3080219; 3211292; 3276808; 3473437; 3866654; 3538948; 65604; 1179695; 1835056; 3407921; 3801089; 3342405; 3866644; 131084; 327693; 458766; 589839; 655376; 720913; 852038; 917523; 1048596; 1114133; 1572886; 1769495; 2293784; 2490393; 2621466; 3080219; 3211292; 3276808; 3473437; 3866654; 3932161; 3539015; 4128774; 262216; 1310793; 1376330; 1966155; 2949196; 3145805; 4194312; 589902; 655439; 917584; 1572886; 2293784; 2490449; 3276808; 3866654; 4259844; 786464; 1441825; 2162722; 2818084; 4521987; 589906; 2359379; 3276808; 4718598; 262216; 1310793; 1376330; 1966164; 2949196; 3145805; 5111812; 65621; 1179695; 1835056; 3407921; 5439490; 1638486; 3735562|]
let gotos = Array.zeroCreate 88
for i = 0 to 87 do
        gotos.[i] <- Array.zeroCreate 60
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
let private lists_reduces = [|[|60,1|]; [|49,1|]; [|48,1|]; [|12,2|]; [|27,1|]; [|26,1|]; [|24,1|]; [|27,2|]; [|50,3|]; [|15,1|]; [|3,2|]; [|2,2|]; [|23,1|]; [|2,3|]; [|1,2|]; [|16,1|]; [|16,1; 15,1|]; [|30,1|]; [|30,1; 28,1|]; [|28,2|]; [|44,1|]; [|44,2|]; [|8,3|]; [|37,3|]; [|41,1|]; [|39,1|]; [|39,2|]; [|7,2|]; [|41,2|]; [|42,1|]; [|45,1|]; [|47,1|]; [|63,7|]; [|63,8|]; [|56,1|]; [|56,2|]; [|31,1|]; [|51,1; 31,1|]; [|53,1|]; [|46,1|]; [|46,2|]; [|18,1|]; [|53,2|]; [|21,4|]; [|54,1|]; [|35,1|]; [|6,2|]; [|0,1|]; [|20,1|]; [|0,2|]; [|33,1|]; [|29,1|]; [|33,2|]; [|5,1|]; [|4,1|]; [|35,2|]; [|36,1|]; [|36,2|]; [|11,1|]; [|9,1|]; [|10,1|]; [|58,1|]; [|58,2|]; [|14,1|]; [|13,1|]; [|62,1|]|]
let private small_reduces =
        [|262146; 3342336; 3735552; 524289; 3735553; 589825; 3735554; 720905; 3080195; 3211267; 3276803; 3342339; 3407875; 3473411; 3538947; 3735555; 3866627; 786436; 2949124; 3145732; 3407876; 3735556; 851972; 2949125; 3145733; 3407877; 3735557; 917508; 2949126; 3145734; 3407878; 3735558; 983044; 2949127; 3145735; 3407879; 3735559; 1114113; 3735560; 1179657; 2752521; 2818057; 2883593; 2949129; 3145737; 3342345; 3407881; 3670025; 3735561; 1310724; 2949130; 3145738; 3407882; 3735562; 1376260; 2949131; 3145739; 3407883; 3735563; 1441796; 2949132; 3145740; 3407884; 3735564; 1507332; 2949133; 3145741; 3407885; 3735565; 1638404; 2949134; 3145742; 3407886; 3735566; 1703940; 2949135; 3145743; 3407887; 3735567; 1769477; 2818057; 2949136; 3145744; 3407888; 3735568; 1835012; 2949137; 3145745; 3407889; 3735570; 1900545; 3735571; 1966081; 3735572; 2031617; 3735573; 2228226; 3407894; 3735574; 2424834; 2883607; 3670039; 2490369; 3670040; 2555905; 3670041; 2621441; 3670042; 2752514; 2883611; 3670043; 2818049; 3670044; 2883585; 3670045; 2949121; 3735582; 3014657; 3735583; 3145729; 3604512; 3211265; 3604513; 3276801; 3604514; 3342337; 3604515; 3407876; 2949156; 3145764; 3407908; 3735589; 3473409; 3538982; 3538945; 3735591; 3604481; 3735592; 3670017; 3342377; 3735553; 3538986; 3997700; 2949163; 3145771; 3407915; 3735595; 4063233; 3538988; 4128770; 3407917; 3735597; 4259844; 2949124; 3145732; 3407876; 3735556; 4325380; 2949137; 3145745; 3407889; 3735569; 4390916; 2949156; 3145764; 3407908; 3735588; 4456452; 2949166; 3145774; 3407918; 3735598; 4521985; 3342383; 4587521; 3342384; 4653057; 3342385; 4718594; 3407922; 3735602; 4784131; 3276851; 3342387; 3866675; 4849666; 3407924; 3735604; 4915203; 3276853; 3342389; 3866677; 4980739; 3276854; 3342390; 3866678; 5046274; 3407927; 3735607; 5111809; 3735608; 5177345; 3735609; 5242882; 3407930; 3735610; 5308418; 3407931; 3735611; 5373954; 3407932; 3735612; 5439489; 3342397; 5505025; 3342398; 5570563; 3276863; 3342399; 3735615; 5636099; 3276864; 3342400; 3735616; 5701634; 3014721; 3211329|]
let reduces = Array.zeroCreate 88
for i = 0 to 87 do
        reduces.[i] <- Array.zeroCreate 60
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
let private lists_zeroReduces = [|[|61|]; [|59|]; [|57|]; [|17|]; [|34|]; [|54; 52|]; [|48; 36; 34|]; [|25|]; [|22|]; [|45; 43|]; [|43|]; [|42; 40|]; [|38|]; [|55|]; [|52|]; [|32|]; [|19|]|]
let private small_zeroReduces =
        [|2; 3014656; 3211264; 196610; 3342337; 3735553; 327681; 3342338; 458756; 3342339; 3407876; 3538949; 3735558; 786436; 2949127; 3145735; 3407879; 3735559; 1376260; 2949128; 3145736; 3407880; 3735560; 1835009; 3735561; 1966081; 3735562; 2097153; 3670027; 2490369; 3670028; 2555905; 3670028; 3145729; 3604493; 3276801; 3604493; 3473412; 3342339; 3407876; 3538958; 3735558; 3538945; 3735561; 3866628; 3342339; 3407876; 3538949; 3735558; 4128770; 3407887; 3735567; 4194305; 3342339; 4259844; 2949127; 3145735; 3407879; 3735559; 4521985; 3342352; 4718594; 3407887; 3735567; 5111809; 3735561; 5439489; 3342338|]
let zeroReduces = Array.zeroCreate 88
for i = 0 to 87 do
        zeroReduces.[i] <- Array.zeroCreate 60
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
let private accStates = Array.zeroCreate 88
for i = 0 to 87 do
        accStates.[i] <- List.exists ((=) i) small_acc
let eofIndex = 55
let errorIndex = 6
let errorRulesExists = false
let private parserSource = new ParserSource<Token> (gotos, reduces, zeroReduces, accStates, rules, rulesStart, leftSide, startRule, eofIndex, tokenToNumber, acceptEmptyInput, numToString, errorIndex, errorRulesExists)
let buildAst : (seq<Token> -> ParseResult<Token>) =
    buildAst<Token> parserSource

let _rnglr_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(42, new Nodes([|box (new AST(new Family(40, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(65, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(48, new Nodes([|box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([|box (new AST(new Family(52, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(52, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(43, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(38, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(32, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(61, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(59, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
let _rnglr_filtered_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(42, new Nodes([|box (new AST(new Family(40, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(65, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(48, new Nodes([|box (new AST(new Family(36, new Nodes([|box (new AST(new Family(34, new Nodes([||])), null)); box (new AST(new Family(45, new Nodes([|box (new AST(new Family(43, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([|box (new AST(new Family(52, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(52, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(43, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(38, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(32, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(61, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(59, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); null|]
for x in _rnglr_filtered_epsilons do if x <> null then x.ChooseSingleAst()
let _rnglr_extra_array, _rnglr_rule_, _rnglr_concats = 
  (Array.zeroCreate 0 : array<'_rnglr_type_a_list * '_rnglr_type_attr_list * '_rnglr_type_attr_stmt * '_rnglr_type_compass_pt * '_rnglr_type_edge_operator * '_rnglr_type_edge_stmt * '_rnglr_type_error * '_rnglr_type_full_stmt * '_rnglr_type_graph * '_rnglr_type_id * '_rnglr_type_node_id * '_rnglr_type_node_stmt * '_rnglr_type_port * '_rnglr_type_stmt_list * '_rnglr_type_subgraph * '_rnglr_type_yard_exp_brackets_131 * '_rnglr_type_yard_exp_brackets_132 * '_rnglr_type_yard_exp_brackets_133 * '_rnglr_type_yard_exp_brackets_134 * '_rnglr_type_yard_exp_brackets_135 * '_rnglr_type_yard_exp_brackets_136 * '_rnglr_type_yard_exp_brackets_137 * '_rnglr_type_yard_exp_brackets_138 * '_rnglr_type_yard_exp_brackets_139 * '_rnglr_type_yard_exp_brackets_140 * '_rnglr_type_yard_many_55 * '_rnglr_type_yard_many_56 * '_rnglr_type_yard_many_57 * '_rnglr_type_yard_many_58 * '_rnglr_type_yard_many_59 * '_rnglr_type_yard_many_60 * '_rnglr_type_yard_opt_55 * '_rnglr_type_yard_opt_56 * '_rnglr_type_yard_opt_57 * '_rnglr_type_yard_opt_58 * '_rnglr_type_yard_opt_59 * '_rnglr_type_yard_opt_60 * '_rnglr_type_yard_rule_127 * '_rnglr_type_yard_rule_129 * '_rnglr_type_yard_rule_list_128 * '_rnglr_type_yard_rule_list_130 * '_rnglr_type_yard_start_rule>), 
  [|
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SUBGR _rnglr_val -> [_rnglr_val] | a -> failwith "SUBGR expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_60) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 68 "DotGrammar.yrd"
                                          1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_140) 
# 271 "DotParser.fs"
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
                  
# 66 "DotGrammar.yrd"
                                                  1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_139) 
# 293 "DotParser.fs"
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
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_58) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 66 "DotGrammar.yrd"
                                                         1
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_138) 
# 317 "DotParser.fs"
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
                  
# 66 "DotGrammar.yrd"
                                                  1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_138) 
# 339 "DotParser.fs"
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
                
# 57 "DotGrammar.yrd"
                                              p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_137) 
# 359 "DotParser.fs"
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
                
# 57 "DotGrammar.yrd"
                                                               p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_137) 
# 379 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_operator) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_129) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 32 "DotGrammar.yrd"
                                                                              i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_136) 
# 401 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMMA _rnglr_val -> [_rnglr_val] | a -> failwith "COMMA expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_127) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 32 "DotGrammar.yrd"
                                                                              i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_135) 
# 423 "DotParser.fs"
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
                    
# 50 "DotGrammar.yrd"
                                                               l
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_134) 
# 447 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with GRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "GRAPH expected, but %A found" a )
             |> List.iter (fun (n) -> 
              _rnglr_cycle_res := (
                
# 47 "DotGrammar.yrd"
                                                n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_133) 
# 467 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with NODE _rnglr_val -> [_rnglr_val] | a -> failwith "NODE expected, but %A found" a )
             |> List.iter (fun (n) -> 
              _rnglr_cycle_res := (
                
# 47 "DotGrammar.yrd"
                                                             n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_133) 
# 487 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with EDGE _rnglr_val -> [_rnglr_val] | a -> failwith "EDGE expected, but %A found" a )
             |> List.iter (fun (n) -> 
              _rnglr_cycle_res := (
                
# 47 "DotGrammar.yrd"
                                                                          n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_133) 
# 507 "DotParser.fs"
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
                  
# 38 "DotGrammar.yrd"
                                                    l
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_132) 
# 529 "DotParser.fs"
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
                
# 35 "DotGrammar.yrd"
                                               t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_131) 
# 549 "DotParser.fs"
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
                
# 35 "DotGrammar.yrd"
                                                               t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_131) 
# 569 "DotParser.fs"
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
                
# 72 "DotGrammar.yrd"
                           i
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 72 "DotGrammar.yrd"
               : '_rnglr_type_id) 
# 589 "DotParser.fs"
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
                
# 70 "DotGrammar.yrd"
                                   
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 70 "DotGrammar.yrd"
               : '_rnglr_type_compass_pt) 
# 609 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 68 "DotGrammar.yrd"
                          None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 68 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_59) 
# 627 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_140) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 68 "DotGrammar.yrd"
                            Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 68 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_59) 
# 647 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_60) 
# 665 "DotParser.fs"
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
                
# 35 "DotGrammar.yrd"
                                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_60) 
# 685 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_59) 
             |> List.iter (fun (_) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_stmt_list) 
                 |> List.iter (fun (l) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 68 "DotGrammar.yrd"
                                                                                   l
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 68 "DotGrammar.yrd"
               : '_rnglr_type_subgraph) 
# 711 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 66 "DotGrammar.yrd"
                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 66 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_58) 
# 729 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_139) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 66 "DotGrammar.yrd"
                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 66 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_58) 
# 749 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_138) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 66 "DotGrammar.yrd"
                                                                               1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 66 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 769 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeID ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 63 "DotGrammar.yrd"
                                   None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 63 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_57) 
# 787 "DotParser.fs"
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
                
# 63 "DotGrammar.yrd"
                                     Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 63 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_57) 
# 807 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (nodeID) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_57) nodeID
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 64 "DotGrammar.yrd"
                   nodeID
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 63 "DotGrammar.yrd"
               : '_rnglr_type_node_id) 
# 829 "DotParser.fs"
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
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 61 "DotGrammar.yrd"
                   CollectDataFuncs.stmtGetData [nodeID] lst vertices_lists vertices_attrs
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 60 "DotGrammar.yrd"
               : '_rnglr_type_node_stmt) 
# 851 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_137) 
             |> List.iter (fun (op) -> 
              _rnglr_cycle_res := (
                
# 58 "DotGrammar.yrd"
                 CollectDataFuncs.addInfo graph_info [op, ""]
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 57 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 871 "DotParser.fs"
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
                
# 54 "DotGrammar.yrd"
                                                        nodeID
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_129) 
# 891 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_129) 
# 901 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
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
               : '_rnglr_type_yard_many_60) 
# 919 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_136) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_60) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 54 "DotGrammar.yrd"
                                                                              yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_60) 
# 941 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 32 "DotGrammar.yrd"
                                      []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 32 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_130) 
# 959 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_129) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_60) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 32 "DotGrammar.yrd"
                                                                                    hd::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 32 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_130) 
# 981 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_130) 
             |> List.iter (fun (edges) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 55 "DotGrammar.yrd"
                   CollectDataFuncs.stmtGetData edges lst vertices_lists edges_attrs
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_edge_stmt) 
# 1003 "DotParser.fs"
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
                    
# 52 "DotGrammar.yrd"
                                                         (k,v)
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_127) 
# 1027 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 52 "DotGrammar.yrd"
                                                           []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_59) 
# 1045 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_135) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_59) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 52 "DotGrammar.yrd"
                                                               yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_59) 
# 1067 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 32 "DotGrammar.yrd"
                                      []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 32 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_128) 
# 1085 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_127) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_59) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 32 "DotGrammar.yrd"
                                                                                    hd::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 32 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_128) 
# 1107 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_128) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 52 "DotGrammar.yrd"
                                                                     lst
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_a_list) 
# 1127 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 50 "DotGrammar.yrd"
                             []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_58) 
# 1145 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_134) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_58) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 50 "DotGrammar.yrd"
                                 yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_58) 
# 1167 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_58) 
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 50 "DotGrammar.yrd"
                                                                 List.concat l
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_attr_list) 
# 1187 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_133) 
             |> List.iter (fun (attr_name) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 48 "DotGrammar.yrd"
                   CollectDataFuncs.addAttribute attr_name lst general_attrs
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 47 "DotGrammar.yrd"
               : '_rnglr_type_attr_stmt) 
# 1209 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_stmt) 
             |> List.iter (fun (ns) -> 
              _rnglr_cycle_res := (
                
# 41 "DotGrammar.yrd"
                                     
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1229 "DotParser.fs"
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
                
# 42 "DotGrammar.yrd"
                                      
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1249 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_attr_stmt) 
             |> List.iter (fun (attr_s) -> 
              _rnglr_cycle_res := (
                
# 43 "DotGrammar.yrd"
                                      
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1269 "DotParser.fs"
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
                    
# 44 "DotGrammar.yrd"
                                          assign_stmt_list.Add (k, v)
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1293 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_subgraph) 
             |> List.iter (fun (sub) -> 
              _rnglr_cycle_res := (
                
# 45 "DotGrammar.yrd"
                                  
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1313 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 38 "DotGrammar.yrd"
                                 []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_57) 
# 1331 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_132) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_57) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 38 "DotGrammar.yrd"
                                     yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_57) 
# 1353 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_57) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 38 "DotGrammar.yrd"
                                                        
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_stmt_list) 
# 1373 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name -> fun x ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 35 "DotGrammar.yrd"
                                                                            []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_56) 
# 1391 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name -> fun x ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_56) s g name x
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 35 "DotGrammar.yrd"
                                                                                yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_56) 
# 1413 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 35 "DotGrammar.yrd"
                                                                            []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_55) 
# 1431 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g -> fun name ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_55) s g name
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 35 "DotGrammar.yrd"
                                                                                yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_55) 
# 1453 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun s -> fun g ->
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
               : '_rnglr_type_yard_opt_56) 
# 1471 "DotParser.fs"
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
                
# 35 "DotGrammar.yrd"
                                                                         Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_56) 
# 1491 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_55) 
# 1509 "DotParser.fs"
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
                
# 35 "DotGrammar.yrd"
                           Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_55) 
# 1529 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_55) 
             |> List.iter (fun (s) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_131) s
               |> List.iter (fun (g) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_56) s g
                 |> List.iter (fun (name) -> 
                  ((unbox _rnglr_children.[3]) : '_rnglr_type_yard_many_55) s g name
                   |> List.iter (fun (_) -> 
                    (match ((unbox _rnglr_children.[4]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
                     |> List.iter (fun (_) -> 
                      ((unbox _rnglr_children.[5]) : '_rnglr_type_stmt_list) 
                       |> List.iter (fun (x) -> 
                        (match ((unbox _rnglr_children.[6]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                         |> List.iter (fun (_) -> 
                          ((unbox _rnglr_children.[7]) : '_rnglr_type_yard_many_56) s g name x
                           |> List.iter (fun (_) -> 
                            _rnglr_cycle_res := (
                              
# 36 "DotGrammar.yrd"
                               CollectDataFuncs.addInfo graph_info [strict_key, (Utils.optToStr s); type_key, g; name_key, (Utils.optToStr name)]
                                )::!_rnglr_cycle_res ) ) ) ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_graph) 
# 1563 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          ((unbox _rnglr_children.[0]) : '_rnglr_type_graph) 
            )
# 35 "DotGrammar.yrd"
               : '_rnglr_type_yard_start_rule) 
# 1573 "DotParser.fs"
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
# 1591 "DotParser.fs"
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
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_131)  s ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_132)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_133)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_134)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_135)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_136)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_137)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_138)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_139)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_140)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s -> fun g -> fun name ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_55)  s g name ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s -> fun g -> fun name -> fun x ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_56)  s g name x ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_57)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_58)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_59)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_60)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_55)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun s -> fun g ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_56)  s g ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun nodeID ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_57)  nodeID ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_58)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_59)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_60)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_127)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_129)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_128)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_130)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_start_rule)   ) |> List.concat));
  |] 
let translate (args : TranslateArguments<_,_>) (tree : Tree<_>) (dict : _ ) : '_rnglr_type_yard_start_rule = 
  unbox (tree.Translate _rnglr_rule_  leftSide _rnglr_concats (if args.filterEpsilons then _rnglr_filtered_epsilons else _rnglr_epsilons) args.tokenToRange args.zeroPosition args.clearAST dict) : '_rnglr_type_yard_start_rule
