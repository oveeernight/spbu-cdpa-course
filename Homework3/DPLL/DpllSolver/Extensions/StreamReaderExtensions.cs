using Dpll.SatModels;

namespace Dpll.Extensions;

public static class StreamReaderExtensions
{
    public static SatFormula ToSatFormula(this StreamReader streamReader)
    {
        var configuration = streamReader.ReadLine()?.Split().Skip(2).Select(int.Parse).ToArray() ?? Array.Empty<int>();
        var literalsCount = configuration[0];
        var unitLiterals = new List<int>();
        var clauses = new List<Clause>();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line == null || line[0] == 'c')
            {
                continue;
            }

            var clause = line.ToClause();
            if (clause.Literals.Count == 1)
            {
                unitLiterals.Add(clause.Literals[0]);
            }

            clauses.Add(line.ToClause());
        }

        var pureLiterals = clauses.GetPureLiterals();

        return new SatFormula(
            clauses: clauses,
            unitLiterals: unitLiterals,
            pureLiterals: pureLiterals,
            containsEmptyClause: false,
            literalsCount: literalsCount);
    }
}