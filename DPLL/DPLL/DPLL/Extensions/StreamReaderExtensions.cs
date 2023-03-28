using Dpll.SatModels;

namespace Dpll.Extensions;

public static class FileStreamExtensions
{
    public static SatFormula ToSatFormula(this StreamReader streamReader)
    {
        var configuration = streamReader.ReadLine() ?? string.Empty;
        var parsedConfiguration = configuration.Split().Skip(2).Select(int.Parse).ToArray();
        var (literalsCount, clausesCount) = (parsedConfiguration[0], parsedConfiguration[1]);
        var clauses = new List<Clause>();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line == null || line[0] == 'c')
            {
                continue;
            }
            
            clauses.Add(line.ToClause());
        }

        return new SatFormula(clauses, literalsCount, clausesCount);
    }
}