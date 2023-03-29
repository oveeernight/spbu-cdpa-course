namespace Dpll.SatModels;

public class Clause
{
    public List<int> Literals { get; }
    public bool IsEmpty => Literals.Count == 0;

    public Clause(List<int> literals)
    {
        Literals = literals;
    }

    public bool IsSatisfiableFor(SatFormula satFormula)
    {
        var hashsetLiterals = Literals.ToHashSet();

        // if literal does not occur in clause, assign arbitrary value (let it be true) for it
        for (var i = 1; i <= satFormula.LiteralsCount; i++)
        {
            if (!(hashsetLiterals.Contains(i) && hashsetLiterals.Contains(-i)))
            {
                hashsetLiterals.Add(i);
            }
        }

        foreach (var clause in satFormula.Clauses)
        {
            if (clause.Literals.Any(literal => hashsetLiterals.Contains(literal)))
            {
                continue;
            }

            return false;
        }

        return true;
    }
}