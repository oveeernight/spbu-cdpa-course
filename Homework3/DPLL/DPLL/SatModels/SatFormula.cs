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

    [Pure]
    public SatFormula SimplifyFormulaAssigningLiteral(int literal, bool assignedValue)
    {
        var literalAbs = Math.Abs(literal);
        var multiplier = assignedValue ? 1 : -1;
        var simplifiedClauses = Clauses
            .Where(clause => !clause.Literals.Contains(multiplier * literalAbs))
            .Select(clause =>
            {
                var listCopy = new int[clause.Literals.Count];
                clause.Literals.CopyTo(listCopy);
                return new Clause(listCopy.ToList());
            })
            .ToList();

        var containsEmptyClause = false;
        var unitLiterals = new List<int>();
        foreach (var clause in simplifiedClauses)
        {
            clause.Literals.Remove(-multiplier * literalAbs);
            if (clause.Literals.Count == 0)
            {
                containsEmptyClause = true;
            }
            else if (clause.Literals.Count == 1)
            {
                unitLiterals.Add(clause.Literals[0]);
            }
        }

        var pureLiterals = simplifiedClauses.GetPureLiterals();

        return new SatFormula(
            clauses: simplifiedClauses,
            unitLiterals: unitLiterals,
            pureLiterals: pureLiterals,
            containsEmptyClause: containsEmptyClause,
            literalsCount: LiteralsCount - 1);
    }
}