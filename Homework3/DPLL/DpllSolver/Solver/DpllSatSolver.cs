using Dpll.Extensions;
using Dpll.SatModels;

namespace Dpll.Solver;

public class DpllSatSolver : ISatSolver
{
    public (SatResult result, Clause? satSuit) Solve(string path)
    {
        var file = File.Open(path, FileMode.Open);
        var streamReader = new StreamReader(file);
        var satFormula = streamReader.ToSatFormula();
        var emptyClause = new Clause(new List<int>());
        return Solve(satFormula, emptyClause);
    }

    public (SatResult result, Clause? satSuit) Solve(SatFormula satFormula, Clause result)
    {
        if (satFormula.ContainsEmptyClause)
        {
            return (SatResult.Unsat, null);
        }

        if (satFormula.Clauses.Count == 0)
        {
            return (SatResult.Sat, result);
        }

        while (satFormula.ContainsUnitLiteral)
        {
            var unitLiteral = satFormula.UnitLiterals.First();
            var simplifiedFormula = satFormula.SimplifyFormulaAssigningLiteral(
                unitLiteral,
                unitLiteral > 0);
            return Solve(simplifiedFormula, new Clause(result.Literals.Append(unitLiteral).ToList()));
        }

        while (satFormula.ContainsPureLiteral)
        {
            var pureLiteral = satFormula.PureLiterals.First();
            var simplifiedFormula = satFormula.SimplifyFormulaAssigningLiteral(
                pureLiteral,
                pureLiteral > 0);
            return Solve(simplifiedFormula, new Clause(result.Literals.Append(pureLiteral).ToList()));
        }

        var firstLiteralAbs = Math.Abs(satFormula.Clauses[0].Literals[0]);
        var formulaWithFalseLiteral = satFormula.SimplifyFormulaAssigningLiteral(firstLiteralAbs, false);
        var (falseResult, falseSatSuit) = Solve(formulaWithFalseLiteral, new Clause(result.Literals.Append(-firstLiteralAbs).ToList()));
        if (falseResult == SatResult.Sat)
        {
            return (falseResult, falseSatSuit);
        }

        var formulaWithTrueLiteral = satFormula.SimplifyFormulaAssigningLiteral(firstLiteralAbs, true);
        var (trueResult, trueTestSuit) = Solve(formulaWithTrueLiteral, new Clause(result.Literals.Append(firstLiteralAbs).ToList()));
        return trueResult == SatResult.Sat ? (trueResult, trueTestSuit) : (SatResult.Unsat, null);
    }
}

