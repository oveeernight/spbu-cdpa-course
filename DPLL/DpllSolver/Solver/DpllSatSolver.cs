using Dpll.Extensions;
using Dpll.SatModels;

namespace Dpll.Solver;

public class DpllSatSolver : ISatSolver
{
    public (SatResult result, List<int>? satSuit) Solve(string path)
    {
        using var file = File.Open(path, FileMode.Open);
        var streamReader = new StreamReader(file);
        var satFormula = streamReader.ToSatFormula();
        var result = new List<int>();
        return Solve(satFormula, result);
    }

    private (SatResult result, List<int>? satSuit) Solve(SatFormula satFormula, List<int> accumulator)
    {
        if (satFormula.ContainsEmptyClause)
        {
            return (SatResult.Unsat, null);
        }

        if (satFormula.Clauses.Count == 0)
        {
            return (SatResult.Sat, accumulator);
        }

        if (satFormula.ContainsPureLiteral || satFormula.ContainsUnitLiteral)
        {
            satFormula.SimplifyAssigningUnivocalLiterals(accumulator);
            return Solve(satFormula, accumulator);
        }

        var firstLiteral = satFormula.Clauses[0].Literals[0];
        var (formulaWithFalseLiteral, updatedWithFalseAccumulator) = satFormula.SimplifyAssigningLiteral(firstLiteral, false, accumulator);
        var (falseResult, falseSatSuit) = Solve(formulaWithFalseLiteral, updatedWithFalseAccumulator);
        if (falseResult == SatResult.Sat)
        {
            return (falseResult, falseSatSuit);
        }

        var (formulaWithTrueLiteral, updatedWithTrueAccumulator) = satFormula.SimplifyAssigningLiteral(
            firstLiteral,
            assignedValue: true,
            accumulator,
            copyByValue: false);
        var (trueResult, trueTestSuit) = Solve(formulaWithTrueLiteral, updatedWithTrueAccumulator);
        return trueResult == SatResult.Sat ? (trueResult, trueTestSuit) : (SatResult.Unsat, null);
    }
}

