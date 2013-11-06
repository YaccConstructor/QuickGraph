
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
    | 16 -> "yard_exp_brackets_56"
    | 17 -> "yard_exp_brackets_57"
    | 18 -> "yard_exp_brackets_58"
    | 19 -> "yard_exp_brackets_59"
    | 20 -> "yard_exp_brackets_60"
    | 21 -> "yard_exp_brackets_61"
    | 22 -> "yard_exp_brackets_62"
    | 23 -> "yard_exp_brackets_63"
    | 24 -> "yard_exp_brackets_64"
    | 25 -> "yard_opt_106"
    | 26 -> "yard_opt_107"
    | 27 -> "yard_opt_108"
    | 28 -> "yard_opt_109"
    | 29 -> "yard_opt_110"
    | 30 -> "yard_opt_111"
    | 31 -> "yard_opt_112"
    | 32 -> "yard_opt_113"
    | 33 -> "yard_opt_114"
    | 34 -> "yard_opt_115"
    | 35 -> "yard_opt_116"
    | 36 -> "yard_opt_117"
    | 37 -> "yard_opt_118"
    | 38 -> "yard_opt_119"
    | 39 -> "yard_opt_120"
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
let leftSide = [|24; 24; 23; 22; 21; 21; 20; 20; 19; 19; 18; 18; 18; 17; 16; 16; 9; 3; 38; 38; 39; 39; 15; 37; 37; 12; 36; 36; 10; 35; 35; 11; 5; 5; 34; 34; 4; 33; 33; 6; 32; 32; 31; 31; 0; 30; 30; 29; 29; 1; 2; 13; 13; 13; 13; 13; 27; 27; 28; 28; 14; 26; 26; 25; 25; 8; 40|]
let private rules = [|56; 13; 56; 58; 39; 42; 3; 42; 9; 37; 42; 3; 10; 15; 10; 15; 48; 52; 46; 24; 28; 48; 45; 49; 49; 23; 9; 38; 50; 14; 53; 22; 21; 12; 9; 36; 1; 10; 35; 47; 44; 4; 5; 20; 34; 1; 19; 4; 33; 0; 43; 9; 41; 9; 31; 32; 1; 0; 51; 29; 55; 30; 18; 1; 11; 6; 2; 9; 41; 9; 15; 17; 14; 27; 9; 57; 25; 16; 26; 50; 14; 53; 8|]
let private rulesStart = [|0; 1; 3; 5; 7; 10; 12; 13; 14; 15; 16; 17; 18; 19; 21; 22; 23; 24; 25; 25; 26; 26; 27; 31; 31; 32; 33; 33; 34; 36; 36; 37; 39; 40; 41; 41; 42; 45; 45; 46; 49; 49; 50; 50; 51; 56; 56; 57; 57; 58; 62; 64; 65; 66; 67; 70; 71; 71; 72; 72; 73; 74; 74; 75; 75; 76; 82; 83|]
let startRule = 66

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 81; 3; 79; 80; 4; 5; 15; 6; 7; 8; 9; 25; 41; 42; 44; 46; 47; 48; 50; 63; 68; 70; 64; 72; 73; 74; 75; 76; 10; 11; 12; 13; 16; 14; 17; 18; 24; 19; 20; 21; 22; 23; 26; 27; 28; 29; 30; 37; 31; 32; 33; 36; 34; 35; 38; 39; 40; 43; 45; 49; 51; 54; 61; 62; 52; 53; 55; 56; 57; 58; 59; 60; 65; 66; 67; 69; 71; 77; 78|]
let private small_gotos =
        [|3; 524288; 1638401; 3735554; 131075; 1048579; 2949124; 3145733; 196611; 589830; 1703943; 3211272; 327681; 3276809; 393237; 131082; 393227; 589836; 655373; 720910; 851983; 917520; 983057; 1114130; 1179667; 1245204; 1507349; 1572886; 1769495; 2490392; 3014681; 3145754; 3211272; 3407899; 3670044; 3801117; 589829; 786462; 1376287; 2359328; 2687009; 2752546; 851970; 589859; 3211272; 1048579; 196644; 589861; 3211302; 1179651; 1441831; 2424872; 2752553; 1376258; 196650; 3211307; 1638403; 65580; 2293805; 3342382; 1835012; 47; 589872; 1900593; 3211272; 1966081; 2687026; 2031618; 589875; 3211272; 2097154; 2031668; 2818101; 2162692; 54; 589872; 2097207; 3211272; 2424833; 3604536; 2490371; 65593; 1966138; 3342382; 2752513; 3670075; 2883585; 3473468; 3145730; 65597; 3342382; 3276804; 262206; 327743; 2883648; 3080257; 3342339; 65602; 2162755; 3342382; 3538952; 589892; 655429; 983110; 1310791; 1507349; 2490392; 3211272; 3801117; 3604484; 786462; 1376287; 2359328; 2752546; 3801093; 262216; 327743; 2228297; 2883648; 3080257; 4194305; 3276874; 4259861; 131082; 393227; 589836; 655373; 720910; 851983; 917579; 983057; 1114130; 1179667; 1245204; 1507349; 1572886; 1769495; 2490392; 3014681; 3145754; 3211272; 3407899; 3670044; 3801117; 4325377; 3473484; 4456470; 131082; 393227; 589836; 655373; 720910; 851983; 917581; 983057; 1114130; 1179667; 1245204; 1507349; 1572886; 1769495; 1835086; 2490392; 3014681; 3145754; 3211272; 3407899; 3670044; 3801117; 4980739; 589903; 2555984; 3211272|]
let gotos = Array.zeroCreate 82
for i = 0 to 81 do
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
let private lists_reduces = [|[|62,1|]; [|53,1|]; [|52,1|]; [|28,1|]; [|27,1|]; [|25,1|]; [|28,2|]; [|54,3|]; [|16,1|]; [|5,2|]; [|4,2|]; [|24,1|]; [|4,3|]; [|3,2|]; [|17,1|]; [|17,1; 16,1|]; [|8,1|]; [|31,1|]; [|30,1|]; [|31,2|]; [|48,1|]; [|44,3|]; [|44,4|]; [|41,1|]; [|44,5|]; [|43,1|]; [|49,3|]; [|46,1|]; [|49,4|]; [|51,1|]; [|1,2|]; [|65,6|]; [|9,1|]; [|55,1|]; [|57,1|]; [|50,2|]; [|39,2|]; [|38,1|]; [|39,3|]; [|6,1|]; [|7,1|]; [|36,2|]; [|35,1|]; [|36,3|]; [|33,1|]; [|32,1|]; [|19,1|]; [|22,4|]; [|13,1|]; [|59,1|]; [|60,1|]; [|13,2|]; [|12,1|]; [|10,1|]; [|11,1|]; [|0,1|]; [|2,1|]; [|21,1|]; [|2,2|]; [|15,1|]; [|14,1|]; [|64,1|]|]
let private small_reduces =
        [|262145; 3276800; 458753; 3670017; 524289; 3670018; 589828; 2883587; 3080195; 3342339; 3670019; 655364; 2883588; 3080196; 3342340; 3670020; 720900; 2883589; 3080197; 3342341; 3670021; 786436; 2883590; 3080198; 3342342; 3670022; 917505; 3670023; 983050; 2686984; 2752520; 2818056; 2883592; 3080200; 3211272; 3276808; 3342344; 3604488; 3670024; 1114116; 2883593; 3080201; 3342345; 3670025; 1179652; 2883594; 3080202; 3342346; 3670026; 1245188; 2883595; 3080203; 3342347; 3670027; 1310724; 2883596; 3080204; 3342348; 3670028; 1441796; 2883597; 3080205; 3342349; 3670029; 1507332; 2883598; 3080206; 3342350; 3670030; 1572869; 2752520; 2883599; 3080207; 3342351; 3670031; 1638403; 2883600; 3080208; 3670033; 1703937; 3670034; 1769473; 3670035; 1900545; 3604500; 2097153; 3604501; 2162689; 3604502; 2228225; 3604503; 2293761; 3604504; 2359298; 3211289; 3604505; 2490369; 3670042; 2555905; 3670043; 2621441; 3670044; 2686977; 3670045; 2818056; 3014686; 3145758; 3211294; 3276830; 3407902; 3473438; 3670046; 3801118; 2949121; 3538975; 3014659; 2883616; 3080224; 3670049; 3080193; 3473442; 3211265; 3670051; 3342337; 3670052; 3407873; 3670053; 3473409; 3670054; 3604484; 2883587; 3080195; 3342339; 3670019; 3670020; 2883623; 3080231; 3342375; 3670055; 3735556; 2883624; 3080232; 3342376; 3670056; 3801090; 3342377; 3670057; 3866626; 3342378; 3670058; 3932162; 3342379; 3670059; 3997699; 3211308; 3276844; 3801132; 4063235; 3211309; 3276845; 3801133; 4128769; 3276846; 4390916; 2883631; 3080239; 3342383; 3670063; 4456449; 3473456; 4521985; 3473457; 4587521; 3473458; 4653057; 3473459; 4718593; 3342388; 4784129; 3342389; 4849665; 3342390; 4915208; 3014711; 3145783; 3211319; 3276855; 3407927; 3473463; 3670071; 3801143; 4980737; 3276856; 5046273; 3276857; 5111809; 3276858; 5177346; 3211323; 3276859; 5242882; 3211324; 3276860; 5308418; 2949181; 3145789|]
let reduces = Array.zeroCreate 82
for i = 0 to 81 do
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
let private lists_zeroReduces = [|[|63|]; [|61|]; [|18|]; [|60; 56|]; [|26|]; [|23|]; [|29|]; [|47|]; [|42|]; [|40|]; [|45|]; [|37|]; [|34|]; [|60; 59; 58; 56|]; [|20|]|]
let private small_zeroReduces =
        [|2; 2949120; 3145728; 196609; 3276801; 393218; 3276802; 3473411; 589828; 2883588; 3080196; 3342340; 3670020; 1179652; 2883589; 3080197; 3342341; 3670021; 1638401; 3670022; 1835009; 3604487; 2097154; 3211272; 3604488; 2162689; 3604489; 2490369; 3670026; 3342337; 3670027; 3538945; 3276802; 3604484; 2883588; 3080196; 3342340; 3670020; 3801090; 3342348; 3670028; 4259842; 3276802; 3473411; 4456450; 3276802; 3473421; 4980737; 3276814|]
let zeroReduces = Array.zeroCreate 82
for i = 0 to 81 do
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
let private accStates = Array.zeroCreate 82
for i = 0 to 81 do
        accStates.[i] <- List.exists ((=) i) small_acc
let eofIndex = 54
let errorIndex = 7
let errorRulesExists = false
let private parserSource = new ParserSource<Token> (gotos, reduces, zeroReduces, accStates, rules, rulesStart, leftSide, startRule, eofIndex, tokenToNumber, acceptEmptyInput, numToString, errorIndex, errorRulesExists)
let buildAst : (seq<Token> -> ParseResult<Token>) =
    buildAst<Token> parserSource


