using Dpll.SatModels;

namespace Dpll.Solver;

public interface SatSolver
{
    (SatResult result, Clause? satSuit) Solve(string path);
}