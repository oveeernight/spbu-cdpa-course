using System.Diagnostics.Contracts;
using Dpll.Extensions;

namespace Dpll.SatModels;

public class SatFormula
{
    public List<Clause> Clauses { get; }
    public bool ContainsUnitLiteral { get; }
    public bool ContainsEmptyClause { get; }
    public bool ContainsPureLiteral { get; }
    public List<int> UnitLiterals { get; }
    public List<int> PureLiterals { get; }
    public int LiteralsCount { get; }
    public SatFormula(
        List<Clause> clauses,
        List<int> unitLiterals,
        List<int> pureLiterals,
        bool containsEmptyClause,
        int literalsCount)
    {
        Clauses = clauses;
        UnitLiterals = unitLiterals;
        PureLiterals = pureLiterals;
        ContainsUnitLiteral = unitLiterals.Count > 0;
        ContainsEmptyClause = containsEmptyClause;
        ContainsPureLiteral = pureLiterals.Count > 0;
        LiteralsCount = literalsCount;
    }

    public SatFormula(bool containsEmptyClause)
    {
        ContainsEmptyClause = containsEmptyClause;
    }

    /// <summary>
    /// Gets simplified formula assigning specified literal boolean value.
    /// </summary>
    /// <param name="literal">Literal to assign.</param>
    /// <param name="assignedValue">Value of assigned literal.</param>
    /// <returns>Simplified <see cref="SatFormula"/>.</returns>
    [Pure]
    public (SatFormula formula, List<int> accumulator) SimplifyAssigningLiteral(int literal, bool assignedValue, List<int> accumulator)
    {
        var literalAbs = Math.Abs(literal);
        var multiplier = assignedValue ? 1 : -1;
        var univocalLiterals = new HashSet<int> { multiplier * literalAbs };
        return SimplifyAssigningUnivocalLiterals(univocalLiterals, accumulator);
    }

    public (SatFormula formula, List<int> accumulator) SimplifyAssigningUnivocalLiterals(
        HashSet<int> literals,
        List<int> accumulator,
        bool copyClausesByValue = true)
    {
        if (literals.Any(literal => literals.Contains(-literal)))
        {
            // unsat case
            return (new SatFormula(true), accumulator);
        }
        List<int> updatedAccumulator;
        if (copyClausesByValue)
        {
            updatedAccumulator = accumulator.Union(literals).ToList();
        }
        else
        {
            updatedAccumulator = accumulator;
            accumulator.AddRange(literals);
        }
        var enumerableClauses = Clauses
            .Where(clause => !clause.Literals.Any(literals.Contains));
        var simplifiedClauses =
            copyClausesByValue ? enumerableClauses.CopyByValue().ToList() : enumerableClauses.ToList();
        var unitLiterals = new List<int>();
        var containsEmptyClose = false;
        foreach (var clause in simplifiedClauses)
        {
            clause.Literals.RemoveAll(literal => literals.Contains(-literal));
            if (clause.Literals.Count == 1)
            {
                unitLiterals.Add(clause.Literals[0]);
            }

            if (clause.Literals.Count == 0)
            {
                containsEmptyClose = true;
            }
        }
        var pureLiterals = simplifiedClauses.GetPureLiterals();
        var simplifiedFormula = new SatFormula(
            clauses: simplifiedClauses,
            unitLiterals: unitLiterals,
            pureLiterals: pureLiterals,
            containsEmptyClause: containsEmptyClose,
            literalsCount: LiteralsCount - literals.Count);
        if (unitLiterals.Count + pureLiterals.Count == 0 || simplifiedClauses.Count == 0 || containsEmptyClose)
        {
            return (simplifiedFormula, updatedAccumulator);
        }

        // can simplify once again
        var univocalLiterals = unitLiterals.Union(pureLiterals).ToHashSet();
        if (univocalLiterals.Any(literal => univocalLiterals.Contains(-literal)))
        {
            return (new SatFormula(true), accumulator);
        }

        return simplifiedFormula.SimplifyAssigningUnivocalLiterals(
           univocalLiterals,
           updatedAccumulator,
           false);
    }
}