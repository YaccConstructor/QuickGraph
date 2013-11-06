
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
    | 16 -> "yard_exp_brackets_22"
    | 17 -> "yard_exp_brackets_23"
    | 18 -> "yard_exp_brackets_24"
    | 19 -> "yard_exp_brackets_25"
    | 20 -> "yard_exp_brackets_26"
    | 21 -> "yard_exp_brackets_27"
    | 22 -> "yard_exp_brackets_28"
    | 23 -> "yard_opt_46"
    | 24 -> "yard_opt_47"
    | 25 -> "yard_opt_48"
    | 26 -> "yard_opt_49"
    | 27 -> "yard_opt_50"
    | 28 -> "yard_opt_51"
    | 29 -> "yard_opt_52"
    | 30 -> "yard_opt_53"
    | 31 -> "yard_opt_54"
    | 32 -> "yard_opt_55"
    | 33 -> "yard_opt_56"
    | 34 -> "yard_opt_57"
    | 35 -> "yard_opt_58"
    | 36 -> "yard_opt_59"
    | 37 -> "yard_opt_60"
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
let leftSide = [|22; 21; 20; 20; 19; 19; 18; 18; 18; 17; 16; 16; 9; 3; 36; 36; 37; 37; 15; 35; 35; 12; 12; 34; 34; 10; 33; 33; 11; 5; 5; 32; 32; 4; 31; 31; 6; 30; 30; 29; 29; 0; 28; 28; 27; 27; 1; 2; 13; 13; 13; 13; 13; 25; 25; 26; 26; 14; 24; 24; 23; 23; 8; 38|]
let private rules = [|57; 37; 40; 3; 10; 15; 10; 15; 47; 51; 45; 13; 55; 26; 47; 44; 48; 42; 22; 9; 36; 49; 14; 52; 21; 40; 9; 35; 40; 3; 12; 9; 34; 1; 10; 33; 46; 43; 4; 5; 20; 32; 1; 19; 4; 31; 0; 41; 9; 39; 9; 29; 30; 1; 0; 50; 27; 54; 28; 18; 1; 11; 6; 2; 9; 39; 9; 15; 17; 14; 25; 9; 56; 23; 16; 24; 49; 14; 52; 8|]
let private rulesStart = [|0; 2; 4; 5; 6; 7; 8; 9; 10; 11; 14; 15; 16; 17; 18; 18; 19; 19; 20; 24; 24; 25; 28; 30; 30; 31; 33; 33; 34; 36; 37; 38; 38; 39; 42; 42; 43; 46; 46; 47; 47; 48; 53; 53; 54; 54; 55; 59; 61; 62; 63; 64; 67; 68; 68; 69; 69; 70; 71; 71; 72; 72; 73; 79; 80|]
let startRule = 63

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 77; 3; 75; 76; 4; 5; 14; 6; 7; 8; 9; 23; 39; 40; 73; 43; 44; 45; 47; 60; 65; 61; 66; 67; 68; 69; 10; 11; 12; 15; 13; 16; 17; 22; 18; 19; 20; 21; 24; 25; 26; 27; 28; 35; 29; 30; 31; 34; 32; 33; 36; 37; 38; 41; 42; 72; 46; 48; 51; 58; 59; 49; 50; 52; 53; 54; 55; 56; 57; 62; 63; 64; 70; 71; 74|]
let private small_gotos =
        [|3; 524288; 1507329; 3670018; 131075; 1048579; 2883588; 3080197; 196611; 589830; 1572871; 3145736; 327681; 3211273; 393235; 131082; 393227; 589836; 655373; 720910; 851983; 917520; 983057; 1114130; 1179667; 1245204; 1441813; 1638422; 2359319; 2949144; 3080217; 3145736; 3342362; 3735579; 589828; 786460; 2228253; 2555934; 2621471; 786434; 589856; 3145736; 983044; 196641; 589858; 2752547; 3145736; 1114115; 1376292; 2293797; 2621478; 1310722; 196647; 2752547; 1507331; 65576; 2162729; 3276842; 1703940; 43; 589868; 1769517; 3145736; 1835009; 2555950; 1900546; 589871; 3145736; 1966082; 1900592; 2687025; 2031620; 50; 589868; 1966131; 3145736; 2293761; 3538996; 2359299; 65589; 1835062; 3276842; 2621441; 3604535; 2686996; 131082; 393227; 589836; 655373; 720910; 851983; 917560; 983057; 1114130; 1179667; 1245204; 1441813; 1638422; 1703993; 2359319; 2949144; 3080217; 3145736; 3342362; 3735579; 2949122; 65594; 3276842; 3080196; 262203; 327740; 2818109; 3014718; 3145731; 65599; 2031680; 3276842; 3342344; 589889; 655426; 983107; 1310788; 1441813; 2359319; 3145736; 3735579; 3407875; 786460; 2228253; 2621471; 3604485; 262213; 327740; 2097222; 2818109; 3014718; 3997697; 3211335; 4063251; 131082; 393227; 589836; 655373; 720910; 851983; 917576; 983057; 1114130; 1179667; 1245204; 1441813; 1638422; 2359319; 2949144; 3080217; 3145736; 3342362; 3735579; 4128769; 3407945; 4521987; 589898; 2424907; 3145736; 4784129; 3407948|]
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


