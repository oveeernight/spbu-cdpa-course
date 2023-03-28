namespace Dpll.SatModels;

public class Clause
{
    public List<int> Literals { get; }
    public bool IsEmpty => Literals.Count == 0;

    public Clause(List<int> literals)
    {
        Literals = literals;
    }
}