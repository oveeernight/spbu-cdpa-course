using Dpll.SatModels;

namespace Dpll.Extensions;

public static class ClauseListExtensions
{
    public static List<int> GetPureLiterals(this List<Clause> clauses)
    {
        if (clauses.Count == 0)
        {
            return new List<int>();
        }

        var literals = clauses
            .SelectMany(clause => clause.Literals)
            .Distinct()
            .ToHashSet();
        var pureLiterals = literals.Where(literal => !literals.Contains(-literal)).ToList();
        return pureLiterals;
    }

    public static (List<int> unitLiterals, bool ContainsEmptyClose) SimplifyExtractingInfo(this List<Clause> clauses, List<int> literals)
    {
        var containsEmptyClause = false;
        clauses.RemoveAll(clause => clause.Literals.Any(literals.Contains));
        var unitLiterals = new List<int>();
        foreach (var clause in clauses)
        {
            clause.Literals.RemoveAll(literal => literals.Contains(-literal));
            if (clause.Literals.Count == 0)
            {
                containsEmptyClause = true;
            }

            if (clause.Literals.Count == 1)
            {
                unitLiterals.Add(clause.Literals[0]);
            }
        }

        return (unitLiterals, containsEmptyClause);
    }
}