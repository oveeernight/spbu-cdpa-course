namespace Dpll.SatModels;

public class SatFormula
{
    public List<Clause> Clauses { get; }
    public int LiteralsCount { get; set; }
    public int ClausesCount { get; private set; }
    public bool ContainsEmptyClause => Clauses.Any(clause => clause.IsEmpty);

    public SatFormula(List<Clause> clauses, int literalsCount, int clausesCount)
    {
        Clauses = clauses;
        LiteralsCount = literalsCount;
        ClausesCount = clausesCount;
    }

    public SatFormula CreateFormulaAssigningFirstLiteral(bool firstLiteralIsTrue = false)
    {
        var firstLiteral = Math.Abs(Clauses[0].Literals[0]);
        var multiplier = firstLiteralIsTrue ? 1 : -1;
        var simplifiedClauses = Clauses
            .Where(clause => !clause.Literals.Contains(multiplier * firstLiteral))
            .ToList();

        foreach (var clause in simplifiedClauses)
        {
            clause.Literals.Remove(-multiplier * firstLiteral);
        }

        return new SatFormula(clauses: simplifiedClauses, literalsCount: LiteralsCount - 1,
            clausesCount: simplifiedClauses.Count);
    }
}