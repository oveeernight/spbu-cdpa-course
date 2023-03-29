using Dpll.SatModels;
using Dpll.Solver;

var path = args[0];
ISatSolver solver = new DpllSatSolver();
var (result, suit) = solver.Solve(path);
if (result == SatResult.Sat && result != null)
{
    Console.WriteLine("SAT");
    Console.WriteLine(string.Join(' ', suit?.Literals));
}
else
{
    Console.WriteLine("UNSAT");
}

