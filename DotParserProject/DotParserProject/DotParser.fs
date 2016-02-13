
# 2 "DotParser.fs"
module DotParserProject.DotParser
#nowarn "64";; // From fsyacc: turn off warnings that type variables used in production annotations are instantiated to concrete type
open Yard.Generators.RNGLR.Parser
open Yard.Generators.RNGLR
open Yard.Generators.RNGLR.AST

# 1 "DotGrammar.yrd"

open System.Collections.Generic
open DotParserProject.GraphDataContainer

let graphs = new ResizeArray<GraphDataContainer>()


# 17 "DotParser.fs"
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
    | 7 -> "graph"
    | 8 -> "id"
    | 9 -> "node_id"
    | 10 -> "node_stmt"
    | 11 -> "port"
    | 12 -> "stmt"
    | 13 -> "stmt_list"
    | 14 -> "subgraph"
    | 15 -> "yard_exp_brackets_367"
    | 16 -> "yard_exp_brackets_368"
    | 17 -> "yard_exp_brackets_369"
    | 18 -> "yard_exp_brackets_370"
    | 19 -> "yard_exp_brackets_371"
    | 20 -> "yard_exp_brackets_372"
    | 21 -> "yard_exp_brackets_373"
    | 22 -> "yard_exp_brackets_374"
    | 23 -> "yard_exp_brackets_375"
    | 24 -> "yard_exp_brackets_376"
    | 25 -> "yard_many_119"
    | 26 -> "yard_many_120"
    | 27 -> "yard_many_121"
    | 28 -> "yard_many_122"
    | 29 -> "yard_opt_154"
    | 30 -> "yard_opt_155"
    | 31 -> "yard_opt_156"
    | 32 -> "yard_opt_157"
    | 33 -> "yard_opt_158"
    | 34 -> "yard_opt_159"
    | 35 -> "yard_opt_160"
    | 36 -> "yard_rule_362"
    | 37 -> "yard_rule_364"
    | 38 -> "yard_rule_365"
    | 39 -> "yard_rule_list_363"
    | 40 -> "yard_rule_list_366"
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
let leftSide = [|24; 23; 22; 22; 21; 21; 20; 19; 18; 17; 17; 17; 16; 15; 15; 8; 3; 34; 34; 35; 35; 14; 33; 33; 11; 32; 32; 9; 10; 4; 37; 37; 38; 28; 28; 40; 40; 5; 36; 27; 27; 39; 39; 0; 26; 26; 1; 2; 12; 12; 12; 12; 12; 25; 25; 31; 31; 13; 30; 30; 29; 29; 7; 41|]
let private rules = [|59; 35; 43; 3; 43; 8; 33; 43; 3; 48; 45; 38; 37; 44; 36; 52; 0; 56; 49; 53; 47; 12; 31; 49; 46; 50; 50; 24; 8; 34; 51; 13; 54; 23; 22; 11; 8; 32; 9; 1; 21; 9; 14; 4; 20; 28; 37; 28; 40; 1; 8; 42; 8; 19; 27; 36; 27; 39; 18; 26; 26; 17; 1; 10; 5; 2; 8; 42; 8; 14; 16; 25; 57; 25; 8; 58; 29; 15; 30; 51; 13; 54; 7|]
let private rulesStart = [|0; 2; 4; 7; 9; 10; 11; 13; 15; 18; 19; 20; 21; 23; 24; 25; 26; 27; 27; 28; 28; 29; 33; 33; 34; 35; 35; 36; 38; 40; 41; 42; 43; 44; 44; 46; 46; 48; 50; 53; 53; 55; 55; 57; 58; 58; 60; 61; 63; 64; 65; 66; 69; 70; 70; 72; 72; 73; 74; 74; 75; 75; 76; 82; 83|]
let startRule = 63

let acceptEmptyInput = false

let defaultAstToDot =
    (fun (tree : Yard.Generators.RNGLR.AST.Tree<Token>) -> tree.AstToDot numToString tokenToNumber leftSide)

let private lists_gotos = [|1; 2; 83; 3; 81; 82; 4; 5; 15; 6; 7; 8; 9; 25; 43; 44; 47; 49; 50; 51; 53; 59; 55; 60; 76; 78; 79; 80; 70; 10; 11; 12; 13; 16; 14; 17; 18; 24; 19; 20; 21; 22; 23; 26; 27; 42; 29; 28; 30; 32; 35; 41; 31; 33; 34; 36; 40; 38; 37; 39; 45; 46; 48; 54; 52; 56; 57; 58; 61; 62; 63; 75; 65; 73; 74; 64; 66; 67; 68; 69; 71; 72; 77|]
let private small_gotos =
        [|3; 458752; 1900545; 3801090; 131075; 983043; 3014660; 3211269; 196611; 524294; 1966087; 3276808; 327681; 3342345; 393236; 131082; 327691; 524300; 589837; 655374; 786447; 851984; 917521; 1048594; 1114131; 1572884; 1638421; 2228246; 2424855; 2621464; 3080217; 3211290; 3276808; 3473435; 3866652; 589829; 720925; 1441822; 2097183; 2752544; 2818081; 851970; 524322; 3276808; 1048579; 196643; 524324; 3276837; 1179651; 1507366; 2162727; 2818088; 1376258; 196649; 3276842; 1638404; 65579; 1179692; 1703981; 3407918; 1769475; 1179692; 1703983; 3407918; 1900549; 48; 524337; 2359346; 2555955; 3276808; 1966081; 3670068; 2097153; 2752565; 2162690; 524342; 3276808; 2293763; 1245239; 1769528; 2883641; 2359299; 1245239; 1769530; 2883641; 2490371; 524337; 2359355; 3276808; 2883586; 2031676; 3735613; 3080193; 3539006; 3276819; 131082; 327691; 524300; 589837; 655374; 786447; 917521; 1048594; 1114131; 1572884; 1638463; 2228246; 2424855; 2621464; 3080217; 3211290; 3276808; 3473435; 3866652; 3342340; 65600; 1179692; 1703981; 3407918; 3604481; 3342401; 3670036; 131082; 327691; 524300; 589837; 655374; 786447; 852034; 917521; 1048594; 1114131; 1572884; 1638421; 2228246; 2424855; 2621464; 3080217; 3211290; 3276808; 3473435; 3866652; 3735553; 3539011; 3932167; 262212; 1310789; 1376326; 1835079; 2490440; 2949193; 3145802; 4063239; 262212; 1310789; 1376326; 1835083; 2490440; 2949193; 3145802; 4259848; 524364; 589901; 917582; 1572884; 2228246; 2424911; 3276808; 3866652; 4325380; 720925; 1441822; 2097183; 2818081; 4587523; 524368; 2293841; 3276808; 4980740; 65618; 1179692; 1703981; 3407918|]
let gotos = Array.zeroCreate 84
for i = 0 to 83 do
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
let private lists_reduces = [|[|59,1|]; [|50,1|]; [|49,1|]; [|27,1|]; [|26,1|]; [|24,1|]; [|27,2|]; [|51,3|]; [|15,1|]; [|3,2|]; [|2,2|]; [|23,1|]; [|2,3|]; [|1,2|]; [|16,1|]; [|16,1; 15,1|]; [|30,1|]; [|30,1; 28,1|]; [|28,2|]; [|45,1|]; [|45,2|]; [|8,3|]; [|38,3|]; [|42,1|]; [|40,1|]; [|40,2|]; [|7,2|]; [|42,2|]; [|43,1|]; [|46,1|]; [|48,1|]; [|12,1|]; [|12,2|]; [|56,1|]; [|62,6|]; [|31,1|]; [|52,1; 31,1|]; [|54,1|]; [|47,1|]; [|47,2|]; [|18,1|]; [|54,2|]; [|21,4|]; [|57,1|]; [|36,1|]; [|32,1|]; [|34,1|]; [|29,1|]; [|34,2|]; [|6,2|]; [|0,1|]; [|20,1|]; [|0,2|]; [|5,1|]; [|4,1|]; [|36,2|]; [|37,1|]; [|37,2|]; [|11,1|]; [|9,1|]; [|10,1|]; [|14,1|]; [|13,1|]; [|61,1|]|]
let private small_reduces =
        [|262145; 3342336; 458761; 3080193; 3211265; 3276801; 3342337; 3407873; 3473409; 3538945; 3735553; 3866625; 524297; 3080194; 3211266; 3276802; 3342338; 3407874; 3473410; 3538946; 3735554; 3866626; 589835; 2949123; 3080195; 3145731; 3211267; 3276803; 3342339; 3407875; 3473411; 3538947; 3735555; 3866627; 655371; 2949124; 3080196; 3145732; 3211268; 3276804; 3342340; 3407876; 3473412; 3538948; 3735556; 3866628; 720907; 2949125; 3080197; 3145733; 3211269; 3276805; 3342341; 3407877; 3473413; 3538949; 3735557; 3866629; 786443; 2949126; 3080198; 3145734; 3211270; 3276806; 3342342; 3407878; 3473414; 3538950; 3735558; 3866630; 917513; 3080199; 3211271; 3276807; 3342343; 3407879; 3473415; 3538951; 3735559; 3866631; 983055; 2752520; 2818056; 2883592; 2949128; 3080200; 3145736; 3211272; 3276808; 3342344; 3407880; 3473416; 3538952; 3670024; 3735560; 3866632; 1114123; 2949129; 3080201; 3145737; 3211273; 3276809; 3342345; 3407881; 3473417; 3538953; 3735561; 3866633; 1179659; 2949130; 3080202; 3145738; 3211274; 3276810; 3342346; 3407882; 3473418; 3538954; 3735562; 3866634; 1245195; 2949131; 3080203; 3145739; 3211275; 3276811; 3342347; 3407883; 3473419; 3538955; 3735563; 3866635; 1310731; 2949132; 3080204; 3145740; 3211276; 3276812; 3342348; 3407884; 3473420; 3538956; 3735564; 3866636; 1441803; 2949133; 3080205; 3145741; 3211277; 3276813; 3342349; 3407885; 3473421; 3538957; 3735565; 3866637; 1507339; 2949134; 3080206; 3145742; 3211278; 3276814; 3342350; 3407886; 3473422; 3538958; 3735566; 3866638; 1572876; 2818056; 2949135; 3080207; 3145743; 3211279; 3276815; 3342351; 3407887; 3473423; 3538959; 3735567; 3866639; 1638411; 2949136; 3080209; 3145744; 3211281; 3276817; 3342353; 3407889; 3473425; 3538961; 3735569; 3866641; 1703945; 3080210; 3211282; 3276818; 3342354; 3407890; 3473426; 3538962; 3735570; 3866642; 1769481; 3080211; 3211283; 3276819; 3342355; 3407891; 3473427; 3538963; 3735571; 3866643; 1835017; 3080212; 3211284; 3276820; 3342356; 3407892; 3473428; 3538964; 3735572; 3866644; 2031625; 3080213; 3211285; 3276821; 3342357; 3407893; 3473429; 3538965; 3735573; 3866645; 2228226; 2883606; 3670038; 2293761; 3670039; 2359297; 3670040; 2424833; 3670041; 2555906; 2883610; 3670042; 2621441; 3670043; 2686977; 3670044; 2752521; 3080221; 3211293; 3276829; 3342365; 3407901; 3473437; 3538973; 3735581; 3866653; 2818057; 3080222; 3211294; 3276830; 3342366; 3407902; 3473438; 3538974; 3735582; 3866654; 2883593; 3080223; 3211295; 3276831; 3342367; 3407903; 3473439; 3538975; 3735583; 3866655; 2949129; 3080224; 3211296; 3276832; 3342368; 3407904; 3473440; 3538976; 3735584; 3866656; 3014665; 3080225; 3211297; 3276833; 3342369; 3407905; 3473441; 3538977; 3735585; 3866657; 3145729; 3604514; 3211275; 2949155; 3080228; 3145763; 3211300; 3276836; 3342372; 3407908; 3473444; 3538980; 3735588; 3866660; 3276801; 3538981; 3342345; 3080230; 3211302; 3276838; 3342374; 3407910; 3473446; 3538982; 3735590; 3866662; 3407881; 3080231; 3211303; 3276839; 3342375; 3407911; 3473447; 3538983; 3735591; 3866663; 3473409; 3342376; 3538945; 3538985; 3801099; 2949162; 3080234; 3145770; 3211306; 3276842; 3342378; 3407914; 3473450; 3538986; 3735594; 3866666; 3866625; 3538987; 3932169; 3080236; 3211308; 3276844; 3342380; 3407916; 3473452; 3538988; 3735596; 3866668; 3997699; 3276845; 3342381; 3866669; 4063241; 3080238; 3211310; 3276846; 3342382; 3407918; 3473454; 3538990; 3735598; 3866670; 4128771; 3276847; 3342383; 3866671; 4194313; 3080240; 3211312; 3276848; 3342384; 3407920; 3473456; 3538992; 3735600; 3866672; 4325387; 2949123; 3080195; 3145731; 3211267; 3276803; 3342339; 3407875; 3473411; 3538947; 3735555; 3866627; 4390923; 2949136; 3080208; 3145744; 3211280; 3276816; 3342352; 3407888; 3473424; 3538960; 3735568; 3866640; 4456459; 2949155; 3080227; 3145763; 3211299; 3276835; 3342371; 3407907; 3473443; 3538979; 3735587; 3866659; 4521995; 2949169; 3080241; 3145777; 3211313; 3276849; 3342385; 3407921; 3473457; 3538993; 3735601; 3866673; 4587521; 3342386; 4653057; 3342387; 4718593; 3342388; 4784131; 3276853; 3342389; 3866677; 4849667; 3276854; 3342390; 3866678; 4915209; 3080247; 3211319; 3276855; 3342391; 3407927; 3473463; 3538999; 3735607; 3866679; 4980745; 3080248; 3211320; 3276856; 3342392; 3407928; 3473464; 3539000; 3735608; 3866680; 5046281; 3080249; 3211321; 3276857; 3342393; 3407929; 3473465; 3539001; 3735609; 3866681; 5111817; 3080250; 3211322; 3276858; 3342394; 3407930; 3473466; 3539002; 3735610; 3866682; 5177353; 3080251; 3211323; 3276859; 3342395; 3407931; 3473467; 3539003; 3735611; 3866683; 5242889; 3080252; 3211324; 3276860; 3342396; 3407932; 3473468; 3539004; 3735612; 3866684; 5308418; 3276861; 3342397; 5373954; 3276862; 3342398; 5439490; 3014719; 3211327|]
let reduces = Array.zeroCreate 84
for i = 0 to 83 do
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
let private lists_zeroReduces = [|[|60|]; [|58|]; [|49; 37; 35; 12|]; [|49; 37; 35; 17; 12|]; [|57; 54; 53; 49; 37; 35; 12|]; [|25|]; [|22|]; [|46; 44|]; [|44|]; [|43; 41|]; [|39|]; [|55|]; [|54; 53; 49; 37; 35; 12|]; [|33|]; [|17|]; [|19|]|]
let private small_zeroReduces =
        [|2; 3014656; 3211264; 196609; 3342337; 393225; 3080194; 3211266; 3276802; 3342339; 3407874; 3473410; 3538948; 3735554; 3866626; 589835; 2949125; 3080197; 3145733; 3211269; 3276805; 3342341; 3407877; 3473413; 3538949; 3735557; 3866629; 1179659; 2949126; 3080198; 3145734; 3211270; 3276806; 3342342; 3407878; 3473414; 3538950; 3735558; 3866630; 1638409; 3080199; 3211271; 3276807; 3342343; 3407879; 3473415; 3538951; 3735559; 3866631; 1769481; 3080200; 3211272; 3276808; 3342344; 3407880; 3473416; 3538952; 3735560; 3866632; 1900545; 3670025; 2293761; 3670026; 2359297; 3670026; 2883593; 3080203; 3211275; 3276811; 3342347; 3407883; 3473419; 3538955; 3735563; 3866635; 3276809; 3080194; 3211266; 3276802; 3342339; 3407874; 3473410; 3538956; 3735554; 3866626; 3342345; 3080199; 3211271; 3276807; 3342343; 3407879; 3473415; 3538951; 3735559; 3866631; 3670025; 3080194; 3211266; 3276802; 3342339; 3407874; 3473410; 3538948; 3735554; 3866626; 3932169; 3080205; 3211277; 3276813; 3342349; 3407885; 3473421; 3538957; 3735565; 3866637; 4063241; 3080205; 3211277; 3276813; 3342349; 3407885; 3473421; 3538957; 3735565; 3866637; 4259841; 3342350; 4325387; 2949125; 3080197; 3145733; 3211269; 3276805; 3342341; 3407877; 3473413; 3538949; 3735557; 3866629; 4587521; 3342351; 4980745; 3080199; 3211271; 3276807; 3342343; 3407879; 3473415; 3538951; 3735559; 3866631|]
let zeroReduces = Array.zeroCreate 84
for i = 0 to 83 do
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
let private accStates = Array.zeroCreate 84
for i = 0 to 83 do
        accStates.[i] <- List.exists ((=) i) small_acc
let eofIndex = 55
let errorIndex = 6
let errorRulesExists = false
let private parserSource = new ParserSource<Token> (gotos, reduces, zeroReduces, accStates, rules, rulesStart, leftSide, startRule, eofIndex, tokenToNumber, acceptEmptyInput, numToString, errorIndex, errorRulesExists)
let buildAst : (seq<Token> -> ParseResult<Token>) =
    buildAst<Token> parserSource

let _rnglr_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(43, new Nodes([|box (new AST(new Family(41, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(64, new Nodes([||])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(49, new Nodes([|box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(12, new Nodes([|box (new AST(new Family(49, new Nodes([|box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null))|])), null)); box (new AST(new Family(55, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(53, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(44, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(39, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(33, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(60, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(58, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(41, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(35, new Nodes([||])), null)), null); null|]
let _rnglr_filtered_epsilons : Tree<Token>[] = [|new Tree<_>(null,box (new AST(new Family(43, new Nodes([|box (new AST(new Family(41, new Nodes([||])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(64, new Nodes([||])), null)), null); null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(49, new Nodes([|box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null))|])), null)), null); new Tree<_>(null,box (new AST(new Family(57, new Nodes([|box (new AST(new Family(53, new Nodes([||])), null))|])), null)), null); null; null; new Tree<_>(null,box (new AST(new Family(12, new Nodes([|box (new AST(new Family(49, new Nodes([|box (new AST(new Family(37, new Nodes([|box (new AST(new Family(35, new Nodes([||])), null)); box (new AST(new Family(46, new Nodes([|box (new AST(new Family(44, new Nodes([||])), null))|])), null))|])), null))|])), null)); box (new AST(new Family(55, new Nodes([||])), null))|])), null)), null); null; null; null; null; null; null; null; null; new Tree<_>(null,box (new AST(new Family(53, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(44, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(39, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(33, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(60, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(58, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(55, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(25, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(22, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(17, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(19, new Nodes([||])), null)), null); null; null; null; new Tree<_>(null,box (new AST(new Family(41, new Nodes([||])), null)), null); new Tree<_>(null,box (new AST(new Family(35, new Nodes([||])), null)), null); null|]
for x in _rnglr_filtered_epsilons do if x <> null then x.ChooseSingleAst()
let _rnglr_extra_array, _rnglr_rule_, _rnglr_concats = 
  (Array.zeroCreate 0 : array<'_rnglr_type_a_list * '_rnglr_type_attr_list * '_rnglr_type_attr_stmt * '_rnglr_type_compass_pt * '_rnglr_type_edge_operator * '_rnglr_type_edge_stmt * '_rnglr_type_error * '_rnglr_type_graph * '_rnglr_type_id * '_rnglr_type_node_id * '_rnglr_type_node_stmt * '_rnglr_type_port * '_rnglr_type_stmt * '_rnglr_type_stmt_list * '_rnglr_type_subgraph * '_rnglr_type_yard_exp_brackets_367 * '_rnglr_type_yard_exp_brackets_368 * '_rnglr_type_yard_exp_brackets_369 * '_rnglr_type_yard_exp_brackets_370 * '_rnglr_type_yard_exp_brackets_371 * '_rnglr_type_yard_exp_brackets_372 * '_rnglr_type_yard_exp_brackets_373 * '_rnglr_type_yard_exp_brackets_374 * '_rnglr_type_yard_exp_brackets_375 * '_rnglr_type_yard_exp_brackets_376 * '_rnglr_type_yard_many_119 * '_rnglr_type_yard_many_120 * '_rnglr_type_yard_many_121 * '_rnglr_type_yard_many_122 * '_rnglr_type_yard_opt_154 * '_rnglr_type_yard_opt_155 * '_rnglr_type_yard_opt_156 * '_rnglr_type_yard_opt_157 * '_rnglr_type_yard_opt_158 * '_rnglr_type_yard_opt_159 * '_rnglr_type_yard_opt_160 * '_rnglr_type_yard_rule_362 * '_rnglr_type_yard_rule_364 * '_rnglr_type_yard_rule_365 * '_rnglr_type_yard_rule_list_363 * '_rnglr_type_yard_rule_list_366 * '_rnglr_type_yard_start_rule>), 
  [|
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SUBGR _rnglr_val -> [_rnglr_val] | a -> failwith "SUBGR expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_160) (gr7:GraphDataContainer)
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 60 "DotGrammar.yrd"
                                                                          i
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_376) 
# 261 "DotParser.fs"
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
                  
# 58 "DotGrammar.yrd"
                                                  1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_375) 
# 283 "DotParser.fs"
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
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_158) 
                 |> List.iter (fun (_) -> 
                  _rnglr_cycle_res := (
                    
# 58 "DotGrammar.yrd"
                                                         1
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_374) 
# 307 "DotParser.fs"
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
                  
# 58 "DotGrammar.yrd"
                                                  1
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_374) 
# 329 "DotParser.fs"
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
                
# 49 "DotGrammar.yrd"
                                              p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_373) 
# 349 "DotParser.fs"
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
                
# 49 "DotGrammar.yrd"
                                                               p
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_373) 
# 369 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_365) 
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_364) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 22 "DotGrammar.yrd"
                                                                                 i 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_372) 
# 391 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with COMMA _rnglr_val -> [_rnglr_val] | a -> failwith "COMMA expected, but %A found" a )
             |> List.iter (fun (_) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_rule_362) 
               |> List.iter (fun (i) -> 
                _rnglr_cycle_res := (
                  
# 22 "DotGrammar.yrd"
                                                                                 i 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_371) 
# 413 "DotParser.fs"
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
                    
# 41 "DotGrammar.yrd"
                                                               l
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_370) 
# 437 "DotParser.fs"
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
                
# 38 "DotGrammar.yrd"
                                                                            n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_369) 
# 457 "DotParser.fs"
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
                
# 38 "DotGrammar.yrd"
                                                                                         n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_369) 
# 477 "DotParser.fs"
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
                
# 38 "DotGrammar.yrd"
                                                                                                      n
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_369) 
# 497 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_stmt) gr
             |> List.iter (fun (s) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_156) (gr:GraphDataContainer) s
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 29 "DotGrammar.yrd"
                                                                              s
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_368) 
# 519 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun strict ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with GRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "GRAPH expected, but %A found" a )
             |> List.iter (fun (t) -> 
              _rnglr_cycle_res := (
                
# 25 "DotGrammar.yrd"
                                                             t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_367) 
# 539 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun strict ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with DIGRAPH _rnglr_val -> [_rnglr_val] | a -> failwith "DIGRAPH expected, but %A found" a )
             |> List.iter (fun (t) -> 
              _rnglr_cycle_res := (
                
# 25 "DotGrammar.yrd"
                                                                             t
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )

               : '_rnglr_type_yard_exp_brackets_367) 
# 559 "DotParser.fs"
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
                
# 65 "DotGrammar.yrd"
                            i 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 65 "DotGrammar.yrd"
               : '_rnglr_type_id) 
# 579 "DotParser.fs"
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
                
# 63 "DotGrammar.yrd"
                                   
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 63 "DotGrammar.yrd"
               : '_rnglr_type_compass_pt) 
# 599 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 60 "DotGrammar.yrd"
                                                        None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 60 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_159) 
# 617 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_376) (gr7:GraphDataContainer)
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 60 "DotGrammar.yrd"
                                                          Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 60 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_159) 
# 637 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
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
               : '_rnglr_type_yard_opt_160) 
# 655 "DotParser.fs"
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
                
# 25 "DotGrammar.yrd"
                                                                                       Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_160) 
# 675 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr7:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_159) (gr7:GraphDataContainer)
             |> List.iter (fun (n) -> 
              (match ((unbox _rnglr_children.[1]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
               |> List.iter (fun (_) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_stmt_list) gr7
                 |> List.iter (fun (l) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    _rnglr_cycle_res := (
                      
# 61 "DotGrammar.yrd"
                        Utils.addSubgraphToArray n gr7 graphs 
                        )::!_rnglr_cycle_res ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 60 "DotGrammar.yrd"
               : '_rnglr_type_subgraph) 
# 701 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_158) 
# 719 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_375) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 58 "DotGrammar.yrd"
                        Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_158) 
# 739 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_374) 
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 58 "DotGrammar.yrd"
                                                                               1
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 58 "DotGrammar.yrd"
               : '_rnglr_type_port) 
# 759 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun nodeID ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 55 "DotGrammar.yrd"
                                   None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 55 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_157) 
# 777 "DotParser.fs"
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
                
# 55 "DotGrammar.yrd"
                                     Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 55 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_157) 
# 797 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (nodeID) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_opt_157) nodeID
               |> List.iter (fun (_) -> 
                _rnglr_cycle_res := (
                  
# 56 "DotGrammar.yrd"
                    nodeID 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 55 "DotGrammar.yrd"
               : '_rnglr_type_node_id) 
# 819 "DotParser.fs"
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
                  
# 53 "DotGrammar.yrd"
                    gr6.AddNodeStmtData nodeID lst 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 52 "DotGrammar.yrd"
               : '_rnglr_type_node_stmt) 
# 841 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_373) 
             |> List.iter (fun (op) -> 
              _rnglr_cycle_res := (
                
# 50 "DotGrammar.yrd"
                  op 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 49 "DotGrammar.yrd"
               : '_rnglr_type_edge_operator) 
# 861 "DotParser.fs"
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
                
# 46 "DotGrammar.yrd"
                                             nodeID 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_364) 
# 881 "DotParser.fs"
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
                
# 46 "DotGrammar.yrd"
                                                                                                     sub 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_364) 
# 901 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_operator) 
             |> List.iter (fun (l) -> 
              _rnglr_cycle_res := (
                
# 46 "DotGrammar.yrd"
                                                                                                                             l
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_365) 
# 921 "DotParser.fs"
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
               : '_rnglr_type_yard_many_122) 
# 939 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_372) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_122) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 46 "DotGrammar.yrd"
                                                                                                                 yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 46 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_122) 
# 961 "DotParser.fs"
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
# 22 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_366) 
# 979 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_364) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_122) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 22 "DotGrammar.yrd"
                                                                                         hd::tl 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 22 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_366) 
# 1001 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr4:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_366) 
             |> List.iter (fun (edges) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 47 "DotGrammar.yrd"
                    gr4.AddEdgeStmtData edges lst 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 45 "DotGrammar.yrd"
               : '_rnglr_type_edge_stmt) 
# 1023 "DotParser.fs"
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
                    
# 43 "DotGrammar.yrd"
                                                         (k,v)
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 43 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_362) 
# 1047 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 43 "DotGrammar.yrd"
                                                           []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 43 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_121) 
# 1065 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun hd ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_371) hd
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_121) hd
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 43 "DotGrammar.yrd"
                                                               yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 43 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_121) 
# 1087 "DotParser.fs"
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
# 22 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_363) 
# 1105 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_362) 
             |> List.iter (fun (hd) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_121) hd
               |> List.iter (fun (tl) -> 
                _rnglr_cycle_res := (
                  
# 22 "DotGrammar.yrd"
                                                                                         hd::tl 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 22 "DotGrammar.yrd"
               : '_rnglr_type_yard_rule_list_363) 
# 1127 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_rule_list_363) 
             |> List.iter (fun (lst) -> 
              _rnglr_cycle_res := (
                
# 43 "DotGrammar.yrd"
                                                                      lst 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 43 "DotGrammar.yrd"
               : '_rnglr_type_a_list) 
# 1147 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 41 "DotGrammar.yrd"
                             []
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 41 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_120) 
# 1165 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_370) 
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_120) 
               |> List.iter (fun (yard_tail) -> 
                _rnglr_cycle_res := (
                  
# 41 "DotGrammar.yrd"
                                 yard_head::yard_tail
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 41 "DotGrammar.yrd"
               : '_rnglr_type_yard_many_120) 
# 1187 "DotParser.fs"
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
                
# 41 "DotGrammar.yrd"
                                                                  List.concat l 
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 41 "DotGrammar.yrd"
               : '_rnglr_type_attr_list) 
# 1207 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr3:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_369) (gr3:GraphDataContainer)
             |> List.iter (fun (attr_name) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_attr_list) 
               |> List.iter (fun (lst) -> 
                _rnglr_cycle_res := (
                  
# 39 "DotGrammar.yrd"
                    gr3.AddGeneralAttrs attr_name lst 
                    )::!_rnglr_cycle_res ) )
            !_rnglr_cycle_res
          )
            )
# 38 "DotGrammar.yrd"
               : '_rnglr_type_attr_stmt) 
# 1229 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr2:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_node_stmt) gr2
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 32 "DotGrammar.yrd"
                                         
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1249 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr2:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_edge_stmt) gr2
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 33 "DotGrammar.yrd"
                                         
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1269 "DotParser.fs"
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
                
# 34 "DotGrammar.yrd"
                                             
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1289 "DotParser.fs"
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
                    
# 35 "DotGrammar.yrd"
                                           gr2.AddAssignStmt k v 
                      )::!_rnglr_cycle_res ) ) )
            !_rnglr_cycle_res
          )
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1313 "DotParser.fs"
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
                
# 36 "DotGrammar.yrd"
                                                                
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 31 "DotGrammar.yrd"
               : '_rnglr_type_stmt) 
# 1333 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
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
# 1351 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_exp_brackets_368) (gr:GraphDataContainer)
             |> List.iter (fun (yard_head) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_many_119) (gr:GraphDataContainer)
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
# 1373 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) -> fun s ->
          (
            let _rnglr_cycle_res = ref []
            _rnglr_cycle_res := (
              
# 29 "DotGrammar.yrd"
                                                                    None
                )::!_rnglr_cycle_res
            !_rnglr_cycle_res
          )
            )
# 29 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_156) 
# 1391 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) -> fun s ->
          (
            let _rnglr_cycle_res = ref []
            (match ((unbox _rnglr_children.[0]) : Token) with SEP _rnglr_val -> [_rnglr_val] | a -> failwith "SEP expected, but %A found" a )
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 29 "DotGrammar.yrd"
                                                                      Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 29 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_156) 
# 1411 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun (gr:GraphDataContainer) ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_many_119) (gr:GraphDataContainer)
             |> List.iter (fun (_) -> 
              _rnglr_cycle_res := (
                
# 29 "DotGrammar.yrd"
                                                                                  
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 29 "DotGrammar.yrd"
               : '_rnglr_type_stmt_list) 
# 1431 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun strict -> fun graph_type ->
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
               : '_rnglr_type_yard_opt_155) 
# 1449 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( fun strict -> fun graph_type ->
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_id) 
             |> List.iter (fun (yard_elem) -> 
              _rnglr_cycle_res := (
                
# 25 "DotGrammar.yrd"
                                                                                       Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_155) 
# 1469 "DotParser.fs"
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
               : '_rnglr_type_yard_opt_154) 
# 1487 "DotParser.fs"
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
                
# 25 "DotGrammar.yrd"
                                Some(yard_elem)
                  )::!_rnglr_cycle_res )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_opt_154) 
# 1507 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          (
            let _rnglr_cycle_res = ref []
            ((unbox _rnglr_children.[0]) : '_rnglr_type_yard_opt_154) 
             |> List.iter (fun (strict) -> 
              ((unbox _rnglr_children.[1]) : '_rnglr_type_yard_exp_brackets_367) strict
               |> List.iter (fun (graph_type) -> 
                ((unbox _rnglr_children.[2]) : '_rnglr_type_yard_opt_155) strict graph_type
                 |> List.iter (fun (name) -> 
                  (match ((unbox _rnglr_children.[3]) : Token) with LCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "LCURBRACE expected, but %A found" a )
                   |> List.iter (fun (_) -> 
                    ((unbox _rnglr_children.[4]) : '_rnglr_type_stmt_list) (graphs.[0])
                     |> List.iter (fun (_) -> 
                      (match ((unbox _rnglr_children.[5]) : Token) with RCURBRACE _rnglr_val -> [_rnglr_val] | a -> failwith "RCURBRACE expected, but %A found" a )
                       |> List.iter (fun (_) -> 
                        _rnglr_cycle_res := (
                          
# 27 "DotGrammar.yrd"
                            graphs.[0].AddGeneralInfo strict graph_type name 
                            )::!_rnglr_cycle_res ) ) ) ) ) )
            !_rnglr_cycle_res
          )
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_graph) 
# 1537 "DotParser.fs"
      );
  (
    fun (_rnglr_children : array<_>) (parserRange : (uint64 * uint64)) -> 
      box (
        ( 
          ((unbox _rnglr_children.[0]) : '_rnglr_type_graph) 
            )
# 25 "DotGrammar.yrd"
               : '_rnglr_type_yard_start_rule) 
# 1547 "DotParser.fs"
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
# 1565 "DotParser.fs"
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
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_operator)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr4:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_edge_stmt)  (gr4:GraphDataContainer) ) |> List.concat));
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
      box ( fun (gr6:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_node_stmt)  (gr6:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_port)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr2:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_stmt)  (gr2:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_stmt_list)  (gr:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_subgraph)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun strict ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_367)  strict ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_368)  (gr:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr3:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_369)  (gr3:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_370)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_371)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun hd ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_372)  hd ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_373)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_374)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_375)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_exp_brackets_376)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_many_119)  (gr:GraphDataContainer) ) |> List.concat));
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
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_154)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun strict -> fun graph_type ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_155)  strict graph_type ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr:GraphDataContainer) -> fun s ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_156)  (gr:GraphDataContainer) s ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun nodeID ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_157)  nodeID ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_158)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_159)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( fun (gr7:GraphDataContainer) ->
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_opt_160)  (gr7:GraphDataContainer) ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_362)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_364)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_365)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_363)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_rule_list_366)   ) |> List.concat));
    (fun (_rnglr_list : list<_>) -> 
      box ( 
        _rnglr_list |> List.map (fun _rnglr_item -> ((unbox _rnglr_item) : '_rnglr_type_yard_start_rule)   ) |> List.concat));
  |] 
let translate (args : TranslateArguments<_,_>) (tree : Tree<_>) (dict : _ ) : '_rnglr_type_yard_start_rule = 
  unbox (tree.Translate _rnglr_rule_  leftSide _rnglr_concats (if args.filterEpsilons then _rnglr_filtered_epsilons else _rnglr_epsilons) args.tokenToRange args.zeroPosition args.clearAST dict) : '_rnglr_type_yard_start_rule
