using Dpll.SatModels;

namespace Dpll.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<Clause> CopyByValue(this IEnumerable<Clause> clauses)
    {
        return clauses.Select(clause =>
        {
            var valuesCopy = new int[clause.Literals.Count];
            clause.Literals.CopyTo(valuesCopy);
            return new Clause(valuesCopy.ToList());
        });
    }
}