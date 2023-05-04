using Dpll.SatModels;

namespace Dpll.Extensions;

public static class IntListExtensions
{
    /// <summary>
    /// Determines whether the provided suit is satisfiable for the formula.
    /// </summary>
    /// <param name="suit">Values, assigned to literals.</param>
    /// <param name="satFormula">Formula.</param>
    public static bool IsSatisfiableFor(this List<int> suit, SatFormula satFormula)
    {
        var hashsetLiterals = suit.ToHashSet();
        if (hashsetLiterals.Any(literal => hashsetLiterals.Contains(-literal))) return false;

        // if literal does not occur in clause, assign arbitrary value (let it be true) for it
        for (var i = 1; i <= satFormula.LiteralsCount; i++)
        {
            if (!hashsetLiterals.Contains(i) && !hashsetLiterals.Contains(-i))
            {
                hashsetLiterals.Add(i);
            }
        }

        return satFormula.Clauses.All(clause => clause.Literals.Any(literal => hashsetLiterals.Contains(literal)));
    }
    
    public static List<int> CopyByValue(this List<int> list)
    {
        var copy = new int[list.Count];
        list.CopyTo(copy);
        return copy.ToList();
    }
}