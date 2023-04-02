﻿using Dpll.Extensions;
using Dpll.SatModels;

namespace Dpll.Solver;

public class DpllSatSolver : ISatSolver
{
    public (SatResult result, List<int>? satSuit) Solve(string path)
    {
        var file = File.Open(path, FileMode.Open);
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

        if (satFormula.PureLiterals.Count + satFormula.UnitLiterals.Count > 0)
        {
            var univocalLiterals = satFormula.PureLiterals.Union(satFormula.UnitLiterals).ToHashSet();
            var (simplifiedFormula, updatedAccumulator) =
                satFormula.SimplifyAssigningUnivocalLiterals(univocalLiterals, accumulator);
            return Solve(simplifiedFormula, updatedAccumulator);
        }

        var firstLiteral = satFormula.Clauses[0].Literals[0];
        var (formulaWithFalseLiteral, updatedWithFalseAccumulator) = satFormula.SimplifyAssigningLiteral(firstLiteral, false, accumulator);
        var (falseResult, falseSatSuit) = Solve(formulaWithFalseLiteral, updatedWithFalseAccumulator);
        if (falseResult == SatResult.Sat)
        {
            return (falseResult, falseSatSuit);
        }

        var (formulaWithTrueLiteral, updatedWithTrueAccumulator) = satFormula.SimplifyAssigningLiteral(firstLiteral, true, accumulator);
        var (trueResult, trueTestSuit) = Solve(formulaWithTrueLiteral, updatedWithTrueAccumulator);
        return trueResult == SatResult.Sat ? (trueResult, trueTestSuit) : (SatResult.Unsat, null);
    }
}

