using System.Collections;
using Dpll.Extensions;
using Dpll.SatModels;

namespace Dpll.Solver;

public class DpllSatSolver : SatSolver
{
    private Stack<int> assignedLiterals = new();
    
    public (SatResult result, Clause? satSuit) Solve(string path)
    {
        var file = File.Open(path, FileMode.Open);
        var streamReader = new StreamReader(file);
        var satFormula = streamReader.ToSatFormula();
        return Solve(satFormula);
    }

    public (SatResult result, Clause? satSuit) Solve(SatFormula satFormula)
    {
        if (satFormula.ContainsEmptyClause)
        {
            return (SatResult.Unsat, null);
        }

        if (satFormula.ClausesCount == 0)
        {
            return (SatResult.Sat, )
        }
        var formulaWithFalseLiteral = satFormula.CreateFormulaAssigningFirstLiteral();
        var (falseResult, falseSatSuit) = Solve(formulaWithFalseLiteral);
        if (falseResult == SatResult.Sat){}
        var formulaWithTrueLiteral = satFormula.CreateFormulaAssigningFirstLiteral(true);
        var simplifiedWithTrue = Solve(formulaWithTrueLiteral);
        


    }
    
    
}

