using Dpll.SatModels;

namespace Dpll.Extensions;

public static class ClauseListExtensions
{
    public static List<int> GetPureLiterals(this List<Clause> clauses)
    {
        return clauses[0].Literals
            .Where(literal => clauses.TrueForAll(clause => clause.Literals.Contains(literal)))
            .ToList();
    }
}