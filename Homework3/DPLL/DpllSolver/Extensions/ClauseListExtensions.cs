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
}