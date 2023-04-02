using Dpll.SatModels;

namespace Dpll.Solver;

public interface ISatSolver
{
    (SatResult result, List<int> satSuit) Solve(string path);
}