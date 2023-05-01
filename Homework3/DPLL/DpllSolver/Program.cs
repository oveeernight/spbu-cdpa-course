using System.Diagnostics;
using Dpll.SatModels;
using Dpll.Solver;

var path = args[0];
ISatSolver solver = new DpllSatSolver();
var timer = new Stopwatch();
timer.Start();
var (result, suit) = solver.Solve(path);
var elapsed = timer.Elapsed;
if (result == SatResult.Sat && result != null)
{
    Console.WriteLine("SAT");
    Console.WriteLine(string.Join(' ', suit));
}
else
{
    Console.WriteLine("UNSAT");
}

Console.WriteLine($"{elapsed.TotalSeconds:00}ss::{elapsed.Milliseconds:00}ms");



