using System.Diagnostics.Contracts;
using Dpll.Extensions;

namespace Dpll.SatModels;

public class SatFormula
{
    public List<Clause> Clauses { get; private set; }
    public bool ContainsUnitLiteral { get; private set; }
    public bool ContainsEmptyClause { get; private set; }
    public bool ContainsPureLiteral { get; private set; }
    public List<int> UnitLiterals { get; private set; }
    public List<int> PureLiterals { get; private set; }
    public int LiteralsCount { get; private set; }
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
    /// <summary>
    /// Gets simplified formula assigning specified literal boolean value.
    /// </summary>
    /// <param name="literal">Literal to assign.</param>
    /// <param name="assignedValue">Value of assigned literal.</param>
    /// <returns>Simplified <see cref="SatFormula"/>.</returns>
    public (SatFormula formula, List<int> accumulator) SimplifyAssigningLiteral(
        int literal,
        bool assignedValue,
        List<int> accumulator,
        bool copyByValue = true)
    {
        var newClauses = Clauses;
        var literalAbs = Math.Abs(literal);
        var multiplier = assignedValue ? 1 : -1;
        if (copyByValue)
        {
            accumulator = accumulator.CopyByValue();
            newClauses = Clauses.CopyByValue().ToList();
        }

        accumulator.Add(multiplier * literalAbs);

        var (newUnitLiterals, containsEmptyClause) = newClauses.SimplifyExtractingInfo(
            new List<int> { multiplier * literalAbs });
        var newPureLiterals = newClauses.GetPureLiterals();
        var simplifiedFormula = new SatFormula(
            clauses: newClauses,
            unitLiterals: newUnitLiterals,
            pureLiterals: newPureLiterals,
            containsEmptyClause: containsEmptyClause,
            literalsCount: LiteralsCount - 1);
        return (simplifiedFormula, accumulator);
    }

    public void SimplifyAssigningUnivocalLiterals(List<int> accumulator)
    {
        while (ContainsUnitLiteral || ContainsPureLiteral)
        {
            if (Clauses.Count == 0 || ContainsEmptyClause)
            {
                return;
            }
            // unsat case
            if (UnitLiterals.Any(unitLiteral => UnitLiterals.Contains(-unitLiteral)))
            {
                ContainsEmptyClause = true;
                return;
            }
            accumulator.AddRange(UnitLiterals.Union(PureLiterals));

            var (newUnitLiterals, containsEmptyClose) = Clauses.SimplifyExtractingInfo(UnitLiterals.Union(PureLiterals).ToList());
            // no need to save info since unit literals or empty close will not appear after removing pure literals
            //accumulator.AddRange(PureLiterals);
            //Clauses.SimplifyExtractingInfo(PureLiterals);
            
            LiteralsCount -= PureLiterals.Count + UnitLiterals.Count;
            PureLiterals = Clauses.GetPureLiterals();
            UnitLiterals = newUnitLiterals;
            ContainsEmptyClause = containsEmptyClose;
            ContainsPureLiteral = PureLiterals.Count > 0;
            ContainsUnitLiteral = UnitLiterals.Count > 0;
        }
    }
}