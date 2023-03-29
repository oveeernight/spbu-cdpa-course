using Dpll.SatModels;

namespace Dpll.Solver;

public interface ISatSolver
{
    (SatResult result, Clause? satSuit) Solve(string path);
}