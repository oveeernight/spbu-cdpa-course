using Dpll.SatModels;

namespace Dpll.Extensions;

public static class StringExtensions
{
    public static Clause ToClause(this string s)
    {
        return new Clause(s.Split()
            .Where(x => !string.IsNullOrEmpty(x))
            .SkipLast(1)
            .Select(int.Parse)
            .ToList());
    }
}