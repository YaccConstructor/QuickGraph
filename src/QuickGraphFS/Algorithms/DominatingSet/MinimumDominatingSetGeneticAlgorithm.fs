(* 
    Hybrid Genetic Algorithm for Minimum Dominating Set Problem
    Authors: Abdel-Rahman Hedar and Rashad Ismail
    Implemented by Dmitrii Petukhov <dimart.sp@gmail.com>
*)

namespace QuickGraph.Algorithms

open System
open QuickGraph

module GeneticConstants =
    let M     = 40   // Population size (number of solutions)
    let pc    = 0.8  // Crossover probability (a good value is about .7)
    let pm    = 0.01 // Mutation probability (ussualy .001)
    let nl    = 2    // Number of iterations in LocalSearch
    let nDS   = 45   // Max number of the best dominating sets used to update DS
    let nCore = 3    // The number of the best dominating sets used to compute xCore
    let gMax  = 100  // Max number of generations

type Chromosome = int []
type Fitness    = float
type Individual = Chromosome * Fitness
type Population = Individual []

type MinimumDominatingSetGeneticAlgorithm(graph : IUndirectedGraph<int, IUndirectedEdge<int>>) =
    let rnd = Random()
    let toDouble (x : int) = Convert.ToDouble x
    let getDegree i = graph.AdjacentDegree(i)

    //////////////////////HELPFUL FUNCTIONS////////////////////////////
    let findBestIndividual  = Array.maxBy (fun (i : Individual) -> snd i)
    let findWorstIndividual = Array.minBy (fun (i : Individual) -> snd i)

    let replaceBestIndividual (p : Population) (oldBest : Individual) (newBest : Individual) =
        p.[Array.IndexOf(p, oldBest)] <- newBest
        p

    let replaceWorstIndividual (p : Population) (with' : Individual) =
        p.[Array.IndexOf(p, findWorstIndividual p)] <- with'
        p

    let isDominating (i : Individual) = snd i >= 1.0

    let trd (_, _, z) = z

    let findIndexOfNodeWithHighestDegree (xcore : Chromosome) =
        let res = Array.fold 
                   (fun (deg, i, res) bit -> 
                    if (bit = 0) && (getDegree(i) > deg) 
                    then (getDegree(i), i + 1, i) 
                    else (deg, i + 1, res))
                    (0, 0, -1) xcore
        trd res

    let computeNumberOfCoveredNodesBy (ch : Chromosome) =
        let mutable n = 0
        let marked = Array.create ch.Length false
        for i = 0 to ch.Length - 1 do
            if ch.[i] = 1
            then if (not marked.[i])
                 then marked.[i] <- true
                      n <- n + 1;
                 for e in graph.AdjacentEdges(i) do
                    if not (marked.[e.Target] && marked.[e.Source])
                    then marked.[e.Target] <- true 
                         marked.[e.Source] <- true
                         n <- n + 1
        n

    //////////////////// FITNESS //////////////////////////////////////
    let computeFitness (chromosome : Chromosome) : Fitness =
        let n = computeNumberOfCoveredNodesBy chromosome |> toDouble
        let V = graph.VertexCount |> toDouble
        let gamma = Array.sum(chromosome)
        if gamma = 0 then 0.0
                     else n / V + 1.0 / (V * toDouble (gamma))

    let updateFitnesses (p : Population) = 
        Array.init<Individual> p.Length (fun i -> (fst p.[i], computeFitness (fst p.[i]))) 

    /////////////////////INITIALIZATION///////////////////////////////
    let createChromosome size : Individual =
        let chromosome = Array.init<int> size (fun _ -> if rnd.NextDouble() > 0.5 then 1 else 0)
        let fitness = computeFitness chromosome
        (chromosome, fitness)

    let initPopulation withSize : Population =
        Array.init<Individual> withSize (fun _ -> createChromosome graph.VertexCount)

    /////////////////////LOCAL SEARCH/////////////////////////////////
    let rec selectRandomComponentWithValue (x : int) =
        fun (i : Individual) ->
            let bits = fst i
            let at = rnd.Next(bits.Length)
            let elem = bits.[at]
            if (elem <> x)
            then (selectRandomComponentWithValue x i)
            else let bits' = bits.Clone() :?> Chromosome
                 bits'.[at] <- 1 - elem
                 (bits', computeFitness bits')

    let localSearch (p : Population) =
        let xBest = findBestIndividual p
        let at = Array.IndexOf(p, xBest)
        let rec localSearch' n (xBest : Individual) =
            if (n = 0)
            then xBest
            else let mutable xBest' = xBest
                 if (snd xBest' >= 1.0) then xBest' <- selectRandomComponentWithValue 1 xBest'
                 if (snd xBest' < 1.0)  then xBest' <- selectRandomComponentWithValue 0 xBest'
                 localSearch' (n - 1) (if (snd xBest' > snd xBest) then xBest' else xBest)
        let xBest' = localSearch' GeneticConstants.nl xBest
        p.[at] <- xBest'
        p

    //////////////////////// SELECTION ////////////////////////////////
    // linear ranking selection
    let lrs (population : Population) : Population =
        let populationSize = population.Length
        let n = toDouble populationSize
        let div = n * (n - 1.0)
        let sortedPopulation = Array.sortBy (fun (_, fit) -> fit) population
        let rank i = Array.IndexOf (sortedPopulation, population.[i]) + 1
        let p = rank >> toDouble >> (fun x -> x / div)
        let v = 1.0 / (n - 2.001)
        Array.map (fun _ -> 
            let alpha = rnd.NextDouble() * v
            let index = snd <| Array.foldBack (fun _ (j, res) -> if (p j <= alpha) then (j + 1, res) else (j + 1, j)) population (0, 0)
            Array.get population index
            ) population

    /////////////////////////CROSSOVER/////////////////////////////////
    //Single-point crossover
    let crossover (p : Population) : Population =
        let spc (c1 : Chromosome) (c2 : Chromosome) =
            let point = rnd.Next(c1.Length)
            let c1' = Array.append c1.[0..point - 1] c2.[point..c2.Length - 1]
            let c2' = Array.append c2.[0..point - 1] c1.[point..c1.Length - 1]
            (c1', c2')
        in
        for i in 0 .. 2 .. (p.Length - 1) do
            if (rnd.NextDouble() < GeneticConstants.pc) then
                let c1, c2 = spc (fst p.[i]) (fst p.[i + 1])
                p.[i] <- (c1, snd p.[i])
                p.[i + 1] <- (c2, snd p.[i + 1])
        p

    /////////////////////////MUTATION//////////////////////////////////
    //Uniform mutation
    let mutation (p : Population) : Population =
        let uniformMutation (c : Chromosome) =
            c |> Array.map (fun bit -> if (rnd.NextDouble() < GeneticConstants.pm) then 1-bit else bit)
        in
        Array.init<Individual> p.Length (fun i -> if (rnd.NextDouble() < GeneticConstants.pm) 
                                                  then let ogen = fst p.[i]
                                                       let mgen = uniformMutation <| fst p.[i]
                                                       (mgen, snd p.[i])
                                                  else p.[i])

    /////////////////////FILTERING//////////////////////////////////////
    let filtering (p : Population) (best : Individual) =
        if (isDominating best)
        then let index = Array.IndexOf(p, best)
             let mutable bits = fst best
             let fit = snd best
             let W = Array.mapi (fun i e -> if (e = 1) then i else -i) bits
             for i = 0 to W.Length - 1 do
                 if (W.[i] >= 0)
                 then let bits' = bits.Clone() :?> Chromosome
                      bits'.[W.[i]] <- 0 
                      let fit' = computeFitness (bits')
                      if (fit' > fit)
                      then bits <- bits'
                           p.[index] <- (bits, fit')
        p

    /////////////////////INTERSECT/////////////////////////////////////
    let intersect (ds : Option<Individual> []) =
        let rec intersect' (ds : Option<Individual> []) i (res : Option<Individual>) =
            if i < GeneticConstants.nCore - 1
            then match (res, ds.[i + 1]) with
                 | Some x, Some y -> let c1 = fst x
                                     let c2 = fst y
                                     let c : Chromosome  = Array.map2 (fun bit1 bit2 -> bit1 &&& bit2) c1 c2
                                     let fit = computeFitness c
                                     intersect' ds (i + 1) (Some (c, fit))
                 | Some x, None -> intersect' ds (i + 1) res
                 | None, Some y -> intersect' ds (i + 1) ds.[i + 1]
                 | None, None -> intersect' ds (i + 1) None
            else res
        in
        intersect' ds 0 ds.[0]

    /////////////////////ELITE INSPIRATION/////////////////////////////
    let eliteInspiration (ds : Option<Individual> []) (xbest : Chromosome) =
        let rec eliteInspiration' (xcore : Individual) (nf : int) =
            if (Array.sum <| fst xcore) < nf - 1
            then if snd xcore >= 1.0 
                 then xcore 
                 else let c = fst xcore
                      c.[findIndexOfNodeWithHighestDegree(c)] <- 1
                      eliteInspiration' (c, computeFitness c) nf
            else xcore
        in
        if Array.exists (fun ind -> match ind with Some x -> true | None -> false) ds
        then let nf = Array.sum(xbest) //number of nodes in the best solution
             let xcore = intersect ds 
             match xcore with
             | Some x -> let c = eliteInspiration' x nf
                         Some c
             | None -> None 
        else None

    ////////////////////////UPDATE DS//////////////////////////////////
    let updateDS (ds :  Option<Individual> []) (best : Individual) (i : int) =
        if Array.exists (fun (id : Option<Individual>) -> match id with Some x -> x = best | None -> false) ds
        then (ds, i)
        else if i >= GeneticConstants.nCore - 1
             then let min = Array.minBy (fun (id : Option<Individual>) -> match id with Some x -> snd x | None -> 100.0) ds;
                  match min with 
                  | Some x -> ds.[Array.IndexOf(ds, min)] <- Some best
                  | None -> ()
                  (ds, i)
             else ds.[i + 1] <- Some best
                  (ds, i + 1)

    //Returns an int array such that 1 means vertex in MDS, 0 - vertex not in MDS
    member x.Compute() =
        let mutable ds : Option<Individual> [] = Array.create GeneticConstants.nCore None
        let p0 = initPopulation GeneticConstants.M
    
        let mutable pt   = p0 |> localSearch |> updateFitnesses
        let mutable best = findBestIndividual pt // last best individual value
        let mutable t    = 0 // current generation number
        let mutable j    = 0 // last used index in ds
        ds.[0] <- Some best

        while not (isDominating best) do //t < GeneticConstants.gMax || 
              pt <- pt 
                   |> lrs 
                   |> crossover 
                   |> mutation 
                   |> updateFitnesses
                   |> localSearch
          
              let mutable best' = findBestIndividual pt
    
              pt <- filtering pt best' |> updateFitnesses
              best' <- findBestIndividual pt

              if (snd best' <= snd best)
              then pt <- replaceWorstIndividual pt best
              best' <- findBestIndividual pt

              let (ds', j') = updateDS ds best' j
              ds <- ds'; j <- j'

              best <- best'
              t <- t + 1

        let best' = eliteInspiration ds (fst best)
        match best' with
        | Some b -> if (snd b > snd best) then best <- b
        | None -> ()
        fst best