module HelperTypes

type MarkedVal<'a when 'a: comparison> = {
    Value: 'a
    Marked: bool }
module MarkedValFuns =
    let marked v = { Value = v; Marked = true }
    let notMarked v = { Value = v; Marked = false }

type TwoSets<'a when 'a: comparison> = {
    Marked: Set<'a>;
    NotMarked: Set<'a> }
module TwoSetsFuns =
    let empty = { Marked = Set.empty; NotMarked = Set.empty }
    let add (mv: MarkedVal<_>) (ts: TwoSets<_>) = 
        if mv.Marked
        then { ts with Marked = Set.add mv.Value ts.Marked }
        else { ts with NotMarked = Set.add mv.Value ts.NotMarked }
    let contains v (ts: TwoSets<_>) = 
        Set.contains v ts.Marked || Set.contains v ts.NotMarked

type StateFromFsa = StateFromFsa of MarkedVal<int> 
module StateFromFsaFuns = 
    let fromFsa1 (id: int) = StateFromFsa (MarkedValFuns.notMarked id)
    let fromFsa2 (id: int) = StateFromFsa (MarkedValFuns.marked id)
    let isFromFsa1 (StateFromFsa mv) = not mv.Marked
    let isFromFsa2 (StateFromFsa mv) = mv.Marked

type EqClass = EqClass of TwoSets<int>
module EqClassFuns = 
    let empty = EqClass (TwoSetsFuns.empty)
    let fsa1States (EqClass ts) = ts.NotMarked
    let fsa2States (EqClass ts) = ts.Marked
    let add (StateFromFsa mv) (EqClass ts) = 
        EqClass (TwoSetsFuns.add mv ts)
    let contains v (EqClass ts) = TwoSetsFuns.contains v ts