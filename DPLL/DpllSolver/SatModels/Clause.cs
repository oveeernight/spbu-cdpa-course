namespace Dpll.SatModels;

public class Clause
{
    public List<int> Literals { get; }
    public Clause(List<int> literals)
    {
        Literals = literals;
    }
}