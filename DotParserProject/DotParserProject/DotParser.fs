
# 2 "DotParser.fs"
module DotParserProject.DotParser
#nowarn "64";; // From fsyacc: turn off warnings that type variables used in production annotations are instantiated to concrete type
open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR
open Yard.Generators.RNGLR.AST

# 1 "DotGrammar.yrd"

open System.Collections.Generic
open ParsingFuncs
let adj_list = new Dictionary<string, HashSet<string>>()

# 15 "DotParser.fs"
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
    | 15 -> "yard_exp_brackets_913"
    | 16 -> "yard_exp_brackets_914"
    | 17 -> "yard_exp_brackets_915"
    | 18 -> "yard_exp_brackets_916"
    | 19 -> "yard_exp_brackets_917"
    | 20 -> "yard_exp_brackets_918"
    | 21 -> "yard_exp_brackets_919"
    | 22 -> "yard_exp_brackets_920"
    | 23 -> "yard_exp_brackets_921"
    | 24 -> "yard_exp_brackets_922"
    | 25 -> "yard_exp_brackets_923"
    | 26 -> "yard_exp_brackets_924"
    | 27 -> "yard_many_253"
    | 28 -> "yard_many_254"
    | 29 -> "yard_many_255"
    | 30 -> "yard_many_256"
    | 31 -> "yard_opt_379"
    | 32 -> "yard_opt_380"
    | 33 -> "yard_opt_381"
    | 34 -> "yard_opt_382"
    | 35 -> "yard_opt_383"
    | 36 -> "yard_opt_384"
    | 37 -> "yard_rule_910"
    | 38 -> "yard_rule_list_911"
    | 39 -> "yard_rule_list_912"
    | 40 -> "yard_start_rule"
    | 41 -> "ASSIGN"
    | 42 -> "COL"
    | 43 -> "COMMA"
    | 44 -> "DIEDGEOP"
    | 45 -> "DIGRAPH"
    | 46 -> "EDGE"
    | 47 -> "EDGEOP"
    | 48 -> "GRAPH"
    | 49 -> "ID"
    | 50 -> "LCURBRACE"
    | 51 -> "LSQBRACE"
    | 52 -> "NODE"
    | 53 -> "RCURBRACE"
    | 54 -> "RNGLR_EOF"
    | 55 -> "RSQBRACE"
    | 56 -> "SEP"
    | 57 -> "STRICT"
    | 58 -> "SUBGR"
    | _ -> ""

let tokenToNumber = function
    | ASSIGN _ -> 41
    | COL _ -> 42
    | COMMA _ -> 43
    | DIEDGEOP _ -> 44
    | DIGRAPH _ -> 45
    | EDGE _ -> 46
    | EDGEOP _ -> 47
    | GRAPH _ -> 48
    | ID _ -> 49
    | LCURBRACE _ -> 50
    | LSQBRACE _ -> 51
    | NODE _ -> 52
    | RCURBRACE _ -> 53
    | RNGLR_EOF _ -> 54
    | RSQBRACE _ -> 55
    | SEP _ -> 56
    | STRICT _ -> 57
    | SUBGR _ -> 58

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
let leftSide = [|26; 26; 25; 24; 23; 23; 22; 21; 21; 20; 19; 18; 18; 18; 17; 16; 15; 15; 9; 3; 35; 35; 36; 36; 14; 34; 34; 12; 33; 33; 10; 11; 4; 4; 30; 30; 39; 39; 5; 37; 29; 29; 38; 38; 0; 28; 28; 1; 2; 7; 7; 7; 7; 7; 27; 27; 13; 32; 32; 31; 31; 8; 40|]
let private rules = [|10; 14; 58; 36; 42; 3; 42; 9; 34; 42; 3; 4; 26; 10; 14; 43; 37; 51; 0; 55; 48; 52; 46; 7; 56; 57; 48; 45; 49; 49; 25; 9; 35; 50; 13; 53; 24; 23; 12; 9; 33; 10; 1; 47; 44; 22; 30; 21; 30; 39; 1; 9; 41; 9; 20; 29; 37; 29; 38; 19; 28; 28; 18; 1; 11; 5; 2; 9; 41; 9; 14; 17; 27; 27; 9; 16; 31; 15; 32; 50; 13; 53; 56; 8|]
let private rulesStart = [|0; 1; 2; 4; 6; 9; 11; 13; 14; 15; 17; 20; 21; 22; 23; 25; 26; 27; 28; 29; 30; 30; 31; 31; 32; 36; 36; 37; 38; 38; 39; 41; 43; 44; 45; 45; 47; 47; 49; 51; 54; 54; 56; 56; 58; 59; 59; 61; 62; 64; 65; 66; 67; 70; 71; 71; 73; 74; 74; 75; 75; 76; 83; 84|]
let startRule = 62

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 3; 82; 4; 80; 81; 5; 6; 18; 7; 8; 9; 10; 12; 28; 46; 47; 50; 51; 52; 54; 59; 65; 61; 66; 68; 69; 70; 71; 11; 13; 14; 15; 16; 19; 17; 20; 21; 27; 22; 23; 24; 25; 26; 29; 30; 45; 32; 31; 33; 35; 38; 44; 34; 36; 37; 39; 43; 41; 40; 42; 48; 49; 79; 53; 55; 74; 78; 76; 77; 56; 57; 58; 60; 62; 63; 64; 67; 72; 73; 75|]
let private small_gotos =
        [|4; 524288; 1048577; 2031618; 3735555; 196611; 983044; 2949125; 3145734; 262147; 589831; 2097160; 3211273; 393217; 3276810; 458772; 131083; 327692; 458765; 589838; 655375; 720912; 851985; 917522; 1114131; 1179668; 1376277; 1638422; 1769495; 2293784; 2555929; 3014682; 3145755; 3211273; 3407900; 3801117; 655361; 3670046; 786437; 786463; 1507360; 2162721; 2687010; 2752547; 1048578; 589860; 3211273; 1245187; 196645; 589862; 3211303; 1376259; 1572904; 2228265; 2752554; 1572866; 196651; 3211308; 1835012; 65581; 1245230; 1835055; 3342384; 1966083; 1245230; 1835057; 3342384; 2097157; 50; 589875; 2424884; 2490421; 3211273; 2162689; 3604534; 2293761; 2687031; 2359298; 589880; 3211273; 2490371; 1310777; 1900602; 2818107; 2555907; 1310777; 1900604; 2818107; 2686979; 589875; 2424893; 3211273; 3080193; 3473470; 3145729; 3670079; 3342355; 131083; 327692; 458765; 589838; 655375; 720912; 917522; 1114131; 1179668; 1376277; 1638422; 1769536; 2293784; 2555929; 3014682; 3145755; 3211273; 3407900; 3801117; 3407876; 65601; 1245230; 1835055; 3342384; 3538949; 262210; 1441859; 1966148; 2883653; 3080262; 3604488; 589895; 655432; 917577; 1638422; 1704010; 2293784; 3211273; 3801117; 3670020; 786463; 1507360; 2162721; 2752547; 3997697; 3276875; 4063252; 131083; 327692; 458765; 589838; 655375; 720912; 852044; 917522; 1114131; 1179668; 1376277; 1638422; 1769495; 2293784; 2555929; 3014682; 3145755; 3211273; 3407900; 3801117; 4128769; 3473485; 4325380; 65614; 1245230; 1835055; 3342384; 4653059; 589903; 2359376; 3211273; 4849669; 262210; 1441859; 1966161; 2883653; 3080262|]
let gotos = Array.zeroCreate 83
for i = 0 to 82 do
        gotos.[i] <- Array.zeroCreate 59
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
let private lists_reduces = [|[|60,1|]; [|58,1|]; [|51,1|]; [|50,1|]; [|14,2|]; [|30,1|]; [|29,1|]; [|27,1|]; [|30,2|]; [|52,3|]; [|18,1|]; [|5,2|]; [|4,2|]; [|26,1|]; [|4,3|]; [|3,2|]; [|19,1|]; [|19,1; 18,1|]; [|7,1|]; [|31,1; 7,1|]; [|31,2|]; [|46,1|]; [|46,2|]; [|10,3|]; [|39,3|]; [|43,1|]; [|41,1|]; [|41,2|]; [|9,2|]; [|43,2|]; [|44,1|]; [|47,1|]; [|49,1|]; [|61,7|]; [|8,1|]; [|53,1; 8,1|]; [|55,1|]; [|48,1|]; [|48,2|]; [|37,1|]; [|0,1|]; [|1,1|]; [|21,1|]; [|6,2|]; [|24,4|]; [|56,1|]; [|38,1|]; [|38,2|]; [|13,1|]; [|11,1|]; [|12,1|]; [|2,1|]; [|23,1|]; [|2,2|]; [|35,1|]; [|35,2|]; [|33,1|]; [|32,1|]; [|37,2|]; [|55,2|]; [|17,1|]; [|16,1|]; [|15,1|]|]
let private small_reduces =
        [|131074; 2949120; 3145728; 327681; 3276801; 524289; 3670018; 589825; 3670019; 720905; 3014660; 3145732; 3211268; 3276804; 3342340; 3407876; 3473412; 3670020; 3801092; 786436; 2883589; 3080197; 3342341; 3670021; 851972; 2883590; 3080198; 3342342; 3670022; 917508; 2883591; 3080199; 3342343; 3670023; 983044; 2883592; 3080200; 3342344; 3670024; 1114113; 3670025; 1179657; 2686986; 2752522; 2818058; 2883594; 3080202; 3276810; 3342346; 3604490; 3670026; 1310724; 2883595; 3080203; 3342347; 3670027; 1376260; 2883596; 3080204; 3342348; 3670028; 1441796; 2883597; 3080205; 3342349; 3670029; 1507332; 2883598; 3080206; 3342350; 3670030; 1638404; 2883599; 3080207; 3342351; 3670031; 1703940; 2883600; 3080208; 3342352; 3670032; 1769477; 2752522; 2883601; 3080209; 3342353; 3670033; 1835012; 2883602; 3080210; 3342354; 3670035; 1900545; 3670036; 1966081; 3670037; 2031617; 3670038; 2228226; 3342359; 3670039; 2424834; 2818072; 3604504; 2490369; 3604505; 2555905; 3604506; 2621441; 3604507; 2752514; 2818076; 3604508; 2818049; 3604509; 2883585; 3604510; 2949121; 3670047; 3014657; 3670048; 3211265; 3538977; 3276804; 2883618; 3080226; 3342370; 3670051; 3342337; 3473444; 3407873; 3670053; 3473409; 3670054; 3538946; 3342375; 3670055; 3670020; 2883589; 3080197; 3342341; 3670021; 3735556; 2883624; 3080232; 3342376; 3670056; 3801092; 2883625; 3080233; 3342377; 3670057; 3866625; 3276842; 3932164; 2883627; 3080235; 3342379; 3670059; 4194308; 2883628; 3080236; 3342380; 3670060; 4259841; 3473453; 4325377; 3670062; 4390913; 3670063; 4456450; 3342384; 3670064; 4521986; 3342385; 3670065; 4587522; 3342386; 3670066; 4653057; 3276851; 4718593; 3276852; 4784129; 3276853; 4849666; 3342390; 3670070; 4915202; 3342391; 3670071; 4980739; 3211320; 3276856; 3801144; 5046275; 3211321; 3276857; 3801145; 5111810; 3342394; 3670074; 5177345; 3473467; 5242882; 3211324; 3276860; 5308418; 3211325; 3276861; 5373954; 2949182; 3145790|]
let reduces = Array.zeroCreate 83
for i = 0 to 82 do
        reduces.[i] <- Array.zeroCreate 59
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
let private lists_zeroReduces = [|[|59|]; [|57|]; [|20|]; [|36|]; [|56; 54|]; [|50; 38; 36|]; [|28|]; [|25|]; [|47; 45|]; [|45|]; [|44; 42|]; [|40|]; [|54|]; [|34|]; [|22|]|]
let private small_zeroReduces =
        [|2; 2949120; 3145728; 262145; 3276801; 458756; 3276802; 3342339; 3473412; 3670021; 786436; 2883590; 3080198; 3342342; 3670022; 1376260; 2883591; 3080199; 3342343; 3670023; 1835009; 3670024; 1966081; 3670025; 2097153; 3604490; 2490369; 3604491; 2555905; 3604491; 3342340; 3276802; 3342339; 3473420; 3670021; 3407873; 3670024; 3538946; 3342349; 3670029; 3604481; 3276802; 3670020; 2883590; 3080198; 3342342; 3670022; 4063236; 3276802; 3342339; 3473412; 3670021; 4325377; 3670024; 4653057; 3276814; 4849666; 3342349; 3670029|]
let zeroReduces = Array.zeroCreate 83
for i = 0 to 82 do
        zeroReduces.[i] <- Array.zeroCreate 59
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
let eofIndex = 54
let errorIndex = 6
let errorRulesExists = false
let private parserSource = new ParserSource<Token> (gotos, reduces, zeroReduces, accStates, rules, rulesStart, leftSide, startRule, eofIndex, tokenToNumber, acceptEmptyInput, numToString, errorIndex, errorRulesExists)
let buildAst : (seq<Token> -> ParseResult<Token>) =
    buildAst<Token> parserSource

let _rnglr_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(44, new Nodes([|box (new AST(new Family(42, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(47, new Nodes([|box (new AST(new Family(45, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(38, new Nodes([|box (new AST(new Family(36, new Nodes([||])), null)); box (new AST(new Family(47, new Nodes([|box (new AST(new Family(45, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(63, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(50, new Nodes([|box (new AST(new Family(38, new Nodes([|box (new AST(new Family(36, new Nodes([||])), null)); box (new AST(new Family(47, new Nodes([|box (new AST(new Family(45, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(56, new Nodes([|box (new AST(new Family(54, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(59, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(28, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(20, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); null; new Tree<_>(null,box (new AST(new Family(42, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(36, new Nodes([||])), null)), null); null|]
let _rnglr_filtered_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(44, new Nodes([|box (new AST(new Family(42, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(47, new Nodes([|box (new AST(new Family(45, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(38, new Nodes([|box (new AST(new Family(36, new Nodes([||])), null)); box (new AST(new Family(47, new Nodes([|box (new AST(new Family(45, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(63, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(50, new Nodes([|box (new AST(new Family(38, new Nodes([|box (new AST(new Family(36, new Nodes([||])), null)); box (new AST(new Family(47, new Nodes([|box (new AST(new Family(45, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(56, new Nodes([|box (new AST(new Family(54, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(54, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(45, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(40, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(34, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(59, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(28, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(20, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); null; new Tree<_>(null,box (new AST(new Family(42, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(36, new Nodes([||])), null)), null); null|]
for x in _rnglr_filtered_epsilons do if x <> null then x.ChooseSingleAst()
let _rnglr_extra_array, _rnglr_rule_, _rnglr_concats = 
  (Array.zeroCreate 0 : array<'_rnglr_type_a_list * '_rnglr_type_attr_list * '_rnglr_type_attr_stmt * '_rnglr_type_compass_pt * '_rnglr_type_edge_operator * '_rnglr_type_edge_stmt * '_rnglr_type_error * '_rnglr_type_full_stmt * '_rnglr_type_graph * '_rnglr_type_id * '_rnglr_type_node_id * '_rnglr_type_node_stmt * '_rnglr_type_port * '_rnglr_type_stmt_list * '_rnglr_type_subgraph * '_rnglr_type_yard_exp_brackets_913 * '_rnglr_type_yard_exp_brackets_914 * '_rnglr_type_yard_exp_brackets_915 * '_rnglr_type_yard_exp_brackets_916 * '_rnglr_type_yard_exp_brackets_917 * '_rnglr_type_yard_exp_brackets_918 * '_rnglr_type_yard_exp_brackets_919 * '_rnglr_type_yard_exp_brackets_920 * '_rnglr_type_yard_exp_brackets_921 * '_rnglr_type_yard_exp_brackets_922 * '_rnglr_type_yard_exp_brackets_923 * '_rnglr_type_yard_exp_brackets_924 * '_rnglr_type_yard_many_253 * '_rnglr_type_yard_many_254 * '_rnglr_type_yard_many_255 * '_rnglr_type_yard_many_256 * '_rnglr_type_yard_opt_379 * '_rnglr_type_yard_opt_380 * '_rnglr_type_yard_opt_381 * '_rnglr_type_yard_opt_382 * '_rnglr_type_yard_opt_383 * '_rnglr_type_yard_opt_384 * '_rnglr_type_yard_rule_910 * '_rnglr_type_yard_rule_list_911 * '_rnglr_type_yard_rule_list_912 * '_rnglr_type_yard_start_rule>), 
  [|
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          []
            )

               : '_rnglr_type_yard_exp_brackets_924) 
# 246 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          []
            )

               : '_rnglr_type_yard_exp_brackets_924) 
# 256 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SUBGR _rnglr_val -> [_rnglr_val] | a -> failwith "SUBGR expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_384) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 50 "DotGrammar.yrd"
                                             1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_923) 
# 278 "DotParser.fs"
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
                  
# 48 "DotGrammar.yrd"
                                                   1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_922) 
# 300 "DotParser.fs"
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
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_382) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 48 "DotGrammar.yrd"
                                                          1
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_921) 
# 324 "DotParser.fs"
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
                  
# 48 "DotGrammar.yrd"
                                                   1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_921) 
# 346 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_operator) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_924) hd
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 20 "DotGrammar.yrd"
                                                                              string i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_920) 
# 368 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_919) 
# 378 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_919) 
# 388 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMMA _rnglr_val -> [_rnglr_val] | a -> failwith "COMMA expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_910) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 20 "DotGrammar.yrd"
                                                                              string i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_918) 
# 410 "DotParser.fs"
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
                    
# 36 "DotGrammar.yrd"
                                                               
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_917) 
# 434 "DotParser.fs"
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
                
# 23 "DotGrammar.yrd"
                                                1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_916) 
# 454 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_916) 
# 464 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )

               : '_rnglr_type_yard_exp_brackets_916) 
# 474 "DotParser.fs"
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
                  
# 25 "DotGrammar.yrd"
                                                    l
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_915) 
# 496 "DotParser.fs"
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
                
# 23 "DotGrammar.yrd"
                                  1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_914) 
# 516 "DotParser.fs"
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
                
# 23 "DotGrammar.yrd"
                                                1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_913) 
# 536 "DotParser.fs"
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
                
# 23 "DotGrammar.yrd"
                                                              1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_913) 
# 556 "DotParser.fs"
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
                
# 54 "DotGrammar.yrd"
                            i
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 54 "DotGrammar.yrd"
               : '_rnglr_type_id) 
# 576 "DotParser.fs"
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
                
# 52 "DotGrammar.yrd"
                                   
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_compass_pt) 
# 596 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
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
               : '_rnglr_type_yard_opt_383) 
# 614 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_923) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 50 "DotGrammar.yrd"
                             Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_383) 
# 634 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_384) 
# 652 "DotParser.fs"
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
                
# 23 "DotGrammar.yrd"
                                                                    Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_384) 
# 672 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_383) 
             |> List.iter (fun (_) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_stmt_list) 
                 |> List.iter (fun (l) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 50 "DotGrammar.yrd"
                                                                                      
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 50 "DotGrammar.yrd"
               : '_rnglr_type_subgraph) 
# 698 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 48 "DotGrammar.yrd"
                      None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_382) 
# 716 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_922) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 48 "DotGrammar.yrd"
                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_382) 
# 736 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_921) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 48 "DotGrammar.yrd"
                                                                                1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 48 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 756 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeID ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 46 "DotGrammar.yrd"
                                    None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_381) 
# 774 "DotParser.fs"
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
                
# 46 "DotGrammar.yrd"
                                      Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_381) 
# 794 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (nodeID) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_381) nodeID
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 46 "DotGrammar.yrd"
                                                nodeID
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_node_id) 
# 816 "DotParser.fs"
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
                  
# 44 "DotGrammar.yrd"
                                                          nodeID
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 44 "DotGrammar.yrd"
               : '_rnglr_type_node_stmt) 
# 838 "DotParser.fs"
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
                
# 42 "DotGrammar.yrd"
                                        1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 858 "DotParser.fs"
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
                
# 42 "DotGrammar.yrd"
                                                       1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 42 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 878 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
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
               : '_rnglr_type_yard_many_256) 
# 896 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_920) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_256) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 40 "DotGrammar.yrd"
                                                                     yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_256) 
# 918 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 20 "DotGrammar.yrd"
                                      []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 20 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_912) 
# 936 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_919) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_256) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 20 "DotGrammar.yrd"
                                                                                           (string hd)::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 20 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_912) 
# 958 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_912) 
             |> List.iter (fun (edges) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 40 "DotGrammar.yrd"
                                                                                               ParsingFuncs.AddEdges adj_list edges
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 40 "DotGrammar.yrd"
               : '_rnglr_type_edge_stmt) 
# 980 "DotParser.fs"
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
                    
# 31 "DotGrammar.yrd"
                                          
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_910) 
# 1004 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
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
               : '_rnglr_type_yard_many_255) 
# 1022 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_918) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_255) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 38 "DotGrammar.yrd"
                                                          yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_255) 
# 1044 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 20 "DotGrammar.yrd"
                                      []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 20 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_911) 
# 1062 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_910) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_255) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 20 "DotGrammar.yrd"
                                                                                           (string hd)::tl
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 20 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_911) 
# 1084 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_911) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 38 "DotGrammar.yrd"
                                                                
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_a_list) 
# 1104 "DotParser.fs"
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
               : '_rnglr_type_yard_many_254) 
# 1122 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_917) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_254) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 36 "DotGrammar.yrd"
                                 yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 36 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_254) 
# 1144 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_254) 
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 36 "DotGrammar.yrd"
                                                                
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 36 "DotGrammar.yrd"
               : '_rnglr_type_attr_list) 
# 1164 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_916) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 34 "DotGrammar.yrd"
                                                                       
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 34 "DotGrammar.yrd"
               : '_rnglr_type_attr_stmt) 
# 1186 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1196 "DotParser.fs"
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
                
# 29 "DotGrammar.yrd"
                                      l
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1216 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1226 "DotParser.fs"
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
                    
# 31 "DotGrammar.yrd"
                                          
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1250 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          []
            )
# 27 "DotGrammar.yrd"
               : '_rnglr_type_full_stmt) 
# 1260 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 25 "DotGrammar.yrd"
                                 []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_253) 
# 1278 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_915) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_253) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 25 "DotGrammar.yrd"
                                     yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_253) 
# 1300 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_253) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 25 "DotGrammar.yrd"
                                                        
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_stmt_list) 
# 1320 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_380) 
# 1338 "DotParser.fs"
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
                
# 23 "DotGrammar.yrd"
                                                                    Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_380) 
# 1358 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_379) 
# 1376 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_914) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 23 "DotGrammar.yrd"
                          Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_379) 
# 1396 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_379) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_913) 
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_380) 
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
                            
# 23 "DotGrammar.yrd"
                                                                                                                          
                              )::!_rnglr_cycle_res ) ) ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_graph) 
# 1428 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          ((unbox _rnglr_children.[0]) : '_rnglr_type_graph) 
            )
# 23 "DotGrammar.yrd"
               : '_rnglr_type_yard_start_rule) 
# 1438 "DotParser.fs"
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
# 1456 "DotParser.fs"
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
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_913)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_914)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_915)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_916)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_917)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_918)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_919)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_920)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_921)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_922)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_923)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_924)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_253)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_254)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_255)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_256)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_379)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_380)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun nodeID ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_381)  nodeID ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_382)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_383)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_384)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_910)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_911)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_912)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_start_rule)   ) |> List.concat));
  |] 
let translate (args : TranslateArguments<_,_>) (tree : Tree<_>) (dict : _ ) : '_rnglr_type_yard_start_rule = 
  unbox (tree.Translate _rnglr_rule_  leftSide _rnglr_concats (if args.filterEpsilons then _rnglr_filtered_epsilons else _rnglr_epsilons) args.tokenToRange args.zeroPosition args.clearAST dict) : '_rnglr_type_yard_start_rule
